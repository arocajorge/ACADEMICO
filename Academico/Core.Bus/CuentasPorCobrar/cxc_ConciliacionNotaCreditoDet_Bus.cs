using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Bus
    {
        cxc_ConciliacionNotaCreditoDet_Data odata = new cxc_ConciliacionNotaCreditoDet_Data();

        public List<cxc_ConciliacionNotaCreditoDet_Info> GetList(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdConciliacion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<cxc_ConciliacionNotaCreditoDet_Info> GetListPorCruzar(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.GetListPorCruzar(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
