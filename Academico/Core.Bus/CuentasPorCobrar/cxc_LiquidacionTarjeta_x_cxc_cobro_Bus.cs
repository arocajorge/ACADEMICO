using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_x_cxc_cobro_Bus
    {
        cxc_LiquidacionTarjeta_x_cxc_cobro_Data odata = new cxc_LiquidacionTarjeta_x_cxc_cobro_Data();
        public List<cxc_LiquidacionTarjeta_x_cxc_cobro_Info> GetList(int IdEmpresa, int IdSucursal, decimal? IdLiquidacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdLiquidacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
