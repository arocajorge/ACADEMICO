using System;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_004_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public string IdUsuario { get; set; }
        public string NomAnio { get; set; }
        public string CodigoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public string NombreJornada { get; set; }
        public decimal SaldoDeudor { get; set; }
        public decimal SaldoAcreedor { get; set; }
        public decimal SaldoFinal { get; set; }


        public string IdCtaCbleDebe { get; set; }
        public string pc_Cuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }

    }
}
