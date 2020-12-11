using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_PlantillaTipo_Data
    {
        public List<aca_PlantillaTipo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_PlantillaTipo_Info> Lista = new List<aca_PlantillaTipo_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_PlantillaTipo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados == true ? q.Estado : true)).Select(q => new aca_PlantillaTipo_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdTipoPlantilla = q.IdTipoPlantilla,
                        NomPlantillaTipo = q.NomPlantillaTipo,
                        EsBeca = q.EsBeca,
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

        private int GetId(int IdEmpresa)
        {
            try
            {
                int ID = 1;
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    int Cont = db.aca_PlantillaTipo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (Cont > 0)
                        ID = db.aca_PlantillaTipo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdTipoPlantilla) + 1;
                    
                }
                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_PlantillaTipo_Info getInfo(int IdEmpresa, int IdTipoPlantilla)
        {
            try
            {
                aca_PlantillaTipo_Info info = new aca_PlantillaTipo_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_PlantillaTipo.Where(q => q.IdEmpresa == IdEmpresa  && q.IdTipoPlantilla == IdTipoPlantilla).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_PlantillaTipo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdTipoPlantilla = Entity.IdTipoPlantilla,
                        NomPlantillaTipo = Entity.NomPlantillaTipo,
                        EsBeca = Entity.EsBeca??false,
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

        public  bool GuardarDB(aca_PlantillaTipo_Info info)
        {
            try
            {
                EntitiesAcademico dbAca = new EntitiesAcademico();
                
                #region Cabecera
                aca_PlantillaTipo Entity = new aca_PlantillaTipo
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoPlantilla = info.IdTipoPlantilla = GetId(info.IdEmpresa),
                    NomPlantillaTipo = info.NomPlantillaTipo,
                    EsBeca = (info.EsBeca==null) ? false : info.EsBeca,
                    Estado = true,
                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                    FechaCreacion =DateTime.Now,

                };

                #endregion
                dbAca.aca_PlantillaTipo.Add(Entity);
                dbAca.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_PlantillaTipo_Info info)
        {
            try
            {
                EntitiesAcademico dbAca = new EntitiesAcademico();

                #region Cabecera
                var Entity = dbAca.aca_PlantillaTipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoPlantilla == info.IdTipoPlantilla).FirstOrDefault();
                if (Entity == null)
                    return false;
                Entity.NomPlantillaTipo = info.NomPlantillaTipo;
                Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                Entity.EsBeca = (info.EsBeca == null) ? false : info.EsBeca;
                Entity.FechaModificacion = DateTime.Now;
                #endregion

                dbAca.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(aca_PlantillaTipo_Info info)
        {
            try
            {
                EntitiesAcademico dbAca = new EntitiesAcademico();

                #region Cabecera
                var Entity = dbAca.aca_PlantillaTipo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoPlantilla == info.IdTipoPlantilla).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Estado = false;
                Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                Entity.FechaAnulacion = DateTime.Now;
                Entity.MotivoAnulacion = info.MotivoAnulacion;
                #endregion
                dbAca.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
