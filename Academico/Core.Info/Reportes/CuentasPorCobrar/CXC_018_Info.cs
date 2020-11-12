using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_018_Info
    {
        public int Num { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPagare { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public string Codigo { get; set; }
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
        public bool Estado { get; set; }
        public string PersonaPagare { get; set; }
        public string Alumno { get; set; }
    }
}
