using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_002_Aplicaciones_Data
    {
        public List<CXC_002_Aplicaciones_Info> get_list(int cbr_IdEmpresa, int cbr_IdSucursal, decimal cbr_IdCobro)
        {
            try
            {
                List<CXC_002_Aplicaciones_Info> Lista = new List<CXC_002_Aplicaciones_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    var lst = Context.VWCXC_002_Aplicaciones.Where(q => q.IdEmpresa == cbr_IdEmpresa && q.IdSucursal == cbr_IdSucursal && q.IdCobro == cbr_IdCobro).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_002_Aplicaciones_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCobro = item.IdCobro,
                            secuencial = item.secuencial,
                            dc_TipoDocumento = item.dc_TipoDocumento,
                            vt_NumFactura = item.vt_NumFactura,
                            Total = item.Total,
                            dc_ValorProntoPago = item.dc_ValorProntoPago,
                            dc_ValorPago = item.dc_ValorPago
                        });
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
