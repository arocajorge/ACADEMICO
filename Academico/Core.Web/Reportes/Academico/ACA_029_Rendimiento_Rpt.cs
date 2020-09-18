using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Reportes.Academico;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_029_Rendimiento_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_029_Rendimiento_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_029_Rendimiento_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            aca_CatalogoTipo_Bus bus_catalogo_tipo = new aca_CatalogoTipo_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            int IdCatalogoTipo = string.IsNullOrEmpty(p_IdCatalogoTipo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoTipo.Value);
            int IdMateria = string.IsNullOrEmpty(p_IdMateria.Value.ToString()) ? 0 : Convert.ToInt32(p_IdMateria.Value);

            ACA_029_Rendimiento_Bus bus_rpt = new ACA_029_Rendimiento_Bus();
            List<ACA_029_Rendimiento_Info> lst_rpt = new List<ACA_029_Rendimiento_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoTipo);

            this.DataSource = lst_rpt;
        }
    }
}
