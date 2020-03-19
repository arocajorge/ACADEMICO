using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_006_Data
    {
        public List<CXC_006_Info> GetList(int IdEmpresa, List<int> ListaSucursal, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<CXC_006_Info> Lista = new List<CXC_006_Info>();

                
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    foreach (var item in ListaSucursal)
                    {
                        var lst = db.SPCXC_006(IdEmpresa, item, FechaIni, FechaFin).ToList();
                        foreach (var q in lst)
                        {
                            Lista.Add(new CXC_006_Info
                            {
                                IdEmpresa = q.IdEmpresa,
                                IdSucursal = q.IdSucursal,
                                IdLiquidacion = q.IdLiquidacion,
                                Lote = q.Lote,
                                Fecha = q.Fecha,
                                IdBanco = q.IdBanco,
                                ba_descripcion = q.ba_descripcion,
                                ValorCobro = q.ValorCobro,
                                ValorComision = q.ValorComision,
                                ValorImpuesto = q.ValorImpuesto,
                                DepositoNeto = q.DepositoNeto,
                                Su_Descripcion = q.Su_Descripcion,
                                Observacion = q.Observacion
                            });
                        }
                    }
                }

                var lstSuc = Lista.GroupBy(q => q.Su_Descripcion).ToList();
                if (lstSuc.Count > 0)
                {
                    string SucursalFiltro = string.Empty;
                    foreach (var item in lstSuc)
                    {
                        SucursalFiltro += item.Key + " ";
                    }
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
