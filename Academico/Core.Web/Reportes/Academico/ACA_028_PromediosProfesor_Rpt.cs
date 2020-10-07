using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Info.Helps;
using System.Linq;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_028_PromediosProfesor_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public ACA_028_PromediosProfesor_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_028_PromediosProfesor_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            int IdCatalogoTipo = string.IsNullOrEmpty(p_IdCatalogoTipo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoTipo.Value);
            ACA_028_Promedios_Bus bus_rpt = new ACA_028_Promedios_Bus();
            List<ACA_028_Promedios_Info> lst_rpt = new List<ACA_028_Promedios_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoTipo);
            var lst_cuantitativas = lst_rpt.Where(q=>q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
            decimal SumaPromedio = 0;
            var PromedioGeneral = "";
            var cont = 0;
            foreach (var item in lst_cuantitativas)
            {
                if(string.IsNullOrEmpty(item.PromedioQuimestre))
                {
                    cont++;
                }
                else
                {
                    SumaPromedio = SumaPromedio + Convert.ToDecimal(item.PromedioQuimestre);
                }
            }

            if (cont == 0)
            {
                if (lst_cuantitativas.Count() >0)
                {
                    decimal Calculo = Math.Round(Convert.ToDecimal(SumaPromedio / lst_cuantitativas.Count()), 2, MidpointRounding.AwayFromZero);
                    PromedioGeneral = Convert.ToString(Calculo);
                }
            }

            Promedio.Text = PromedioGeneral;
            this.DataSource = lst_rpt;
        }
    }
}
