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
    
    public partial class VWCAJ_001
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbte { get; set; }
        public decimal IdCbteCble { get; set; }
        public int secuencia { get; set; }
        public string pc_Cuenta { get; set; }
        public double dc_Valor { get; set; }
        public double dc_Valor_Debe { get; set; }
        public Nullable<double> dc_Valor_Haber { get; set; }
        public string tc_descripcion { get; set; }
        public double cr_Valor { get; set; }
        public string cm_Signo { get; set; }
        public int IdTipoMovi { get; set; }
        public string tm_descripcion { get; set; }
        public string cm_observacion { get; set; }
        public int IdCaja { get; set; }
        public string ca_Descripcion { get; set; }
        public System.DateTime cm_fecha { get; set; }
        public string Estado { get; set; }
        public string tc_TipoCbte { get; set; }
        public string IdCtaCble { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Su_Descripcion { get; set; }
        public decimal SecuenciaCaja { get; set; }
    }
}
