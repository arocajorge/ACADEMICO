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
        cxc_ConciliacionNotaCreditoDetPorCruzar_List lstDetPorCruzar = new cxc_ConciliacionNotaCreditoDetPorCruzar_List();
        cxc_ConciliacionNotaCreditoDet_List lstDet = new cxc_ConciliacionNotaCreditoDet_List();
        cxc_ConciliacionNotaCreditoDet_Bus busDet = new cxc_ConciliacionNotaCreditoDet_Bus();
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
            var lst = busDet.GetListPorCruzar(IdEmpresa, IdAlumno);
            lstDetPorCruzar.set_list(lst, IdTransaccionSession);
            return Json(lst.Count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditingAddNewFactura(string IDs = "", double TotalACobrar = 0, decimal IdTransaccionSession = 0)
        {
            double saldo = TotalACobrar;
            double ValorProntoPago = 0;
            if (IDs != "")
            {
                int IdEmpresaSesion = Convert.ToInt32(SessionFixed.IdEmpresa);
                var lst_x_ingresar = lstDetPorCruzar.get_list(IdTransaccionSession);
                string[] array = IDs.Split(',');

                foreach (var item in array)
                {
                    var info_det = lst_x_ingresar.Where(q => q.secuencia == item).FirstOrDefault();
                    if (info_det != null)
                        lstDet.AddRow(info_det, IdTransaccionSession);
                }
            }
            var lst = lstDet.get_list(IdTransaccionSession);
            foreach (var item in lst)
            {
                ValorProntoPago = (item.vt_total - item.ValorProntoPago ?? 0);
                if (saldo > 0)
                {
                    item.ValorProntoPago = saldo >= ValorProntoPago ? ValorProntoPago : 0;
                    item.Valor = saldo >= Convert.ToDouble(item.Saldo) ? Convert.ToDouble(item.Saldo) - ValorProntoPago : saldo;
                    item.Saldo_final = Convert.ToDouble(item.Saldo - ValorProntoPago) - item.Valor;
                    saldo = saldo - item.Valor;
                }
                else
                    item.Valor = 0;
            }
            lstDet.set_list(lst, IdTransaccionSession);

            var resultado = saldo;
            return Json(Math.Round(resultado, 2, MidpointRounding.AwayFromZero), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSaldoNC(int IdEmpresa = 0, string IdString = "")
        {
            double Valor = 0;
            var NC = bus_Nc.get_info(IdEmpresa, IdString);
            if (NC != null)
            {
                Valor = NC.sc_saldo ?? 0;
            }
            return Json(Valor,JsonRequestBehavior.AllowGet);
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

        #region Grid
        public ActionResult GridViewPartial_ConciliacionNCPorConciliar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = lstDetPorCruzar.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionNCPorConciliar", model);
        }


        public ActionResult GridViewPartial_ConciliacionNCDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = lstDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ConciliacionNCDet", model);
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
            if (list.Where(q => q.secuencia == info_det.secuencia).FirstOrDefault() == null)
            {
                info_det.Saldo_final = Convert.ToDouble(info_det.Saldo) - info_det.Valor;
                list.Add(info_det);
            }
        }

        public void UpdateRow(cxc_ConciliacionNotaCreditoDet_Info info_det, decimal IdTransaccionSession)
        {
            cxc_ConciliacionNotaCreditoDet_Info edited_info = get_list(IdTransaccionSession).Where(m => m.secuencia == info_det.secuencia).First();
            edited_info.Saldo_final = Convert.ToDouble(edited_info.Saldo) - info_det.Valor;
            edited_info.Valor = info_det.Valor;
        }

        public void DeleteRow(string secuencia, decimal IdTransaccionSession)
        {
            List<cxc_ConciliacionNotaCreditoDet_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.secuencia == secuencia).FirstOrDefault());
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