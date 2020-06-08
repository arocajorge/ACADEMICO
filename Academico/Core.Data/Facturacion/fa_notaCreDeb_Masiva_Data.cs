using Core.Data.Base;
using Core.Data.Facturacion;
using Core.Data.General;
using Core.Data.Inventario;
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
        fa_TipoNota_Data odata_tipo_nota = new fa_TipoNota_Data();
        tb_sis_Impuesto_Data odata_impuesto = new tb_sis_Impuesto_Data();
        in_Producto_Data odata_producto = new in_Producto_Data();
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
                                no_fecha_venc = q.no_fecha_venc,
                                CreDeb = q.CreDeb,
                                sc_observacion = q.sc_observacion,
                                NaturalezaNota = q.NaturalezaNota,
                                Estado = q.Estado
                             }).ToList();
                }

                return Lista.OrderByDescending(q => q.IdNCMasivo).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public fa_notaCreDeb_Masiva_Info get_info(int IdEmpresa, decimal IdNCMasivo)
        {
            try
            {
                fa_notaCreDeb_Masiva_Info info;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_notaCreDeb_Masiva.Where(q => q.IdEmpresa == IdEmpresa && q.IdNCMasivo == IdNCMasivo).FirstOrDefault();
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
                    var info_tipo_nota = odata_tipo_nota.get_info(info.IdEmpresa, info.IdTipoNota);
                    var info_producto = odata_producto.get_info(info.IdEmpresa, Convert.ToDecimal(info_tipo_nota.IdProducto));
                    var info_ImpuestoIVA = odata_impuesto.get_info(info_producto.IdCod_Impuesto_Iva);

                    foreach (var item in info.lst_det)
                    {
                        var ValorIVA = Math.Round(Convert.ToDouble(item.Subtotal * (info_ImpuestoIVA.porcentaje / 100)), 2, MidpointRounding.AwayFromZero);
                        var ValorTotal = Math.Round((Convert.ToDouble(item.Subtotal) + ValorIVA), 2, MidpointRounding.AwayFromZero);

                        Context.fa_notaCreDeb_MasivaDet.Add(new fa_notaCreDeb_MasivaDet
                        {
                            IdEmpresa = item.IdEmpresa = info.IdEmpresa,
                            IdNCMasivo = item.IdNCMasivo = info.IdNCMasivo,
                            Secuencia = item.Secuencia = Secuencia++,
                            IdAlumno = item.IdAlumno,
                            Subtotal = item.Subtotal,
                            IVA = ValorIVA,
                            vt_por_iva = info_ImpuestoIVA.porcentaje,
                            IdCod_Impuesto_Iva = info_ImpuestoIVA.IdCod_Impuesto,
                            Total = ValorTotal,
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

        public bool anularDB(fa_notaCreDeb_Masiva_Info info)
        {
            try
            {
                using (EntitiesFacturacion db_f = new EntitiesFacturacion())
                {
                    var entity = db_f.fa_notaCreDeb_Masiva.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdNCMasivo == info.IdNCMasivo).FirstOrDefault();
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
