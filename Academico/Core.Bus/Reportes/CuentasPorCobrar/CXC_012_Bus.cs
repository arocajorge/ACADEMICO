using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_012_Bus
    {
        CXC_012_Data odata = new CXC_012_Data();

        public List<CXC_012_Info> Get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_ini, fecha_fin);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
