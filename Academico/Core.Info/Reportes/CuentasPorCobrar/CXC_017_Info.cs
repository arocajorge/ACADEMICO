using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_017_Info
    {
        public int IdEmpresa { get; set; }
        public int IdConvenio { get; set; }
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public decimal IdPersonaConvenio { get; set; }
        public double TotalCuota { get; set; }
        public string Observacion { get; set; }
        public string Alumno { get; set; }
        public string PersonaConvenio { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_direccion { get; set; }
        public string pe_correo { get; set; }
        public string pe_celular { get; set; }
        public string Codigo { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string Descripcion { get; set; }
        public int NumCuota { get; set; }
        public System.DateTime FechaPago { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenJornada { get; set; }
        public int OrdenCurso { get; set; }
        public int OrdenParalelo { get; set; }
        public double Valor { get; set; }
        public bool Estado { get; set; }

        #region Campos que no existen en la tabla
        public string ValorString { get; set; }
        public string ValorTexto { get; set; }
        public string FechaActual { get; set; }
        public string FechaConvenio { get; set; }
        public string Anio { get; set; }
        public string Acta { get; set; }
        #endregion
    }
}
