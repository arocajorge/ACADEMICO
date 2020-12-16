using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_066_Info
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
        public decimal IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdPersona { get; set; }
        public string Observacion { get; set; }
        public string Codigo { get; set; }
        public string NomPlantillaTipo { get; set; }
    }
}
