using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_011_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string LugarNacimiento { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public decimal IdPersonaR { get; set; }
        public string Parentezco { get; set; }
        public string Representante { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public int IdMateria { get; set; }
        public string NomMateria { get; set; }
        public int OrdenMateria { get; set; }
        public Nullable<decimal> IdProfesor { get; set; }
        public Nullable<decimal> CalificacionP1 { get; set; }
        public Nullable<decimal> CalificacionP2 { get; set; }
        public Nullable<decimal> CalificacionP3 { get; set; }
        public Nullable<decimal> PromedioQ1 { get; set; }
        public Nullable<decimal> ExamenQ1 { get; set; }
        public Nullable<decimal> PromedioFinalQ1 { get; set; }
        public Nullable<decimal> CalificacionP4 { get; set; }
        public Nullable<decimal> CalificacionP5 { get; set; }
        public Nullable<decimal> CalificacionP6 { get; set; }
        public Nullable<decimal> PromedioQ2 { get; set; }
        public Nullable<decimal> ExamenQ2 { get; set; }
        public Nullable<decimal> PromedioFinalQ2 { get; set; }
        public Nullable<decimal> ExamenMejoramiento { get; set; }
        public Nullable<decimal> ExamenSupletorio { get; set; }
        public Nullable<decimal> ExamenRemedial { get; set; }
        public Nullable<decimal> ExamenGracia { get; set; }
        public Nullable<decimal> PromedioFinal { get; set; }
    }
}
