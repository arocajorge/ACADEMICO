using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_007_Resumen_Bus
    {
        CXC_007_Resumen_Data odata = new CXC_007_Resumen_Data();

        public List<CXC_007_Resumen_Info> GetList(int IdEmpresa, DateTime FechaCorte)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaCorte);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
