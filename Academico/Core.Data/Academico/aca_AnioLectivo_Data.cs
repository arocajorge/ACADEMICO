using Core.Data.Base;
using Core.Data.SeguridadAcceso;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Data
    {
        aca_AnioLectivo_Periodo_Data odata_periodo = new aca_AnioLectivo_Periodo_Data();
        aca_AnioLectivo_Sede_NivelAcademico_Data odata_sede_nivel = new aca_AnioLectivo_Sede_NivelAcademico_Data();
        aca_AnioLectivo_NivelAcademico_Jornada_Data odata_nivel_jornada = new aca_AnioLectivo_NivelAcademico_Jornada_Data();
        aca_AnioLectivo_Jornada_Curso_Data odata_jornada_curso = new aca_AnioLectivo_Jornada_Curso_Data();
        aca_AnioLectivo_Curso_Paralelo_Data odata_curso_paralelo = new aca_AnioLectivo_Curso_Paralelo_Data();
        aca_AnioLectivo_Curso_Documento_Data odata_curso_documento = new aca_AnioLectivo_Curso_Documento_Data();
        aca_AnioLectivo_Curso_Materia_Data odata_curso_materia = new aca_AnioLectivo_Curso_Materia_Data();
        aca_AnioLectivo_Paralelo_Profesor_Data odata_paralelo_profesor = new aca_AnioLectivo_Paralelo_Profesor_Data();
        aca_Sede_Data odata_sede = new aca_Sede_Data();
        aca_NivelAcademico_Data odata_nivel = new aca_NivelAcademico_Data();
        aca_Jornada_Data odata_jornada = new aca_Jornada_Data();
        aca_Curso_Data odata_curso = new aca_Curso_Data();
        aca_Paralelo_Data odata_paralelo = new aca_Paralelo_Data();
        aca_Materia_Data odata_materia = new aca_Materia_Data();
        aca_Profesor_Data odata_profesor = new aca_Profesor_Data();
        aca_AnioLectivoCalificacionCualitativa_Data odata_calificacion_cualitativa = new aca_AnioLectivoCalificacionCualitativa_Data();
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta_equivalencia = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_AnioLectivoEquivalenciaPromedio_Data odata_equivalencia_promedio = new aca_AnioLectivoEquivalenciaPromedio_Data();
        aca_Plantilla_Data odata_plantilla = new aca_Plantilla_Data();
        aca_AnioLectivo_Curso_Plantilla_Data odata_curso_plantilla = new aca_AnioLectivo_Curso_Plantilla_Data();
        aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Data odata_curso_plantilla_parametrizacion = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Data();
        aca_AnioLectivo_Rubro_Data odata_anio_rubro = new aca_AnioLectivo_Rubro_Data();
        aca_AnioLectivo_Rubro_Periodo_Data odata_anio_rubro_periodo = new aca_AnioLectivo_Rubro_Periodo_Data();
        aca_Documento_Data odata_documento = new aca_Documento_Data();
        seg_usuario_Data odata_usuario = new seg_usuario_Data();

        public List<aca_AnioLectivo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q=>q.Descripcion).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Descripcion = q.Descripcion,
                            FechaDesde = q.FechaDesde,
                            FechaHasta = q.FechaHasta,
                            EnCurso = q.EnCurso,
                            BloquearMatricula = q.BloquearMatricula,
                            IdAnioLectivoAnterior = q.IdAnioLectivoAnterior,
                            PromedioMinimoParcial = q.PromedioMinimoParcial,
                            PromedioMinimoPromocion = q.PromedioMinimoPromocion,
                            IdCursoBachiller = q.IdCursoBachiller,
                            Estado = q.Estado
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

        public List<aca_AnioLectivo_Info> getList_Matricula(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.BloquearMatricula==false && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q => q.Descripcion).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Descripcion = q.Descripcion,
                            FechaDesde = q.FechaDesde,
                            FechaHasta = q.FechaHasta,
                            EnCurso = q.EnCurso,
                            BloquearMatricula = q.BloquearMatricula,
                            PromedioMinimoParcial = q.PromedioMinimoParcial,
                            PromedioMinimoPromocion = q.PromedioMinimoPromocion,
                            IdAnioLectivoAnterior = q.IdAnioLectivoAnterior,
                            IdCursoBachiller = q.IdCursoBachiller,
                            Estado = q.Estado
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
        public List<aca_AnioLectivo_Info> getList_update(int IdEmpresa)
        {
            try
            {
                List<aca_AnioLectivo_Info> Lista = new List<aca_AnioLectivo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true && q.BloquearMatricula == false).Select(q => new aca_AnioLectivo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        Descripcion = q.Descripcion,
                        FechaDesde = q.FechaDesde,
                        FechaHasta = q.FechaHasta,
                        EnCurso = q.EnCurso,
                        BloquearMatricula = q.BloquearMatricula,
                        PromedioMinimoParcial = q.PromedioMinimoParcial,
                        PromedioMinimoPromocion = q.PromedioMinimoPromocion,
                        IdCursoBachiller = q.IdCursoBachiller,
                        Estado = q.Estado
                    }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo(int IdEmpresa, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
                        IdAnioLectivoAnterior = Entity.IdAnioLectivoAnterior,
                        PromedioMinimoParcial = Entity.PromedioMinimoParcial,
                        PromedioMinimoPromocion = Entity.PromedioMinimoPromocion,
                        IdCursoBachiller = Entity.IdCursoBachiller,
                        CalificacionMaxima = Entity.CalificacionMaxima,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo_x_Anio(int IdEmpresa, int AnioIni, int AnioFin)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.FechaDesde.Year == AnioIni && q.FechaHasta.Year == AnioFin).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
                        IdAnioLectivoAnterior = Entity.IdAnioLectivoAnterior,
                        PromedioMinimoParcial = Entity.PromedioMinimoParcial,
                        PromedioMinimoPromocion = Entity.PromedioMinimoPromocion,
                        IdCursoBachiller = Entity.IdCursoBachiller,
                        CalificacionMaxima = Entity.CalificacionMaxima,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo_AnioAnterior(int IdEmpresa, int Anio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.FechaDesde.Year == Anio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        BloquearMatricula = Entity.BloquearMatricula,
                        IdAnioLectivoAnterior = Entity.IdAnioLectivoAnterior,
                        PromedioMinimoParcial = Entity.PromedioMinimoParcial,
                        PromedioMinimoPromocion = Entity.PromedioMinimoPromocion,
                        IdCursoBachiller = Entity.IdCursoBachiller,
                        CalificacionMaxima = Entity.CalificacionMaxima,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Info getInfo_AnioEnCurso(int IdEmpresa, int IdAnio)
        {
            try
            {
                aca_AnioLectivo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado==true && q.EnCurso==true && (IdAnio==0 ? q.IdAnio == q.IdAnio : q.IdAnio!= IdAnio)).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        Descripcion = Entity.Descripcion,
                        FechaDesde = Entity.FechaDesde,
                        FechaHasta = Entity.FechaHasta,
                        EnCurso = Entity.EnCurso,
                        IdAnioLectivoAnterior = Entity.IdAnioLectivoAnterior,
                        PromedioMinimoParcial = Entity.PromedioMinimoParcial,
                        PromedioMinimoPromocion = Entity.PromedioMinimoPromocion,
                        IdCursoBachiller = Entity.IdCursoBachiller,
                        CalificacionMaxima = Entity.CalificacionMaxima,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAnio) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var IdPeriodo = 0;
                    aca_AnioLectivo Entity = new aca_AnioLectivo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio = getId(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        FechaDesde = info.FechaDesde,
                        FechaHasta = info.FechaHasta,
                        EnCurso = info.EnCurso,
                        BloquearMatricula = info.BloquearMatricula,
                        IdAnioLectivoAnterior = info.IdAnioLectivoAnterior,
                        PromedioMinimoParcial = info.PromedioMinimoParcial,
                        PromedioMinimoPromocion = info.PromedioMinimoPromocion,
                        IdCursoBachiller = info.IdCursoBachiller,
                        CalificacionMaxima = info.CalificacionMaxima,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_AnioLectivo.Add(Entity);

                    if (info.lst_periodos.Count >0)
                    {
                        IdPeriodo = odata_periodo.getId(info.IdEmpresa);
                        foreach (var item in info.lst_periodos)
                        {
                            aca_AnioLectivo_Periodo Entity_Periodo = new aca_AnioLectivo_Periodo
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdPeriodo = IdPeriodo++,
                                IdAnio = info.IdAnio,
                                IdMes = item.IdMes,
                                FechaDesde = item.FechaDesde,
                                FechaHasta = item.FechaHasta,
                                FechaProntoPago = null,
                                Estado = true,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = info.FechaCreacion = DateTime.Now

                            };
                                Context.aca_AnioLectivo_Periodo.Add(Entity_Periodo);
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo Entity = Context.aca_AnioLectivo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;

                    Entity.Descripcion = info.Descripcion;
                    Entity.FechaDesde = info.FechaDesde;
                    Entity.FechaHasta = info.FechaHasta;
                    Entity.EnCurso = info.EnCurso;
                    Entity.BloquearMatricula = info.BloquearMatricula;
                    Entity.IdAnioLectivoAnterior = info.IdAnioLectivoAnterior;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.PromedioMinimoParcial = info.PromedioMinimoParcial;
                    Entity.PromedioMinimoPromocion = info.PromedioMinimoPromocion;
                    Entity.IdCursoBachiller = info.IdCursoBachiller;
                    Entity.CalificacionMaxima = info.CalificacionMaxima;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivo Entity = Context.aca_AnioLectivo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Entity.EnCurso = false;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarAperturaDB(aca_AnioLectivo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_equivalencia_promedio_x_anio = odata_equivalencia_promedio.getList(info.IdEmpresa,info.IdAnio,false);
                    if (lst_equivalencia_promedio_x_anio.Count > 0)
                    {
                        var IdEquivalenciaPromedio = odata_equivalencia_promedio.getId(info.IdEmpresa);
                        foreach (var item in lst_equivalencia_promedio_x_anio)
                        {
                            aca_AnioLectivoEquivalenciaPromedio Entity_EquivalenciaPromedio = new aca_AnioLectivoEquivalenciaPromedio
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdEquivalenciaPromedio = IdEquivalenciaPromedio++,
                                IdAnio = info.IdAnioApertura,
                                Descripcion = item.Descripcion,
                                Codigo = item.Codigo,
                                ValorMinimo = item.ValorMinimo,
                                ValorMaximo = item.ValorMaximo,
                                Estado = item.Estado,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            Context.aca_AnioLectivoEquivalenciaPromedio.Add(Entity_EquivalenciaPromedio);
                        }
                    }

                    var lst_equivalencia_cualitativa_x_anio = odata_calificacion_cualitativa.getList(info.IdEmpresa, info.IdAnio, false);
                    if (lst_equivalencia_cualitativa_x_anio.Count > 0)
                    {
                        var IdEquivalenciaPromedio = odata_calificacion_cualitativa.getId(info.IdEmpresa);
                        foreach (var item in lst_equivalencia_cualitativa_x_anio)
                        {
                            aca_AnioLectivoCalificacionCualitativa Entity_EquivalenciaCualitativa = new aca_AnioLectivoCalificacionCualitativa
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdCalificacionCualitativa = IdEquivalenciaPromedio++,
                                IdAnio = info.IdAnioApertura,
                                DescripcionCorta = item.DescripcionCorta,
                                DescripcionLarga = item.DescripcionLarga,
                                Codigo = item.Codigo,
                                Calificacion = item.Calificacion,
                                Estado = item.Estado,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            Context.aca_AnioLectivoCalificacionCualitativa.Add(Entity_EquivalenciaCualitativa);
                        }
                    }

                    var lst_equivalencia_conducta_x_anio = odata_conducta_equivalencia.getList(info.IdEmpresa, info.IdAnio, false);
                    if (lst_equivalencia_conducta_x_anio.Count > 0)
                    {
                        var Secuencia = 1;
                        foreach (var item in lst_equivalencia_conducta_x_anio)
                        {
                            aca_AnioLectivoConductaEquivalencia Entity_EquivalenciaConducta = new aca_AnioLectivoConductaEquivalencia
                            {
                                IdEmpresa = item.IdEmpresa,
                                Secuencia = Secuencia++,
                                IdAnio = info.IdAnioApertura,
                                Equivalencia = item.Equivalencia,
                                DescripcionEquivalencia = item.DescripcionEquivalencia,
                                Letra = item.Letra,
                                Calificacion = item.Calificacion,
                                IngresaInspector = item.IngresaInspector,
                                IngresaMotivo = item.IngresaMotivo,
                                IngresaProfesor = item.IngresaProfesor
                            };
                            Context.aca_AnioLectivoConductaEquivalencia.Add(Entity_EquivalenciaConducta);
                        }
                    }


                    //RUBROS//
                    var lst_rubros_x_anio = odata_anio_rubro.getList(info.IdEmpresa, info.IdAnio, false);
                    if (lst_rubros_x_anio.Count > 0)
                    {
                        foreach (var item in lst_rubros_x_anio)
                        {
                            aca_AnioLectivo_Rubro Entity_AnioRubro = new aca_AnioLectivo_Rubro
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdAnio = info.IdAnioApertura,
                                IdRubro = item.IdRubro,
                                AplicaProntoPago = item.AplicaProntoPago,
                                NomRubro = item.NomRubro,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                NumeroCuotas = item.NumeroCuotas
                            };
                            Context.aca_AnioLectivo_Rubro.Add(Entity_AnioRubro);
                        }
                    }

                    //var lst_rubros_x_anio_periodos = odata_anio_rubro_periodo.getList(info.IdEmpresa, info.IdAnio, false);
                    //if (lst_rubros_x_anio_periodos.Count > 0)
                    //{
                    //    foreach (var item in lst_rubros_x_anio_periodos)
                    //    {
                    //        aca_AnioLectivo_Rubro_Periodo Entity_AnioRubro_Periodo = new aca_AnioLectivo_Rubro_Periodo
                    //        {
                    //            IdEmpresa = item.IdEmpresa,
                    //            IdAnio = info.IdAnioApertura,
                    //            IdRubro = item.IdRubro,
                    //            IdPeriodo = item.IdPeriodo,
                    //            Secuencia = item.Secuencia,
                    //            Observacion = item.Observacion
                    //        };
                    //        Context.aca_AnioLectivo_Rubro_Periodo.Add(Entity_AnioRubro_Periodo);
                    //    }
                    //}


                    // PLANTILLAS POR AÑO
                    var lst_plantillas_x_año = odata_plantilla.getList(info.IdEmpresa, info.IdAnio, false);
                    if (lst_plantillas_x_año.Count > 0)
                    {
                        var IdPlantilla = odata_plantilla.getId(info.IdEmpresa);
                        foreach (var item in lst_plantillas_x_año)
                        {
                            aca_Plantilla Entity_Plantilla = new aca_Plantilla
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdAnio = info.IdAnioApertura,
                                IdPlantilla = IdPlantilla++,
                                NomPlantilla = item.NomPlantilla,
                                Valor = item.Valor,
                                TipoDescuento = item.TipoDescuento,
                                Estado = item.Estado,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = DateTime.Now,
                                IdTipoPlantilla = item.IdTipoPlantilla,
                                IdTipoNota = item.IdTipoNota,
                                AplicaParaTodo = item.AplicaParaTodo
                            };
                            Context.aca_Plantilla.Add(Entity_Plantilla);
                        }
                    }

                    var lst_asignacion_sede_nivel = odata_sede_nivel.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio).Where(q=> q.seleccionado==true).ToList();
                    if (lst_asignacion_sede_nivel.Count > 0)
                    {
                        #region SedeNivel
                        foreach (var item_sn in lst_asignacion_sede_nivel)
                        {
                            var info_sede = odata_sede.GetInfo(info.IdEmpresa, item_sn.IdSede);
                            var info_nivel = odata_nivel.getInfo(info.IdEmpresa, item_sn.IdNivel);
                            aca_AnioLectivo_Sede_NivelAcademico Entity_SedeNivel = new aca_AnioLectivo_Sede_NivelAcademico
                            {
                                IdEmpresa = item_sn.IdEmpresa,
                                IdAnio = info.IdAnioApertura,
                                IdSede = item_sn.IdSede,
                                IdNivel = item_sn.IdNivel,
                                NomNivel = info_nivel.NomNivel,
                                NomSede = info_sede.NomSede
                            };
                            Context.aca_AnioLectivo_Sede_NivelAcademico.Add(Entity_SedeNivel);
                        }
                        #endregion

                        var lst_nivel = lst_asignacion_sede_nivel.GroupBy(q => q.IdNivel).ToList();
                        if (lst_nivel.Count > 0)
                        {
                            foreach (var item_niv in lst_nivel)
                            {
                                var lst_asignacion_nivel_jornada = odata_nivel_jornada.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key)).Where(q => q.seleccionado == true).ToList();

                                foreach (var item_nj in lst_asignacion_nivel_jornada)
                                {
                                    var info_jornada = odata_jornada.getInfo(info.IdEmpresa, item_nj.IdJornada);
                                    aca_AnioLectivo_NivelAcademico_Jornada Entity_NivelJornada = new aca_AnioLectivo_NivelAcademico_Jornada
                                    {
                                        IdEmpresa = item_nj.IdEmpresa,
                                        IdAnio = info.IdAnioApertura,
                                        IdSede = item_nj.IdSede,
                                        IdNivel = item_nj.IdNivel,
                                        IdJornada = item_nj.IdJornada,
                                        NomJornada = info_jornada.NomJornada,
                                        OrdenJornada = info_jornada.OrdenJornada
                                    };
                                    Context.aca_AnioLectivo_NivelAcademico_Jornada.Add(Entity_NivelJornada);
                                }

                                var lst_jornada = lst_asignacion_nivel_jornada.GroupBy(q => q.IdJornada).ToList();
                                if (lst_jornada.Count > 0)
                                {
                                    foreach (var item_jor in lst_jornada)
                                    {
                                        var lst_asignacion_jornada_curso = odata_jornada_curso.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key)).Where(q => q.seleccionado == true).ToList();

                                        foreach (var item_jc in lst_asignacion_jornada_curso)
                                        {
                                            var info_curso = odata_curso.getInfo(info.IdEmpresa, item_jc.IdCurso);
                                            aca_AnioLectivo_Jornada_Curso Entity_JornadaCurso = new aca_AnioLectivo_Jornada_Curso
                                            {
                                                IdEmpresa = item_jc.IdEmpresa,
                                                IdAnio = info.IdAnioApertura,
                                                IdSede = item_jc.IdSede,
                                                IdNivel = item_jc.IdNivel,
                                                IdJornada = item_jc.IdJornada,
                                                IdCurso = item_jc.IdCurso,
                                                NomCurso = info_curso.NomCurso,
                                                OrdenCurso = info_curso.OrdenCurso
                                            };
                                            Context.aca_AnioLectivo_Jornada_Curso.Add(Entity_JornadaCurso);
                                        }

                                        var lst_curso = lst_asignacion_jornada_curso.GroupBy(q => q.IdCurso).ToList();
                                        if (lst_curso.Count > 0)
                                        {
                                            foreach (var item_cur in lst_curso)
                                            {
                                                var lst_asignacion_curso_paralelo = odata_curso_paralelo.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();

                                                foreach (var item_cp in lst_asignacion_curso_paralelo)
                                                {
                                                    var inf_paralelo = odata_paralelo.getInfo(info.IdEmpresa, item_cp.IdParalelo);
                                                    var info_profesor_insp = odata_profesor.getInfo(info.IdEmpresa, Convert.ToDecimal(item_cp.IdProfesorInspector));
                                                    var info_profesor_tutor = odata_profesor.getInfo(info.IdEmpresa, Convert.ToDecimal(item_cp.IdProfesorTutor));

                                                    aca_AnioLectivo_Curso_Paralelo Entity_CursoParalelo = new aca_AnioLectivo_Curso_Paralelo
                                                    {
                                                        IdEmpresa = item_cp.IdEmpresa,
                                                        IdAnio = info.IdAnioApertura,
                                                        IdSede = item_cp.IdSede,
                                                        IdNivel = item_cp.IdNivel,
                                                        IdJornada = item_cp.IdJornada,
                                                        IdCurso = item_cp.IdCurso,
                                                        IdParalelo = item_cp.IdParalelo,
                                                        CodigoParalelo = inf_paralelo.CodigoParalelo,
                                                        NomParalelo = inf_paralelo.NomParalelo,
                                                        OrdenParalelo = inf_paralelo.OrdenParalelo,
                                                        IdProfesorInspector = (info_profesor_insp==null ? (decimal?)null : (info_profesor_insp.Estado==false ? (decimal?)null : item_cp.IdProfesorInspector)),
                                                        IdProfesorTutor = (info_profesor_tutor == null ? (decimal?)null : (info_profesor_tutor.Estado == false ? (decimal?)null : item_cp.IdProfesorInspector)),
                                                    };
                                                    Context.aca_AnioLectivo_Curso_Paralelo.Add(Entity_CursoParalelo);
                                                }

                                                var lst_asignacion_curso_materia = odata_curso_materia.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();
                                                if (lst_asignacion_curso_materia.Count > 0)
                                                {
                                                    foreach (var item_cm in lst_asignacion_curso_materia)
                                                    {
                                                        var info_materia = odata_materia.getInfo(info.IdEmpresa, item_cm.IdMateria);
                                                        aca_AnioLectivo_Curso_Materia Entity_CursoMateria = new aca_AnioLectivo_Curso_Materia
                                                        {
                                                            IdEmpresa = item_cm.IdEmpresa,
                                                            IdAnio = info.IdAnioApertura,
                                                            IdSede = item_cm.IdSede,
                                                            IdNivel = item_cm.IdNivel,
                                                            IdJornada = item_cm.IdJornada,
                                                            IdCurso = item_cm.IdCurso,
                                                            IdMateria = item_cm.IdMateria,
                                                            NomMateria = info_materia.NomMateria,
                                                            NomMateriaArea = info_materia.NomMateriaArea,
                                                            NomMateriaGrupo = info_materia.NomMateriaGrupo,
                                                            EsObligatorio = info_materia.EsObligatorio,
                                                            OrdenMateria = info_materia.OrdenMateria,
                                                            OrdenMateriaArea = info_materia.OrdenMateriaArea,
                                                            OrdenMateriaGrupo = info_materia.OrdenMateriaGrupo,
                                                            IdCatalogoTipoCalificacion = info_materia.IdCatalogoTipoCalificacion
                                                        };
                                                        Context.aca_AnioLectivo_Curso_Materia.Add(Entity_CursoMateria);
                                                    }
                                                }

                                                var lst_asignacion_curso_documento = odata_curso_documento.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();
                                                if (lst_asignacion_curso_documento.Count > 0)
                                                {
                                                    foreach (var item_cd in lst_asignacion_curso_documento)
                                                    {
                                                        var inf_documento = odata_documento.getInfo(info.IdEmpresa, item_cd.IdDocumento);
                                                        aca_AnioLectivo_Curso_Documento Entity_CursoDocumento = new aca_AnioLectivo_Curso_Documento
                                                        {
                                                            IdEmpresa = item_cd.IdEmpresa,
                                                            IdAnio = info.IdAnioApertura,
                                                            IdSede = item_cd.IdSede,
                                                            IdNivel = item_cd.IdNivel,
                                                            IdJornada = item_cd.IdJornada,
                                                            IdCurso = item_cd.IdCurso,
                                                            IdDocumento = item_cd.IdDocumento,
                                                            NomDocumento = inf_documento.NomDocumento,
                                                            OrdenDocumento = inf_documento.OrdenDocumento
                                                        };
                                                        Context.aca_AnioLectivo_Curso_Documento.Add(Entity_CursoDocumento);
                                                    }
                                                }

                                                /*
                                                var lst_plantillas_x_anio = odata_curso_plantilla.getList(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key));
                                                if (lst_plantillas_x_anio.Count > 0)
                                                {
                                                    foreach (var item_p in lst_plantillas_x_anio)
                                                    {
                                                        //var inf_documento = odata_documento.getInfo(info.IdEmpresa, item_cd.IdDocumento);
                                                        aca_AnioLectivo_Curso_Plantilla Entity_CursoPlantilla = new aca_AnioLectivo_Curso_Plantilla
                                                        {
                                                            IdEmpresa = item_p.IdEmpresa,
                                                            IdAnio = info.IdAnioApertura,
                                                            IdSede = item_p.IdSede,
                                                            IdNivel = item_p.IdNivel,
                                                            IdJornada = item_p.IdJornada,
                                                            IdCurso = item_p.IdCurso,
                                                            IdPlantilla = item_p.IdPlantilla,
                                                            Observacion = item_p.Observacion
                                                        };
                                                        Context.aca_AnioLectivo_Curso_Plantilla.Add(Entity_CursoPlantilla);
                                                    }
                                                }


                                                var lst_plantillas_x_anio_periodo = odata_curso_plantilla_parametrizacion.GetList_x_Curso(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key));
                                                if (lst_plantillas_x_anio_periodo.Count > 0)
                                                {
                                                    foreach (var item_p in lst_plantillas_x_anio_periodo)
                                                    {
                                                        aca_AnioLectivo_Curso_Plantilla_Parametrizacion Entity_CursoPlantilla_Parametrizacion = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion
                                                        {
                                                            IdEmpresa = item_p.IdEmpresa,
                                                            IdAnio = info.IdAnioApertura,
                                                            IdSede = item_p.IdSede,
                                                            IdNivel = item_p.IdNivel,
                                                            IdJornada = item_p.IdJornada,
                                                            IdCurso = item_p.IdCurso,
                                                            IdPlantilla = item_p.IdPlantilla,
                                                            IdRubro = item_p.IdRubro,
                                                            IdCtaCbleDebe = item_p.IdCtaCbleDebe,
                                                            IdCtaCbleHaber = item_p.IdCtaCbleHaber
                                                        };
                                                        Context.aca_AnioLectivo_Curso_Plantilla_Parametrizacion.Add(Entity_CursoPlantilla_Parametrizacion);
                                                    }
                                                }
                                                */
                                            }
                                            
                                            Context.SaveChanges();

                                            //PARALELO POR PROFESOR
                                            foreach (var item_cur in lst_curso)
                                            {
                                                var lst_asignacion_curso_paralelo = odata_curso_paralelo.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key)).Where(q => q.seleccionado == true).ToList();
                                                if (lst_asignacion_curso_paralelo.Count > 0)
                                                {
                                                    foreach (var item_cp in lst_asignacion_curso_paralelo)
                                                    {
                                                        var lst_asignacion_paralelo_profesor = odata_paralelo_profesor.get_list_asignacion(info.IdEmpresa, info.IdSede, info.IdAnio, Convert.ToInt32(item_niv.Key), Convert.ToInt32(item_jor.Key), Convert.ToInt32(item_cur.Key), Convert.ToInt32(item_cp.IdParalelo)).ToList();
                                                        if (lst_asignacion_paralelo_profesor.Count>0)
                                                        { 
                                                            foreach (var item_pp in lst_asignacion_paralelo_profesor)
                                                            {
                                                                var info_profesor = odata_profesor.getInfo(info.IdEmpresa, Convert.ToDecimal(item_pp.IdProfesor));
                                                                aca_AnioLectivo_Paralelo_Profesor Entity_ParaleloProfesor = new aca_AnioLectivo_Paralelo_Profesor
                                                                {
                                                                    IdEmpresa = item_pp.IdEmpresa,
                                                                    IdAnio = info.IdAnioApertura,
                                                                    IdSede = item_pp.IdSede,
                                                                    IdNivel = item_pp.IdNivel,
                                                                    IdJornada = item_pp.IdJornada,
                                                                    IdCurso = item_pp.IdCurso,
                                                                    IdMateria = item_pp.IdMateria,
                                                                    IdParalelo = item_pp.IdParalelo,
                                                                    IdProfesor = (info_profesor == null ? (decimal?)null : (info_profesor.Estado == false ? (decimal?)null : item_pp.IdProfesor))
                                                                };
                                                                Context.aca_AnioLectivo_Paralelo_Profesor.Add(Entity_ParaleloProfesor);
                                                            }
                                                        }
                                                    }
                                                }
                                                Context.SaveChanges();
                                            } //lista de curso
                                        }
                                    }
                                }
                            }
                        }
                    }
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
