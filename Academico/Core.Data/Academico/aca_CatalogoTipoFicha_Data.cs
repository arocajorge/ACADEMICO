using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_CatalogoTipoFicha_Data
    {
        public List<aca_CatalogoTipoFicha_Info> get_list()
        {
            try
            {
                List<aca_CatalogoTipoFicha_Info> Lista;
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = (from q in Context.aca_CatalogoTipoFicha
                             select new aca_CatalogoTipoFicha_Info
                             {
                                 IdCatalogoTipoFicha = q.IdCatalogoTipoFicha,
                                 Codigo = q.Codigo,
                                 NomCatalogoTipoFicha = q.NomCatalogoTipoFicha

                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_CatalogoTipoFicha_Info get_info(int IdCatalogoTipoFicha)
        {
            try
            {
                aca_CatalogoTipoFicha_Info info = new aca_CatalogoTipoFicha_Info();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoTipoFicha Entity = Context.aca_CatalogoTipoFicha.FirstOrDefault(q => q.IdCatalogoTipoFicha == IdCatalogoTipoFicha);
                    if (Entity == null) return null;
                    info = new aca_CatalogoTipoFicha_Info
                    {
                        IdCatalogoTipoFicha = Entity.IdCatalogoTipoFicha,
                        Codigo = Entity.Codigo,
                        Estado = Entity.Estado,
                        NomCatalogoTipoFicha = Entity.NomCatalogoTipoFicha
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int get_id()
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_CatalogoTipoFicha.Count();
                    if (cont > 0)
                        ID = Context.aca_CatalogoTipoFicha.Max(q => q.IdCatalogoTipoFicha) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_CatalogoTipoFicha_Info info)
        {
            try
            {

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoTipoFicha Entity = new aca_CatalogoTipoFicha
                    {
                        IdCatalogoTipoFicha = info.IdCatalogoTipoFicha = get_id(),
                        Codigo = info.Codigo,
                        NomCatalogoTipoFicha = info.NomCatalogoTipoFicha,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_CatalogoTipoFicha.Add(Entity);
                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarDB(aca_CatalogoTipoFicha_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoTipoFicha Entity = Context.aca_CatalogoTipoFicha.FirstOrDefault(q => q.IdCatalogoTipoFicha == info.IdCatalogoTipoFicha);
                    if (Entity == null)
                        return false;
                    Entity.IdCatalogoTipoFicha = info.IdCatalogoTipoFicha;
                    Entity.Codigo = info.Codigo;
                    Entity.NomCatalogoTipoFicha = info.NomCatalogoTipoFicha;
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
    }
}
