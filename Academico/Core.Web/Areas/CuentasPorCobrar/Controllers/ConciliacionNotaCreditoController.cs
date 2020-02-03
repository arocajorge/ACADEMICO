using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Bus.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using Core.Info.General;
using Core.Bus.General;

namespace Core.Web.Areas.CuentasPorCobrar.Controllers
{
    public class ConciliacionNotaCreditoController : Controller
    {
        #region Variables
        cxc_ConciliacionNotaCredito_List Lista = new cxc_ConciliacionNotaCredito_List();
        cxc_ConciliacionNotaCredito_Bus busConciliacion = new cxc_ConciliacionNotaCredito_Bus();
        cxc_ConciliacionNotaCreditoDet_Bus busConciliacionDet = new cxc_ConciliacionNotaCreditoDet_Bus();
        cxc_ConciliacionNotaCreditoDet_List ListaDet = new cxc_ConciliacionNotaCreditoDet_List();
        cxc_ConciliacionNotaCreditoDetPorCruzar_List ListaDetPorCruzar = new cxc_ConciliacionNotaCreditoDetPorCruzar_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        tb_persona_Bus busPersona = new tb_persona_Bus();
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

            Lista.set_list(busConciliacion.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            Lista.set_list(busConciliacion.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ConciliacionNC()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<cxc_ConciliacionNotaCredito_Info> model = Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionNC", model);
        }
        #endregion

        #region Alumno bajo demanda
        #region Combo bajo demanda Alumno
        public ActionResult Cmb_Alumno_ConciliacionNC()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno_ConciliacionNC", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return busPersona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return busPersona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        #endregion
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
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Nuevo(cxc_ConciliacionNotaCredito_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

            if (!busConciliacion.GuardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdConciliacion = model.IdConciliacion, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, decimal IdConciliacion = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_ConciliacionNotaCredito_Info model = busConciliacion.GetInfo(IdEmpresa, IdConciliacion);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            ListaDet.set_list(busConciliacionDet.GetList(model.IdEmpresa, model.IdConciliacion), model.IdTransaccionSession);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }


        [HttpPost]
        public ActionResult Modificar(cxc_ConciliacionNotaCredito_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

            if (!busConciliacion.ModificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdConciliacion = model.IdConciliacion, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, decimal IdConciliacion = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            cxc_ConciliacionNotaCredito_Info model = busConciliacion.GetInfo(IdEmpresa, IdConciliacion);
            if (model == null)
                return RedirectToAction("Index");

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            ListaDet.set_list(busConciliacionDet.GetList(model.IdEmpresa, model.IdConciliacion), model.IdTransaccionSession);
            
            return View(model);
        }


        [HttpPost]
        public ActionResult Anular(cxc_ConciliacionNotaCredito_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

            if (!busConciliacion.AnularDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Grid
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ConciliacionNC_PorCruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDetPorCruzar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ConciliacionNC_PorCruzar", model);
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

    public class cxc_ConciliacionNotaCreditoDet_List
    {
        string Variable = "cxc_ConciliacionNotaCreditoDet_Info";
        public List<cxc_ConciliacionNotaCreditoDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> list = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_ConciliacionNotaCreditoDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_ConciliacionNotaCreditoDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(cxc_ConciliacionNotaCreditoDet_Info info_det, decimal IdTransaccionSession)
        {
            List<cxc_ConciliacionNotaCreditoDet_Info> list = get_list(IdTransaccionSession);
            if (list.Where(q => q.Secuencia == info_det.Secuencia).FirstOrDefault() == null)
                list.Add(info_det);
            
        }

        public void UpdateRow(cxc_ConciliacionNotaCreditoDet_Info info_det, decimal IdTransaccionSession)
        {
            cxc_ConciliacionNotaCreditoDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.Valor = info_det.Valor;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<cxc_ConciliacionNotaCreditoDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }

    public class cxc_ConciliacionNotaCreditoDetPorCruzar_List
    {
        string Variable = "cxc_ConciliacionNotaCreditoDetPorCruzar_Info";
        public List<cxc_ConciliacionNotaCreditoDet_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> list = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<cxc_ConciliacionNotaCreditoDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<cxc_ConciliacionNotaCreditoDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}