using Core.Data.CuentasPorCobrar;
using Core.Data.Facturacion;
using Core.Info.CuentasPorCobrar;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Facturacion
{
    public class fa_AplicacionMasiva_Bus
    {
        fa_AplicacionMasiva_Data odata = new fa_AplicacionMasiva_Data();
        fa_notaCreDeb_Data odata_nc = new fa_notaCreDeb_Data();
        cxc_cobro_det_Data odata_cxc = new cxc_cobro_det_Data();
        cxc_ConciliacionNotaCredito_Data odata_conciliacion = new cxc_ConciliacionNotaCredito_Data();
        public List<fa_AplicacionMasiva_Info> Get_list(int IdEmpresa, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, Fecha_ini, Fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public fa_AplicacionMasiva_Info Get_info(int IdEmpresa, decimal IdNCMasivo)
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

        public bool GuardarDB(fa_AplicacionMasiva_Info info)
        {
            try
            {
                if (odata.guardarDB(info))
                {
                    foreach (var item in info.lst_det)
                    {
                        double SaldoRealNC = 0;
                        double ValorProntoPagoCxC = 0;
                        double ValorProntoPago = 0;
                        double Valor = 0;
                        double Saldo_final = 0;
                        //double Saldo = 0;

                        var lst_nc = odata_nc.get_list_credito_favor(info.IdEmpresa, item.IdAlumno).ToList();
                        //10
                        foreach (var item_nc in lst_nc)
                        {
                            SaldoRealNC = item_nc.sc_saldo ?? 0;
                            #region Cabecera
                            var info_conciliacion = new cxc_ConciliacionNotaCredito_Info
                            {
                                IdEmpresa = item_nc.IdEmpresa,
                                IdAplicacion = info.IdAplicacion,
                                IdAlumno = item.IdAlumno,
                                IdSucursal = item_nc.IdSucursal,
                                IdBodega = item_nc.IdBodega,
                                IdNota = item_nc.IdNota,
                                Fecha = info.Fecha,
                                Valor = SaldoRealNC,
                                IdUsuarioCreacion = info.IdUsuarioCreacion,
                                Observacion = info.Observacion,
                            };
                            #endregion
                            
                            info_conciliacion.ListaDet = new List<cxc_ConciliacionNotaCreditoDet_Info>();
                            var Secuencia = 1;
                            var lst_cxc = odata_cxc.get_list_AP(info.IdEmpresa, item.IdAlumno).Where(q => q.IdEmpresa == item.IdEmpresa && q.IdAlumno == item.IdAlumno).OrderByDescending(q => q.cr_fecha).ToList();
                            
                            foreach (var item_cxc in lst_cxc)
                            {
                                
                                if (SaldoRealNC > 0)
                                {
                                    ValorProntoPagoCxC = Math.Round((item_cxc.vt_total - item_cxc.dc_ValorProntoPago ?? 0), 2, MidpointRounding.AwayFromZero);
                                    //ValorProntoPago = SaldoRealNC >= (item_cxc.Saldo - ValorProntoPagoCxC) ? ValorProntoPagoCxC : 0;
                                    ValorProntoPago = Math.Round(SaldoRealNC, 2, MidpointRounding.AwayFromZero) >= Math.Round((Convert.ToDouble(item_cxc.Saldo) - ValorProntoPagoCxC), 2, MidpointRounding.AwayFromZero) ? Math.Round(ValorProntoPagoCxC, 2, MidpointRounding.AwayFromZero) : 0;

                                    Valor = SaldoRealNC >= Convert.ToDouble(item_cxc.Saldo - ValorProntoPagoCxC) ? Convert.ToDouble(item_cxc.Saldo) - ValorProntoPago : SaldoRealNC;
                                    Saldo_final = Convert.ToDouble(item_cxc.Saldo - ValorProntoPago) - Valor;
                                    SaldoRealNC = SaldoRealNC - Valor;

                                    info_conciliacion.ListaDet.Add(new cxc_ConciliacionNotaCreditoDet_Info
                                    {
                                        IdEmpresa = item_nc.IdEmpresa,
                                        Secuencia = Secuencia++,
                                        IdSucursal = item_nc.IdSucursal,
                                        IdBodega = item_nc.IdBodega,
                                        IdCbteVtaNota = item_cxc.IdCbte_vta_nota,
                                        vt_TipoDoc = item_cxc.dc_TipoDocumento,
                                        ValorProntoPago = ValorProntoPago,
                                        Valor = Valor,
                                        Saldo_final = Saldo_final,
                                        Saldo = SaldoRealNC

                                        //secuencia_nt = item_nc.
                                    });
                                }
                                
                            }
                            if(info_conciliacion.ListaDet.Count >0)
                            {
                                info_conciliacion.Valor = info_conciliacion.ListaDet.Sum(q => q.Valor);

                                odata_conciliacion.GuardarDB(info_conciliacion);
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

        public bool AnularDB(fa_AplicacionMasiva_Info info)
        {
            try
            {
                if (odata.anularDB(info))
                {
                    var lst = odata_conciliacion.GetList_X_Aplicacion(info.IdEmpresa, info.IdAplicacion);
                    foreach (var item in lst)
                    {
                        item.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                        item.MotivoAnulacion = info.MotivoAnulacion;
                        odata_conciliacion.AnularDB(item);
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
