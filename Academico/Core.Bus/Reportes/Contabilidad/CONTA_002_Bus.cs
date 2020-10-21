using Core.Data.Reportes.Contabilidad;
using Core.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Contabilidad
{
    public class CONTA_002_Bus
    {
        CONTA_002_Data odata = new CONTA_002_Data();
        public List<CONTA_002_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin, string Tipo)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaIni, FechaFin, Tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
