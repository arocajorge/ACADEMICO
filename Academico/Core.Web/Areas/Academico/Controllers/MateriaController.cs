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
    public class MateriaController : Controller
    {
        #region Variables
        aca_Materia_Bus bus_materia = new aca_Materia_Bus();
        aca_MateriaGrupo_Bus bus_grupo = new aca_MateriaGrupo_Bus();
        aca_Materia_List Lista_Materia = new aca_Materia_List();
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

            aca_Materia_Info model = new aca_Materia_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Materia_Info> lista = bus_materia.GetList(model.IdEmpresa, true);
            Lista_Materia.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_materia()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Materia_Info> model = Lista_Materia.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_materia", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_grupos = bus_grupo.GetList(IdEmpresa, false);
            ViewBag.lst_grupos = lst_grupos;
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
            var orden = bus_materia.GetOrden(Convert.ToInt32(SessionFixed.IdEmpresa));
            aca_Materia_Info model = new aca_Materia_Info
            {
                IdEmpresa = IdEmpresa,
                OrdenMateria = orden,
                EsObligatorio = true
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Materia_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_materia.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMateria = model.IdMateria, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdMateria = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Materia_Info model = bus_materia.GetInfo(IdEmpresa, IdMateria);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Materia_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            var info_grupo = bus_grupo.GetInfo(model.IdEmpresa, Convert.ToInt32(model.IdMateriaGrupo));
            model.NomMateriaGrupo = (info_grupo == null ? "" : info_grupo.NomMateriaGrupo);
            if (!bus_materia.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMateria = model.IdMateria, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdMateria = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Materia_Info model = bus_materia.GetInfo(IdEmpresa, IdMateria);

            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Materia_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_materia.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_Materia_List
    {
        string Variable = "aca_Materia_Info";
        public List<aca_Materia_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Materia_Info> list = new List<aca_Materia_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Materia_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Materia_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}