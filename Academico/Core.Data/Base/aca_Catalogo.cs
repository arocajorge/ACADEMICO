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
    
    public partial class aca_Catalogo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_Catalogo()
        {
            this.aca_PermisoMatricula = new HashSet<aca_PermisoMatricula>();
            this.aca_MatriculaCondicionalParrafo = new HashSet<aca_MatriculaCondicionalParrafo>();
            this.aca_MatriculaCondicional = new HashSet<aca_MatriculaCondicional>();
            this.aca_MatriculaCalificacionParcial = new HashSet<aca_MatriculaCalificacionParcial>();
            this.aca_AnioLectivoParcial = new HashSet<aca_AnioLectivoParcial>();
            this.aca_Materia = new HashSet<aca_Materia>();
            this.aca_MatriculaCalificacionCualitativa = new HashSet<aca_MatriculaCalificacionCualitativa>();
            this.aca_Matricula = new HashSet<aca_Matricula>();
            this.aca_Admision = new HashSet<aca_Admision>();
            this.aca_Admision1 = new HashSet<aca_Admision>();
            this.aca_Admision2 = new HashSet<aca_Admision>();
            this.aca_Admision3 = new HashSet<aca_Admision>();
            this.aca_Alumno = new HashSet<aca_Alumno>();
            this.aca_Alumno1 = new HashSet<aca_Alumno>();
        }
    
        public int IdCatalogo { get; set; }
        public int IdCatalogoTipo { get; set; }
        public string Codigo { get; set; }
        public string NomCatalogo { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        public virtual aca_CatalogoTipo aca_CatalogoTipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_PermisoMatricula> aca_PermisoMatricula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCondicionalParrafo> aca_MatriculaCondicionalParrafo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCondicional> aca_MatriculaCondicional { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCalificacionParcial> aca_MatriculaCalificacionParcial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivoParcial> aca_AnioLectivoParcial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Materia> aca_Materia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCalificacionCualitativa> aca_MatriculaCalificacionCualitativa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula> aca_Matricula { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Admision> aca_Admision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Admision> aca_Admision1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Admision> aca_Admision2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Admision> aca_Admision3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Alumno> aca_Alumno { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Alumno> aca_Alumno1 { get; set; }
    }
}
