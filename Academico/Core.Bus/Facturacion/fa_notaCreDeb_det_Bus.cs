using Core.Data.Facturacion;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;

namespace Core.Bus.Facturacion
{
    public class fa_notaCreDeb_det_Bus
    {
        fa_notaCreDeb_det_Data odata = new fa_notaCreDeb_det_Data();
        public List<fa_notaCreDeb_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota, string CodDocumento)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota, CodDocumento);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
