using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Paralelo_Data
    {
        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Curso_Paralelo
                             join c in Context.aca_Paralelo
                             on new { q.IdEmpresa, q.IdParalelo } equals new { c.IdEmpresa, c.IdParalelo }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && c.Estado == true
                             select new aca_AnioLectivo_Curso_Paralelo_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 NomParalelo = q.NomParalelo,
                                 OrdenParalelo = q.OrdenParalelo
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Paralelo
                                    where !Context.aca_AnioLectivo_Curso_Paralelo.Any(n => n.IdParalelo == j.IdParalelo && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCurso)
                                    && j.Estado == true
                                    select new aca_AnioLectivo_Curso_Paralelo_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = IdCurso,
                                        IdParalelo = j.IdParalelo,
                                        NomParalelo = j.NomParalelo,
                                        OrdenParalelo = j.OrdenParalelo
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Curso_Paralelo_Info getInfo(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                aca_AnioLectivo_Curso_Paralelo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Curso_Paralelo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        NomParalelo = Entity.NomParalelo,
                        OrdenParalelo = Entity.OrdenParalelo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Paralelo_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_ParaleloPorCurso = Context.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();
                    Context.aca_AnioLectivo_Curso_Paralelo.RemoveRange(lst_ParaleloPorCurso);

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Curso_Paralelo Entity = new aca_AnioLectivo_Curso_Paralelo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdParalelo = info.IdParalelo,
                                CodigoParalelo = info.CodigoParalelo,
                                NomParalelo = info.NomParalelo,
                                OrdenParalelo = info.OrdenParalelo
                            };
                            Context.aca_AnioLectivo_Curso_Paralelo.Add(Entity);
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
