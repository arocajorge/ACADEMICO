using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivoParcial_Bus
    {
        aca_AnioLectivoParcial_Data odata = new aca_AnioLectivoParcial_Data();
        public List<aca_AnioLectivoParcial_Info> GetList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
