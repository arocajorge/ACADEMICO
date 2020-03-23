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
using System.IO;
using ExcelDataReader;
using Core.Bus.SeguridadAcceso;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ProfesorController : Controller
    {
        #region Variables
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        aca_Profesor_List Lista_Profesor = new aca_Profesor_List();
        tb_profesion_Bus bus_profesion = new tb_profesion_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
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
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Profesor", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_profesor(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Profesor_Info> model = Lista_Profesor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
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
            var lst_profesion = bus_profesion.GetList(false);
            var lst_usuario = bus_usuario.get_list(false);

            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
            ViewBag.lst_profesion = lst_profesion;
            ViewBag.lst_usuario = lst_usuario;
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
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Profesor", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Profesor_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            string return_naturaleza = "";

            if (cl_funciones.ValidaIdentificacion(model.IdTipoDocumento, model.pe_Naturaleza, model.pe_cedulaRuc, ref return_naturaleza))
            {
                model.info_persona = new tb_persona_Info
                {
                    IdPersona = model.IdPersona,
                    pe_Naturaleza = model.pe_Naturaleza,
                    pe_nombreCompleto = model.pe_nombreCompleto,
                    pe_razonSocial = model.pe_razonSocial,
                    pe_apellido = model.pe_apellido,
                    pe_nombre = model.pe_nombre,
                    pe_fechaNacimiento = model.pe_fechaNacimiento,
                    pe_sexo = model.pe_sexo,
                    IdTipoDocumento = model.IdTipoDocumento,
                    pe_cedulaRuc = model.pe_cedulaRuc,
                    pe_direccion = model.Direccion,
                    pe_telfono_Contacto = model.Telefonos,
                    pe_correo = model.Correo,
                    IdEstadoCivil = model.IdEstadoCivil,
                    CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                    NumeroCarnetConadis = model.NumeroCarnetConadis,
                    PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                    IdProfesion = model.IdProfesion
                };

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
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Profesor", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

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
            model.info_persona = new tb_persona_Info
            {
                IdPersona = model.IdPersona,
                pe_Naturaleza = model.pe_Naturaleza,
                pe_nombreCompleto = model.pe_nombreCompleto,
                pe_razonSocial = model.pe_razonSocial,
                pe_apellido = model.pe_apellido,
                pe_nombre = model.pe_nombre,
                pe_fechaNacimiento = model.pe_fechaNacimiento,
                pe_sexo = model.pe_sexo,
                IdTipoDocumento = model.IdTipoDocumento,
                pe_cedulaRuc = model.pe_cedulaRuc,
                pe_direccion = model.Direccion,
                pe_telfono_Contacto = model.Telefonos,
                pe_correo = model.Correo,
                IdEstadoCivil = model.IdEstadoCivil,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                NumeroCarnetConadis = model.NumeroCarnetConadis,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                IdProfesion = model.IdProfesion
            };

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
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Profesor", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
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

        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Importacion
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings_Profesor.UploadValidationSettings, UploadControlSettings_Profesor.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
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
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(aca_Profesor_Info model)
        {
            try
            {
                var Lista_Profesores = Lista_Profesor.get_list(model.IdTransaccionSession);
                foreach (var item in Lista_Profesores)
                {
                    if (!bus_profesor.GuardarDB(item))
                    {
                        ViewBag.mensaje = "Error al importar el archivo";
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());

                ViewBag.error = ex.Message.ToString();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GridViewPartial_ProfesorImportacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Profesor.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ProfesorImportacion", model);
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

    public class UploadControlSettings_Profesor
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            aca_Profesor_List ListaProfesores = new aca_Profesor_List();
            List<aca_Profesor_Info> Lista_Profesores = new List<aca_Profesor_Info>();
            tb_persona_Bus bus_persona = new tb_persona_Bus();
            aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();

            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region Profesor   
                var lst_persona = bus_persona.get_list(false);
                var IdProfesor = 1;
                var no_validas = "";
                var repetidos = "";
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var return_naturaleza = "";
                        var cedula_ruc_profesor = (Convert.ToString(reader.GetValue(0))).Trim();

                        tb_persona_Info info_persona_profesor = new tb_persona_Info();
                        tb_persona_Info info_persona_profe = new tb_persona_Info();

                        info_persona_profe = lst_persona.Where(q => q.pe_cedulaRuc == cedula_ruc_profesor).FirstOrDefault();
                        info_persona_profesor = info_persona_profe;
                        
                        if (cl_funciones.ValidaIdentificacion(Convert.ToString(reader.GetValue(2)), Convert.ToString(reader.GetValue(1)), cedula_ruc_profesor, ref return_naturaleza))
                        {
                            if (info_persona_profesor == null || info_persona_profe.IdPersona == 0)
                            {
                                tb_persona_Info info_profesor = new tb_persona_Info
                                {
                                    pe_Naturaleza = Convert.ToString(reader.GetValue(1)),
                                    pe_nombreCompleto = Convert.ToString(reader.GetValue(3)).Trim() + ' ' + Convert.ToString(reader.GetValue(4)).Trim(),
                                    pe_razonSocial = (Convert.ToString(reader.GetValue(1)) == "NATU" ? "" : Convert.ToString(reader.GetValue(3)).Trim() + ' ' + Convert.ToString(reader.GetValue(4)).Trim()),
                                    pe_apellido = Convert.ToString(reader.GetValue(3)).Trim(),
                                    pe_nombre = Convert.ToString(reader.GetValue(4)).Trim(),
                                    pe_fechaNacimiento = Convert.ToDateTime(reader.GetValue(5)),
                                    pe_sexo = Convert.ToString(reader.GetValue(6)),
                                    IdTipoDocumento = Convert.ToString(reader.GetValue(2)),
                                    pe_cedulaRuc = cedula_ruc_profesor,
                                    pe_direccion = Convert.ToString(reader.GetValue(8)).Trim(),
                                    pe_telfono_Contacto = Convert.ToString(reader.GetValue(10)).Trim(),
                                    pe_correo = Convert.ToString(reader.GetValue(9)).Trim(),
                                    IdProfesion= Convert.ToInt32(reader.GetValue(11))
                                };
                                info_persona_profesor = info_profesor;
                            }
                            else
                            {
                                info_persona_profesor = bus_persona.get_info(info_persona_profe.IdPersona);
                                //var Naturaleza = Convert.ToString(reader.GetValue(1));
                                //info_persona_profesor.pe_Naturaleza = Naturaleza;
                                //info_persona_profesor.pe_nombreCompleto = Convert.ToString(reader.GetValue(3)).Trim() + ' ' + Convert.ToString(reader.GetValue(4)).Trim();
                                //info_persona_profesor.pe_razonSocial = (Convert.ToString(reader.GetValue(1)) == "NATU" ? "" : Convert.ToString(reader.GetValue(3)) + ' ' + Convert.ToString(reader.GetValue(4)));
                                //info_persona_profesor.pe_apellido = Convert.ToString(reader.GetValue(3)).Trim();
                                //info_persona_profesor.pe_nombre = Convert.ToString(reader.GetValue(4)).Trim();
                                //info_persona_profesor.IdTipoDocumento = Convert.ToString(reader.GetValue(2)).Trim();
                                //info_persona_profesor.pe_cedulaRuc = cedula_ruc_profesor;
                                //info_persona_profesor.pe_direccion = Convert.ToString(reader.GetValue(8)).Trim();
                                //info_persona_profesor.pe_telfono_Contacto = Convert.ToString(reader.GetValue(10)).Trim();
                                //info_persona_profesor.pe_correo = Convert.ToString(reader.GetValue(9)).Trim();
                                //info_persona_profesor.pe_sexo = Convert.ToString(reader.GetValue(6)).Trim();
                                //info_persona_profesor.IdProfesion = Convert.ToInt32(reader.GetValue(11));
                            }

                            //info_persona_profesor.pe_Naturaleza = return_naturaleza;
                            //info_persona_profesor.pe_nombreCompleto = (info_persona_profesor.pe_razonSocial != "" ? info_persona_profesor.pe_razonSocial : (info_persona_profesor.pe_apellido + ' ' + info_persona_profesor.pe_nombre));

                            aca_Profesor_Info info = new aca_Profesor_Info
                            {
                                IdEmpresa = IdEmpresa,
                                IdProfesor = IdProfesor,
                                IdPersona = info_persona_profesor.IdPersona,
                                Direccion = info_persona_profesor.pe_direccion,
                                Telefonos = info_persona_profesor.pe_telfono_Contacto,
                                Correo = info_persona_profesor.pe_correo,
                                Estado = true,
                                EsProfesor = true,
                                EsInspector = (Convert.ToString(reader.GetValue(12)).Trim()=="SI" ? true : false),
                                IdUsuarioCreacion = SessionFixed.IdUsuario,
                                FechaCreacion = DateTime.Now
                            };

                            info.info_persona = info_persona_profesor;
                            IdProfesor++;

                            if (Lista_Profesores.Where(q =>q.info_persona.pe_cedulaRuc == info_persona_profesor.pe_cedulaRuc).Count() == 0)
                            {
                                Lista_Profesores.Add(info);
                            }
                            else
                            {
                                repetidos = repetidos + cedula_ruc_profesor + " ";
                            }
                                
                        }
                        else
                        {
                             no_validas = no_validas + cedula_ruc_profesor+" ";
                        }
                    }
                    cont++;
                }
                no_validas = " " + no_validas;
                repetidos = " " + repetidos;
                ListaProfesores.set_list(Lista_Profesores, IdTransaccionSession);
                #endregion
            }
        }
    }
}