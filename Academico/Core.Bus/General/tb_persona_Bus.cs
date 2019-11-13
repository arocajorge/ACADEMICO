using Core.Data.General;
using Core.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_persona_Bus
    {
        tb_persona_Data odata = new tb_persona_Data();
        public decimal validar_existe_cedula(string pe_CedulaRuc)
        {
            try
            {
                return odata.validar_existe_cedula(pe_CedulaRuc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            return odata.get_list_bajo_demanda(args, IdEmpresa, IdTipoPersona);
        }

        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            return odata.get_info_bajo_demanda(args, IdEmpresa, IdTipoPersona);
        }
    }
}
