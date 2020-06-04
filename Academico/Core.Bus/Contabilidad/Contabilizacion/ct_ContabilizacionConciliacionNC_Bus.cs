using Core.Data.Contabilidad.Contabilizacion;
using Core.Info.Contabilidad.Contabilizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Contabilidad.Contabilizacion
{
    public class ct_ContabilizacionConciliacionNC_Bus
    {
        ct_ContabilizacionConciliacionNC_Data odata = new ct_ContabilizacionConciliacionNC_Data();

        public List<ct_ContabilizacionConciliacionNC_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
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
