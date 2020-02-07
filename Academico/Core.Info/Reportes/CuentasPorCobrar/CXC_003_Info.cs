using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_003_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdCobro { get; set; }
        public int secuencial { get; set; }
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public Nullable<int> IdBodega_Cbte { get; set; }
        public decimal IdCbte_vta_nota { get; set; }
        public string dc_TipoDocumento { get; set; }
        public double dc_ValorPago { get; set; }
        public double dc_ValorPagoNC { get; set; }
        public string CodigAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NumFactura { get; set; }
        public string IdCobro_tipo { get; set; }
        public System.DateTime cr_fecha { get; set; }
        public Nullable<int> IdTarjeta { get; set; }
        public string NombreTarjeta { get; set; }
        public string cr_Banco { get; set; }
        public Nullable<int> AnioFactura { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string GrupoTipoCobro { get; set; }
        public string NombreTipoCobro { get; set; }
        public double TotalPago { get; set; }
    }
}
