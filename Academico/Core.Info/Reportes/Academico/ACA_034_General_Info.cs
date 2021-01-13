using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_034_General_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> IdMateria { get; set; }
        public string NombreMateria { get; set; }
        public string NombreGrupo { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<int> OrdenGrupo { get; set; }
        public Nullable<int> PromediarGrupo { get; set; }
        public Nullable<int> IdCatalogoTipoCalificacion { get; set; }
        public string Codigo { get; set; }
        public string NombreAlumno { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string NombreTutor { get; set; }
        public string Calificacion { get; set; }
        public Nullable<decimal> CalificacionNumerica { get; set; }
        public string Columna { get; set; }
        public int OrdenColumna { get; set; }
        public Nullable<decimal> PromedioCalculado { get; set; }
        public Nullable<decimal> SupletorioCalculado { get; set; }
        public Nullable<decimal> PromedioFinalCalculado { get; set; }
        public Nullable<decimal> SumaGeneral { get; set; }
        public int NoTieneCalificacion { get; set; }
    }
}
