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
    
    public partial class aca_CatalogoFicha
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_CatalogoFicha()
        {
            this.aca_SocioEconomico = new HashSet<aca_SocioEconomico>();
            this.aca_SocioEconomico1 = new HashSet<aca_SocioEconomico>();
            this.aca_SocioEconomico2 = new HashSet<aca_SocioEconomico>();
            this.aca_SocioEconomico3 = new HashSet<aca_SocioEconomico>();
            this.aca_SocioEconomico4 = new HashSet<aca_SocioEconomico>();
            this.aca_SocioEconomico5 = new HashSet<aca_SocioEconomico>();
            this.aca_Familia = new HashSet<aca_Familia>();
        }
    
        public int IdCatalogoFicha { get; set; }
        public int IdCatalogoTipoFicha { get; set; }
        public string Codigo { get; set; }
        public string NomCatalogoFicha { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    
        public virtual aca_CatalogoTipoFicha aca_CatalogoTipoFicha { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_SocioEconomico> aca_SocioEconomico5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_Familia> aca_Familia { get; set; }
    }
}
