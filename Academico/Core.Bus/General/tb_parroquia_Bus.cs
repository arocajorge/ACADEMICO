using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_parroquia_Bus
    {
        tb_parroquia_Data odata = new tb_parroquia_Data();
        public List<tb_parroquia_Info> get_list(string IdCiudad, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdCiudad, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
