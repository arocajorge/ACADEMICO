﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Info.CuentasPorPagar;
using Core.Data.Base;

namespace Core.Data.CuentasPorPagar
{
  public  class cp_pagos_sri_Data
    {
        public List<cp_pagos_sri_Info> get_list()
        {
            try
            {
                List<cp_pagos_sri_Info> Lista;
                using (EntitiesCuentasPorPagar Context = new EntitiesCuentasPorPagar())
                {
                        Lista = (from q in Context.cp_pagos_sri
                                
                                 select new cp_pagos_sri_Info
                                 {
                                      codigo_pago_sri=q.codigo_pago_sri,
                                       formas_pago_sri=q.formas_pago_sri
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
