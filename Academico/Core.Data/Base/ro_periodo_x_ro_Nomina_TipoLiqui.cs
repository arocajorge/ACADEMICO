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
    
    public partial class ro_periodo_x_ro_Nomina_TipoLiqui
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ro_periodo_x_ro_Nomina_TipoLiqui()
        {
            this.ro_nomina_x_horas_extras = new HashSet<ro_nomina_x_horas_extras>();
            this.ro_participacion_utilidad = new HashSet<ro_participacion_utilidad>();
            this.ro_rol = new HashSet<ro_rol>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdNomina_Tipo { get; set; }
        public int IdNomina_TipoLiqui { get; set; }
        public int IdPeriodo { get; set; }
        public string Cerrado { get; set; }
        public string Procesado { get; set; }
        public string Contabilizado { get; set; }
    
        public virtual ro_Nomina_Tipoliqui ro_Nomina_Tipoliqui { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ro_nomina_x_horas_extras> ro_nomina_x_horas_extras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ro_participacion_utilidad> ro_participacion_utilidad { get; set; }
        public virtual ro_periodo ro_periodo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ro_rol> ro_rol { get; set; }
    }
}
