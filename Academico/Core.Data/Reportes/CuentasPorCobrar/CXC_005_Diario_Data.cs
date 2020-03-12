using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_005_Diario_Data
    {
        public List<CXC_005_Diario_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {

                List<CXC_005_Diario_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWCXC_005_Diario.Where(q => q.IdEmpresa == IdEmpresa
                   && q.IdSucursal == IdSucursal
                   && q.IdLiquidacion == IdLiquidacion
                   ).Select(q => new CXC_005_Diario_Info
                   {
                       IdLiquidacion = q.IdLiquidacion,
                       IdEmpresa = q.IdEmpresa,
                       IdSucursal = q.IdSucursal,
                       Debe = q.Debe,
                       Haber = q.Haber,
                       IdCtaCble = q.IdCtaCble,
                       pc_Cuenta = q.pc_Cuenta,
                       secuencia = q.secuencia
                   }).ToList();
                }
                return Lista;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}