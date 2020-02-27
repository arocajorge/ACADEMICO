using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Data
    {
        public List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> GetList(int IdEmpresa, decimal IdMotivo)
        {
            try
            {
                List<cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info> Lista;
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = Context.vwcxc_MotivoLiquidacionTarjeta_x_tb_sucursal.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdMotivo == IdMotivo).Select(q => new cxc_MotivoLiquidacionTarjeta_x_tb_sucursal_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMotivo = q.IdMotivo,
                        IdCtaCble = q.IdCtaCble,
                        IdSucursal = q.IdSucursal,
                        Secuencia = q.Secuencia,
                        pc_Cuenta = q.pc_Cuenta,
                        Su_Descripcion = q.Su_Descripcion
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
