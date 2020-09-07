using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaCalificacionCualitativa_Data
    {
        aca_AnioLectivoParcial_Data odata_parcial = new aca_AnioLectivoParcial_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();
        public List<aca_MatriculaCalificacionCualitativa_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            IdProfesor = q.IdProfesor,
                            IdCalificacionCualitativa = q.IdCalificacionCualitativa,
                            Conducta = q.Conducta,
                            MotivoConducta = q.MotivoConducta
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_MatriculaCalificacionCualitativa_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial, decimal IdProfesor)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == IdEmpresa 
                    && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdCatalogoParcial == IdCatalogoParcial && q.IdProfesor == IdProfesor).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            IdCalificacionCualitativa = q.IdCalificacionCualitativa,
                            Conducta = q.Conducta,
                            MotivoConducta = q.MotivoConducta,
                            Letra = q.Letra,
                            CodigoCalificacion = q.CodigoCalificacion,
                            DescripcionCorta = q.DescripcionCorta,
                            RegistroValido = true,
                            RegistroValidoCalificacion = true,
                            RegistroValidoConducta = true
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionCualitativa_Info> GetList_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == IdEmpresa 
                    && q.IdSede == IdSede && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdCatalogoParcial == IdCatalogoParcial).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            IdCalificacionCualitativaParcial = q.IdCalificacionCualitativa,
                            Conducta = q.Conducta,
                            MotivoConducta = q.MotivoConducta,
                            Letra = q.Letra,
                            CodigoCalificacion = q.CodigoCalificacion,
                            DescripcionCorta = q.DescripcionCorta,
                            RegistroValido = true,
                            RegistroValidoCalificacion = true,
                            RegistroValidoConducta = true
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_MatriculaCalificacionCualitativa_Info get_Info(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial, int IdMateria, decimal IdProfesor)
        {
            try
            {
                aca_MatriculaCalificacionCualitativa_Info info = new aca_MatriculaCalificacionCualitativa_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.aca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.IdMateria == IdMateria
                    && q.IdProfesor == IdProfesor && q.IdCatalogoParcial == IdCatalogoParcial).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaCalificacionCualitativa_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdMateria = Entity.IdMateria,
                        IdProfesor = Entity.IdProfesor,
                        IdCatalogoParcial = Entity.IdCatalogoParcial,
                        IdCalificacionCualitativa = Entity.IdCalificacionCualitativa,
                        Conducta = Entity.Conducta,
                        MotivoConducta = Entity.MotivoConducta
                    };

                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    odata.Database.CommandTimeout = 5000;
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_CalificacionesCualitativas.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdAnio == IdAnio && q.IdSede == IdSede).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo ?? 0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0,
                            NomMateria = q.NomMateria,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    odata.Database.CommandTimeout = 5000;
                    var lst = odata.vwaca_AnioLectivo_Paralelo_Profesor_CalificacionesCualitativas.Where(q => q.IdEmpresa == IdEmpresa
                    && q.IdAnio==IdAnio && q.IdSede==IdSede && q.IdProfesor == (EsSuperAdmin == true ? q.IdProfesor : IdProfesor)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            OrdenNivel = q.OrdenNivel ?? 0,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada ?? 0,
                            NomCurso = q.NomCurso,
                            OrdenCurso = q.OrdenCurso ?? 0,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo ?? 0,
                            CodigoParalelo = q.CodigoParalelo,
                            IdProfesorTutor = q.IdProfesorTutor ?? 0,
                            IdProfesorInspector = q.IdProfesorInspector ?? 0,
                            NomMateria = q.NomMateria,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool generarCalificacion(List<aca_MatriculaCalificacionCualitativa_Info> lst_parcial)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_matricula = (from q in lst_parcial
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdMatricula
                                         } into mat
                                         select new aca_Matricula_Info
                                         {
                                             IdEmpresa = mat.Key.IdEmpresa,
                                             IdMatricula = mat.Key.IdMatricula
                                         }).ToList();

                    foreach (var item in lst_matricula)
                    {
                        var lst_calificacion_cualitativa = Context.aca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacionCualitativa.RemoveRange(lst_calificacion_cualitativa);

                        var lst_x_matricula = lst_parcial.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();

                        if (lst_x_matricula != null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaCalificacionCualitativa Entity = new aca_MatriculaCalificacionCualitativa
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdCatalogoParcial = info.IdCatalogoParcial,
                                    IdProfesor = info.IdProfesor,
                                    IdCalificacionCualitativa = info.IdCalificacionCualitativa,
                                    Conducta = info.Conducta,
                                    MotivoConducta = info.MotivoConducta,
                                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                                    FechaCreacion = info.FechaCreacion,
                                    IdUsuarioModificacion = info.IdUsuarioModificacion,
                                    FechaModificacion = info.FechaModificacion
                                };

                                Context.aca_MatriculaCalificacionCualitativa.Add(Entity);
                            }
                        }
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool modificarDB(aca_MatriculaCalificacionCualitativa_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);

                    aca_MatriculaCalificacionCualitativa Entity = Context.aca_MatriculaCalificacionCualitativa.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula && q.IdMateria == info.IdMateria && q.IdProfesor == info.IdProfesor && q.IdCatalogoParcial == info.IdCatalogoParcial);
                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Entity.IdCalificacionCualitativa = info.IdCalificacionCualitativa;
                    Entity.Conducta = info.Conducta;
                    Entity.MotivoConducta = info.MotivoConducta;

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
