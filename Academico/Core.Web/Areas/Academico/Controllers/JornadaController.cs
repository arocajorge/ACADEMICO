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
    public class JornadaController : Controller
    {
        #region Variables
        aca_Jornada_Bus bus_jornada = new aca_Jornada_Bus();
        aca_Jornada_List Lista_Jornada = new aca_Jornada_List();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
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

            aca_Jornada_Info model = new aca_Jornada_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Jornada_Info> lista = bus_jornada.GetList(model.IdEmpresa, true);
            Lista_Jornada.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_jornada()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Jornada_Info> model = Lista_Jornada.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_jornada", model);
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
            var orden = bus_jornada.GetOrden(Convert.ToInt32(SessionFixed.IdEmpresa));
            aca_Jornada_Info model = new aca_Jornada_Info
            {
                IdEmpresa = IdEmpresa,
                OrdenJornada = orden
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Jornada_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_jornada.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdJornada = model.IdJornada, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdJornada = 0, bool Exito=false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Jornada_Info model = bus_jornada.GetInfo(IdEmpresa, IdJornada);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Jornada_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_jornada.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdJornada = model.IdJornada, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdJornada = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Jornada_Info model = bus_jornada.GetInfo(IdEmpresa, IdJornada);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Jornada_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_jornada.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_Jornada_List
    {
        string Variable = "aca_Jornada_Info";
        public List<aca_Jornada_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Jornada_Info> list = new List<aca_Jornada_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Jornada_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Jornada_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}