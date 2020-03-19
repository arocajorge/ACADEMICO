using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_006_Bus
    {
        CXC_006_Data odata = new CXC_006_Data();
        public List<CXC_006_Info> GetList(int IdEmpresa, int[] ListaSucursal, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, ListaSucursal, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}
