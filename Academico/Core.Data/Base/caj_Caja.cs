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
    
    public partial class caj_Caja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public caj_Caja()
        {
            this.cp_conciliacion_Caja = new HashSet<cp_conciliacion_Caja>();
            this.caj_Caja_x_seg_usuario = new HashSet<caj_Caja_x_seg_usuario>();
            this.caj_Caja_Movimiento = new HashSet<caj_Caja_Movimiento>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdCaja { get; set; }
        public int IdSucursal { get; set; }
        public string ca_Codigo { get; set; }
        public string ca_Descripcion { get; set; }
        public string IdCtaCble { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string Estado { get; set; }
        public string IdUsuario_Responsable { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cp_conciliacion_Caja> cp_conciliacion_Caja { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caj_Caja_x_seg_usuario> caj_Caja_x_seg_usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<caj_Caja_Movimiento> caj_Caja_Movimiento { get; set; }
    }
}
