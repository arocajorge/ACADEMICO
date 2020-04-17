using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_012_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdMatricula { get; set; }
        public int IdPeriodo { get; set; }
        public int IdRubro { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdBodega { get; set; }
        public Nullable<decimal> IdCbteVta { get; set; }
        public decimal IdAlumno { get; set; }
        public int IdAnio { get; set; }
        public int IdSede { get; set; }
        public int IdNivel { get; set; }
        public int IdJornada { get; set; }
        public int IdCurso { get; set; }
        public int IdParalelo { get; set; }
        public string vt_NumFactura { get; set; }
        public Nullable<System.DateTime> vt_fecha { get; set; }
        public string vt_Observacion { get; set; }
        public Nullable<System.DateTime> cr_fecha { get; set; }
        public Nullable<double> dc_ValorPago { get; set; }
        public string NomSede { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomNivel { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> OrdenCurso { get; set; }
        public string NomCurso { get; set; }
        public Nullable<int> OrdenParalelo { get; set; }
        public string NomParalelo { get; set; }
        public string CodigoAlumno { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public Nullable<System.DateTime> pe_fechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string NomRubro { get; set; }
        public string DescripcionAnio { get; set; }
        public string EstadoPago { get; set; }
    }
}
