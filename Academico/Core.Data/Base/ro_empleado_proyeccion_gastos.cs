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
    
    public partial class ro_empleado_proyeccion_gastos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ro_empleado_proyeccion_gastos()
        {
            this.ro_empleado_proyeccion_gastos_det = new HashSet<ro_empleado_proyeccion_gastos_det>();
        }
    
        public int IdEmpresa { get; set; }
        public decimal IdTransaccion { get; set; }
        public decimal IdEmpleado { get; set; }
        public int AnioFiscal { get; set; }
        public string Observacion { get; set; }
        public bool estado { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transac { get; set; }
        public string IdUsuarioUltMod { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotiAnula { get; set; }
    
        public virtual ro_empleado ro_empleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ro_empleado_proyeccion_gastos_det> ro_empleado_proyeccion_gastos_det { get; set; }
    }
}
