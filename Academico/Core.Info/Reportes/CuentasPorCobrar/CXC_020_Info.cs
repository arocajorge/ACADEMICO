using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_020_Info
    {
        public double SaldoInicial { get; set; }
        public double Facturas { get; set; }
        public double NotasDeDebito { get; set; }
        public double SumanDebe { get; set; }
        public double NotasCredito { get; set; }
        public double PagoAnticipado { get; set; }
        public double NetoNotaCredito { get; set; }
        public double Cobros { get; set; }
        public double SaldoNeto { get; set; }
        public double SaldoAcreedorFinal { get; set; }
        public double SaldoFinal { get; set; }
        public double SaldoCalculado { get; set; }
        public double Diferencia { get; set; }
    }
}
