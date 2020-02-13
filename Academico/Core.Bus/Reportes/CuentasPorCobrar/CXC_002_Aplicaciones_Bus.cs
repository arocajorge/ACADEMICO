using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_002_Aplicaciones_Bus
    {
        CXC_002_Aplicaciones_Data odata = new CXC_002_Aplicaciones_Data();
    
        public List<CXC_002_Aplicaciones_Info> get_list(int cbr_IdEmpresa, int cbr_IdSucursal, decimal cbr_IdCobro)
        {
            try
            {
                return odata.get_list(cbr_IdEmpresa, cbr_IdSucursal, cbr_IdCobro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
