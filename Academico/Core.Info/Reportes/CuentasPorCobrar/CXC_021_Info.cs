using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_021_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public Nullable<decimal> IdPreMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public System.DateTime Fecha { get; set; }
        public string NomAlumno { get; set; }
        public string Referencia { get; set; }
        public Nullable<double> vt_Subtotal { get; set; }
        public Nullable<double> vt_total { get; set; }
        public Nullable<double> ValorProntoPago { get; set; }
        public Nullable<System.DateTime> FechaProntoPago { get; set; }
        public Nullable<double> Saldo { get; set; }
        public Nullable<double> SaldoProntoPago { get; set; }
        public string vt_NunDocumento { get; set; }
        public Nullable<decimal> IdComprobante { get; set; }
    }
}
