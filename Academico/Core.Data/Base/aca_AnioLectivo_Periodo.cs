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
    
    public partial class aca_AnioLectivo_Periodo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public aca_AnioLectivo_Periodo()
        {
            this.aca_AnioLectivo_Rubro_Periodo = new HashSet<aca_AnioLectivo_Rubro_Periodo>();
        }
    
        public int IdEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int IdAnio { get; set; }
        public int IdMes { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public System.DateTime FechaHasta { get; set; }
        public Nullable<System.DateTime> FechaProntoPago { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdPuntoVta { get; set; }
        public Nullable<bool> Procesado { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }
        public Nullable<decimal> TotalAlumnos { get; set; }
        public Nullable<decimal> TotalValorFacturado { get; set; }
    
        public virtual aca_AnioLectivo aca_AnioLectivo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<aca_AnioLectivo_Rubro_Periodo> aca_AnioLectivo_Rubro_Periodo { get; set; }
    }
}
