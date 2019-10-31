using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class AlumnoController : Controller
    {
        #region Variables
        aca_alumno_Bus bus_alumno = new aca_alumno_Bus();
        aca_familia_Bus bus_familia = new aca_familia_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
        aca_alumno_List Lista_Alumno = new aca_alumno_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
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

            aca_alumno_Info model = new aca_alumno_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_alumno_Info> lista = bus_alumno.GetList(model.IdEmpresa, true);
            Lista_Alumno.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_alumno()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_alumno_Info> model = Lista_Alumno.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_alumno", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            var lst_tipo_sangre = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPOSANGRE), false);
            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = ""});

            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_tipo_sangre = lst_tipo_sangre;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
        }

        private bool validar(aca_alumno_Info info, ref string msg)
        {
            string return_naturaleza = "";
            string return_naturaleza_padre = "";
            string return_naturaleza_madre = "";

            if (cl_funciones.ValidaIdentificacion(info.info_persona_alumno.IdTipoDocumento, info.info_persona_alumno.pe_Naturaleza, info.info_persona_alumno.pe_cedulaRuc, ref return_naturaleza))
            {
                info.info_persona_alumno.pe_Naturaleza = return_naturaleza;
            }
            else
            {
                msg = "Número de identificación del alumno inválida";
                return false;
            }

            if (cl_funciones.ValidaIdentificacion(info.info_persona_padre.IdTipoDocumento, info.info_persona_padre.pe_Naturaleza, info.info_persona_padre.pe_cedulaRuc, ref return_naturaleza_padre))
            {
                info.info_persona_padre.pe_Naturaleza = return_naturaleza_padre;
            }
            else
            {
                msg = "Número de identificación del padre inválida";
                return false;
            }

            if (cl_funciones.ValidaIdentificacion(info.info_persona_madre.IdTipoDocumento, info.info_persona_madre.pe_Naturaleza, info.info_persona_madre.pe_cedulaRuc, ref return_naturaleza_madre))
            {
                info.info_persona_madre.pe_Naturaleza = return_naturaleza_madre;
            }
            else
            {
                msg = "Número de identificación de la madre inválida";
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
            aca_alumno_Info model = new aca_alumno_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                pe_Naturaleza = "NATU",
                CodCatalogoCONADIS = ""
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_alumno_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            var info_persona_alumno = new tb_persona_Info
            {
                IdPersona = model.IdPersona,
                pe_Naturaleza = model.pe_Naturaleza,
                IdTipoDocumento = model.IdTipoDocumento,
                pe_cedulaRuc = model.pe_cedulaRuc,
                pe_nombre = model.pe_nombre,
                pe_apellido = model.pe_apellido,
                pe_nombreCompleto = model.pe_nombreCompleto,
                pe_sexo = model.pe_sexo,
                CodCatalogoSangre = model.CodCatalogoSangre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                NumeroCarnetConadis =model.NumeroCarnetConadis,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                pe_fechaNacimiento = model.pe_fechaNacimiento,
                pe_telfono_Contacto = model.pe_telfono_Contacto,
                pe_correo = model.Correo,
                pe_celular = model.Celular,
                pe_direccion = model.Direccion
            };

            var info_persona_padre = new tb_persona_Info
            {
                IdPersona = model.IdPersona_padre,
                pe_Naturaleza = model.pe_Naturaleza_padre,
                IdTipoDocumento = model.IdTipoDocumento_padre,
                pe_cedulaRuc = model.pe_cedulaRuc_padre,
                pe_nombre = model.pe_nombre_padre,
                pe_apellido = model.pe_apellido_padre,
                pe_nombreCompleto = model.pe_apellido_padre + " " + model.pe_nombre_padre,
                pe_sexo = model.pe_sexo_padre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS_padre,
                NumeroCarnetConadis = model.NumeroCarnetConadis_padre,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad_padre,
                pe_fechaNacimiento = model.pe_fechaNacimiento_padre,
                pe_telfono_Contacto = model.pe_telfono_Contacto_padre,
                pe_correo = model.Correo_padre,
                pe_celular = model.Celular_padre,
                pe_direccion = model.Direccion_padre,
                IdEstadoCivil = model.IdEstadoCivil_padre
            };

            var info_persona_madre = new tb_persona_Info
            {
                IdPersona = model.IdPersona_madre,
                pe_Naturaleza = model.pe_Naturaleza_madre,
                IdTipoDocumento = model.IdTipoDocumento_madre,
                pe_cedulaRuc = model.pe_cedulaRuc_madre,
                pe_nombre = model.pe_nombre_madre,
                pe_apellido = model.pe_apellido_madre,
                pe_nombreCompleto = model.pe_apellido_madre + " " + model.pe_nombre_madre,
                pe_sexo = model.pe_sexo_madre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS_madre,
                NumeroCarnetConadis = model.NumeroCarnetConadis_madre,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad_madre,
                pe_fechaNacimiento = model.pe_fechaNacimiento_madre,
                pe_telfono_Contacto = model.pe_telfono_Contacto_madre,
                pe_correo = model.Correo_madre,
                pe_celular = model.Celular_madre,
                pe_direccion = model.Direccion_madre,
                IdEstadoCivil = model.IdEstadoCivil_madre
            };

            model.info_persona_alumno = info_persona_alumno;
            model.info_persona_padre = info_persona_padre;
            model.info_persona_madre = info_persona_madre;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }

            if (!bus_alumno.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAlumno = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_alumno_Info model = bus_alumno.GetInfo(IdEmpresa, IdAlumno);
            //model.info_persona_alumno.CodCatalogoCONADIS = (model.info_persona.CodCatalogoCONADIS == null ? "" : model.info_persona.CodCatalogoCONADIS);
            //model.info_persona_padre.CodCatalogoCONADIS = (model.info_persona_padre.CodCatalogoCONADIS == null ? "" : model.info_persona_padre.CodCatalogoCONADIS);
            //model.info_persona_madre.CodCatalogoCONADIS = (model.info_persona_madre.CodCatalogoCONADIS == null ? "" : model.info_persona_madre.CodCatalogoCONADIS);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_alumno_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!bus_alumno.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos();
                return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Exito = true });
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAlumno = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_alumno_Info model = bus_alumno.GetInfo(IdEmpresa, IdAlumno);

            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_alumno_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_alumno.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
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
            var resultado = bus_alumno.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula_familia(int IdEmpresa = 0, string pe_cedulaRuc = "")
        {
            var resultado = bus_familia.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_alumno_List
    {
        string Variable = "aca_alumno_Info";
        public List<aca_alumno_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_alumno_Info> list = new List<aca_alumno_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_alumno_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_alumno_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}