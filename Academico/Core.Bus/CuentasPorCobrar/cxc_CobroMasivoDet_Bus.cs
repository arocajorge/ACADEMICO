using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_CobroMasivoDet_Bus
    {
        cxc_CobroMasivoDet_Data odata = new cxc_CobroMasivoDet_Data();
        public List<cxc_CobroMasivoDet_Info> GetList(int IdEmpresa, decimal IdCobroMasivo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdCobroMasivo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarDB(cxc_CobroMasivoDet_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
