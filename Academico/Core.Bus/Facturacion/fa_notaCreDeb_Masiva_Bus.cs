using Core.Data.Academico;
using Core.Data.Facturacion;
using Core.Data.General;
using Core.Data.Inventario;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Facturacion
{
    public class fa_notaCreDeb_Masiva_Bus
    {
        fa_notaCreDeb_Masiva_Data odata = new fa_notaCreDeb_Masiva_Data();
        fa_notaCreDeb_MasivaDet_Data odata_det = new fa_notaCreDeb_MasivaDet_Data();
        fa_PuntoVta_Data bus_punto_venta = new fa_PuntoVta_Data();
        tb_sis_Documento_Tipo_Talonario_Data bus_talonario = new tb_sis_Documento_Tipo_Talonario_Data();
        aca_AnioLectivo_Data bus_anio = new aca_AnioLectivo_Data();
        aca_Matricula_Data bus_matricula = new aca_Matricula_Data();
        fa_TipoNota_Data bus_tipo_nota = new fa_TipoNota_Data();
        tb_sis_Impuesto_Data bus_impuesto = new tb_sis_Impuesto_Data();
        fa_notaCreDeb_Data odata_ncd = new fa_notaCreDeb_Data();
        in_Producto_Data odata_prod = new in_Producto_Data();
        fa_factura_Data odata_fact = new fa_factura_Data();
        fa_notaCreDeb_x_fa_factura_NotaDeb_Data odata_saldo = new fa_notaCreDeb_x_fa_factura_NotaDeb_Data();

        public List<fa_notaCreDeb_Masiva_Info> Get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, Fecha_ini, Fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_notaCreDeb_Masiva_Info Get_info(int IdEmpresa, decimal IdNCMasivo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdNCMasivo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(fa_notaCreDeb_Masiva_Info info)
        {
            try
            {
                var info_tipo_nota = bus_tipo_nota.get_info(info.IdEmpresa, info.IdTipoNota);
                var info_producto = odata_prod.get_info(info.IdEmpresa, Convert.ToDecimal(info_tipo_nota.IdProducto));
                var info_ImpuestoIVA = bus_impuesto.get_info(info_producto.IdCod_Impuesto_Iva);

                if (odata.guardarDB(info))
                {
                    foreach (var item in info.lst_det)
                    {
                        var info_ncd = new fa_notaCreDeb_Info();
                        info_ncd.lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>();
                        info_ncd.lst_det = new List<fa_notaCreDeb_det_Info>();
                        info_ncd.info_resumen = new fa_notaCreDeb_resumen_Info();

                        #region Nota CreDeb
                        info_ncd.IdEmpresa = info.IdEmpresa;
                        info_ncd.IdSucursal = info.IdSucursal;
                        info_ncd.IdBodega = info.IdBodega;
                        info_ncd.CreDeb = info.CreDeb;
                        info_ncd.CodDocumentoTipo = (info.CreDeb == "D" ? "NTDB": "NTCR") ;
                        info_ncd.IdAlumno = item.IdAlumno;
                        info_ncd.IdCliente = item.IdCliente;
                        info_ncd.no_fecha = info.no_fecha;
                        info_ncd.no_fecha_venc = info.no_fecha_venc;
                        info_ncd.IdTipoNota = info.IdTipoNota;
                        info_ncd.sc_observacion = item.ObservacionDet;
                        info_ncd.CodNota = "PROCESO MASIVO NDC";
                        info_ncd.IdUsuario = info.IdUsuarioCreacion;
                        info_ncd.Estado = "A";
                        info_ncd.NaturalezaNota = info.NaturalezaNota;
                        info_ncd.IdCtaCble_TipoNota = info.IdCtaCble_TipoNota;
                        info_ncd.IdPuntoVta = info.IdPuntoVta;
                        info_ncd.IdCtaCble_TipoNota = info.IdCtaCble_TipoNota;
                        info_ncd.aprobada_enviar_sri = false;
                        info_ncd.IdCobro_tipo = (info.CreDeb == "D" ? "NTDB" : "NTCR");

                        #region Resumen
                        var info_anio = bus_anio.getInfo_AnioEnCurso(info_ncd.IdEmpresa, 0);
                        var info_matricula = bus_matricula.getInfo_ExisteMatricula(info_ncd.IdEmpresa, (info_anio == null ? 0 : info_anio.IdAnio), Convert.ToDecimal(info_ncd.IdAlumno));

                        decimal ValorIVA = 0;
                        decimal ValorTotal = 0;

                        var Descuento = 0;
                        ValorIVA = Math.Round( Convert.ToDecimal(item.Subtotal * (info_ImpuestoIVA.porcentaje / 100)), 2, MidpointRounding.AwayFromZero);
                        ValorTotal = Math.Round((Convert.ToDecimal(item.Subtotal) + ValorIVA), 2, MidpointRounding.AwayFromZero);
                        var SubtotalIVASinDscto = Math.Round((info_ImpuestoIVA.porcentaje > 0 ? Convert.ToDecimal(item.Subtotal):0), 2, MidpointRounding.AwayFromZero);
                        var SubtotalSinIVASinDscto = Math.Round((info_ImpuestoIVA.porcentaje == 0 ? Convert.ToDecimal(item.Subtotal):0), 2, MidpointRounding.AwayFromZero);
                        var SubtotalIVAConDscto = Math.Round((info_ImpuestoIVA.porcentaje > 0 ? Convert.ToDecimal(item.Subtotal) : 0), 2, MidpointRounding.AwayFromZero);
                        var SubtotalSinIVAConDscto = Math.Round((info_ImpuestoIVA.porcentaje == 0 ? Convert.ToDecimal(item.Subtotal) : 0), 2, MidpointRounding.AwayFromZero);
                        var SubtotalSinDscto = Math.Round((SubtotalIVASinDscto+ SubtotalSinIVASinDscto), 2, MidpointRounding.AwayFromZero);
                        var SubtotalConDscto = Math.Round((SubtotalIVAConDscto + SubtotalSinIVAConDscto), 2, MidpointRounding.AwayFromZero);
                        var Total = Math.Round(Convert.ToDecimal(ValorTotal), 2, MidpointRounding.AwayFromZero);
                        decimal PorIVA = Convert.ToDecimal(info_ImpuestoIVA.porcentaje);

                        info_ncd.info_resumen = new fa_notaCreDeb_resumen_Info
                        {
                            IdEmpresa = info_ncd.IdEmpresa,
                            IdSucursal = info_ncd.IdSucursal,
                            IdBodega = info_ncd.IdBodega,
                            IdNota = info_ncd.IdNota,
                            SubtotalIVASinDscto = SubtotalIVASinDscto,
                            SubtotalSinIVASinDscto = SubtotalSinIVASinDscto,
                            SubtotalSinDscto = SubtotalSinDscto,
                            Descuento = Descuento,
                            SubtotalIVAConDscto = SubtotalIVAConDscto,
                            SubtotalSinIVAConDscto = SubtotalSinIVAConDscto,
                            SubtotalConDscto = SubtotalConDscto,
                            PorIva = PorIVA,
                            ValorIVA = ValorIVA,
                            Total = Total,
                            IdCod_Impuesto_IVA = info_ImpuestoIVA.IdCod_Impuesto,
                            IdAnio = (info_anio == null ? (int?)null : info_anio.IdAnio),
                            IdMatricula = (info_matricula == null ? (decimal?)null : info_matricula.IdMatricula)
                        };
                        #endregion
                        
                        info_ncd.lst_det = new List<fa_notaCreDeb_det_Info>
                        {
                            new fa_notaCreDeb_det_Info
                            {
                                IdEmpresa = info_ncd.IdEmpresa,
                                IdSucursal = info_ncd.IdSucursal,
                                IdBodega = info_ncd.IdBodega,
                                IdNota = info_ncd.IdNota,
                                Secuencia = 1,
                                IdProducto = Convert.ToDecimal(info_tipo_nota.IdProducto),
                                sc_cantidad = 1,
                                sc_cantidad_factura = 0,
                                sc_Precio = Convert.ToDouble(info_ncd.info_resumen.SubtotalConDscto),
                                sc_descUni = 0,
                                sc_PordescUni = 0,
                                sc_precioFinal = Convert.ToDouble(info_ncd.info_resumen.SubtotalConDscto),
                                vt_por_iva = Convert.ToDouble(info_ncd.info_resumen.PorIva),
                                sc_iva = Convert.ToDouble(info_ncd.info_resumen.ValorIVA),
                                IdCod_Impuesto_Iva = info_ImpuestoIVA.IdCod_Impuesto,
                                sc_subtotal = Convert.ToDouble(info_ncd.info_resumen.SubtotalConDscto),
                                sc_total = Convert.ToDouble(info_ncd.info_resumen.Total),
                                IdCentroCosto = null,
                                IdPunto_Cargo = null,
                                IdPunto_cargo_grupo = null
                            }
                        };

                        #region Cruce
                        info_ncd.lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>();
                        var info_factura = odata_fact.get_info(info_ncd.IdEmpresa, info_ncd.IdSucursal, info_ncd.IdBodega, item.IdAlumno, item.vt_serie1, item.vt_serie2, item.vt_NumFactura);
                        var info_saldo = odata_saldo.get_info_cartera_academico(info_ncd.IdEmpresa, info_ncd.IdSucursal, info_ncd.IdBodega, item.IdCliente, item.IdAlumno, info_factura.IdCbteVta);

                        if (info_factura != null)
                        {
                            info_ncd.lst_cruce.Add(new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                                {
                                    IdEmpresa_nt = info_ncd.IdEmpresa,
                                    IdSucursal_nt = info_ncd.IdSucursal,
                                    IdBodega_nt = info_ncd.IdBodega,
                                    secuencia = 1,
                                    IdEmpresa_fac_nd_doc_mod = info_factura.IdEmpresa,
                                    IdSucursal_fac_nd_doc_mod = info_factura.IdSucursal,
                                    IdBodega_fac_nd_doc_mod = info_factura.IdBodega,
                                    IdCbteVta_fac_nd_doc_mod = info_factura.IdCbteVta,
                                    vt_tipoDoc = info_factura.vt_tipoDoc,
                                    Valor_Aplicado = (info_saldo==null || info_saldo.Saldo==0 || info_saldo.Saldo< item.Subtotal ? 0 : item.Subtotal),
                                    fecha_cruce = DateTime.Now,
                                    ValorProntoPago = 0
                            });
                        }
                        #endregion

                        odata_ncd.guardarDB(info_ncd);

                        item.IdSucursal = info_ncd.IdSucursal;
                        item.IdBodega = info_ncd.IdBodega;
                        item.IdNota = info_ncd.IdNota;
                        odata_det.modificarDB(item);
                    }
                    #endregion

                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AnularDB(fa_notaCreDeb_Masiva_Info info)
        {
            try
            {
                if (odata.anularDB(info))
                {
                    foreach (var item in info.lst_det)
                    {
                        var info_nota = odata_ncd.get_info(info.IdEmpresa, Convert.ToInt32(item.IdSucursal), Convert.ToInt32(item.IdBodega), Convert.ToInt32(item.IdNota));

                        info_nota.IdUsuarioUltAnu = info.IdUsuarioAnulacion;
                        info_nota.MotiAnula = info.MotivoAnulacion;

                        odata_ncd.anularDB(info_nota);
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
