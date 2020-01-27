using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_CatalogoFicha_Data
    {
        public List<aca_CatalogoFicha_Info> getList(bool MostrarAnulados)
        {
            try
            {
                List<aca_CatalogoFicha_Info> Lista = new List<aca_CatalogoFicha_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_CatalogoFicha.Where(q => q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_CatalogoFicha_Info
                        {
                            IdCatalogoFicha = q.IdCatalogoFicha,
                            IdCatalogoTipoFicha = q.IdCatalogoTipoFicha,
                            NomCatalogoFicha = q.NomCatalogoFicha,
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

        public List<aca_CatalogoFicha_Info> getList_x_Tipo(int IdCatalogoTipoFicha, bool MostrarAnulados)
        {
            try
            {
                List<aca_CatalogoFicha_Info> Lista = new List<aca_CatalogoFicha_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_CatalogoFicha.Where(q => q.IdCatalogoTipoFicha == IdCatalogoTipoFicha && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_CatalogoFicha_Info
                        {
                            IdCatalogoFicha = q.IdCatalogoFicha,
                            IdCatalogoTipoFicha = q.IdCatalogoTipoFicha,
                            NomCatalogoFicha = q.NomCatalogoFicha,
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

        public aca_CatalogoFicha_Info getInfo(int IdCatalogoFicha)
        {
            try
            {
                aca_CatalogoFicha_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_CatalogoFicha.Where(q => q.IdCatalogoFicha == IdCatalogoFicha).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_CatalogoFicha_Info
                    {
                        IdCatalogoFicha = Entity.IdCatalogoFicha,
                        IdCatalogoTipoFicha = Entity.IdCatalogoTipoFicha,
                        NomCatalogoFicha = Entity.NomCatalogoFicha,
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
                    var cont = Context.aca_CatalogoFicha.Count();
                    if (cont > 0)
                        ID = Context.aca_CatalogoFicha.Max(q => q.IdCatalogoFicha) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getOrden(int IdCatalogoTipoFicha)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_CatalogoFicha.Where(q => q.Estado == true && q.IdCatalogoTipoFicha == IdCatalogoTipoFicha).Count();
                    if (cont > 0)
                        ID = Context.aca_CatalogoFicha.Where(q => q.Estado == true && q.IdCatalogoTipoFicha == IdCatalogoTipoFicha).Max(q => q.Orden) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodCatalogo(string Codigo)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = from q in Context.aca_CatalogoFicha
                              where Codigo == q.Codigo
                              select q;

                    if (lst.Count() > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_CatalogoFicha_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoFicha Entity = new aca_CatalogoFicha
                    {
                        IdCatalogoFicha = info.IdCatalogoFicha = getId(),
                        IdCatalogoTipoFicha = info.IdCatalogoTipoFicha,
                        NomCatalogoFicha = info.NomCatalogoFicha,
                        Orden = info.Orden,
                        Codigo = info.Codigo,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_CatalogoFicha.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_CatalogoFicha_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoFicha Entity = Context.aca_CatalogoFicha.FirstOrDefault(q => q.IdCatalogoFicha == info.IdCatalogoFicha);
                    if (Entity == null)
                        return false;
                    Entity.IdCatalogoTipoFicha = info.IdCatalogoTipoFicha;
                    Entity.NomCatalogoFicha = info.NomCatalogoFicha;
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

        public bool anularDB(aca_CatalogoFicha_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CatalogoFicha Entity = Context.aca_CatalogoFicha.FirstOrDefault(q => q.IdCatalogoFicha == info.IdCatalogoFicha);
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
