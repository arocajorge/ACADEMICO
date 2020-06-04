using Core.Bus.Contabilidad.Contabilizacion;
using Core.Bus.CuentasPorCobrar;
using Core.Bus.Facturacion;
using Core.Info.Contabilidad.Contabilizacion;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Contabilidad.Controllers
{
    public class AuditoriaContableController : Controller
    {
        #region Variables
        ct_ContabilizacionFacturas_Bus busContaFactura = new ct_ContabilizacionFacturas_Bus();
        ct_ContabilizacionNotas_Bus busContaNota = new ct_ContabilizacionNotas_Bus();
        ct_ContabilizacionCobros_Bus busContaCobro = new ct_ContabilizacionCobros_Bus();
        ct_ContabilizacionConciliacionNC_Bus bus_ContaConciliacionNC = new ct_ContabilizacionConciliacionNC_Bus();

        fa_factura_Bus busFactura = new fa_factura_Bus();
        cxc_cobro_Bus busCobro = new cxc_cobro_Bus();
        fa_notaCreDeb_Bus busNota = new fa_notaCreDeb_Bus();

        ct_ContabilizacionFacturas_List ListaContaFactura = new ct_ContabilizacionFacturas_List();
        ct_ContabilizacionCobros_List ListaContaCobro = new ct_ContabilizacionCobros_List();
        ct_ContabilizacionNotas_List ListaContaNota = new ct_ContabilizacionNotas_List();
        ct_ContabilizacionConciliacionNC_List ListaContaConciliacion = new ct_ContabilizacionConciliacionNC_List();
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

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            ListaContaFactura.set_list(busContaFactura.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            ListaContaNota.set_list(busContaNota.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            ListaContaCobro.set_list(busContaCobro.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            ListaContaConciliacion.set_list(bus_ContaConciliacionNC.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            ListaContaFactura.set_list(busContaFactura.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            ListaContaNota.set_list(busContaNota.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            ListaContaCobro.set_list(busContaCobro.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            ListaContaConciliacion.set_list(bus_ContaConciliacionNC.GetList(model.IdEmpresa, model.fecha_ini, model.fecha_fin), model.IdTransaccionSession);
            return View(model);
        }
        #endregion

        #region Factura
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ContaFactura()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaContaFactura.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ContaFactura", model);
        }
        #endregion

        #region Nota
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ContaNota()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaContaNota.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ContaNota", model);
        }
        #endregion

        #region Cobro
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ContaCobro()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaContaCobro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ContaCobro", model);
        }
        #endregion

        #region Conciliacion
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ContaConciliacionNC()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaContaConciliacion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ContaConciliacionNC", model);
        }
        #endregion

        #region Json
        public JsonResult ContabilizarFactura(int IdEmpresa=0, string Ids="")
        {
            var resultado = "";
            string[] array = Ids.Split(',');
            int IdSucursal = 0;
            int IdBodega = 0;
            decimal IdCbteVta = 0;

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    IdSucursal = Convert.ToInt32(item.Substring(0, 4));
                    IdBodega = Convert.ToInt32(item.Substring(4, 4));
                    IdCbteVta = Convert.ToDecimal(item.Substring(8, 10));

                    if (!busFactura.Contabilizar(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, ""))
                    {
                        mensaje = "No se ha contabilizado las facturas";
                        ViewBag.mensaje = mensaje;
                        resultado = mensaje;
                    }
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ContabilizarNota(int IdEmpresa = 0, string Ids = "")
        {
            var resultado = "";
            string[] array = Ids.Split(',');
            int IdSucursal = 0;
            int IdBodega = 0;
            decimal IdNota = 0;

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    IdSucursal = Convert.ToInt32(item.Substring(0, 4));
                    IdBodega = Convert.ToInt32(item.Substring(4, 4));
                    IdNota = Convert.ToDecimal(item.Substring(8, 10));

                    if (!busNota.Contabilizar(IdEmpresa, IdSucursal, IdBodega, IdNota))
                    {
                        mensaje = "No se ha contabilizado las facturas";
                        ViewBag.mensaje = mensaje;
                        resultado = mensaje;
                    }
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ContabilizarCobro(int IdEmpresa = 0, string Ids = "")
        {
            var resultado = "";
            string[] array = Ids.Split(',');
            int IdSucursal = 0;
            decimal IdCobro = 0;

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    IdSucursal = Convert.ToInt32(item.Substring(0, 4));
                    IdCobro = Convert.ToDecimal(item.Substring(4, 10));
                    if (!busCobro.Contabilizar(IdEmpresa, IdSucursal, IdCobro))
                    {
                        mensaje = "No se ha contabilizado el cobro # "+IdCobro.ToString();
                        ViewBag.mensaje = mensaje;
                        resultado = mensaje;
                    }
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ContabilizarConciliacionNC(int IdEmpresa = 0, string Ids = "")
        {
            var resultado = "";
            string[] array = Ids.Split(',');
            int IdSucursal = 0;
            decimal IdConciliacion = 0;

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    IdSucursal = Convert.ToInt32(item.Substring(0, 4));
                    IdConciliacion = Convert.ToDecimal(item.Substring(4, 10));
                    //if (!busCobro.Contabilizar(IdEmpresa, IdSucursal, IdCobro))
                    //{
                    //    mensaje = "No se ha contabilizado la conciliacionc  # " + IdCobro.ToString();
                    //    ViewBag.mensaje = mensaje;
                    //    resultado = mensaje;
                    //}
                }
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class ct_ContabilizacionFacturas_List
    {
        string Variable = "ct_ContabilizacionFacturas_Info";
        public List<ct_ContabilizacionFacturas_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_ContabilizacionFacturas_Info> list = new List<ct_ContabilizacionFacturas_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_ContabilizacionFacturas_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_ContabilizacionFacturas_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }

    public class ct_ContabilizacionNotas_List
    {
        string Variable = "ct_ContabilizacionNotas_Info";
        public List<ct_ContabilizacionNotas_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_ContabilizacionNotas_Info> list = new List<ct_ContabilizacionNotas_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_ContabilizacionNotas_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_ContabilizacionNotas_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }

    public class ct_ContabilizacionCobros_List
    {
        string Variable = "ct_ContabilizacionCobros_Info";
        public List<ct_ContabilizacionCobros_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_ContabilizacionCobros_Info> list = new List<ct_ContabilizacionCobros_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_ContabilizacionCobros_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_ContabilizacionCobros_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }

    public class ct_ContabilizacionConciliacionNC_List
    {
        string Variable = "ct_ContabilizacionConciliacionNC_Info";
        public List<ct_ContabilizacionConciliacionNC_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_ContabilizacionConciliacionNC_Info> list = new List<ct_ContabilizacionConciliacionNC_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_ContabilizacionConciliacionNC_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_ContabilizacionConciliacionNC_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }
}