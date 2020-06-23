using Core.Bus.Academico;
using Core.Bus.General;
using Core.Bus.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Info.SeguridadAcceso;
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
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();

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

        private void cargar_usuario_check(int IdEmpresa, string[] StringArray)
        {
            seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
            List<seg_usuario_Info> lst_usuario = new List<seg_usuario_Info>();
           var infoUsuario = bus_usuario.get_info(SessionFixed.IdUsuario);

            if (infoUsuario !=null && infoUsuario.es_super_admin==true)
            {
                lst_usuario = bus_usuario.GetListCobradores(IdEmpresa);
            }
            else
            {
                lst_usuario.Add(infoUsuario);
            }
            
            if (StringArray == null || StringArray.Count() == 0)
            {
                lst_usuario.Where(q => q.IdUsuario == SessionFixed.IdUsuario).FirstOrDefault().Seleccionado = true;
            }
            else
                foreach (var item in lst_usuario)
                {
                    item.Seleccionado = (StringArray.Where(q => q == item.IdUsuario).Count() > 0 ? true : false);
                }
            ViewBag.lst_usuario = lst_usuario;
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

        public ActionResult CXC_002(int IdSucursal = 0, decimal IdCobro = 0)
        {
            CXC_002_Rpt model = new CXC_002_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_002");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                model.LoadLayout(RootReporte);
            }
            #endregion
            model.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.p_IdSucursal.Value = IdSucursal;
            model.p_IdCobro.Value = IdCobro;
            model.usuario = SessionFixed.IdUsuario;
            
            return View(model);
        }

        public ActionResult CXC_003()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                fecha_ini = DateTime.Now.AddMonths(-1),
                fecha_fin = DateTime.Now,
                StringArray = new string[] { SessionFixed.IdUsuario }
            };

            cargar_usuario_check(model.IdEmpresa, model.StringArray);
            CXC_003_Rpt report = new CXC_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.StringArray = model.StringArray;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_003(cl_filtros_Info model)
        {
            CXC_003_Rpt report = new CXC_003_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_003");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            //CargarSucursal(model);
            
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.StringArray = model.StringArray;
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            cargar_usuario_check(model.IdEmpresa, model.StringArray);
            ViewBag.Report = report;

            return View(model);
        }

        public ActionResult CXC_004()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCliente = 0,
                
            };
           
            CXC_004_Rpt report = new CXC_004_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_FechaCorte.Value = model.fecha_corte;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_004(cl_filtros_facturacion_Info model)
        {
            CXC_004_Rpt report = new CXC_004_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_004");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdUsuario.Value = SessionFixed.IdUsuario;
            report.p_FechaCorte.Value = model.fecha_corte;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;

            return View(model);
        }
        public ActionResult CXC_005(int IdSucursal = 0, decimal IdLiquidacion = 0)
        {
            CXC_005_Rpt report = new CXC_005_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_005");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = Convert.ToInt32(SessionFixed.IdEmpresa);
            report.p_IdSucursal.Value = IdSucursal;
            report.p_IdLiquidacion.Value = IdLiquidacion;
            report.usuario = SessionFixed.IdUsuario.ToString();
            report.empresa = SessionFixed.NomEmpresa;
            ViewBag.Report = report;
            return View(report);
        }

        #region CXC_006
        private void CargarCombos_CXC_006(int IdEmpresa, int[] intArray)
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
        
        public ActionResult CXC_006()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdCliente = 0,

            };

            CXC_006_Rpt report = new CXC_006_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.IntArray = model.IntArray;
            report.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa ?? "0");
            ViewBag.Report = report;
            CargarCombos_CXC_006(model.IdEmpresa,model.IntArray);
            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_006(cl_filtros_facturacion_Info model)
        {
            CXC_006_Rpt report = new CXC_006_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_006");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_FechaIni.Value = model.fecha_ini;
            report.p_FechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.IntArray = model.IntArray;
            report.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa ?? "0");
            ViewBag.Report = report;
            CargarCombos_CXC_006(model.IdEmpresa, model.IntArray);
            return View(model);
        }
        #endregion

        #region CXC_007
        public ActionResult CXC_007()
        {
            cl_filtros_facturacion_Info model = new cl_filtros_facturacion_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
            };

            CXC_007_Rpt report = new CXC_007_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fechaCorte.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;
            report.RequestParameters = false;
            ViewBag.Report = report;

           CXC_007_Resumen_Rpt ReportResumen = new CXC_007_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportResum = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_007_Resumen");
            if (reportResum != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportResum.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_fechaCorte.Value = model.fecha_fin;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportResumen = ReportResumen;
            return View(model);
        }

        [HttpPost]
        public ActionResult CXC_007(cl_filtros_facturacion_Info model)
        {
            CXC_007_Rpt report = new CXC_007_Rpt();
            #region Cargo diseño desde base
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var reporte = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_007");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion
            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_fechaCorte.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            report.RequestParameters = false;
            ViewBag.Report = report;

            CXC_007_Resumen_Rpt ReportResumen = new CXC_007_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportResum = bus_rep_x_emp.GetInfo(IdEmpresa, "CXC_007_Resumen");
            if (reportResum != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportResum.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_fechaCorte.Value = model.fecha_fin;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportResumen = ReportResumen;
            return View(model);
        }
        #endregion

        #region CXC_008
        public ActionResult CXC_008()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = new aca_AnioLectivo_Info();
            info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = info_anio == null ? 0 : info_anio.IdAnio;
            model.mostrarAnulados = true;
            CXC_008_Rpt report = new CXC_008_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.p_FechaCorte.Value = model.fecha_fin;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;

            CXC_008_Resumen_Rpt ReportResumen = new CXC_008_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportResum = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_008_Resumen");
            if (reportResum != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportResum.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_IdAnio.Value = model.IdAnio;
            ReportResumen.p_IdSede.Value = model.IdSede;
            ReportResumen.p_IdNivel.Value = model.IdNivel;
            ReportResumen.p_IdJornada.Value = model.IdJornada;
            ReportResumen.p_IdCurso.Value = model.IdCurso;
            ReportResumen.p_IdParalelo.Value = model.IdParalelo;
            ReportResumen.p_FechaCorte.Value = model.fecha_fin;
            ReportResumen.p_IdAlumno.Value = model.IdAlumno;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportResumen = ReportResumen;

            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_008(cl_filtros_Info model)
        {
            CXC_008_Rpt report = new CXC_008_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_008");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAnio.Value = model.IdAnio;
            report.p_IdSede.Value = model.IdSede;
            report.p_IdNivel.Value = model.IdNivel;
            report.p_IdJornada.Value = model.IdJornada;
            report.p_IdCurso.Value = model.IdCurso;
            report.p_IdParalelo.Value = model.IdParalelo;
            report.p_FechaCorte.Value = model.fecha_fin;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;

            CXC_008_Resumen_Rpt ReportResumen = new CXC_008_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportResum = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_008_Resumen");
            if (reportResum != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportResum.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_IdAnio.Value = model.IdAnio;
            ReportResumen.p_IdSede.Value = model.IdSede;
            ReportResumen.p_IdNivel.Value = model.IdNivel;
            ReportResumen.p_IdJornada.Value = model.IdJornada;
            ReportResumen.p_IdCurso.Value = model.IdCurso;
            ReportResumen.p_IdParalelo.Value = model.IdParalelo;
            ReportResumen.p_FechaCorte.Value = model.fecha_fin;
            ReportResumen.p_IdAlumno.Value = model.IdAlumno;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;

            ViewBag.ReportResumen = ReportResumen;

            return View(model);
        }
        #endregion

        #region CXC_009
        public ActionResult CXC_009()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = new aca_AnioLectivo_Info();
            info_anio = bus_anio.GetInfo_AnioEnCurso(model.IdEmpresa, 0);

            model.IdAnio = info_anio == null ? 0 : info_anio.IdAnio;
            model.mostrarAnulados = true;
            CXC_009_Rpt report = new CXC_009_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_FechaIni.Value = model.fecha_ini.Date;
            report.p_FechaFin.Value = model.fecha_fin.Date;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;


            CXC_009_Resumen_Rpt ReportResumen = new CXC_009_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportResum = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_009_Resumen");
            if (reportResum != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportResum.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_FechaIni.Value = model.fecha_ini.Date;
            ReportResumen.p_FechaFin.Value = model.fecha_fin.Date;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;
            ViewBag.ReportResumen = ReportResumen;

            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_009(cl_filtros_Info model)
        {
            CXC_009_Rpt report = new CXC_009_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_009");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_FechaIni.Value = model.fecha_ini.Date;
            report.p_FechaFin.Value = model.fecha_fin.Date;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;


            CXC_009_Resumen_Rpt ReportResumen = new CXC_009_Resumen_Rpt();
            #region Cargo diseño desde base
            var reportResum = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_009_Resumen");
            if (reportResum != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reportResum.ReporteDisenio);
                ReportResumen.LoadLayout(RootReporte);
            }
            #endregion
            ReportResumen.p_IdEmpresa.Value = model.IdEmpresa;
            ReportResumen.p_FechaIni.Value = model.fecha_ini.Date;
            ReportResumen.p_FechaFin.Value = model.fecha_fin.Date;
            ReportResumen.usuario = SessionFixed.IdUsuario;
            ReportResumen.empresa = SessionFixed.NomEmpresa;
            ViewBag.ReportResumen = ReportResumen;
            return View(model);
        }
        #endregion

        #region CXC_010
        public ActionResult CXC_010()
        {
            cl_filtros_Info model = new cl_filtros_Info();
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            CXC_010_Rpt report = new CXC_010_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }
        [HttpPost]
        public ActionResult CXC_010(cl_filtros_Info model)
        {
            CXC_010_Rpt report = new CXC_010_Rpt();

            #region Cargo diseño desde base
            var reporte = bus_rep_x_emp.GetInfo(model.IdEmpresa, "CXC_010");
            if (reporte != null)
            {
                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                report.LoadLayout(RootReporte);
            }
            #endregion

            report.p_IdEmpresa.Value = model.IdEmpresa;
            report.p_IdAlumno.Value = model.IdAlumno;
            report.p_fechaIni.Value = model.fecha_ini;
            report.p_fechaFin.Value = model.fecha_fin;
            report.usuario = SessionFixed.IdUsuario;
            report.empresa = SessionFixed.NomEmpresa;

            ViewBag.Report = report;
            return View(model);
        }
        #endregion
    }
}