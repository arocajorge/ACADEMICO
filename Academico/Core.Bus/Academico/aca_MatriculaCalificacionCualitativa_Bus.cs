using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCalificacionCualitativa_Bus
    {
        aca_MatriculaCalificacionCualitativa_Data odata = new aca_MatriculaCalificacionCualitativa_Data();

        public List<aca_MatriculaCalificacionCualitativa_Info> getList(int IdEmpresa, decimal IdMatricula)
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

        public aca_MatriculaCalificacionCualitativa_Info GetInfo_X_Matricula(int IdEmpresa, decimal IdMatricula, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                return odata.getInfo_X_Matricula(IdEmpresa, IdMatricula, IdMateria, IdCatalogoParcial);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionCualitativa_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial, decimal IdProfesor)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo,IdMateria, IdCatalogoParcial,IdProfesor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_MatriculaCalificacionCualitativa_Info get_Info(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial, int IdMateria, decimal IdProfesor)
        {
            try
            {
                return odata.get_Info(IdEmpresa, IdMatricula,IdCatalogoParcial,IdMateria, IdProfesor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdAnio, IdSede);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdAnio, IdSede, IdProfesor,EsSuperAdmin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_MatriculaCalificacionCualitativa_Info> GetList_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                return odata.GetList_SuperAdmin(IdEmpresa, IdSede,IdAnio,IdNivel,IdJornada,IdCurso,IdParalelo,IdMateria,IdCatalogoParcial);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool generarCalificacion(List<aca_MatriculaCalificacionCualitativa_Info> lst_parcial)
        {
            try
            {
                return odata.generarCalificacion(lst_parcial);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(aca_MatriculaCalificacionCualitativa_Info info)
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
    }
}
