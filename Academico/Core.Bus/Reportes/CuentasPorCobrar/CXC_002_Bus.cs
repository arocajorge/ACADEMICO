using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_002_Bus
    {
        CXC_002_Data odata = new CXC_002_Data();
        public List<CXC_002_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCobro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
