using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Jornada_Data
    {
        public List<aca_Jornada_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Jornada_Info> Lista = new List<aca_Jornada_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q => q.OrdenJornada).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Jornada_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdJornada = q.IdJornada,
                            NomJornada = q.NomJornada,
                            OrdenJornada = q.OrdenJornada,
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

        public List<aca_Jornada_Info> getList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel)
        {
            try
            {
                List<aca_Jornada_Info> Lista = new List<aca_Jornada_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_NivelAcademico_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel).OrderBy(q => q.OrdenJornada).GroupBy(q => new { q.IdJornada, q.NomJornada }).Select(q => new { q.Key.IdJornada, q.Key.NomJornada }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Jornada_Info
                        {
                            IdJornada = q.IdJornada,
                            NomJornada = q.NomJornada,
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

        public aca_Jornada_Info getInfo(int IdEmpresa, int IdJornada)
        {
            try
            {
                aca_Jornada_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.IdJornada == IdJornada).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Jornada_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdJornada = Entity.IdJornada,
                        NomJornada = Entity.NomJornada,
                        OrdenJornada = Entity.OrdenJornada,
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
                    var cont = Context.aca_Jornada.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Jornada.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdJornada) + 1;
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
                    var cont = Context.aca_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.Estado==true).Count();
                    if (cont > 0)
                        ID = Context.aca_Jornada.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenJornada) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Jornada_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Jornada Entity = new aca_Jornada
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdJornada = info.IdJornada = getId(info.IdEmpresa),
                        NomJornada = info.NomJornada,
                        OrdenJornada = info.OrdenJornada,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Jornada.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Jornada_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Jornada Entity = Context.aca_Jornada.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdJornada == info.IdJornada);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomJornada = info.NomJornada;
                    Entity.OrdenJornada = info.OrdenJornada;
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

        public bool anularDB(aca_Jornada_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Jornada Entity = Context.aca_Jornada.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdJornada == info.IdJornada);
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
