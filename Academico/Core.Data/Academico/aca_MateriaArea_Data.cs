using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MateriaArea_Data
    {
        public List<aca_MateriaArea_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_MateriaArea_Info> Lista = new List<aca_MateriaArea_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MateriaArea_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMateriaArea = q.IdMateriaArea,
                            NomMateriaArea = q.NomMateriaArea,
                            OrdenMateriaArea = q.OrdenMateriaArea,
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

        public aca_MateriaArea_Info getInfo(int IdEmpresa, int IdMateriaArea)
        {
            try
            {
                aca_MateriaArea_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa && q.IdMateriaArea == IdMateriaArea).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MateriaArea_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMateriaArea = Entity.IdMateriaArea,
                        NomMateriaArea = Entity.NomMateriaArea,
                        OrdenMateriaArea = Entity.OrdenMateriaArea,
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
                    var cont = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMateriaArea) + 1;
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
                    var cont = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenMateriaArea) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MateriaArea_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaArea Entity = new aca_MateriaArea
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMateriaArea = info.IdMateriaArea = getId(info.IdEmpresa),
                        NomMateriaArea = info.NomMateriaArea,
                        OrdenMateriaArea = info.OrdenMateriaArea,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_MateriaArea.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_MateriaArea_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaArea Entity = Context.aca_MateriaArea.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaArea == info.IdMateriaArea);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomMateriaArea = info.NomMateriaArea;
                    Entity.OrdenMateriaArea = info.OrdenMateriaArea;
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

        public bool anularDB(aca_MateriaArea_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaArea Entity = Context.aca_MateriaArea.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaArea == info.IdMateriaArea);
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
