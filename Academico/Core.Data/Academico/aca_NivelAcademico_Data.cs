using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_NivelAcademico_Data
    {
        public List<aca_NivelAcademico_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_NivelAcademico_Info> Lista = new List<aca_NivelAcademico_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_NivelAcademico_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdNivel = q.IdNivel,
                            NomNivel = q.NomNivel,
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

        public List<aca_NivelAcademico_Info> getList(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                List<aca_NivelAcademico_Info> Lista = new List<aca_NivelAcademico_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Sede_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).GroupBy(q=> new { q.IdNivel, q.NomNivel}).Select(q=> new { q.Key.IdNivel, q.Key.NomNivel}).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_NivelAcademico_Info
                        {
                            IdNivel = q.IdNivel,
                            NomNivel = q.NomNivel,
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

        public aca_NivelAcademico_Info getInfo(int IdEmpresa, int IdNivel)
        {
            try
            {
                aca_NivelAcademico_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdNivel == IdNivel).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_NivelAcademico_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNivel = Entity.IdNivel,
                        NomNivel = Entity.NomNivel,
                        Orden = Entity.Orden,
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
                    var lst = from q in Context.aca_NivelAcademico
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNivel) + 1;
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
                    var lst = from q in Context.aca_NivelAcademico
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.Orden) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_NivelAcademico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_NivelAcademico Entity = new aca_NivelAcademico
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNivel = info.IdNivel = getId(info.IdEmpresa),
                        NomNivel = info.NomNivel,
                        Orden = info.Orden,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_NivelAcademico.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_NivelAcademico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_NivelAcademico Entity = Context.aca_NivelAcademico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomNivel = info.NomNivel;
                    Entity.Orden = info.Orden;
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

        public bool anularDB(aca_NivelAcademico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_NivelAcademico Entity = Context.aca_NivelAcademico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel);
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
