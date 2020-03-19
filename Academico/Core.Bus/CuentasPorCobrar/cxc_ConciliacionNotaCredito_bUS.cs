using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCredito_Bus
    {
        cxc_ConciliacionNotaCredito_Data odata = new cxc_ConciliacionNotaCredito_Data();

        public List<cxc_ConciliacionNotaCredito_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_ConciliacionNotaCredito_Info GetInfo(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, IdConciliacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(cxc_ConciliacionNotaCredito_Info info)
        {
            try
            {
                return odata.AnularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidarEnConciliacionNC(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota, string Tipo)
        {
            try
            {
                return odata.ValidarEnConciliacionNC(IdEmpresa, IdSucursal, IdBodega, IdNota, Tipo);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
