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
    
    public partial class tb_sis_Impuesto
    {
        public string IdCod_Impuesto { get; set; }
        public string nom_impuesto { get; set; }
        public bool Usado_en_Ventas { get; set; }
        public bool Usado_en_Compras { get; set; }
        public double porcentaje { get; set; }
        public Nullable<int> IdCodigo_SRI { get; set; }
        public bool estado { get; set; }
        public string IdTipoImpuesto { get; set; }
    
        public virtual tb_sis_Impuesto_Tipo tb_sis_Impuesto_Tipo { get; set; }
    }
}
