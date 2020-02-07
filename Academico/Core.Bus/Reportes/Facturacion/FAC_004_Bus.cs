using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
   public class FAC_004_Bus
    {
        FAC_004_Data odata = new FAC_004_Data();
        public List<FAC_004_Info> GetList(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
