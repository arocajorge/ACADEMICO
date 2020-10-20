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
    public class ACA_030_Data
    {
        public List<ACA_030_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcialTipo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_030_Info> Lista = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaFinal = new List<ACA_030_Info>();

                List<ACA_030_Info> ListaObligatorias = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaOptativas = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaProyectos = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaComportamiento = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaPromedioObligatorias = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaPromedioOptativas = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaPromedioProyectos = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaPromedioGeneral = new List<ACA_030_Info>();

                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_030(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados, IdCatalogoParcialTipo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_030_Info
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
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo??0,
                            OrdenMateria = q.OrdenMateria,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            NombreTutor = q.NombreTutor,
                            NombreInspector = q.NombreInspector,
                            Calificacion =q.Calificacion,
                            Columna=q.Columna,
                            OrdenColumna = q.OrdenColumna,
                            PromediarGrupo = q.PromediarGrupo??0,
                            IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion,
                            CalificacionNumerica = q.CalificacionNumerica
                        });
                    }
                }

                var ListaNulos = new List<ACA_030_Info>();
                var lstPromediosNull_Obligatorias = Lista.Where(q => q.Columna=="PF" && q.PromediarGrupo==0 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI) && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                }).Select(q => new ACA_030_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula
                }).ToList();

                var lstPromediosNull_Optativas = Lista.Where(q => q.Columna == "PF" && q.PromediarGrupo == 1 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI) && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                }).Select(q => new ACA_030_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula
                }).ToList();

                var lstPromediosNull_Proyectos = Lista.Where(q => q.Columna == "PF" && q.PromediarGrupo == 1 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUALI) && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                }).Select(q => new ACA_030_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula
                }).ToList();

                ListaObligatorias = Lista.Where(q=> q.PromediarGrupo == 0 && q.Columna!= "PENSUM" && q.Columna != "OPTATIVAS" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativas = Lista.Where(q => q.PromediarGrupo == 1 && q.Columna != "OPTATIVAS" && q.Columna != "PENSUM" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaProyectos = Lista.Where(q => q.PromediarGrupo == 0 && q.Columna != "PROYECTOS" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUALI)).ToList();
                ListaComportamiento = Lista.Where(q => q.PromediarGrupo == 0 && q.NomMateriaGrupo== "COMPORTAMIENTO" && q.IdCatalogoTipoCalificacion == null).ToList();
                ListaPromedioObligatorias = Lista.Where(q => q.PromediarGrupo == 0 && q.Columna == "PENSUM" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaPromedioOptativas = Lista.Where(q => q.PromediarGrupo == 1 && q.Columna == "OPTATIVAS" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaPromedioProyectos = Lista.Where(q => q.PromediarGrupo == 0 && q.Columna == "PROYECTOS" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUALI)).ToList();
                ListaPromedioGeneral= Lista.Where(q => q.PromediarGrupo == 0 && q.Columna == "P FINAL" && q.IdCatalogoTipoCalificacion == null).ToList();

                ListaFinal.AddRange(ListaObligatorias);

                var lstLeftJoin_PromediosObligatorias =
                  (from a in ListaPromedioObligatorias
                   join b in lstPromediosNull_Obligatorias on a.IdMatricula equals b.IdMatricula into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna== "PENSUM"
                   select new ACA_030_Info
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
                       OrdenMateriaGrupo = a.OrdenMateriaGrupo ?? 0,
                       OrdenMateria = a.OrdenMateria,
                       NomMateriaGrupo = a.NomMateriaGrupo,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn == null) ? a.CalificacionNumerica : null
                   }
                  ).ToList();

                ListaFinal.AddRange(lstLeftJoin_PromediosObligatorias);
                ListaFinal.AddRange(ListaOptativas);

                var lstLeftJoin_PromediosOptativas =
                  (from a in ListaPromedioOptativas
                   join b in lstPromediosNull_Optativas on a.IdMatricula equals b.IdMatricula into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna == "OPTATIVAS"
                   select new ACA_030_Info
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
                       OrdenMateriaGrupo = a.OrdenMateriaGrupo ?? 0,
                       OrdenMateria = a.OrdenMateria,
                       NomMateriaGrupo = a.NomMateriaGrupo,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn == null) ? a.CalificacionNumerica : null
                   }
                  ).ToList();

                ListaFinal.AddRange(lstLeftJoin_PromediosOptativas);
                ListaFinal.AddRange(ListaProyectos);

                var lstLeftJoin_PromediosProyectos =
                  (from a in ListaPromedioProyectos
                   join b in lstPromediosNull_Proyectos on a.IdMatricula equals b.IdMatricula into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna == "PROYECTOS"
                   select new ACA_030_Info
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
                       OrdenMateriaGrupo = a.OrdenMateriaGrupo ?? 0,
                       OrdenMateria = a.OrdenMateria,
                       NomMateriaGrupo = a.NomMateriaGrupo,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn == null) ? a.CalificacionNumerica : null
                   }
                  ).ToList();

                ListaFinal.AddRange(lstLeftJoin_PromediosProyectos);
                ListaFinal.AddRange(ListaPromedioGeneral);

                #region Calculo de Promedio General
                var ListaParaValidar = Lista.Where(q => q.Columna == "PF").ToList();
                int TipoCatalogoCuantitativo = Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI);
                var lstPromedioAgrupada = ListaParaValidar.Where(q => q.IdCatalogoTipoCalificacion == TipoCatalogoCuantitativo && q.PromediarGrupo == 1).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.NomMateriaGrupo
                }).Select(q => new ACA_030_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    NomMateriaGrupo = q.Key.NomMateriaGrupo,
                    CalificacionNumerica = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var lstPromedioMateriasNoAgrupada = ListaParaValidar.Where(q => q.IdCatalogoTipoCalificacion == TipoCatalogoCuantitativo && q.PromediarGrupo == 0).ToList();
                lstPromedioMateriasNoAgrupada.AddRange(lstPromedioAgrupada);

                var ListaPromedios = lstPromedioMateriasNoAgrupada.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_030_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    PromedioGeneralNumerico =Math.Round( Convert.ToDecimal(q.Average(g=>g.CalificacionNumerica)),2,MidpointRounding.AwayFromZero)
                }).ToList();


                ListaParaValidar.ForEach(q=> q.ExisteNulos = (q.Calificacion==null ? 1 : 0));
                var ListaAgrupadaValidar = ListaParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_030_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    ExisteNulos = q.Sum(g=> g.ExisteNulos)
                }).ToList();

                var ListaMatriculaPromedio = (from a in ListaPromedios
                         join b in ListaAgrupadaValidar
                         on new { a.IdEmpresa, a.IdMatricula } equals new { b.IdEmpresa, b.IdMatricula }
                         select new ACA_030_Info
                         {
                             IdEmpresa = a.IdEmpresa,
                             IdMatricula=a.IdMatricula,
                             Calificacion = (b.ExisteNulos > 0 ? "" : Convert.ToString(a.PromedioGeneralNumerico)),
                             CalificacionNumerica = (b.ExisteNulos>0 ? (decimal?)null : a.PromedioGeneralNumerico)
                         }).ToList();

                foreach (var a in ListaMatriculaPromedio)
                {
                    foreach (var b in ListaFinal)
                    {
                        if (a.IdMatricula == b.IdMatricula)
                        {
                            if (b.Columna == "P FINAL")
                            {
                                b.Calificacion = a.Calificacion;
                                b.CalificacionNumerica = a.CalificacionNumerica;
                            }
                        }
                    }
                }
                
                /*
                var lstLeftJoin_PromediosGenerales =
                  (from a in ListaFinal
                   join b in ListaMatriculaPromedio on a.IdMatricula equals b.IdMatricula into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna == "P FINAL"
                   select new ACA_030_Info
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
                       OrdenMateriaGrupo = a.OrdenMateriaGrupo ?? 0,
                       OrdenMateria = a.OrdenMateria,
                       NomMateriaGrupo = a.NomMateriaGrupo,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn == null) ? a.CalificacionNumerica : null
                   }
                  ).ToList();
*/
                #endregion

                ListaFinal.AddRange(ListaComportamiento);

                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
