using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.General;
using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Info.CuentasPorCobrar;
using Core.Info.General;
using Core.Info.Helps;
using Core.Info.Academico;
using Core.Info.Contabilidad;
using Core.Web.Helps;
using DevExpress.Web;
using Core.Info.Facturacion;
using Core.Bus.Facturacion;

namespace Core.Web.Areas.CuentasPorCobrar.Controllers
{
    public class ConciliacionNCController : Controller
    {
        #region Variables
        cxc_ConciliacionNotaCredito_Bus busConciliacion = new cxc_ConciliacionNotaCredito_Bus();
        cxc_ConciliacionNotaCredito_List lstConciliacion = new cxc_ConciliacionNotaCredito_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_notaCreDeb_Bus bus_Nc = new fa_notaCreDeb_Bus();
        #endregion

        #region Combo bajo demanda Alumno
        public ActionResult CmbAlumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno_ConciliacionNC", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        #endregion

        #region Combo bajo demanda Notas de crédito
        public ActionResult CmbNotaCreditoPorConciliar()
        {
            decimal model = new decimal();
            return PartialView("_CmbNotaCreditoPorConciliar", model);
        }
        public List<fa_notaCreDeb_Info> get_list_bajo_demanda_NC(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            decimal IdAlumno = string.IsNullOrEmpty(SessionFixed.IdAlumno) ? -1 : Convert.ToDecimal(SessionFixed.IdAlumno);
            return bus_Nc.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa),IdAlumno);
        }
        public fa_notaCreDeb_Info get_info_bajo_demanda_NC(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_Nc.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
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

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            lstConciliacion.set_list(busConciliacion.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            lstConciliacion.set_list(busConciliacion.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ConciliacionNC()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = lstConciliacion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionNC", model);
        }
        #endregion

        #region Json
        public JsonResult SetDatosAlumno(decimal IdAlumno = 0)
        {
            SessionFixed.IdAlumno = IdAlumno.ToString();
            return Json(0,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListFacturas_PorIngresar(decimal IdTransaccionSession = 0, int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            //var lst = bus_det.get_list_cartera(IdEmpresa, 0, IdAlumno, false);

            //List_x_Cruzar.set_list(lst, IdTransaccionSession);

            return Json(0, JsonRequestBehavior.AllowGet);
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

            cxc_ConciliacionNotaCredito_Info model = new cxc_ConciliacionNotaCredito_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
                Fecha = DateTime.Now.Date
            };
            SessionFixed.IdAlumno = "0";
            return View(model);
        }
        #endregion
    }

    public class cxc_ConciliacionNotaCredito_List
    {
        string Variable = "cxc_ConciliacionNotaCredito_Info";
        public List<cxc_ConciliacionNotaCredito_Info> get_list(decimal IdTransaccionSession)
        {
            
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_ConciliacionNotaCredito_Info> list = new List<cxc_ConciliacionNotaCredito_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_ConciliacionNotaCredito_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_ConciliacionNotaCredito_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}