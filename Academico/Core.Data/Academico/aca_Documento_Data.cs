using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Documento_Data
    {
        public List<aca_Documento_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Documento_Info> Lista = new List<aca_Documento_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q => q.OrdenDocumento).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Documento_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdDocumento = q.IdDocumento,
                            NomDocumento = q.NomDocumento,
                            OrdenDocumento = q.OrdenDocumento,
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
        public aca_Documento_Info getInfo(int IdEmpresa, int IdDocumento)
        {
            try
            {
                aca_Documento_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.IdDocumento == IdDocumento).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Documento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDocumento = Entity.IdDocumento,
                        NomDocumento = Entity.NomDocumento,
                        OrdenDocumento = Entity.OrdenDocumento,
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
                    var cont = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdDocumento) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getOrden(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenDocumento) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Documento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Documento Entity = new aca_Documento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdDocumento = info.IdDocumento = getId(info.IdEmpresa),
                        NomDocumento = info.NomDocumento,
                        OrdenDocumento = info.OrdenDocumento??0,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Documento.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Documento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Documento Entity = Context.aca_Documento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDocumento == info.IdDocumento);
                    if (Entity == null)
                        return false;

                    Entity.NomDocumento = info.NomDocumento;
                    Entity.OrdenDocumento = info.OrdenDocumento??0;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
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

        public bool anularDB(aca_Documento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Documento Entity = Context.aca_Documento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDocumento == info.IdDocumento);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
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
