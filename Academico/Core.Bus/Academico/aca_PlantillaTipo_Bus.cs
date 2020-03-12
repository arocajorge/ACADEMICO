using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_PlantillaTipo_Bus
    {
        aca_PlantillaTipo_Data odata = new aca_PlantillaTipo_Data();

        public List<aca_PlantillaTipo_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
