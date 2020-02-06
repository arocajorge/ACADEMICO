using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_001_Bus
    {
        CXC_001_Data odata = new CXC_001_Data();

        public List<CXC_001_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdAlumno, DateTime FechaCorte, bool MostrarSaldo0)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdSucursal, IdAlumno, FechaCorte, MostrarSaldo0);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
