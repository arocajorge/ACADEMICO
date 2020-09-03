using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_024_Info
    {
        public int Num { get; set; }
        public Nullable<int> IdEmpresa { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
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
        public Nullable<int> IdCatalogoParcial { get; set; }
        public string NomCatalogo { get; set; }
        public Nullable<int> IdMateria { get; set; }
        public string NomMateria { get; set; }
        public Nullable<int> OrdenMateriaArea { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<decimal> Calificacion1 { get; set; }
        public string C1 { get; set; }
        public Nullable<decimal> Calificacion2 { get; set; }
        public string C2 { get; set; }
        public Nullable<decimal> Calificacion3 { get; set; }
        public string C3 { get; set; }
        public Nullable<decimal> Calificacion4 { get; set; }
        public string C4 { get; set; }
        public Nullable<decimal> Evaluacion { get; set; }
        public string Ev { get; set; }
    }
}
