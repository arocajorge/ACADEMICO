using Core.Data.Base;
using Core.Data.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Paralelo_Profesor_Data
    {
        aca_AnioLectivo_Curso_Materia_Data odata_curso_materia = new aca_AnioLectivo_Curso_Materia_Data();
        public List<aca_AnioLectivo_Paralelo_Profesor_Info> get_list_asignacion(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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
                             select new aca_AnioLectivo_Paralelo_Profesor_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 IdMateria = q.IdMateria,
                                 NomMateria = q.NomMateria,
                                 IdProfesor = q.IdProfesor,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion??0,
                                 seleccionado = true
                             }).ToList();

                    Lista.AddRange((from q in Context.vwaca_AnioLectivo_Paralelo_Profesor_NoAsignados
                                    where q.IdEmpresa == IdEmpresa
                                     && q.IdAnio == IdAnio
                                     && q.IdSede == IdSede
                                     && q.IdJornada == IdJornada
                                     && q.IdNivel == IdNivel
                                     && q.IdCurso == IdCurso
                                     && q.IdParalelo == IdParalelo
                                     && !Context.aca_AnioLectivo_Paralelo_Profesor.Any(me =>
                                     me.IdEmpresa == IdEmpresa
                                     && me.IdAnio == IdAnio
                                     && me.IdSede == IdSede
                                     && me.IdCurso == IdCurso
                                     && me.IdParalelo == IdParalelo)
                                    select new aca_AnioLectivo_Paralelo_Profesor_Info
                                    {
                                        IdEmpresa = q.IdEmpresa,
                                        IdSede = q.IdSede,
                                        IdAnio = q.IdAnio,
                                        IdNivel = q.IdNivel,
                                        IdJornada = q.IdJornada,
                                        IdCurso = q.IdCurso,
                                        IdParalelo = IdParalelo,
                                        IdMateria = q.IdMateria,
                                        NomMateria = q.NomMateria,
                                        IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion ?? 0,
                                        seleccionado = false
                                    }).ToList());
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, List<aca_AnioLectivo_Paralelo_Profesor_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    if (lista.Count > 0)
                    {
                        var info_anio_curso = Context.aca_AnioLectivo.Where(q=> q.IdEmpresa==IdEmpresa && q.IdAnio== IdAnio && q.EnCurso==true).FirstOrDefault();
                        if (info_anio_curso!=null)
                        {
                            var lst_MateriaProfesor = Context.aca_AnioLectivo_Paralelo_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).ToList();
                            Context.aca_AnioLectivo_Paralelo_Profesor.RemoveRange(lst_MateriaProfesor);

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
                                    IdProfesor = info.IdProfesor
                                };
                                Context.aca_AnioLectivo_Paralelo_Profesor.Add(Entity);

                                var lst_matricula = Context.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede==info.IdSede
                                && q.IdNivel ==info.IdNivel && q.IdJornada==IdJornada && q.IdCurso ==IdCurso && q.IdParalelo==IdParalelo).ToList();

                                var info_materia_curso = odata_curso_materia.getInfo(IdEmpresa,IdSede,IdAnio,IdNivel,IdJornada,IdCurso,info.IdMateria);
                                if (lst_matricula!=null && lst_matricula.Count>0)
                                {
                                    foreach (var item in lst_matricula)
                                    {

                                        if (info_materia_curso.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI))
                                        {
                                            var lst_MatriculaCalificacionParcial = Context.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == item.IdMatricula && q.IdMateria == info.IdMateria).ToList();
                                            if (lst_MatriculaCalificacionParcial.Count > 0)
                                            {
                                                lst_MatriculaCalificacionParcial.ForEach(q => q.IdProfesor = info.IdProfesor);
                                            }

                                            var lst_MatriculaCalificacion = Context.aca_MatriculaCalificacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == item.IdMatricula && q.IdMateria == info.IdMateria).ToList();
                                            if (lst_MatriculaCalificacion.Count > 0)
                                            {
                                                lst_MatriculaCalificacion.ForEach(q => q.IdProfesor = info.IdProfesor);
                                            }
                                        }
                                        else
                                        {
                                            var lst_MatriculaCalificacionCualitativaParcial = Context.aca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == item.IdMatricula && q.IdMateria == info.IdMateria).ToList();
                                            if (lst_MatriculaCalificacionCualitativaParcial.Count > 0)
                                            {
                                                lst_MatriculaCalificacionCualitativaParcial.ForEach(q => q.IdProfesor = info.IdProfesor);
                                            }

                                            var lst_MatriculaCalificacionPromedio = Context.aca_MatriculaCalificacionCualitativaPromedio.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == item.IdMatricula && q.IdMateria == info.IdMateria).ToList();
                                            if (lst_MatriculaCalificacionPromedio.Count > 0)
                                            {
                                                lst_MatriculaCalificacionPromedio.ForEach(q => q.IdProfesor = info.IdProfesor);
                                            }
                                        }
                                        
                                    }
                                }
                            }                           
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

        public List<aca_AnioLectivo_Paralelo_Profesor_Info> get_list(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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
                             select new aca_AnioLectivo_Paralelo_Profesor_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 IdMateria = q.IdMateria,
                                 NomMateria = q.NomMateria,
                                 IdProfesor = q.IdProfesor,
                                 pe_nombreCompleto = q.pe_nombreCompleto,
                                 IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion??0
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Paralelo_Profesor_Info> get_list_x_curso(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso)
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
                             select new aca_AnioLectivo_Paralelo_Profesor_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 IdMateria = q.IdMateria,
                                 NomMateria = q.NomMateria,
                                 IdProfesor = q.IdProfesor,
                                 pe_nombreCompleto = q.pe_nombreCompleto
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Paralelo_Profesor_Info> get_list_compartidas(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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
                             && q.IdProfesor == null
                             select new aca_AnioLectivo_Paralelo_Profesor_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSede = q.IdSede,
                                 IdAnio = q.IdAnio,
                                 IdNivel = q.IdNivel,
                                 IdJornada = q.IdJornada,
                                 IdCurso = q.IdCurso,
                                 IdParalelo = q.IdParalelo,
                                 IdMateria = q.IdMateria,
                                 NomMateria = q.NomMateria,
                                 IdProfesor = q.IdProfesor,
                                 pe_nombreCompleto = q.pe_nombreCompleto
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
