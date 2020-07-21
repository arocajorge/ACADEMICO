using Core.Data.Base;
using Core.Data.General;
using Core.Info.CuentasPorCobrar;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_CobroMasivo_Data
    {
        public List<cxc_CobroMasivo_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                List<cxc_CobroMasivo_Info> Lista= new List<cxc_CobroMasivo_Info>();
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = Context.cxc_CobroMasivo.Where(q => q.IdEmpresa == IdEmpresa && Fecha_ini <= q.Fecha && q.Fecha <= Fecha_fin && q.Estado == (MostrarAnulados == true ? q.Estado : true)).ToList();
                    lst.ForEach(q =>
                    {
                        Lista.Add(new cxc_CobroMasivo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdCobroMasivo = q.IdCobroMasivo,
                            Fecha = q.Fecha,
                            Total =q.Total,
                            Observacion = q.Observacion,
                            Estado = q.Estado
                        });
                    });
                }

                return Lista.OrderByDescending(q => q.IdCobroMasivo).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public cxc_CobroMasivo_Info get_info(int IdEmpresa, decimal IdCobroMasivo)
        {
            try
            {
                cxc_CobroMasivo_Info info;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var Entity = Context.cxc_CobroMasivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdCobroMasivo == IdCobroMasivo).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new cxc_CobroMasivo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdCobroMasivo = Entity.IdCobroMasivo,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado,
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = from q in Context.cxc_CobroMasivo
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCobroMasivo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(cxc_CobroMasivo_Info info)
        {
            try
            {
                #region Variables
                int Secuencia = 1;
                #endregion

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    #region Cabecera
                    cxc_CobroMasivo Entity = new cxc_CobroMasivo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCobroMasivo = info.IdCobroMasivo = get_id(info.IdEmpresa),
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        Total = info.Total,
                        Estado = info.Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    #endregion

                    #region Detalle
                    foreach (var item in info.lst_det)
                    {
                        Context.cxc_CobroMasivoDet.Add(new cxc_CobroMasivoDet
                        {
                            IdEmpresa = item.IdEmpresa = info.IdEmpresa,
                            IdCobroMasivo = item.IdCobroMasivo = info.IdCobroMasivo,
                            Secuencia = item.Secuencia = Secuencia++,
                            IdAlumno = item.IdAlumno,
                            Valor = item.Valor,
                            Fecha = item.Fecha,
                        });
                    }
                    #endregion

                    Context.cxc_CobroMasivo.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "cxc_CobroMasivo_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool anularDB(cxc_CobroMasivo_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar db_f = new EntitiesCuentasPorCobrar())
                {
                    var entity = db_f.cxc_CobroMasivo.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCobroMasivo == info.IdCobroMasivo).FirstOrDefault();
                    if (entity == null) return false;

                    entity.Estado = false;
                    entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    entity.FechaAnulacion = DateTime.Now;
                    entity.MotivoAnulacion = info.MotivoAnulacion;

                    db_f.SaveChanges();
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
