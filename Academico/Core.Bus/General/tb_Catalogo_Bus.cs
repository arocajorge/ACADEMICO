using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_Catalogo_Bus
    {
        tb_Catalogo_Data odata = new tb_Catalogo_Data();
        public List<tb_Catalogo_Info> get_list(int IdTipoCatalogo, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdTipoCatalogo, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_Catalogo_Info get_info(string CodCatalogo)
        {
            try
            {
                return odata.get_info(CodCatalogo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
