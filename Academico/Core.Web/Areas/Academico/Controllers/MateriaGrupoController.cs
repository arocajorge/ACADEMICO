using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MateriaGrupoController : Controller
    {
        #region Variables
        aca_MateriaGrupo_Bus bus_materia_grupo = new aca_MateriaGrupo_Bus();
        aca_MateriaGrupo_List Lista_MateriaGrupo = new aca_MateriaGrupo_List();
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

            aca_MateriaGrupo_Info model = new aca_MateriaGrupo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_MateriaGrupo_Info> lista = bus_materia_grupo.GetList(model.IdEmpresa, true);
            Lista_MateriaGrupo.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_materia_grupo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_MateriaGrupo_Info> model = Lista_MateriaGrupo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_materia_grupo", model);
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
            var orden = bus_materia_grupo.GetOrden(Convert.ToInt32(SessionFixed.IdEmpresa));
            aca_MateriaGrupo_Info model = new aca_MateriaGrupo_Info
            {
                IdEmpresa = IdEmpresa,
                OrdenMateriaGrupo = orden
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_MateriaGrupo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_materia_grupo.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMateriaGrupo = model.IdMateriaGrupo, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdMateriaGrupo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MateriaGrupo_Info model = bus_materia_grupo.GetInfo(IdEmpresa, IdMateriaGrupo);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_MateriaGrupo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_materia_grupo.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMateriaGrupo = model.IdMateriaGrupo, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdMateriaGrupo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MateriaGrupo_Info model = bus_materia_grupo.GetInfo(IdEmpresa, IdMateriaGrupo);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_MateriaGrupo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_materia_grupo.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_MateriaGrupo_List
    {
        string Variable = "aca_MateriaGrupo_Info";
        public List<aca_MateriaGrupo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MateriaGrupo_Info> list = new List<aca_MateriaGrupo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MateriaGrupo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MateriaGrupo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}