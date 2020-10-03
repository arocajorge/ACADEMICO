using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_038_Rubros_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public int IdRubro { get; set; }
        public string Pension { get; set; }
        public double TotalFacturado { get; set; }
        public double TotalCobrado { get; set; }
     
    }
}
