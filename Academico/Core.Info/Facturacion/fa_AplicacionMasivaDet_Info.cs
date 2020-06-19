using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Facturacion
{
    public class fa_AplicacionMasivaDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAplicacion { get; set; }
        public int Secuencia { get; set; }
        public decimal IdAlumno { get; set; }
        public double Saldo { get; set; }

        #region Campos que no existen en la tabla
        public string Codigo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public double SaldoReal { get; set; }
        #endregion
    }
}
