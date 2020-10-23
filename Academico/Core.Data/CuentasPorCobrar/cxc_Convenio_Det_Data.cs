using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_Convenio_Det_Data
    {
        public List<cxc_Convenio_Det_Info> getList(int IdEmpresa, int IdConvenio)
        {
            try
            {
                List<cxc_Convenio_Det_Info> Lista = new List<cxc_Convenio_Det_Info>();

                using (EntitiesCuentasPorCobrar odata = new EntitiesCuentasPorCobrar())
                {
                    var lst = odata.cxc_Convenio_Det.Where(q => q.IdEmpresa == IdEmpresa && q.IdConvenio == IdConvenio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new cxc_Convenio_Det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdConvenio = q.IdConvenio,
                            NumCuota = q.NumCuota,
                            FechaPago = q.FechaPago,
                            TotalCuota = q.TotalCuota,
                            IdCatalogoEstadoPago = q.IdCatalogoEstadoPago,
                            Observacion_det = q.Observacion_det,
                            Saldo=q.Saldo,
                            SaldoInicial=q.SaldoInicial
                        });
                    });
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
