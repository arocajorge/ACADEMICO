using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_002_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public string Descripcion { get; set; }
        public bool EnCurso { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string CodigoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string CedulaRep { get; set; }
        public string NombreRep { get; set; }
        public string NomPlantilla { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    }
}
