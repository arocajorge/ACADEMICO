using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_014_Data
    {
        public List<ACA_014_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_014_Info> Lista = new List<ACA_014_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_014(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_014_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomMateria = q.NomMateria,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                            OrdenMateriaArea = q.OrdenMateriaArea,
                            OrdenMateria = q.OrdenMateria,
                            EsObligatorio = q.EsObligatorio,
                            NomMateriaArea = q.NomMateriaArea,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2=q.CalificacionP2,
                            CalificacionP3=q.CalificacionP3,
                            ExamenQ1=q.ExamenQ1,
                            PorcentajePromedioQ1 = q.PorcentajePromedioQ1,
                            PorcentajeExamenQ1 =q.PorcentajeExamenQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            EquivalenciaPromedioP1 = q.EquivalenciaPromedioP1,
                            EquivalenciaPromedioP2 = q.EquivalenciaPromedioP2,
                            EquivalenciaPromedioP3 = q.EquivalenciaPromedioP3,
                            EquivalenciaPromedioEQ1 = q.EquivalenciaPromedioEQ1,
                            EquivalenciaPromedioQ1 = q.EquivalenciaPromedioQ1,
                            CalificacionP4 = (IdCatalogoParcial== Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)? q.CalificacionP4 : null),
                            CalificacionP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP5 :null),
                            CalificacionP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CalificacionP6 : null),
                            ExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenQ2 :null),
                            PorcentajePromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PorcentajePromedioQ2 : null),
                            PorcentajeExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PorcentajeExamenQ2 : null),
                            PromedioFinalQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinalQ2 : null),
                            ExamenSupletorio = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenSupletorio: null),
                            ExamenMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenMejoramiento : null),
                            ExamenGracia = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenGracia : null),
                            ExamenRemedial = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.ExamenRemedial:null),
                            CampoMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.CampoMejoramiento : null),
                            PromedioFinal = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioFinal :null),
                            EquivalenciaPromedioP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)? q.EquivalenciaPromedioP4 : ""),
                            EquivalenciaPromedioP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioP5 : ""),
                            EquivalenciaPromedioP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioP6 :""),
                            EquivalenciaPromedioEQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioEQ2 :""),
                            EquivalenciaPromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioQ2 : ""),
                            PromedioQuimestralFinal = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioQuimestralFinal : null),
                            EquivalenciaPromedioPF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioPF :""),
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF,
                            IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion,
                            NombreRepresentante = q.NombreRepresentante,
                            NombreInspector = q.NombreInspector
                        });
                    }
                }

                var ListaAgrupadaXMatricula = (from q in Lista
                                group q by new
                                {
                                    q.IdEmpresa,
                                    q.IdMatricula,
                                    q.IdMateria,
                                    q.IdCatalogoTipoCalificacion,
                                    q.PromedioFinalQ1,
                                    q.PromedioFinalQ2,
                                    q.PromedioQuimestralFinal,
                                    q.PromedioFinal
                                } into a
                                select new ACA_014_Info
                                {
                                    IdEmpresa = a.Key.IdEmpresa,
                                    IdMatricula = a.Key.IdMatricula,
                                    IdMateria = a.Key.IdMateria,
                                    IdCatalogoTipoCalificacion = a.Key.IdCatalogoTipoCalificacion,
                                    PromedioFinalQ1 = a.Key.PromedioFinalQ1,
                                    PromedioFinalQ2 = a.Key.PromedioFinalQ2,
                                    PromedioQuimestralFinal = a.Key.PromedioQuimestralFinal,
                                    PromedioFinal = a.Key.PromedioFinal,
                                }).ToList();

                var lst_cuantitativas = ListaAgrupadaXMatricula.Where(q => q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                decimal SumaPromedioQ1 = 0;
                decimal SumaPromedioQ2 = 0;
                decimal SumaPromedioQuim = 0;
                decimal SumaPromedioFinal = 0;
                var PromedioGeneralQ1 = "";
                var PromedioGeneralQ2 = "";
                var PromedioGeneralQuim = "";
                var PromedioGeneralFinal = "";
                var contQ1 = 0;
                var contQ2 = 0;
                var contQuim = 0;
                var contFinal = 0;

                foreach (var item in lst_cuantitativas)
                {
                    if (string.IsNullOrEmpty(item.PromedioFinalQ1))
                    {
                        contQ1++;
                    }

                    if (string.IsNullOrEmpty(item.PromedioFinalQ2))
                    {
                        contQ2++;
                    }

                    if (string.IsNullOrEmpty(item.PromedioQuimestralFinal))
                    {
                        contQuim++;
                    }

                    if (string.IsNullOrEmpty(item.PromedioFinal))
                    {
                        contFinal++;
                    }

                    item.CantQ1 = contQ1;
                    item.CantQ2 = contQ2;
                    item.CantQuim = contQuim;
                    item.CantFinal = contFinal;
                }

                foreach (var item in lst_cuantitativas)
                {
                    if (contQ1 == 0)
                    {
                        decimal CalculoQ1 = Math.Round(Convert.ToDecimal(SumaPromedioQ1 / lst_cuantitativas.Count()), 2, MidpointRounding.AwayFromZero);
                        PromedioGeneralQ1 = Convert.ToString(CalculoQ1);
                    }

                    if (contQ2 == 0)
                    {
                        decimal CalculoQ2 = Math.Round(Convert.ToDecimal(SumaPromedioQ2 / lst_cuantitativas.Count()), 2, MidpointRounding.AwayFromZero);
                        PromedioGeneralQ2 = Convert.ToString(CalculoQ2);
                    }

                    if (contQuim == 0)
                    {
                        decimal CalculoQuim = Math.Round(Convert.ToDecimal(SumaPromedioQ2 / lst_cuantitativas.Count()), 2, MidpointRounding.AwayFromZero);
                        PromedioGeneralQuim = Convert.ToString(CalculoQuim);
                    }

                    if (contFinal == 0)
                    {
                        decimal CalculoFinal = Math.Round(Convert.ToDecimal(SumaPromedioQ1 / lst_cuantitativas.Count()), 2, MidpointRounding.AwayFromZero);
                        PromedioGeneralFinal = Convert.ToString(CalculoFinal);
                    }

                    Lista.ForEach(q => { q.PromedioGeneralQ1 = PromedioGeneralQ1; q.PromedioGeneralQ2 = PromedioGeneralQ2; q.PromedioGeneralQuim = PromedioGeneralQuim; q.PromedioGeneralFinal = PromedioGeneralFinal; });
                }


                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ACA_014_EquivalenciaCualitativa_Info> get_list_EquivalenciaCualitativa(int IdEmpresa, int IdAnio)
        {
            try
            {

                List<ACA_014_EquivalenciaCualitativa_Info> Lista = new List<ACA_014_EquivalenciaCualitativa_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.aca_AnioLectivoCalificacionCualitativa.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_014_EquivalenciaCualitativa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Codigo = q.Codigo,
                            IdCalificacionCualitativa = q.IdCalificacionCualitativa,
                            DescripcionCorta = q.DescripcionCorta,
                            DescripcionLarga = q.DescripcionLarga
                        });
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ACA_014_EquivalenciaConducta_Info> get_list_EquivalenciaConducta(int IdEmpresa, int IdAnio)
        {
            try
            {

                List<ACA_014_EquivalenciaConducta_Info> Lista = new List<ACA_014_EquivalenciaConducta_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.aca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_014_EquivalenciaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Letra = q.Letra,
                            Equivalencia = q.Equivalencia,
                            DescripcionEquivalencia = q.DescripcionEquivalencia
                        });
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
