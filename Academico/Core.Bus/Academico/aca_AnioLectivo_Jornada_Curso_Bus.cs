using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Jornada_Curso_Bus
    {
        aca_AnioLectivo_Jornada_Curso_Data odata = new aca_AnioLectivo_Jornada_Curso_Data();
        public List<aca_AnioLectivo_Jornada_Curso_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, List<aca_AnioLectivo_Jornada_Curso_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public List<aca_AnioLectivo_Jornada_Curso_Info> GetListCursoPromoverAlumno(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                return odata.GetListCursoPromoverAlumno(IdEmpresa, IdAlumno, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
