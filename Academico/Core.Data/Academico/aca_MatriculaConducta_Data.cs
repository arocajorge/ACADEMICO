using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaConducta_Data
    {
        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            PromedioQ1 = q.PromedioQ1,
                            PromedioQ2 = q.PromedioQ2,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            PromedioFinal = q.PromedioFinal,
                            PromedioGeneral = q.PromedioGeneral
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

        public bool generarCalificacion(List<aca_MatriculaConducta_Info> lst_conducta)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_matricula = (from q in lst_conducta
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
                        var lista_calificacion_conducta = Context.aca_MatriculaConducta.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaConducta.RemoveRange(lista_calificacion_conducta);

                        var lst_x_matricula = lst_conducta.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();

                        if (lst_x_matricula != null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaConducta Entity = new aca_MatriculaConducta
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdProfesor = info.IdProfesor,
                                    CalificacionP1 = info.CalificacionP1,
                                    CalificacionP2 = info.CalificacionP2,
                                    CalificacionP3 = info.CalificacionP3,
                                    CalificacionP4 = info.CalificacionP4,
                                    CalificacionP5 = info.CalificacionP5,
                                    CalificacionP6 = info.CalificacionP6,
                                    PromedioQ1 = info.PromedioQ1,
                                    PromedioFinalQ1 = info.PromedioFinalQ1,
                                    PromedioQ2 = info.PromedioQ2,
                                    PromedioFinalQ2 = info.PromedioFinalQ2,
                                    PromedioGeneral = info.PromedioGeneral,
                                    PromedioFinal = info.PromedioFinal
                                };

                                Context.aca_MatriculaConducta.Add(Entity);
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
    }
}
