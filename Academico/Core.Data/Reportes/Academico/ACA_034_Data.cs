using Core.Data.Academico;
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
    public class ACA_034_Data
    {
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        public List<ACA_034_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_034_Info> Lista = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaFinal = new List<ACA_034_Info>();

                List<ACA_034_Info> ListaInicial = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaObligatorias = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaOptativas = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaOptativasIndividuales = new List<ACA_034_Info>();

                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_034(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_034_Info
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
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateria = q.OrdenMateria,
                            Calificacion = q.Calificacion,
                            CalificacionNumerica = q.CalificacionNumerica,
                            IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion,
                            Columna = q.Columna,
                            NombreGrupo = q.NombreGrupo,
                            NombreMateria = q.NombreMateria,
                            OrdenColumna = q.OrdenColumna,
                            OrdenGrupo = q.OrdenGrupo,
                            PromediarGrupo = q.PromediarGrupo
                        });
                    }
                }

                ListaInicial = Lista.Where(q => q.IdMateria == 0 ).ToList();
                ListaFinal.AddRange(ListaInicial);

                ListaObligatorias = Lista.Where(q => q.PromediarGrupo == 0 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativasIndividuales = Lista.Where(q => q.PromediarGrupo == 1 && q.IdMateria != null && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativas= Lista.Where(q => q.PromediarGrupo == 1 && q.IdMateria == null && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();

                var lstPromediosNull_Obligatorias = ListaObligatorias.Where(q => (q.Columna == "I QUIMESTRE" || q.Columna == "II QUIMESTRE") && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdMateria
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdMateria = q.Key.IdMateria
                }).ToList();

                ListaFinal.AddRange(ListaObligatorias.Where(q=>q.Columna!= "PROMEDIO FINAL"));

                var lstLeftJoin_PromediosObligatorias =
                  (from a in ListaObligatorias
                   join b in lstPromediosNull_Obligatorias on new { a.IdMatricula, a.IdMateria } equals new { b.IdMatricula, b.IdMateria } into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna == "PROMEDIO FINAL"
                   select new ACA_034_Info
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
                       NomCurso = a.NomCurso,
                       NomParalelo = a.NomParalelo,
                       OrdenNivel = a.OrdenNivel,
                       OrdenJornada = a.OrdenJornada,
                       OrdenCurso = a.OrdenCurso,
                       OrdenParalelo = a.OrdenParalelo,
                       OrdenMateria = a.OrdenMateria,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn != null) ? a.CalificacionNumerica : null,
                       NombreGrupo=a.NombreGrupo,
                       NombreMateria=a.NombreMateria,
                       OrdenGrupo=a.OrdenGrupo
                   }
                  ).ToList();

                ListaFinal.AddRange(lstLeftJoin_PromediosObligatorias);

                ListaFinal.AddRange(ListaOptativasIndividuales);
                var Lista_Validar_PromediosOptativa = new List<ACA_034_Info>();

                var IQuim_ParaValidar = new List<ACA_034_Info>();
                IQuim_ParaValidar = ListaOptativasIndividuales.Where(q => q.Columna == "I QUIMESTRE").ToList();
                IQuim_ParaValidar.ForEach(q=>q.OptativasNulas = (q.Calificacion==null ? 1 : 0));

                var lstPromedio_IQuimestre = IQuim_ParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var IQuim_Optativa = ListaOptativas.Where(q => q.Columna == "I QUIMESTRE").ToList();

                foreach (var item in lstPromedio_IQuimestre)
                {
                    var Promedio = (decimal?)null;
                    if (item.OptativasNulas==0)
                    {
                        Promedio = Math.Round(Convert.ToDecimal(item.PromedioCalculado),2,MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Promedio = (decimal?)null;
                    }

                    IQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = Convert.ToString(Promedio);
                    IQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica = Promedio;
                }

                
                ListaFinal.AddRange(IQuim_Optativa);
                Lista_Validar_PromediosOptativa.AddRange(IQuim_Optativa);

                var IIQuim_ParaValidar = new List<ACA_034_Info>();
                IQuim_ParaValidar = ListaOptativasIndividuales.Where(q => q.Columna == "II QUIMESTRE").ToList();
                IQuim_ParaValidar.ForEach(q => q.OptativasNulas = (q.Calificacion == null ? 1 : 0));

                var lstPromedio_IIQuimestre = IQuim_ParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var IIQuim_Optativa = ListaOptativas.Where(q => q.Columna == "II QUIMESTRE").ToList();

                foreach (var item in lstPromedio_IIQuimestre)
                {
                    var Promedio = (decimal?)null;
                    if (item.OptativasNulas == 0)
                    {
                        Promedio = Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Promedio = (decimal?)null;
                    }

                    IIQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = Convert.ToString(Promedio);
                    IIQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica = Promedio;
                }


                ListaFinal.AddRange(IIQuim_Optativa);
                Lista_Validar_PromediosOptativa.AddRange(IIQuim_Optativa);

                var Lista_CalificacionesQuimestrales = new List<ACA_034_Info>();
                Lista_CalificacionesQuimestrales.AddRange(IQuim_Optativa);
                Lista_CalificacionesQuimestrales.AddRange(IIQuim_Optativa);

                var PromedioQuim_ParaValidar = new List<ACA_034_Info>();
                PromedioQuim_ParaValidar = Lista_CalificacionesQuimestrales;
                PromedioQuim_ParaValidar.ForEach(q => { q.Calificacion = (string.IsNullOrEmpty(q.Calificacion) ? null : q.Calificacion); q.OptativasNulas = (string.IsNullOrEmpty(q.Calificacion) ? 1 : 0); });

                var lstPromedio_Promedio = PromedioQuim_ParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var PromedioQuim = ListaOptativas.Where(q => q.Columna == "PROMEDIO").ToList();

                foreach (var item in lstPromedio_Promedio)
                {
                    var Promedio = (decimal?)null;
                    if (item.OptativasNulas == 0)
                    {
                        Promedio = Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Promedio = (decimal?)null;
                    }

                    PromedioQuim.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = Convert.ToString(Promedio);
                    PromedioQuim.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica= Promedio;
                }

                ListaFinal.AddRange(PromedioQuim);
                Lista_Validar_PromediosOptativa.AddRange(PromedioQuim);

                var Lista_Validar_Promedios = Lista_Validar_PromediosOptativa.Where(q => q.Columna != "PROMEDIO").ToList();
                Lista_Validar_Promedios.ForEach(q => q.OptativasNulas = (q.Calificacion == null ? 1 : 0));
                var info_anio = odata_anio.getInfo(IdEmpresa, IdAnio);
                if (info_anio != null)
                {
                    Lista_Validar_Promedios.ForEach(q => q.MuestraSupletorio = (string.IsNullOrEmpty(q.Calificacion) ? 0 : (Convert.ToDecimal(q.CalificacionNumerica) < Convert.ToDecimal(info_anio.PromedioMinimoPromocion) ? 1 : 0)));
                }

                var Lista_Agrupada_Validar_PromediosOptativa = Lista_Validar_Promedios.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    MuestraSupletorio = q.Sum(g => g.MuestraSupletorio),
                 }).ToList();

                var Supletorio = new List<ACA_034_Info>();
                Supletorio = ListaOptativasIndividuales.Where(q => q.Columna == "SUPLETORIO").ToList();
                Supletorio.ForEach(q => q.OptativasNulas = (q.Calificacion == null ? 1 : 0));

                var lsT_Supletorio = Supletorio.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    SupletorioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                //para agregar a la lista final
                var Lista_Supletorio_Optativa = ListaOptativas.Where(q => q.Columna == "SUPLETORIO").ToList();
                foreach (var item in Lista_Supletorio_Optativa)
                {
                    var PromedioSupletorio = (decimal?)null;

                    var Muestra_Supletorio = 0;
                    Muestra_Supletorio = Lista_Agrupada_Validar_PromediosOptativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().MuestraSupletorio;

                    if (Muestra_Supletorio > 0)
                    {
                        var OptativasNulas = 0;
                        OptativasNulas = lsT_Supletorio.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().OptativasNulas;
                        PromedioSupletorio = (OptativasNulas == 0 ? lsT_Supletorio.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().SupletorioCalculado : (decimal?)null);
                    }

                    item.CalificacionNumerica = PromedioSupletorio;
                    item.Calificacion = Convert.ToString(PromedioSupletorio) == "" ? null : Convert.ToString(PromedioSupletorio);
                }

                foreach (var item in Lista_Supletorio_Optativa)
                {
                    Lista_Supletorio_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = item.Calificacion;
                    Lista_Supletorio_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica = item.CalificacionNumerica;
                }

                ListaFinal.AddRange(Lista_Supletorio_Optativa);
                Lista_Validar_PromediosOptativa.AddRange(Lista_Supletorio_Optativa);

                //para agregar a la lista final
                var Lista_PromedioFinal_Optativa = ListaOptativas.Where(q => q.Columna == "PROMEDIO FINAL").ToList();

                foreach (var item in Lista_PromedioFinal_Optativa)
                {
                    //var PromedioFinal = (decimal?)null;
                    var SupletorioOptativa = Lista_Validar_PromediosOptativa.Where(q => q.Columna == "SUPLETORIO" && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica;
                    var PromedioFinal = Lista_Validar_PromediosOptativa.Where(q => q.Columna == "PROMEDIO" && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica;
                    if (SupletorioOptativa==null)
                    {
                        item.Calificacion = Convert.ToString(PromedioFinal);
                        item.CalificacionNumerica = PromedioFinal;
                    }
                    else
                    {
                        item.Calificacion = (info_anio == null ? null : Convert.ToDecimal(info_anio.PromedioMinimoPromocion).ToString("n2"));
                        item.CalificacionNumerica = (info_anio == null ? (decimal?)null : Convert.ToDecimal(info_anio.PromedioMinimoPromocion));
                    }
                }

                ListaFinal.AddRange(Lista_PromedioFinal_Optativa);

                var ListaPromedioGeneral = new List<ACA_034_Info>();
                var ListaPromedioGeneral_Agregar = new List<ACA_034_Info>();
                ListaPromedioGeneral = ListaFinal.Where(q => q.Columna == "PROMEDIO FINAL").ToList();
                ListaPromedioGeneral.ForEach(q => q.OptativasNulas = (string.IsNullOrEmpty(q.Calificacion) ? 1 : 0));
                var ListaPromedioGeneral_Validar = ListaPromedioGeneral.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdAnio,
                    q.IdSede,
                    q.IdJornada,
                    q.IdNivel,
                    q.IdCurso,
                    q.IdParalelo,
                    q.OrdenJornada,
                    q.OrdenNivel,
                    q.OrdenCurso,
                    q.OrdenParalelo,
                    q.NomSede,
                    q.Descripcion,
                    q.NomJornada,
                    q.NomCurso,
                    q.NomNivel,
                    q.NomParalelo,
                    q.IdAlumno,
                    q.pe_nombreCompleto
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdAnio = q.Key.IdAnio,
                    IdSede = q.Key.IdSede,
                    IdJornada = q.Key.IdJornada,
                    IdNivel = q.Key.IdNivel,
                    IdCurso = q.Key.IdCurso,
                    IdParalelo = q.Key.IdParalelo,
                    NomSede = q.Key.NomSede,
                    Descripcion = q.Key.Descripcion,
                    NomJornada = q.Key.NomJornada,
                    NomCurso = q.Key.NomCurso,
                    NomNivel = q.Key.NomNivel,
                    NomParalelo = q.Key.NomParalelo,
                    IdAlumno = q.Key.IdAlumno,
                    pe_nombreCompleto = q.Key.pe_nombreCompleto,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    /*SumaGeneral = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)),
                    /*PromedioFinalCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))*/
                }).ToList();

                foreach (var item in ListaPromedioGeneral_Validar)
                {
                    var suma = new ACA_034_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdAlumno = item.IdAlumno,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        Columna = "SUMA TOTAL",
                        NombreGrupo = null,
                        OrdenMateria = 999,
                        OrdenGrupo = 999,
                        Calificacion = (item.OptativasNulas > 0 ? null : Convert.ToString(item.SumaGeneral)),
                        CalificacionNumerica = (item.OptativasNulas > 0 ? (decimal?)null : item.SumaGeneral),
                    };
                    ListaPromedioGeneral_Agregar.Add(suma);

                    var promedio = new ACA_034_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdAlumno = item.IdAlumno,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        Columna = "PROMEDIO GENERAL",
                        NombreGrupo = null,
                        OrdenMateria = 999,
                        OrdenGrupo = 999,
                        Calificacion = (item.OptativasNulas > 0 ? null : Convert.ToString(item.SumaGeneral)),
                        CalificacionNumerica = (item.OptativasNulas > 0 ? (decimal?)null : item.SumaGeneral),
                    };
                    ListaPromedioGeneral_Agregar.Add(promedio);
                }

                ListaFinal.AddRange(ListaPromedioGeneral_Agregar);
                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
