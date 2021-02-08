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
    
    public partial class aca_Plantilla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_Plantilla()
        {
            this.aca_AnioLectivo_Curso_Plantilla = new HashSet<aca_AnioLectivo_Curso_Plantilla>();
            this.aca_Plantilla_Rubro = new HashSet<aca_Plantilla_Rubro>();
            this.aca_Matricula_Rubro = new HashSet<aca_Matricula_Rubro>();
            this.aca_Matricula = new HashSet<aca_Matricula>();
            this.aca_PreMatricula_Rubro = new HashSet<aca_PreMatricula_Rubro>();
            this.aca_PreMatricula = new HashSet<aca_PreMatricula>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdPlantilla { get; set; }
        public string NomPlantilla { get; set; }
        public decimal Valor { get; set; }
        public string TipoDescuento { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public int IdTipoNota { get; set; }
        public Nullable<int> IdTipoPlantilla { get; set; }
        public Nullable<bool> AplicaParaTodo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Curso_Plantilla> aca_AnioLectivo_Curso_Plantilla { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Plantilla_Rubro> aca_Plantilla_Rubro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula_Rubro> aca_Matricula_Rubro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Matricula> aca_Matricula { get; set; }
        public virtual aca_PlantillaTipo aca_PlantillaTipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_PreMatricula_Rubro> aca_PreMatricula_Rubro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_PreMatricula> aca_PreMatricula { get; set; }
    }
}
