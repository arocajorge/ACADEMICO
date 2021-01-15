using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.CuentasPorCobrar;
using System.Collections.Generic;
using Core.Bus.Reportes.CuentasPorCobrar;

namespace Core.Web.Reportes.CuentasPorCobrar
{
    public partial class CXC_013_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public CXC_013_Rpt()
        {
            InitializeComponent();
        }
        
        private void CXC_013_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;


            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdAlumno.Value);
            DateTime Fecha = string.IsNullOrEmpty(p_FechaCorte.Value.ToString()) ? DateTime.Now.Date : Convert.ToDateTime(p_FechaCorte.Value.ToString());
            CXC_013_Bus bus_rpt = new CXC_013_Bus();
            List<CXC_013_Info> lst_rpt = new List<CXC_013_Info>();

            lst_rpt.AddRange(bus_rpt.get_list(IdEmpresa, IdAnio, IdSede, IdJornada, IdNivel, IdCurso, IdParalelo, IdAlumno));
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

            lblHeader.Text = lblHeader.Text.Replace("[FECHA]", Fecha.ToString("d 'de' MMMM 'de' yyyy"));
            lblHeader.Text = lblHeader.Text.Replace("[REPRESENTANTE]", lst_rpt.Count > 0 ? lst_rpt[0].Representante : "");
            lblFooter.Text = lblFooter.Text.Replace("[CODIGO]", lst_rpt.Count > 0 ? lst_rpt[0].Codigo : "");
        }

        private void ProntoPagoHasta_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
