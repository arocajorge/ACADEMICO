using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_004_Bus
    {
        CXC_004_Data odata = new CXC_004_Data();
        public List<CXC_004_Info> Getlist(int IdEmpresa, string IdUsuario, DateTime FechaCorte)
        {
            try
            {

                return odata.Getlist(IdEmpresa, IdUsuario, FechaCorte);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<CXC_004_Info> Getlist_Resumen(int IdEmpresa, string IdUsuario, DateTime FechaCorte)
        {
            try
            {

                return odata.Getlist_Resumen(IdEmpresa, IdUsuario, FechaCorte);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
