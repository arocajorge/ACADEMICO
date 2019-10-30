using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class SedeController : Controller
    {
        #region Index / Metodo

        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_Sede_List Lista_Sede = new aca_Sede_List();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;

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
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Sede_Info> lista = bus_sede.GetList(model.IdEmpresa, true);
            Lista_Sede.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            cargar_filtros(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<aca_Sede_Info> lista = bus_sede.GetList(model.IdEmpresa, true);
            Lista_Sede.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            cargar_filtros(model);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_sede()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Sede_Info> model = Lista_Sede.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_sede", model);
        }
        #endregion

        #region Metodos
        private void cargar_filtros(cl_filtros_Info model)
        {
            var lst_empresa = bus_empresa.get_list(false);
            ViewBag.lst_empresa = lst_empresa;

            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        private void cargar_combos(aca_Sede_Info model)
        {
            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;
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

            aca_Sede_Info model = new aca_Sede_Info
            {
                IdEmpresa = IdEmpresa
            };

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Sede_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_sede.guardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSede = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Sede_Info model = bus_sede.GetInfo(IdEmpresa, IdSede);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Sede_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_sede.modificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSede = model.IdSede, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdSede = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Sede_Info model = bus_sede.GetInfo(IdEmpresa, IdSede);

            if (model == null)
                return RedirectToAction("Index");

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Sede_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_sede.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos(model);
                return View(model);
            }

            cargar_combos(model);
            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_Sede_List
    {
        string Variable = "aca_Sede_Info";
        public List<aca_Sede_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Sede_Info> list = new List<aca_Sede_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Sede_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Sede_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}