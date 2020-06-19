using Core.Data.Facturacion;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Facturacion
{
    public class fa_AplicacionMasivaDet_Bus
    {
        fa_AplicacionMasivaDet_Data odata = new fa_AplicacionMasivaDet_Data();

        public List<fa_AplicacionMasivaDet_Info> GetList(int IdEmpresa, decimal IdAplicacion)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAplicacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
