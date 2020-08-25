using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_020_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public string Codigo { get; set; }
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdPersona { get; set; }
        public string NombreAlumno { get; set; }
        public string CedulaAlumno { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string NomSede { get; set; }
        public string Descripcion { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomParalelo { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public decimal IdPersonaR { get; set; }
        public string CedulaLegal { get; set; }
        public string NombreLegal { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal IdPersonaF { get; set; }
        public string CedulaFactura { get; set; }
        public string NombreFactura { get; set; }
        public Nullable<bool> EsRetirado { get; set; }
        public string FechaActual { get; set; }
    }
}
