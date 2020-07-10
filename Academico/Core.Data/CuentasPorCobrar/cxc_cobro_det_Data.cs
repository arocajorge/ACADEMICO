using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_cobro_det_Data
    {
        public List<cxc_cobro_det_Info> get_list_cartera(int IdEmpresa, int IdSucursal, decimal IdCliente, bool FiltrarPorCliente)
        {
            try
            {
                List<cxc_cobro_det_Info> Lista = new List<cxc_cobro_det_Info>();

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = Context.vwcxc_cartera_x_cobrar.Where(q=> q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             
                             && q.IdCliente == (FiltrarPorCliente == true ? IdCliente : q.IdCliente)
                             && q.IdAlumno == (FiltrarPorCliente == false ? IdCliente : q.IdAlumno)

                             && q.Saldo > 0).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new cxc_cobro_det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdBodega_Cbte = q.IdBodega,
                            dc_TipoDocumento = q.vt_tipoDoc,
                            vt_NumDocumento = q.vt_NunDocumento,
                            Observacion = q.Referencia,
                            IdCbte_vta_nota = q.IdComprobante,
                            vt_fecha = q.vt_fecha,
                            vt_total = q.vt_total,
                            Saldo = q.Saldo,
                            vt_Subtotal = q.vt_Subtotal,
                            vt_iva = q.vt_iva,
                            vt_fech_venc = q.vt_fech_venc,
                            NomCliente = q.NomCliente,
                            ValorProntoPago = q.ValorProntoPago,
                            FechaProntoPago = q.FechaProntoPago,

                            IdAnio = q.IdAnio,
                            IdPlantilla = q.IdPlantilla,
                            IdPuntoVta = q.IdPuntoVta,
                            IdCliente = q.IdCliente,
                            IdAlumno = q.IdAlumno
                        });
                    }

                    Lista.ForEach(q => { q.secuencia = q.dc_TipoDocumento + "-" + q.IdBodega_Cbte.ToString() + "-" + q.IdCbte_vta_nota.ToString(); q.dc_ValorPago = Convert.ToDouble(q.Saldo); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list_cartera_x_alumno(int IdEmpresa, int IdSucursal, decimal IdAlumno)
        {
            try
            {
                List<cxc_cobro_det_Info> Lista = new List<cxc_cobro_det_Info>();

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = Context.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal && q.IdAlumno == IdAlumno).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new cxc_cobro_det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdBodega_Cbte = q.IdBodega,
                            dc_TipoDocumento = q.vt_tipoDoc,
                            vt_NumDocumento = q.vt_NunDocumento,
                            Observacion = q.Referencia,
                            IdCbte_vta_nota = q.IdComprobante,
                            vt_fecha = q.vt_fecha,
                            vt_total = q.vt_total,
                            Saldo = q.Saldo,
                            vt_Subtotal = q.vt_Subtotal,
                            vt_iva = q.vt_iva,
                            vt_fech_venc = q.vt_fech_venc,
                            NomCliente = q.NomCliente,
                            ValorProntoPago = q.ValorProntoPago,
                            FechaProntoPago = q.FechaProntoPago,
                            IdAnio = q.IdAnio,
                            IdPlantilla = q.IdPlantilla,
                            IdPuntoVta = q.IdPuntoVta,
                            IdCliente = q.IdCliente,
                            IdAlumno = q.IdAlumno
                        });
                    }

                    Lista.ForEach(q => { q.secuencia = q.dc_TipoDocumento + "-" + q.IdBodega_Cbte.ToString() + "-" + q.IdCbte_vta_nota.ToString(); q.dc_ValorPago = Convert.ToDouble(q.Saldo); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdCobro)
        {
            try
            {
                List<cxc_cobro_det_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cobro_det
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdCobro == IdCobro
                             select new cxc_cobro_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega_Cbte = q.IdBodega_Cbte,
                                 dc_TipoDocumento = q.dc_TipoDocumento,
                                 vt_NumDocumento = q.vt_NumFactura,
                                 Observacion = q.vt_Observacion,
                                 IdCbte_vta_nota = q.IdCbte_vta_nota,
                                 vt_fecha = q.vt_fecha,
                                 vt_total = q.vt_total,
                                 Saldo = q.saldo_sin_cobro,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 vt_fech_venc = q.vt_fech_venc,
                                 IdCobro_tipo_det = q.IdCobro_tipo,
                                 IdCobro = q.IdCobro,
                                 dc_ValorPago = q.dc_ValorPago,
                                 IdNotaCredito = q.IdNotaCredito,
                                 dc_ValorProntoPago = q.dc_ValorProntoPago,
                                 IdAnio = q.IdAnio,
                                 IdPlantilla = q.IdPlantilla,
                                 IdPuntoVta = q.IdPuntoVta,
                                 IdAlumno = q.IdAlumno
                             }).ToList();

                    Lista.ForEach(q => { q.secuencia = q.dc_TipoDocumento +"-"+ q.IdBodega_Cbte.ToString() +"-"+ q.IdCbte_vta_nota.ToString(); q.Saldo_final = Convert.ToDouble(q.Saldo - q.dc_ValorPago); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, string dc_TipoDocumento)
        {
            try
            {
                List<cxc_cobro_det_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cobro_det_retencion
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdBodega_Cbte == IdBodega
                             && q.IdCbte_vta_nota == IdCbteVta
                             && q.dc_TipoDocumento == dc_TipoDocumento
                             select new cxc_cobro_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega_Cbte = q.IdBodega_Cbte,
                                 IdCbte_vta_nota = q.IdCbte_vta_nota,
                                 dc_TipoDocumento = q.dc_TipoDocumento,
                                 IdCobro = q.IdCobro,
                                 secuencial = q.secuencial,
                                 IdCobro_tipo_det = q.IdCobro_tipo,
                                 dc_ValorPago = q.dc_ValorPago,
                                 tc_descripcion = q.tc_descripcion,
                                 ESRetenIVA = q.ESRetenIVA,
                                 ESRetenFTE = q.ESRetenFTE,
                                 PorcentajeRet = q.PorcentajeRet,
                                 cr_fecha = q.cr_fecha,
                                 cr_NumDocumento = q.cr_NumDocumento
                             }).ToList();
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list_AP(int IdEmpresa)
        {
            try
            {
                List<cxc_cobro_det_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cartera_x_cobrar
                             where q.IdEmpresa == IdEmpresa && q.vt_tipoDoc=="FACT"
                             select new cxc_cobro_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega_Cbte = q.IdBodega,
                                 dc_TipoDocumento = q.vt_tipoDoc,
                                 vt_NumDocumento = q.vt_NunDocumento,
                                 IdCbte_vta_nota = q.IdComprobante,
                                 vt_fecha = q.vt_fecha,
                                 vt_total = q.vt_total,
                                 Saldo = q.Saldo,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 vt_fech_venc = q.vt_fech_venc,
                                 dc_ValorProntoPago = q.ValorProntoPago,
                                 IdAnio = q.IdAnio,
                                 IdPlantilla = q.IdPlantilla,
                                 IdPuntoVta = q.IdPuntoVta,
                                 IdAlumno = q.IdAlumno
                             }).ToList();

                    Lista.ForEach(q => { q.secuencia = q.dc_TipoDocumento + "-" + q.IdBodega_Cbte.ToString() + "-" + q.IdCbte_vta_nota.ToString(); q.Saldo_final = Convert.ToDouble(q.Saldo - q.dc_ValorPago); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_cobro_det_Info> get_list_AP(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<cxc_cobro_det_Info> Lista;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cartera_x_cobrar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdAlumno == IdAlumno
                             select new cxc_cobro_det_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega_Cbte = q.IdBodega,
                                 dc_TipoDocumento = q.vt_tipoDoc,
                                 vt_NumDocumento = q.vt_NunDocumento,
                                 IdCbte_vta_nota = q.IdComprobante,
                                 vt_fecha = q.vt_fecha,
                                 vt_total = q.vt_total,
                                 Saldo = q.Saldo,
                                 vt_Subtotal = q.vt_Subtotal,
                                 vt_iva = q.vt_iva,
                                 vt_fech_venc = q.vt_fech_venc,
                                 dc_ValorProntoPago = q.ValorProntoPago,
                                 ValorProntoPago = q.ValorProntoPago,
                                 IdAnio = q.IdAnio,
                                 IdPlantilla = q.IdPlantilla,
                                 IdPuntoVta = q.IdPuntoVta,
                                 IdAlumno = q.IdAlumno
                             }).ToList();

                    Lista.ForEach(q => { q.secuencia = q.dc_TipoDocumento + "-" + q.IdBodega_Cbte.ToString() + "-" + q.IdCbte_vta_nota.ToString(); q.Saldo_final = Convert.ToDouble(q.Saldo - q.dc_ValorPago); });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
