using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_002_Bus
    {
        ACA_002_Data odata = new ACA_002_Data();
        public List<ACA_002_Info> GetList(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAlumno, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
