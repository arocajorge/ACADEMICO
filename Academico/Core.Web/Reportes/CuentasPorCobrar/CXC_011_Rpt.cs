using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using Core.Info.Reportes.CuentasPorCobrar;
using Core.Bus.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using Core.Bus.Academico;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_011_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public int[] IntArray { get; set; }

        public CXC_011_Rpt()
        {
            InitializeComponent();
        }

        private void CXC_011_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_sede.Text = empresa;
            lbl_usuario.Text = usuario;
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSede = p_IdSede.Value == null ? 0 : Convert.ToInt32(p_IdSede.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);

            CXC_011_Bus bus_rpt = new CXC_011_Bus();
            List<CXC_011_Info> lst_rpt = new List<CXC_011_Info>();

            lst_rpt.AddRange(bus_rpt.get_list(IdEmpresa, IdAlumno));
            this.DataSource = lst_rpt;

            if (lst_rpt.Count > 0)
            {
                if (lst_rpt.FindLast(q => q.MostrarValoresDesdeHasta == q.MostrarValoresDesdeHasta).MostrarValoresDesdeHasta == true)
                {
                    ValoresDesdeHasta.Visible = true;
                }
                else
                {
                    ValoresDesdeHasta.Visible = false;
                }
            }
            else
            {
                ValoresDesdeHasta.Visible = false;
            }

            tb_mes_Bus bus_mes = new tb_mes_Bus();
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null && emp.em_logo != null)
            {
                ImageConverter obj = new ImageConverter();
                lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
            }

            DateTime fecha = DateTime.Now;
            var mes = fecha.Month;
            var lst_mes = bus_mes.get_list();
            var descripcion_mes = "";
            foreach (var item in lst_mes)
            {
                if (item.idMes==mes)
                {
                    descripcion_mes = item.smes;
                }
            }

            Fecha.Text = "Guayaquil, " + fecha.Day.ToString() + " de " + descripcion_mes + " de " + fecha.Year.ToString();
            lbl_texto.Text = "Mediante reporte generado con corte al" + fecha.Date + ", el departamento de Cobranzas informa el detalle de su estado de cuenta, considerando las facturas pendientes de pago:";

            aca_Sede_Bus bus_sede = new aca_Sede_Bus();
            var sede = bus_sede.GetInfo(IdEmpresa, IdSede);
            if (sede!=null)
            {
                lbl_sede.Text =  sede.NomSede.ToUpper();
                lbl_direccion.Text = sede.Direccion.ToUpper();
            }
        }
    }
}
