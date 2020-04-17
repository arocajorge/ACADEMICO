using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_012_Bus
    {
        ACA_012_Data odata = new ACA_012_Data();

        public List<ACA_012_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin, decimal IdRubro, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.GetList(IdEmpresa,FechaIni,FechaFin,IdRubro,IdAnio,IdSede,IdNivel,IdJornada,IdCurso);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
