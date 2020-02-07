using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_003_Bus
    {
        CXC_003_Data odata = new CXC_003_Data();
        public List<CXC_003_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, string IdUsuario)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_ini, fecha_fin, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
