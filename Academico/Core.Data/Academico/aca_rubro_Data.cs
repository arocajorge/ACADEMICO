using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_rubro_Data
    {
        public List<aca_rubro_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_rubro_Info> Lista = new List<aca_rubro_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_rubro.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_rubro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdRubro = q.IdRubro,
                            NomRubro = q.NomRubro,
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

        public aca_rubro_Info getInfo(int IdEmpresa, int IdRubro)
        {
            try
            {
                aca_rubro_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_rubro.Where(q => q.IdEmpresa == IdEmpresa && q.IdRubro == IdRubro).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_rubro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRubro = Entity.IdRubro,
                        NomRubro = Entity.NomRubro,
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
                    var cont = Context.aca_rubro.Where(q=> q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_rubro.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdRubro) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_rubro Entity = new aca_rubro
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRubro = info.IdRubro = getId(info.IdEmpresa),
                        NomRubro = info.NomRubro,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_rubro.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_rubro Entity = Context.aca_rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRubro == info.IdRubro);
                    if (Entity == null)
                        return false;
                    Entity.NomRubro = info.NomRubro;
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

        public bool anularDB(aca_rubro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_rubro Entity = Context.aca_rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRubro == info.IdRubro);
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
