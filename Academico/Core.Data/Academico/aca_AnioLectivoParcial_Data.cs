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
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio).OrderBy(q => q.Orden).ToList();

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
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden = q.Orden
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

        public List<aca_AnioLectivoParcial_Info> getList_x_Tipo(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoTipo == IdCatalogoTipo).OrderBy(q=>q.Orden).ToList();

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
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden = q.Orden
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

        public aca_AnioLectivoParcial_Info getInfo(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoParcial)
        {
            try
            {
                aca_AnioLectivoParcial_Info info = new aca_AnioLectivoParcial_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.aca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoParcial == IdCatalogoParcial).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoParcial_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdCatalogoParcial = Entity.IdCatalogoParcial,
                        EsExamen = Entity.EsExamen,
                        ValidaEstadoAlumno = Entity.ValidaEstadoAlumno,
                        Orden = Entity.Orden
                    };

                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivoParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo, DateTime FechaActual)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoTipo == IdCatalogoTipo && FechaActual >= q.FechaInicio && FechaActual <= q.FechaFin).OrderBy(q => q.Orden).ToList();

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
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden=q.Orden
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
                            EsExamen = item.EsExamen,
                            Orden = item.Orden,
                            ValidaEstadoAlumno = item.ValidaEstadoAlumno,
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
                    Entity.EsExamen = info.EsExamen;
                    Entity.ValidaEstadoAlumno = info.ValidaEstadoAlumno;
                    Entity.Orden = info.Orden;
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
