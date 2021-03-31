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
        public List<aca_MatriculaCalificacionParticipacion_Info> GetList_Combos(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdAnio, IdSede, IdProfesor, EsSuperAdmin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionParticipacion_Info> GetList_CombosParticipacion(int IdEmpresa, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                return odata.getList_CombosParticipacion(IdEmpresa, IdSede, IdProfesor, EsSuperAdmin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionParticipacion_Info> GetList_Calificaciones(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCampoAccion, int IdTematica, int IdCatalogoParcialTipo, decimal IdProfesor)
        {
            try
            {
                return odata.getList_Calificaciones(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdCampoAccion, IdTematica, IdCatalogoParcialTipo, IdProfesor);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionParticipacion_Info> GetList_Calificaciones_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCampoAccion, int IdTematica, int IdCatalogoParcialTipo)
        {
            try
            {
                return odata.getList_Calificaciones_SuperAdmin(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, IdParalelo, IdCampoAccion, IdTematica, IdCatalogoParcialTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool modificarDB(aca_MatriculaCalificacionParticipacion_Info info)
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
