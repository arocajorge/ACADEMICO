﻿using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Curso_Materia_Data
    {
        public List<aca_AnioLectivo_Curso_Materia_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Materia_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Curso_Materia
                             join c in Context.aca_Materia
                             on new { q.IdEmpresa, q.IdMateria } equals new { c.IdEmpresa, c.IdMateria }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && q.IdCurso == IdCurso
                             && c.Estado == true
                             select new aca_AnioLectivo_Curso_Materia_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdMateria = q.IdMateria,
                                 NomMateria = q.NomMateria,
                                 OrdenMateria = q.OrdenMateria,
                                 EsObligatorio = q.EsObligatorio
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Materia
                                    where !Context.aca_AnioLectivo_Curso_Materia.Any(n => n.IdMateria == j.IdMateria && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCurso)
                                    && j.Estado == true
                                    select new aca_AnioLectivo_Curso_Materia_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = IdCurso,
                                        IdMateria = j.IdMateria,
                                        NomMateria = j.NomMateria,
                                        OrdenMateria = j.OrdenMateria,
                                        EsObligatorio = j.EsObligatorio
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, List<aca_AnioLectivo_Curso_Materia_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_MateriaPorCurso = Context.aca_AnioLectivo_Curso_Materia.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();
                    Context.aca_AnioLectivo_Curso_Materia.RemoveRange(lst_MateriaPorCurso);

                    var lst_antigua_materia_profesor = Context.aca_AnioLectivo_Paralelo_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).ToList();

                    if (lista.Count > 0)
                    {
                        foreach (var info in lista)
                        {
                            aca_AnioLectivo_Curso_Materia Entity = new aca_AnioLectivo_Curso_Materia
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdAnio = info.IdAnio,
                                IdSede = info.IdSede,
                                IdNivel = info.IdNivel,
                                IdJornada = info.IdJornada,
                                IdCurso = info.IdCurso,
                                IdMateria = info.IdMateria,
                                NomMateria = info.NomMateria,
                                NomMateriaArea = info.NomMateriaArea,
                                NomMateriaGrupo = info.NomMateriaGrupo,
                                OrdenMateria = info.OrdenMateria,
                                OrdenMateriaArea = (info.NomMateriaArea==null ? null : info.OrdenMateriaArea),
                                OrdenMateriaGrupo = (info.NomMateriaGrupo==null ? null : info.OrdenMateriaGrupo),
                                EsObligatorio = info.EsObligatorio
                            };

                            Context.aca_AnioLectivo_Curso_Materia.Add(Entity);
                        }


                    }

                    
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Materia_Info> getList_Update(int IdEmpresa, int IdAnio, int IdMateria)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Materia_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Curso_Materia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdMateria == IdMateria).Select(q => new aca_AnioLectivo_Curso_Materia_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSede = q.IdSede,
                        IdAnio = q.IdAnio,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdMateria = q.IdMateria,
                        NomMateria = q.NomMateria,
                        NomMateriaArea = q.NomMateriaArea,
                        NomMateriaGrupo = q.NomMateriaGrupo,
                        OrdenMateria = q.OrdenMateria,
                        OrdenMateriaArea = q.OrdenMateriaArea,
                        OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                        EsObligatorio = q.EsObligatorio
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(List<aca_AnioLectivo_Curso_Materia_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            aca_AnioLectivo_Curso_Materia Entity = Context.aca_AnioLectivo_Curso_Materia.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa
                            && q.IdSede == item.IdSede && q.IdAnio == item.IdAnio && q.IdNivel == item.IdNivel && q.IdJornada == item.IdJornada && q.IdCurso == item.IdCurso && q.IdMateria == item.IdMateria);
                            if (Entity == null)
                                return false;

                            Entity.NomMateria = item.NomMateria;
                            Entity.NomMateriaArea = item.NomMateriaArea;
                            Entity.NomMateriaGrupo = item.NomMateriaGrupo;
                            Entity.EsObligatorio = item.EsObligatorio;
                            Entity.OrdenMateria = item.OrdenMateria;
                            Entity.OrdenMateriaArea = item.OrdenMateriaArea;
                            Entity.OrdenMateriaGrupo = item.OrdenMateriaGrupo;

                        }
                        Context.SaveChanges();
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
