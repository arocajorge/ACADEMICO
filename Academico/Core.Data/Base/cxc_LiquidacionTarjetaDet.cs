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
    
    public partial class cxc_LiquidacionTarjetaDet
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdMotivo { get; set; }
        public double Porcentaje { get; set; }
        public double Valor { get; set; }
    
        public virtual cxc_LiquidacionTarjeta cxc_LiquidacionTarjeta { get; set; }
        public virtual cxc_MotivoLiquidacionTarjeta cxc_MotivoLiquidacionTarjeta { get; set; }
    }
}
