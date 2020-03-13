using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionCobros_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string IdCobro_tipo { get; set; }
        public Nullable<int> ct_IdEmpresa { get; set; }
        public Nullable<int> ct_IdTipoCbte { get; set; }
        public Nullable<decimal> ct_IdCbteCble { get; set; }
        public double TotalModulo { get; set; }
        public Nullable<double> TotalContabilidad { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public string cr_ObservacionPantalla { get; set; }

        public string IdString { get; set; }
    }
}
