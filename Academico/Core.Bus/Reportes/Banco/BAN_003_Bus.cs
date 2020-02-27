using Core.Data.Reportes.Banco;
using Core.Info.Reportes.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Banco
{
    public class BAN_003_Bus
    {
        BAN_003_Data odata = new BAN_003_Data();
    
        public List<BAN_003_Info> get_list(int IdEmpresa, int IdTipocbte, decimal IdCbteCble)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdTipocbte, IdCbteCble);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
