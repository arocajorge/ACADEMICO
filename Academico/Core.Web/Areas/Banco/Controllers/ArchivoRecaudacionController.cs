using Core.Bus.Academico;
using Core.Bus.Banco;
using Core.Bus.Contabilidad;
using Core.Bus.General;
using Core.Bus.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.Banco;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Banco.Controllers
{
    public class ArchivoRecaudacionController : Controller
    {
        #region Variables
        string rutafile = System.IO.Path.GetTempPath();
        ba_ArchivoRecaudacion_Bus bus_archivo = new ba_ArchivoRecaudacion_Bus();
        ba_ArchivoRecaudacionDet_Bus bus_archivo_det = new ba_ArchivoRecaudacionDet_Bus();
        ba_ArchivoRecaudacion_List Lista = new ba_ArchivoRecaudacion_List();
        ba_ArchivoRecaudacionDet_List Lista_det = new ba_ArchivoRecaudacionDet_List();
        ba_ArchivoRecaudacionDet_Saldo_List Lista_det_Saldo = new ba_ArchivoRecaudacionDet_Saldo_List();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        tb_banco_procesos_bancarios_x_empresa_Bus bus_procesos_bancarios = new tb_banco_procesos_bancarios_x_empresa_Bus();
        ba_Banco_Cuenta_Bus bus_cuentas_bancarias = new ba_Banco_Cuenta_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_banco_Bus bus_banco = new tb_banco_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        string mensaje = string.Empty;

        ba_Banco_Cuenta_Bus bus_banco_cuenta = new ba_Banco_Cuenta_Bus();
        ba_parametros_Bus bus_param = new ba_parametros_Bus();
        ba_TipoFlujo_Bus bus_flujo = new ba_TipoFlujo_Bus();

        string MensajeSuccess = "La transacción se ha realizado con éxito";
        tb_ColaImpresionDirecta_Bus bus_impresion = new tb_ColaImpresionDirecta_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
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
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                fecha_ini = DateTime.Now.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            List<ba_ArchivoRecaudacion_Info> lista = bus_archivo.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Banco", "ArchivoRecaudacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            List<ba_ArchivoRecaudacion_Info> lista = bus_archivo.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Banco", "ArchivoRecaudacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ArchivoRecaudacion(DateTime? fecha_ini, DateTime? fecha_fin, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.fecha_ini = fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(fecha_ini);
            ViewBag.fecha_fin = fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(fecha_fin);

            List<ba_ArchivoRecaudacion_Info> model = Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ArchivoRecaudacion", model);

        }
        #endregion

        #region Metodos
        private bool validar(ba_ArchivoRecaudacion_Info i_validar, ref string msg)
        {
            if (!bus_periodo.ValidarFechaTransaccion(i_validar.IdEmpresa, i_validar.Fecha, cl_enumeradores.eModulo.BANCO, i_validar.IdSucursal, ref msg))
            {
                return false;
            }

            if (i_validar.Lst_det.Count == 0)
            {
                mensaje = "Debe ingresar registros en el detalle";
                return false;
            }

            var pro = bus_procesos_bancarios.get_info(i_validar.IdEmpresa, i_validar.IdProceso_bancario);
            //i_validar.Cod_Empresa = pro.Codigo_Empresa;

            return true;
        }
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var lst_cuenta_bancarias = bus_cuentas_bancarias.get_list(IdEmpresa, Convert.ToInt32(SessionFixed.IdSucursal), false);
            ViewBag.lst_cuenta_bancarias = lst_cuenta_bancarias;

            var lst_proceso = bus_procesos_bancarios.get_list(IdEmpresa, false);
            ViewBag.lst_proceso = lst_proceso;
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
            ba_ArchivoRecaudacion_Info model = new ba_ArchivoRecaudacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                Fecha = DateTime.Now,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                Lst_det = new List<ba_ArchivoRecaudacionDet_Info>(),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            Lista_det.set_list(model.Lst_det, model.IdTransaccionSession);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Banco", "ArchivoRecaudacion", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(ba_ArchivoRecaudacion_Info model)
        {
            model.Nom_Archivo = "COBROS_MULTICASH";
            var anio = (Convert.ToDateTime(model.Fecha).Year).ToString();
            var mes = ((Convert.ToDateTime(model.Fecha).Month).ToString());
            var dia = ((Convert.ToDateTime(model.Fecha).Day).ToString());
            if (model.IdProceso_bancario == Convert.ToInt32(cl_enumeradores.eTipoProcesoBancarioCobrosAcademico.RECBG))
            {
                model.Nom_Archivo = "RCE_"+ anio + mes.PadLeft(2,'0') + dia.PadLeft(2, '0') + "_PAH";
            }

            if (model.IdProceso_bancario == Convert.ToInt32(cl_enumeradores.eTipoProcesoBancarioCobrosAcademico.RECPB))
            {
                model.Nom_Archivo = "PRODUBANCO"+ anio + mes.PadLeft(2, '0') + dia.PadLeft(2, '0');
            }

            if (model.IdProceso_bancario == Convert.ToInt32(cl_enumeradores.eTipoProcesoBancarioCobrosAcademico.RECBB))
            {
                model.Nom_Archivo = "ALUMNOS094";
            }

            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.Lst_det = Lista_det.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }
            if (!bus_archivo.GuardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdArchivo = model.IdArchivo, Exito = true });
        }
        public ActionResult Consultar(int IdEmpresa = 0, decimal IdArchivo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_ArchivoRecaudacion_Info model = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Banco", "ArchivoRecaudacion", "Index");
            if (model.Estado == false)
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_archivo_det.GetList(model.IdEmpresa, model.IdArchivo);
            Lista_det.set_list(model.Lst_det, model.IdTransaccionSession);

            cargar_combos();

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            return View(model);
        }
        public ActionResult Modificar(int IdEmpresa = 0, decimal IdArchivo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ba_ArchivoRecaudacion_Info model = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Banco", "ArchivoRecaudacion", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_archivo_det.GetList(model.IdEmpresa, model.IdArchivo);
            Lista_det.set_list(model.Lst_det, model.IdTransaccionSession);

            cargar_combos();

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ba_ArchivoRecaudacion_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.Lst_det = Lista_det.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }
            if (!bus_archivo.ModificarDB(model))
            {
                cargar_combos();
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdArchivo = model.IdArchivo, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdArchivo = 0)
        {
            ba_ArchivoRecaudacion_Info model = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Banco", "ArchivoRecaudacion", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.Lst_det = bus_archivo_det.GetList(model.IdEmpresa, model.IdArchivo);
            Lista_det.set_list(model.Lst_det, model.IdTransaccionSession);
            cargar_combos();

            #region Validacion Periodo CXC
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.Fecha, cl_enumeradores.eModulo.BANCO, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ba_ArchivoRecaudacion_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_archivo.AnularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Detalle Archivo
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ArchivoRecaudacionDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = Lista_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ArchivoRecaudacionDet", model);
        }

        public ActionResult GridViewPartial_ArchivoRecaudacion_Saldo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = Lista_det_Saldo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ArchivoRecaudacion_Saldo", model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditingAddNew(string IDs = "", decimal IdTransaccionSession = 0, int IdEmpresa = 0)
        {
            if (IDs != "")
            {
                string[] array = IDs.Split(',');
                var Lista = Lista_det.get_list(IdTransaccionSession);

                var ListaSaldo = Lista_det_Saldo.get_list(IdTransaccionSession);
                foreach (var item in array)
                {
                    var info_det = ListaSaldo.Where(q => q.IdEmpresa==IdEmpresa && q.IdAlumno == Convert.ToInt32(item)).FirstOrDefault();
                    if (info_det != null)
                    {
                        info_det.Valor = Convert.ToDouble(info_det.Saldo);
                        info_det.ValorProntoPago = Convert.ToDouble(info_det.SaldoProntoPago);
                        info_det.FechaProntoPago = info_det.FechaProntoPago;
                        Lista_det.AddRow(info_det, IdTransaccionSession);
                    }
                }
            }
            var model = Lista_det.get_list(IdTransaccionSession);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] ba_ArchivoRecaudacionDet_Info info_det)
        {

            if (ModelState.IsValid)
                Lista_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ArchivoRecaudacionDet", model);
        }
        public ActionResult EditingDelete(int Secuencia)
        {
            Lista_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ArchivoRecaudacionDet", model);
        }
        #endregion

        #region Json
        public JsonResult GetListPorCruzar(int IdEmpresa = 0, decimal IdTransaccionSession = 0, int IdSucursal = 0)
        {
            var lst = bus_archivo_det.GetList_ConSaldo(IdEmpresa);
            Lista_det_Saldo.set_list(lst, IdTransaccionSession);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetValor(decimal IdTransaccionSession = 0)
        {
            var Lista = Lista_det.get_list(IdTransaccionSession);

            double Valor = Math.Round(Lista.Sum(q => q.Valor), 2, MidpointRounding.AwayFromZero);
            double ValorProntoPago = Math.Round(Lista.Sum(q => q.ValorProntoPago), 2, MidpointRounding.AwayFromZero);
            return Json(new { Valor = Valor, ValorProntoPago= ValorProntoPago }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Archivo

        public FileResult get_archivo(int IdEmpresa = 0, int IdArchivo = 0)
        {
            byte[] archivo;
            ba_ArchivoRecaudacion_Bus bus_tipo_file = new ba_ArchivoRecaudacion_Bus();

            var info_archivo = bus_archivo.GetInfo(IdEmpresa, IdArchivo);
            info_archivo.Lst_det = bus_archivo_det.GetList_Archivo(IdEmpresa, IdArchivo);
            var NombreArchivo = info_archivo.Nom_Archivo + "_" +info_archivo.SecuencialDescarga;

            archivo = GetArchivo(info_archivo, NombreArchivo);
            info_archivo.IdUsuarioModificacion = SessionFixed.IdUsuario;
            bus_archivo.ModificarSecuenciaDescargaDB(info_archivo);

            return File(archivo, "application/xml", NombreArchivo + ".txt");
        }

        private byte[] GetMulticash(ba_ArchivoRecaudacion_Info info, string NombreArchivo)
        {
            try
            {
                System.IO.File.Delete(rutafile + NombreArchivo + ".txt");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(rutafile + NombreArchivo + ".txt", true))
                {
                    var Lista = info.Lst_det;
                    DateTime FechaIni = Convert.ToDateTime("01" + "-" + (Convert.ToDateTime(info.Fecha).Month) + "-" + (Convert.ToDateTime(info.Fecha).Year));
                    DateTime FechaFin = Convert.ToDateTime(FechaIni.AddDays(-1).Day + "-" + FechaIni.Month + "-" + FechaIni.Year);

                    #region PRODUBANCO
                    if (info.IdProceso_bancario == Convert.ToInt32(cl_enumeradores.eTipoProcesoBancarioCobrosAcademico.RECPB))
                    {
                        foreach (var item in Lista)
                        {
                            string linea1 = "";
                            string linea2 = "";
                            double Valor = 0;
                            double valorEntero = 0;
                            double valorDecimal = 0;

                            Valor = (info.Fecha<= item.FechaProntoPago ? Convert.ToDouble(item.ValorProntoPago) : Convert.ToDouble(item.Valor));
                            valorEntero = Math.Floor(Valor);
                            valorDecimal = Convert.ToDouble((Valor - valorEntero).ToString("N2")) * 100;

                            linea1 += "CO" + "\t";
                            linea1 += string.IsNullOrEmpty(item.ba_Num_Cuenta) ? "" : item.ba_Num_Cuenta.PadLeft(11, '0') + "\t";
                            linea1 += item.Secuencia.ToString() + "\t";
                            linea1 += "\t";//COMPROBANTE DE COBRO
                            linea1 += item.CodigoAlumno + "\t";//CONTRAPARTIDA
                            linea1 += "USD" + "\t";
                            linea1 += (valorEntero.ToString() + valorDecimal.ToString().PadRight(2, '0')).PadLeft(13, '0') + "\t";
                            linea1 += "REC" + "\t";//TIPO DE PAGO
                            linea1 += item.CodigoLegal.ToString().PadLeft(4, '0') + "\t";
                            linea1 += "\t";//SOLO SI ES TIPO DE PAGO ES CUENTA
                            linea1 += "\t";//SOLO SI ES TIPO DE PAGO ES CUENTA
                            linea1 += (item.IdTipoDocumento == "CED" ? "C" : (item.IdTipoDocumento == "RUC" ? "R" : "P")) + "\t";
                            linea1 += item.pe_cedulaRuc.Trim() + "\t";
                            linea1 += (string.IsNullOrEmpty(item.pe_nombreCompleto) ? "" : (item.pe_nombreCompleto.Length > 40 ? item.pe_nombreCompleto.Substring(0, 40) : item.pe_nombreCompleto.Trim())).PadRight(40, ' ') + "\t";
                            linea1 += "\t";//Direccion;
                            linea1 += "\t";//Ciudad
                            linea1 += "\t";//Telefono
                            linea1 += "\t";//Localidad
                            //var Referencia = string.Empty;
                            //linea1 += (string.IsNullOrEmpty(Referencia) ? "" : (Referencia.Length > 200 ? Referencia.Substring(0, 200) : Referencia.Trim())) + "\t";
                            linea1 += item.CodigoAlumno + "\t";//REFERENCIA
                            linea1 += item.CodigoAlumno + "\t";//REFERENCIA adicional
                            linea1 += "\t";//Base Iva 0%
                            linea1 += "\t";//Base ICE
                            linea1 += "\t";//

                            file.WriteLine(linea1);

                            linea2 += "RC" + "\t";
                            linea2 += item.Secuencia.ToString() + "\t";
                            linea2 += "PP" + "\t";//COMPROBANTE DE PAGO
                            linea2 += FechaIni.Day.ToString().PadLeft(2, '0') + FechaIni.Month.ToString().PadLeft(2, '0') + FechaIni.Year.ToString() + "\t";//DESDE
                            //linea2 += Convert.ToDateTime(item.FechaProntoPago).Day.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.FechaProntoPago).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.FechaProntoPago).Year.ToString() + "\t";//HASTA
                            linea2 += FechaFin.Day.ToString().PadLeft(2, '0') + FechaFin.Month.ToString().PadLeft(2, '0') + FechaFin.Year.ToString() + "\t";//HASTA
                            linea2 += "C" + "\t";
                            linea2 += "FI" + "\t";
                            linea2 += (valorEntero.ToString() + valorDecimal.ToString().PadRight(2, '0')).PadLeft(13, '0') + "\t";
                            linea2 += "0".PadLeft(13, '0') + "\t";
                            linea2 += (valorEntero.ToString() + valorDecimal.ToString().PadRight(2, '0')).PadLeft(5, '0');//ESTE CAMPO NO ESTA EN LA FICHA

                            file.WriteLine(linea2);
                        }
                    }
                    #endregion

                    #region GUAYAQUIL
                    if (info.IdProceso_bancario == Convert.ToInt32(cl_enumeradores.eTipoProcesoBancarioCobrosAcademico.RECBG))
                    {
                        foreach (var item in Lista)
                        {
                            string linea1 = "";
                            string linea2 = "";
                            string linea3 = "";
                            double Valor = 0;
                            double valorEntero = 0;
                            double valorDecimal = 0;

                            Valor = (item.Fecha <= item.FechaProntoPago ? Convert.ToDouble(item.ValorProntoPago) : Convert.ToDouble(item.Valor));
                            valorEntero = Math.Floor(Valor);
                            valorDecimal = Convert.ToDouble((Valor - valorEntero).ToString("N2")) * 100;

                            linea1 += "CO";
                            linea1 += item.Secuencia.ToString().PadLeft(7, '0');
                            linea1 += item.CodigoAlumno.PadRight(15, ' ');
                            linea1 += "USD";
                            linea1 += (valorEntero.ToString() + valorDecimal.ToString().PadRight(2, '0')).PadLeft(10, '0'); // valor dependiendo si aplica o no descuento
                            linea1 += "REC";
                            linea1 += (string.IsNullOrEmpty(item.pe_nombreCompleto) ? "" : (item.pe_nombreCompleto.Length > 40 ? item.pe_nombreCompleto.Substring(0, 40) : item.pe_nombreCompleto.Trim())).PadRight(40, ' ');
                            linea1 += item.Fecha.Year.ToString() + item.Fecha.Month.ToString().PadLeft(2, '0');
                            linea1 += "SN";//Curso;
                            linea1 += "SN";//Paralelo
                            linea1 += "SN";//Especialidad
                            linea1 += item.CodigoAlumno.PadRight(15, ' ');

                            file.WriteLine(linea1);

                            var ValorDiferencia = Convert.ToDouble(item.Valor) - Convert.ToDouble(item.ValorProntoPago);
                            var valorEnteroDiferencia = Math.Floor(ValorDiferencia);
                            var valorDecimalDiferencia = Convert.ToDouble((ValorDiferencia - valorEnteroDiferencia).ToString("N2")) * 100;

                            linea2 += "RC";
                            linea2 += item.Secuencia.ToString().PadLeft(7, '0');
                            linea2 += "PP";
                            linea2 += FechaIni.Year.ToString() + FechaIni.Month.ToString().PadLeft(2, '0') + FechaIni.Day.ToString().PadLeft(2, '0');//DESDE
                            linea2 += Convert.ToDateTime(item.FechaProntoPago).Year.ToString() + Convert.ToDateTime(item.FechaProntoPago).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.FechaProntoPago).Day.ToString().PadLeft(2, '0');// HASTA
                            linea2 += "FI";
                            linea2 += "0000000000";
                            linea2 += "0000000000";
                            linea2 += "0000000000";// siempre va 0

                            file.WriteLine(linea2);

                            linea3 += "RC";
                            linea3 += item.Secuencia.ToString().PadLeft(7, '0');
                            linea3 += "VM";
                            linea3 += FechaIni.Year.ToString() + FechaIni.Month.ToString().PadLeft(2, '0') + FechaIni.Day.ToString().PadLeft(2, '0');//DESDE
                            linea3 += FechaFin.Year.ToString() + FechaFin.Month.ToString().PadLeft(2, '0') + FechaFin.Day.ToString().PadLeft(2, '0');//HASTA                                                                                                                         
                            //linea3 += Convert.ToDateTime(item.FechaProntoPago).Year.ToString() + Convert.ToDateTime(item.FechaProntoPago).Month.ToString().PadLeft(2, '0') + Convert.ToDateTime(item.FechaProntoPago).Day.ToString().PadLeft(2, '0');// HASTA
                            linea3 += "FI";
                            linea3 += "0000000000";
                            linea3 += "0000000000";
                            linea3 += "0000000000";// siempre va 0

                            file.WriteLine(linea3);
                        }
                    }
                    #endregion

                    #region BOLIVARIANO
                    if (info.IdProceso_bancario == Convert.ToInt32(cl_enumeradores.eTipoProcesoBancarioCobrosAcademico.RECBB))
                    {
                        string linea1 = "";

                        linea1 += "999";
                        linea1 += "094";
                        linea1 += "0".PadRight(12, ' ');
                        linea1 += info.Fecha.Month.ToString().PadLeft(2, '0') + "/" + info.Fecha.Day.ToString().PadLeft(2, '0') + "/" + info.Fecha.Year.ToString();
                        file.WriteLine(linea1);

                        foreach (var item in Lista)
                        {
                            string linea2 = "";
                            double Valor = 0;
                            double valorEntero = 0;
                            double valorDecimal = 0;
                            double ValorSinDescuento = 0;
                            double valorEnteroSinDescuento = 0;
                            double valorDecimalSinDescuento = 0;
                            double ValorConDescuento = 0;
                            double valorEnteroConDescuento = 0;
                            double valorDecimalConDescuento = 0;
                            var info_matricula = bus_matricula.GetInfo(item.IdEmpresa, Convert.ToDecimal(item.IdMatricula));

                            Valor = (item.Fecha <= item.FechaProntoPago ? Convert.ToDouble(item.ValorProntoPago) : Convert.ToDouble(item.Valor));
                            valorEntero = Math.Floor(Valor);
                            valorDecimal = Convert.ToDouble((Valor - valorEntero).ToString("N2")) * 100;

                            ValorSinDescuento =Convert.ToDouble(item.Valor);
                            valorEnteroSinDescuento = Math.Floor(ValorSinDescuento);
                            valorDecimalSinDescuento = Convert.ToDouble((ValorSinDescuento - valorEnteroSinDescuento).ToString("N2")) * 100;

                            ValorConDescuento = Convert.ToDouble(item.ValorProntoPago);
                            valorEnteroConDescuento = Math.Floor(ValorConDescuento);
                            valorDecimalConDescuento = Convert.ToDouble((ValorConDescuento - valorEnteroConDescuento).ToString("N2")) * 100;

                            var ValorDiferencia = Convert.ToDouble(item.Valor) - Convert.ToDouble(item.ValorProntoPago);
                            var valorEnteroDiferencia = Math.Floor(ValorDiferencia);
                            var valorDecimalDiferencia = Convert.ToDouble((ValorDiferencia - valorEnteroDiferencia).ToString("N2")) * 100;

                            linea2 += "094";
                            linea2 += item.CodigoAlumno.ToString().PadRight(15, ' ');
                            linea2 += FechaIni.Month.ToString().PadLeft(2, '0') + "/" + FechaIni.Day.ToString().PadLeft(2, '0') + "/" + FechaIni.Year.ToString();
                            linea2 += "0".PadRight(3, ' ');
                            linea2 += (valorEntero.ToString().PadLeft(8, '0') + "." + valorDecimal.ToString().PadRight(2, '0'));
                            linea2 += FechaFin.Month.ToString().PadLeft(2, '0') + "/" + FechaFin.Day.ToString().PadLeft(2, '0') + "/" + FechaFin.Year.ToString();//FECHA TOPE DE PAGO
                            linea2 += Convert.ToDateTime(item.FechaProntoPago).Month.ToString().PadLeft(2, '0') + "/" + Convert.ToDateTime(item.FechaProntoPago).Day.ToString().PadLeft(2, '0') + "/" + Convert.ToDateTime(item.FechaProntoPago).Year.ToString();//FECHA PRONTO PAGO
                            linea2 += "N";
                            linea2 += (string.IsNullOrEmpty(item.pe_nombreCompleto) ? "" : (item.pe_nombreCompleto.Length > 30 ? item.pe_nombreCompleto.Substring(0, 30) : item.pe_nombreCompleto.Trim())).PadRight(30, ' ');
                            linea2 += (string.IsNullOrEmpty(info_matricula.NomCurso) ? "" : (info_matricula.NomCurso.Length > 15 ? info_matricula.NomCurso.Substring(0, 15) : info_matricula.NomCurso.Trim().PadRight(15, ' ')));//CURSO
                            linea2 += (string.IsNullOrEmpty(info_matricula.NomParalelo) ? "" : (info_matricula.NomParalelo.Length > 3 ? info_matricula.NomParalelo.Substring(0, 3) : info_matricula.NomParalelo.Trim().PadRight(3, ' ')));//PARALELO
                            linea2 += (string.IsNullOrEmpty(info_matricula.NomJornada) ? "" : (info_matricula.NomJornada.Length > 15 ? info_matricula.NomJornada.Substring(0, 15) : info_matricula.NomJornada.Trim().PadRight(15, ' ')));//SECCION
                            linea2 += (valorEntero.ToString().PadLeft(8, '0') + "." + valorDecimal.ToString().PadRight(2, '0'));
                            linea2 += " ".PadRight(10, ' ');
                            linea2 += "1";
                            linea2 += (valorEnteroConDescuento.ToString().PadLeft(8, '0') + "." + valorDecimalConDescuento.ToString().PadRight(2, '0'));//valor con descuento
                            linea2 += (valorEnteroSinDescuento.ToString().PadLeft(8, '0') + "." + valorDecimalSinDescuento.ToString().PadRight(2, '0'));//valor sin descuento
                            //linea2 += "\t";

                            file.WriteLine(linea2);
                        }
                    }
                    #endregion
                }
                byte[] filebyte = System.IO.File.ReadAllBytes(rutafile + NombreArchivo + ".txt");
                return filebyte;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public byte[] GetArchivo(ba_ArchivoRecaudacion_Info info, string nombre_file)
        {
            try
            {
                return GetMulticash(info, nombre_file);

            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }

    public class ba_ArchivoRecaudacion_List
    {
        string Variable = "ba_ArchivoRecaudacion_Info";
        public List<    ba_ArchivoRecaudacion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_ArchivoRecaudacion_Info> list = new List<ba_ArchivoRecaudacion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_ArchivoRecaudacion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_ArchivoRecaudacion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class ba_ArchivoRecaudacionDet_Saldo_List
    {
        string Variable = "ba_ArchivoRecaudacionDet_Saldo_Info";
        public List<ba_ArchivoRecaudacionDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_ArchivoRecaudacionDet_Info> list = new List<ba_ArchivoRecaudacionDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_ArchivoRecaudacionDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }
        public void set_list(List<ba_ArchivoRecaudacionDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
    public class ba_ArchivoRecaudacionDet_List
    {
        string Variable = "ba_ArchivoRecaudacionDet_Info";
        public List<ba_ArchivoRecaudacionDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<ba_ArchivoRecaudacionDet_Info> list = new List<ba_ArchivoRecaudacionDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<ba_ArchivoRecaudacionDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<ba_ArchivoRecaudacionDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(ba_ArchivoRecaudacionDet_Info info_det, decimal IdTransaccionSession)
        {
            List<ba_ArchivoRecaudacionDet_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            if (list.Where(q => q.IdAlumno == info_det.IdAlumno).Count() == 0)
                list.Add(info_det);
        }

        public void UpdateRow(ba_ArchivoRecaudacionDet_Info info_det, decimal IdTransaccionSession)
        {
            ba_ArchivoRecaudacionDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            if (edited_info != null)
            {
                edited_info.Valor = info_det.Valor;
                edited_info.ValorProntoPago = info_det.ValorProntoPago;
            }
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<ba_ArchivoRecaudacionDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).First());
        }
    }
}