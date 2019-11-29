using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MecanismoDePago_Data
    {
        public List<aca_MecanismoDePago_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_MecanismoDePago_Info> Lista = new List<aca_MecanismoDePago_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados == true ? q.Estado : true)).Select(q => new aca_MecanismoDePago_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMecanismo = q.IdMecanismo,
                        NombreMecanismo = q.NombreMecanismo,
                        IdTerminoPago = q.IdTerminoPago,
                        nom_TerminoPago = q.nom_TerminoPago,
                        Estado = q.Estado
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_MecanismoDePago_Info getInfo(int IdEmpresa, int IdMecanismo)
        {
            try
            {
                aca_MecanismoDePago_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa && q.IdMecanismo == IdMecanismo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MecanismoDePago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMecanismo = Entity.IdMecanismo,
                        NombreMecanismo = Entity.NombreMecanismo,
                        IdTerminoPago = Entity.IdTerminoPago,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMecanismo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MecanismoDePago_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MecanismoDePago Entity = new aca_MecanismoDePago
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMecanismo = info.IdMecanismo = getId(info.IdEmpresa),
                        NombreMecanismo = info.NombreMecanismo,
                        IdTerminoPago = info.IdTerminoPago,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_MecanismoDePago.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_MecanismoDePago_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MecanismoDePago Entity = Context.aca_MecanismoDePago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMecanismo == info.IdMecanismo);
                    if (Entity == null)
                        return false;

                    Entity.NombreMecanismo = info.NombreMecanismo;
                    Entity.IdTerminoPago = info.IdTerminoPago;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool anularDB(aca_MecanismoDePago_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MecanismoDePago Entity = Context.aca_MecanismoDePago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMecanismo == info.IdMecanismo);
                    if (Entity == null)
                        return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
