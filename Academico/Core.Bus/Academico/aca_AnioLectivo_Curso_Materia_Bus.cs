using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Curso_Materia_Bus
    {
        aca_AnioLectivo_Curso_Materia_Data odata = new aca_AnioLectivo_Curso_Materia_Data();
        aca_AnioLectivo_Paralelo_Profesor_Data odata_ParaleloProfesor = new aca_AnioLectivo_Paralelo_Profesor_Data();
        public List<aca_AnioLectivo_Curso_Materia_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
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

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Materia_Info> lista)
        {
            try
            {
                if (odata.guardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, IdJornada, IdCurso, lista))
                {
                    var lst_ParaleloProfesor = odata_ParaleloProfesor.get_list_x_curso(IdEmpresa,IdSede,IdAnio,IdNivel,IdJornada,IdCurso);
                    List<aca_AnioLectivo_Paralelo_Profesor_Info> nueva_lista_profesor = new List<aca_AnioLectivo_Paralelo_Profesor_Info>();

                    foreach (var item in lista)
                    {
                        var lst_existe = lst_ParaleloProfesor.Where(q=> q.IdMateria==item.IdMateria).ToList();
                        if (lst_existe!=null)
                        {
                            nueva_lista_profesor.AddRange(lst_existe);
                        }
                    }

                    var listaParalelos = nueva_lista_profesor.GroupBy(q => new { q.IdEmpresa, q.IdSede, q.IdAnio, q.IdNivel, q.IdJornada, q.IdCurso, q.IdParalelo }).Select(q=> 
                    new {
                        IdEmpresa = q.Key.IdEmpresa,
                        IdSede = q.Key.IdSede,
                        IdAnio= q.Key.IdAnio,
                        IdNivel = q.Key.IdNivel,
                        IdJornada = q.Key.IdJornada,
                        IdCurso = q.Key.IdCurso,
                        IdParalelo = q.Key.IdParalelo
                    }).ToList();

                    foreach (var item in listaParalelos)
                    {
                        List<aca_AnioLectivo_Paralelo_Profesor_Info> NuevaListaParaleloProfesor = nueva_lista_profesor.Where(q=> q.IdEmpresa==item.IdEmpresa && q.IdSede==item.IdSede && q.IdAnio==item.IdAnio
                        && q.IdNivel==item.IdNivel && q.IdJornada==item.IdJornada && q.IdCurso==item.IdCurso && q.IdParalelo==item.IdParalelo).ToList();

                        odata_ParaleloProfesor.guardarDB(item.IdEmpresa, item.IdSede, item.IdAnio, item.IdNivel, item.IdJornada, item.IdCurso, item.IdParalelo, NuevaListaParaleloProfesor);
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
