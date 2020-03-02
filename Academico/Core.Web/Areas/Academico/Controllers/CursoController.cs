using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class CursoController : Controller
    {
        #region Variables
        aca_Curso_Bus bus_curso = new aca_Curso_Bus();
        aca_Curso_List Lista_Curso = new aca_Curso_List();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Profesor_Bus bus_profesor = new aca_Profesor_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_ProfesorTutor()
        {
            aca_Curso_Info model = new aca_Curso_Info();
            return PartialView("_Cmb_ProfesorTutor", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_tutor(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.TUTOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_tutor(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.TUTOR.ToString());
        }

        public ActionResult Cmb_ProfesorInspector()
        {
            aca_Curso_Info model = new aca_Curso_Info();
            return PartialView("_Cmb_ProfesorInspector", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_inspector(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.INSPECTOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_inspector(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.INSPECTOR.ToString());
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

            aca_Curso_Info model = new aca_Curso_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Curso_Info> lista = bus_curso.GetList(model.IdEmpresa, true);
            Lista_Curso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Curso", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_curso(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Curso_Info> model = Lista_Curso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_curso", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_curso = bus_curso.GetList(IdEmpresa, false);
            lst_curso.Add(new aca_Curso_Info { IdCurso = 0, NomCurso = "" });

            ViewBag.lst_curso = lst_curso;
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
            var orden = bus_curso.GetOrden(Convert.ToInt32(SessionFixed.IdEmpresa));
            aca_Curso_Info model = new aca_Curso_Info
            {
                IdEmpresa = IdEmpresa,
                OrdenCurso = orden,
                IdCursoAPromover = 0
            };
            cargar_combos();
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Curso", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Curso_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.IdCursoAPromover = (model.IdCursoAPromover ==0 ? null : model.IdCursoAPromover );
            if (!bus_curso.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdCurso = model.IdCurso, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdCurso = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            
            aca_Curso_Info model = bus_curso.GetInfo(IdEmpresa, IdCurso);
            model.IdCursoAPromover = (model.IdCursoAPromover == null ? 0 : model.IdCursoAPromover);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Curso", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Curso_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.IdCursoAPromover = (model.IdCursoAPromover == 0 ? null : model.IdCursoAPromover);
            if (!bus_curso.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdCurso = model.IdCurso, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdCurso = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Curso_Info model = bus_curso.GetInfo(IdEmpresa, IdCurso);
            model.IdCursoAPromover = (model.IdCursoAPromover == null ? 0 : model.IdCursoAPromover);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Curso", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Curso_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_curso.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_Curso_List
    {
        string Variable = "aca_Curso_Info";
        public List<aca_Curso_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Curso_Info> list = new List<aca_Curso_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Curso_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Curso_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}