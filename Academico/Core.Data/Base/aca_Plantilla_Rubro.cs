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
    
    public partial class aca_Plantilla_Rubro
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdPlantilla { get; set; }
        public int IdRubro { get; set; }
        public decimal IdProducto { get; set; }
        public decimal Subtotal { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
    
        public virtual aca_Plantilla aca_Plantilla { get; set; }
    }
}
