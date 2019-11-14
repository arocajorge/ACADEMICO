using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Plantilla_Rubro_Bus
    {
        aca_Plantilla_Rubro_Data odata = new aca_Plantilla_Rubro_Data();
        public List<aca_Plantilla_Rubro_Info> GetListAsignacion(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
