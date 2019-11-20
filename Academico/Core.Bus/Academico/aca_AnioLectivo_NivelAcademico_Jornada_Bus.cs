using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_NivelAcademico_Jornada_Bus
    {
        aca_AnioLectivo_NivelAcademico_Jornada_Data odata = new aca_AnioLectivo_NivelAcademico_Jornada_Data();
        public List<aca_AnioLectivo_NivelAcademico_Jornada_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdSede, IdAnio, IdNivel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
