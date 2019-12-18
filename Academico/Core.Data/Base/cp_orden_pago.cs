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
    
    public partial class cp_orden_pago
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cp_orden_pago()
        {
            this.cp_orden_pago_det = new HashSet<cp_orden_pago_det>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int IdSucursal { get; set; }
        public string Observacion { get; set; }
        public string IdTipo_op { get; set; }
        public string IdTipo_Persona { get; set; }
        public decimal IdPersona { get; set; }
        public decimal IdEntidad { get; set; }
        public System.DateTime Fecha { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public string IdFormaPago { get; set; }
        public string Estado { get; set; }
        public string ReferenciaGen { get; set; }
        public string IdUsuarioAprobacion { get; set; }
        public string MotivoAprobacion { get; set; }
        public Nullable<System.DateTime> FechaAprobacion { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public string FechaUltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public string MotivoAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public Nullable<int> SecuenciaProveedor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cp_orden_pago_det> cp_orden_pago_det { get; set; }
        public virtual cp_orden_pago_formapago cp_orden_pago_formapago { get; set; }
        public virtual cp_orden_pago_tipo cp_orden_pago_tipo { get; set; }
    }
}
