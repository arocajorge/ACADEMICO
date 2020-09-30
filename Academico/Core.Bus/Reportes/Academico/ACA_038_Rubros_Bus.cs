using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_038_Rubros_Bus
    {
        ACA_038_Rubros_Data odata = new ACA_038_Rubros_Data();
        public List<ACA_038_Rubros_Info> get_list(int IdEmpresa, int IdAnio, decimal IdMatricula)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
