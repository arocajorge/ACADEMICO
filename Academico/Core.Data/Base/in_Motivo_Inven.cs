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
    
    public partial class in_Motivo_Inven
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public in_Motivo_Inven()
        {
            this.in_Ing_Egr_Inven = new HashSet<in_Ing_Egr_Inven>();
            this.in_movi_inve = new HashSet<in_movi_inve>();
            this.in_parametro = new HashSet<in_parametro>();
            this.in_parametro1 = new HashSet<in_parametro>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdMotivo_Inv { get; set; }
        public string Cod_Motivo_Inv { get; set; }
        public string Desc_mov_inv { get; set; }
        public string Genera_Movi_Inven { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaHoraAnul { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
        public string Tipo_Ing_Egr { get; set; }
        public string IdCtaCble { get; set; }
    
        public virtual in_Catalogo in_Catalogo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<in_Ing_Egr_Inven> in_Ing_Egr_Inven { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<in_movi_inve> in_movi_inve { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<in_parametro> in_parametro { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<in_parametro> in_parametro1 { get; set; }
    }
}
