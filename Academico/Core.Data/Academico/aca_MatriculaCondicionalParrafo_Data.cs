using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;

namespace Core.Data.Academico
{
    public class aca_MatriculaCondicionalParrafo_Data
    {
      public List<aca_MatriculaCondicionalParrafo_Info> GetList()
        {
            try
            {
                List<aca_MatriculaCondicionalParrafo_Info> Lista = new List<aca_MatriculaCondicionalParrafo_Info>();
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var lst = db.aca_MatriculaCondicionalParrafo.ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new aca_MatriculaCondicionalParrafo_Info
                        {
                            Id = item.Id,
                            IdCatalogo = item.IdCatalogo,
                            Nombre = item.Nombre,
                            Parrafo = item.Parrafo,
                            Orden = item.Orden
                        });
                    }
                }


                    return Lista;
            }
            
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCondicionalParrafo_Info GetInfo(int Id)
        {
            try
            {
                aca_MatriculaCondicionalParrafo_Info info = new aca_MatriculaCondicionalParrafo_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MatriculaCondicionalParrafo.Where(q => q.Id == Id).FirstOrDefault();
                    if (Entity == null)
                        return null;
                    info = new aca_MatriculaCondicionalParrafo_Info
                    {
                        Id = Entity.Id,
                        IdCatalogo = Entity.IdCatalogo,
                        Nombre = Entity.Nombre,
                        Parrafo = Entity.Parrafo,
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

        public bool GuardarDB(aca_MatriculaCondicionalParrafo_Info info)
        {
            try
            {
                EntitiesAcademico dbACA = new EntitiesAcademico();

                #region Cabecera
                aca_MatriculaCondicionalParrafo Entity = new aca_MatriculaCondicionalParrafo
                {
                    Id = info.Id,
                    IdCatalogo = info.IdCatalogo,
                    Nombre = info.Nombre,
                    Parrafo = info.Parrafo,
                    Orden = info.Orden,
                    Estado = true,
                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now
                };
                #endregion
                #region MyRegion

                #endregion
                dbACA.aca_MatriculaCondicionalParrafo.Add(Entity);
                dbACA.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_MatriculaCondicionalParrafo_Info info)
        {
            try
            {
                EntitiesAcademico dbACA = new EntitiesAcademico();
                #region Cabecera

                int Id = 0;
                var Entity = dbACA.aca_MatriculaCondicionalParrafo.Where(q => q.Id == Id).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Parrafo = info.Parrafo;
                Entity.Nombre = info.Nombre;
                Entity.FechaModificacion = DateTime.Now;
                Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                #endregion


                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(aca_MatriculaCondicionalParrafo_Info info)
        {
            try
            {
                EntitiesAcademico dbACA = new EntitiesAcademico();

                #region Cabecera

                int Id = 0;
                var Entity = dbACA.aca_MatriculaCondicionalParrafo.Where(q => q.Id == Id).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Estado = false;
                Entity.FechaAnulacion = DateTime.Now;
                Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                Entity.MotivoAnulacion = info.MotivoAnulacion;
                #endregion

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
	}
    
}
