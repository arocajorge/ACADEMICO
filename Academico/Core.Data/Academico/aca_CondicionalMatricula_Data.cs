using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_CondicionalMatricula_Data
    {
        public List<aca_CondicionalMatricula_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_CondicionalMatricula_Info> Lista = new List<aca_CondicionalMatricula_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_CondicionalMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio
                    && q.Estado == (MostrarAnulados == true ? q.Estado : true)).Select(q => new aca_CondicionalMatricula_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdCondicional = q.IdCondicional,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoCONDIC = q.IdCatalogoCONDIC,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
                        AnioLectivo = q.Descripcion,
                        Alumno = q.pe_nombreCompleto,
                        Estado = q.Estado,
                        NomCatalogo = q.NomCatalogo
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_CondicionalMatricula_Info> getList_ExisteCondicional(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoCONDIC)
        {
            try
            {
                List<aca_CondicionalMatricula_Info> Lista = new List<aca_CondicionalMatricula_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_CondicionalMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno 
                    && q.IdCatalogoCONDIC == IdCatalogoCONDIC  && q.Estado == true).Select(q => new aca_CondicionalMatricula_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdCondicional = q.IdCondicional,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoCONDIC = q.IdCatalogoCONDIC,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
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

        public List<aca_CondicionalMatricula_Info> getList_Matricula(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                List<aca_CondicionalMatricula_Info> Lista = new List<aca_CondicionalMatricula_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_CondicionalMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno
                    && q.Estado == true).Select(q => new aca_CondicionalMatricula_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdCondicional = q.IdCondicional,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoCONDIC = q.IdCatalogoCONDIC,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
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
        public aca_CondicionalMatricula_Info getInfo(int IdEmpresa, int IdCondicional)
        {
            try
            {
                aca_CondicionalMatricula_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_CondicionalMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdCondicional == IdCondicional).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_CondicionalMatricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCondicional = Entity.IdCondicional,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoCONDIC = Entity.IdCatalogoCONDIC,
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
                    var cont = Context.aca_CondicionalMatricula.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_CondicionalMatricula.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdCondicional) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_CondicionalMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CondicionalMatricula Entity = new aca_CondicionalMatricula
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdCondicional = info.IdCondicional = getId(info.IdEmpresa),
                        IdAlumno = info.IdAlumno,
                        IdCatalogoCONDIC = info.IdCatalogoCONDIC,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_CondicionalMatricula.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_CondicionalMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CondicionalMatricula Entity = Context.aca_CondicionalMatricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdCondicional == info.IdCondicional);
                    if (Entity == null)
                        return false;

                    Entity.Fecha = info.Fecha;
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

        public bool anularDB(aca_CondicionalMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CondicionalMatricula Entity = Context.aca_CondicionalMatricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdCondicional == info.IdCondicional);
                    if (Entity == null)
                        return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

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
