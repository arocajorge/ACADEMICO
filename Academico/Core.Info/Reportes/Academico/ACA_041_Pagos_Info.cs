using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_041_Pagos_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdMatricula { get; set; }
        public string Descripcion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public double TotalCobrado { get; set; }
        public double TotalFacturado { get; set; }
    }
}
