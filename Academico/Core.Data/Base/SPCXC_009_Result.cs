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
    
    public partial class SPCXC_009_Result
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public int secuencial { get; set; }
        public Nullable<int> IdBodega_Cbte { get; set; }
        public Nullable<decimal> IdCbte_vta_nota { get; set; }
        public string dc_TipoDocumento { get; set; }
        public double dc_ValorPago { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public Nullable<System.DateTime> vt_fecha { get; set; }
        public string Periodo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Codigo { get; set; }
        public string IdCobro_tipo { get; set; }
        public string Tipo { get; set; }
        public int Orden { get; set; }
        public string OrdenRubros { get; set; }
        public string Observacion { get; set; }
    }
}
