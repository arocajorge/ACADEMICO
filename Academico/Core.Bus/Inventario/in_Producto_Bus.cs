using Core.Data.Inventario;
using Core.Info.Helps;
using Core.Info.Inventario;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Inventario
{
    public class in_Producto_Bus
    {
        in_Producto_Data odata = new in_Producto_Data();
        public List<in_Producto_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, cl_enumeradores.eTipoBusquedaProducto Busqueda, cl_enumeradores.eModulo Modulo, int IdSucursal, int IdBodega = 0)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa, Busqueda, Modulo, IdSucursal, IdBodega);
        }

        public in_Producto_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            return odata.get_info_bajo_demanda(args, IdEmpresa);
        }

        public in_Producto_Info get_info(int IdEmpresa, decimal IdProducto)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdProducto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarCodigoExists(int IdEmpresa, string Codigo)
        {
            try
            {
                return odata.ValidarCodigoExists(IdEmpresa, Codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
