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
    
    public partial class aca_MatriculaCalificacionParcial
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdMateria { get; set; }
        public string Parcial { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
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
    
        public virtual aca_Materia aca_Materia { get; set; }
        public virtual aca_Matricula aca_Matricula { get; set; }
        public virtual aca_Profesor aca_Profesor { get; set; }
    }
}
