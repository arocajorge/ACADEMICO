using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_CobroMasivoDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdCobroMasivo { get; set; }
        public int Secuencia { get; set; }
        public decimal IdAlumno { get; set; }
        public double Valor { get; set; }
        public System.DateTime Fecha { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<decimal> IdCobro { get; set; }

        #region Campos que no existen en la tabla
        public string CodigoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public bool ExisteAlumno { get; set; }
        public bool Repetido { get; set; }
        public bool ValorIgual { get; set; }
        public bool Error { get; set; }
        public string ErrorDetalle { get; set; }
        public decimal IdCliente { get; set; }
        #endregion
    }
}
