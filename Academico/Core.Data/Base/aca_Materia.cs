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
    
    public partial class aca_Materia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_Materia()
        {
            this.aca_AnioLectivo_Curso_Materia = new HashSet<aca_AnioLectivo_Curso_Materia>();
            this.aca_AnioLectivo_Paralelo_Profesor = new HashSet<aca_AnioLectivo_Paralelo_Profesor>();
            this.aca_MatriculaCalificacionParcial = new HashSet<aca_MatriculaCalificacionParcial>();
            this.aca_MatriculaCalificacionCualitativa = new HashSet<aca_MatriculaCalificacionCualitativa>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdMateria { get; set; }
        public Nullable<int> IdMateriaGrupo { get; set; }
        public Nullable<int> IdMateriaArea { get; set; }
        public string NomMateria { get; set; }
        public bool EsObligatorio { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<int> IdCatalogoTipoCalificacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Curso_Materia> aca_AnioLectivo_Curso_Materia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Paralelo_Profesor> aca_AnioLectivo_Paralelo_Profesor { get; set; }
        public virtual aca_Catalogo aca_Catalogo { get; set; }
        public virtual aca_MateriaArea aca_MateriaArea { get; set; }
        public virtual aca_MateriaGrupo aca_MateriaGrupo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCalificacionParcial> aca_MatriculaCalificacionParcial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_MatriculaCalificacionCualitativa> aca_MatriculaCalificacionCualitativa { get; set; }
    }
}
