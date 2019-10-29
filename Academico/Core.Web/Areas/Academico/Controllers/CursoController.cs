using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Web.Helps;
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

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_curso()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Curso_Info> model = Lista_Curso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_curso", model);
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
                OrdenCurso = orden
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Curso_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_curso.GuardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdCurso = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Curso_Info model = bus_curso.GetInfo(IdEmpresa, IdCurso);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Curso_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_curso.ModificarDB(model))
            {
                return View(model);
            }

            return RedirectToAction("Index");
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

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Curso_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_curso.AnularDB(model))
            {
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