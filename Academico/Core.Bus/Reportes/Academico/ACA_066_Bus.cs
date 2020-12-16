using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_066_Bus
    {
        ACA_066_Data odata = new ACA_066_Data();

        public List<ACA_066_Info> GetList(int IdEmpresa, int IdAnio)
        {
            return odata.GetList(IdEmpresa, IdAnio);
        }
    }
}
