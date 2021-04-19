using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_008_Bus
    {
        FAC_008_Data odata = new FAC_008_Data();
        public List<FAC_008_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                return odata.get_list(IdEmpresa,IdSucursal, IdBodega, IdNota);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
