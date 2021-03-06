﻿using Core.Data.Base;
using Core.Info.Reportes.Caja;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Reportes.Caja
{
    public class CAJ_002_ValesNoConciliados_Data
    {
        public List<CAJ_002_ValesNoConciliados_Info> GetList(int IdEmpresa, decimal IdConciliacion_Caja)
        {
            try
            {
                List<CAJ_002_ValesNoConciliados_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWCAJ_002_ValesNoConciliados.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdConciliacion_Caja == IdConciliacion_Caja).Select(q => new CAJ_002_ValesNoConciliados_Info
                    {
                        IdConciliacion_Caja = q.IdConciliacion_Caja,
                        IdEmpresa = q.IdEmpresa,
                        cm_fecha = q.cm_fecha,
                        cm_observacion = q.cm_observacion,
                        IdCbteCble = q.IdCbteCble,
                        IdTipocbte = q.IdTipocbte,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Valor = q.Valor,
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
