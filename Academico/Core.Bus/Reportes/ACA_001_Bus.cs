using Core.Data.Reportes;
using Core.Info.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes
{
    public class ACA_001_Bus
    {
        ACA_001_Data odata = new ACA_001_Data();
        public List<ACA_001_Info> GetList(int IdEmpresa, decimal IdAlumno, int IdAnio)
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
