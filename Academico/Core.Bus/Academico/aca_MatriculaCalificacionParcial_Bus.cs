using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCalificacionParcial_Bus
    {
        aca_MatriculaCalificacionParcial_Data odata = new aca_MatriculaCalificacionParcial_Data();
        public List<aca_MatriculaCalificacionParcial_Info> GetList(int IdEmpresa, decimal IdMatricula)
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

        public List<aca_MatriculaCalificacionParcial_Info> GenerarCalificacion(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.generarCalificacion(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
