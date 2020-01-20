using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_Religion_Data
    {
        public List<tb_Religion_Info> getList(bool MostrarAnulados)
        {
            try
            {
                List<tb_Religion_Info> Lista = new List<tb_Religion_Info>();

                using (EntitiesGeneral odata = new EntitiesGeneral())
                {
                    var lst = odata.tb_Religion.Where(q => q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new tb_Religion_Info
                        {
                            IdReligion = q.IdReligion,
                            NomReligion = q.NomReligion,
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
        public tb_Religion_Info getInfo(int IdReligion)
        {
            try
            {
                tb_Religion_Info info;

                using (EntitiesGeneral db = new EntitiesGeneral())
                {
                    var Entity = db.tb_Religion.Where(q => q.IdReligion == IdReligion).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new tb_Religion_Info
                    {
                        IdReligion = Entity.IdReligion,
                        NomReligion = Entity.NomReligion,
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

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    var cont = Context.tb_Religion.Count();
                    if (cont > 0)
                        ID = Context.tb_Religion.Max(q => q.IdReligion) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_Religion_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_Religion Entity = new tb_Religion
                    {
                        IdReligion = info.IdReligion = getId(),
                        NomReligion = info.NomReligion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.tb_Religion.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(tb_Religion_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_Religion Entity = Context.tb_Religion.FirstOrDefault(q => q.IdReligion == info.IdReligion);
                    if (Entity == null)
                        return false;

                    Entity.NomReligion = info.NomReligion;
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

        public bool anularDB(tb_Religion_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_Religion Entity = Context.tb_Religion.FirstOrDefault(q => q.IdReligion == info.IdReligion);
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
