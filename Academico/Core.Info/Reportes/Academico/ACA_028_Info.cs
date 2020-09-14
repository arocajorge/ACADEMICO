using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_028_Info
    {
        public int IdEmpresa { get; set; }
        public int IdAnio { get; set; }
        public string Descripcion { get; set; }
        public int IdSede { get; set; }
        public string NomSede { get; set; }
        public int IdNivel { get; set; }
        public string NomNivel { get; set; }
        public int IdJornada { get; set; }
        public string NomJornada { get; set; }
        public int IdCurso { get; set; }
        public string NomCurso { get; set; }
        public int IdParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomParalelo { get; set; }
        public int OrdenParalelo { get; set; }
        public Nullable<decimal> IdProfesorTutor { get; set; }
        public Nullable<decimal> IdProfesorInspector { get; set; }
        public Nullable<decimal> IdPersonaTutor { get; set; }
        public Nullable<decimal> IdPersonaInpector { get; set; }
        public string NomTutor { get; set; }
        public string NomInspector { get; set; }
        public int OrdenNivel { get; set; }
        public int OrdenJornada { get; set; }
        public int OrdenCurso { get; set; }
    }
}
