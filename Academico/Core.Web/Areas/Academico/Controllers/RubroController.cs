using Core.Bus.Academico;
using Core.Bus.Inventario;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Info.Inventario;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class RubroController : Controller
    {
        #region Variables
        aca_Rubro_Bus bus_rubro = new aca_Rubro_Bus();
        aca_rubro_List Lista_Rubro = new aca_rubro_List();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
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

            aca_Rubro_Info model = new aca_Rubro_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Rubro_Info> lista = bus_rubro.GetList(model.IdEmpresa, true);
            Lista_Rubro.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_rubro()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Rubro_Info> model = Lista_Rubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_rubro", model);
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

            aca_Rubro_Info model = new aca_Rubro_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Rubro_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!bus_rubro.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdRubro = model.IdRubro, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdRubro = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Rubro_Info model = bus_rubro.GetInfo(IdEmpresa, IdRubro);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Rubro_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!bus_rubro.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdRubro = model.IdRubro, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdRubro = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Rubro_Info model = bus_rubro.GetInfo(IdEmpresa, IdRubro);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Rubro_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_rubro.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_rubro_List
    {
        string Variable = "aca_rubro_Info";
        public List<aca_Rubro_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Rubro_Info> list = new List<aca_Rubro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Rubro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Rubro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}