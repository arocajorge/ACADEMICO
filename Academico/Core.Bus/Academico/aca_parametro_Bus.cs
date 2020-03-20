using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Parametro_Bus
    {
        public aca_Parametro_Info getInfo(int IdEmpresa)
        {
            aca_Parametro_Data odata = new aca_Parametro_Data();

            try
            {
                return odata.getInfo(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
