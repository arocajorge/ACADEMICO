using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_001_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public decimal IdAlumno { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomAnio { get; set; }
        public string CodigoAlumno { get; set; }
        public string pe_nombre { get; set; }
        public string pe_apellido { get; set; }
        public string pe_sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string Direccion { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string LugarNacimiento { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public System.DateTime Fecha { get; set; }
        public string NomPlantilla { get; set; }
        public string AntiguaInstitucion { get; set; }
        public Nullable<decimal> Conducta { get; set; }
        public Nullable<decimal> Promedio { get; set; }
        public string DocumentosCompletos { get; set; }
        public string NomVivienda { get; set; }
        public string NomTipoVivienda { get; set; }
        public string NomAgua { get; set; }
        public string TieneElectricidad { get; set; }
        public string NomGrupoEtnico { get; set; }
        public string TieneHermanos { get; set; }
        public string TieneConadis { get; set; }
        public string NomConadis { get; set; }
        public string NomViveCon { get; set; }
        public Nullable<int> CantidadHermanos { get; set; }
        public Nullable<double> TotalGastos { get; set; }
        public string Observacion { get; set; }
        public string Titulo { get; set; }
        public string NomFamiliar { get; set; }
        public string NomEstadoCivil { get; set; }
        public string DireccionFamiliar { get; set; }
        public string NomInstruccion { get; set; }
        public string EmpresaTrabajo { get; set; }
        public string CorreoFamiliar { get; set; }
        public string VehiculoPropio { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public Nullable<int> AnioVehiculo { get; set; }
        public string CedulaRucFamiliar { get; set; }
        public string NomProfesion { get; set; }
        public string CelularFamiliar { get; set; }
        public Nullable<double> IngresoMensual { get; set; }
        public Nullable<double> OtrosIngresos { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public string NivelProcedencia { get; set; }
        public Bitmap ImageUrl { get; set; }
    }
}
