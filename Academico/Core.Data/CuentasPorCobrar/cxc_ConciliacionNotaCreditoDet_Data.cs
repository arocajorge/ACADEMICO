using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Data
    {
        public List<cxc_ConciliacionNotaCreditoDet_Info> GetList(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> Lista = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdConciliacion = item.IdConciliacion,
                            Secuencia = item.Secuencia,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVtaNota = item.IdCbteVtaNota,
                            vt_TipoDoc = item.vt_TipoDoc,
                            Valor = item.Valor,
                            ReferenciaDet = item.ReferenciaDet
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
