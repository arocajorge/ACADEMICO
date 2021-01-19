using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Info.Reportes.Academico;
using Core.Bus.Reportes.Academico;
using System.Collections.Generic;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_013_MatriculaCalificacionCualitativa_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_013_MatriculaCalificacionCualitativa_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_013_MatriculaCalificacionCualitativa_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            decimal IdMatricula = string.IsNullOrEmpty(p_IdMatricula.Value.ToString()) ? 0 : Convert.ToDecimal(p_IdMatricula.Value);
            int IdCatalogoParcial = string.IsNullOrEmpty(p_IdCatalogoParcial.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoParcial.Value);
            bool MostrarComportamiento = string.IsNullOrEmpty(p_MostrarComportamiento.Value.ToString()) ? false : Convert.ToBoolean(p_MostrarComportamiento.Value);

            ACA_013_MatriculaCalificacionCualitativa_Bus bus_rpt = new ACA_013_MatriculaCalificacionCualitativa_Bus();
            List<ACA_013_MatriculaCalificacionCualitativa_Info> lst_rpt = new List<ACA_013_MatriculaCalificacionCualitativa_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdMatricula, IdCatalogoParcial);

            this.DataSource = lst_rpt;
        }
    }
}
