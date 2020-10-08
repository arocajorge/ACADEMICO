using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Reportes.Academico;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_048_Rendimiento : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_048_Rendimiento()
        {
            InitializeComponent();
        }

        private void ACA_048_Rendimiento_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            aca_CatalogoTipo_Bus bus_catalogo_tipo = new aca_CatalogoTipo_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            int IdCatalogoParcialTipo = string.IsNullOrEmpty(p_IdCatalogoParcialTipo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoParcialTipo.Value);
            int IdMateria = string.IsNullOrEmpty(p_IdMateria.Value.ToString()) ? 0 : Convert.ToInt32(p_IdMateria.Value);

            ACA_048_Rendimiento_Bus bus_rpt = new ACA_048_Rendimiento_Bus();
            List<ACA_048_Rendimiento_Info> lst_rpt = new List<ACA_048_Rendimiento_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcialTipo);

            this.DataSource = lst_rpt;

            lst_rpt.ForEach(q => q.AlumnosConCalificacion = (q.AlumnosConCalificacion == null) ? 0 : q.AlumnosConCalificacion);
            xrChart1.DataSource = lst_rpt;
        }
    }
}
