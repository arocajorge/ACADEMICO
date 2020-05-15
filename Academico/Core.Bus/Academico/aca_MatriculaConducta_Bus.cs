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

        public aca_MatriculaConducta_Info GetInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaConducta_Info> GetList_Combos(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaConducta_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo);
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

        public bool ModicarPromedioFinal(aca_MatriculaConducta_Info info)
        {
            try
            {
                return odata.modicarPromedioFinal(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modicarPromedioPaseAnio(aca_MatriculaConducta_Info info)
        {
            try
            {
                return odata.modicarPromedioPaseAnio(info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
