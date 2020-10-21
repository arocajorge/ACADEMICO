using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_013_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public int IdCatalogoParcial { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
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
        public decimal IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string NomMateria { get; set; }
        public string NomMateriaArea { get; set; }
        public string NomMateriaGrupo { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<int> OrdenMateriaGrupo { get; set; }
        public Nullable<int> OrdenMateriaArea { get; set; }
        public string NomCatalogo { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Calificacion1 { get; set; }
        public Nullable<decimal> Calificacion2 { get; set; }
        public Nullable<decimal> Calificacion3 { get; set; }
        public Nullable<decimal> Calificacion4 { get; set; }
        public Nullable<decimal> Remedial1 { get; set; }
        public Nullable<decimal> Remedial2 { get; set; }
        public Nullable<decimal> Evaluacion { get; set; }
        public string Letra { get; set; }
        public Nullable<decimal> Calificacion { get; set; }
        public string MotivoCalificacion { get; set; }
        public string AccionRemedial { get; set; }
        public Nullable<decimal> PromedioParcial { get; set; }
        public Nullable<int> SecuenciaPromedioConducta { get; set; }
        public string LetraPromedioConducta { get; set; }
        public Nullable<decimal> IdProfesorTutor { get; set; }
        public string NombreTutor { get; set; }
        public string NombreRepresentante { get; set; }
        public string CodigoEquivalenciaPromedio { get; set; }
        public string NomCatalogoTipo { get; set; }

        #region Campos que no existen en la base
        public int NoMostrarPromedio { get; set; }
        #endregion
    }
}
