using Core.Data.Caja;
using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_CobroMasivo_Bus
    {
        cxc_CobroMasivo_Data odata = new cxc_CobroMasivo_Data();
        cxc_CobroMasivoDet_Data odata_det = new cxc_CobroMasivoDet_Data();
        cxc_cobro_Data odata_cobro = new cxc_cobro_Data();
        cxc_cobro_det_Data odata_cobro_det = new cxc_cobro_det_Data();
        caj_Caja_Data odata_caja = new caj_Caja_Data();
        public List<cxc_CobroMasivo_Info> GetList(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try {
                return odata.get_list(IdEmpresa, Fecha_ini, Fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public cxc_CobroMasivo_Info GetInfo(int IdEmpresa, decimal IdCobroMasivo)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdCobroMasivo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(cxc_CobroMasivo_Info info)
        {
            try
            {
                if (odata.guardarDB(info))
                {
                    var Caja = odata_caja.GetIdCajaPorSucursal(info.IdEmpresa, info.IdSucursal);
                   
                    foreach (var item in info.lst_det)
                    {
                        double SaldoReal = 0;
                        double ValorProntoPagoCxC = 0;
                        double ValorProntoPago = 0;
                        double dc_ValorProntoPago = 0;
                        double Saldo_final = 0;
                        double dc_ValorPago = 0;

                        #region Cabecera Cobro
                        var info_cobro = new cxc_cobro_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdCaja = Caja,
                            cr_Codigo = "",

                            IdCobro_tipo = info.IdCobro_tipo,
                            cr_Banco = info.cr_Banco,
                            cr_cuenta = info.cr_cuenta,
                            cr_NumDocumento = info.cr_NumDocumento,
                            IdBanco = info.IdBanco,
                            IdTarjeta = info.IdTarjeta,
                            cr_Tarjeta = info.cr_Tarjeta,

                            IdCliente = item.IdCliente,
                            IdAlumno = item.IdAlumno,
                            cr_fecha = item.Fecha.Date,
                            cr_fechaDocu = item.Fecha.Date,
                            cr_fechaCobro = item.Fecha.Date,
                            cr_ObservacionPantalla = info.Observacion,
                            IdUsuario = info.IdUsuarioCreacion
                        };
                        info_cobro.lst_det = new List<cxc_cobro_det_Info>();
                        #endregion

                        SaldoReal = item.Valor;
                        var lst_deuda = odata_cobro_det.get_list_cartera_x_alumno(info.IdEmpresa, info.IdSucursal, item.IdAlumno).ToList();
                        foreach (var item_cxc in lst_deuda)
                        {
                            var Secuencia = 1;
                            ValorProntoPago = Math.Round((item_cxc.vt_total - item_cxc.ValorProntoPago ?? 0), 2, MidpointRounding.AwayFromZero);
                            if (SaldoReal > 0)
                            {
                                dc_ValorProntoPago = Math.Round(SaldoReal, 2, MidpointRounding.AwayFromZero) >= Math.Round((Convert.ToDouble(item_cxc.Saldo) - ValorProntoPago), 2, MidpointRounding.AwayFromZero) ? Math.Round(ValorProntoPago, 2, MidpointRounding.AwayFromZero) : 0;
                                dc_ValorPago = Math.Round(SaldoReal, 2, MidpointRounding.AwayFromZero) >= Math.Round((Convert.ToDouble(item_cxc.Saldo) - ValorProntoPago), 2, MidpointRounding.AwayFromZero) ? Math.Round(Convert.ToDouble(item_cxc.Saldo) - ValorProntoPago, 2, MidpointRounding.AwayFromZero) : Math.Round(SaldoReal, 2, MidpointRounding.AwayFromZero);
                                Saldo_final = Math.Round(Convert.ToDouble(item_cxc.Saldo - ValorProntoPago) - item_cxc.dc_ValorPago, 2, MidpointRounding.AwayFromZero);
                                ValorProntoPagoCxC = ValorProntoPago;
                                SaldoReal = Math.Round(Convert.ToDouble(SaldoReal-dc_ValorPago), 2, MidpointRounding.AwayFromZero);

                                info_cobro.lst_det.Add(new cxc_cobro_det_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = info.IdSucursal,
                                    secuencial = Secuencia++,
                                    IdBodega_Cbte = item_cxc.IdBodega_Cbte,
                                    IdCbte_vta_nota = item_cxc.IdCbte_vta_nota,
                                    IdUsuario = info.IdUsuarioCreacion,
                                    Fecha_Transac = DateTime.Now,
                                    estado = "A",
                                    IdCobro_tipo_det = info_cobro.IdCobro_tipo,
                                    dc_TipoDocumento = item_cxc.dc_TipoDocumento,
                                    ValorProntoPago = ValorProntoPagoCxC,
                                    dc_ValorPago = dc_ValorPago,
                                    dc_ValorProntoPago = dc_ValorProntoPago,
                                    IdNotaCredito = item_cxc.IdNotaCredito,
                                    Saldo_final =Saldo_final,
                                    Saldo = SaldoReal
                                });
                            }

                        }
                        info_cobro.cr_TotalCobro = info_cobro.lst_det.Sum(q => q.dc_ValorPago);
                        odata_cobro.guardarDB(info_cobro);

                        item.IdSucursal = info_cobro.IdSucursal;
                        item.IdCobro = info_cobro.IdCobro;
                        odata_det.modificarDB(item);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AnularDB(cxc_CobroMasivo_Info info)
        {
            try
            {
                if (odata.anularDB(info))
                {
                    var lst = info.lst_det;
                    foreach (var item in lst)
                    {
                        var info_cobro = odata_cobro.get_info(info.IdEmpresa, Convert.ToInt32(item.IdSucursal), Convert.ToDecimal(item.IdCobro));
                        info_cobro.IdUsuarioUltAnu = info.IdUsuarioAnulacion;
                        info_cobro.MotiAnula = info.MotivoAnulacion;
                        odata_cobro.anularDB(info_cobro);
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
