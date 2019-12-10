using Core.Data.Facturacion;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Bus.Facturacion
{
    public class fa_factura_det_Bus
    {
        fa_factura_det_Data odata = new fa_factura_det_Data();
        public List<fa_factura_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
              return   odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<fa_factura_det_Info> get_list_rubros_x_facturar(int IdEmpresa, int IdSucursal, int IdAnio, decimal IdAlumno)
        {
            try
            {
                return odata.get_list_rubros_x_facturar(IdEmpresa, IdSucursal, IdAnio, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
