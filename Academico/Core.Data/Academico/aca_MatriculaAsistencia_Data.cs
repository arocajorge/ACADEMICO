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
    public class aca_MatriculaAsistencia_Data
    {
        public List<aca_MatriculaAsistencia_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                List<aca_MatriculaAsistencia_Info> Lista = new List<aca_MatriculaAsistencia_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaAsistencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio==IdAnio && q.IdNivel==IdNivel && q.IdJornada==IdJornada && q.IdCurso==IdCurso && q.IdParalelo==IdParalelo).OrderBy(q=>q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaAsistencia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            FJustificadaP1 = q.FJustificadaP1,
                            FInjustificadaP1 = q.FInjustificadaP1,
                            AtrasosP1 = q.AtrasosP1,
                            FJustificadaP2 = q.FJustificadaP2,
                            FInjustificadaP2 = q.FInjustificadaP2,
                            AtrasosP2 = q.AtrasosP2,
                            FInjustificadaP3 = q.FInjustificadaP3,
                            FJustificadaP3 = q.FJustificadaP3,
                            AtrasosP3 = q.AtrasosP3,
                            FJustificadaP4 = q.FJustificadaP4,
                            FInjustificadaP4 = q.FInjustificadaP4,
                            AtrasosP4 = q.AtrasosP4,
                            FJustificadaP5 = q.FJustificadaP5,
                            FInjustificadaP5 = q.FInjustificadaP5,
                            AtrasosP5 = q.AtrasosP5,
                            FJustificadaP6 = q.FJustificadaP6,
                            FInjustificadaP6 = q.FInjustificadaP6,
                            AtrasosP6 = q.AtrasosP6,
                            pe_nombreCompleto = q.pe_nombreCompleto
                        });
                    });
                }

                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public aca_MatriculaAsistencia_Info getInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaAsistencia_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_MatriculaAsistencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaAsistencia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        Codigo = Entity.Codigo,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        FJustificadaP1 = Entity.FJustificadaP1,
                        FInjustificadaP1 = Entity.FInjustificadaP1,
                        AtrasosP1 = Entity.AtrasosP1,
                        FJustificadaP2 = Entity.FJustificadaP2,
                        FInjustificadaP2 = Entity.FInjustificadaP2,
                        AtrasosP2 = Entity.AtrasosP2,
                        FInjustificadaP3 = Entity.FInjustificadaP3,
                        FJustificadaP3 = Entity.FJustificadaP3,
                        AtrasosP3 = Entity.AtrasosP3,
                        FJustificadaP4 = Entity.FJustificadaP4,
                        FInjustificadaP4 = Entity.FInjustificadaP4,
                        AtrasosP4 = Entity.AtrasosP4,
                        FJustificadaP5 = Entity.FJustificadaP5,
                        FInjustificadaP5 = Entity.FInjustificadaP5,
                        AtrasosP5 = Entity.AtrasosP5,
                        FJustificadaP6 = Entity.FJustificadaP6,
                        FInjustificadaP6 = Entity.FInjustificadaP6,
                        AtrasosP6 = Entity.AtrasosP6
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificar(aca_MatriculaAsistencia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaAsistencia EntityAsistencia = Context.aca_MatriculaAsistencia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityAsistencia == null)
                        return false;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        EntityAsistencia.FInjustificadaP1 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP1 = info.FJustificada;
                        EntityAsistencia.AtrasosP1 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        EntityAsistencia.FInjustificadaP2 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP2 = info.FJustificada;
                        EntityAsistencia.AtrasosP2 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        EntityAsistencia.FInjustificadaP3 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP3 = info.FJustificada;
                        EntityAsistencia.AtrasosP3 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        EntityAsistencia.FInjustificadaP4 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP4 = info.FJustificada;
                        EntityAsistencia.AtrasosP4 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        EntityAsistencia.FInjustificadaP5 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP5 = info.FJustificada;
                        EntityAsistencia.AtrasosP5 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        EntityAsistencia.FInjustificadaP6 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP6 = info.FJustificada;
                        EntityAsistencia.AtrasosP6 = info.Atrasos;
                    }

                    EntityAsistencia.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    EntityAsistencia.FechaModificacion = DateTime.Now;

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
