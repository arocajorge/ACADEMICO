using Core.Data.Reportes.Caja;
using Core.Info.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Caja
{
   public class CAJ_002_Bus
    {
        CAJ_002_Data odata = new CAJ_002_Data();
        public List<CAJ_002_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaFin, FechaIni);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
