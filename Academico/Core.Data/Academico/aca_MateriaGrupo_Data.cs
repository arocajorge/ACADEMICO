using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MateriaGrupo_Data
    {
        public List<aca_MateriaGrupo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_MateriaGrupo_Info> Lista = new List<aca_MateriaGrupo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MateriaGrupo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMateriaGrupo = q.IdMateriaGrupo,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo,
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

        public aca_MateriaGrupo_Info getInfo(int IdEmpresa, int IdMateriaGrupo)
        {
            try
            {
                aca_MateriaGrupo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa && q.IdMateriaGrupo == IdMateriaGrupo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MateriaGrupo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMateriaGrupo = Entity.IdMateriaGrupo,
                        NomMateriaGrupo = Entity.NomMateriaGrupo,
                        OrdenMateriaGrupo = Entity.OrdenMateriaGrupo,
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
                    var cont = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMateriaGrupo) + 1;
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
                    var cont = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenMateriaGrupo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MateriaGrupo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaGrupo Entity = new aca_MateriaGrupo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMateriaGrupo = info.IdMateriaGrupo = getId(info.IdEmpresa),
                        NomMateriaGrupo = info.NomMateriaGrupo,
                        OrdenMateriaGrupo = info.OrdenMateriaGrupo,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_MateriaGrupo.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_MateriaGrupo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaGrupo Entity = Context.aca_MateriaGrupo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaGrupo == info.IdMateriaGrupo);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomMateriaGrupo = info.NomMateriaGrupo;
                    Entity.OrdenMateriaGrupo = info.OrdenMateriaGrupo;
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

        public bool anularDB(aca_MateriaGrupo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaGrupo Entity = Context.aca_MateriaGrupo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaGrupo == info.IdMateriaGrupo);
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
