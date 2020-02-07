using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System.Collections.Generic;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_001_Bus
    {
        FAC_001_Data odata = new FAC_001_Data();

        public List<FAC_001_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, bool mostrar_cuotas)
        {
            try
            {
                return odata.get_list( IdEmpresa, IdSucursal, IdBodega, IdCbteVta, mostrar_cuotas);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
