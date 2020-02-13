using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_002_Aplicaciones_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public int secuencial { get; set; }
        public string dc_TipoDocumento { get; set; }
        public string vt_NumFactura { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<double> dc_ValorProntoPago { get; set; }
        public double dc_ValorPago { get; set; }
    }
}
