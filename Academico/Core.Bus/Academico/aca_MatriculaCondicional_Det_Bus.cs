using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCondicional_Det_Bus
    {
        aca_MatriculaCondicional_Det_Data odata = new aca_MatriculaCondicional_Det_Data();
        public List<aca_MatriculaCondicional_Det_Info> getList(int IdEmpresa, decimal IdCondicional)
        {
            try
            {
                return odata.getList(IdEmpresa, IdCondicional);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
