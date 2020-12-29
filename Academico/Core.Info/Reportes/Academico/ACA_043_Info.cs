using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_043_Info
    {
        public int Num { get; set; }
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public decimal IdMatricula { get; set; }
        public decimal IdProfesor { get; set; }
        public decimal IdAlumno { get; set; }
        public string NombreRector { get; set; }
        public string TelefonoRector { get; set; }
        public string CelularRector { get; set; }
        public string CorreoRector { get; set; }
        public string NombreProfesor { get; set; }
        public string TelefonoProfesor { get; set; }
        public string CelularProfesor { get; set; }
        public string CorreoProfesor { get; set; }
        public string NombreCampoAccion { get; set; }
        public string NombreTematica { get; set; }

        public Nullable<decimal> PromedioFinalConsulta { get; set; }
        public Nullable<decimal> Promedio1 { get; set; }
        public Nullable<decimal> Promedio2 { get; set; }
        public Nullable<decimal> Promedio3 { get; set; }
        public Nullable<decimal> PromedioFinal { get; set; }
        public int NumCalificaciones { get; set; }
        public int NumHoras { get; set; }
    }
}
