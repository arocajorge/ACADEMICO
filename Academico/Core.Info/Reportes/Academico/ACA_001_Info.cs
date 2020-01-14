using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_001_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public System.DateTime FechaMatricula { get; set; }
        public string CodMatricula { get; set; }
        public string CodAlumno { get; set; }
        public decimal IdAlumno { get; set; }
        public decimal IdPersonaAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string Anio { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string Sexo { get; set; }
        public string LugarNacimiento { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string DireccionAlumno { get; set; }
        public string CelularAlumno { get; set; }
        public Nullable<bool> TieneElectricidad { get; set; }
        public Nullable<bool> TieneHermanos { get; set; }
        public string NombreHermanos { get; set; }
        public string TipoVivienda { get; set; }
        public string TenenciaVivienda { get; set; }
        public string CodCatalogoCONADIS { get; set; }
        public Nullable<double> PorcentajeDiscapacidad { get; set; }
        public string NumeroCarnetConadis { get; set; }
        public string NacionalidadAlumno { get; set; }
        public string TelefonoAlumno { get; set; }
        public string Agua { get; set; }
        public string TipoDiscapacidadAlumno { get; set; }
        public Nullable<double> SueldoPadre { get; set; }
        public Nullable<double> SueldoMadre { get; set; }
        public Nullable<double> OtroIngreso { get; set; }
        public Nullable<double> GastoAlimentacion { get; set; }
        public Nullable<double> GastoEducacion { get; set; }
        public Nullable<double> GastoServicioBasico { get; set; }
        public Nullable<double> GastoSalud { get; set; }
        public Nullable<double> GastoArriendo { get; set; }
        public Nullable<double> GastoPrestamo { get; set; }
        public Nullable<double> OtroGasto { get; set; }
        public string Descripcion { get; set; }
        public int IdAnio { get; set; }
        public string Observacion { get; set; }

        #region Familiares
        //public int IdEmpresa { get; set; }
        //public decimal IdAlumno { get; set; }
        public int Secuencia { get; set; }
        public int IdCatalogoPAREN { get; set; }
        public string NomInstruccion { get; set; }
        public string NomCatalogo { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string NomEstadoCivil { get; set; }
        public string Direccion { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string NomProfesion { get; set; }
        public string Correo { get; set; }
        public string Celular { get; set; }
        //public Nullable<double> SueldoPadre { get; set; }
        public Nullable<double> Sueldo { get; set; }
        public Nullable<double> OtrosIngresos { get; set; }
        public string VehiculoPropio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Nullable<int> AniosServicio { get; set; }
        public Nullable<int> AnioVehiculo { get; set; }
        public string Titulo { get; set; }
        #endregion
    }
}
