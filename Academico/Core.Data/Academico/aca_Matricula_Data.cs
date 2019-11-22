using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Matricula_Data
    {
        public List<aca_Matricula_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Matricula_Info> Lista = new List<aca_Matricula_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Matricula.Where(q => q.IdEmpresa == IdEmpresa).ToList();

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
                            pe_nombreCompleto =q.pe_nombreCompleto
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
        public aca_Matricula_Info getInfo(int IdEmpresa, int IdMatricula)
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
                        Codigo = info.Codigo,
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
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_Matricula.Add(Entity);

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
                                IdProducto = item.IdProducto,
                                Subtotal = item.IdProducto,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                FechaFacturacion = item.FechaFacturacion
                            };
                            Context.aca_Matricula_Rubro.Add(Entity_Det);
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
                    Entity.Codigo = info.Codigo;
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
