﻿using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_cobro_tipo_Param_conta_x_sucursal_Data
    {
        public List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> get_list(int IdEmpresa, string IdCobro_tipo)
        {
            try
            {
                List<cxc_cobro_tipo_Param_conta_x_sucursal_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.cxc_cobro_tipo_Param_conta_x_sucursal
                             where q.IdEmpresa == IdEmpresa
                             && q.IdCobro_tipo == IdCobro_tipo
                             select new cxc_cobro_tipo_Param_conta_x_sucursal_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdCobro_tipo = q.IdCobro_tipo,
                                 IdCtaCble = q.IdCtaCble,
                                 IdCtaCble_Anticipo = q.IdCtaCble_Anticipo,                                 
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
