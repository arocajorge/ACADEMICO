using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_010_Bus
    {
        CXC_010_Data odata = new CXC_010_Data();
        public List<CXC_010_Info> get_list(int IdEmpresa, decimal IdAlumno, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAlumno, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
