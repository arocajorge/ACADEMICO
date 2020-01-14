﻿using Core.Data.Base;
using Core.Data.Contabilidad;
using Core.Data.General;
using Core.Info.Contabilidad;
using Core.Info.Facturacion;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
   public class fa_factura_Data
    {
        public List<fa_factura_consulta_Info> get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin)
        {
            try
            {
                List<fa_factura_consulta_Info> Lista;
                Fecha_ini = Fecha_ini.Date;
                Fecha_fin = Fecha_fin.Date;
                int IdSucursalIni = IdSucursal;
                int IdSucursalFin = IdSucursal == 0 ? 999 : IdSucursal;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_factura
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal >= IdSucursalIni
                             && q.IdSucursal <= IdSucursalFin
                             && Fecha_ini <= q.vt_fecha && q.vt_fecha <= Fecha_fin
                             orderby q.IdCbteVta descending
                             select new fa_factura_consulta_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdCbteVta = q.IdCbteVta,

                                 vt_NumFactura = q.vt_NumFactura,
                                 vt_fecha = q.vt_fecha,
                                 NomContacto = q.Nombres,
                                 Ve_Vendedor = q.Ve_Vendedor,
                                 vt_Subtotal0 = q.vt_Subtotal0,
                                 vt_SubtotalIVA = q.vt_SubtotalIVA,
                                 vt_iva = q.vt_iva,
                                 vt_total = q.vt_total,
                                 Estado = q.Estado,
                                 esta_impresa = q.esta_impresa,

                                 IdEmpresa_in_eg_x_inv = q.IdEmpresa_in_eg_x_inv,
                                 IdSucursal_in_eg_x_inv = q.IdSucursal_in_eg_x_inv,
                                 IdMovi_inven_tipo_in_eg_x_inv = q.IdMovi_inven_tipo_in_eg_x_inv,
                                 IdNumMovi_in_eg_x_inv = q.IdNumMovi_in_eg_x_inv,

                                 vt_autorizacion = q.vt_autorizacion,
                                 Fecha_Autorizacion = q.Fecha_Autorizacion,
                                 NombresAlumno = q.NombresAlumno,
                                 EstadoBool = q.Estado == "A" ? true : false

                             }).ToList();
                }

                return Lista;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_factura_Data", Metodo = "get_list", IdUsuario = "consulta" });
                return new List<fa_factura_consulta_Info>();
            }
        }
        public fa_factura_Info get_info(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                fa_factura_Info info = new fa_factura_Info();
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_factura Entity = Context.fa_factura.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta);
                    if (Entity == null) return null;
                    info = new fa_factura_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSucursal = Entity.IdSucursal,
                        IdBodega = Entity.IdBodega,
                        IdCbteVta = Entity.IdCbteVta,
                        CodCbteVta = Entity.CodCbteVta,
                        vt_tipoDoc = Entity.vt_tipoDoc,
                        vt_serie1 = Entity.vt_serie1,
                        vt_serie2 = Entity.vt_serie2,
                        vt_NumFactura = Entity.vt_NumFactura,
                        Fecha_Autorizacion = Entity.fecha_primera_cuota,
                        vt_autorizacion = Entity.vt_autorizacion,
                        vt_fecha = Entity.vt_fecha,
                        vt_fech_venc = Entity.vt_fech_venc,
                        IdCliente = Entity.IdCliente,
                        IdVendedor = Entity.IdVendedor,
                        vt_plazo = Entity.vt_plazo,
                        vt_Observacion = Entity.vt_Observacion,
                        vt_tipo_venta = Entity.vt_tipo_venta,
                        IdCaja = Entity.IdCaja,
                        IdPuntoVta = Entity.IdPuntoVta,
                        fecha_primera_cuota = Entity.fecha_primera_cuota,
                        Fecha_Transaccion = Entity.fecha_primera_cuota,
                        Estado = Entity.Estado,
                        esta_impresa = Entity.esta_impresa,
                        valor_abono = Entity.valor_abono,
                        IdNivel = Entity.IdNivel,
                        IdCatalogo_FormaPago = Entity.IdCatalogo_FormaPago,
                        IdAlumno = Entity.IdAlumno
                    };

                    info.info_resumen = Context.fa_factura_resumen.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta).Select(q => new fa_factura_resumen_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdBodega = q.IdBodega,
                        IdCbteVta = q.IdCbteVta,
                        SubtotalConDscto = q.SubtotalConDscto,
                        SubtotalIVAConDscto = q.SubtotalIVAConDscto,
                        SubtotalIVASinDscto = q.SubtotalIVASinDscto,
                        SubtotalSinDscto = q.SubtotalSinDscto,
                        SubtotalSinIVAConDscto = q.SubtotalSinIVAConDscto,
                        SubtotalSinIVASinDscto = q.SubtotalSinIVASinDscto,
                        Total = q.Total,
                        ValorEfectivo = q.ValorEfectivo,
                        Descuento = q.Descuento,
                        ValorIVA = q.ValorIVA,
                        Cambio = q.Cambio
                    }).FirstOrDefault();

                    info.info_resumen = info.info_resumen ?? new fa_factura_resumen_Info();
                }
                return info;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_factura_Data", Metodo = "get_info" });
                return new fa_factura_Info();
            }
        }

        private decimal get_id(int IdEmpresa, int IdSucursal, int IdBodega)
        {
            try
            {
                decimal ID = 1;
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var lst = from q in Context.fa_factura
                              where q.IdEmpresa == IdEmpresa
                              && q.IdSucursal == IdSucursal
                              && q.IdBodega == IdBodega
                              select q;
                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdCbteVta) + 1;
                }
                return ID;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool guardarDB(fa_factura_Info info)
        {
            EntitiesFacturacion db_f = new EntitiesFacturacion();
            EntitiesGeneral db_g = new EntitiesGeneral();
            EntitiesAcademico db_a = new EntitiesAcademico();
            try
            {
                #region Variables
                int secuencia = 1;
                ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
                #endregion

                #region Factura

                #region Cabecera
                var factura = new fa_factura
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdCbteVta = info.IdCbteVta = get_id(info.IdEmpresa, info.IdSucursal, info.IdBodega),
                    CodCbteVta = info.CodCbteVta,
                    vt_tipoDoc = info.vt_tipoDoc,
                    vt_serie1 = info.vt_serie1,
                    vt_serie2 = info.vt_serie2,
                    vt_NumFactura = info.vt_NumFactura,
                    Fecha_Autorizacion = info.Fecha_Autorizacion,
                    vt_autorizacion = info.vt_autorizacion,
                    vt_fecha = info.vt_fecha.Date,
                    vt_fech_venc = info.vt_fech_venc.Date,
                    IdCliente = info.IdCliente,
                    IdVendedor = info.IdVendedor,
                    vt_plazo = info.vt_plazo,
                    vt_Observacion = string.IsNullOrEmpty(info.vt_Observacion) ? "" : info.vt_Observacion,
                    IdCatalogo_FormaPago = info.IdCatalogo_FormaPago,
                    vt_tipo_venta = info.vt_tipo_venta,
                    IdCaja = info.IdCaja,
                    IdPuntoVta = info.IdPuntoVta,
                    fecha_primera_cuota = info.fecha_primera_cuota,
                    Fecha_Transaccion = DateTime.Now,
                    Estado = info.Estado = "A",
                    esta_impresa = info.esta_impresa,
                    valor_abono = info.valor_abono,
                    IdUsuario = info.IdUsuario,
                    IdNivel = info.IdNivel,
                    IdAlumno = info.IdAlumno

                };
                #endregion

                #region Resumen
                db_f.fa_factura_resumen.Add(new fa_factura_resumen
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdCbteVta = info.IdCbteVta,

                    SubtotalConDscto = info.info_resumen.SubtotalConDscto,
                    SubtotalIVAConDscto = info.info_resumen.SubtotalIVAConDscto,
                    SubtotalIVASinDscto = info.info_resumen.SubtotalIVASinDscto,
                    SubtotalSinDscto = info.info_resumen.SubtotalSinDscto,
                    SubtotalSinIVAConDscto = info.info_resumen.SubtotalSinIVAConDscto,
                    SubtotalSinIVASinDscto = info.info_resumen.SubtotalSinIVASinDscto,

                    Total = info.info_resumen.Total,
                    Descuento = info.info_resumen.Descuento,
                    ValorEfectivo = info.info_resumen.ValorEfectivo,
                    ValorIVA = info.info_resumen.ValorIVA,
                    Cambio = info.info_resumen.Cambio,

                    FechaProntoPago = info.info_resumen.FechaProntoPago,
                    ValorProntoPago = info.info_resumen.ValorProntoPago
                });
                #endregion

                #region Detalle
                foreach (var item in info.lst_det)
                {
                    db_f.fa_factura_det.Add(new fa_factura_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdCbteVta = info.IdCbteVta,
                        Secuencia = item.Secuencia = secuencia++,

                        IdProducto = item.IdProducto,
                        vt_cantidad = item.vt_cantidad,
                        vt_Precio = item.vt_Precio,
                        vt_PorDescUnitario = item.vt_PorDescUnitario,
                        vt_DescUnitario = item.vt_DescUnitario,
                        vt_PrecioFinal = item.vt_PrecioFinal,
                        vt_Subtotal = item.vt_Subtotal,
                        vt_por_iva = item.vt_por_iva,
                        IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                        vt_iva = item.vt_iva,
                        vt_total = item.vt_total,

                        IdEmpresa_pf = item.IdEmpresa_pf,
                        IdSucursal_pf = item.IdSucursal_pf,
                        IdProforma = item.IdProforma,
                        Secuencia_pf = item.Secuencia_pf,

                        IdCentroCosto = item.IdCentroCosto,
                        IdPunto_Cargo = item.IdPunto_Cargo,
                        IdPunto_cargo_grupo = item.IdPunto_cargo_grupo,

                        aca_IdPeriodo = item.aca_IdPeriodo,
                        aca_IdRubro = item.aca_IdRubro
                    });

                    #region MatriculaRubro
                    aca_Matricula_Rubro Entity_MatricularRubro = db_a.aca_Matricula_Rubro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == item.IdMatricula 
                    && q.IdPeriodo == item.aca_IdPeriodo && q.IdRubro == item.aca_IdRubro);
                    if (Entity_MatricularRubro == null) return false;

                    Entity_MatricularRubro.FechaFacturacion = info.vt_fecha.Date;

                    db_a.SaveChanges();
                    #endregion
                }
                #endregion

                var cliente = db_f.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                var persona = db_g.tb_persona.Where(q => q.IdPersona == cliente.IdPersona).FirstOrDefault();

                #region Talonario
                //var tal = odata_tal.GetUltimoNoUsadoFacElec(info.IdEmpresa, info.vt_tipoDoc, info.vt_serie1, info.vt_serie2);
                //if (tal != null)
                //    factura.vt_NumFactura = info.vt_NumFactura = tal.NumDocumento;

                fa_PuntoVta_Data data_puntovta = new fa_PuntoVta_Data();
                tb_sis_Documento_Tipo_Talonario_Data data_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
                fa_PuntoVta_Info info_puntovta = new fa_PuntoVta_Info();
                tb_sis_Documento_Tipo_Talonario_Info ultimo_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
                tb_sis_Documento_Tipo_Talonario_Info info_talonario = new tb_sis_Documento_Tipo_Talonario_Info();
                info_puntovta = data_puntovta.get_info(info.IdEmpresa, info.IdSucursal, info.IdPuntoVta??0);

                if (info_puntovta != null)
                {
                    if (info_puntovta.EsElectronico == true)
                    {
                        ultimo_talonario = data_talonario.GetUltimoNoUsado(info.IdEmpresa, info_puntovta.codDocumentoTipo, info_puntovta.Su_CodigoEstablecimiento, info_puntovta.cod_PuntoVta, info_puntovta.EsElectronico,true);

                        if (ultimo_talonario != null)
                        {
                            factura.vt_serie1 = info.vt_serie1 = ultimo_talonario.Establecimiento;
                            factura.vt_serie2 = info.vt_serie2 = ultimo_talonario.PuntoEmision;
                            factura.vt_NumFactura = info.vt_NumFactura = ultimo_talonario.NumDocumento;
                        }
                    }
                    else
                    {
                        info_talonario.IdEmpresa = info.IdEmpresa;
                        info_talonario.CodDocumentoTipo = info.vt_tipoDoc;
                        info_talonario.Establecimiento = info.vt_serie1;
                        info_talonario.PuntoEmision = info.vt_serie2;
                        info_talonario.NumDocumento = info.vt_NumFactura;
                        info_talonario.IdSucursal = info.IdSucursal;
                        info_talonario.Usado = true;

                        data_talonario.modificar_estado_usadoDB(info_talonario);
                    }
                }
                #endregion

                db_f.fa_factura.Add(factura);
                db_f.SaveChanges();

                #region Contabilidad
                var parametro = db_f.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();

                if (!string.IsNullOrEmpty(cliente.IdCtaCble_cxc_Credito))
                {
                    ct_cbtecble_Info diario = armar_diario(info, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, persona == null ? "" : persona.pe_nombreCompleto);
                    if (diario != null)
                        if (data_ct.guardarDB(diario))
                        {
                            db_f.fa_factura_x_ct_cbtecble.Add(new fa_factura_x_ct_cbtecble
                            {
                                vt_IdEmpresa = info.IdEmpresa,
                                vt_IdSucursal = info.IdSucursal,
                                vt_IdBodega = info.IdBodega,
                                vt_IdCbteVta = info.IdCbteVta,

                                ct_IdEmpresa = diario.IdEmpresa,
                                ct_IdTipoCbte = diario.IdTipoCbte,
                                ct_IdCbteCble = diario.IdCbteCble,
                            });
                            db_f.SaveChanges();
                        }
                }
                #endregion

                #endregion

                db_f.Dispose();
                db_a.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_factura_Data", Metodo = "guardarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public ct_cbtecble_Info armar_diario(fa_factura_Info info, int IdTipoCbte, string IdCtaCble_Cliente, string IdCtaCble_Dscto, string nomContacto)
        {
            try
            {
                #region Variables
                string IdCtaCble_VentasIVA = string.Empty;
                string IdCtaCble_Ventas0 = string.Empty;
                string IdCtaCble_IVA = string.Empty;
                #endregion

                #region Validar cuentas

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    var porcentajes = (from q in info.lst_det
                                             group q by new { q.IdCod_Impuesto_Iva} into g
                                             select g.Key).ToList();

                    foreach (var item in porcentajes)
                    {
                        var impuesto = Context.tb_sis_Impuesto_x_ctacble.Include("tb_sis_Impuesto").Where(q => q.IdEmpresa_cta == info.IdEmpresa && q.IdCod_Impuesto == item.IdCod_Impuesto_Iva).FirstOrDefault();
                        if (impuesto != null)
                        {
                            if (impuesto.tb_sis_Impuesto.porcentaje > 0)
                            {
                                IdCtaCble_VentasIVA = impuesto.IdCtaCble_vta;
                                IdCtaCble_IVA = impuesto.IdCtaCble;
                            }else
                            {
                                IdCtaCble_Ventas0 = impuesto.IdCtaCble_vta;
                            }
                        }                            
                    }                    
                }

                #endregion

                ct_cbtecble_Info diario = new ct_cbtecble_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdTipoCbte = IdTipoCbte,
                    IdCbteCble = 0,
                    cb_Fecha = info.vt_fecha.Date,
                    IdSucursal = info.IdSucursal,
                    
                    IdUsuario = info.IdUsuario,
                    IdUsuarioUltModi = info.IdUsuarioUltModi,
                    cb_Observacion = "FACT# " + info.vt_serie1 + "-" + info.vt_serie2 + "-" + info.vt_NumFactura + " " + "CLIENTE: " + nomContacto + " " + info.vt_Observacion,
                    CodCbteCble = "FACT# " + info.vt_NumFactura,
                    cb_Valor = 0,
                    lst_ct_cbtecble_det = new List<ct_cbtecble_det_Info>()
                };
                int secuencia = 1;

                #region Ventas con IVA
                if (!string.IsNullOrEmpty(IdCtaCble_VentasIVA))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_VentasIVA,
                        dc_Valor = string.IsNullOrEmpty(IdCtaCble_Dscto) ? Convert.ToDouble(info.info_resumen.SubtotalIVAConDscto*-1) : Convert.ToDouble(info.info_resumen.SubtotalIVASinDscto * -1)
                    });
                #endregion

                #region Ventas IVA 0
                if (!string.IsNullOrEmpty(IdCtaCble_Ventas0))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_Ventas0,
                        dc_Valor = string.IsNullOrEmpty(IdCtaCble_Dscto) ? Convert.ToDouble(info.info_resumen.SubtotalSinIVAConDscto * -1) : Convert.ToDouble(info.info_resumen.SubtotalSinIVASinDscto * -1)
                    });
                #endregion

                #region IVA
                if (!string.IsNullOrEmpty(IdCtaCble_IVA))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_IVA,
                        dc_Valor = Convert.ToDouble(info.info_resumen.ValorIVA * -1)
                    });
                #endregion

                #region Cliente
                if (!string.IsNullOrEmpty(IdCtaCble_Cliente))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_Cliente,
                        dc_Valor = Convert.ToDouble(info.info_resumen.Total)
                    });
                #endregion

                #region Descuento
                if (!string.IsNullOrEmpty(IdCtaCble_Dscto))
                    diario.lst_ct_cbtecble_det.Add(new ct_cbtecble_det_Info
                    {
                        IdEmpresa = diario.IdEmpresa,
                        IdTipoCbte = diario.IdTipoCbte,
                        IdCbteCble = diario.IdCbteCble,
                        secuencia = secuencia++,
                        IdCtaCble = IdCtaCble_Dscto,
                        dc_Valor = Convert.ToDouble(info.info_resumen.Descuento)
                    });
                #endregion

                if (info.lst_det.Count == 0)
                    return null;

                diario.lst_ct_cbtecble_det.RemoveAll(q=>q.dc_Valor == 0);

                if (diario.lst_ct_cbtecble_det.Count == 0)
                    return null;                

                if (diario.lst_ct_cbtecble_det.Where(q=>q.dc_Valor == 0).Count() > 0)
                    return null;

                double descuadre = Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero);
                if (descuadre < -0.02 || 0.02 <= descuadre)
                    return null;

                if (descuadre <= 0.02 || -0.02 <= descuadre && descuadre != 0)
                {
                    if (descuadre > 0)
                        diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor < 0).FirstOrDefault().dc_Valor -= descuadre;
                    else
                        diario.lst_ct_cbtecble_det.Where(q => q.dc_Valor > 0).FirstOrDefault().dc_Valor += (descuadre*-1);
                }

                descuadre = Math.Round(diario.lst_ct_cbtecble_det.Sum(q => q.dc_Valor), 2, MidpointRounding.AwayFromZero);
                if (descuadre != 0)
                    return null;

                return diario;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool modificarDB(fa_factura_Info info)
        {
            EntitiesFacturacion db_f = new EntitiesFacturacion();
            EntitiesGeneral db_g = new EntitiesGeneral();
            try
            {
                #region Variables
                int secuencia = 1;
                ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
                #endregion

                #region Factura

                #region Cabecera
                fa_factura Entity = db_f.fa_factura.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta);
                if (Entity == null) return false;
                
                Entity.vt_fecha = info.vt_fecha.Date;
                Entity.vt_fech_venc = info.vt_fech_venc.Date;
                Entity.IdCliente = info.IdCliente;
                Entity.IdVendedor = info.IdVendedor;
                Entity.vt_plazo = info.vt_plazo;
                Entity.vt_Observacion = string.IsNullOrEmpty(info.vt_Observacion) ? "" : info.vt_Observacion;
                Entity.IdCatalogo_FormaPago = info.IdCatalogo_FormaPago;
                Entity.vt_tipo_venta = info.vt_tipo_venta;
                Entity.fecha_primera_cuota = info.fecha_primera_cuota;
                Entity.valor_abono = info.valor_abono;
                Entity.IdNivel = info.IdNivel;
                Entity.IdUsuarioUltModi = info.IdUsuarioUltModi;
                Entity.Fecha_UltMod = DateTime.Now;

                #endregion
                var cliente = db_f.fa_cliente.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCliente == info.IdCliente).FirstOrDefault();
                var persona = db_g.tb_persona.Where(q => q.IdPersona == cliente.IdPersona).FirstOrDefault();

                #region Resumen
                var resu = db_f.fa_factura_resumen.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta).FirstOrDefault();
                if(resu != null)
                    db_f.fa_factura_resumen.Remove(resu);

                db_f.fa_factura_resumen.Add(new fa_factura_resumen
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdCbteVta = info.IdCbteVta,

                    SubtotalConDscto = info.info_resumen.SubtotalConDscto,
                    SubtotalIVAConDscto = info.info_resumen.SubtotalIVAConDscto,
                    SubtotalIVASinDscto = info.info_resumen.SubtotalIVASinDscto,
                    SubtotalSinDscto = info.info_resumen.SubtotalSinDscto,
                    SubtotalSinIVAConDscto = info.info_resumen.SubtotalSinIVAConDscto,
                    SubtotalSinIVASinDscto = info.info_resumen.SubtotalSinIVASinDscto,

                    Total = info.info_resumen.Total,
                    Descuento = info.info_resumen.Descuento,
                    ValorEfectivo = info.info_resumen.ValorEfectivo,
                    ValorIVA = info.info_resumen.ValorIVA,
                    Cambio = info.info_resumen.Cambio,

                    FechaProntoPago = info.info_resumen.FechaProntoPago,
                    ValorProntoPago = info.info_resumen.ValorProntoPago
                });
                #endregion

                #region Detalle
                var lst_det = db_f.fa_factura_det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta).ToList();
                db_f.fa_factura_det.RemoveRange(lst_det);

                foreach (var item in info.lst_det)
                {
                    db_f.fa_factura_det.Add(new fa_factura_det
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdBodega = info.IdBodega,
                        IdCbteVta = info.IdCbteVta,
                        Secuencia = item.Secuencia = secuencia++,

                        IdProducto = item.IdProducto,
                        vt_cantidad = item.vt_cantidad,
                        vt_Precio = item.vt_Precio,
                        vt_PorDescUnitario = item.vt_PorDescUnitario,
                        vt_DescUnitario = item.vt_DescUnitario,
                        vt_PrecioFinal = item.vt_PrecioFinal,
                        vt_Subtotal = item.vt_Subtotal,
                        vt_por_iva = item.vt_por_iva,
                        IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                        vt_iva = item.vt_iva,
                        vt_total = item.vt_total,

                        IdEmpresa_pf = item.IdEmpresa_pf,
                        IdSucursal_pf = item.IdSucursal_pf,
                        IdProforma = item.IdProforma,
                        Secuencia_pf = item.Secuencia_pf,

                        IdCentroCosto = item.IdCentroCosto,
                        IdPunto_Cargo = item.IdPunto_Cargo,
                        IdPunto_cargo_grupo = item.IdPunto_cargo_grupo
                    });
                }
                #endregion
                #endregion
                db_f.SaveChanges();

                #region Contabilidad
                var parametro = db_f.fa_parametro.Where(q => q.IdEmpresa == info.IdEmpresa).FirstOrDefault();
                if (!string.IsNullOrEmpty(cliente.IdCtaCble_cxc_Credito))
                {
                    var conta = db_f.fa_factura_x_ct_cbtecble.Where(q => q.vt_IdEmpresa == info.IdEmpresa && q.vt_IdSucursal == info.IdSucursal && q.vt_IdBodega == info.IdBodega && q.vt_IdCbteVta == info.IdCbteVta).FirstOrDefault();
                    if (conta == null)
                    {
                        ct_cbtecble_Info diario = armar_diario(info, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, persona == null ? "" : persona.pe_nombreCompleto);
                        if (diario != null)
                        {
                            if (data_ct.guardarDB(diario))
                            {
                                db_f.fa_factura_x_ct_cbtecble.Add(new fa_factura_x_ct_cbtecble
                                {
                                    vt_IdEmpresa = info.IdEmpresa,
                                    vt_IdSucursal = info.IdSucursal,
                                    vt_IdBodega = info.IdBodega,
                                    vt_IdCbteVta = info.IdCbteVta,

                                    ct_IdEmpresa = diario.IdEmpresa,
                                    ct_IdTipoCbte = diario.IdTipoCbte,
                                    ct_IdCbteCble = diario.IdCbteCble,
                                });
                                db_f.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        ct_cbtecble_Info diario = armar_diario(info, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, persona == null ? "" : persona.pe_nombreCompleto);
                        if (diario != null)
                        {
                            diario.IdCbteCble = conta.ct_IdCbteCble;
                            data_ct.modificarDB(diario);
                        }
                    }
                }
                #endregion

                db_f.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                tb_LogError_Data LogData = new tb_LogError_Data();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message, Clase = "fa_factura_Data", Metodo = "modificarDB", IdUsuario = info.IdUsuario });
                return false;
            }
        }

        public bool modificarEstadoImpresion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, bool estado_impresion)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_factura.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta).FirstOrDefault();
                    if (Entity != null)
                    {
                        Entity.esta_impresa = estado_impresion;
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

        public bool anularDB(fa_factura_Info info)
        {
            try
            {
                #region Variables
                ct_cbtecble_Data odata_ct = new ct_cbtecble_Data();
                #endregion

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    fa_factura Entity = Context.fa_factura.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSucursal == info.IdSucursal && q.IdBodega == info.IdBodega && q.IdCbteVta == info.IdCbteVta);
                    if (Entity == null) return false;
                    {
                        Entity.MotivoAnulacion = info.MotivoAnulacion;
                        Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                        Entity.Estado = "I";
                    }
                    
                    var conta = Context.fa_factura_x_ct_cbtecble.Where(q => q.vt_IdEmpresa == info.IdEmpresa && q.vt_IdSucursal == info.IdSucursal && q.vt_IdBodega == info.IdBodega && q.vt_IdCbteVta == info.IdCbteVta).FirstOrDefault();
                    if (conta != null)
                        if (!odata_ct.anularDB(new ct_cbtecble_Info { IdEmpresa = conta.ct_IdEmpresa, IdTipoCbte = conta.ct_IdTipoCbte, IdCbteCble = conta.ct_IdCbteCble, IdUsuarioAnu = info.IdUsuarioUltAnu, cb_MotivoAnu = info.MotivoAnulacion }))
                        {
                            Entity.MotivoAnulacion = null;
                            Entity.IdUsuarioUltAnu = null;
                            Entity.Estado = "A";
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

        public bool MostrarCuotasRpt(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var fa = (from f in Context.fa_factura
                              where f.IdEmpresa == IdEmpresa
                              && f.IdSucursal == IdSucursal
                              && f.IdBodega == IdBodega
                              && f.IdCbteVta == IdCbteVta
                             join t in Context.fa_TerminoPago
                             on new { IdTerminoPago = f.vt_tipo_venta } equals new { t.IdTerminoPago }
                             select new
                             {
                                 Num_Coutas = t.Num_Coutas
                             }).FirstOrDefault();
                    if (fa.Num_Coutas > 0)
                        return true;
                }
                return false;
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Contabilizar(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string NombreContacto)
        {
            EntitiesFacturacion db = new EntitiesFacturacion();
            ct_cbtecble_Data data_ct = new ct_cbtecble_Data();
            try
            {
                var factura = get_info(IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
                if (factura != null)
                {
                    fa_factura_det_Data odata_det = new fa_factura_det_Data();
                    factura.lst_det = odata_det.get_list(factura.IdEmpresa, IdSucursal, IdBodega, IdCbteVta);
                }
                var parametro = db.fa_parametro.Where(q => q.IdEmpresa == factura.IdEmpresa).FirstOrDefault();
                var cliente = db.fa_cliente.Where(q => q.IdEmpresa == factura.IdEmpresa && q.IdCliente == factura.IdCliente).FirstOrDefault();
                if (!string.IsNullOrEmpty(cliente.IdCtaCble_cxc_Credito))
                {
                    var conta = db.fa_factura_x_ct_cbtecble.Where(q => q.vt_IdEmpresa == factura.IdEmpresa && q.vt_IdSucursal == factura.IdSucursal && q.vt_IdBodega == factura.IdBodega && q.vt_IdCbteVta == factura.IdCbteVta).FirstOrDefault();
                    if (conta == null)
                    {
                        ct_cbtecble_Info diario = armar_diario(factura, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, NombreContacto);
                        if (diario != null)
                        {
                            if (data_ct.guardarDB(diario))
                            {
                                db.fa_factura_x_ct_cbtecble.Add(new fa_factura_x_ct_cbtecble
                                {
                                    vt_IdEmpresa = factura.IdEmpresa,
                                    vt_IdSucursal = factura.IdSucursal,
                                    vt_IdBodega = factura.IdBodega,
                                    vt_IdCbteVta = factura.IdCbteVta,

                                    ct_IdEmpresa = diario.IdEmpresa,
                                    ct_IdTipoCbte = diario.IdTipoCbte,
                                    ct_IdCbteCble = diario.IdCbteCble,
                                });
                                db.SaveChanges();
                                return true;
                            }
                        }
                    }
                    else
                    {
                        ct_cbtecble_Info diario = armar_diario(factura, Convert.ToInt32(parametro.IdTipoCbteCble_Factura), cliente.IdCtaCble_cxc_Credito, parametro.pa_IdCtaCble_descuento, NombreContacto);
                        if (diario != null)
                        {
                            diario.IdCbteCble = conta.ct_IdCbteCble;
                            data_ct.modificarDB(diario);
                            return true;
                        }
                    }
                }

                db.Dispose();
                return false;
            }
            catch (Exception)
            {
                db.Dispose();
                throw;
            }
        }

        public bool modificarEstadoAutorizacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta)
        {
            try
            {
                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    var Entity = Context.fa_factura.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega && q.IdCbteVta == IdCbteVta).FirstOrDefault();
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

        public bool ValidarDocumentoAnulacion(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string vt_tipoDoc, ref string mensaje)
        {
            try
            {
                using (EntitiesFacturacion db = new EntitiesFacturacion())
                {
                    var obj = db.vwfa_factura_sin_automatico.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega_Cbte == IdBodega && q.IdCbte_vta_nota == IdCbteVta && q.dc_TipoDocumento == vt_tipoDoc && q.estado == "A").Count();
                    if (obj > 0)
                    {
                        mensaje = "El documento no puede ser anulado porque se encuentra parcial o totalmente cobrado";
                        return false;
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