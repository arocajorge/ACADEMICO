using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
   public class CXC_005_Data
    {
        public List<CXC_005_Info> GetList(int IdEmpresa, int IdSucursal, decimal IdLiquidacion)
        {
            try
            {
                List<CXC_005_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWCXC_005.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdSucursal == IdSucursal
                    && q.IdLiquidacion == IdLiquidacion
                    ).Select(q => new CXC_005_Info
                    {
                        IdLiquidacion = q.IdLiquidacion ,
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        ba_descripcion = q.ba_descripcion,
                        DescripcionMotivo = q.DescripcionMotivo,
                        Estado = q.Estado,
                        Fecha = q.Fecha,
                        IdCbteCble_ct = q.IdCbteCble_ct,
                        IdEmpresa_ct = q.IdEmpresa_ct,
                        IdMotivo = q.IdMotivo,
                        IdTipoCbte_ct = q.IdTipoCbte_ct,
                        Lote = q.Lote,
                        NombreUsuario = q.NombreUsuario,
                        Observacion = q.Observacion,
                        Secuencia = q.Secuencia,
                        Su_Descripcion = q.Su_Descripcion,
                        Valor = q.Valor
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
