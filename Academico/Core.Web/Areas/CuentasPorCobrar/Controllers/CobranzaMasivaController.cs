using Core.Bus.Academico;
using Core.Bus.Banco;
using Core.Bus.Contabilidad;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.CuentasPorCobrar;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.CuentasPorCobrar.Controllers
{
    public class CobranzaMasivaController : Controller
    {
        #region Variables
        cxc_CobroMasivo_List Lista_CobroMasivo = new cxc_CobroMasivo_List();
        cxc_CobroMasivoDet_List Lista_CobroMasivoDet = new cxc_CobroMasivoDet_List();
        cxc_CobroMasivo_Bus bus_cobro_masivo = new cxc_CobroMasivo_Bus();
        cxc_CobroMasivoDet_Bus bus_cobro_masivo_det = new cxc_CobroMasivoDet_Bus();
        cxc_cobro_tipo_Bus bus_cobro_tipo = new cxc_cobro_tipo_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_banco_Bus bus_banco = new tb_banco_Bus();
        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        tb_TarjetaCredito_Bus bus_tarjeta = new tb_TarjetaCredito_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now.Date
            };

            List<cxc_CobroMasivo_Info> lista = bus_cobro_masivo.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista_CobroMasivo.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            List<cxc_CobroMasivo_Info> lista = bus_cobro_masivo.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista_CobroMasivo.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CobranzaMasiva(DateTime? Fecha_ini, DateTime? Fecha_fin, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<cxc_CobroMasivo_Info> model = Lista_CobroMasivo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            return PartialView("_GridViewPartial_CobranzaMasiva", model);
        }
        #endregion

        #region Metodos
        private bool validar(cxc_CobroMasivo_Info info, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.Fecha, cl_enumeradores.eModulo.FAC, info.IdSucursal, ref msg))
            {
                return false;
            }

            switch (info.IdCobro_tipo)
            {
                case "DEPO":
                    if (info.IdBanco == null)
                    {
                        msg = "El campo cuenta bancaria es obligatorio para depositos";
                        return false;
                    }
                    info.cr_Banco = null;
                    info.IdTarjeta = null;
                    info.cr_Tarjeta = null;
                    break;
                case "TARJ":
                    if (info.IdTarjeta == null || string.IsNullOrEmpty(info.cr_Tarjeta))
                    {
                        msg = "El campo tarjeta de crédito es obligatorio";
                        return false;
                    }
                    info.cr_Banco = null;
                    info.IdBanco = null;
                    break;
                case "CHQF":
                    if (string.IsNullOrEmpty(info.cr_Banco))
                    {
                        msg = "El campo banco es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(info.cr_cuenta))
                    {
                        msg = "El campo cuenta es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(info.cr_NumDocumento))
                    {
                        msg = "El campo # cheque es obligatorio para cheques";
                        return false;
                    }
                    info.IdBanco = null;
                    //i_validar.cr_Banco = null;
                    info.IdTarjeta = null;
                    info.cr_Tarjeta = null;
                    break;

                case "CHQV":
                    if (string.IsNullOrEmpty(info.cr_Banco))
                    {
                        msg = "El campo banco es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(info.cr_cuenta))
                    {
                        msg = "El campo cuenta es obligatorio para cheques";
                        return false;
                    }
                    if (string.IsNullOrEmpty(info.cr_NumDocumento))
                    {
                        msg = "El campo # cheque es obligatorio para cheques";
                        return false;
                    }
                    info.IdBanco = null;
                    //i_validar.cr_Banco = null;
                    info.IdTarjeta = null;
                    info.cr_Tarjeta = null;
                    break;
                default:
                    info.IdBanco = null;
                    info.cr_Banco = null;
                    info.IdTarjeta = null;
                    info.cr_Tarjeta = null;
                    break;
            }

            if (info.lst_det.Count() == 0)
            {
                msg = "Debe de ingresar al menos 1 item válido en el detalle";
                return false;
            }

            if (info.lst_det.Where(q=>q.ExisteAlumno==false).Count()>0)
            {
                msg = "Existen estudiantes no registrados en el sistema";
                return false;
            }

            if (info.lst_det.Where(q => q.Repetido == true).Count() > 0)
            {
                msg = "Existen estudiantes repetidos en el detalle";
                return false;
            }

            if (info.lst_det.Where(q => q.ValorIgual == false).Count() > 0)
            {
                msg = "Existen pagos que no concuerdan con el valor adeudado";
                return false;
            }

            return true;
        }

        private void cargar_combos(int IdEmpresa, int IdSucursal)
        {
            var lst_cobro_tipo = bus_cobro_tipo.get_list(false);
            lst_cobro_tipo = lst_cobro_tipo.Where(q => q.IdMotivo_tipo_cobro != "RET" && !q.IdCobro_tipo.StartsWith("CRU") && !q.IdCobro_tipo.StartsWith("NT") && !q.IdCobro_tipo.StartsWith("NC") && !q.IdCobro_tipo.StartsWith("TRAN_CLI") && !q.IdCobro_tipo.StartsWith("CHQF")).ToList();
            ViewBag.lst_cobro_tipo = lst_cobro_tipo;

            var lst_banco = bus_banco.get_list(false);
            ViewBag.lst_banco = lst_banco;

            var lst_banco_cuenta = bus_banco_cuenta.get_list(IdEmpresa, IdSucursal, false);
            ViewBag.lst_banco_cuenta = lst_banco_cuenta;

            var lst_tarjeta = bus_tarjeta.GetList(IdEmpresa, false);
            ViewBag.lst_tarjeta = lst_tarjeta;
        }
        #endregion

        #region CargaDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_CobranzaMasivaDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_CobroMasivoDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CobranzaMasivaDet", model);
        }

        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
        #endregion

        #region Json
        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "CobranzaMasiva", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            cxc_CobroMasivo_Info model = new cxc_CobroMasivo_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                Fecha = DateTime.Now.Date,
                IdCobro_tipo = "EFEC",
                lst_det = new List<cxc_CobroMasivoDet_Info>(),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            Lista_CobroMasivoDet.set_list(model.lst_det, model.IdTransaccionSession);
            cargar_combos(model.IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cxc_CobroMasivo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario.ToString();
            var ListaDetalle = Lista_CobroMasivoDet.get_list(model.IdTransaccionSession);

            model.lst_det = ListaDetalle.ToList();

            if (!validar(model, ref mensaje))
            {
                Lista_CobroMasivoDet.set_list(Lista_CobroMasivoDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                return View(model);
            }

            if (!bus_cobro_masivo.GuardarDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                Lista_CobroMasivoDet.set_list(Lista_CobroMasivoDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";

                return View(model);
            };

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCobroMasivo = model.IdCobroMasivo, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, decimal IdCobroMasivo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_CobroMasivo_Info model = bus_cobro_masivo.GetInfo(IdEmpresa, IdCobroMasivo);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_cobro_masivo_det.GetList(IdEmpresa, IdCobroMasivo);
            Lista_CobroMasivoDet.set_list(model.lst_det, model.IdTransaccionSession);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.CXC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            #region Permisos
            var MostrarSRI = true;
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "CobranzaMasiva", "Index");
            if (model.Estado == false)
            {
                info.Anular = false;
                info.Modificar = false;
                MostrarSRI = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Anular = info.Anular;
            ViewBag.MostrarSRI = MostrarSRI;
            #endregion

            cargar_combos(model.IdEmpresa, model.IdSucursal);
            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdCobroMasivo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_CobroMasivo_Info model = bus_cobro_masivo.GetInfo(IdEmpresa, IdCobroMasivo);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "CobranzaMasiva", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_cobro_masivo_det.GetList(IdEmpresa, IdCobroMasivo);
            Lista_CobroMasivoDet.set_list(model.lst_det, model.IdTransaccionSession);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.CXC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            cargar_combos(model.IdEmpresa, model.IdSucursal);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_CobroMasivo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            model.lst_det = Lista_CobroMasivoDet.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_cobro_masivo.AnularDB(model))
            {
                cargar_combos(model.IdEmpresa, model.IdSucursal);
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                Lista_CobroMasivoDet.set_list(Lista_CobroMasivoDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                return View(model);
            };

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class cxc_CobroMasivo_List
    {
        string Variable = "cxc_CobroMasivo_Info";
        public List<cxc_CobroMasivo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_CobroMasivo_Info> list = new List<cxc_CobroMasivo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_CobroMasivo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_CobroMasivo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cxc_CobroMasivoDet_List
    {
        string Variable = "cxc_CobroMasivoDet_Info";
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();

        public List<cxc_CobroMasivoDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_CobroMasivoDet_Info> list = new List<cxc_CobroMasivoDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_CobroMasivoDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_CobroMasivoDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class UploadControlSettings
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            List<cxc_CobroMasivoDet_Info> Lista_CobroMasivoDet = new List<cxc_CobroMasivoDet_Info>();
            cxc_CobroMasivoDet_List List_CobroMasivoDet = new cxc_CobroMasivoDet_List();
            aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
            tb_persona_Bus bus_persona = new tb_persona_Bus();
            aca_Familia_Bus bus_familia = new aca_Familia_Bus();
            fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
            cxc_cobro_det_Bus bus_cobro_det = new cxc_cobro_det_Bus();

            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                var lista = List_CobroMasivoDet.get_list(IdTransaccionSession);
                var Secuencia = lista.Count == 0 ? 1 : lista.Max(q => q.Secuencia) + 1;
                Lista_CobroMasivoDet.AddRange(List_CobroMasivoDet.get_list(IdTransaccionSession));

                #region Detalle   
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var CodigoAlumno = (Convert.ToString(reader.GetValue(0))).Trim();
                        var Valor = (Convert.ToDouble(reader.GetValue(1)));
                        var Fecha = (Convert.ToDateTime(reader.GetValue(2)));
                        var ExisteAlumno = true;
                        var Repetido = false;
                        var ValorIgual = false;

                        #region Alumno
                        var info_alumno = bus_alumno.GetInfo_Codigo(IdEmpresa, CodigoAlumno);

                        if (info_alumno == null)
                        {
                            ExisteAlumno = false;
                        }
                        #endregion

                        #region Cliente
                        var infoRepEconomico = bus_familia.GetInfo_Representante(IdEmpresa, (info_alumno==null ? 0 : Convert.ToDecimal(info_alumno.IdAlumno)), cl_enumeradores.eTipoRepresentante.ECON.ToString());
                        var TieneCliente = true;
                        var info_cliente = bus_cliente.get_info_x_num_cedula(IdEmpresa, (infoRepEconomico == null ? "" : infoRepEconomico.pe_cedulaRuc));
                        if (info_cliente == null || info_cliente.IdCliente == 0)
                        {
                            TieneCliente = false;
                        }
                        #endregion

                        var ExisteRepetido = Lista_CobroMasivoDet.Where(q=>q.IdEmpresa==IdEmpresa && q.IdAlumno== (info_alumno==null ? 0 : info_alumno.IdAlumno) ).Count();
                        Repetido = (ExisteRepetido > 0 ? true : false);

                        var lst_AlumnoCartertaXCobrar = bus_cobro_det.get_list_cartera_x_alumno(IdEmpresa, IdSucursal, (info_alumno == null ? 0 : info_alumno.IdAlumno)).ToList();
                        var ValorCxC = Math.Round((Convert.ToDouble(lst_AlumnoCartertaXCobrar == null ? 0 : lst_AlumnoCartertaXCobrar.Sum(q => q.ValorProntoPago - q.dc_ValorProntoPago))),2, MidpointRounding.AwayFromZero);
                        ValorIgual = (Valor == ValorCxC ? true : false);

                        var info_det = new cxc_CobroMasivoDet_Info
                        {
                            IdEmpresa = IdEmpresa,
                            Secuencia = Secuencia++,
                            CodigoAlumno = (info_alumno == null ? "" : info_alumno.Codigo),
                            NombreAlumno = (info_alumno == null ? "" : info_alumno.pe_nombreCompleto),
                            IdAlumno = (info_alumno == null ? 0 : info_alumno.IdAlumno),
                            IdCliente = (info_cliente ==null ? 0 : info_cliente.IdCliente),
                            Fecha = Fecha.Date,
                            Valor = Valor,
                            ExisteAlumno = ExisteAlumno,
                            Repetido = Repetido,
                            ValorIgual = ValorIgual,
                            Error = ((ExisteAlumno==false || Repetido==true || ValorIgual==false) ? true : false),
                            ErrorDetalle = ((ExisteAlumno==false ? "No existe el código del estudiante" : (Repetido==true ? "Existe estudiante repetido" : (ValorIgual==false ? "El valor a cancelar debe ser igual al de la cartera por cobrar "+ ValorCxC.ToString("C2") : ""))))
                        };

                        Lista_CobroMasivoDet.Add(info_det);
                    }
                    else
                        cont++;
                }
                #endregion

                List_CobroMasivoDet.set_list(Lista_CobroMasivoDet, IdTransaccionSession);
            }
        }
    }
}