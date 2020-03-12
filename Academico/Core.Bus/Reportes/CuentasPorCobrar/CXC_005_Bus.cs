using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_005_Bus
    {
        CXC_005_Data odata = new CXC_005_Data();
        public List<CXC_005_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
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
