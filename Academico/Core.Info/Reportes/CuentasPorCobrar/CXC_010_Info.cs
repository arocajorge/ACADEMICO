using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_010_Info
    {
        public int IdEmpresa { get; set; }
        public string Tipo { get; set; }
        public decimal IdCbteVta { get; set; }
        public int Orden { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string vt_Observacion { get; set; }
        public string Referencia { get; set; }
        public Nullable<int> Anio { get; set; }
        public Nullable<int> IdMes { get; set; }
        public string smes { get; set; }
        public Nullable<double> SaldoInicial { get; set; }
        public decimal Debe { get; set; }
        public double Haber { get; set; }
        public Nullable<double> Valor { get; set; }
        public Nullable<double> Saldo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string CodigoAlumno { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
    }
}
