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
    
    public partial class vwin_Ing_Egr_Inven_PorDespachar
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdMovi_inven_tipo { get; set; }
        public decimal IdNumMovi { get; set; }
        public Nullable<int> IdBodega { get; set; }
        public string signo { get; set; }
        public string CodMoviInven { get; set; }
        public string cm_observacion { get; set; }
        public System.DateTime cm_fecha { get; set; }
        public Nullable<int> IdMotivo_Inv { get; set; }
        public Nullable<decimal> IdResponsable { get; set; }
        public string IdEstadoAproba { get; set; }
        public string IdUsuarioAR { get; set; }
        public Nullable<System.DateTime> FechaAR { get; set; }
        public string IdUsuarioDespacho { get; set; }
        public Nullable<System.DateTime> FechaDespacho { get; set; }
        public string bo_Descripcion { get; set; }
        public string Desc_mov_inv { get; set; }
        public string tm_descripcion { get; set; }
        public string Estado { get; set; }
        public string EstadoAprobacion { get; set; }
    }
}
