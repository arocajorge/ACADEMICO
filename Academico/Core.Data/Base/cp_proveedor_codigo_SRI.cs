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
    
    public partial class cp_proveedor_codigo_SRI
    {
        public int IdEmpresa { get; set; }
        public decimal IdProveedor { get; set; }
        public int IdCodigo_SRI { get; set; }
        public string observacion { get; set; }
    
        public virtual cp_codigo_SRI cp_codigo_SRI { get; set; }
        public virtual cp_proveedor cp_proveedor { get; set; }
    }
}
