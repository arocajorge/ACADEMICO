using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes
{
    public class ACA_001_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public System.DateTime FechaMatricula { get; set; }
        public string CodMatricula { get; set; }
        public string CodAlumno { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdPersona { get; set; }
        public string pe_apellido { get; set; }
        public string pe_nombre { get; set; }
        public string Anio { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string Sexo { get; set; }
        public string LugarNacimiento { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Celular { get; set; }
        public bool TieneElectricidad { get; set; }
        public bool TieneHermanos { get; set; }
        public string NombreHermanos { get; set; }
        public string TipoVivienda { get; set; }
        public string TenenciaVivienda { get; set; }
        public string CodCatalogoCONADIS { get; set; }
        public Nullable<double> PorcentajeDiscapacidad { get; set; }
        public string NumeroCarnetConadis { get; set; }
    }
}
