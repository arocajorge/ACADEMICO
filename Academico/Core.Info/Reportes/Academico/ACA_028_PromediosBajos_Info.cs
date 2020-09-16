using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_028_PromediosBajos_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }
        public Nullable<double> PromedioMinimoPromocion { get; set; }


        public Nullable<decimal> PromedioQuimestral { get; set; }
        
    }
}
