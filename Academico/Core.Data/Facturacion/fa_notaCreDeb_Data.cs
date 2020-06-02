using Core.Data.Base;
using Core.Data.Caja;
using Core.Data.Contabilidad;
using Core.Data.CuentasPorCobrar;
using Core.Data.General;
using Core.Info.Caja;
using Core.Info.Contabilidad;
using Core.Info.CuentasPorCobrar;
using Core.Info.Facturacion;
using Core.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.Facturacion
{
    public class fa_notaCreDeb_Data
    {
        #region Variables
        cxc_cobro_tipo_Data odat_TipoCobro = new cxc_cobro_tipo_Data();
        caj_Caja_Movimiento_Data odata_MovimientoCaja = new caj_Caja_Movimiento_Data();
        ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
        fa_notaCreDeb_det_Data odata_Det = new fa_notaCreDeb_det_Data();
        fa_notaCreDeb_x_fa_factura_NotaDeb_Data odata_DetNDFac = new fa_notaCreDeb_x_fa_factura_NotaDeb_Data();
        #endregion

        caj_Caja_Movimiento_Data odataMovCaja = new caj_Caja_Movimiento_Data();
        public List<fa_notaCreDeb_consulta_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, string CreDeb)
        {
            try
            {
                List<fa_notaCreDeb_consulta_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && Fecha_ini <= q.no_fecha
                             && q.no_fecha <= Fecha_fin
                             && q.CreDeb == CreDeb
                             select new fa_notaCreDeb_consulta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 CreDeb = q.CreDeb,
                                 NumNota_Impresa = q.NumNota_Impresa,
                                 no_fecha = q.no_fecha,
                                 Nombres = q.Nombres,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_iva = q.sc_iva,
                                 sc_total = q.sc_total,
                                 Estado = q.Estado,

                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<fa_notaCreDeb_consulta_Info> get_list_academico(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, string CreDeb)
        {
            try
            {
                List<fa_notaCreDeb_consulta_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && Fecha_ini <= q.no_fecha
                             && q.no_fecha <= Fecha_fin
                             && q.CreDeb == CreDeb
                             && q.IdAlumno!= null
                             select new fa_notaCreDeb_consulta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 CreDeb = q.CreDeb,
                                 NumNota_Impresa = q.NumNota_Impresa,
                                 no_fecha = q.no_fecha,
                                 Nombres = q.Nombres,
                                 NombresAlumno=q.pe_nombreCompleto,
                                 Usuario=q.IdUsuario,
                                 sc_subtotal = q.sc_subtotal,
                                 sc_iva = q.sc_iva,
                                 sc_total = q.sc_total,
                                 Estado = q.Estado,
                                 NaturalezaNota =q.NaturalezaNota,
                                 No_Descripcion = q.No_Descripcion,
                                 NumAutorizacion = q.NumAutorizacion,
                                 Fecha_Autorizacion = q.Fecha_Autorizacion,
                                 IdUsuario = q.IdUsuario,
                                 EstadoBool = q.Estado == "A" ? true : false
                             }).ToList();
                }

                return Lista.OrderByDescending(q=> q.IdNota).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public fa_notaCreDeb_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                fa_notaCreDeb_Info info;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_notaCreDeb.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_notaCreDeb_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdNota = Entity.IdNota,
                        IdPuntoVta = Entity.IdPuntoVta,
                        dev_IdEmpresa = Entity.dev_IdEmpresa,
                        dev_IdDev_Inven = Entity.dev_IdDev_Inven,
                        CodNota = Entity.CodNota,
                        CreDeb = Entity.CreDeb.Trim(),
                        CodDocumentoTipo = Entity.CodDocumentoTipo,
                        Serie1 = Entity.Serie1,
                        Serie2 = Entity.Serie2,
                        NumNota_Impresa = Entity.NumNota_Impresa,
                        NumAutorizacion = Entity.NumAutorizacion,
                        Fecha_Autorizacion = Entity.Fecha_Autorizacion,
                        IdCliente = Entity.IdCliente,
                        IdAlumno = Entity.IdAlumno,
                        no_fecha = Entity.no_fecha,
                        no_fecha_venc = Entity.no_fecha_venc,
                        IdTipoNota = Entity.IdTipoNota,
                        sc_observacion = Entity.sc_observacion,
                        Estado = Entity.Estado,
                        NaturalezaNota = Entity.NaturalezaNota,
                        IdCtaCble_TipoNota = Entity.IdCtaCble_TipoNota,
                        IdCobro_tipo = Entity.IdCobro_tipo
                    };

                    info.info_resumen = new fa_notaCreDeb_resumen_Info();
                    var EntityResumen = Context.fa_notaCreDeb_resumen.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (EntityResumen==null)
                    {
                        info.info_resumen = new fa_notaCreDeb_resumen_Info();
                    }
                    else
                    {
                        info.info_resumen = new fa_notaCreDeb_resumen_Info
                        {
                            IdEmpresa = EntityResumen.IdEmpresa,
                            IdSucursal = EntityResumen.IdSucursal,
                            IdBodega = EntityResumen.IdBodega,
                            IdNota = EntityResumen.IdNota,
                            SubtotalIVASinDscto = EntityResumen.SubtotalIVASinDscto,
                            SubtotalSinIVASinDscto = EntityResumen.SubtotalSinIVASinDscto,
                            SubtotalSinDscto = EntityResumen.SubtotalSinDscto,
                            Descuento = EntityResumen.Descuento,
                            SubtotalIVAConDscto = EntityResumen.SubtotalIVAConDscto,
                            SubtotalSinIVAConDscto = EntityResumen.SubtotalSinIVAConDscto,
                            SubtotalConDscto = EntityResumen.SubtotalConDscto,
                            IdCod_Impuesto_IVA = EntityResumen.IdCod_Impuesto_IVA,
                            ValorIVA = EntityResumen.ValorIVA,
                            Total = EntityResumen.Total,
                            PorIva = EntityResumen.PorIva
                        };
                    }
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DocumentoExiste(int IdEmpresa, string CodDocumentoTipo, string Serie1, string Serie2, string NumNota_Impresa)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = from q in Context.fa_notaCreDeb
                              where q.IdEmpresa == IdEmpresa
                              && q.CodDocumentoTipo == CodDocumentoTipo
                              && q.Serie1 == Serie1
                              && q.Serie2 == Serie2
                              && q.NumNota_Impresa == NumNota_Impresa
                              select q;

                    if (lst.Count() > 0)
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private decimal get_id(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = from q in Context.fa_notaCreDeb
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              && q.IdBodega == IdBodega
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdNota) + 1;
                }

                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardarDB(fa_notaCreDeb_Info info)
        {            
            try
            {
                #region Variables
                int Secuencia = 1;
                
                cxc_cobro_Data odata_cobr = new cxc_cobro_Data();
                
                #endregion

                using (EntitiesFacturacion db_f = new EntitiesFacturacion())
                {
                    var TipoNota = db_f.fa_TipoNota.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota).FirstOrDefault();
                    if (TipoNota != null)
                    {
                        info.IdCtaCble_TipoNota = info.CreDeb == "D" ? TipoNota.IdCtaCbleCXC : TipoNota.IdCtaCble;
                    }

                    #region Nota de debito credito

                    #region Cabecera
                    fa_notaCreDeb Entity = new fa_notaCreDeb
                    //db_f.fa_notaCreDeb.Add(new fa_notaCreDeb
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdNota = info.IdNota = get_id(info.IdEmpresa, info.IdSucursal, info.IdBodega),
                        IdPuntoVta = info.IdPuntoVta,
                        CodNota = info.CodNota,
                        CreDeb = info.CreDeb.Trim(),
                        CodDocumentoTipo = info.CodDocumentoTipo,
                        Serie1 = info.Serie1,
                        Serie2 = info.Serie2,
                        NumNota_Impresa = info.NumNota_Impresa,
                        NumAutorizacion = info.NumAutorizacion,
                        Fecha_Autorizacion = info.Fecha_Autorizacion,
                        IdCliente = info.IdCliente,
                        IdAlumno = info.IdAlumno,
                        no_fecha = info.no_fecha.Date,
                        no_fecha_venc = info.no_fecha_venc.Date,
                        IdTipoNota = info.IdTipoNota,
                        sc_observacion = info.sc_observacion,
                        Estado = info.Estado = "A",
                        NaturalezaNota = info.NaturalezaNota,
                        IdCtaCble_TipoNota = info.IdCtaCble_TipoNota,
                        IdCobro_tipo = string.IsNullOrEmpty(info.IdCobro_tipo) ? (info.CreDeb == "C" ? "NTCR" : "NTDB"): info.IdCobro_tipo,
                        IdUsuario = info.IdUsuario,
                    };
                    #endregion

                    #region Detalle
                    foreach (var item in info.lst_det)
                    {
                        db_f.fa_notaCreDeb_det.Add(new fa_notaCreDeb_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdNota = info.IdNota,
                            Secuencia = Secuencia++,
                            IdProducto = item.IdProducto,
                            sc_cantidad = item.sc_cantidad,
                            sc_cantidad_factura=item.sc_cantidad_factura,
                            sc_Precio = item.sc_Precio,
                            sc_descUni = item.sc_descUni,
                            sc_PordescUni = item.sc_PordescUni,
                            sc_precioFinal = item.sc_precioFinal,
                            vt_por_iva = item.vt_por_iva,
                            sc_iva = item.sc_iva,
                            IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                            sc_subtotal = item.sc_subtotal,
                            sc_total = item.sc_total,
                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_Cargo = item.IdPunto_Cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        });
                    }
                    #endregion

                    #region Resumen
                    fa_notaCreDeb_resumen Entity_Resumen = new fa_notaCreDeb_resumen
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdNota = info.IdNota,
                        SubtotalIVASinDscto = info.info_resumen.SubtotalIVASinDscto,
                        SubtotalSinIVASinDscto = info.info_resumen.SubtotalSinIVASinDscto,
                        SubtotalSinDscto = info.info_resumen.SubtotalSinDscto,
                        Descuento = info.info_resumen.Descuento,
                        SubtotalIVAConDscto = info.info_resumen.SubtotalIVAConDscto,
                        SubtotalSinIVAConDscto = info.info_resumen.SubtotalSinIVAConDscto,
                        SubtotalConDscto = info.info_resumen.SubtotalConDscto,
                        IdCod_Impuesto_IVA = info.info_resumen.IdCod_Impuesto_IVA,
                        ValorIVA = info.info_resumen.ValorIVA,
                        Total = info.info_resumen.Total,
                        PorIva = info.info_resumen.PorIva,
                        IdAnio = info.info_resumen.IdAnio,
                        IdMatricula= info.info_resumen.IdMatricula
                    };
                    db_f.fa_notaCreDeb_resumen.Add(Entity_Resumen);
                    #endregion

                    #region Cruce
                    Secuencia = 1;
                    foreach (var item in info.lst_cruce)
                    {
                        db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                        {
                            IdEmpresa_nt = info.IdEmpresa,
                            IdSucursal_nt = info.IdSucursal,
                            IdBodega_nt = info.IdBodega,
                            IdNota_nt = info.IdNota,
                            secuencia = Secuencia++,
                            IdEmpresa_fac_nd_doc_mod = item.IdEmpresa_fac_nd_doc_mod,
                            IdSucursal_fac_nd_doc_mod = item.IdSucursal_fac_nd_doc_mod,
                            IdBodega_fac_nd_doc_mod = item.IdBodega_fac_nd_doc_mod,
                            IdCbteVta_fac_nd_doc_mod = item.IdCbteVta_fac_nd_doc_mod,
                            vt_tipoDoc = item.vt_tipoDoc,
                            Valor_Aplicado = item.Valor_Aplicado,
                            fecha_cruce = DateTime.Now,
                            ValorProntoPago = item.ValorProntoPago
                        });
                    }
                    #endregion

                    #region Talonario
                    fa_PuntoVta_Data data_puntovta = new fa_PuntoVta_Data();
                    tb_sis_Documento_Tipo_Talonario_Data data_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
                    fa_PuntoVta_Info info_puntovta = new fa_PuntoVta_Info();
                    tb_sis_Documento_Tipo_Talonario_Info ultimo_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
                    tb_sis_Documento_Tipo_Talonario_Info info_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
                    info_puntovta = data_puntovta.get_info(info.IdEmpresa, info.IdSucursal, info.IdPuntoVta);

                    if (info_puntovta != null && info.NaturalezaNota == "SRI")
                    {
                        if (info_puntovta.EsElectronico == true)
                        {
                            ultimo_talonario = data_talonario.GetUltimoNoUsado(info.IdEmpresa, info_puntovta.codDocumentoTipo, info_puntovta.Su_CodigoEstablecimiento, info_puntovta.cod_PuntoVta, info_puntovta.EsElectronico,true);

                            if (ultimo_talonario != null)
                            {
                                Entity.Serie1 = info.Serie1 = ultimo_talonario.Establecimiento;
                                Entity.Serie2 = info.Serie2 = ultimo_talonario.PuntoEmision;
                                Entity.NumNota_Impresa = info.NumNota_Impresa = ultimo_talonario.NumDocumento;
                            }
                        }
                        else
                        {
                            info_talonario.IdEmpresa = info.IdEmpresa;
                            info_talonario.CodDocumentoTipo = info.CodDocumentoTipo;
                            info_talonario.Establecimiento = info.Serie1;
                            info_talonario.PuntoEmision = info.Serie2;
                            info_talonario.NumDocumento = info.NumNota_Impresa;
                            info_talonario.IdSucursal = info.IdSucursal;
                            info_talonario.Usado = true;

                            data_talonario.modificar_estado_usadoDB(info_talonario);
                        }
                    }
                    else{
                        Entity.Serie1 = null;
                        Entity.Serie2 = null;
                        Entity.NumNota_Impresa = null;
                    }
                    #endregion

                    db_f.fa_notaCreDeb.Add(Entity);
                    db_f.SaveChanges();

                    #endregion

                    #region Contabilidad

                    var TipoCobro = odat_TipoCobro.get_info(info.IdCobro_tipo);
                    if (TipoCobro != null)
                    {
                        ct_cbtecble_Info diario = armar_diario(info);

                        if (diario != null)
                        {
                            if (TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA")
                            {
                                caj_Caja_Movimiento_Info MovimientoCaja = armar_movimiendoCaja(info, diario);
                                if (MovimientoCaja != null)
                                {
                                    if (odata_MovimientoCaja.guardarDB(MovimientoCaja))
                                    {
                                        db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                        {
                                            no_IdEmpresa = info.IdEmpresa,
                                            no_IdSucursal = info.IdSucursal,
                                            no_IdBodega = info.IdBodega,
                                            no_IdNota = info.IdNota,

                                            ct_IdEmpresa = diario.IdEmpresa,
                                            ct_IdTipoCbte = diario.IdTipoCbte,
                                            ct_IdCbteCble = MovimientoCaja.IdCbteCble,

                                            observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                        });
                                        db_f.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                if (odata_ct.guardarDB(diario))
                                {
                                    db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                    {
                                        no_IdEmpresa = info.IdEmpresa,
                                        no_IdSucursal = info.IdSucursal,
                                        no_IdBodega = info.IdBodega,
                                        no_IdNota = info.IdNota,

                                        ct_IdEmpresa = diario.IdEmpresa,
                                        ct_IdTipoCbte = diario.IdTipoCbte,
                                        ct_IdCbteCble = diario.IdCbteCble,

                                        observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                    });
                                    db_f.SaveChanges();
                                }
                            }
                        }
                    }
                    #endregion

                    #region Cobranza
                    if (info.CreDeb.Trim() == "C" && info.lst_cruce.Count != 0)
                    {
                        cxc_cobro_Info cobro = armar_cobro(info);
                        if (cobro != null)
                        {
                            if (odata_cobr.guardarDB(cobro))
                            {
                                db_f.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                                {
                                    IdEmpresa_nt = info.IdEmpresa,
                                    IdSucursal_nt = info.IdSucursal,
                                    IdBodega_nt = info.IdBodega,
                                    IdNota_nt = info.IdNota,
                                    IdEmpresa_cbr = cobro.IdEmpresa,
                                    IdSucursal_cbr = cobro.IdSucursal,
                                    IdCobro_cbr = cobro.IdCobro,
                                    Valor_cobro = Math.Round(info.lst_cruce.Sum(q => q.Valor_Aplicado), 2, MidpointRounding.AwayFromZero)
                                });
                                db_f.SaveChanges();
                            }
                        }
                    }

                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool modificarDB(fa_notaCreDeb_Info info)
        {
            try
            {
                #region Variables
                int Secuencia = 1;
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                cxc_cobro_Data odata_cobr = new cxc_cobro_Data();
                cxc_cobro_tipo_Data odat_TipoCobro = new cxc_cobro_tipo_Data();
                caj_Caja_Movimiento_Data odata_MovimientoCaja = new caj_Caja_Movimiento_Data();
                #endregion

                using (EntitiesFacturacion db_f = new EntitiesFacturacion())
                {
                    var TipoNota = db_f.fa_TipoNota.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota).FirstOrDefault();
                    if (TipoNota != null)
                    {
                        info.IdCtaCble_TipoNota = info.CreDeb == "D" ? TipoNota.IdCtaCbleCXC : TipoNota.IdCtaCble;
                    }

                    #region Nota de debito credito

                    #region Cabecera
                    var entity = db_f.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                    if (entity == null) return false;

                    //entity.IdPuntoVta = info.IdPuntoVta;
                    entity.CodNota = info.CodNota;
                    entity.CreDeb = info.CreDeb.Trim();
                    entity.CodDocumentoTipo = info.CodDocumentoTipo;
                    entity.Serie1 = info.Serie1;
                    entity.Serie2 = info.Serie2;
                    entity.NumNota_Impresa = info.NumNota_Impresa;
                    entity.NumAutorizacion = info.NumAutorizacion;
                    entity.Fecha_Autorizacion = info.Fecha_Autorizacion;
                    entity.IdCliente = info.IdCliente;
                    entity.IdAlumno = info.IdAlumno;
                    entity.no_fecha = info.no_fecha.Date;
                    entity.no_fecha_venc = info.no_fecha_venc.Date;
                    entity.IdTipoNota = info.IdTipoNota;
                    entity.sc_observacion = info.sc_observacion;
                    entity.NaturalezaNota = info.NaturalezaNota;
                    entity.IdCtaCble_TipoNota = info.IdCtaCble_TipoNota;
                    entity.IdUsuarioUltMod = info.IdUsuarioUltMod;
                    entity.Fecha_UltMod = DateTime.Now;

                    #endregion

                    #region Detalle
                    var lst = db_f.fa_notaCreDeb_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).ToList();
                    db_f.fa_notaCreDeb_det.RemoveRange(lst);

                    foreach (var item in info.lst_det)
                    {
                        db_f.fa_notaCreDeb_det.Add(new fa_notaCreDeb_det
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdNota = info.IdNota,
                            Secuencia = Secuencia++,
                            IdProducto = item.IdProducto,
                            sc_cantidad = item.sc_cantidad,
                            sc_cantidad_factura = item.sc_cantidad_factura,
                            sc_Precio = item.sc_Precio,
                            sc_descUni = item.sc_descUni,
                            sc_PordescUni = item.sc_PordescUni,
                            sc_precioFinal = item.sc_precioFinal,
                            vt_por_iva = item.vt_por_iva,
                            sc_iva = item.sc_iva,
                            IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                            sc_subtotal = item.sc_subtotal,
                            sc_total = item.sc_total,
                            IdCentroCosto = item.IdCentroCosto,
                            IdPunto_Cargo = item.IdPunto_Cargo,
                            IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                        });
                    }
                    #endregion

                    #region Resumen
                    fa_notaCreDeb_resumen Entity_Resumen = new fa_notaCreDeb_resumen
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdNota = info.IdNota,
                        SubtotalIVASinDscto = info.info_resumen.SubtotalIVASinDscto,
                        SubtotalSinIVASinDscto = info.info_resumen.SubtotalSinIVASinDscto,
                        SubtotalSinDscto = info.info_resumen.SubtotalSinDscto,
                        Descuento = info.info_resumen.Descuento,
                        SubtotalIVAConDscto = info.info_resumen.SubtotalIVAConDscto,
                        SubtotalSinIVAConDscto = info.info_resumen.SubtotalSinIVAConDscto,
                        SubtotalConDscto = info.info_resumen.SubtotalConDscto,
                        IdCod_Impuesto_IVA = info.info_resumen.IdCod_Impuesto_IVA,
                        ValorIVA = info.info_resumen.ValorIVA,
                        Total = info.info_resumen.Total,
                        PorIva = info.info_resumen.PorIva
                    };

                    var notaDebCred = db_f.fa_notaCreDeb_resumen.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                    if (notaDebCred != null)
                        db_f.fa_notaCreDeb_resumen.Remove(notaDebCred);

                    db_f.fa_notaCreDeb_resumen.Add(Entity_Resumen);
                    #endregion

                    #region Cruce
                    var lst_cruce = db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).ToList();
                    db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.RemoveRange(lst_cruce);
                    Secuencia = 1;
                    foreach (var item in info.lst_cruce)
                    {
                        db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb
                        {
                            IdEmpresa_nt = info.IdEmpresa,
                            IdSucursal_nt = info.IdSucursal,
                            IdBodega_nt = info.IdBodega,
                            IdNota_nt = info.IdNota,
                            secuencia = Secuencia++,
                            IdEmpresa_fac_nd_doc_mod = item.IdEmpresa_fac_nd_doc_mod,
                            IdSucursal_fac_nd_doc_mod = item.IdSucursal_fac_nd_doc_mod,
                            IdBodega_fac_nd_doc_mod = item.IdBodega_fac_nd_doc_mod,
                            IdCbteVta_fac_nd_doc_mod = item.IdCbteVta_fac_nd_doc_mod,
                            vt_tipoDoc = item.vt_tipoDoc,
                            Valor_Aplicado = item.Valor_Aplicado,
                            fecha_cruce = DateTime.Now,
                            ValorProntoPago = item.ValorProntoPago
                        });
                    }
                    #endregion

                    db_f.SaveChanges();

                    #endregion

                    #region Contabilidad
                    info.IdCobro_tipo = entity.IdCobro_tipo;
                    var TipoCobro = odat_TipoCobro.get_info(entity.IdCobro_tipo);
                    if (TipoCobro != null)
                    {
                        var rel_conta = db_f.fa_notaCreDeb_x_ct_cbtecble.Where(q => q.no_IdEmpresa == info.IdEmpresa && q.no_IdSucursal == info.IdSucursal && q.no_IdBodega == info.IdBodega && q.no_IdNota == info.IdNota).FirstOrDefault();
                        ct_cbtecble_Info diario = armar_diario(info);
                        if (diario != null)
                        {
                            if (rel_conta == null)
                            {
                                if (TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA")
                                {
                                    caj_Caja_Movimiento_Info MovimientoCaja = armar_movimiendoCaja(info, diario);
                                    if (MovimientoCaja != null)
                                    {
                                        if (odata_MovimientoCaja.guardarDB(MovimientoCaja))
                                        {
                                            db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                            {
                                                no_IdEmpresa = info.IdEmpresa,
                                                no_IdSucursal = info.IdSucursal,
                                                no_IdBodega = info.IdBodega,
                                                no_IdNota = info.IdNota,

                                                ct_IdEmpresa = diario.IdEmpresa,
                                                ct_IdTipoCbte = diario.IdTipoCbte,
                                                ct_IdCbteCble = MovimientoCaja.IdCbteCble,

                                                observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                            });
                                            db_f.SaveChanges();
                                        }
                                    }
                                }
                                else
                                {
                                    if (odata_ct.guardarDB(diario))
                                    {
                                        db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                        {
                                            no_IdEmpresa = info.IdEmpresa,
                                            no_IdSucursal = info.IdSucursal,
                                            no_IdBodega = info.IdBodega,
                                            no_IdNota = info.IdNota,

                                            ct_IdEmpresa = diario.IdEmpresa,
                                            ct_IdTipoCbte = diario.IdTipoCbte,
                                            ct_IdCbteCble = diario.IdCbteCble,

                                            observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                        });
                                        db_f.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                diario.IdCbteCble = rel_conta.ct_IdCbteCble;
                                if (TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA")
                                {
                                    caj_Caja_Movimiento_Info MovimientoCaja = armar_movimiendoCaja(info, diario);
                                    if (MovimientoCaja != null)
                                    {
                                        MovimientoCaja.IdCbteCble = rel_conta.ct_IdCbteCble;
                                        odata_MovimientoCaja.modificarDB(MovimientoCaja);
                                    }
                                }
                                else
                                {   
                                    odata_ct.modificarDB(diario);
                                }
                            }
                        }
                    }
                        
                    #endregion

                    #region Cobranza
                    if (info.CreDeb.Trim() == "C" && info.lst_cruce.Count != 0)
                    {
                        cxc_cobro_Info cobro = armar_cobro(info);                                                
                        if (cobro != null)
                        {
                            var rel_cobr = db_f.fa_notaCreDeb_x_cxc_cobro.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).FirstOrDefault();
                            if (rel_cobr == null)
                            {
                                if (odata_cobr.guardarDB(cobro))
                                {
                                    db_f.fa_notaCreDeb_x_cxc_cobro.Add(new fa_notaCreDeb_x_cxc_cobro
                                    {
                                        IdEmpresa_nt = info.IdEmpresa,
                                        IdSucursal_nt = info.IdSucursal,
                                        IdBodega_nt = info.IdBodega,
                                        IdNota_nt = info.IdNota,
                                        IdEmpresa_cbr = cobro.IdEmpresa,
                                        IdSucursal_cbr = cobro.IdSucursal,
                                        IdCobro_cbr = cobro.IdCobro,
                                        Valor_cobro = Math.Round(info.lst_cruce.Sum(q => q.Valor_Aplicado), 2, MidpointRounding.AwayFromZero)
                                    });
                                    db_f.SaveChanges();
                                }
                            }else
                            {
                                cobro.IdCobro = rel_cobr.IdCobro_cbr;
                                odata_cobr.modificarDB(cobro);
                            }
                        }
                    }

                    #endregion
                }

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_notaCreDeb_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }
        public bool anularDB(fa_notaCreDeb_Info info)
        {
            try
            {
                #region Variables
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                cxc_cobro_Data odata_cobr = new cxc_cobro_Data();
                #endregion
                
                using (EntitiesFacturacion db_f = new EntitiesFacturacion())
                {
                    #region Nota de debito credito

                    #region Cabecera
                    var entity = db_f.fa_notaCreDeb.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota).FirstOrDefault();
                    if (entity == null) return false;

                    entity.Estado = "I";
                    entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    entity.Fecha_UltAnu = DateTime.Now;
                    entity.MotiAnula = info.MotiAnula;
                    #endregion

                    var lst_cruce = db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).ToList();
                    db_f.fa_notaCreDeb_x_fa_factura_NotaDeb.RemoveRange(lst_cruce);
                    #endregion

                    #region Contabilidad
                    var rel_conta = db_f.fa_notaCreDeb_x_ct_cbtecble.Where(q => q.no_IdEmpresa == info.IdEmpresa && q.no_IdSucursal == info.IdSucursal && q.no_IdBodega == info.IdBodega && q.no_IdNota == info.IdNota).FirstOrDefault();
                    if (rel_conta != null)
                    {
                        var MovCaj = odataMovCaja.get_info(rel_conta.ct_IdEmpresa, rel_conta.ct_IdTipoCbte, rel_conta.ct_IdCbteCble);
                        if (MovCaj != null)
                        {
                            if(!odataMovCaja.anularDB(new caj_Caja_Movimiento_Info
                            {
                                IdEmpresa = rel_conta.ct_IdEmpresa,
                                IdTipocbte = rel_conta.ct_IdTipoCbte,
                                IdCbteCble = rel_conta.ct_IdCbteCble
                            }))
                            {
                                entity.Estado = "A";
                                entity.IdUsuarioUltAnu = null;
                                entity.Fecha_UltAnu = null;
                                entity.MotiAnula = null;
                            }
                        }else
                        if (!odata_ct.anularDB(new ct_cbtecble_Info { IdEmpresa = rel_conta.ct_IdEmpresa, IdTipoCbte = rel_conta.ct_IdTipoCbte, IdCbteCble = rel_conta.ct_IdCbteCble, IdUsuarioAnu = info.IdUsuarioUltAnu }))
                        {
                            entity.Estado = "A";
                            entity.IdUsuarioUltAnu = null;
                            entity.Fecha_UltAnu = null;
                            entity.MotiAnula = null;
                        }
                    }

                    #endregion

                    #region Cobranza

                    var rel_cobr = db_f.fa_notaCreDeb_x_cxc_cobro.Where(q => q.IdEmpresa_nt == info.IdEmpresa && q.IdSucursal_nt == info.IdSucursal && q.IdBodega_nt == info.IdBodega && q.IdNota_nt == info.IdNota).FirstOrDefault();
                    if (rel_cobr != null)
                    {
                        if (lst_cruce.Count > 0)
                        {
                            if (!odata_cobr.anularDB(new cxc_cobro_Info { IdEmpresa = rel_cobr.IdEmpresa_cbr, IdSucursal = rel_cobr.IdSucursal_cbr, IdCobro = rel_cobr.IdCobro_cbr, IdUsuarioUltAnu = info.IdUsuarioUltAnu }))
                            {
                                entity.Estado = "A";
                                entity.IdUsuarioUltAnu = null;
                                entity.Fecha_UltAnu = null;
                                entity.MotiAnula = null;
                            }
                        }
                    }
                    #endregion

                    db_f.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private cxc_cobro_Info armar_cobro(fa_notaCreDeb_Info info)
        {
            try
            {
                cxc_cobro_Info cobro = new cxc_cobro_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdCobro = 0,
                    IdCobro_tipo = info.CreDeb.Trim() == "C" ? "NTCR" : "NTDB",
                    cr_fecha = info.no_fecha,
                    cr_fechaCobro = info.no_fecha,
                    cr_fechaDocu = info.no_fecha,
                    cr_NumDocumento = info.CodDocumentoTipo +  (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000")),
                    cr_observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000")),
                    cr_TotalCobro = Math.Round(info.lst_cruce.Sum(q=>q.Valor_Aplicado),2,MidpointRounding.AwayFromZero),
                    IdCaja = 1,
                    IdCliente = info.IdCliente,
                    IdAlumno = info.IdAlumno,
                    IdUsuario = info.IdUsuario,
                    IdTipoNotaCredito = info.IdTipoNota,
                    IdUsuarioUltMod = info.IdUsuarioUltMod,
                    lst_det = new List<cxc_cobro_det_Info>()
                };

                int Secuencia = 1;
                foreach (var item in info.lst_cruce)
                {
                    cobro.lst_det.Add(new cxc_cobro_det_Info
                    {
                        IdEmpresa = cobro.IdEmpresa,
                        IdSucursal = cobro.IdSucursal,
                        IdCobro = cobro.IdCobro,
                        secuencial = Secuencia++,
                        IdCobro_tipo_det = cobro.IdCobro_tipo,
                        IdBodega_Cbte = item.IdBodega_fac_nd_doc_mod,
                        IdCbte_vta_nota = item.IdCbteVta_fac_nd_doc_mod,
                        dc_TipoDocumento = item.vt_tipoDoc,
                        dc_ValorPago = item.Valor_Aplicado,  
                        dc_ValorProntoPago = item.ValorProntoPago                      
                    });
                }

                return cobro;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private ct_cbtecble_Info armar_diario(fa_notaCreDeb_Info info)
        {
            try
            {
                EntitiesCaja dbCaj = new EntitiesCaja();
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                using (EntitiesFacturacion db = new EntitiesFacturacion())
                {
                    var lst = db.spfa_notaCreDeb_ParaContabilizarAcademico(info.IdEmpresa, info.IdSucursal, info.IdBodega, info.IdNota).ToList();
                    var NCND = lst.Count > 0 ? lst[0] : null;
                    if (NCND == null)
                        return null;

                    var EnConciliacion = dbCxc.cxc_ConciliacionNotaCredito.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdNota == info.IdNota && q.Estado == true).Count();

                    var paramFac = db.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    if (paramFac == null)
                        return null;

                    var paramCaj = dbCaj.caj_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                    if (paramCaj == null)
                        return null;

                    var TipoCobro = dbCxc.cxc_cobro_tipo.Where(q => q.IdCobro_tipo == info.IdCobro_tipo).FirstOrDefault();
                    if (TipoCobro == null)
                        return null;

                    var ptoVta = db.vwfa_PuntoVta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdPuntoVta == info.IdPuntoVta).FirstOrDefault();
                    if (ptoVta == null)
                        return null;

                    var Caja = dbCaj.caj_Caja.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCaja == ptoVta.IdCaja).FirstOrDefault();
                    if (Caja == null)
                        return null;

                    if (EnConciliacion > 0)
                    {
                        var TipoNota = db.fa_TipoNota.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdTipoNota == info.IdTipoNota).FirstOrDefault();
                        if (TipoCobro == null)
                            return null;
                        NCND.IdCtaCbleHaber = TipoNota.IdCtaCble;
                    }
                    
                    #region Cabecera
                    ct_cbtecble_Info diario = new ct_cbtecble_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdTipoCbte = TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA" ? paramCaj.IdTipoCbteCble_MoviCaja_Ing : (info.CreDeb == "C" ? paramFac.IdTipoCbteCble_NC : paramFac.IdTipoCbteCble_ND),
                        IdCbteCble = 0,
                        cb_Fecha = info.no_fecha.Date,
                        IdSucursal = info.IdSucursal,
                        IdPeriodo = Convert.ToInt32(info.no_fecha.ToString("yyyyMM")),
                        IdUsuario = info.IdUsuario,
                        IdUsuarioUltModi = info.IdUsuarioUltMod,
                        cb_Observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString()) +" CLIENTE: "+NCND.NomCliente+" ALUMNO: "+NCND.NomAlumno + " " + info.sc_observacion,
                        CodCbteCble = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? info.NumNota_Impresa : info.IdNota.ToString()),
                        cb_Valor = 0,
                        lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                    };
                    #endregion

                    int secuencia = 1;

                    if (NCND.CreDeb.Trim() == "C")
                    {
                        #region Debe
                        diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = secuencia++,
                            IdCtaCble = TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA" ? Caja.IdCtaCble : NCND.IdCtaCbleDebe,
                            dc_Observacion = NCND.vt_NumFactura,
                            dc_Valor = Math.Round(Convert.ToDouble(NCND.Total), 2, MidpointRounding.AwayFromZero)
                        });
                        #endregion

                        if (lst.Where(q => q.Valor_Aplicado != null).Count() > 0 && EnConciliacion == 0)
                        {
                            foreach (var item in lst)
                            {
                                #region Haber
                                diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                                {
                                    IdEmpresa = diario.IdEmpresa,
                                    IdTipoCbte = diario.IdTipoCbte,
                                    IdCbteCble = diario.IdCbteCble,
                                    secuencia = secuencia++,
                                    IdCtaCble = NCND.IdCtaCbleHaber,
                                    dc_Observacion = NCND.vt_NumFactura,
                                    dc_Valor = Math.Round(item.Valor_Aplicado ?? 0, 2, MidpointRounding.AwayFromZero) * -1
                                });
                                #endregion
                            }
                        }else
                        {
                            diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                            {
                                IdEmpresa = diario.IdEmpresa,
                                IdTipoCbte = diario.IdTipoCbte,
                                IdCbteCble = diario.IdCbteCble,
                                secuencia = secuencia++,
                                IdCtaCble = NCND.IdCtaCbleHaber,
                                dc_Observacion = NCND.vt_NumFactura,
                                dc_Valor = Math.Round(Convert.ToDouble(NCND.Total), 2, MidpointRounding.AwayFromZero) * -1
                            });
                        }
                    }else
                    {
                        #region Debe
                        diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = secuencia++,
                            IdCtaCble = NCND.IdCtaCbleDebe,
                            dc_Observacion = NCND.vt_NumFactura,
                            dc_Valor = Math.Round(Convert.ToDouble(NCND.Total), 2, MidpointRounding.AwayFromZero)
                        });
                        #endregion

                        #region Debe
                        diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                        {
                            IdEmpresa = diario.IdEmpresa,
                            IdTipoCbte = diario.IdTipoCbte,
                            IdCbteCble = diario.IdCbteCble,
                            secuencia = secuencia++,
                            IdCtaCble = NCND.IdCtaCbleHaber,
                            dc_Observacion = NCND.vt_NumFactura,
                            dc_Valor = Math.Round(Convert.ToDouble(NCND.Total), 2, MidpointRounding.AwayFromZero)*-1
                        });
                        #endregion
                    }

                    if (diario.lst_ct_cbtecble_det.Count == 0)
                        return null;

                    if (Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero) != 0)
                        return null;

                    if (diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor == 0).Count() > 0)
                        return null;

                    return diario;
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private caj_Caja_Movimiento_Info armar_movimiendoCaja(fa_notaCreDeb_Info infoNC, ct_cbtecble_Info infoCT)
        {
            try
            {
                EntitiesCaja dbCaj = new EntitiesCaja();
                EntitiesCuentasPorCobrar dbCxc = new EntitiesCuentasPorCobrar();
                EntitiesFacturacion dbFac = new EntitiesFacturacion();
                EntitiesAcademico dbAca = new EntitiesAcademico();

                var paramCaj = dbCaj.caj_parametro.Where(q => q.IdEmpresa == infoNC.IdEmpresa).FirstOrDefault();
                if (paramCaj == null)
                    return null;

                var paramCxc = dbCxc.cxc_Parametro.Where(q => q.IdEmpresa == infoNC.IdEmpresa).FirstOrDefault();
                if (paramCxc == null)
                    return null;

                var ptoVta = dbFac.vwfa_PuntoVta.Where(q => q.IdEmpresa == infoNC.IdEmpresa && q.IdPuntoVta == infoNC.IdPuntoVta).FirstOrDefault();
                if (ptoVta == null)
                    return null;

                var Alumno = dbAca.vwaca_Alumno.Where(q => q.IdEmpresa == infoNC.IdEmpresa && q.IdAlumno == infoNC.IdAlumno).FirstOrDefault();
                if (Alumno == null)
                    return null;

                caj_Caja_Movimiento_Info Movimiento = new caj_Caja_Movimiento_Info
                {
                    IdEmpresa = infoNC.IdEmpresa,
                    IdTipocbte = paramCaj.IdTipoCbteCble_MoviCaja_Ing,
                    IdTipoMovi = paramCxc.pa_IdTipoMoviCaja_x_Cobros_x_cliente,
                    IdCaja = ptoVta.IdCaja,
                    IdTipo_Persona = "ALUMNO",
                    IdPersona = Alumno.IdPersona,
                    IdEntidad  = Alumno.IdAlumno,
                    cm_fecha = infoNC.no_fecha,
                    cm_observacion = infoNC.sc_observacion,
                    cm_Signo = "+",
                    cm_valor = Convert.ToDouble(infoNC.info_resumen.Total),
                    info_caj_Caja_Movimiento_det = new caj_Caja_Movimiento_det_Info
                    {
                        cr_Valor = Convert.ToDouble(infoNC.info_resumen.Total),
                        IdCobro_tipo = infoNC.IdCobro_tipo
                    },
                    lst_ct_cbtecble_det = infoCT.lst_ct_cbtecble_det
                };

                return Movimiento;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool modificarEstadoAutorizacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_notaCreDeb.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.aprobada_enviar_sri = true;
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, decimal IdAlumno)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<fa_notaCreDeb_Info> Lista = new List<fa_notaCreDeb_Info>();
            Lista = GetListNCConSaldo(IdEmpresa,IdAlumno, skip, take, args.Filter);
            return Lista;
        }

        public List<fa_notaCreDeb_Info> GetListNCConSaldo(int IdEmpresa, decimal IdAlumno,int Skip, int take, string Filter)
        {
            try
            {
                List<fa_notaCreDeb_Info> Lista = new List<fa_notaCreDeb_Info>();

                using (EntitiesFacturacion db = new EntitiesFacturacion())
                {
                    var lst = db.vwfa_notaCreDeb_ParaConciliarNC.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList().Skip(Skip).Take(take).OrderBy(q => q.IdNota);

                    foreach (var item in lst)
                    {
                        Lista.Add(new fa_notaCreDeb_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdNota = item.IdNota,
                            no_fecha = item.no_fecha,
                            sc_observacion = item.sc_observacion,
                            sc_total = Convert.ToDouble(item.Total),
                            sc_saldo = Math.Round(Convert.ToDouble(item.Total - item.Valor_Aplicado),2,MidpointRounding.AwayFromZero),
                            IdString = item.IdSucursal.ToString().PadLeft(4,'0') + item.IdBodega.ToString().PadLeft(4, '0') + item.IdNota.ToString().PadLeft(10, '0'),
                            NumNota_Impresa = item.NumNota
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

        public fa_notaCreDeb_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            if (args.Value == null)
                return null;
            return get_info(IdEmpresa, args.Value.ToString());
        }
        public fa_notaCreDeb_Info get_info(int IdEmpresa,string IdString)
        {
            try
            {
                fa_notaCreDeb_Info info;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    int IdSucursal = string.IsNullOrEmpty(IdString) ? 0 : Convert.ToInt32(IdString.Substring(0, 4));
                    int IdBodega = string.IsNullOrEmpty(IdString) ? 0 : Convert.ToInt32(IdString.Substring(4, 4));
                    int IdNota = string.IsNullOrEmpty(IdString) ? 0 : Convert.ToInt32(IdString.Substring(8, 10));
                    if (IdNota == 0)
                        return null;

                    var Entity = Context.vwfa_notaCreDeb.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (Entity == null) return null;
                    info = new fa_notaCreDeb_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdNota = Entity.IdNota,
                        no_fecha = Entity.no_fecha,
                        sc_observacion = Entity.sc_observacion,
                        sc_total = Convert.ToDouble(Entity.sc_total),
                        NumNota_Impresa = Entity.NumNota_Impresa,
                        IdString = Entity.IdSucursal.ToString("0000") + Entity.IdBodega.ToString("0000") + Entity.IdNota.ToString("0000000000")
                    };

                    var NCSaldo = Context.vwfa_notaCreDeb_ParaConciliarNC.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdNota == IdNota).FirstOrDefault();
                    if (NCSaldo != null)
                    {
                        info.sc_saldo = Math.Round(Convert.ToDouble(NCSaldo.Total - NCSaldo.Valor_Aplicado ?? 0), 2, MidpointRounding.AwayFromZero);
                    }
                }
                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_Info> get_list_credito_favor(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<fa_notaCreDeb_Info> Lista = new List<fa_notaCreDeb_Info>();
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = Context.vwfa_notaCreDeb_ParaConciliarNC.Where(q=> q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();

                    foreach (var item in lst)
                    {
                        var info = new fa_notaCreDeb_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdAlumno = item.IdAlumno,
                            sc_saldo = Convert.ToDouble(item.Saldo)
                        };
                        Lista.Add(info);
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Contabilizar(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                EntitiesFacturacion db_f = new EntitiesFacturacion();
                var info = get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
                if (info == null || info.Estado == "I")
                    return false;

                info.lst_det = odata_Det.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
                info.lst_cruce = odata_DetNDFac.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);

                var TipoCobro = odat_TipoCobro.get_info(info.IdCobro_tipo);
                if (TipoCobro != null)
                {
                    var rel_conta = db_f.fa_notaCreDeb_x_ct_cbtecble.Where(q => q.no_IdEmpresa == info.IdEmpresa && q.no_IdSucursal == info.IdSucursal && q.no_IdBodega == info.IdBodega && q.no_IdNota == info.IdNota).FirstOrDefault();
                    ct_cbtecble_Info diario = armar_diario(info);
                    if (diario != null)
                    {
                        if (rel_conta == null)
                        {
                            if (TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA")
                            {
                                caj_Caja_Movimiento_Info MovimientoCaja = armar_movimiendoCaja(info, diario);
                                if (MovimientoCaja != null)
                                {
                                    if (odata_MovimientoCaja.guardarDB(MovimientoCaja))
                                    {
                                        db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                        {
                                            no_IdEmpresa = info.IdEmpresa,
                                            no_IdSucursal = info.IdSucursal,
                                            no_IdBodega = info.IdBodega,
                                            no_IdNota = info.IdNota,

                                            ct_IdEmpresa = diario.IdEmpresa,
                                            ct_IdTipoCbte = diario.IdTipoCbte,
                                            ct_IdCbteCble = MovimientoCaja.IdCbteCble,

                                            observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                        });
                                        db_f.SaveChanges();
                                    }
                                }
                            }
                            else
                            {
                                if (odata_ct.guardarDB(diario))
                                {
                                    db_f.fa_notaCreDeb_x_ct_cbtecble.Add(new fa_notaCreDeb_x_ct_cbtecble
                                    {
                                        no_IdEmpresa = info.IdEmpresa,
                                        no_IdSucursal = info.IdSucursal,
                                        no_IdBodega = info.IdBodega,
                                        no_IdNota = info.IdNota,

                                        ct_IdEmpresa = diario.IdEmpresa,
                                        ct_IdTipoCbte = diario.IdTipoCbte,
                                        ct_IdCbteCble = diario.IdCbteCble,

                                        observacion = info.CodDocumentoTipo + (info.NaturalezaNota == "SRI" ? ("-" + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa) : info.IdNota.ToString("000000000"))
                                    });
                                    db_f.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            diario.IdCbteCble = rel_conta.ct_IdCbteCble;
                            if (TipoCobro.tc_Tomar_Cta_Cble_De == "CAJA")
                            {
                                caj_Caja_Movimiento_Info MovimientoCaja = armar_movimiendoCaja(info, diario);
                                if (MovimientoCaja != null)
                                {
                                    MovimientoCaja.IdCbteCble = rel_conta.ct_IdCbteCble;
                                    odata_MovimientoCaja.modificarDB(MovimientoCaja);
                                }
                            }
                            else
                            {
                                odata_ct.modificarDB(diario);
                            }
                        }
                    }
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

