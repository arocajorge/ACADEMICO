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
    public class aca_MatriculaCalificacionParcial_Data
    {
        public List<aca_MatriculaCalificacionParcial_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            IdProfesor = q.IdProfesor,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial
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

        public List<aca_MatriculaCalificacionParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdCatalogoParcial == IdCatalogoParcial).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial
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

        public List<aca_MatriculaCalificacionParcial_Info> getList_x_Parcial(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula
                    && q.IdCatalogoParcial == IdCatalogoParcial).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial
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

        public bool generarCalificacion(List<aca_MatriculaCalificacionParcial_Info> lst_parcial)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

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
                        var lst_calificacion_parcial = Context.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacionParcial.RemoveRange(lst_calificacion_parcial);

                        var lst_x_matricula = lst_parcial.Where(q=> q.IdEmpresa == item.IdEmpresa && q.IdMatricula ==item.IdMatricula).ToList();

                        if (lst_x_matricula!=null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaCalificacionParcial Entity = new aca_MatriculaCalificacionParcial
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdCatalogoParcial = info.IdCatalogoParcial,
                                    IdProfesor = info.IdProfesor,
                                    Calificacion1 = info.Calificacion1,
                                    Calificacion2 = info.Calificacion2,
                                    Calificacion3 = info.Calificacion3,
                                    Calificacion4 = info.Calificacion4,
                                    Remedial1 = info.Remedial1,
                                    Remedial2 = info.Remedial2,
                                    Evaluacion = info.Evaluacion,
                                    Conducta = info.Conducta,
                                    MotivoCalificacion = info.MotivoCalificacion,
                                    MotivoConducta = info.MotivoConducta,
                                    AccionRemedial = info.AccionRemedial,
                                    IdUsuarioCreacion= info.IdUsuarioCreacion,
                                    FechaCreacion = DateTime.Now
                                };

                                Context.aca_MatriculaCalificacionParcial.Add(Entity);
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

        public bool modificarDB(aca_MatriculaCalificacionParcial_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaCalificacionParcial Entity = Context.aca_MatriculaCalificacionParcial.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdMatricula == info.IdMatricula && q.IdMateria== info.IdMateria && q.IdProfesor== info.IdProfesor && q.IdCatalogoParcial == info.IdCatalogoParcial);
                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Entity.Calificacion1 = info.Calificacion1;
                    Entity.Calificacion2 = info.Calificacion2;
                    Entity.Calificacion3 = info.Calificacion3;
                    Entity.Calificacion4 = info.Calificacion4;
                    Entity.Remedial1 = info.Remedial1;
                    Entity.Remedial2 = info.Remedial2;
                    Entity.Evaluacion = info.Evaluacion;
                    Entity.Conducta = info.Conducta;
                    Entity.MotivoCalificacion = info.MotivoCalificacion;
                    Entity.MotivoConducta = info.MotivoConducta;
                    Entity.AccionRemedial = info.AccionRemedial;

                    aca_MatriculaCalificacion EntityCalificacion = Context.aca_MatriculaCalificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdMatricula == info.IdMatricula && q.IdProfesor == info.IdProfesor && q.IdMateria == info.IdMateria);
                    if (EntityCalificacion == null)
                        return false;
                    
                    if(info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                        EntityCalificacion.CalificacionP1 = info.PromedioParcial;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                        EntityCalificacion.CalificacionP2 = info.PromedioParcial;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                        EntityCalificacion.CalificacionP3 = info.PromedioParcial;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                        EntityCalificacion.CalificacionP4 = info.PromedioParcial;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                        EntityCalificacion.CalificacionP5 = info.PromedioParcial;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                        EntityCalificacion.CalificacionP6 = info.PromedioParcial;

                    Context.SaveChanges();

                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    var lst_calificaciones_parciales = getList_x_Parcial(info.IdEmpresa, info.IdMatricula, info.IdCatalogoParcial);
                    decimal PromedioParcialConducta = Convert.ToDecimal(lst_calificaciones_parciales.Average(q=>q.Conducta));

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                        EntityConducta.PromedioP1 = PromedioParcialConducta;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                        EntityConducta.PromedioP2 = PromedioParcialConducta;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                        EntityConducta.PromedioP3 = PromedioParcialConducta;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                        EntityConducta.PromedioP4 = PromedioParcialConducta;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                        EntityConducta.PromedioP5 = PromedioParcialConducta;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                        EntityConducta.PromedioP6 = PromedioParcialConducta;
                    

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
