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
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal
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

        public List<aca_MatriculaConducta_Info> getList_PaseAnio(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin && q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin
                    && q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin && q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin
                    && q.IdAlumno >= IdAlumnoIni && q.IdAlumno <= IdAlumnoFin).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel=q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso=q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal
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
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal
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
                            SecuenciaPromedioP1 = info.SecuenciaPromedioP1,
                            PromedioP1 = info.PromedioP1,
                            SecuenciaPromedioFinalP1 = info.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = info.PromedioFinalP1,
                            SecuenciaPromedioP2 = info.SecuenciaPromedioP2,
                            PromedioP2 = info.PromedioP2,
                            SecuenciaPromedioFinalP2 = info.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = info.PromedioFinalP2,
                            SecuenciaPromedioP3 = info.SecuenciaPromedioP3,
                            PromedioP3 = info.PromedioP3,
                            SecuenciaPromedioFinalP3 = info.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = info.PromedioFinalP3,
                            SecuenciaPromedioP4 = info.SecuenciaPromedioP4,
                            PromedioP4 = info.PromedioP4,
                            SecuenciaPromedioFinalP4 = info.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = info.PromedioFinalP4,
                            SecuenciaPromedioP5 = info.SecuenciaPromedioP5,
                            PromedioP5 = info.PromedioP5,
                            SecuenciaPromedioFinalP5 = info.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = info.PromedioFinalP5,
                            SecuenciaPromedioP6 = info.SecuenciaPromedioP6,
                            PromedioP6 = info.PromedioP6,
                            SecuenciaPromedioFinalP6 = info.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = info.PromedioFinalP6,
                            SecuenciaPromedioQ1 = info.SecuenciaPromedioQ1,
                            PromedioQ1 = info.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = info.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = info.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = info.SecuenciaPromedioQ2,
                            PromedioQ2 = info.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = info.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = info.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = info.SecuenciaPromedioGeneral,
                            PromedioGeneral = info.PromedioGeneral,
                            SecuenciaPromedioFinal = info.SecuenciaPromedioFinal,
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
