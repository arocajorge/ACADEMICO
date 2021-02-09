using Core.Data.Base;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Facturacion
{
    public class fa_notaCreDeb_x_fa_factura_NotaDeb_Data
    {

        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list_cartera(int IdEmpresa, int IdSucursal, decimal IdCliente, bool mostrar_saldo0)
        {
            try
            {
                List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    if(!mostrar_saldo0)
                    Lista = (from q in Context.vwcxc_cartera_x_cobrar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdCliente == IdCliente
                             && q.Saldo > 0
                             && q.Estado == "A"
                             select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                             {
                                 IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                                 IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                                 IdBodega_fac_nd_doc_mod = q.IdBodega,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 IdCbteVta_fac_nd_doc_mod = q.IdComprobante,
                                 vt_NumDocumento = q.vt_NunDocumento,
                                 Observacion = q.Referencia,                                 
                                 vt_fecha = q.vt_fecha,
                                 vt_total = q.vt_total,
                                 Saldo = q.Saldo,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 NumDocumento = q.vt_NunDocumento
                             }).ToList();
                    else
                        Lista = (from q in Context.vwcxc_cartera_x_cobrar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdCliente == IdCliente
                                 && q.Estado == "A"
                                 select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                                 {
                                     IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                                     IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                                     IdBodega_fac_nd_doc_mod = q.IdBodega,
                                     vt_tipoDoc = q.vt_tipoDoc,
                                     IdCbteVta_fac_nd_doc_mod = q.IdComprobante,
                                     vt_NumDocumento = q.vt_NunDocumento,
                                     Observacion = q.Referencia,
                                     vt_fecha = q.vt_fecha,
                                     vt_total = q.vt_total,
                                     Saldo = q.Saldo,
                                     vt_Subtotal = q.vt_Subtotal,
                                     vt_iva = q.vt_iva,
                                     NumDocumento = q.vt_NunDocumento
                                 }).ToList();

                    Lista.ForEach(q => { q.secuencial = q.vt_tipoDoc + "-" + q.IdBodega_fac_nd_doc_mod.ToString() + "-" + q.IdCbteVta_fac_nd_doc_mod.ToString(); q.Valor_Aplicado = Convert.ToDouble(q.Saldo); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list_cartera_academico(int IdEmpresa, int IdSucursal, decimal IdCliente, decimal IdAlumno, bool mostrar_saldo0)
        {
            try
            {
                List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    if (!mostrar_saldo0)
                        Lista = (from q in Context.vwcxc_cartera_x_cobrar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdAlumno == IdAlumno
                                 && q.Saldo > 0
                                 && q.Estado == "A"
                                 select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                                 {
                                     IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                                     IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                                     IdBodega_fac_nd_doc_mod = q.IdBodega,
                                     vt_tipoDoc = q.vt_tipoDoc,
                                     IdCbteVta_fac_nd_doc_mod = q.IdComprobante,
                                     vt_NumDocumento = q.vt_NunDocumento,
                                     Observacion = q.Referencia,
                                     vt_fecha = q.vt_fecha,
                                     vt_total = q.vt_total,
                                     Saldo = q.Saldo,
                                     vt_Subtotal = q.vt_Subtotal,
                                     vt_iva = q.vt_iva,
                                     NumDocumento = q.vt_NunDocumento,
                                     TieneSaldo0 = false,
                                     SaldoProntoPago = q.SaldoProntoPago??0
                                 }).OrderBy(q => q.vt_fecha).ToList();
                    else
                        Lista = (from q in Context.vwcxc_cartera_x_cobrar
                                 where q.IdEmpresa == IdEmpresa
                                 && q.IdSucursal == IdSucursal
                                 && q.IdAlumno == IdAlumno
                                 && q.Estado == "A"
                                 select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                                 {
                                     IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                                     IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                                     IdBodega_fac_nd_doc_mod = q.IdBodega,
                                     vt_tipoDoc = q.vt_tipoDoc,
                                     IdCbteVta_fac_nd_doc_mod = q.IdComprobante,
                                     vt_NumDocumento = q.vt_NunDocumento,
                                     Observacion = q.Referencia,
                                     vt_fecha = q.vt_fecha,
                                     vt_total = q.vt_total,
                                     Saldo = q.Saldo,
                                     vt_Subtotal = q.vt_Subtotal,
                                     vt_iva = q.vt_iva,
                                     NumDocumento = q.vt_NunDocumento,
                                     TieneSaldo0 = false,
                                     SaldoProntoPago = q.SaldoProntoPago ?? 0
                                 }).OrderBy(q=> q.vt_fecha).ToList();

                    Lista.ForEach(q => { q.secuencial = q.vt_tipoDoc + "-" + q.IdBodega_fac_nd_doc_mod.ToString() + "-" + q.IdCbteVta_fac_nd_doc_mod.ToString(); q.Valor_Aplicado = Convert.ToDouble(q.Saldo);});
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list_cartera_saldo_cero(int IdEmpresa, int IdSucursal, decimal IdCliente, decimal IdAlumno)
        {
            try
            {
                List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> Lista = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>();

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = Context.vwcxc_cartera_cobrada_saldo0.Where(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdCliente == IdCliente).OrderBy(q=>q.vt_fecha).ToList();

                    foreach (var q in lst)
                    {
                        var info = new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                        {
                            IdEmpresa_fac_nd_doc_mod = q.IdEmpresa,
                            IdSucursal_fac_nd_doc_mod = q.IdSucursal,
                            IdBodega_fac_nd_doc_mod = q.IdBodega,
                            vt_tipoDoc = q.vt_tipoDoc,
                            IdCbteVta_fac_nd_doc_mod = q.IdCbteVta,
                            vt_NumDocumento = q.vt_NunDocumento,
                            Observacion = q.vt_Observacion,
                            vt_fecha = q.vt_fecha,
                            vt_total = Convert.ToDouble(q.Total),
                            NumDocumento = q.vt_NunDocumento,
                            Saldo = 0,
                            TieneSaldo0 = true
                        };
                        Lista.Add(info);
                    }
                    

                    Lista.ForEach(q => { q.secuencial = q.vt_tipoDoc + "-" + q.IdBodega_fac_nd_doc_mod.ToString() + "-" + q.IdCbteVta_fac_nd_doc_mod.ToString(); q.Valor_Aplicado = Convert.ToDouble(q.Saldo); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            try
            {
                List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info> Lista;

                using (EntitiesFacturacion Context = new EntitiesFacturacion())
                {
                    Lista = (from q in Context.vwfa_notaCreDeb_x_fa_factura_NotaDeb
                             where q.IdEmpresa_nt == IdEmpresa
                             && q.IdSucursal_nt == IdSucursal
                             && q.IdBodega_nt == IdBodega
                             && q.IdNota_nt == IdNota
                             select new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                             {
                                 IdEmpresa_fac_nd_doc_mod = q.IdEmpresa_nt,
                                 IdSucursal_fac_nd_doc_mod = q.IdSucursal_fac_nd_doc_mod,
                                 IdBodega_fac_nd_doc_mod = q.IdBodega_fac_nd_doc_mod,
                                 vt_tipoDoc = q.vt_tipoDoc,
                                 IdCbteVta_fac_nd_doc_mod = q.IdCbteVta_fac_nd_doc_mod,
                                 vt_NumDocumento = q.vt_NumFactura,
                                 Observacion = q.vt_Observacion,
                                 vt_fecha = q.vt_fecha,
                                 fecha_cruce = q.fecha_cruce,
                                 vt_total = q.vt_total,
                                 Saldo = q.saldo_sin_cobro,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 Saldo_final = q.saldo,
                                 seleccionado = true,
                                 Valor_Aplicado = q.Valor_Aplicado,
                                 NumDocumento = null,
                                 ValorProntoPago = q.ValorProntoPago ?? 0
                             }).ToList();
                }
                Lista.ForEach(q => { q.secuencial = q.vt_tipoDoc + "-" + q.IdBodega_fac_nd_doc_mod.ToString() + "-" + q.IdCbteVta_fac_nd_doc_mod.ToString(); });
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_notaCreDeb_x_fa_factura_NotaDeb_Info get_info_cartera_academico(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCliente, decimal IdAlumno, decimal IdCbteVta)
        {
            try
            {
                fa_notaCreDeb_x_fa_factura_NotaDeb_Info info = new fa_notaCreDeb_x_fa_factura_NotaDeb_Info(); ;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    vwcxc_cartera_x_cobrar Entity = Context.vwcxc_cartera_x_cobrar.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega
                    && q.IdCliente == IdCliente && q.IdAlumno == IdAlumno && q.IdComprobante == IdCbteVta && q.vt_tipoDoc=="FACT");
                    if (Entity == null) return null;

                    info = new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                    {
                        IdEmpresa_fac_nd_doc_mod = Entity.IdEmpresa,
                        IdSucursal_fac_nd_doc_mod = Entity.IdSucursal,
                        IdBodega_fac_nd_doc_mod = Entity.IdBodega,
                        vt_tipoDoc = Entity.vt_tipoDoc,
                        IdCbteVta_fac_nd_doc_mod = Entity.IdComprobante,
                        vt_NumDocumento = Entity.vt_NunDocumento,
                        Observacion = Entity.Referencia,
                        vt_fecha = Entity.vt_fecha,
                        vt_total = Entity.vt_total,
                        Saldo = Entity.Saldo,
                        vt_Subtotal = Entity.vt_Subtotal,
                        vt_iva = Entity.vt_iva,
                        NumDocumento = Entity.vt_NunDocumento
                    };

                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public fa_notaCreDeb_x_fa_factura_NotaDeb_Info get_info_SaldoDocumento(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCliente, decimal IdAlumno, decimal IdCbteVta, string vt_tipoDoc)
        {
            try
            {
                fa_notaCreDeb_x_fa_factura_NotaDeb_Info info = new fa_notaCreDeb_x_fa_factura_NotaDeb_Info(); ;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    vwcxc_cartera_x_cobrar Entity = Context.vwcxc_cartera_x_cobrar.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdSucursal == IdSucursal && q.IdBodega == IdBodega
                    && q.IdCliente == IdCliente && q.IdAlumno == IdAlumno && q.IdComprobante == IdCbteVta && q.vt_tipoDoc == vt_tipoDoc);
                    if (Entity == null) return null;

                    info = new fa_notaCreDeb_x_fa_factura_NotaDeb_Info
                    {
                        IdEmpresa_fac_nd_doc_mod = Entity.IdEmpresa,
                        IdSucursal_fac_nd_doc_mod = Entity.IdSucursal,
                        IdBodega_fac_nd_doc_mod = Entity.IdBodega,
                        vt_tipoDoc = Entity.vt_tipoDoc,
                        IdCbteVta_fac_nd_doc_mod = Entity.IdComprobante,
                        vt_NumDocumento = Entity.vt_NunDocumento,
                        Observacion = Entity.Referencia,
                        vt_fecha = Entity.vt_fecha,
                        vt_total = Entity.vt_total,
                        Saldo = Entity.Saldo,
                        vt_Subtotal = Entity.vt_Subtotal,
                        vt_iva = Entity.vt_iva,
                        NumDocumento = Entity.vt_NunDocumento
                    };

                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
