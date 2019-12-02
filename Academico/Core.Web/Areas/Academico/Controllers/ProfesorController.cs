using DevExpress.Web.Mvc;
using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Bus.General;
using Core.Info.Helps;
using Core.Info.General;
using DevExpress.Web;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ProfesorController : Controller
    {
        #region Variables
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        aca_Profesor_List Lista_Profesor = new aca_Profesor_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        public static UploadedFile file { get; set; }
        public static byte[] imagen { get; set; }
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

            aca_Profesor_Info model = new aca_Profesor_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Profesor_Info> lista = bus_profesor.GetList(model.IdEmpresa, true);
            Lista_Profesor.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_profesor()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Profesor_Info> model = Lista_Profesor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_profesor", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "" });

            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
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
            aca_Profesor_Info model = new aca_Profesor_Info
            {
                IdEmpresa = IdEmpresa,
                EsProfesor = true,
                pe_Naturaleza = "NATU",
                CodCatalogoCONADIS = "",
                prof_foto = new byte[0],
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Profesor_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            string return_naturaleza = "";

            if (cl_funciones.ValidaIdentificacion(model.IdTipoDocumento, model.pe_Naturaleza, model.pe_cedulaRuc, ref return_naturaleza))
            {
                if (!bus_profesor.GuardarDB(model))
                {
                    ViewBag.mensaje = "No se ha podido guardar el registro";
                    cargar_combos();
                    return View(model);
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdProfesor = model.IdProfesor, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdProfesor = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Profesor_Info model = bus_profesor.GetInfo(IdEmpresa, IdProfesor);

            if (model.prof_foto == null)
                model.prof_foto = new byte[0];

            try
            {

                model.prof_foto = System.IO.File.ReadAllBytes(Server.MapPath(UploadDirectory) + model.IdEmpresa.ToString("000") + model.IdProfesor.ToString("000000") + ".jpg");
            }
            catch (Exception)
            {

                model.prof_foto = new byte[0];
            }

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Profesor_Info model)
        {
            var return_naturaleza = "";
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if ((cl_funciones.ValidaIdentificacion(model.IdTipoDocumento, model.pe_Naturaleza, model.pe_cedulaRuc, ref return_naturaleza)))
            {
                if (!bus_profesor.ModificarDB(model))
                {
                    ViewBag.mensaje = "No se ha podido modificar el registro";
                    cargar_combos();
                    return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdProfesor = model.IdProfesor, Exito = true });
                }
            }
            else
            {
                ViewBag.mensaje = "Número de identificación inválida";
                cargar_combos();
                return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdProfesor = model.IdProfesor, Exito = true });
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdProfesor = model.IdProfesor, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdProfesor = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Profesor_Info model = bus_profesor.GetInfo(IdEmpresa, IdProfesor);
            if (model.prof_foto == null)
                model.prof_foto = new byte[0];

            try
            {

                model.prof_foto = System.IO.File.ReadAllBytes(Server.MapPath(UploadDirectory) + model.IdEmpresa.ToString("000") + model.IdProfesor.ToString("000000") + ".jpg");
            }
            catch (Exception)
            {

                model.prof_foto = new byte[0];
            }
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Profesor_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_profesor.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Funciones foto
        public JsonResult nombre_imagen(decimal IdProfesor = 0)
        {
            try
            {
                if (IdProfesor == 0)
                    IdProfesor = bus_profesor.GetId(Convert.ToInt32(SessionFixed.IdEmpresa));
                SessionFixed.NombreImagenProfesor = IdProfesor.ToString("000000");
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult get_imagen_general()
        {

            byte[] model = empresa_imagen.pr_imagen;
            if (model == null)
                model = new byte[0];
            return PartialView("_Empresa_imagen", model);
        }
        public class empresa_imagen
        {
            public static byte[] pr_imagen { get; set; }
            public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" },
                MaxFileSize = 4000000
            };
            public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
            {

                if (e.UploadedFile.IsValid)
                {
                    pr_imagen = e.UploadedFile.FileBytes;
                }
            }
        }
        public JsonResult actualizar_div()
        {
            return Json(SessionFixed.NombreImagenProfesor, JsonRequestBehavior.AllowGet);
        }
        public string UploadDirectory = "~/Content/imagenes/profesores/";
        public ActionResult DragAndDropImageUpload([ModelBinder(typeof(DragAndDropSupportDemoBinder_Profesor))]IEnumerable<UploadedFile> ucDragAndDrop)
        {

            try
            {
                //Extract Image File Name.
                string fileName = System.IO.Path.GetFileName(ucDragAndDrop.FirstOrDefault().FileName);
                var IdEmpresa = Convert.ToString(SessionFixed.IdEmpresa).PadLeft(3, '0');
                //Set the Image File Path.
                UploadDirectory = UploadDirectory + IdEmpresa + SessionFixed.NombreImagenProfesor + ".jpg";
                imagen = ucDragAndDrop.FirstOrDefault().FileBytes;
                //Save the Image File in Folder.
                ucDragAndDrop.FirstOrDefault().SaveAs(Server.MapPath(UploadDirectory));
                SessionFixed.NombreImagenProfesor = UploadDirectory;

                file = ucDragAndDrop.FirstOrDefault();
                return Json(ucDragAndDrop.FirstOrDefault().FileBytes, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return View();
            }

        }

        #endregion

        #region Json
        public JsonResult Validar_cedula_ruc(string naturaleza = "", string tipo_documento = "", string cedula_ruc = "")
        {
            var return_naturaleza = "";
            var isValid = cl_funciones.ValidaIdentificacion(tipo_documento, naturaleza, cedula_ruc, ref return_naturaleza);

            return Json(new { isValid = isValid, return_naturaleza = return_naturaleza }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula(int IdEmpresa = 0, string pe_cedulaRuc = "")
        {
            var resultado = bus_profesor.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            resultado.anio = Convert.ToDateTime(resultado.pe_fechaNacimiento).Year.ToString();
            var mes = Convert.ToDateTime(resultado.pe_fechaNacimiento).Month;
            mes = mes - 1;
            resultado.mes = mes.ToString();
            resultado.dia = Convert.ToDateTime(resultado.pe_fechaNacimiento).Day.ToString();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    #region Clases para imagen
    public class DragAndDropSupportDemoBinder_Profesor : DevExpressEditorsBinder
    {
        public DragAndDropSupportDemoBinder_Profesor()
        {
            UploadControlBinderSettings.ValidationSettings.Assign(UploadControlDemosHelper_Profesor.UploadValidationSettings);
            UploadControlBinderSettings.FileUploadCompleteHandler = UploadControlDemosHelper_Profesor.FileUploadComplete;
        }
    }
    public class UploadControlDemosHelper_Profesor
    {
        public static byte[] em_foto { get; set; }
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {

            if (e.UploadedFile.IsValid)
            {
                em_foto = e.UploadedFile.FileBytes;
                //var filename = Path.GetFileName(e.UploadedFile.FileName);
                //e.UploadedFile.SaveAs("~/Content/imagenes/"+e.UploadedFile.FileName, true);
            }
        }
    }
    #endregion

    public class aca_Profesor_List
    {
        string Variable = "aca_Profesor_Info";
        public List<aca_Profesor_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Profesor_Info> list = new List<aca_Profesor_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Profesor_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Profesor_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}