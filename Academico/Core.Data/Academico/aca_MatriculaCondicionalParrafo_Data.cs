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
      public List<aca_MatriculaCondicionalParrafo_Info> GetList(bool MostrarAnulados)
        {
            try
            {
                List<aca_MatriculaCondicionalParrafo_Info> Lista = new List<aca_MatriculaCondicionalParrafo_Info>();
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var lst = db.aca_MatriculaCondicionalParrafo.Where(q=> q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new aca_MatriculaCondicionalParrafo_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdParrafo = item.IdParrafo,
                            IdCatalogoCONDIC = item.IdCatalogoCONDIC,
                            Nombre = item.Nombre,
                            Parrafo = item.Parrafo,
                            Orden = item.Orden,
                            Estado = item.Estado
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

        private int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    int count = Context.aca_MatriculaCondicionalParrafo.Where(q=>q.IdEmpresa==IdEmpresa).Count();
                    if (count > 0)
                    ID = Context.aca_MatriculaCondicionalParrafo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdParrafo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCondicionalParrafo_Info GetInfo(int IdEmpresa, int IdParrafo)
        {
            try
            {
                aca_MatriculaCondicionalParrafo_Info info = new aca_MatriculaCondicionalParrafo_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MatriculaCondicionalParrafo.Where(q => q.IdEmpresa==IdEmpresa && q.IdParrafo == IdParrafo).FirstOrDefault();
                    if (Entity == null)
                        return null;
                    info = new aca_MatriculaCondicionalParrafo_Info
                    {
                        IdEmpresa=Entity.IdEmpresa,
                        IdParrafo = Entity.IdParrafo,
                        IdCatalogoCONDIC = Entity.IdCatalogoCONDIC,
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
                    IdEmpresa = info.IdEmpresa,
                    IdParrafo = getId(info.IdEmpresa),
                    IdCatalogoCONDIC = info.IdCatalogoCONDIC,
                    Nombre = info.Nombre,
                    Parrafo = info.Parrafo,
                    Orden = info.Orden,
                    Estado = true,
                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                    FechaCreacion = DateTime.Now
                };
                #endregion
              
                dbACA.aca_MatriculaCondicionalParrafo.Add(Entity);
                dbACA.SaveChanges();

                return true;
            }
            catch (Exception ex)
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
                
                var Entity = dbACA.aca_MatriculaCondicionalParrafo.Where(q => q.IdEmpresa==info.IdEmpresa && q.IdParrafo == info.IdParrafo).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Parrafo = info.Parrafo;
                Entity.Nombre = info.Nombre;
                Entity.Orden = info.Orden;
                Entity.FechaModificacion = DateTime.Now;
                Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                #endregion
                dbACA.SaveChanges();

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
                var Entity = dbACA.aca_MatriculaCondicionalParrafo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdParrafo == info.IdParrafo).FirstOrDefault();
                if (Entity == null)
                    return false;

                Entity.Estado = false;
                Entity.FechaAnulacion = DateTime.Now;
                Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                Entity.MotivoAnulacion = info.MotivoAnulacion;

                dbACA.SaveChanges();
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
