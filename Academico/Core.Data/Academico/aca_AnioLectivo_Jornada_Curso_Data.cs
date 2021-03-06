﻿using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Jornada_Curso_Data
    {
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();
        public List<aca_AnioLectivo_Jornada_Curso_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada)
        {
            try
            {
                List<aca_AnioLectivo_Jornada_Curso_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_AnioLectivo_Jornada_Curso
                             join c in Context.aca_Curso
                             on new { q.IdEmpresa, q.IdCurso } equals new { c.IdEmpresa, c.IdCurso }
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdNivel == IdNivel
                             && q.IdJornada == IdJornada
                             && c.Estado == true
                             select new aca_AnioLectivo_Jornada_Curso_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 NomCurso = q.NomCurso,
                                 OrdenCurso = q.OrdenCurso
                             }).ToList();

                    Lista.AddRange((from j in Context.aca_Curso
                                    where !Context.aca_AnioLectivo_Jornada_Curso.Any(n => n.IdCurso == j.IdCurso && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada)
                                    && j.Estado == true && j.IdEmpresa == IdEmpresa
                                    select new aca_AnioLectivo_Jornada_Curso_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdSede = IdSede,
                                        IdAnio = IdAnio,
                                        IdNivel = IdNivel,
                                        IdJornada = IdJornada,
                                        IdCurso = j.IdCurso,
                                        NomCurso = j.NomCurso,
                                        OrdenCurso = j.OrdenCurso
                                    }).ToList());
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, List<aca_AnioLectivo_Jornada_Curso_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_anio_curso = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.EnCurso == true).FirstOrDefault();
                    if (info_anio_curso != null)
                    {
                        var lst_JornadaPorCurso = Context.aca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada).ToList();
                        Context.aca_AnioLectivo_Jornada_Curso.RemoveRange(lst_JornadaPorCurso);

                        if (lista.Count > 0)
                        {
                            foreach (var info in lista)
                            {
                                aca_AnioLectivo_Jornada_Curso Entity = new aca_AnioLectivo_Jornada_Curso
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAnio = info.IdAnio,
                                    IdSede = info.IdSede,
                                    IdNivel = info.IdNivel,
                                    IdJornada = info.IdJornada,
                                    IdCurso = info.IdCurso,
                                    NomCurso = info.NomCurso,
                                    OrdenCurso = info.OrdenCurso
                                };
                                Context.aca_AnioLectivo_Jornada_Curso.Add(Entity);
                            }
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

        public List<aca_AnioLectivo_Jornada_Curso_Info> GetListCursoPreMatricula(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Jornada_Curso_Info> Lista = new List<aca_AnioLectivo_Jornada_Curso_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    int IdCursoIni = 0;
                    int IdCursoFin = 0;

                    IdCursoIni = 0;
                    IdCursoFin = 999999;
                    var lst = odata.vwaca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin).OrderBy(q => q.OrdenCurso).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Jornada_Curso_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            NomCurso = q.ComboCurso,
                            OrdenCurso = q.OrdenCurso
                        });
                    });
                }

                Lista.ForEach(v => { v.IdComboCurso = v.IdEmpresa.ToString("0000") + v.IdAnio.ToString("0000") + v.IdSede.ToString("0000") + v.IdNivel.ToString("0000") + v.IdJornada.ToString("0000") + v.IdCurso.ToString("0000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_AnioLectivo_Jornada_Curso_Info> GetListCursoPromoverAlumno(int IdEmpresa, decimal IdAlumno, int IdAnio, string Validar)
        {
            try
            {
                List<aca_AnioLectivo_Jornada_Curso_Info> Lista = new List<aca_AnioLectivo_Jornada_Curso_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    int IdCursoIni = 0;
                    int IdCursoFin = 0;

                    if (Validar == "S")
                    {
                        aca_Alumno alumno = odata.aca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).FirstOrDefault();
                        if (alumno == null)
                            return new List<aca_AnioLectivo_Jornada_Curso_Info>();

                        aca_Curso curso = odata.aca_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdCurso == alumno.IdCurso).FirstOrDefault();
                        IdCursoIni = curso == null ? 0 : (curso.IdCursoAPromover ?? 0);
                        IdCursoFin = curso == null ? 999999 : (curso.IdCursoAPromover ?? 999999);
                    }
                    else
                    {
                        IdCursoIni = 0;
                        IdCursoFin = 999999;
                    }

                    var lst = odata.vwaca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin).OrderBy(q => q.OrdenCurso).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Jornada_Curso_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            NomCurso = q.ComboCurso,
                            OrdenCurso = q.OrdenCurso
                        });
                    });
                }

                Lista.ForEach(v => { v.IdComboCurso = v.IdEmpresa.ToString("0000") + v.IdAnio.ToString("0000") + v.IdSede.ToString("0000") + v.IdNivel.ToString("0000")+ v.IdJornada.ToString("0000")+ v.IdCurso.ToString("0000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Jornada_Curso_Info> GetListCursoCambioMatricula(int IdEmpresa, decimal IdAlumno, int IdAnio, string Validar)
        {
            try
            {
                List<aca_AnioLectivo_Jornada_Curso_Info> Lista = new List<aca_AnioLectivo_Jornada_Curso_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    int IdCursoIni = 0;
                    int IdCursoFin = 0;
                    
                    aca_Alumno alumno = odata.aca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).FirstOrDefault();
                    if (alumno == null)
                        return new List<aca_AnioLectivo_Jornada_Curso_Info>();

                    IdCursoIni = alumno.IdCurso == null ? 0 : (alumno.IdCurso ?? 0);
                    IdCursoFin = alumno.IdCurso == null ? 999999 : (alumno.IdCurso ?? 999999);

                    var lst = odata.vwaca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && IdCursoIni <= q.IdCurso && q.IdCurso <= IdCursoFin).OrderBy(q => q.OrdenCurso).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Jornada_Curso_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            NomCurso = q.ComboCurso,
                            OrdenCurso = q.OrdenCurso
                        });
                    });
                }

                Lista.ForEach(v => { v.IdComboCurso = v.IdEmpresa.ToString("0000") + v.IdAnio.ToString("0000") + v.IdSede.ToString("0000") + v.IdNivel.ToString("0000") + v.IdJornada.ToString("0000") + v.IdCurso.ToString("0000"); });
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Jornada_Curso_Info GetInfoCursoMatricula(int IdEmpresa, int IdAnio, decimal IdMatricula)
        {
            try
            {
                aca_AnioLectivo_Jornada_Curso_Info info_curso = new aca_AnioLectivo_Jornada_Curso_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var matricula = odata_matricula.getInfo(IdEmpresa,IdMatricula);
                    var IdCurso = (matricula == null ? 0 : matricula.IdCurso);
                    var IdJornada = (matricula == null ? 0 : matricula.IdJornada);
                    var IdNivel = (matricula == null ? 0 : matricula.IdNivel);
                    var IdSede = (matricula == null ? 0 : matricula.IdSede);

                    var info = odata.vwaca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede==IdSede && q.IdNivel==IdNivel && q.IdJornada== IdJornada && q.IdCurso == IdCurso).OrderBy(q => q.OrdenCurso).FirstOrDefault();
                    if (info == null) info = new vwaca_AnioLectivo_Jornada_Curso();
                    info_curso.NomCurso = info.NomCurso;
                    info_curso.IdComboCurso = info.IdEmpresa.ToString("0000") + info.IdAnio.ToString("0000") + info.IdSede.ToString("0000") + info.IdNivel.ToString("0000") + info.IdJornada.ToString("0000") + info.IdCurso.ToString("0000");
                }

                return info_curso;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Jornada_Curso_Info> getList_Update(int IdEmpresa, int IdAnio, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Jornada_Curso_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdCurso == IdCurso).Select(q => new aca_AnioLectivo_Jornada_Curso_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        NomCurso = q.NomCurso,
                        OrdenCurso = q.OrdenCurso
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(List<aca_AnioLectivo_Jornada_Curso_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            aca_AnioLectivo_Jornada_Curso Entity = Context.aca_AnioLectivo_Jornada_Curso.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa
                            && q.IdSede == item.IdSede && q.IdAnio == item.IdAnio && q.IdNivel == item.IdNivel && q.IdJornada == item.IdJornada && q.IdCurso == item.IdCurso);
                            if (Entity == null)
                                return false;

                            Entity.NomCurso = item.NomCurso;
                            Entity.OrdenCurso = item.OrdenCurso;
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

        public List<aca_AnioLectivo_Jornada_Curso_Info> GetListCurso_x_Anio(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Jornada_Curso_Info> Lista = new List<aca_AnioLectivo_Jornada_Curso_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    Lista = odata.vwaca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).OrderBy(q=> q.OrdenCurso).GroupBy(q => new { q.IdEmpresa, q.IdAnio, q.IdCurso, q.NomCurso, q.OrdenCurso }).Select(q => new aca_AnioLectivo_Jornada_Curso_Info
                    {
                        IdEmpresa = q.Key.IdEmpresa,
                        IdAnio = q.Key.IdAnio,
                        IdCurso = q.Key.IdCurso,
                        NomCurso = q.Key.NomCurso,
                        OrdenCurso = q.Key.OrdenCurso
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
