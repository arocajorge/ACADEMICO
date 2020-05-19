using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Banco
{
    public class BAN_004_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public string ba_Num_Cuenta { get; set; }
        public string ba_descripcion { get; set; }
        public string NombreProceso { get; set; }
        public decimal IdAlumno { get; set; }
        public string Codigo { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public double Valor { get; set; }
        public double ValorProntoPago { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
    }
}
