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
    
    public partial class vwcxc_ConciliacionNotaCredito
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public decimal IdCobro { get; set; }
        public System.DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string Referencia { get; set; }
        public string pe_nombreCompleto { get; set; }
    }
}
