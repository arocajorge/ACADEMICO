using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_028_ConductaBaja_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<int> SecuenciaQ1 { get; set; }
        public Nullable<decimal> CalificacionQ1 { get; set; }
        public string LetraQ1 { get; set; }
        public string MotivoPromedioFinalQ1 { get; set; }
        public Nullable<int> SecuenciaQ2 { get; set; }
        public Nullable<decimal> CalificacionQ2 { get; set; }
        public string LetraQ2 { get; set; }
        public string MotivoPromedioFinalQ2 { get; set; }
        public Nullable<int> SecuenciaPF { get; set; }
        public Nullable<decimal> CalificacionPF { get; set; }
        public string LetraPF { get; set; }
        public string MotivoPromedioFinal { get; set; }

        #region Campos que no existen
        public int Num { get; set; }
        public Nullable<decimal> Calificacion { get; set; }
        public string Letra { get; set; }
        public string Motivo { get; set; }
        #endregion
    }
}
