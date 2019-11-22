using Core.Data.Facturacion;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Facturacion
{
    public class fa_TerminoPago_Bus
    {
        fa_TerminoPago_Data odata = new fa_TerminoPago_Data();

        public List<fa_TerminoPago_Info> get_list(bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
