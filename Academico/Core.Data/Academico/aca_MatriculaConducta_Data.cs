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
                            PromedioP1 = q.PromedioP1,
                            PromedioP2 = q.PromedioP2,
                            PromedioP3 = q.PromedioP3,
                            PromedioP4 = q.PromedioP4,
                            PromedioP5 = q.PromedioP5,
                            PromedioP6 = q.PromedioP6,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            PromedioFinalP6 = q.PromedioFinalP6,
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

        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio==IdAnio && q.IdNivel==IdNivel && q.IdJornada==IdJornada && q.IdCurso==IdCurso && q.IdParalelo==IdParalelo).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            PromedioP1 = q.PromedioP1,
                            PromedioP2 = q.PromedioP2,
                            PromedioP3 = q.PromedioP3,
                            PromedioP4 = q.PromedioP4,
                            PromedioP5 = q.PromedioP5,
                            PromedioP6 = q.PromedioP6,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            PromedioFinalP6 = q.PromedioFinalP6,
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
                    foreach (var info in lst_conducta)
                    {
                        var lista_calificacion_conducta = Context.aca_MatriculaConducta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                        Context.aca_MatriculaConducta.RemoveRange(lista_calificacion_conducta);

                        aca_MatriculaConducta Entity = new aca_MatriculaConducta
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdMatricula = info.IdMatricula,
                            PromedioP1 = info.PromedioP1,
                            PromedioP2 = info.PromedioP2,
                            PromedioP3 = info.PromedioP3,
                            PromedioP4 = info.PromedioP4,
                            PromedioP5 = info.PromedioP5,
                            PromedioP6 = info.PromedioP6,
                            PromedioFinalP1 = info.PromedioFinalP1,
                            PromedioFinalP2 = info.PromedioFinalP2,
                            PromedioFinalP3 = info.PromedioFinalP3,
                            PromedioFinalP4 = info.PromedioFinalP4,
                            PromedioFinalP5 = info.PromedioFinalP5,
                            PromedioFinalP6 = info.PromedioFinalP6,
                            PromedioQ1 = info.PromedioQ1,
                            PromedioFinalQ1 = info.PromedioFinalQ1,
                            PromedioQ2 = info.PromedioQ2,
                            PromedioFinalQ2 = info.PromedioFinalQ2,
                            PromedioGeneral = info.PromedioGeneral,
                            PromedioFinal = info.PromedioFinal
                        };

                        Context.aca_MatriculaConducta.Add(Entity);
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
