using Core.Data.Base;
using Core.Data.Facturacion;
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
    public class fa_notaCreDeb_Masiva_Data
    {
        public List<fa_notaCreDeb_Masiva_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                List<fa_notaCreDeb_Masiva_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.fa_notaCreDeb_Masiva
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && Fecha_ini <= q.no_fecha
                             && q.no_fecha <= Fecha_fin
                             && q.Estado == (MostrarAnulados == true ? q.Estado : true)
                             select new fa_notaCreDeb_Masiva_Info
                             {
                                IdEmpresa = q.IdEmpresa,
                                IdSucursal = q.IdSucursal,
                                IdBodega = q.IdBodega,
                                IdNCMasivo = q.IdNCMasivo,
                                no_fecha = q.no_fecha,
                                no_fecha_venc = q.no_fecha_venc
                             }).ToList();
                }

                return Lista.OrderByDescending(q => q.IdNCMasivo).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public fa_notaCreDeb_Masiva_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNCMasivo)
        {
            try
            {
                fa_notaCreDeb_Masiva_Info info;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_notaCreDeb_Masiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNCMasivo == IdNCMasivo).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_notaCreDeb_Masiva_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdNCMasivo = Entity.IdNCMasivo,
                        IdPuntoVta = Entity.IdPuntoVta,
                        CreDeb = Entity.CreDeb.Trim(),
                        no_fecha = Entity.no_fecha,
                        no_fecha_venc = Entity.no_fecha_venc,
                        IdTipoNota = Entity.IdTipoNota,
                        sc_observacion = Entity.sc_observacion,
                        Estado = Entity.Estado,
                        NaturalezaNota = Entity.NaturalezaNota
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
                    var lst = from q in Context.fa_notaCreDeb_Masiva
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNCMasivo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(fa_notaCreDeb_Masiva_Info info)
        {
            try
            {
                #region Variables
                int Secuencia = 1;
                #endregion

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    #region Cabecera
                    fa_notaCreDeb_Masiva Entity = new fa_notaCreDeb_Masiva
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNCMasivo = info.IdNCMasivo = get_id(info.IdEmpresa),
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdPuntoVta = info.IdPuntoVta,
                        CreDeb = info.CreDeb.Trim(),
                        no_fecha = info.no_fecha.Date,
                        no_fecha_venc = info.no_fecha_venc.Date,
                        IdTipoNota = info.IdTipoNota,
                        NaturalezaNota = info.NaturalezaNota,
                        sc_observacion = info.sc_observacion,
                        Estado = info.Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    #endregion

                    #region Detalle
                    foreach (var item in info.lst_det)
                    {
                        Context.fa_notaCreDeb_MasivaDet.Add(new fa_notaCreDeb_MasivaDet
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdNCMasivo = info.IdNCMasivo,
                            Secuencia = Secuencia++,
                            IdAlumno = item.IdAlumno,
                            Subtotal = item.Subtotal,
                            IVA = item.IVA,
                            vt_por_iva = item.vt_por_iva,
                            IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                            Total = item.Total,
                            ObservacionDet = item.ObservacionDet,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdNota = item.IdNota
                        });
                    }
                    #endregion

                    Context.fa_notaCreDeb_Masiva.Add(Entity);
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Masiva_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuarioCreacion });
                return false;
            }
        }
       
    }
}
