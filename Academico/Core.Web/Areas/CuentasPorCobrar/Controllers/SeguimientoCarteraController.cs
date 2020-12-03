using Core.Bus.Academico;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.General;
using Core.Bus.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.CuentasPorCobrar;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Areas.Academico.Controllers;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using System.IO;
using ExcelDataReader;
using Core.Web.Helps;

namespace Core.Web.Areas.CuentasPorCobrar.Controllers
{
    public class SeguimientoCarteraController : Controller
    {
        #region Variables
        cxc_SeguimientoCartera_Bus bus_seguimiento = new cxc_SeguimientoCartera_Bus();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        string mensaje = string.Empty;
        string mensajeInfo = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        cxc_SeguimientoCartera_List Lista_Seguimiento = new cxc_SeguimientoCartera_List();
        cxc_SeguimientoCartera_x_Alumno_List Lista_Seguimiento_x_Alumno = new cxc_SeguimientoCartera_x_Alumno_List();
        tb_ColaCorreo_Bus bus_cola_correo = new tb_ColaCorreo_Bus();
        cxc_Parametro_Bus bus_parametros = new cxc_Parametro_Bus();
        public static UploadedFile file { get; set; }
        public static byte[] imagen { get; set; }
        public static byte[] seguimiento_foto { get; set; }
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
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
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAlumno = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                fecha_ini = DateTime.Now.Date.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            var Lista = bus_seguimiento.getList(model.IdEmpresa, model.IdAlumno, true, model.fecha_ini, model.fecha_fin);
            Lista_Seguimiento.set_list(Lista, model.IdTransaccionSession);

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            var Lista = bus_seguimiento.getList(model.IdEmpresa, model.IdAlumno, true, model.fecha_ini, model.fecha_fin);
            Lista_Seguimiento.set_list(Lista, model.IdTransaccionSession);

            return View(model);
        }
        #endregion

        #region Grids
        [ValidateInput(false)]
        public ActionResult GridViewPartial_SeguimientoCartera(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdAlumno = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<cxc_SeguimientoCartera_Info> model = Lista_Seguimiento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_SeguimientoCartera", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_SeguimientoCarteraDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<cxc_SeguimientoCartera_Info> model = Lista_Seguimiento_x_Alumno.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_SeguimientoCarteraDet", model);
        }
        #endregion

        #region Json
        public JsonResult GetInfoAlumno(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            var info = bus_alumno.GetInfo_PeriodoActual(IdEmpresa, IdAlumno);
            var info_papa = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
            var info_mama = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA));
            if (info_papa!=null)
            {
                info.pe_nombreCompleto_padre = info_papa.pe_nombreCompleto;
                info.TelefonoTrabajo_padre = info_papa.Telefono;
                info.Celular_padre = info_papa.Celular;
                info.Correo_padre = info_papa.Correo;
            }
            if (info_mama != null)
            {
                info.pe_nombreCompleto_madre = info_mama.pe_nombreCompleto;
                info.TelefonoTrabajo_madre = info_mama.Telefono;
                info.Celular_madre = info_mama.Celular;
                info.Correo_madre = info_mama.Correo;
            }
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetList_x_Alumno(decimal IdTransaccionSession = 0, int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            var lst = bus_seguimiento.getList_x_Alumno(IdEmpresa, IdAlumno);

            Lista_Seguimiento_x_Alumno.set_list(lst, IdTransaccionSession);

            return Json(lst.Count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EnviarCorreo(int IdSeguimiento = 0)
        {
            string resultado = string.Empty;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var model = bus_seguimiento.get_info(IdEmpresa, IdSeguimiento);
            var info_alumno = bus_alumno.GetInfo_PeriodoActual(model.IdEmpresa, model.IdAlumno);

            if (model != null)
            {
                var info_correo = new tb_ColaCorreo_Info
                {
                    IdEmpresa = IdEmpresa,
                    Asunto = "SEGUIMIENTO DE COBRANZA",
                    Cuerpo = model.Observacion,
                    Destinatarios = (info_alumno==null ? "" : ((string.IsNullOrEmpty(info_alumno.correoRepEconomico)?"" : info_alumno.correoRepEconomico+";") + (string.IsNullOrEmpty(info_alumno.CorreoRepLegal) ? "" : info_alumno.CorreoRepLegal + ";")) ),
                    Codigo = "",
                    Parametros = IdEmpresa.ToString() + ";" + model.IdAlumno.ToString(),
                    IdUsuarioCreacion = SessionFixed.IdUsuario
                };

                if (bus_cola_correo.GuardarDB(info_correo))
                {
                    model.IdUsuarioModificacion = SessionFixed.IdUsuario;
                    bus_seguimiento.EnviarCorreoDB(model);

                    resultado = "Correo enviado";
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Funciones imagen alumno
        public JsonResult nombre_imagen(decimal IdSeguimiento = 0)
        {
            try
            {
                if (IdSeguimiento == 0)
                    IdSeguimiento = bus_seguimiento.getId(Convert.ToInt32(SessionFixed.IdEmpresa));
                SessionFixed.NombreImagenSeguimiento = IdSeguimiento.ToString("000000");
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult get_imagen_general()
        {
            byte[] model = empresa_imagen.seguimiento_foto;
            if (model == null)
                model = new byte[0];
            return PartialView("_Empresa_imagen", model);
        }
        public class empresa_imagen
        {
            public static byte[] seguimiento_foto { get; set; }
            public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" },
                MaxFileSize = 4000000
            };
            public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
            {

                if (e.UploadedFile.IsValid)
                {
                    seguimiento_foto = e.UploadedFile.FileBytes;
                }
            }
        }
        public JsonResult actualizar_div()
        {
            return Json(SessionFixed.NombreImagenSeguimiento, JsonRequestBehavior.AllowGet);
        }
        public string UploadDirectory = "~/Content/imagenes/seguimiento/";
        public ActionResult DragAndDropImageUpload([ModelBinder(typeof(DragAndDropSupportDemoBinder))]IEnumerable<UploadedFile> ucDragAndDrop)
        {

            try
            {
                //Extract Image File Name.
                string fileName = System.IO.Path.GetFileName(ucDragAndDrop.FirstOrDefault().FileName);
                var IdEmpresa = Convert.ToString(SessionFixed.IdEmpresa).PadLeft(3, '0');
                //Set the Image File Path.
                UploadDirectory = UploadDirectory + IdEmpresa + SessionFixed.NombreImagenSeguimiento + ".jpg";
                imagen = ucDragAndDrop.FirstOrDefault().FileBytes;
                //Save the Image File in Folder.
                ucDragAndDrop.FirstOrDefault().SaveAs(Server.MapPath(UploadDirectory));
                SessionFixed.NombreImagenSeguimiento = UploadDirectory;

                file = ucDragAndDrop.FirstOrDefault();
                return Json(ucDragAndDrop.FirstOrDefault().FileBytes, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return View();
            }

        }

        #endregion

        #region Metodos
        private bool validar(cxc_SeguimientoCartera_Info info, ref string msg)
        {
            if (info.IdAlumno==0)
            {
                msg = "Debe seleccionar al estudiante";
                return false;
            }

            return true;
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
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            cxc_SeguimientoCartera_Info model = new cxc_SeguimientoCartera_Info
            {
                IdEmpresa = IdEmpresa,
                Fecha = DateTime.Now.Date,
                lst_det = new List<cxc_SeguimientoCartera_Info>(),
                seguimiento_foto = new byte[0],
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            Lista_Seguimiento_x_Alumno.set_list(model.lst_det, model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(cxc_SeguimientoCartera_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario.ToString();
            var ListaDetalle = Lista_Seguimiento_x_Alumno.get_list(model.IdTransaccionSession);
            model.lst_det = ListaDetalle.ToList();

            if (model.seguimiento_foto == null)
                model.seguimiento_foto = new byte[0];

            if (!validar(model, ref mensaje))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                Lista_Seguimiento_x_Alumno.set_list(Lista_Seguimiento_x_Alumno.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_seguimiento.guardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                Lista_Seguimiento_x_Alumno.set_list(Lista_Seguimiento_x_Alumno.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";

                return View(model);
            };

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdSeguimiento = model.IdSeguimiento, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, int IdSeguimiento = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_SeguimientoCartera_Info model = bus_seguimiento.get_info(IdEmpresa, IdSeguimiento);
            if (model == null)
                return RedirectToAction("Index");

            if (model.seguimiento_foto == null)
                model.seguimiento_foto = new byte[0];

            try
            {

                model.seguimiento_foto = System.IO.File.ReadAllBytes(Server.MapPath(UploadDirectory) + model.IdEmpresa.ToString("000") + model.IdSeguimiento.ToString("000000") + ".jpg");
                if (model.seguimiento_foto == null)
                    model.seguimiento_foto = new byte[0];
            }
            catch (Exception)
            {

                model.seguimiento_foto = new byte[0];
            }

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            if (model.Estado == false)
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            var info_alumno = bus_alumno.GetInfo_PeriodoActual(model.IdEmpresa, model.IdAlumno);
            model.DatosAcademicos = (info_alumno==null ? "" : (info_alumno.NomSede + " - " + info_alumno.NomJornada + " - " + info_alumno.NomNivel + " - " + info_alumno.NomCurso + " - " + info_alumno.NomParalelo));
            model.Saldo = (info_alumno == null ? 0 : info_alumno.Saldo).ToString("C2");
            model.SaldoProntoPago = (info_alumno == null ? 0 : info_alumno.SaldoProntoPago).ToString("C2");
            model.RepLegal = (info_alumno == null ? "" : info_alumno.NomRepLegal);
            model.TelefonoLegal = (info_alumno == null ? "" : info_alumno.TelefonoRepresentante);
            model.CelularRepresentante = (info_alumno == null ? "" : info_alumno.CelularRepresentante);
            model.CorreoLegal = (info_alumno == null ? "" : info_alumno.correoRepEconomico);
            model.RepEconomico = (info_alumno == null ? "" : info_alumno.NomRepEconomico);
            model.TelefonoEconomico = (info_alumno == null ? "" : info_alumno.TelefonoEmiteFactura);
            model.CelularEmiteFactura = (info_alumno == null ? "" : info_alumno.CelularEmiteFactura);
            model.CorreoEconomico = (info_alumno == null ? "" : info_alumno.correoRepEconomico);
            model.NomPlantillaTipo = (info_alumno == null ? "" : info_alumno.NomPlantillaTipo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);

            var info_papa = bus_familia.GetListTipo(IdEmpresa, model.IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
            var info_mama = bus_familia.GetListTipo(IdEmpresa, model.IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA));

            if (info_papa != null)
            {
                model.Papa = info_papa.pe_nombreCompleto;
                model.TelefonoPapa = info_papa.Telefono;
                model.CelularPapa = info_papa.Celular;
                model.CorreoPapa = info_papa.Correo;
            }
            if (info_mama != null)
            {
                model.Mama = info_mama.pe_nombreCompleto;
                model.TelefonoMama = info_mama.Telefono;
                model.CelularMama = info_mama.Celular;
                model.CorreoMama = info_mama.Correo;
            }

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSeguimiento = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            cxc_SeguimientoCartera_Info model = bus_seguimiento.get_info(IdEmpresa, IdSeguimiento);
            if (model == null)
                return RedirectToAction("Index");
            if (model == null)
                return RedirectToAction("Index");

            if (model.seguimiento_foto == null)
                model.seguimiento_foto = new byte[0];

            try
            {

                model.seguimiento_foto = System.IO.File.ReadAllBytes(Server.MapPath(UploadDirectory) + model.IdEmpresa.ToString("000") + model.IdSeguimiento.ToString("000000") + ".jpg");
                if (model.seguimiento_foto == null)
                    model.seguimiento_foto = new byte[0];
            }
            catch (Exception)
            {

                model.seguimiento_foto = new byte[0];
            }

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "CuentasPorCobrar", "SeguimientoCartera", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            var info_alumno = bus_alumno.GetInfo_PeriodoActual(model.IdEmpresa, model.IdAlumno);
            model.DatosAcademicos = (info_alumno == null ? "" : (info_alumno.NomSede + " - " + info_alumno.NomJornada + " - " + info_alumno.NomNivel + " - " + info_alumno.NomCurso + " - " + info_alumno.NomParalelo));
            model.Saldo = (info_alumno == null ? 0 : info_alumno.Saldo).ToString("C2");
            model.SaldoProntoPago = (info_alumno == null ? 0 : info_alumno.SaldoProntoPago).ToString("C2");
            model.RepLegal = (info_alumno == null ? "" : info_alumno.NomRepLegal);
            model.CelularRepresentante = (info_alumno == null ? "" : info_alumno.CelularRepresentante);
            model.TelefonoLegal = (info_alumno == null ? "" : info_alumno.TelefonoRepresentante);
            model.CorreoLegal = (info_alumno == null ? "" : info_alumno.correoRepEconomico);
            model.RepEconomico = (info_alumno == null ? "" : info_alumno.NomRepEconomico);
            model.TelefonoEconomico = (info_alumno == null ? "" : info_alumno.TelefonoEmiteFactura);
            model.CelularEmiteFactura = (info_alumno == null ? "" : info_alumno.CelularEmiteFactura);
            model.CorreoEconomico = (info_alumno == null ? "" : info_alumno.correoRepEconomico);
            model.NomPlantillaTipo = (info_alumno == null ? "" : info_alumno.NomPlantillaTipo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(cxc_SeguimientoCartera_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;

            if (model.seguimiento_foto == null)
                model.seguimiento_foto = new byte[0];

            if (!bus_seguimiento.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                return View(model);
            };

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class cxc_SeguimientoCartera_List
    {
        string Variable = "cxc_SeguimientoCartera_Info";
        public List<cxc_SeguimientoCartera_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_SeguimientoCartera_Info> list = new List<cxc_SeguimientoCartera_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_SeguimientoCartera_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_SeguimientoCartera_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class cxc_SeguimientoCartera_x_Alumno_List
    {
        string Variable = "cxc_SeguimientoCartera_x_Alumno_Info";
        public List<cxc_SeguimientoCartera_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_SeguimientoCartera_Info> list = new List<cxc_SeguimientoCartera_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_SeguimientoCartera_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_SeguimientoCartera_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}