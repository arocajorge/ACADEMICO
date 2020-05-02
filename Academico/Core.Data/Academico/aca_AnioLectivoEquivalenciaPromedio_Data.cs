using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoEquivalenciaPromedio_Data
    {
        public List<aca_AnioLectivoEquivalenciaPromedio_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivoEquivalenciaPromedio_Info> Lista = new List<aca_AnioLectivoEquivalenciaPromedio_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoEquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoEquivalenciaPromedio_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdEquivalenciaPromedio = q.IdEquivalenciaPromedio,
                            DescripcionAnio = q.DescripcionAnio,
                            Descripcion = q.Descripcion,
                            Codigo = q.Codigo,
                            ValorMinimo = q.ValorMinimo,
                            ValorMaximo = q.ValorMaximo,
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

        public aca_AnioLectivoEquivalenciaPromedio_Info getInfo(int IdEmpresa, int IdAnio, int IdEquivalenciaPromedio)
        {
            try
            {
                aca_AnioLectivoEquivalenciaPromedio_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivoEquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdEquivalenciaPromedio == IdEquivalenciaPromedio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoEquivalenciaPromedio_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdEquivalenciaPromedio = Entity.IdEquivalenciaPromedio,
                        Descripcion = Entity.Descripcion,
                        Codigo = Entity.Codigo,
                        ValorMinimo = Entity.ValorMinimo,
                        ValorMaximo = Entity.ValorMaximo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoEquivalenciaPromedio_Info getInfo_x_Promedio(int IdEmpresa, int IdAnio, decimal PromedioFinal)
        {
            try
            {
                aca_AnioLectivoEquivalenciaPromedio_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivoEquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && PromedioFinal >= q.ValorMinimo && PromedioFinal<= q.ValorMaximo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoEquivalenciaPromedio_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdEquivalenciaPromedio = Entity.IdEquivalenciaPromedio,
                        Descripcion = Entity.Descripcion,
                        Codigo = Entity.Codigo,
                        ValorMinimo = Entity.ValorMinimo,
                        ValorMaximo = Entity.ValorMaximo
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
                    var cont = Context.aca_AnioLectivoEquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivoEquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdEquivalenciaPromedio) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivoEquivalenciaPromedio_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoEquivalenciaPromedio Entity = new aca_AnioLectivoEquivalenciaPromedio
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdEquivalenciaPromedio = info.IdEquivalenciaPromedio = getId(info.IdEmpresa),
                        Descripcion = info.Descripcion,
                        Codigo = info.Codigo,
                        ValorMinimo = info.ValorMinimo,
                        ValorMaximo = info.ValorMaximo,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_AnioLectivoEquivalenciaPromedio.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivoEquivalenciaPromedio_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoEquivalenciaPromedio Entity = Context.aca_AnioLectivoEquivalenciaPromedio.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEquivalenciaPromedio == info.IdEquivalenciaPromedio);
                    if (Entity == null)
                        return false;

                    Entity.IdAnio = info.IdAnio;
                    Entity.Descripcion = info.Descripcion;
                    Entity.Codigo = info.Codigo;
                    Entity.ValorMinimo = info.ValorMinimo;
                    Entity.ValorMaximo = info.ValorMaximo;
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

        public bool anularDB(aca_AnioLectivoEquivalenciaPromedio_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoEquivalenciaPromedio Entity = Context.aca_AnioLectivoEquivalenciaPromedio.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdEquivalenciaPromedio == info.IdEquivalenciaPromedio);
                    if (Entity == null)
                        return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

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
