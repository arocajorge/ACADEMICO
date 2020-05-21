using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Curso_Bus
    {
        aca_Curso_Data odata = new aca_Curso_Data();
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivo_Jornada_Curso_Data odata_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Data();
        public List<aca_Curso_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Curso_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Curso_Info> GetList_Combos(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel)
        {
            try
            {
                return odata.getList_Combos(IdEmpresa, IdAnio, IdSede, IdJornada, IdNivel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Curso_Info> GetList_CambioCurso(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, decimal IdMatricula)
        {
            try
            {
                return odata.getList_CambioCurso(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdMatricula);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_Curso_Info GetInfo(int IdEmpresa, int IdCurso)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetOrden(int IdEmpresa)
        {
            try
            {
                return odata.getOrden(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Curso_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_Curso_Info info)
        {
            try
            {
                var lst_anios = odata_anio.getList_update(info.IdEmpresa);
                if (odata.modificarDB(info))
                {
                    if (lst_anios.Count > 0)
                    {
                        foreach (var item in lst_anios)
                        {
                            var lst_jornada_curso = odata_jornada_curso.getList_Update(info.IdEmpresa, item.IdAnio, info.IdCurso);
                            if (lst_jornada_curso.Count > 0)
                            {
                                foreach (var info_jornada_curso in lst_jornada_curso)
                                {
                                    info_jornada_curso.NomCurso = info.NomCurso;
                                    info_jornada_curso.OrdenCurso = info.OrdenCurso;
                                }

                                return (odata_jornada_curso.modificarDB(lst_jornada_curso));
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(aca_Curso_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
