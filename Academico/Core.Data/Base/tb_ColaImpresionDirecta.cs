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
    
    public partial class tb_ColaImpresionDirecta
    {
        public int IdEmpresa { get; set; }
        public decimal IdImpresion { get; set; }
        public string CodReporte { get; set; }
        public string IPUsuario { get; set; }
        public string IPImpresora { get; set; }
        public string Parametros { get; set; }
        public string Usuario { get; set; }
        public string NombreEmpresa { get; set; }
        public System.DateTime FechaEnvio { get; set; }
        public Nullable<System.DateTime> FechaImpresion { get; set; }
        public string Comentario { get; set; }
        public Nullable<int> NumCopias { get; set; }
    }
}
