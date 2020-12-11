using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_062_Bus
    {
        ACA_062_Data odata = new ACA_062_Data();

        public List<ACA_062_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            return odata.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados);
        }
    }
}
