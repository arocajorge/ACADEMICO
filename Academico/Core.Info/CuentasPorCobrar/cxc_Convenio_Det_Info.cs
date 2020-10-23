using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_Convenio_Det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdConvenio { get; set; }
        public int NumCuota { get; set; }
        public double SaldoInicial { get; set; }
        public double TotalCuota { get; set; }
        public double Saldo { get; set; }
        public System.DateTime FechaPago { get; set; }
        public string Observacion_det { get; set; }
        public int IdCatalogoEstadoPago { get; set; }
    }
}
