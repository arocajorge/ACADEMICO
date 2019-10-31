using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_CatalogoTipo_Data
    {
        public List<aca_CatalogoTipo_Info> getList(bool MostrarAnulados)
        {
            try
            {
                List<aca_CatalogoTipo_Info> Lista = new List<aca_CatalogoTipo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_CatalogoTipo.Where(q => q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_CatalogoTipo_Info
                        {
                            IdCatalogoTipo = q.IdCatalogoTipo,
                            NomCatalogoTipo = q.NomCatalogoTipo,
                            Codigo = q.Codigo,
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

        public aca_CatalogoTipo_Info getInfo(int IdCatalogoTipo)
        {
            try
            {
                aca_CatalogoTipo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_CatalogoTipo.Where(q => q.IdCatalogoTipo == IdCatalogoTipo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_CatalogoTipo_Info
                    {
                        IdCatalogoTipo = Entity.IdCatalogoTipo,
                        NomCatalogoTipo = Entity.NomCatalogoTipo,
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
                    var cont = Context.aca_CatalogoTipo.Count();
                    if (cont > 0)
                        ID = Context.aca_CatalogoTipo.Max(q => q.IdCatalogoTipo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_CatalogoTipo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoTipo Entity = new aca_CatalogoTipo
                    {
                        IdCatalogoTipo = info.IdCatalogoTipo= getId(),
                        NomCatalogoTipo = info.NomCatalogoTipo,
                        Codigo = info.Codigo,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_CatalogoTipo.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_CatalogoTipo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoTipo Entity = Context.aca_CatalogoTipo.FirstOrDefault(q =>q.IdCatalogoTipo == info.IdCatalogoTipo);
                    if (Entity == null)
                        return false;
                    Entity.NomCatalogoTipo = info.NomCatalogoTipo;
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

        public bool anularDB(aca_CatalogoTipo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoTipo Entity = Context.aca_CatalogoTipo.FirstOrDefault(q => q.IdCatalogoTipo == info.IdCatalogoTipo);
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
