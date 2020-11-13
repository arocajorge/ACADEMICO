using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_019_Info
    {
        public int Num { get; set; }
        public int IdEmpresa { get; set; }
        public int IdConvenio { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public string Codigo { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdPersonaConvenio { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
        public System.DateTime Fecha { get; set; }
        public System.DateTime FechaPrimerPago { get; set; }
        public bool Estado { get; set; }
        public string PersonaConvenio { get; set; }
        public string Alumno { get; set; }
        public string IdUsuario { get; set; }
        public int NumCuotas { get; set; }
        public string Descripcion { get; set; }
    }
}
