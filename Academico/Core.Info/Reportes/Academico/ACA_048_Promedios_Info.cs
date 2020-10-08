using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_048_Promedios_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public Nullable<int> IdMateria { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }

        #region Campos que no existen
        public Nullable<decimal> PromedioFinal { get; set; }
        #endregion
    }
}
