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
    
    public partial class aca_Matricula
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_Matricula()
        {
            this.aca_MatriculaCalificacionParcial = new HashSet<aca_MatriculaCalificacionParcial>();
            this.aca_AlumnoRetiro = new HashSet<aca_AlumnoRetiro>();
            this.aca_MatriculaCambios = new HashSet<aca_MatriculaCambios>();
            this.aca_Matricula_Rubro = new HashSet<aca_Matricula_Rubro>();
            this.aca_MatriculaCalificacion = new HashSet<aca_MatriculaCalificacion>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public string Codigo { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public decimal IdPersonaF { get; set; }
        public decimal IdPersonaR { get; set; }
        public int IdPlantilla { get; set; }
        public decimal IdMecanismo { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public Nullable<int> IdEmpresa_rol { get; set; }
        public Nullable<decimal> IdEmpleado { get; set; }
    
        public virtual aca_Alumno aca_Alumno { get; set; }
        public virtual aca_AnioLectivo_Curso_Paralelo aca_AnioLectivo_Curso_Paralelo { get; set; }
        public virtual aca_MecanismoDePago aca_MecanismoDePago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCalificacionParcial> aca_MatriculaCalificacionParcial { get; set; }
        public virtual aca_Plantilla aca_Plantilla { get; set; }
        public virtual aca_MatriculaConducta aca_MatriculaConducta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AlumnoRetiro> aca_AlumnoRetiro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCambios> aca_MatriculaCambios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula_Rubro> aca_Matricula_Rubro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCalificacion> aca_MatriculaCalificacion { get; set; }
    }
}
