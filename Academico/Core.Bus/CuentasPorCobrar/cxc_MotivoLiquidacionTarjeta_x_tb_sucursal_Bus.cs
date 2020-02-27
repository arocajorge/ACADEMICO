using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Bus
    {
        cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Data odata = new cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Data();
        public List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> GetList(int IdEmpresa, decimal IdMotivo)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdMotivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
