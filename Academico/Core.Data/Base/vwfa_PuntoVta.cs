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
    
    public partial class vwfa_PuntoVta
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public string Su_Descripcion { get; set; }
        public int IdPuntoVta { get; set; }
        public string cod_PuntoVta { get; set; }
        public string nom_PuntoVta { get; set; }
        public bool estado { get; set; }
        public int IdBodega { get; set; }
        public string Su_CodigoEstablecimiento { get; set; }
        public int IdCaja { get; set; }
        public string IPImpresora { get; set; }
        public Nullable<int> NumCopias { get; set; }
        public bool CobroAutomatico { get; set; }
        public bool EsElectronico { get; set; }
        public string codDocumentoTipo { get; set; }
        public string Descripcion { get; set; }
    }
}
