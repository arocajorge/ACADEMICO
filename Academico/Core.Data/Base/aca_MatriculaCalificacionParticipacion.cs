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
    
    public partial class aca_MatriculaCalificacionParticipacion
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public int IdTematica { get; set; }
        public int IdCampoAccion { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<decimal> CalificacionP1 { get; set; }
        public Nullable<decimal> CalificacionP2 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<decimal> CalificacionP3 { get; set; }
        public Nullable<decimal> CalificacionP4 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }
        public string PromedioFinal { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual aca_Alumno aca_Alumno { get; set; }
        public virtual aca_Alumno aca_Alumno1 { get; set; }
        public virtual aca_Matricula aca_Matricula { get; set; }
        public virtual aca_Matricula aca_Matricula1 { get; set; }
        public virtual aca_Profesor aca_Profesor { get; set; }
        public virtual aca_CampoAccion aca_CampoAccion { get; set; }
        public virtual aca_Tematica aca_Tematica { get; set; }
    }
}
