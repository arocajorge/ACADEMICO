using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Paralelo_Profesor_Bus
    {
        aca_AnioLectivo_Paralelo_Profesor_Data odata = new aca_AnioLectivo_Paralelo_Profesor_Data();

        public List<aca_AnioLectivo_Paralelo_Profesor_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, List<aca_AnioLectivo_Paralelo_Profesor_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Paralelo_Profesor_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
