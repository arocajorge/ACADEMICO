using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_056_Bus
    {
        ACA_056_Data odata = new ACA_056_Data();

        public List<ACA_056_Info> GetList(int IdEmpresa)
        {
            return odata.GetList(IdEmpresa);
        }
    }
}
