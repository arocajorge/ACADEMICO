using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_CampoAccion_Data
    {
        public List<aca_CampoAccion_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_CampoAccion_Info> Lista = new List<aca_CampoAccion_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_CampoAccion.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_CampoAccion_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdCampoAccion = q.IdCampoAccion,
                            NombreCampoAccion = q.NombreCampoAccion,
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

        public aca_CampoAccion_Info getInfo(int IdEmpresa, int IdCampoAccion)
        {
            try
            {
                aca_CampoAccion_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_CampoAccion.Where(q => q.IdEmpresa == IdEmpresa && q.IdCampoAccion == IdCampoAccion).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_CampoAccion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCampoAccion = Entity.IdCampoAccion,
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
                    var cont = Context.aca_CampoAccion.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_CampoAccion.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdCampoAccion) + 1;
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
                    var cont = Context.aca_CampoAccion.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_CampoAccion.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenCampoAccion) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_CampoAccion_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CampoAccion Entity = new aca_CampoAccion
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCampoAccion = info.IdCampoAccion = getId(info.IdEmpresa),
                        NombreCampoAccion = info.NombreCampoAccion,
                        OrdenCampoAccion = info.OrdenCampoAccion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_CampoAccion.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_CampoAccion_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CampoAccion Entity = Context.aca_CampoAccion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCampoAccion == info.IdCampoAccion);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NombreCampoAccion = info.NombreCampoAccion;
                    Entity.OrdenCampoAccion = info.OrdenCampoAccion;
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

        public bool anularDB(aca_CampoAccion_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_CampoAccion Entity = Context.aca_CampoAccion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCampoAccion == info.IdCampoAccion);
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
