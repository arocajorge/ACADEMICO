﻿using Core.Data.Base;
using Core.Info.Academico;
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
                                    AccionRemedial = info.AccionRemedial
                                };

                                Context.aca_MatriculaCalificacionParcial.Add(Entity);
                            }
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