using Core.Data.Academico;
using Core.Info.Academico;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Matricula_Bus
    {
        aca_Matricula_Data odata = new aca_Matricula_Data();
        public List<aca_Matricula_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, MostrarAnulados);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<aca_Matricula_Info> GetList_PorCurso(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.getList_PorCurso(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Matricula_Info GetInfo(int IdEmpresa, decimal IdMatricula)
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

        public aca_Matricula_Info GetInfo_ExisteMatricula(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                return odata.getInfo_ExisteMatricula(IdEmpresa, IdAnio, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Matricula_Info info)
        {
            try
            {
                if (odata.guardarDB(info))
                {
                    var lst_rubros_x_cobrar = info.lst_MatriculaRubro.Where(q => q.seleccionado == true);
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_Matricula_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarPlantillaDB(aca_Matricula_Info info)
        {
            try
            {
                return odata.modificarPlantillaDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ModificarCursoParaleloDB(aca_Matricula_Info info)
        {
            try
            {
                return odata.modificarCursoParaleloDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool EliminarDB(aca_Matricula_Info info)
        {
            try
            {
                return odata.eliminarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
