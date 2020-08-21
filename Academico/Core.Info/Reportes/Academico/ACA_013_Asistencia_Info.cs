using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_013_Asistencia_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public Nullable<int> FaltasInjustificadas { get; set; }
        public Nullable<int> FaltasJustificadas { get; set; }
        public Nullable<int> Atrasos { get; set; }

    }
}
