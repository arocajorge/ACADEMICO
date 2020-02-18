using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Data
    {
        public List<cxc_ConciliacionNotaCreditoDet_Info> GetList(int IdEmpresa,decimal IdConciliacion)
        {
            try
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> Lista = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).ToList();
                    foreach (var item in lst)
                    {

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
