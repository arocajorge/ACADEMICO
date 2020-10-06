using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCalificacionCualitativaPromedio_Bus
    {
        aca_MatriculaCalificacionCualitativaPromedio_Data odata = new aca_MatriculaCalificacionCualitativaPromedio_Data();
        public List<aca_MatriculaCalificacionCualitativaPromedio_Info> GetList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getList(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GenerarCalificacion(List<aca_MatriculaCalificacionCualitativaPromedio_Info> lst_calificacion_promedio)
        {
            try
            {
                return odata.generarCalificacion(lst_calificacion_promedio);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
