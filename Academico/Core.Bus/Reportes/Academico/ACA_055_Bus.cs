using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_055_Bus
    {
        ACA_055_Data odata = new ACA_055_Data();

        public List<ACA_055_Info> GetList(int IdEmpresa,int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            return odata.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
        }
    }
}
