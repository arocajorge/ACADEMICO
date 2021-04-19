using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_cobro_det_Bus
    {
        cxc_cobro_det_Data odata = new cxc_cobro_det_Data();
        public List<cxc_cobro_det_Info> get_list_cartera(int IdEmpresa, int IdSucursal, decimal IdCliente, bool FiltrarPorCliente)
        {
            try
            {
                return odata.get_list_cartera(IdEmpresa, IdSucursal, IdCliente, FiltrarPorCliente);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list_cartera_x_alumno(int IdEmpresa, int IdSucursal, decimal IdAlumno)
        {
            try
            {
                return odata.get_list_cartera_x_alumno(IdEmpresa, IdSucursal, IdAlumno);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public List<cxc_cobro_det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdCobro);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string dc_TipoDocumento)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, dc_TipoDocumento);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list_AP(int IdEmpresa)
        {
            try
            {
                return odata.get_list_AP(IdEmpresa);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list_AP(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.get_list_AP(IdEmpresa, IdAlumno);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<cxc_cobro_det_Info> GetListCobrosSinAplicarNC(int IdEmpresa, DateTime FechIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetListCobrosSinAplicarNC(IdEmpresa, FechIni, FechaFin);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
