using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.CuentasPorCobrar
{
    public class CXC_006_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public decimal IdLiquidacion { get; set; }
        public string Lote { get; set; }
        public System.DateTime Fecha { get; set; }
        public int IdBanco { get; set; }
        public string ba_descripcion { get; set; }
        public double ValorCobro { get; set; }
        public double ValorComision { get; set; }
        public double ValorImpuesto { get; set; }
        public double DepositoNeto { get; set; }
        public string Observacion { get; set; }
        public string Su_Descripcion { get; set; }

        #region Campos que no existen en el SP
        public string Su_DescripcionFiltros { get; set; }
        #endregion
    }
}
