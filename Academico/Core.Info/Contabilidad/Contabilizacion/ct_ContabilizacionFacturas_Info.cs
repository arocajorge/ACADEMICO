using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionFacturas_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_NumFactura { get; set; }
        public string Alumno { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string vt_Observacion { get; set; }
        public string IdCtaCbleDebe { get; set; }
        public string IdCtaCbleHaber { get; set; }
        public Nullable<int> ct_IdEmpresa { get; set; }
        public Nullable<int> ct_IdTipoCbte { get; set; }
        public Nullable<decimal> ct_IdCbteCble { get; set; }
        public string IdCtaCble { get; set; }
        public decimal TotalModulo { get; set; }
        public double TotalContable { get; set; }
        public Nullable<decimal> Diferencia { get; set; }
    }
}
