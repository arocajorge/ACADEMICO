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
            int IdEmpresa = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdSede = p_IdSede.Value == null ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdAnio = p_IdAnio.Value == null ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdJornada = p_IdJornada.Value == null ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdNivel = p_IdNivel.Value == null ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdCurso = p_IdCurso.Value == null ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = p_IdParalelo.Value == null ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);
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
