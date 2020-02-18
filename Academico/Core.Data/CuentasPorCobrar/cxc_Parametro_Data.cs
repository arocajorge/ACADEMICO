using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_Parametro_Data
    {
        public cxc_Parametro_Info get_info(int IdEmpresa)
        {
            try
            {
                cxc_Parametro_Info info = new cxc_Parametro_Info();
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Parametro Entity = Context.cxc_Parametro.FirstOrDefault(q => q.IdEmpresa == IdEmpresa);
                    if (Entity == null) return null;
                    info = new cxc_Parametro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        pa_IdCaja_x_cobros_x_CXC = Entity.pa_IdCaja_x_cobros_x_CXC,
                        pa_IdTipoCbteCble_CxC = Entity.pa_IdTipoCbteCble_CxC,
                        pa_IdTipoMoviCaja_x_Cobros_x_cliente = Entity.pa_IdTipoMoviCaja_x_Cobros_x_cliente,
                        IdTipoNotaProntoPago = Entity.IdTipoNotaProntoPago,
                        DiasTransaccionesAFuturo = Entity.DiasTransaccionesAFuturo,
                        IdTipoCbte_ConciliacionNC = Entity.IdTipoCbte_ConciliacionNC
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cxc_Parametro_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Parametro Entity = Context.cxc_Parametro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa);
                    if (Entity == null)
                    {
                        Entity = new cxc_Parametro
                        {
                            IdEmpresa = info.IdEmpresa,
                            pa_IdCaja_x_cobros_x_CXC = info.pa_IdCaja_x_cobros_x_CXC,
                            pa_IdTipoCbteCble_CxC = info.pa_IdTipoCbteCble_CxC,
                            pa_IdTipoMoviCaja_x_Cobros_x_cliente = info.pa_IdTipoMoviCaja_x_Cobros_x_cliente,
                            IdTipoNotaProntoPago = info.IdTipoNotaProntoPago,
                            DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo,
                            IdTipoCbte_ConciliacionNC = info.IdTipoCbte_ConciliacionNC,
                            IdUsuario = info.IdUsuario,
                            FechaTransac = DateTime.Now
                        };
                        Context.cxc_Parametro.Add(Entity);
                    }
                    else
                        {
                        Entity.pa_IdCaja_x_cobros_x_CXC = info.pa_IdCaja_x_cobros_x_CXC;
                        Entity.pa_IdTipoCbteCble_CxC = info.pa_IdTipoCbteCble_CxC;
                        Entity.pa_IdTipoMoviCaja_x_Cobros_x_cliente = info.pa_IdTipoMoviCaja_x_Cobros_x_cliente;
                        Entity.IdTipoNotaProntoPago = info.IdTipoNotaProntoPago;
                        Entity.DiasTransaccionesAFuturo = info.DiasTransaccionesAFuturo;
                        Entity.IdTipoCbte_ConciliacionNC = info.IdTipoCbte_ConciliacionNC;
                        Entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                        Entity.FechaUltMod = DateTime.Now;
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
