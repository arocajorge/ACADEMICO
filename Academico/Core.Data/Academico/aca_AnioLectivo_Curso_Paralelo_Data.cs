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
        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.vwaca_AnioLectivo_Curso_Paralelo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             group q by new { q.IdEmpresa, q.IdAnio, q.IdSede, q.IdNivel, q.IdJornada, q.IdCurso,
                                 q.NomSede, q.Descripcion,q.NomNivel, q.NomJornada, q.NomCurso } into g
                             select new aca_AnioLectivo_Curso_Paralelo_Info
                             {
                                 IdEmpresa = g.Key.IdEmpresa,
                                 IdSede = g.Key.IdSede,
                                 IdAnio = g.Key.IdAnio,
                                 IdNivel = g.Key.IdNivel,
                                 IdJornada = g.Key.IdJornada,
                                 IdCurso = g.Key.IdCurso,
                                 NomSede = g.Key.NomSede,
                                 Descripcion = g.Key.Descripcion,
                                 NomNivel = g.Key.NomNivel,
                                 NomJornada = g.Key.NomJornada,
                                 NomCurso = g.Key.NomCurso
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, decimal IdProfesor)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.vwaca_AnioLectivo_Curso_Paralelo
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSede == IdSede
                             && q.IdAnio == IdAnio
                             && q.IdProfesorTutor == IdProfesor
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.IdAnio,
                                 q.IdSede,
                                 q.IdNivel,
                                 q.IdJornada,
                                 q.IdCurso,
                                 q.NomSede,
                                 q.Descripcion,
                                 q.NomNivel,
                                 q.NomJornada,
                                 q.NomCurso,
                                 q.OrdenParalelo
                             } into g
                             select new aca_AnioLectivo_Curso_Paralelo_Info
                             {
                                 IdEmpresa = g.Key.IdEmpresa,
                                 IdSede = g.Key.IdSede,
                                 IdAnio = g.Key.IdAnio,
                                 IdNivel = g.Key.IdNivel,
                                 IdJornada = g.Key.IdJornada,
                                 IdCurso = g.Key.IdCurso,
                                 NomSede = g.Key.NomSede,
                                 Descripcion = g.Key.Descripcion,
                                 NomNivel = g.Key.NomNivel,
                                 NomJornada = g.Key.NomJornada,
                                 NomCurso = g.Key.NomCurso,
                                 OrdenParalelo = g.Key.OrdenParalelo

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TreeList_Info> get_list_CorreoMasivo(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<TreeList_Info> Lista = new List<TreeList_Info>();
                var ListaSede = new List<vwaca_AnioLectivo_Curso_Paralelo>(); ;
                var ListaJornada = new List<vwaca_AnioLectivo_Curso_Paralelo>();
                var ListaNivel = new List<vwaca_AnioLectivo_Curso_Paralelo>();
                var ListaCurso = new List<vwaca_AnioLectivo_Curso_Paralelo>();
                var ListaParalelo = new List<vwaca_AnioLectivo_Curso_Paralelo>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_sede = new List<TreeList_Info>();
                    var lst_jornada = new List<TreeList_Info>();
                    var lst_nivel = new List<TreeList_Info>();
                    var lst_curso = new List<TreeList_Info>();
                    var lst_paralelo = new List<TreeList_Info>();

                    ListaSede = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();
                    lst_sede = (from q in ListaSede
                                group q by new
                                {
                                    q.IdEmpresa,
                                    q.IdAnio,
                                    q.IdSede,
                                    q.NomSede
                                } into s
                                select new TreeList_Info
                                {
                                    IdString = s.Key.IdSede.ToString("000"),
                                    IdStringPadre = null,
                                    Descripcion = s.Key.NomSede,
                                    Orden = 0
                                }).OrderBy(q => q.Orden).ToList();

                    Lista.AddRange(lst_sede);

                    foreach (var item_sede in lst_sede)
                    {
                        var IdSede = Convert.ToInt32(item_sede.IdString);
                        ListaJornada = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                        lst_jornada = (from q in ListaJornada
                                        group q by new
                                        {
                                            q.IdEmpresa,
                                            q.IdAnio,
                                            q.IdSede,
                                            q.IdJornada,
                                            q.NomJornada,
                                            q.OrdenJornada
                                        } into s
                                        select new TreeList_Info
                                        {
                                            IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                            IdStringPadre = s.Key.IdSede.ToString("000"),
                                            Descripcion = s.Key.NomJornada,
                                            Orden = s.Key.OrdenJornada
                                        }).OrderBy(q => q.Orden).ToList();

                        Lista.AddRange(lst_jornada);

                        foreach (var item_jornada in lst_jornada)
                        {
                            //var IdSede = Convert.ToInt32(item_jornada.IdString.Substring(0, 3));
                            var IdJornada = Convert.ToInt32(item_jornada.IdString.Substring(3, 3));
                            ListaNivel = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada).ToList();

                            lst_nivel = (from q in ListaNivel
                                            group q by new
                                            {
                                                q.IdEmpresa,
                                                q.IdAnio,
                                                q.IdSede,
                                                q.IdJornada,
                                                q.IdNivel,
                                                q.NomNivel,
                                                q.OrdenNivel
                                            } into s
                                            select new TreeList_Info
                                            {
                                                IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                                IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                                Descripcion = s.Key.NomNivel,
                                                Orden = s.Key.OrdenNivel
                                            }).OrderBy(q => q.Orden).ToList();

                            Lista.AddRange(lst_nivel);

                            foreach (var item_nivel in lst_nivel)
                            {
                                //var IdSede = Convert.ToInt32(item_nivel.IdString.Substring(0, 3));
                                //var IdJornada = Convert.ToInt32(item_nivel.IdString.Substring(3, 3));
                                var IdNivel = Convert.ToInt32(item_nivel.IdString.Substring(6, 3));
                                ListaCurso = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel).ToList();

                                lst_curso = (from q in ListaCurso
                                             group q by new
                                             {
                                                 q.IdEmpresa,
                                                 q.IdAnio,
                                                 q.IdSede,
                                                 q.IdJornada,
                                                 q.IdNivel,
                                                 q.IdCurso,
                                                 q.NomCurso,
                                                 q.OrdenCurso
                                             } into s
                                             select new TreeList_Info
                                             {
                                                 IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                 IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                                 Descripcion = s.Key.NomCurso,
                                                 Orden = s.Key.OrdenCurso
                                             }).OrderBy(q => q.Orden).ToList();

                                Lista.AddRange(lst_curso);

                                foreach (var item_curso in lst_curso)
                                {
                                    //var IdSede = Convert.ToInt32(item_curso.IdString.Substring(0, 3));
                                    //var IdJornada = Convert.ToInt32(item_curso.IdString.Substring(3, 3));
                                    //var IdNivel = Convert.ToInt32(item_curso.IdString.Substring(6, 3));
                                    var IdCurso = Convert.ToInt32(item_curso.IdString.Substring(9, 3));

                                    ListaCurso = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso).ToList();

                                    lst_paralelo = (from q in ListaCurso
                                                    group q by new
                                                    {
                                                        q.IdEmpresa,
                                                        q.IdAnio,
                                                        q.IdSede,
                                                        q.IdJornada,
                                                        q.IdNivel,
                                                        q.IdCurso,
                                                        q.IdParalelo,
                                                        q.NomParalelo,
                                                        q.OrdenParalelo
                                                    } into s
                                                    select new TreeList_Info
                                                    {
                                                        IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                        IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                        Descripcion = s.Key.NomParalelo,
                                                        Orden = s.Key.OrdenParalelo
                                                    }).OrderBy(q => q.Orden).ToList();

                                    Lista.AddRange(lst_paralelo);
                                }
                            }
                        }
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TreeList_Info> get_list_CorreoMasivoDeudores(int IdEmpresa, int IdAnio, int CantidadIni, int CantidadFin)
        {
            try
            {
                List<TreeList_Info> Lista = new List<TreeList_Info>();
                var ListaSede = new List<vwaca_Alumno_PeriodoActual>(); ;
                var ListaJornada = new List<vwaca_Alumno_PeriodoActual>();
                var ListaNivel = new List<vwaca_Alumno_PeriodoActual>();
                var ListaCurso = new List<vwaca_Alumno_PeriodoActual>();
                var ListaParalelo = new List<vwaca_Alumno_PeriodoActual>();
                var ListaAlumno = new List<vwaca_Alumno_PeriodoActual>();

                int CantidadIniConsulta = CantidadIni;
                int CantidadFinConsulta = CantidadIni == 0 ? 9999999 : CantidadFin;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_sede = new List<TreeList_Info>();
                    var lst_jornada = new List<TreeList_Info>();
                    var lst_nivel = new List<TreeList_Info>();
                    var lst_curso = new List<TreeList_Info>();
                    var lst_paralelo = new List<TreeList_Info>();
                    var lst_alumno = new List<TreeList_Info>();

                    Context.Database.CommandTimeout = 5000;
                    var ListaGeneralPorCantidadFacturas = Context.vwaca_Alumno_PeriodoActual.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio 
                    && q.CantDeudas>= CantidadIniConsulta && q.CantDeudas<=CantidadFin).ToList();

                    ListaSede = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList(); ;
                    lst_sede = (from q in ListaSede
                                group q by new
                                {
                                    q.IdEmpresa,
                                    q.IdAnio,
                                    q.IdSede,
                                    q.NomSede
                                } into s
                                select new TreeList_Info
                                {
                                    IdString = s.Key.IdSede.ToString("000"),
                                    IdStringPadre = null,
                                    Descripcion = s.Key.NomSede,
                                    Orden = 0
                                }).OrderBy(q => q.Orden).ToList();

                    Lista.AddRange(lst_sede);

                    foreach (var item_sede in lst_sede)
                    {
                        var IdSede = Convert.ToInt32(item_sede.IdString);
                        ListaJornada = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                        lst_jornada = (from q in ListaJornada
                                       group q by new
                                       {
                                           q.IdEmpresa,
                                           q.IdAnio,
                                           q.IdSede,
                                           q.IdJornada,
                                           q.NomJornada,
                                           q.OrdenJornada
                                       } into s
                                       select new TreeList_Info
                                       {
                                           IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                           IdStringPadre = s.Key.IdSede.ToString("000"),
                                           Descripcion = s.Key.NomJornada,
                                           Orden = s.Key.OrdenJornada??0
                                       }).OrderBy(q => q.Orden).ToList();

                        Lista.AddRange(lst_jornada);

                        foreach (var item_jornada in lst_jornada)
                        {
                            //var IdSede = Convert.ToInt32(item_jornada.IdString.Substring(0, 3));
                            var IdJornada = Convert.ToInt32(item_jornada.IdString.Substring(3, 3));
                            ListaNivel = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada).ToList();

                            lst_nivel = (from q in ListaNivel
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdAnio,
                                             q.IdSede,
                                             q.IdJornada,
                                             q.IdNivel,
                                             q.NomNivel,
                                             q.OrdenNivel
                                         } into s
                                         select new TreeList_Info
                                         {
                                             IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                             IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000"),
                                             Descripcion = s.Key.NomNivel,
                                             Orden = s.Key.OrdenNivel??0
                                         }).OrderBy(q => q.Orden).ToList();

                            Lista.AddRange(lst_nivel);

                            foreach (var item_nivel in lst_nivel)
                            {
                                //var IdSede = Convert.ToInt32(item_nivel.IdString.Substring(0, 3));
                                //var IdJornada = Convert.ToInt32(item_nivel.IdString.Substring(3, 3));
                                var IdNivel = Convert.ToInt32(item_nivel.IdString.Substring(6, 3));
                                ListaCurso = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel).ToList();

                                lst_curso = (from q in ListaCurso
                                             group q by new
                                             {
                                                 q.IdEmpresa,
                                                 q.IdAnio,
                                                 q.IdSede,
                                                 q.IdJornada,
                                                 q.IdNivel,
                                                 q.IdCurso,
                                                 q.NomCurso,
                                                 q.OrdenCurso
                                             } into s
                                             select new TreeList_Info
                                             {
                                                 IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                 IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000"),
                                                 Descripcion = s.Key.NomCurso,
                                                 Orden = s.Key.OrdenCurso??0
                                             }).OrderBy(q => q.Orden).ToList();

                                Lista.AddRange(lst_curso);

                                foreach (var item_curso in lst_curso)
                                {
                                    //var IdSede = Convert.ToInt32(item_curso.IdString.Substring(0, 3));
                                    //var IdJornada = Convert.ToInt32(item_curso.IdString.Substring(3, 3));
                                    //var IdNivel = Convert.ToInt32(item_curso.IdString.Substring(6, 3));
                                    var IdCurso = Convert.ToInt32(item_curso.IdString.Substring(9, 3));

                                    ListaCurso = ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso).ToList();

                                    lst_paralelo = (from q in ListaCurso
                                                    group q by new
                                                    {
                                                        q.IdEmpresa,
                                                        q.IdAnio,
                                                        q.IdSede,
                                                        q.IdJornada,
                                                        q.IdNivel,
                                                        q.IdCurso,
                                                        q.IdParalelo,
                                                        q.NomParalelo,
                                                        q.OrdenParalelo
                                                    } into s
                                                    select new TreeList_Info
                                                    {
                                                        IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                        IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000"),
                                                        Descripcion = s.Key.NomParalelo,
                                                        Orden = s.Key.OrdenParalelo??0
                                                    }).OrderBy(q => q.Orden).ToList();

                                    Lista.AddRange(lst_paralelo);

                                    foreach (var item_paralelo in lst_paralelo)
                                    {
                                        var IdParalelo = Convert.ToInt32(item_paralelo.IdString.Substring(12, 3));

                                        ListaAlumno= ListaGeneralPorCantidadFacturas.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso && q.IdParalelo==IdParalelo).OrderBy(q=>q.NombreAlumno).ToList();

                                        foreach (var item in ListaAlumno)
                                        {
                                            var info = new TreeList_Info
                                            {
                                                IdString = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000") + item.IdAlumno.ToString("000000"),
                                                IdStringPadre = IdSede.ToString("000") + IdJornada.ToString("000") + IdNivel.ToString("000") + IdCurso.ToString("000") + IdParalelo.ToString("000"),
                                                Descripcion = item.Codigo + " - " + item.NombreAlumno,
                                                Orden = 0,
                                                CorreoEmiteFactura = item.CorreoEmiteFactura,
                                                CorreoRepresentante = item.CorreoRepresentante
                                            };
                                            Lista.Add(info);
                                        }
                                        /*
                                        lst_alumno = (from q in ListaParalelo
                                                        group q by new
                                                        {
                                                            q.IdEmpresa,
                                                            q.IdAnio,
                                                            q.IdSede,
                                                            q.IdJornada,
                                                            q.IdNivel,
                                                            q.IdCurso,
                                                            q.IdParalelo,
                                                            q.NomParalelo,
                                                            q.OrdenParalelo,
                                                            q.IdAlumno,
                                                            q.Codigo,
                                                            q.NombreAlumno
                                                        } into s
                                                        select new TreeList_Info
                                                        {
                                                            IdString = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000") + s.Key.IdParalelo.ToString("000") + s.Key.IdAlumno.ToString("000000"),
                                                            IdStringPadre = s.Key.IdSede.ToString("000") + s.Key.IdJornada.ToString("000") + s.Key.IdNivel.ToString("000") + s.Key.IdCurso.ToString("000") + s.Key.IdParalelo.ToString("000"),
                                                            Descripcion = s.Key.Codigo + " - " + s.Key.NombreAlumno,
                                                            Orden = 0
                                                        }).OrderBy(q => q.Orden).ToList();
                                        */
                                        
                                    }
                                }
                            }
                        }
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).Select(q => new aca_AnioLectivo_Curso_Paralelo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSede = q.IdSede,
                        IdAnio = q.IdAnio,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdParalelo = q.IdParalelo,
                        NomParalelo = q.NomParalelo,
                        OrdenParalelo = q.OrdenParalelo,
                        IdProfesorInspector = q.IdProfesorInspector,
                        IdProfesorTutor = q.IdProfesorTutor,
                        NomTutor = q.NomTutor,
                        NomInspector = q.NomInspector
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Curso_Paralelo_Info> getList_Update(int IdEmpresa, int IdAnio, int IdParalelo)
        {
            try
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdParalelo == IdParalelo).Select(q => new aca_AnioLectivo_Curso_Paralelo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdSede = q.IdSede,
                        IdNivel = q.IdNivel,
                        IdJornada = q.IdJornada,
                        IdCurso = q.IdCurso,
                        IdParalelo = q.IdParalelo,
                        CodigoParalelo = q.CodigoParalelo,
                        NomParalelo = q.NomParalelo,
                        OrdenParalelo = q.OrdenParalelo
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(List<aca_AnioLectivo_Curso_Paralelo_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            aca_AnioLectivo_Curso_Paralelo Entity = Context.aca_AnioLectivo_Curso_Paralelo.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa
                            && q.IdSede == item.IdSede && q.IdAnio == item.IdAnio && q.IdNivel == item.IdNivel && q.IdJornada == item.IdJornada && q.IdCurso == item.IdCurso && q.IdParalelo == item.IdParalelo);
                            if (Entity == null)
                                return false;

                            Entity.NomParalelo = item.NomParalelo;
                            Entity.OrdenParalelo = item.OrdenParalelo;
                            Entity.CodigoParalelo = item.CodigoParalelo;
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

        public bool ModificarDB(aca_AnioLectivo_Curso_Paralelo_Info info)
        {
            try
            {
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdNivel == info.IdNivel && q.IdJornada == info.IdJornada && q.IdSede == info.IdSede && q.IdCurso == info.IdCurso && q.IdParalelo == info.IdParalelo).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.IdProfesorInspector = info.IdProfesorInspector;
                        Entity.IdProfesorTutor = info.IdProfesorTutor;
                    }
                    db.SaveChanges();
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
