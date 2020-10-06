using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Tematica_Data
    {
        public List<aca_Tematica_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Tematica_Info> Lista = new List<aca_Tematica_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Tematica.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Tematica_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdCampoAccion = q.IdCampoAccion,
                            NombreTematica = q.NombreTematica,
                            NombreCampoAccion = q.NombreCampoAccion,
                            IdTematica = q.IdTematica,
                            OrdenTematica = q.OrdenTematica,
                            OrdenCampoAccion = q.OrdenCampoAccion,
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

        public aca_Tematica_Info getInfo(int IdEmpresa, int IdTematica)
        {
            try
            {
                aca_Tematica_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Tematica.Where(q => q.IdEmpresa == IdEmpresa && q.IdTematica == IdTematica).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Tematica_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTematica = Entity.IdTematica,
                        IdCampoAccion = Entity.IdCampoAccion,
                        NombreTematica = Entity.NombreTematica,
                        OrdenTematica = Entity.OrdenTematica,
                        NombreCampoAccion = Entity.NombreCampoAccion,
                        OrdenCampoAccion = Entity.OrdenCampoAccion,
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
                    var cont = Context.aca_Tematica.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Tematica.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdTematica) + 1;
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
                    var cont = Context.aca_Tematica.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_Tematica.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenTematica) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Tematica_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Tematica Entity = new aca_Tematica
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTematica = info.IdTematica = getId(info.IdEmpresa),
                        IdCampoAccion = info.IdCampoAccion,
                        NombreTematica = info.NombreTematica,
                        OrdenTematica = info.OrdenTematica,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Tematica.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Tematica_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Tematica Entity = Context.aca_Tematica.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTematica == info.IdTematica);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.IdCampoAccion = info.IdCampoAccion;
                    Entity.NombreTematica = info.NombreTematica;
                    Entity.OrdenTematica = info.OrdenTematica;
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

        public bool anularDB(aca_Tematica_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Tematica Entity = Context.aca_Tematica.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdTematica == info.IdTematica);
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
