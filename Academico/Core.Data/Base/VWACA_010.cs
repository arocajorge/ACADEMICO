//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Data.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class VWACA_010
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public int IdCatalogoParcial { get; set; }
        public string NomCatalogo { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public string Profesor { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdPersona { get; set; }
        public string Alumno { get; set; }
        public Nullable<decimal> Calificacion1 { get; set; }
        public Nullable<decimal> Calificacion2 { get; set; }
        public Nullable<decimal> Calificacion3 { get; set; }
        public Nullable<decimal> Calificacion4 { get; set; }
        public Nullable<decimal> Evaluacion { get; set; }
        public Nullable<decimal> Remedial1 { get; set; }
        public Nullable<decimal> Remedial2 { get; set; }
        public Nullable<int> Conducta { get; set; }
        public string MotivoCalificacion { get; set; }
        public string MotivoConducta { get; set; }
        public string AccionRemedial { get; set; }
    }
}
