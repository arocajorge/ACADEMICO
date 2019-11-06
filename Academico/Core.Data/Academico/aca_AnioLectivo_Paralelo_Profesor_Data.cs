using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Paralelo_Profesor_Data
    {
        public List<aca_AnioLectivo_Paralelo_Profesor_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria)
        {
            try
            {
                List<aca_AnioLectivo_Paralelo_Profesor_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.vwaca_AnioLectivo_Paralelo_Profesor
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && q.IdParalelo == IdParalelo
                             && q.IdMateria == IdMateria
                             && q.Estado == true
                             select new aca_AnioLectivo_Paralelo_Profesor_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 IdMateria = q.IdMateria,
                                 IdProfesor = q.IdProfesor,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 Codigo = q.Codigo
                             }).ToList();

                    Lista.AddRange((from j in Context.vwaca_Profesor
                                    where !Context.aca_AnioLectivo_Paralelo_Profesor.Any(n => n.IdProfesor == j.IdProfesor && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCurso && n.IdParalelo == IdParalelo && n.IdMateria == IdMateria)
                                    && j.Estado == true
                                    select new aca_AnioLectivo_Paralelo_Profesor_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = IdCurso,
                                        IdParalelo = IdParalelo,
                                        IdMateria = IdMateria,
                                        IdProfesor = j.IdProfesor,
                                        pe_nombreCompleto = j.pe_nombreCompleto,
                                        Codigo = j.Codigo
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, List<aca_AnioLectivo_Paralelo_Profesor_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_MateriaProfesor = Context.aca_AnioLectivo_Paralelo_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo && q.IdMateria == IdMateria).ToList();
                    Context.aca_AnioLectivo_Paralelo_Profesor.RemoveRange(lst_MateriaProfesor);

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Paralelo_Profesor Entity = new aca_AnioLectivo_Paralelo_Profesor
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdMateria = info.IdMateria,
                                IdParalelo = info.IdParalelo,
                                IdProfesor = Convert.ToDecimal(info.IdProfesor)
                            };
                            Context.aca_AnioLectivo_Paralelo_Profesor.Add(Entity);
                        }
                    }
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
