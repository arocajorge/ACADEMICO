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
    
    public partial class vwcxc_cobro
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public decimal IdCliente { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string IdCobro_tipo { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public double cr_TotalCobro { get; set; }
        public string cr_estado { get; set; }
        public string Su_Descripcion { get; set; }
        public string cr_observacion { get; set; }
        public string cr_NumDocumento { get; set; }
        public string nom_Motivo_tipo_cobro { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
    }
}
