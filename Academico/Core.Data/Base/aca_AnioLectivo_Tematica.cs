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
    
    public partial class aca_AnioLectivo_Tematica
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdTematica { get; set; }
        public int IdCampoAccion { get; set; }
        public string NombreCampoAccion { get; set; }
        public string NombreTematica { get; set; }
        public Nullable<int> OrdenCampoAccion { get; set; }
        public Nullable<int> OrdenTematica { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
    
        public virtual aca_AnioLectivo aca_AnioLectivo { get; set; }
        public virtual aca_CampoAccion aca_CampoAccion { get; set; }
        public virtual aca_Tematica aca_Tematica { get; set; }
    }
}