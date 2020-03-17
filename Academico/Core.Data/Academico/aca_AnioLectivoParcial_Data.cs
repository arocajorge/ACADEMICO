using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoParcial_Data
    {
        public List<aca_AnioLectivoParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdAnio = q.IdAnio,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            NomCatalogo = q.NomCatalogo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin
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

        public bool guardarDB(List<aca_AnioLectivoParcial_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in lista)
                    {
                        aca_AnioLectivoParcial Entity = new aca_AnioLectivoParcial
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnio = item.IdAnio,
                            IdSede = item.IdSede,
                            IdCatalogoParcial = item.IdCatalogoParcial,
                            FechaInicio = item.FechaInicio,
                            FechaFin = item.FechaFin,
                            IdUsuarioCreacion = item.IdUsuarioCreacion,
                            FechaCreacion = DateTime.Now
                        };
                        Context.aca_AnioLectivoParcial.Add(Entity);
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivoParcial_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoParcial Entity = Context.aca_AnioLectivoParcial.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSede == info.IdSede && q.IdCatalogoParcial== info.IdCatalogoParcial);
                    if (Entity == null)
                        return false;

                    Entity.FechaInicio = info.FechaInicio;
                    Entity.FechaFin = info.FechaFin;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

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
