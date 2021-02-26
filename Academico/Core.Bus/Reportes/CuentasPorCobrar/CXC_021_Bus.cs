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
    public class CXC_021_Bus
    {
        CXC_021_Data odata = new CXC_021_Data();
        public List<CXC_021_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSede, IdAnio, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
