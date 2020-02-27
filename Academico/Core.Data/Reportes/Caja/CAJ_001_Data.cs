using Core.Data.Base;
using Core.Info.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Caja
{
   public class CAJ_001_Data
    {
        public List<CAJ_001_Info> GetList(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                List<CAJ_001_Info> Lista = new List<CAJ_001_Info>();

                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = db.VWCAJ_001.Where(q => q.IdEmpresa == IdEmpresa && q.IdTipoCbte == IdTipoCbte && q.IdCbteCble == IdCbteCble).Select(q => new CAJ_001_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdTipoCbte = q.IdTipoCbte,
                        IdCbteCble = q.IdCbteCble,
                        secuencia = q.secuencia,
                        pc_Cuenta = q.pc_Cuenta,
                        dc_Valor = q.dc_Valor,
                        dc_Valor_Debe = q.dc_Valor_Debe,
                        dc_Valor_Haber = q.dc_Valor_Haber,
                        tc_descripcion = q.tc_descripcion,
                        cr_Valor = q.cr_Valor,
                        cm_Signo = q.cm_Signo,
                        IdTipoMovi = q.IdTipoMovi,
                        tm_descripcion = q.tm_descripcion,
                        cm_observacion = q.cm_observacion,
                        IdCaja = q.IdCaja,
                        ca_Descripcion = q.ca_Descripcion,
                        cm_fecha = q.cm_fecha,
                        Estado = q.Estado,
                        tc_TipoCbte = q.tc_TipoCbte,
                        IdCtaCble = q.IdCtaCble,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Nombre = q.Nombre,
                        Su_Descripcion = q.Su_Descripcion,
                        SecuenciaCaja = q.SecuenciaCaja
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
