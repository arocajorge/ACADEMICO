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
    
    public partial class cp_orden_giro_det
    {
        public int IdEmpresa { get; set; }
        public decimal IdCbteCble_Ogiro { get; set; }
        public int IdTipoCbte_Ogiro { get; set; }
        public int Secuencia { get; set; }
        public decimal IdProducto { get; set; }
        public string IdUnidadMedida { get; set; }
        public double Cantidad { get; set; }
        public double CostoUni { get; set; }
        public double PorDescuento { get; set; }
        public double DescuentoUni { get; set; }
        public double CostoUniFinal { get; set; }
        public double Subtotal { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public double PorIva { get; set; }
        public double ValorIva { get; set; }
        public double Total { get; set; }
        public string IdCtaCbleInv { get; set; }
        public Nullable<int> IdEmpresa_oc { get; set; }
        public Nullable<int> IdSucursal_oc { get; set; }
        public Nullable<decimal> IdOrdenCompra { get; set; }
        public Nullable<int> Secuencia_oc { get; set; }
    
        public virtual cp_orden_giro cp_orden_giro { get; set; }
    }
}
