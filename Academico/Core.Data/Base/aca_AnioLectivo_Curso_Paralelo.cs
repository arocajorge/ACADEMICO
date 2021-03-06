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
    
    public partial class aca_AnioLectivo_Curso_Paralelo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_AnioLectivo_Curso_Paralelo()
        {
            this.aca_Matricula_Rubro = new HashSet<aca_Matricula_Rubro>();
            this.aca_PreMatricula_Rubro = new HashSet<aca_PreMatricula_Rubro>();
            this.aca_Matricula = new HashSet<aca_Matricula>();
            this.aca_PreMatricula = new HashSet<aca_PreMatricula>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenParalelo { get; set; }
        public Nullable<decimal> IdProfesorTutor { get; set; }
        public Nullable<decimal> IdProfesorInspector { get; set; }
    
        public virtual aca_Curso aca_Curso { get; set; }
        public virtual aca_Jornada aca_Jornada { get; set; }
        public virtual aca_NivelAcademico aca_NivelAcademico { get; set; }
        public virtual aca_Paralelo aca_Paralelo { get; set; }
        public virtual aca_Profesor aca_Profesor { get; set; }
        public virtual aca_Profesor aca_Profesor1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula_Rubro> aca_Matricula_Rubro { get; set; }
        public virtual aca_Sede aca_Sede { get; set; }
        public virtual aca_AnioLectivo aca_AnioLectivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_PreMatricula_Rubro> aca_PreMatricula_Rubro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula> aca_Matricula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_PreMatricula> aca_PreMatricula { get; set; }
    }
}
