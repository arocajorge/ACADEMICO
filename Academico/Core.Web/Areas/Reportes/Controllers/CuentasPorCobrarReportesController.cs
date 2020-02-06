using Core.Bus.General;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using Core.Web.Reportes.CuentasPorCobrar;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class CuentasPorCobrarReportesController : Controller
    {
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        string RootReporte = System.IO.Path.GetTempPath() + "Rpt_Facturacion.repx";
        tb_persona_Bus BusPersona = new tb_persona_Bus();

        #region Combos
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return BusPersona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return BusPersona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }        
        #endregion

        #region CargarCombos
        private void CargarSucursal(cl_filtros_facturacion_Info model)
        {
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            var lst_sucursal = bus_sucursal.get_list(model.IdEmpresa, false);
            lst_sucursal.Add(new Info.General.tb_sucursal_Info
            {
                IdSucursal = 0,
                Su_Descripcion = "TODAS"
            });
            ViewBag.lst_sucursal = lst_sucursal;
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
        #endregion

        public ActionResult CXC_001()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCliente = 0,
                IntArray = new int[] { Convert.ToInt32(SessionFixed.IdSucursal) }
            };
            CargarSucursal(model);
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            CXC_001_Rpt report = new CXC_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.IntArray = model.IntArray;
            report.p_IdCliente.Value = model.IdAlumno == null ? 0 : Convert.ToDecimal(model.IdAlumno);
            report.p_fecha_corte.Value = model.fecha_corte;
            report.p_mostrarSaldo0.Value = model.mostrarSaldo0;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_001(cl_filtros_facturacion_Info model)
        {
            CXC_001_Rpt report = new CXC_001_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_001");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            CargarSucursal(model);
            report.IntArray = model.IntArray;
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdCliente.Value = model.IdAlumno == null ? 0 : Convert.ToDecimal(model.IdAlumno);
            report.p_fecha_corte.Value = model.fecha_corte;
            report.p_mostrarSaldo0.Value = model.mostrarSaldo0;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            cargar_sucursal_check(model.IdEmpresa, model.IntArray);
            ViewBag.Report = report;

            return View(model);
        }

    }
}