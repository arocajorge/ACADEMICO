using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_002_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string cr_estado { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public string tc_descripcion { get; set; }
        public string CodigoAlumno { get; set; }
        public string cr_observacion { get; set; }
        public string NomSede { get; set; }
        public string NomNivel { get; set; }
        public string NomJornada { get; set; }
        public string NomCurso { get; set; }
        public string NomParalelo { get; set; }
        public string CodigoParalelo { get; set; }
        public string NomPlantilla { get; set; }
        public double cr_TotalCobro { get; set; }
        public Nullable<double> cr_Saldo { get; set; }
        public string NomCliente { get; set; }
        public string CedulaCliente { get; set; }
        public decimal IdCliente { get; set; }
        public string cr_Banco { get; set; }
        public string cr_NumDocumento { get; set; }
    }
}
