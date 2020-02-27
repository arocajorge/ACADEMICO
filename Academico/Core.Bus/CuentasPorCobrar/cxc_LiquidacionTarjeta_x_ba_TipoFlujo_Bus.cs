using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_LiquidacionTarjeta_x_ba_TipoFlujo_Bus
    {
        cxc_LiquidacionTarjeta_x_ba_TipoFlujo_Data odata = new cxc_LiquidacionTarjeta_x_ba_TipoFlujo_Data();

        public List<cxc_LiquidacionTarjeta_x_ba_TipoFlujo_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
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
