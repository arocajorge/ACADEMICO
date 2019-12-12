using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_Parametro_Bus
    {
        cxc_Parametro_Data odata = new cxc_Parametro_Data();
    
        public cxc_Parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                return odata.get_info(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cxc_Parametro_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
