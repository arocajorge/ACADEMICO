using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_003_aplicaciones_Bus
    {
        FAC_003_aplicaciones_Data odata = new FAC_003_aplicaciones_Data();
    
        public List<FAC_003_aplicaciones_Info> get_list(int IdEmpresa_nt, int IdSucursal_nt, int IdBodega_nt, decimal IdNota_nt)
        {
            try
            {
                return odata.get_list(IdEmpresa_nt, IdSucursal_nt, IdBodega_nt, IdNota_nt);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
