using Core.Data.Base;
using Core.Data.General;
using Core.Info.Facturacion;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
    public class fa_AplicacionMasiva_Data
    {
        public List<fa_AplicacionMasiva_Info> get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                List<fa_AplicacionMasiva_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.fa_AplicacionMasiva
                             where q.IdEmpresa == IdEmpresa
                             && Fecha_ini <= q.Fecha
                             && q.Fecha <= Fecha_fin
                             && q.Estado == (MostrarAnulados == true ? q.Estado : true)
                             select new fa_AplicacionMasiva_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdAplicacion = q.IdAplicacion,
                                 Fecha = q.Fecha,
                                 Observacion = q.Observacion,
                                 Estado = q.Estado
                             }).ToList();
                }

                return Lista.OrderByDescending(q => q.IdAplicacion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public fa_AplicacionMasiva_Info get_info(int IdEmpresa, decimal IdAplicacion)
        {
            try
            {
                fa_AplicacionMasiva_Info info;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_AplicacionMasiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdAplicacion == IdAplicacion).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_AplicacionMasiva_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAplicacion = Entity.IdAplicacion,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
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

        private decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = from q in Context.fa_AplicacionMasiva
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdAplicacion) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(fa_AplicacionMasiva_Info info)
        {
            try
            {
                #region Variables
                int Secuencia = 1;
                #endregion

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    #region Cabecera
                    fa_AplicacionMasiva Entity = new fa_AplicacionMasiva
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAplicacion = info.IdAplicacion = get_id(info.IdEmpresa),
                        Fecha = info.Fecha.Date,
                        Observacion = info.Observacion,
                        Estado = info.Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    #endregion

                    #region Detalle
                    foreach (var item in info.lst_det)
                    {
                        Context.fa_AplicacionMasivaDet.Add(new fa_AplicacionMasivaDet
                        {
                            IdEmpresa = item.IdEmpresa = info.IdEmpresa,
                            IdAplicacion = item.IdAplicacion = info.IdAplicacion,
                            Secuencia = item.Secuencia = Secuencia++,
                            IdAlumno = item.IdAlumno,
                            Saldo = item.Saldo
                        });
                    }
                    #endregion

                    Context.fa_AplicacionMasiva.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_AplicacionMasiva_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }

        public bool anularDB(fa_AplicacionMasiva_Info info)
        {
            try
            {
                using (EntitiesFacturacion db_f = new EntitiesFacturacion())
                {
                    var entity = db_f.fa_AplicacionMasiva.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAplicacion == info.IdAplicacion).FirstOrDefault();
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
