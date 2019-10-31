using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Catalogo_Data
    {
        public List<aca_Catalogo_Info> getList(bool MostrarAnulados)
        {
            try
            {
                List<aca_Catalogo_Info> Lista = new List<aca_Catalogo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Catalogo.Where(q => q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Catalogo_Info
                        {
                            IdCatalogo = q.IdCatalogo,
                            IdCatalogoTipo = q.IdCatalogoTipo,
                            NomCatalogo = q.NomCatalogo,
                            Codigo = q.Codigo,
                            Orden = q.Orden,
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

        public aca_Catalogo_Info getInfo(int IdCatalogo)
        {
            try
            {
                aca_Catalogo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Catalogo.Where(q => q.IdCatalogo == IdCatalogo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Catalogo_Info
                    {
                        IdCatalogo = Entity.IdCatalogo,
                        IdCatalogoTipo = Entity.IdCatalogoTipo,
                        NomCatalogo = Entity.NomCatalogo,
                        Orden = Entity.Orden,
                        Codigo = Entity.Codigo,
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

        public int getId()
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Catalogo.Count();
                    if (cont > 0)
                        ID = Context.aca_Catalogo.Max(q => q.IdCatalogo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getOrden(int IdCatalogoTipo)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Catalogo.Where(q => q.Estado == true && q.IdCatalogoTipo == IdCatalogoTipo).Count();
                    if (cont > 0)
                        ID = Context.aca_Catalogo.Where(q => q.Estado == true && q.IdCatalogoTipo == IdCatalogoTipo).Max(q => q.Orden) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_Catalogo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Catalogo Entity = new aca_Catalogo
                    {
                        IdCatalogo = info.IdCatalogo=getId(),
                        IdCatalogoTipo = info.IdCatalogoTipo,
                        NomCatalogo = info.NomCatalogo,
                        Orden = info.Orden,
                        Codigo = info.Codigo,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Catalogo.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Catalogo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Catalogo Entity = Context.aca_Catalogo.FirstOrDefault(q => q.IdCatalogo == info.IdCatalogo);
                    if (Entity == null)
                        return false;
                    Entity.IdCatalogoTipo = info.IdCatalogoTipo;
                    Entity.NomCatalogo = info.NomCatalogo;
                    Entity.Orden = info.Orden;
                    Entity.Codigo = info.Codigo;
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

        public bool anularDB(aca_Catalogo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Catalogo Entity = Context.aca_Catalogo.FirstOrDefault(q => q.IdCatalogo == info.IdCatalogo);
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
