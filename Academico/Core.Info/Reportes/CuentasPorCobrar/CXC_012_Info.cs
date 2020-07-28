using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_012_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSeguimiento { get; set; }
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public string CorreoEnviado { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
    }
}
