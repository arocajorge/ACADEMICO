using Core.Data.Reportes.Caja;
using Core.Info.Reportes.Caja;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.Caja
{
    public class CAJ_002_ValesNoConciliados_Bus
    {
        CAJ_002_ValesNoConciliados_Data odata = new CAJ_002_ValesNoConciliados_Data();
        public List<CAJ_002_ValesNoConciliados_Info> GetList(int IdEmpresa, decimal IdConciliacion_Caja)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdConciliacion_Caja);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
