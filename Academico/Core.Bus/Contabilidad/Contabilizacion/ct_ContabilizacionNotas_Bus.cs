using Core.Data.Contabilidad.Contabilizacion;
using Core.Info.Contabilidad.Contabilizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionNotas_Bus
    {
        ct_ContabilizacionNotas_Data odata = new ct_ContabilizacionNotas_Data();

        public List<ct_ContabilizacionNotas_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
