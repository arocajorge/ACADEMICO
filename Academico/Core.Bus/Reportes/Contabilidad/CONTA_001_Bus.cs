using Core.Data.Reportes.Contabilidad;
using Core.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.Contabilidad
{
    public class CONTA_001_Bus
    {
        CONTA_001_Data odata = new CONTA_001_Data();
        public List<CONTA_001_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
