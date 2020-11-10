using Core.Data.Reportes.Contabilidad;
using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_016_Bus
    {
        CXC_016_Data odata = new CXC_016_Data();
        public List<CXC_016_Info> get_list(int IdEmpresa, int IdPagare)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdPagare);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
