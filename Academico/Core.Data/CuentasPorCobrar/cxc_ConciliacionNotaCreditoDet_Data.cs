using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Data
    {
        public List<cxc_ConciliacionNotaCreditoDet_Info> GetList(int IdEmpresa, decimal IdConciliacion)
        {
            try
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> Lista = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdConciliacion = item.IdConciliacion,
                            Secuencia = item.Secuencia,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVtaNota = item.IdCbteVtaNota,
                            vt_TipoDoc = item.vt_TipoDoc,
                            Valor = item.Valor,
                            ReferenciaDet = item.ReferenciaDet,
                            IdString = item.vt_TipoDoc + item.IdSucursal.ToString("0000") + item.IdBodega.ToString("0000") + item.IdCbteVtaNota.ToString("0000000000")
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

        public List<cxc_ConciliacionNotaCreditoDet_Info> GetListPorCruzar(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> Lista = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();
                    int Secuencia = 1;
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            Secuencia = Secuencia++,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVtaNota = item.IdComprobante,
                            vt_TipoDoc = item.vt_tipoDoc,
                            Valor = item.Saldo ?? 0,
                            Saldo = item.Saldo ?? 0,
                            vt_total = item.vt_total,
                            NomCliente = item.NomCliente,
                            vt_fecha = item.vt_fecha,
                            ReferenciaDet = item.Referencia,
                            ValorProntoPago = item.ValorProntoPago,
                            FechaProntoPago = item.FechaProntoPago,
                            vt_NumDocumento = item.vt_NunDocumento,
                            IdString = item.vt_tipoDoc+item.IdSucursal.ToString("0000")+item.IdBodega.ToString("0000")+item.IdComprobante.ToString("0000000000")
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
    }
}
