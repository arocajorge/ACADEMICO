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
    
    public partial class caj_parametro
    {
        public int IdEmpresa { get; set; }
        public int IdTipoCbteCble_MoviCaja_Ing { get; set; }
        public int IdTipoCbteCble_MoviCaja_Egr { get; set; }
        public Nullable<int> IdTipo_movi_ing_x_reposicion { get; set; }
        public int DiasTransaccionesAFuturo { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> FechaUltMod { get; set; }
        public Nullable<int> DiasTransaccionesAPasado { get; set; }
    }
}
