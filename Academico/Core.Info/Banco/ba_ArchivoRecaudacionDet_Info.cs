using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Banco
{
    public class ba_ArchivoRecaudacionDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdArchivo { get; set; }
        public int Secuencia { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public double Valor { get; set; }
        public Nullable<System.DateTime> FechaProceso { get; set; }
    }
}
