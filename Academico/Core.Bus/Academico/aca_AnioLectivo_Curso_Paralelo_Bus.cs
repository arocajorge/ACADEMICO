using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Curso_Paralelo_Bus
    {
        aca_AnioLectivo_Curso_Paralelo_Data odata = new aca_AnioLectivo_Curso_Paralelo_Data();
        public List<aca_AnioLectivo_Curso_Paralelo_Info> GetList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSede, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TreeList_Info> GetList_CorreoMasivo(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.get_list_CorreoMasivo(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TreeList_Info> GetList_CorreoMasivoDeudores(int IdEmpresa, int IdAnio, int CantidadIni, int CantidadFin)
        {
            try
            {
                return odata.get_list_CorreoMasivoDeudores(IdEmpresa, IdAnio, CantidadIni, CantidadFin);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<aca_AnioLectivo_Curso_Paralelo_Info> GetListByCurso(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarDB(aca_AnioLectivo_Curso_Paralelo_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Paralelo_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Paralelo_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
