using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Bus.Inventario;
using Core.Info.Academico;
using Core.Info.Facturacion;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class NotaDebitoCreditoMasivaController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        fa_notaCreDeb_Masiva_List Lista = new fa_notaCreDeb_Masiva_List();
        fa_notaCreDeb_MasivaDet_List ListaDet = new fa_notaCreDeb_MasivaDet_List();
        fa_notaCreDeb_Bus bus_nota = new fa_notaCreDeb_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        string mensaje = string.Empty;
        fa_notaCreDeb_det_Bus bus_det = new fa_notaCreDeb_det_Bus();
        fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        fa_cliente_contactos_Bus bus_contacto = new fa_cliente_contactos_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        fa_notaCreDeb_Masiva_Bus bus_NotaMasiva = new fa_notaCreDeb_Masiva_Bus();
        fa_notaCreDeb_MasivaDet_Bus bus_NotaMasivaDet = new fa_notaCreDeb_MasivaDet_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Combo bajo demanda
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
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
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                fecha_ini = DateTime.Now.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            List<fa_notaCreDeb_Masiva_Info> lista = bus_NotaMasiva.Get_list(model.IdEmpresa, model.IdSucursal, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdEmpresa);

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
            List<fa_notaCreDeb_Masiva_Info> lista = bus_NotaMasiva.Get_list(model.IdEmpresa, model.IdSucursal, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdEmpresa);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_NotaDebitoCreditoMasiva(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<fa_notaCreDeb_Masiva_Info> model = Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            return PartialView("_GridViewPartial_NotaDebitoCreditoMasiva", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(fa_notaCreDeb_Masiva_Info model)
        {
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_punto_venta = bus_punto_venta.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_punto_venta = lst_punto_venta;

            Dictionary<string, string> lst_naturaleza = new Dictionary<string, string>();
            lst_naturaleza.Add("INT", "INTERNO");
            lst_naturaleza.Add("SRI", "SRI");
            ViewBag.lst_naturaleza = lst_naturaleza;

            Dictionary<string, string> lst_tipo = new Dictionary<string, string>();
            lst_tipo.Add("D", "DEBITO");
            lst_tipo.Add("C", "CREDITO");
            ViewBag.lst_tipo = lst_tipo;

            var lst_tipo_nota = bus_tipo_nota.get_list(model.IdEmpresa, model.CreDeb,false);
            ViewBag.lst_tipo_nota = lst_tipo_nota;

            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        private bool validar(fa_notaCreDeb_Masiva_Info info, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.no_fecha, cl_enumeradores.eModulo.FAC, info.IdSucursal, ref msg))
            {
                return false;
            }

            var info_tipo_nota = bus_tipo_nota.get_info(info.IdEmpresa, info.IdTipoNota);

            if (info_tipo_nota != null && info_tipo_nota.IdCtaCble != null && info_tipo_nota.IdCtaCbleCXC != null && info_tipo_nota.IdProducto != null)
            {
                info.IdCtaCble_TipoNota = info_tipo_nota.IdCtaCble;
            }
            else
            {
                msg = "Faltan parámetros por configurar en el tipo de nota";
                return false;
            }

            if (info.lst_det.Count() == 0)
            {
                msg = "Debe de ingresar al menos 1 item válido en el detalle";
                return false;
            }
            return true;
        }
        #endregion

        #region Json
        public JsonResult CargarPuntosDeVenta(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_punto_venta.get_list_x_tipo_doc(IdEmpresa, IdSucursal, cl_enumeradores.eTipoDocumento.NTDB.ToString());
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarTipoNota(string CreDeb = "")
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_tipo_nota.get_list(IdEmpresa, CreDeb, false);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutorizarSRI(int IdEmpresa, int IdNCMasivo)
        {
            var lista = bus_NotaMasivaDet.GetList(IdEmpresa, IdNCMasivo);
            string retorno = string.Empty;
            foreach (var item in lista)
            {
                if (!bus_nota.modificarEstadoAutorizacion(IdEmpresa, Convert.ToInt32(item.IdSucursal), Convert.ToInt32(item.IdBodega), Convert.ToInt32(item.IdNota)))
                {
                    retorno = "No se autorizaron todos los registros";
                }
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CargaDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_NotaDebitoCreditoMasivaDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_NotaDebitoCreditoMasivaDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_MasivaDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            info_det.IdEmpresa = IdEmpresa;
            #region Cliente
            bool TieneCliente = true;
            var infoRepEconomico = bus_familia.GetInfo_Representante(IdEmpresa, Convert.ToDecimal(info_det.IdAlumno), cl_enumeradores.eTipoRepresentante.ECON.ToString());

            var info_cliente = bus_cliente.get_info_x_num_cedula(IdEmpresa, (infoRepEconomico == null ? "" : infoRepEconomico.pe_cedulaRuc));
            if (info_cliente == null || info_cliente.IdCliente == 0)
            {
                TieneCliente = false;
            }
            info_det.TieneCliente = TieneCliente;
            #endregion
            if (info_det.TieneCliente == true)
            {
                info_det.IdCliente = info_cliente.IdCliente;
                if (ModelState.IsValid)
                    ListaDet.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }

            var model = ListaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
   
            return PartialView("_GridViewPartial_NotaDebitoCreditoMasivaDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_MasivaDet_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            #region Cliente
            bool TieneCliente = true;
            var infoRepEconomico = bus_familia.GetInfo_Representante(IdEmpresa, Convert.ToDecimal(info_det.IdAlumno), cl_enumeradores.eTipoRepresentante.ECON.ToString());

            var info_cliente = bus_cliente.get_info_x_num_cedula(IdEmpresa, (infoRepEconomico == null ? "" : infoRepEconomico.pe_cedulaRuc));
            if (info_cliente == null || info_cliente.IdCliente == 0)
            {
                TieneCliente = false;
            }
            info_det.TieneCliente = TieneCliente;
            #endregion

            if (info_det.TieneCliente == true)
            {
                info_det.IdCliente = info_cliente.IdCliente;
                if (ModelState.IsValid)
                    ListaDet.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }

            var model = ListaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_NotaDebitoCreditoMasivaDet", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            ListaDet.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
   
            return PartialView("_GridViewPartial_NotaDebitoCreditoMasivaDet", model);
        }
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
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
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            fa_notaCreDeb_Masiva_Info model = new fa_notaCreDeb_Masiva_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                no_fecha = DateTime.Now,
                no_fecha_venc = DateTime.Now,
                lst_det = new List<fa_notaCreDeb_MasivaDet_Info>(),
                CreDeb = "D",
                NaturalezaNota = "SRI",
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            ListaDet.set_list(model.lst_det, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_notaCreDeb_Masiva_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario.ToString();
            var ListaDetalleNC = ListaDet.get_list(model.IdTransaccionSession);

            model.lst_det = ListaDetalleNC.Where(q=>q.IdAlumno>0 && q.IdCliente>0).ToList();
            model.IdBodega = (int)bus_punto_venta.get_info(model.IdEmpresa, model.IdSucursal, Convert.ToInt32(model.IdPuntoVta)).IdBodega;

            if (!validar(model, ref mensaje))
            {
                ListaDet.set_list(ListaDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            
            if (!bus_NotaMasiva.GuardarDB(model))
            {
                ListaDet.set_list(ListaDet.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model);
                return View(model);
            };

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdNCMasivo = model.IdNCMasivo, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, decimal IdNCMasivo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_notaCreDeb_Masiva_Info model = bus_NotaMasiva.Get_info(IdEmpresa, IdNCMasivo);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_NotaMasivaDet.GetList(IdEmpresa, IdNCMasivo);
            ListaDet.set_list(model.lst_det, model.IdTransaccionSession);

            cargar_combos(model);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.no_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            if (model.Estado==false)
            {
                info.Anular = false;
                info.Modificar = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdNCMasivo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_notaCreDeb_Masiva_Info model = bus_NotaMasiva.Get_info(IdEmpresa, IdNCMasivo);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_NotaMasivaDet.GetList(IdEmpresa, IdNCMasivo);
            ListaDet.set_list(model.lst_det, model.IdTransaccionSession);

            cargar_combos(model);

            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.no_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_notaCreDeb_Masiva_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            model.lst_det = ListaDet.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }

            if (!bus_NotaMasiva.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos(model);
                return View(model);
            };

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class fa_notaCreDeb_Masiva_List
    {
        string Variable = "fa_notaCreDeb_Masiva_Info";
        public List<fa_notaCreDeb_Masiva_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_notaCreDeb_Masiva_Info> list = new List<fa_notaCreDeb_Masiva_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_notaCreDeb_Masiva_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_notaCreDeb_Masiva_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class fa_notaCreDeb_MasivaDet_List
    {
        string Variable = "fa_notaCreDeb_MasivaDet_Info";
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();

        public List<fa_notaCreDeb_MasivaDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_notaCreDeb_MasivaDet_Info> list = new List<fa_notaCreDeb_MasivaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_notaCreDeb_MasivaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_notaCreDeb_MasivaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(fa_notaCreDeb_MasivaDet_Info info_det, decimal IdTransaccion)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<fa_notaCreDeb_MasivaDet_Info> list = get_list(IdTransaccion);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            var info_alumno = bus_alumno.GetInfo(IdEmpresa ,info_det.IdAlumno);
            info_det.pe_cedulaRuc = info_alumno.pe_cedulaRuc;
            info_det.pe_nombreCompleto = info_alumno.pe_nombreCompleto;

            list.Add(info_det);
        }

        public void UpdateRow(fa_notaCreDeb_MasivaDet_Info info_det, decimal IdTransaccion)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<fa_notaCreDeb_MasivaDet_Info> list = get_list(IdTransaccion);
            fa_notaCreDeb_MasivaDet_Info edited_info = list.Where(m => m.Secuencia == info_det.Secuencia).FirstOrDefault();
            var info_alumno = bus_alumno.GetInfo(IdEmpresa, info_det.IdAlumno);

            edited_info.IdAlumno = info_det.IdAlumno;
            edited_info.pe_cedulaRuc = info_alumno.pe_cedulaRuc;
            edited_info.pe_nombreCompleto = info_alumno.pe_nombreCompleto;
            edited_info.Subtotal = info_det.Subtotal;
            edited_info.ObservacionDet = info_det.ObservacionDet;

        }

        public void DeleteRow(int Secuencia, decimal IdTransaccion)
        {
            List<fa_notaCreDeb_MasivaDet_Info> list = get_list(IdTransaccion);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
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
            List<fa_notaCreDeb_MasivaDet_Info> Lista_DetalleNotas = new List<fa_notaCreDeb_MasivaDet_Info>();
            fa_notaCreDeb_MasivaDet_List List_Detalle = new fa_notaCreDeb_MasivaDet_List();
            aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
            tb_persona_Bus bus_persona = new tb_persona_Bus();
            aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
            aca_Familia_Bus bus_familia = new aca_Familia_Bus();
            fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
            fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
            tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                var lista = List_Detalle.get_list(IdTransaccionSession);
                var Secuencia = lista.Count == 0 ? 1 : lista.Max(q => q.Secuencia) + 1;
                Lista_DetalleNotas.AddRange(List_Detalle.get_list(IdTransaccionSession));
               
                #region Detalle   
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var cedula_ruc_alumno = (Convert.ToString(reader.GetValue(0))).Trim();
                        var info_alumno = bus_alumno.get_info_x_num_cedula(IdEmpresa, cedula_ruc_alumno);
                        var TieneCliente = true;
                        #region Cliente
                        var infoRepEconomico = bus_familia.GetInfo_Representante(IdEmpresa, Convert.ToDecimal(info_alumno.IdAlumno), cl_enumeradores.eTipoRepresentante.ECON.ToString());

                        var info_cliente = bus_cliente.get_info_x_num_cedula(IdEmpresa, (infoRepEconomico==null ? "" : infoRepEconomico.pe_cedulaRuc));
                        if (info_cliente == null || info_cliente.IdCliente == 0)
                        {
                            TieneCliente = false;
                        }
                        #endregion

                        var SubtotalConDscto = Convert.ToDouble(reader.GetValue(2));

                        var info_det = new fa_notaCreDeb_MasivaDet_Info{
                            IdEmpresa = IdEmpresa,
                            Secuencia = Secuencia++,
                            IdAlumno = (info_alumno==null ? 0 : info_alumno.IdAlumno),
                            IdCliente = (info_cliente==null ?0 : info_cliente.IdCliente),
                            Subtotal = SubtotalConDscto,
                            ObservacionDet = Convert.ToString(reader.GetValue(3)),
                            TieneCliente = TieneCliente,
                            pe_nombreCompleto = info_alumno.pe_nombreCompleto,
                            pe_cedulaRuc = info_alumno.pe_cedulaRuc
                        };

                        Lista_DetalleNotas.Add(info_det);
                    }
                    else
                        cont++;
                }
                #endregion

                List_Detalle.set_list(Lista_DetalleNotas, IdTransaccionSession);
            }
        }
    }
}