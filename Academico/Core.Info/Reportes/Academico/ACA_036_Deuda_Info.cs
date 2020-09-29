using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Academico
{
    public class ACA_036_Deuda_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public string vt_tipoDoc { get; set; }
        public string vt_NunDocumento { get; set; }
        public string Referencia { get; set; }
        public decimal IdComprobante { get; set; }
        public string CodComprobante { get; set; }
        public string Su_Descripcion { get; set; }
        public decimal IdCliente { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public Nullable<double> vt_total { get; set; }
        public Nullable<double> Saldo { get; set; }
        public double TotalxCobrado { get; set; }
        public string Bodega { get; set; }
        public Nullable<double> vt_Subtotal { get; set; }
        public Nullable<double> vt_iva { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public Nullable<double> dc_ValorRetFu { get; set; }
        public Nullable<double> dc_ValorRetIva { get; set; }
        public string CodCliente { get; set; }
        public string NomCliente { get; set; }
        public string em_nombre { get; set; }
        public string Estado { get; set; }
        public Nullable<double> ValorProntoPago { get; set; }
        public Nullable<System.DateTime> FechaProntoPago { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<int> IdPlantilla { get; set; }
        public Nullable<int> IdPuntoVta { get; set; }
        public Nullable<double> SaldoProntoPago { get; set; }
    }
}
