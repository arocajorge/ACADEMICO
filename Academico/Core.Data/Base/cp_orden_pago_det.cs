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
    
    public partial class cp_orden_pago_det
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cp_orden_pago_det()
        {
            this.cp_orden_pago_cancelaciones = new HashSet<cp_orden_pago_cancelaciones>();
            this.cp_orden_pago_cancelaciones1 = new HashSet<cp_orden_pago_cancelaciones>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdOrdenPago { get; set; }
        public int Secuencia { get; set; }
        public Nullable<int> IdEmpresa_cxp { get; set; }
        public Nullable<decimal> IdCbteCble_cxp { get; set; }
        public Nullable<int> IdTipoCbte_cxp { get; set; }
        public double Valor_a_pagar { get; set; }
        public string Referencia { get; set; }
        public string IdFormaPago { get; set; }
        public System.DateTime Fecha_Pago { get; set; }
        public string IdEstadoAprobacion { get; set; }
        public Nullable<int> IdBanco { get; set; }
        public string IdUsuario_Aprobacion { get; set; }
        public Nullable<System.DateTime> fecha_hora_Aproba { get; set; }
        public string Motivo_aproba { get; set; }
    
        public virtual cp_orden_pago cp_orden_pago { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cp_orden_pago_cancelaciones> cp_orden_pago_cancelaciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cp_orden_pago_cancelaciones> cp_orden_pago_cancelaciones1 { get; set; }
        public virtual cp_orden_pago_formapago cp_orden_pago_formapago { get; set; }
    }
}
