using Core.Bus.Contabilidad;
using Core.Bus.General;
using Core.Info.Contabilidad;
using Core.Info.Helps;
using Core.Web.Helps;
using Core.Web.Reportes.Contabilidad;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class ContabilidadReportesController : Controller
    {
        #region Combos

        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_contable_Conta()
        {
            cl_filtros_Info model = new cl_filtros_Info();

            return PartialView("_CmbCuenta_contable_Conta", model);
        }

        public ActionResult CmbCuenta_contable_ContaFin()
        {
            cl_filtros_Info model = new cl_filtros_Info();

            return PartialView("_CmbCuenta_contable_ContaFin", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda_cta(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_cta(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }


        #endregion
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";

        public ActionResult CONTA_001(int IdTipoCbte = 0, decimal IdCbteCble = 0)
        {
            CONTA_001_Rpt model = new CONTA_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CONTA_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdTipoCbte.Value = IdTipoCbte;
            model.p_IdCbteCble.Value = IdCbteCble;
            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            model.RequestParameters = false;
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            ct_cbtecble_tipo_Bus bus_tipo = new ct_cbtecble_tipo_Bus();
            var lst_tipo = bus_tipo.get_list(IdEmpresa, false);
            ViewBag.lst_tipo = lst_tipo;

            //ct_punto_cargo_grupo_Bus bus_punto = new ct_punto_cargo_grupo_Bus();
            //var lst_punto = bus_punto.GetList(IdEmpresa, false);
            //ViewBag.lst_punto = lst_punto;
        }
        private void cargar_sucursal_check(int IdEmpresa, int[] intArray)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            if (intArray == null || intArray.Count() == 0)
            {
                lst_sucursal.Where(q => q.IdSucursal == Convert.ToInt32(SessionFixed.IdSucursal)).FirstOrDefault().Seleccionado = true;
            }
            else
                foreach (var item in lst_sucursal)
                {
                    item.Seleccionado = (intArray.Where(q => q == item.IdSucursal).Count() > 0 ? true : false);
                }
            ViewBag.lst_sucursal = lst_sucursal;
        }
        private void cargar_nivel()
        {
            Dictionary<int, string> lst_nivel = new Dictionary<int, string>();
            lst_nivel.Add(6, "Nivel 6");
            lst_nivel.Add(5, "Nivel 5");
            lst_nivel.Add(4, "Nivel 4");
            lst_nivel.Add(3, "Nivel 3");
            lst_nivel.Add(2, "Nivel 2");
            lst_nivel.Add(1, "Nivel 1");
            ViewBag.lst_nivel = lst_nivel;

            Dictionary<string, string> lst_balance = new Dictionary<string, string>();
            lst_balance.Add("BG", "Balance general");
            lst_balance.Add("ER", "Estado de resultado");
            lst_balance.Add("", "Balance de comprobación");
            ViewBag.lst_balance = lst_balance;
        }

        #region CONTA_002
        public ActionResult CONTA_002()
        {
            cl_filtros_contabilidad_Info model = new cl_filtros_contabilidad_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                balance = "FACTURAS"
            };
            CONTA_002_Rpt report = new CONTA_002_Rpt();

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.p_Tipo.Value = model.balance;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            report.RequestParameters = false;
            ViewBag.Report = report;

            CONTA_002_resumen_Rpt reportResumen = new CONTA_002_resumen_Rpt();
            reportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            reportResumen.p_FechaIni.Value = model.fecha_ini;
            reportResumen.p_FechaFin.Value = model.fecha_fin;
            reportResumen.p_Tipo.Value = model.balance;
            reportResumen.usuario = SessionFixed.IdUsuario;
            reportResumen.empresa = SessionFixed.NomEmpresa;
            reportResumen.RequestParameters = false;
            ViewBag.ReportResumen = reportResumen;
            CargarCombosCONTA_002();
            return View(model);
        }
        [HttpPost]
        public ActionResult CONTA_002(cl_filtros_contabilidad_Info model)
        {
            CONTA_002_Rpt report = new CONTA_002_Rpt();
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.p_Tipo.Value = model.balance;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            report.RequestParameters = false;
            ViewBag.Report = report;

            CONTA_002_resumen_Rpt reportResumen = new CONTA_002_resumen_Rpt();
            reportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            reportResumen.p_FechaIni.Value = model.fecha_ini;
            reportResumen.p_FechaFin.Value = model.fecha_fin;
            reportResumen.p_Tipo.Value = model.balance;
            reportResumen.usuario = SessionFixed.IdUsuario;
            reportResumen.empresa = SessionFixed.NomEmpresa;
            reportResumen.RequestParameters = false;
            ViewBag.ReportResumen = reportResumen;
            CargarCombosCONTA_002();
            return View(model);
        }

        public void CargarCombosCONTA_002()
        {
            Dictionary<string, string> lstTipo = new Dictionary<string, string>();
            lstTipo.Add("FACTURAS", "FACTURAS");
            lstTipo.Add("NOTAS DE CREDITO", "NOTAS DE CREDITO");
            lstTipo.Add("NOTAS DE DEBITO", "NOTAS DE DEBITO");
            lstTipo.Add("COBROS", "COBROS");
            lstTipo.Add("CONCILIACION DE NOTAS DE CREDITO", "CONCILIACION DE NOTAS DE CREDITO");
            lstTipo.Add("", "TODOS");
            ViewBag.lstTipo = lstTipo;
        }
        #endregion
    }
}