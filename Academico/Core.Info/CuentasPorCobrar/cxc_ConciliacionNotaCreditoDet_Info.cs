using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        public int Secuencia { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVtaNota { get; set; }
        public string vt_TipoDoc { get; set; }
        public double Valor { get; set; }
        public double ValorProntoPago { get; set; }
        public Nullable<int> secuencia_nt { get; set; }
        #region Campos que no existen en la tabla
        public string ReferenciaDet { get; set; }
        public double? Saldo { get; set; }
        public string secuencia { get; set; }
        public DateTime vt_fecha { get; set; }
        public double? vt_total { get; set; }
        public double? vt_Subtotal { get; set; }
        public double? vt_iva { get; set; }
        public DateTime vt_fech_venc { get; set; }
        public string NomCliente { get; set; }
        public DateTime? FechaProntoPago { get; set; }
        public int? IdAnio { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdPuntoVta { get; set; }
        public decimal IdCliente { get; set; }
        public decimal? IdAlumno { get; set; }
        public double Saldo_final { get; set; }
        public string Observacion { get; set; }
        public double SaldoProntoPago { get; set; }
        #endregion

    }
}
