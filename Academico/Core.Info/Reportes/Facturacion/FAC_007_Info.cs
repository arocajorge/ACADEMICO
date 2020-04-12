using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Facturacion
{
    public class FAC_007_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_NumFactura { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string vt_tipo_venta { get; set; }
        public string nom_TerminoPago { get; set; }
        public int Num_Coutas { get; set; }
        public decimal Total { get; set; }
        public string vt_Observacion { get; set; }
        public string NomAlumno { get; set; }
        public string CedulaAlumno { get; set; }
        public string CodigoAlumno { get; set; }
        public string NomEmpleado { get; set; }
        public string CedulaEmpleado { get; set; }
        public string em_nombre { get; set; }
        public string NomCliente { get; set; }
        public string pe_cedulaRuc { get; set; }
        public Nullable<int> IdEmpresa_rol { get; set; }
    }
}
