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
    
    public partial class SPACA_ContabilizacionNotas_Result
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public System.DateTime no_fecha { get; set; }
        public string sc_observacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string No_Descripcion { get; set; }
        public Nullable<int> ct_IdEmpresa { get; set; }
        public Nullable<int> ct_IdTipoCbte { get; set; }
        public Nullable<decimal> ct_IdCbteCble { get; set; }
        public decimal TotalModulo { get; set; }
        public Nullable<double> TotalContabilidad { get; set; }
        public Nullable<double> Saldo { get; set; }
    }
}