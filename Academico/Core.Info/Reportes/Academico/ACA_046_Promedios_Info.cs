using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_046_Promedios_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdAnio { get; set; }
        public string Descripcion { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public Nullable<decimal> Promedio { get; set; }
        public Nullable<int> SecuenciaConducta { get; set; }
        public string Letra { get; set; }
    }
}
