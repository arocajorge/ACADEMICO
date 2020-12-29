using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_049_Promedio_Bus
    {
        ACA_049_Promedio_Data odata = new ACA_049_Promedio_Data();

        public List<ACA_049_Promedio_Info> GetList(int IdEmpresa, int IdAnio, decimal IdMatricula)
        {
            return odata.GetList(IdEmpresa, IdAnio, IdMatricula);
        }
    }
}
