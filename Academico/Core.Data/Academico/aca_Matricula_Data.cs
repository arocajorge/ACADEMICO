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
    public class aca_Matricula_Data
    {
        aca_MatriculaCambios_Data odata_HistoricoPlantilla = new aca_MatriculaCambios_Data();
        aca_AlumnoDocumento_Data odata_AlumnoDocumento = new aca_AlumnoDocumento_Data();
        public List<aca_Matricula_Info> getList(int IdEmpresa, int IdAnio, int IdSede, bool MostrarAnulados)
        {
            try
            {
                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio==IdAnio && q.IdSede == IdSede).OrderByDescending(q=>q.IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Matricula_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            Codigo = q.Codigo,
                            IdAlumno = q.IdAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            pe_nombreCompleto =q.pe_nombreCompleto,
                            BloquearMatricula = q.BloquearMatricula,
                            EsRetirado = q.EsRetirado??false,
                            NomPlantilla = q.NomPlantilla
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

        public List<aca_Matricula_Info> getList_PorCurso(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_Matricula_AlumnosPorParalelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo && q.EsRetirado==false).OrderBy(q => q.pe_nombreCompleto).ToList();

                    foreach (var item in lst)
                    {
                        var info = new aca_Matricula_Info
                         {
                             IdEmpresa = item.IdEmpresa,
                             IdMatricula = item.IdMatricula,
                             Fecha = item.Fecha,
                             pe_cedulaRuc = item.pe_cedulaRuc,
                             IdAlumno = item.IdAlumno,
                             Codigo = item.Codigo,
                             pe_nombreCompleto = item.pe_nombreCompleto,
                             EsRetirado = item.EsRetirado??false,
                             EsRetiradoString = item.EsRetiradoString
                        };
                        Lista.Add(info);
                    }
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_Matricula_Info getInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_Matricula_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Matricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        Codigo =Entity.Codigo,
                        IdAlumno = Entity.IdAlumno,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        IdPersonaF = Entity.IdPersonaF,
                        IdPersonaR = Entity.IdPersonaR,
                        IdPlantilla = Entity.IdPlantilla,
                        Observacion = Entity.Observacion,
                        Fecha = Entity.Fecha,
                        IdMecanismo = Entity.IdMecanismo,
                        IdEmpresa_rol = Entity.IdEmpresa_rol,
                        IdEmpleado = Entity.IdEmpleado,
                        NomCurso = Entity.NomCurso,
                        NomJornada = Entity.NomJornada,
                        NomParalelo = Entity.NomParalelo,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_Matricula_Info getInfo_UltimaMatricula(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_Matricula_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).OrderByDescending(q=> q.IdMatricula).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Matricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        Codigo = Entity.Codigo,
                        IdAlumno = Entity.IdAlumno,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        IdPersonaF = Entity.IdPersonaF,
                        IdPersonaR = Entity.IdPersonaR,
                        IdPlantilla = Entity.IdPlantilla,
                        Observacion = Entity.Observacion,
                        Fecha = Entity.Fecha,
                        IdMecanismo = Entity.IdMecanismo,
                        IdEmpresa_rol = Entity.IdEmpresa_rol,
                        IdEmpleado = Entity.IdEmpleado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Matricula_Info getInfo_ExisteMatricula(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                aca_Matricula_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Matricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        Codigo = Entity.Codigo,
                        IdAlumno = Entity.IdAlumno,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdNivel = Entity.IdNivel,
                        IdJornada = Entity.IdJornada,
                        IdCurso = Entity.IdCurso,
                        IdParalelo = Entity.IdParalelo,
                        IdPersonaF = Entity.IdPersonaF,
                        IdPersonaR = Entity.IdPersonaR,
                        IdPlantilla = Entity.IdPlantilla,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        NomSede = Entity.NomSede,
                        NomJornada = Entity.NomJornada,
                        NomCurso = Entity.NomCurso,
                        NomNivel = Entity.NomNivel,
                        NomParalelo = Entity.NomParalelo,
                        pe_nombreCompleto = Entity.pe_nombreCompleto
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMatricula) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = new aca_Matricula
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMatricula = info.IdMatricula = getId(info.IdEmpresa),
                        Codigo = info.IdMatricula.ToString("00000"),
                        IdAlumno = info.IdAlumno,
                        IdAnio = info.IdAnio,
                        IdSede = info.IdSede,
                        IdNivel = info.IdNivel,
                        IdJornada = info.IdJornada,
                        IdCurso = info.IdCurso,
                        IdParalelo = info.IdParalelo,
                        IdPersonaF = info.IdPersonaF,
                        IdPersonaR = info.IdPersonaR,
                        IdPlantilla = info.IdPlantilla,
                        IdMecanismo = info.IdMecanismo,
                        Observacion = info.Observacion,
                        IdCatalogoESTMAT = info.IdCatalogoESTMAT,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Fecha = info.Fecha,
                        IdEmpresa_rol=info.IdEmpresa_rol,
                        IdEmpleado = info.IdEmpleado
                    };
                    Context.aca_Matricula.Add(Entity);
                    
                    if (info.lst_calificacion_parcial.Count > 0)
                    {
                        foreach (var item in info.lst_calificacion_parcial)
                        {
                            aca_MatriculaCalificacionParcial Entity_CalificacionParcial = new aca_MatriculaCalificacionParcial
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdMateria = item.IdMateria,
                                IdProfesor = item.IdProfesor,
                                IdCatalogoParcial = item.IdCatalogoParcial,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            Context.aca_MatriculaCalificacionParcial.Add(Entity_CalificacionParcial);
                        }
                    }

                    if (info.lst_MatriculaCalificacionCualitativa.Count > 0)
                    {
                        foreach (var item in info.lst_MatriculaCalificacionCualitativa)
                        {
                            aca_MatriculaCalificacionCualitativa Entity_CalificacionCualitativaParcial = new aca_MatriculaCalificacionCualitativa
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdMateria = item.IdMateria,
                                IdProfesor = item.IdProfesor,
                                IdCatalogoParcial = item.IdCatalogoParcial,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                FechaCreacion = DateTime.Now
                            };
                            Context.aca_MatriculaCalificacionCualitativa.Add(Entity_CalificacionCualitativaParcial);
                        }
                    }

                    if (info.lst_calificacion.Count > 0)
                    {
                        foreach (var item in info.lst_calificacion)
                        {
                            aca_MatriculaCalificacion Entity_Calificacion = new aca_MatriculaCalificacion
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdMateria = item.IdMateria,
                                IdProfesor = item.IdProfesor,
                            };
                            Context.aca_MatriculaCalificacion.Add(Entity_Calificacion);
                        }
                    }

                    aca_MatriculaConducta Entity_Conducta = new aca_MatriculaConducta
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMatricula = info.IdMatricula
                    };
                    Context.aca_MatriculaConducta.Add(Entity_Conducta);

                    aca_MatriculaAsistencia Entity_Asistencia = new aca_MatriculaAsistencia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMatricula = info.IdMatricula,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                    };
                    Context.aca_MatriculaAsistencia.Add(Entity_Asistencia);

                    if (info.lst_MatriculaRubro.Count > 0)
                    {
                        foreach (var item in info.lst_MatriculaRubro)
                        {
                            aca_Matricula_Rubro Entity_Det = new aca_Matricula_Rubro
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdPeriodo = item.IdPeriodo,
                                IdRubro = item.IdRubro,
                                IdMecanismo = item.IdMecanismo,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                FechaFacturacion = null,
                                EnMatricula = item.EnMatricula,
                                IdPlantilla = item.IdPlantilla,
                                IdAnio = item.IdAnio,
                                IdSede = item.IdSede,
                                IdNivel = item.IdNivel,
                                IdJornada = item.IdJornada,
                                IdCurso = item.IdCurso,
                                IdParalelo = item.IdParalelo
                            };
                            Context.aca_Matricula_Rubro.Add(Entity_Det);
                        }
                    }

                    #region Documentos por alumno
                    //Obtengo lista de documentos por curso
                    var Secuencia = odata_AlumnoDocumento.getSecuencia(info.IdEmpresa, info.IdAlumno);
                    var lstDocPorCurso = Context.aca_AnioLectivo_Curso_Documento.Where(q => q.IdEmpresa == info.IdEmpresa
                  && q.IdSede == info.IdSede
                  && q.IdAnio == info.IdAnio
                  && q.IdNivel == info.IdNivel
                  && q.IdJornada == info.IdJornada
                  && q.IdCurso == info.IdCurso).ToList();

                    //Recorro lista de documentos por curso
                    foreach (var item in lstDocPorCurso)
                    {
                        //Valido si en la lista de los seleccionados existe el documento
                        var Documento = info.lst_documentos.Where(q => q.IdDocumento == item.IdDocumento).FirstOrDefault();
                        //Si no existe como seleccionado
                        if (Documento == null)
                        {
                            //Valido si existe en la lista de documentos por alumno
                            var DocumentoAlumno = Context.aca_AlumnoDocumento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.IdDocumento == item.IdDocumento).FirstOrDefault();
                            if (DocumentoAlumno == null)
                            {
                                //Si no existe lo agrego con estado false
                                Context.aca_AlumnoDocumento.Add(new aca_AlumnoDocumento
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAlumno = info.IdAlumno,
                                    IdDocumento = item.IdDocumento,
                                    Secuencia = Secuencia++,
                                    EnArchivo = false
                                });
                            }
                            else
                            {
                                //Si existe lo modifico y le pongo estado false
                                DocumentoAlumno.EnArchivo = false;
                            }
                        }
                        else
                        {
                            //Si existe como seleccionado valido si existe en la tabla de documentos por alumno
                            var DocumentoAlumnoE = Context.aca_AlumnoDocumento.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.IdDocumento == item.IdDocumento).FirstOrDefault();
                            if (DocumentoAlumnoE == null)
                            {
                                //Si no existe lo agrego con estado true
                                Context.aca_AlumnoDocumento.Add(new aca_AlumnoDocumento
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdAlumno = info.IdAlumno,
                                    IdDocumento = item.IdDocumento,
                                    Secuencia = Secuencia++,
                                    EnArchivo = true
                                });
                            }
                            else
                            {
                                //Si existe lo modifico y le pongo estado true
                                DocumentoAlumnoE.EnArchivo = true;
                            }
                        }
                    }
                    #endregion

                    aca_Alumno Entity_Alumno = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    Entity_Alumno.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoMatricula.MATRICULADO);
                    Entity_Alumno.IdCurso = info.IdCurso;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;

                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarPlantillaDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;
                    Entity.IdPlantilla = info.IdPlantilla;
                    Entity.IdEmpresa_rol = (info.IdEmpleado==null ? null : info.IdEmpresa_rol);
                    Entity.IdEmpleado = info.IdEmpleado;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var lst_MatriculaRubro = Context.aca_Matricula_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula && q.FechaFacturacion == null).ToList();
                    Context.aca_Matricula_Rubro.RemoveRange(lst_MatriculaRubro);
                    var nueva_lista_ingresar = info.lst_MatriculaRubro.Where(q => q.FechaFacturacion == null).ToList();

                    if (nueva_lista_ingresar.Count > 0)
                    {
                        foreach (var item in nueva_lista_ingresar)
                        {
                            aca_Matricula_Rubro Entity_Det = new aca_Matricula_Rubro
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdMecanismo = item.IdMecanismo,
                                IdPeriodo = item.IdPeriodo,
                                IdRubro = item.IdRubro,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                EnMatricula = item.EnMatricula,
                                IdPlantilla = item.IdPlantilla,
                                IdAnio = item.IdAnio,
                                IdSede = item.IdSede,
                                IdNivel = item.IdNivel,
                                IdJornada = item.IdJornada,
                                IdCurso = item.IdCurso,
                                IdParalelo = item.IdParalelo,
                            };
                            Context.aca_Matricula_Rubro.Add(Entity_Det);
                        }
                    }

                    #region HistoricoPlantilla
                    aca_MatriculaCambios Entity_Historico = new aca_MatriculaCambios
                    {
                        IdEmpresa = info.info_MatriculaCambios.IdEmpresa,
                        IdMatricula = info.info_MatriculaCambios.IdMatricula,
                        Secuencia = odata_HistoricoPlantilla.getSecuenciaByMatricula(info.info_MatriculaCambios.IdEmpresa, info.info_MatriculaCambios.IdMatricula),
                        IdAnio = info.info_MatriculaCambios.IdAnio,
                        IdSede = info.info_MatriculaCambios.IdSede,
                        IdNivel = info.info_MatriculaCambios.IdNivel,
                        IdJornada = info.info_MatriculaCambios.IdJornada,
                        IdCurso = info.info_MatriculaCambios.IdCurso,
                        IdParalelo = info.info_MatriculaCambios.IdParalelo,
                        IdPlantilla = info.info_MatriculaCambios.IdPlantilla,
                        TipoCambio = info.info_MatriculaCambios.TipoCambio,
                        IdUsuarioCreacion = info.info_MatriculaCambios.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Observacion = info.ObservacionCambio
                    };
                    Context.aca_MatriculaCambios.Add(Entity_Historico);
                    #endregion

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarEstadoMatriculaDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;
                    Entity.IdCatalogoESTMAT = info.IdCatalogoESTMAT;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool modificarCursoParaleloDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;

                    Entity.IdSede = info.IdSede;
                    Entity.IdNivel = info.IdNivel;
                    Entity.IdJornada = info.IdJornada;
                    Entity.IdCurso = info.IdCurso;
                    Entity.IdParalelo = info.IdParalelo;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    

                    #region HistoricoPlantilla
                    aca_MatriculaCambios Entity_Cambios = new aca_MatriculaCambios
                    {
                        IdEmpresa = info.info_MatriculaCambios.IdEmpresa,
                        IdMatricula = info.info_MatriculaCambios.IdMatricula,
                        Secuencia = odata_HistoricoPlantilla.getSecuenciaByMatricula(info.info_MatriculaCambios.IdEmpresa, info.info_MatriculaCambios.IdMatricula),
                        IdAnio = info.info_MatriculaCambios.IdAnio,
                        IdSede = info.info_MatriculaCambios.IdSede,
                        IdNivel = info.info_MatriculaCambios.IdNivel,
                        IdJornada = info.info_MatriculaCambios.IdJornada,
                        IdCurso = info.info_MatriculaCambios.IdCurso,
                        IdParalelo = info.info_MatriculaCambios.IdParalelo,
                        IdPlantilla = info.info_MatriculaCambios.IdPlantilla,
                        TipoCambio = info.info_MatriculaCambios.TipoCambio,
                        IdUsuarioCreacion = info.info_MatriculaCambios.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Observacion = info.ObservacionCambio
                    };
                    Context.aca_MatriculaCambios.Add(Entity_Cambios);
                    #endregion
                    Context.SaveChanges();

                    #region MatriculaRubro
                    var lst_MatriculaRubro = Context.aca_Matricula_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula && q.FechaFacturacion == null).ToList();
                    foreach (var item in lst_MatriculaRubro)
                    {
                        aca_Matricula_Rubro EntityMatriculaRubro = Context.aca_Matricula_Rubro.FirstOrDefault(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula && q.IdPeriodo == item.IdPeriodo && q.IdAnio== item.IdAnio && q.IdRubro == item.IdRubro);
                        EntityMatriculaRubro.IdSede = info.IdSede;
                        EntityMatriculaRubro.IdNivel = info.IdNivel;
                        EntityMatriculaRubro.IdJornada = info.IdJornada;
                        EntityMatriculaRubro.IdCurso = info.IdCurso;
                        EntityMatriculaRubro.IdParalelo = info.IdParalelo;

                        Context.SaveChanges();
                    }

                    #endregion

                    #region Calificaciones
                    var lst_MatriculaCalificacionesParciales = Context.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                    Context.aca_MatriculaCalificacionParcial.RemoveRange(lst_MatriculaCalificacionesParciales);
                    foreach (var cal_parcial in info.lst_MatriculaCalificacionParcial)
                    {
                        aca_MatriculaCalificacionParcial Entity_ParcialCuantitativa = new aca_MatriculaCalificacionParcial
                        {
                            IdEmpresa = cal_parcial.IdEmpresa,
                            IdMatricula = cal_parcial.IdMatricula,
                            IdMateria = cal_parcial.IdMateria,
                            IdCatalogoParcial = cal_parcial.IdCatalogoParcial,
                            IdProfesor = cal_parcial.IdProfesor,
                            Calificacion1 = cal_parcial.Calificacion1,
                            Calificacion2 = cal_parcial.Calificacion2,
                            Calificacion3 = cal_parcial.Calificacion3,
                            Calificacion4 = cal_parcial.Calificacion4,
                            Remedial1 = cal_parcial.Remedial1,
                            Remedial2 = cal_parcial.Remedial2,
                            Evaluacion = cal_parcial.Evaluacion,
                            Conducta = cal_parcial.Conducta,
                            MotivoCalificacion = cal_parcial.MotivoCalificacion,
                            MotivoConducta = cal_parcial.MotivoConducta,
                            AccionRemedial = cal_parcial.AccionRemedial,
                            IdUsuarioCreacion = cal_parcial.IdUsuarioCreacion,
                            FechaCreacion = cal_parcial.FechaCreacion,
                            IdUsuarioModificacion = cal_parcial.IdUsuarioModificacion,
                            FechaModificacion = cal_parcial.FechaModificacion
                        };

                        Context.aca_MatriculaCalificacionParcial.Add(Entity_ParcialCuantitativa);
                    }

                    var lst_MatriculaCalificaciones = Context.aca_MatriculaCalificacion.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                    Context.aca_MatriculaCalificacion.RemoveRange(lst_MatriculaCalificaciones);
                    foreach (var cal_final in info.lst_MatriculaCalificacion)
                    {
                        aca_MatriculaCalificacion Entity_Cuantitativa = new aca_MatriculaCalificacion
                        {
                            IdEmpresa = cal_final.IdEmpresa,
                            IdMatricula = cal_final.IdMatricula,
                            IdMateria = cal_final.IdMateria,
                            IdProfesor = cal_final.IdProfesor,
                            CalificacionP1 = cal_final.CalificacionP1,
                            CalificacionP2 = cal_final.CalificacionP2,
                            CalificacionP3 = cal_final.CalificacionP3,
                            CalificacionP4 = cal_final.CalificacionP4,
                            CalificacionP5 = cal_final.CalificacionP5,
                            CalificacionP6 = cal_final.CalificacionP6,
                            PromedioQ1 = cal_final.PromedioQ1,
                            ExamenQ1 = cal_final.ExamenQ1,
                            PromedioFinalQ1 = cal_final.PromedioFinalQ1,
                            PromedioQ2 = cal_final.PromedioQ2,
                            ExamenQ2 = cal_final.ExamenQ2,
                            PromedioFinalQ2 = cal_final.PromedioFinalQ2,
                            ExamenMejoramiento = cal_final.ExamenMejoramiento,
                            CampoMejoramiento = cal_final.CampoMejoramiento,
                            ExamenSupletorio = cal_final.ExamenSupletorio,
                            ExamenRemedial = cal_final.ExamenRemedial,
                            ExamenGracia = cal_final.ExamenGracia,
                            PromedioFinal = cal_final.PromedioFinal,
                            IdEquivalenciaPromedioP1 = cal_final.IdEquivalenciaPromedioP1,
                            IdEquivalenciaPromedioP2 = cal_final.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioP3 = cal_final.IdEquivalenciaPromedioP2,
                            IdEquivalenciaPromedioEQ1 = cal_final.IdEquivalenciaPromedioEQ1,
                            IdEquivalenciaPromedioQ1 = cal_final.IdEquivalenciaPromedioQ1,
                            IdEquivalenciaPromedioP4 = cal_final.IdEquivalenciaPromedioP4,
                            IdEquivalenciaPromedioP5 = cal_final.IdEquivalenciaPromedioP5,
                            IdEquivalenciaPromedioP6 = cal_final.IdEquivalenciaPromedioP6,
                            IdEquivalenciaPromedioEQ2 = cal_final.IdEquivalenciaPromedioEQ2,
                            IdEquivalenciaPromedioQ2 = cal_final.IdEquivalenciaPromedioQ2,
                            IdEquivalenciaPromedioPF = cal_final.IdEquivalenciaPromedioPF
                        };

                        Context.aca_MatriculaCalificacion.Add(Entity_Cuantitativa);
                    }

                    var lst_MatriculaCalificacionesCualitativas = Context.aca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                    Context.aca_MatriculaCalificacionCualitativa.RemoveRange(lst_MatriculaCalificacionesCualitativas);
                    foreach (var cal_cualit in info.lst_MatriculaCalificacionCualitativa)
                    {
                        aca_MatriculaCalificacionCualitativa Entity_ParcialCualitativa = new aca_MatriculaCalificacionCualitativa
                        {
                            IdEmpresa = cal_cualit.IdEmpresa,
                            IdMatricula = cal_cualit.IdMatricula,
                            IdMateria = cal_cualit.IdMateria,
                            IdCatalogoParcial = cal_cualit.IdCatalogoParcial,
                            IdProfesor = cal_cualit.IdProfesor,
                            IdCalificacionCualitativa = cal_cualit.IdCalificacionCualitativa,
                            Conducta = cal_cualit.Conducta,
                            MotivoConducta = cal_cualit.MotivoConducta,
                            IdUsuarioCreacion = cal_cualit.IdUsuarioCreacion,
                            FechaCreacion = cal_cualit.FechaCreacion,
                            IdUsuarioModificacion = cal_cualit.IdUsuarioModificacion,
                            FechaModificacion = cal_cualit.FechaModificacion
                        };

                        Context.aca_MatriculaCalificacionCualitativa.Add(Entity_ParcialCualitativa);
                    }

                    #endregion

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool eliminarDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;

                    Context.Database.ExecuteSqlCommand("DELETE aca_MatriculaCalificacion WHERE IdEmpresa = " + info.IdEmpresa + " AND IdMatricula = " + info.IdMatricula);
                    Context.Database.ExecuteSqlCommand("DELETE aca_MatriculaConducta WHERE IdEmpresa = " + info.IdEmpresa + " AND IdMatricula = " + info.IdMatricula);
                    Context.Database.ExecuteSqlCommand("DELETE aca_MatriculaCalificacionParcial WHERE IdEmpresa = " + info.IdEmpresa + " AND IdMatricula = " + info.IdMatricula);

                    Context.Database.ExecuteSqlCommand("DELETE aca_MatriculaCambios WHERE IdEmpresa = " + info.IdEmpresa + " AND IdMatricula = " + info.IdMatricula);
                    Context.Database.ExecuteSqlCommand("DELETE aca_Matricula_Rubro WHERE IdEmpresa = " + info.IdEmpresa + " AND IdMatricula = " + info.IdMatricula);
                    Context.Database.ExecuteSqlCommand("DELETE aca_Matricula WHERE IdEmpresa = " + info.IdEmpresa + " AND IdMatricula = " + info.IdMatricula);

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Matricula_Info> getList_Calificaciones(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
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

                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.vwaca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede
                    && q.IdNivel >= IdNivelIni && q.IdNivel <= IdNivelFin && q.IdJornada >= IdJornadaIni && q.IdJornada <= IdJornadaFin
                    && q.IdCurso >= IdCursoIni && q.IdCurso <= IdCursoFin && q.IdParalelo >= IdParaleloIni && q.IdParalelo <= IdParaleloFin
                    && q.IdAlumno >= IdAlumnoIni && q.IdAlumno <= IdAlumnoFin && q.EsRetirado==false).OrderBy(q=> new { q.OrdenJornada, q.OrdenNivel, q.OrdenCurso, q.OrdenParalelo, q.pe_nombreCompleto }).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new aca_Matricula_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            Fecha = q.Fecha,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            NomSede=q.NomSede,
                            Descripcion = q.Descripcion,
                            NomJornada = q.NomJornada,
                            NomNivel = q.NomNivel,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenJornada = q.OrdenJornada??0,
                            OrdenNivel = q.OrdenNivel??0,
                            OrdenCurso = q.OrdenCurso??0,
                            OrdenParalelo = q.OrdenParalelo??0,
                        });
                    }
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
