using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_SeguimientoCartera_Data
    {
        public List<cxc_SeguimientoCartera_Info> getList(int IdEmpresa, decimal IdAlumno, bool MostrarAnulados, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                decimal IdAlumno_ini = IdAlumno;
                decimal IdAlumno_fin = IdAlumno == 0 ? 999999 : IdAlumno;
                List<cxc_SeguimientoCartera_Info> Lista = new List<cxc_SeguimientoCartera_Info>();

                using (EntitiesCuentasPorCobrar odata = new EntitiesCuentasPorCobrar())
                {
                    var lst = odata.vwcxc_SeguimientoCartera.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno>= IdAlumno_ini && q.IdAlumno<=IdAlumno_fin && q.Fecha>=fecha_ini && q.Fecha <=fecha_fin && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderByDescending(q => q.Fecha).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new cxc_SeguimientoCartera_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSeguimiento = q.IdSeguimiento,
                            IdAlumno = q.IdAlumno,
                            Codigo =q.Codigo,
                            NombreAlumno = q.pe_nombreCompleto,
                            Fecha = q.Fecha,
                            Observacion = q.Observacion,
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

        public List<cxc_SeguimientoCartera_Info> getList_x_Alumno(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<cxc_SeguimientoCartera_Info> Lista = new List<cxc_SeguimientoCartera_Info>();

                using (EntitiesCuentasPorCobrar odata = new EntitiesCuentasPorCobrar())
                {
                    var lst = odata.cxc_SeguimientoCartera.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).OrderByDescending(q=>q.Fecha).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new cxc_SeguimientoCartera_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSeguimiento = q.IdSeguimiento,
                            IdAlumno = q.IdAlumno,
                            Fecha = q.Fecha,
                            Observacion = q.Observacion,
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

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var cont = Context.cxc_SeguimientoCartera.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.cxc_SeguimientoCartera.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdSeguimiento) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_SeguimientoCartera_Info get_info(int IdEmpresa, int IdSeguimiento)
        {
            try
            {
                cxc_SeguimientoCartera_Info info = new cxc_SeguimientoCartera_Info();
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_SeguimientoCartera Entity = Context.cxc_SeguimientoCartera.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSeguimiento == IdSeguimiento);
                    if (Entity == null) return null;
                    info = new cxc_SeguimientoCartera_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSeguimiento = Entity.IdSeguimiento,
                        Estado = Entity.Estado,
                        IdAlumno = Entity.IdAlumno,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion
                    };
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(cxc_SeguimientoCartera_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_SeguimientoCartera Entity = new cxc_SeguimientoCartera
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSeguimiento = info.IdSeguimiento = getId(info.IdEmpresa),
                        IdAlumno = info.IdAlumno,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.cxc_SeguimientoCartera.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool anularDB(cxc_SeguimientoCartera_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_SeguimientoCartera Entity = Context.cxc_SeguimientoCartera.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSeguimiento == info.IdSeguimiento);
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
