using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_007_Resumen_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public decimal Total { get; set; }
        public double dc_ValorPago { get; set; }
        public Nullable<decimal> Saldo { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdSede { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> IdCurso { get; set; }
        public Nullable<int> IdParalelo { get; set; }
        public string NomSede { get; set; }
        public string NomJornada { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> Anio { get; set; }
        public string NomRubro { get; set; }
        public int Cantidad { get; set; }
    }
}
