using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_sis_Impuesto_Bus
    {
        tb_sis_Impuesto_Data odata = new tb_sis_Impuesto_Data();

        public List<tb_sis_Impuesto_Info> get_list(string IdTipoImpuesto, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdTipoImpuesto, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public tb_sis_Impuesto_Info get_info(string IdCod_Impuesto = "")
        {
            try
            {
                return odata.get_info(IdCod_Impuesto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
