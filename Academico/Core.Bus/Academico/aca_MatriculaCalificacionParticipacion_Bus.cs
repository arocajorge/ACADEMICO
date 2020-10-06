using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCalificacionParticipacion_Bus
    {
        aca_MatriculaCalificacionParticipacion_Data odata = new aca_MatriculaCalificacionParticipacion_Data();
        public List<aca_MatriculaCalificacionParticipacion_Info> GetListParalelo(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.getListParalelo(IdEmpresa, IdAnio, IdSede,IdNivel, IdJornada, IdCurso);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCalificacionParticipacion_Info GetInfo_X_Matricula(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getInfo_X_Matricula(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Guardar(List<aca_MatriculaCalificacionParticipacion_Info> lst_calificacion_participacion)
        {
            try
            {
                return odata.guardar(lst_calificacion_participacion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
