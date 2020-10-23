using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_Convenio_Det_Bus
    {
        cxc_Convenio_Det_Data odata = new cxc_Convenio_Det_Data();
        public List<cxc_Convenio_Det_Info> GetList(int IdEmpresa, int IdConvenio)
        {
            try
            {
                return odata.getList(IdEmpresa, IdConvenio);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
