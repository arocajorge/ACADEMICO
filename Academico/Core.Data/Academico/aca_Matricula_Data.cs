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
        public List<aca_Matricula_Info> getList(int IdEmpresa, int IdAnio, int IdSede, bool MostrarAnulados)
        {
            try
            {
                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio==IdAnio && q.IdSede == IdSede).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Matricula_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
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
                            BloquearMatricula = q.BloquearMatricula
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
                    Lista = Context.vwaca_Matricula_AlumnosPorParalelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede 
                    && q.IdNivel== IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).Select(q => new aca_Matricula_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        Fecha = q.Fecha,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        pe_nombreCompleto = q.pe_nombreCompleto
                    }).ToList();
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
                    var Entity = db.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();
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
                        IdMecanismo = Entity.IdMecanismo
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
                    var Entity = db.aca_Matricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno).FirstOrDefault();
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
                        Observacion = Entity.Observacion
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
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        Fecha = info.Fecha
                    };
                    Context.aca_Matricula.Add(Entity);

                    if (info.lst_calificacion.Count > 0)
                    {
                        foreach (var item in info.lst_calificacion)
                        {
                            aca_MatriculaCalificacion Entity_Calificacion = new aca_MatriculaCalificacion
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdMateria = item.IdMateria,
                                IdProfesor = item.IdProfesor
                            };
                            Context.aca_MatriculaCalificacion.Add(Entity_Calificacion);
                        }
                    }

                    if (info.lst_conducta.Count > 0)
                    {
                        foreach (var item in info.lst_conducta)
                        {
                            aca_MatriculaConducta Entity_Conducta = new aca_MatriculaConducta
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatricula = info.IdMatricula,
                                IdMateria = item.IdMateria,
                                IdProfesor = item.IdProfesor
                            };
                            Context.aca_MatriculaConducta.Add(Entity_Conducta);
                        }
                    }

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
                                IdAnio = item.IdAnio
                            };
                            Context.aca_Matricula_Rubro.Add(Entity_Det);
                        }
                    }

                    if (info.lst_documentos.Count > 0)
                    {
                        foreach (var item in info.lst_documentos)
                        {
                            aca_AlumnoDocumento Entity_DetDoc = new aca_AlumnoDocumento
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdAlumno = item.IdAlumno,
                                IdDocumento = item.IdDocumento,
                                EnArchivo = true
                            };
                            Context.aca_AlumnoDocumento.Add(Entity_DetDoc);
                        }
                    }

                    aca_Alumno Entity_Alumno = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    Entity_Alumno.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoMatricula.MATRICULADO);

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

                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.IdAnio = info.IdAnio;
                    Entity.IdSede = info.IdSede;
                    Entity.IdNivel = info.IdNivel;
                    Entity.IdJornada = info.IdJornada;
                    Entity.IdCurso = info.IdCurso;
                    Entity.IdParalelo = info.IdParalelo;
                    Entity.IdPlantilla = info.IdPlantilla;
                    Entity.IdPersonaF = info.IdPersonaF;
                    Entity.IdPersonaR = info.IdPersonaR;
                    Entity.Fecha = info.Fecha;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var lst_MatriculaRubro = Context.aca_Matricula_Rubro.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula && q.FechaFacturacion== null).ToList();
                    Context.aca_Matricula_Rubro.RemoveRange(lst_MatriculaRubro);
                    var nueva_lista_ingresar = info.lst_MatriculaRubro.Where(q=> q.FechaFacturacion == null).ToList();

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
                                IdAnio = item.IdAnio
                            };
                            Context.aca_Matricula_Rubro.Add(Entity_Det);
                        }
                    }

                    #region Documentos por alumno
                    //Obtengo lista de documentos por curso
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
                                IdAnio = item.IdAnio
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
                        IdUsuarioCreacion = info.info_MatriculaCambios.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
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

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool anularDB(aca_Matricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Matricula Entity = Context.aca_Matricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (Entity == null)
                        return false;

                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
