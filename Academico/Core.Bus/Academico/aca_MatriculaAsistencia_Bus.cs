using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaAsistencia_Bus
    {
        aca_MatriculaAsistencia_Data odata = new aca_MatriculaAsistencia_Data();

        public List<aca_MatriculaAsistencia_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<aca_MatriculaAsistencia_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getList(IdEmpresa, IdMatricula);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public aca_MatriculaAsistencia_Info getInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return getInfo(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificar(aca_MatriculaAsistencia_Info info)
        {
            try
            {
                return odata.modificar(info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool GenerarCalificacion(List<aca_MatriculaAsistencia_Info> lst_asistencia)
        {
            try
            {
                return odata.generarCalificacion(lst_asistencia);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
