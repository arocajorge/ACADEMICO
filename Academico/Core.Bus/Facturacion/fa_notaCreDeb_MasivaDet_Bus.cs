using Core.Data.Facturacion;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Facturacion
{
    public class fa_notaCreDeb_MasivaDet_Bus
    {
        fa_notaCreDeb_MasivaDet_Data odata = new fa_notaCreDeb_MasivaDet_Data();
        public List<fa_notaCreDeb_MasivaDet_Info> GetList(int IdEmpresa, decimal IdNCMasivo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdNCMasivo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ModificarDB(fa_notaCreDeb_MasivaDet_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
