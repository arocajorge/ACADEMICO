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
                            PromediarGrupo = q.PromediarGrupo,
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
                            PromedioQuimestres_PF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.PromedioQuimestres_PF : null),
                            Promedio_PR = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.Promedio_PR : null),
                            EquivalenciaPromedioPF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? q.EquivalenciaPromedioPF :""),
                            IdEquivalenciaPromedioPF = q.IdEquivalenciaPromedioPF,
                            IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion,
                            NombreRepresentante = q.NombreRepresentante,
                            NombreInspector = q.NombreInspector,
                            //PromedioGrupoQ1Double = (q.IdCatalogoTipoCalificacion== Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)) ? Convert.ToDecimal(q.PromedioFinalQ1) : (decimal?)null,
                            //PromedioGrupoQ2Double = (q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)) ? Convert.ToDecimal(q.PromedioFinalQ2) : (decimal?)null,
                            //PromedioQuimestresGrupoDouble = (q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)) ? Convert.ToDecimal(q.PromedioQuimestres_PF) : (decimal?)null,
                            //PromedioFinalGrupoDouble = (q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)) ? Convert.ToDecimal(q.PromedioFinal) : (decimal?)null
                        });
                    }
                }

                #region Agrupar por "Promedio agrupado"
                int TipoCatalogoCuantitativo = Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI);
                var lstAgrupada = Lista.Where(q => q.IdCatalogoTipoCalificacion == TipoCatalogoCuantitativo && q.PromediarGrupo == true).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.NomMateriaGrupo
                }).Select(q=> new ACA_014_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    NomMateriaGrupo = q.Key.NomMateriaGrupo,
                    PromedioFinalQ1Double = q.Max(g => g.PromedioFinalQ1) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinalQ1)) / q.Count(g=> !string.IsNullOrEmpty(g.PromedioFinalQ1)),
                    PromedioFinalQ2Double = q.Max(g => g.PromedioFinalQ2) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinalQ2)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinalQ2)),
                    PromedioQuimestresDouble = q.Max(g => g.PromedioQuimestres_PF) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioQuimestres_PF)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioQuimestres_PF)),
                    PromedioFinalDouble = q.Max(g => g.PromedioFinal) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinal)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinal)),
                    PromedioGrupoQ1Double = q.Max(g => g.PromedioFinalQ1) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinalQ1)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinalQ1)),
                    PromedioGrupoQ2Double = q.Max(g => g.PromedioFinalQ2) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinalQ2)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinalQ2)),
                    PromedioQuimestresGrupoDouble = q.Max(g => g.PromedioQuimestres_PF) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioQuimestres_PF)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioQuimestres_PF)),
                    PromedioFinalGrupoDouble = q.Max(g => g.PromedioFinal) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinal)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinal))
                }).ToList();
                #endregion

                var lstPromedioMateriasNoAgrupada = Lista.Where(q => q.IdCatalogoTipoCalificacion == TipoCatalogoCuantitativo && (q.PromediarGrupo ?? false) == false).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.NomMateriaGrupo
                }).Select(q => new ACA_014_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    NomMateriaGrupo = q.Key.NomMateriaGrupo,
                    //PromedioGrupoQ1Double = null,
                    //PromedioGrupoQ2Double = null,
                    //PromedioQuimestresGrupoDouble = null,
                    //PromedioFinalGrupoDouble = null
                    PromedioGrupoQ1Double = q.Max(g => g.PromedioFinalQ1) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinalQ1)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinalQ1)),
                    PromedioGrupoQ2Double = q.Max(g => g.PromedioFinalQ2) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinalQ2)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinalQ2)),
                    PromedioQuimestresGrupoDouble = q.Max(g => g.PromedioQuimestres_PF) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioQuimestres_PF)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioQuimestres_PF)),
                    PromedioFinalGrupoDouble = q.Max(g => g.PromedioFinal) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.PromedioFinal)) / q.Count(g => !string.IsNullOrEmpty(g.PromedioFinal))

                }).ToList();
                lstPromedioMateriasNoAgrupada.AddRange(lstAgrupada);

                Lista = (from a in Lista
                         join b in lstPromedioMateriasNoAgrupada
                         on new { a.IdEmpresa, a.IdMatricula, a.NomMateriaGrupo } equals new { b.IdEmpresa, b.IdMatricula, b.NomMateriaGrupo }
                         select new ACA_014_Info
                         {
                             IdEmpresa = a.IdEmpresa,
                             IdMatricula = a.IdMatricula,
                             IdMateria = a.IdMateria,
                             IdAlumno = a.IdAlumno,
                             pe_nombreCompleto = a.pe_nombreCompleto,
                             Codigo = a.Codigo,
                             IdAnio = a.IdAnio,
                             IdSede = a.IdSede,
                             IdNivel = a.IdNivel,
                             IdJornada = a.IdJornada,
                             IdCurso = a.IdCurso,
                             IdParalelo = a.IdParalelo,
                             CodigoParalelo = a.CodigoParalelo,
                             Descripcion = a.Descripcion,
                             NomSede = a.NomSede,
                             NomNivel = a.NomNivel,
                             NomJornada = a.NomJornada,
                             NomMateria = a.NomMateria,
                             NomCurso = a.NomCurso,
                             NomParalelo = a.NomParalelo,
                             OrdenNivel = a.OrdenNivel,
                             OrdenJornada = a.OrdenJornada,
                             OrdenCurso = a.OrdenCurso,
                             OrdenParalelo = a.OrdenParalelo,
                             OrdenMateriaGrupo = a.OrdenMateriaGrupo,
                             OrdenMateriaArea = a.OrdenMateriaArea,
                             OrdenMateria = a.OrdenMateria,
                             PromediarGrupo = a.PromediarGrupo,
                             EsObligatorio = a.EsObligatorio,
                             NomMateriaArea = a.NomMateriaArea,
                             NomMateriaGrupo = a.NomMateriaGrupo,
                             CalificacionP1 = a.CalificacionP1,
                             CalificacionP2 = a.CalificacionP2,
                             CalificacionP3 = a.CalificacionP3,
                             ExamenQ1 = a.ExamenQ1,
                             PorcentajePromedioQ1 = a.PorcentajePromedioQ1,
                             PorcentajeExamenQ1 = a.PorcentajeExamenQ1,
                             PromedioFinalQ1 = a.PromedioFinalQ1,
                             EquivalenciaPromedioP1 = a.EquivalenciaPromedioP1,
                             EquivalenciaPromedioP2 = a.EquivalenciaPromedioP2,
                             EquivalenciaPromedioP3 = a.EquivalenciaPromedioP3,
                             EquivalenciaPromedioEQ1 = a.EquivalenciaPromedioEQ1,
                             EquivalenciaPromedioQ1 = a.EquivalenciaPromedioQ1,
                             CalificacionP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CalificacionP4 : null),
                             CalificacionP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CalificacionP5 : null),
                             CalificacionP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CalificacionP6 : null),
                             ExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenQ2 : null),
                             PorcentajePromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PorcentajePromedioQ2 : null),
                             PorcentajeExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PorcentajeExamenQ2 : null),
                             PromedioFinalQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PromedioFinalQ2 : null),
                             ExamenSupletorio = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenSupletorio : null),
                             ExamenMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenMejoramiento : null),
                             ExamenGracia = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenGracia : null),
                             ExamenRemedial = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenRemedial : null),
                             CampoMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CampoMejoramiento : null),
                             PromedioFinal = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PromedioFinal : null),
                             EquivalenciaPromedioP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioP4 : ""),
                             EquivalenciaPromedioP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioP5 : ""),
                             EquivalenciaPromedioP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioP6 : ""),
                             EquivalenciaPromedioEQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioEQ2 : ""),
                             EquivalenciaPromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioQ2 : ""),
                             PromedioQuimestres_PF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PromedioQuimestres_PF : null),
                             Promedio_PR = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.Promedio_PR : ""),
                             EquivalenciaPromedioPF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioPF : ""),
                             IdEquivalenciaPromedioPF = a.IdEquivalenciaPromedioPF,
                             IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                             NombreRepresentante = a.NombreRepresentante,
                             NombreInspector = a.NombreInspector,

                             PromedioGrupoQ1Double = b.PromedioGrupoQ1Double == null ? (decimal?)null : Math.Round(b.PromedioGrupoQ1Double ?? 0, 2, MidpointRounding.AwayFromZero),
                             PromedioGrupoQ2Double = b.PromedioGrupoQ2Double == null ? (decimal?)null : Math.Round(b.PromedioGrupoQ2Double ?? 0, 2, MidpointRounding.AwayFromZero),
                             PromedioQuimestresGrupoDouble = b.PromedioQuimestresGrupoDouble == null ? (decimal?)null : Math.Round(b.PromedioQuimestresGrupoDouble ?? 0, 2, MidpointRounding.AwayFromZero),
                             PromedioFinalGrupoDouble = b.PromedioFinalGrupoDouble == null ? (decimal?)null : Math.Round(b.PromedioFinalGrupoDouble ?? 0, 2, MidpointRounding.AwayFromZero)
                         }).ToList();


                var lstMateriasNoAgrupada = Lista.Where(q => q.IdCatalogoTipoCalificacion == TipoCatalogoCuantitativo && (q.PromediarGrupo ?? false) == false).Select(q => new ACA_014_Info
                {
                    IdEmpresa = q.IdEmpresa,
                    IdMatricula = q.IdMatricula,
                    PromedioFinalQ1Double = q.PromedioFinalQ1 == null ? (decimal?)null : Convert.ToDecimal(q.PromedioFinalQ1),
                    PromedioFinalQ2Double = q.PromedioFinalQ2 == null ? (decimal?)null : Convert.ToDecimal(q.PromedioFinalQ2),
                    PromedioQuimestresDouble = q.PromedioQuimestres_PF == null ? (decimal?)null : Convert.ToDecimal(q.PromedioQuimestres_PF),
                    PromedioFinalDouble = q.PromedioFinal == null ? (decimal?)null : Convert.ToDecimal(q.PromedioFinal),
                }).ToList();
                lstMateriasNoAgrupada.AddRange(lstAgrupada);

                var lstFinal = lstMateriasNoAgrupada.GroupBy(q => new { q.IdEmpresa, q.IdMatricula }).Select(q => new ACA_014_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    PromedioFinalQ1Double = q.Max(g=> g.PromedioFinalQ1Double) == null ? (decimal?)null : (q.Sum(g=> g.PromedioFinalQ1Double) / q.Count(g => g.PromedioFinalQ1Double != null)),
                    PromedioFinalQ2Double = q.Max(g => g.PromedioFinalQ2Double) == null ? (decimal?)null : (q.Sum(g => g.PromedioFinalQ2Double) / q.Count(g => g.PromedioFinalQ2Double != null)),
                    PromedioQuimestresDouble = q.Max(g => g.PromedioQuimestresDouble) == null ? (decimal?)null : (q.Sum(g => g.PromedioQuimestresDouble) / q.Count(g => g.PromedioQuimestresDouble != null)),
                    PromedioFinalDouble = q.Max(g => g.PromedioFinalDouble) == null ? (decimal?)null : (q.Sum(g => g.PromedioFinalDouble) / q.Count(g => g.PromedioFinalDouble != null)),
                }).ToList();

                Lista = (from a in Lista
                         join b in lstFinal
                         on a.IdMatricula equals b.IdMatricula
                         select new ACA_014_Info
                         {
                             IdEmpresa = a.IdEmpresa,
                             IdMatricula = a.IdMatricula,
                             IdMateria = a.IdMateria,
                             IdAlumno = a.IdAlumno,
                             pe_nombreCompleto = a.pe_nombreCompleto,
                             Codigo = a.Codigo,
                             IdAnio = a.IdAnio,
                             IdSede = a.IdSede,
                             IdNivel = a.IdNivel,
                             IdJornada = a.IdJornada,
                             IdCurso = a.IdCurso,
                             IdParalelo = a.IdParalelo,
                             CodigoParalelo = a.CodigoParalelo,
                             Descripcion = a.Descripcion,
                             NomSede = a.NomSede,
                             NomNivel = a.NomNivel,
                             NomJornada = a.NomJornada,
                             NomMateria = a.NomMateria,
                             NomCurso = a.NomCurso,
                             NomParalelo = a.NomParalelo,
                             OrdenNivel = a.OrdenNivel,
                             OrdenJornada = a.OrdenJornada,
                             OrdenCurso = a.OrdenCurso,
                             OrdenParalelo = a.OrdenParalelo,
                             OrdenMateriaGrupo = a.OrdenMateriaGrupo,
                             OrdenMateriaArea = a.OrdenMateriaArea,
                             OrdenMateria = a.OrdenMateria,
                             PromediarGrupo = a.PromediarGrupo,
                             EsObligatorio = a.EsObligatorio,
                             NomMateriaArea = a.NomMateriaArea,
                             NomMateriaGrupo = a.NomMateriaGrupo,
                             CalificacionP1 = a.CalificacionP1,
                             CalificacionP2 = a.CalificacionP2,
                             CalificacionP3 = a.CalificacionP3,
                             ExamenQ1 = a.ExamenQ1,
                             PorcentajePromedioQ1 = a.PorcentajePromedioQ1,
                             PorcentajeExamenQ1 = a.PorcentajeExamenQ1,
                             PromedioFinalQ1 = a.PromedioFinalQ1,
                             EquivalenciaPromedioP1 = a.EquivalenciaPromedioP1,
                             EquivalenciaPromedioP2 = a.EquivalenciaPromedioP2,
                             EquivalenciaPromedioP3 = a.EquivalenciaPromedioP3,
                             EquivalenciaPromedioEQ1 = a.EquivalenciaPromedioEQ1,
                             EquivalenciaPromedioQ1 = a.EquivalenciaPromedioQ1,
                             CalificacionP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CalificacionP4 : null),
                             CalificacionP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CalificacionP5 : null),
                             CalificacionP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CalificacionP6 : null),
                             ExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenQ2 : null),
                             PorcentajePromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PorcentajePromedioQ2 : null),
                             PorcentajeExamenQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PorcentajeExamenQ2 : null),
                             PromedioFinalQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PromedioFinalQ2 : null),
                             ExamenSupletorio = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenSupletorio : null),
                             ExamenMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenMejoramiento : null),
                             ExamenGracia = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenGracia : null),
                             ExamenRemedial = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.ExamenRemedial : null),
                             CampoMejoramiento = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.CampoMejoramiento : null),
                             PromedioFinal = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PromedioFinal : null),
                             EquivalenciaPromedioP4 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioP4 : ""),
                             EquivalenciaPromedioP5 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioP5 : ""),
                             EquivalenciaPromedioP6 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioP6 : ""),
                             EquivalenciaPromedioEQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioEQ2 : ""),
                             EquivalenciaPromedioQ2 = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioQ2 : ""),
                             PromedioQuimestres_PF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.PromedioQuimestres_PF : ""),
                             Promedio_PR = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.Promedio_PR : ""),
                             EquivalenciaPromedioPF = (IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? a.EquivalenciaPromedioPF : ""),
                             IdEquivalenciaPromedioPF = a.IdEquivalenciaPromedioPF,
                             IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                             NombreRepresentante = a.NombreRepresentante,
                             NombreInspector = a.NombreInspector,

                             PromedioGrupoQ1Double = a.PromedioGrupoQ1Double,
                             PromedioGrupoQ2Double = a.PromedioGrupoQ2Double,
                             PromedioQuimestresGrupoDouble = a.PromedioQuimestresGrupoDouble,
                             PromedioFinalGrupoDouble = a.PromedioFinalGrupoDouble,

                             PFQ1 = b.PromedioFinalQ1Double == null ? (decimal?)null : Math.Round(b.PromedioFinalQ1Double ?? 0,2,MidpointRounding.AwayFromZero),
                             PFQ2 = b.PromedioFinalQ2Double == null ? (decimal?)null : Math.Round(b.PromedioFinalQ2Double ?? 0, 2, MidpointRounding.AwayFromZero),
                             PFQuim = b.PromedioQuimestresDouble == null ? (decimal?)null : Math.Round(b.PromedioQuimestresDouble ?? 0, 2, MidpointRounding.AwayFromZero),
                             PF = b.PromedioFinalDouble == null ? (decimal?)null : Math.Round(b.PromedioFinalDouble ?? 0, 2, MidpointRounding.AwayFromZero)
                         }).ToList();

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
