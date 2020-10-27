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
    public class CXC_017_Bus
    {
        CXC_017_Data odata = new CXC_017_Data();
        public List<CXC_017_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
