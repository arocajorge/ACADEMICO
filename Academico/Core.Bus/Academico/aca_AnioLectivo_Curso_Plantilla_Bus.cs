using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Curso_Plantilla_Bus
    {
        aca_AnioLectivo_Curso_Plantilla_Data odata = new aca_AnioLectivo_Curso_Plantilla_Data();
        public List<aca_AnioLectivo_Curso_Plantilla_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Plantilla_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
