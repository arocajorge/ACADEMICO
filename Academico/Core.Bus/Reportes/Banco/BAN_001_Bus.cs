﻿using Core.Data.Reportes.Banco;
using Core.Info.Reportes.Banco;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.Banco
{
    public class BAN_001_Bus
    {
        BAN_001_Data odata = new BAN_001_Data();
    
        public List<BAN_001_Info> get_list(int IdEmpresa, int IdTipoCbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipoCbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
