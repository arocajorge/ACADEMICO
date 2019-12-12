using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_cobro_tipo_motivo_Bus
    {
        cxc_cobro_tipo_motivo_Data odata = new cxc_cobro_tipo_motivo_Data();
    
        public List<cxc_cobro_tipo_motivo_Info> get_list()
        {
            try
            {
                return odata.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
