using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaConducta_Bus
    {
        aca_MatriculaConducta_Data odata = new aca_MatriculaConducta_Data();

        public List<aca_MatriculaConducta_Info> GetList(int IdEmpresa, decimal IdMatricula)
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

        public List<aca_MatriculaConducta_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GenerarCalificacion(List<aca_MatriculaConducta_Info> lst_conducta)
        {
            try
            {
                return odata.generarCalificacion(lst_conducta);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
