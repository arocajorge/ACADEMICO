using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_003_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public string NomRepresentante { get; set; }
        public string CedulaRepresentante { get; set; }
        public string NomSeFactura { get; set; }
        public string CedulaSeFactura { get; set; }
        public string NomAlumno { get; set; }
        public string CedulaAlumno { get; set; }
        public string NomCurso { get; set; }
        public string DescripcionActual { get; set; }
        public string DescripcionAnterior { get; set; }
        public string Direccion { get; set; }
        public string DescripcionPensiones { get; set; }
        public string CorreoSeFactura { get; set; }
        public string CorreoRepresentante { get; set; }
        public string DireccionRepresentante { get; set; }
        public string NacionalidadRepresentante { get; set; }
        public string SectorRepresentante { get; set; }
        public string CelularRepresentante { get; set; }
    }
}
