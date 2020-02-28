using System;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_001_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_tipoDoc { get; set; }
        public string vt_NumFactura { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string NomCliente { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public System.DateTime vt_fech_venc { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> IVA { get; set; }
        public Nullable<decimal> Total { get; set; }
        public double Cobrado { get; set; }
        public double NotaCredito { get; set; }
        public Nullable<double> Saldo { get; set; }
        public string Su_Descripcion { get; set; }
    }
}
