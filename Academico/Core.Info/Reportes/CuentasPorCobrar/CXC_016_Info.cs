using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_016_Info
    {
        public int IdEmpresa { get; set; }
        public int IdPagare { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdPersonaPagare { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
        public System.DateTime FechaAPagar { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_direccion { get; set; }
        public string pe_correo { get; set; }
        public string pe_celular { get; set; }

        #region Campos que no existen en la tabla

        public string dia { get; set; }
        public string mes { get; set; }
        public string anio { get; set; }
        public string ValorTexto { get; set; }
        public string FechaActual { get; set; }
        public string FechaPagarString { get; set; }
        public string ValorString { get; set; }
        #endregion
    }
}
