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
    
    public partial class ro_EmpleadoFoto
    {
        public int IdEmpresa { get; set; }
        public decimal IdEmpleado { get; set; }
        public byte[] Foto { get; set; }
    
        public virtual ro_empleado ro_empleado { get; set; }
    }
}
