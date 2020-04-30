using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_007_Bus
    {
        CXC_007_Data odata = new CXC_007_Data();
        public List<CXC_007_Info> Get_list(int IdEmpresa, DateTime fechaCorte)
        {
            try
            {
                return odata.get_list(IdEmpresa, fechaCorte);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
