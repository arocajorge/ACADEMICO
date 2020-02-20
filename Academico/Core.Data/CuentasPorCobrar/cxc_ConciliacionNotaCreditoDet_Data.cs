using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Data
    {
        public List<cxc_ConciliacionNotaCreditoDet_Info> GetList(int IdEmpresa,decimal IdConciliacion)
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
                            secuencia_nt = item.secuencia_nt,
                            Observacion = item.vt_Observacion,
                            
                            vt_fecha = item.vt_fecha ?? DateTime.Now.Date,
                            vt_total = item.Valor,
                            Saldo = 0,
                            vt_Subtotal = 0,
                            vt_iva = 0,
                            ValorProntoPago = item.ValorProntoPago,
                            //FechaProntoPago = item.FechaProntoPago,

                            IdAnio = item.IdAnio,
                            IdPlantilla = item.IdPlantilla,
                            IdPuntoVta = item.IdPuntoVta,
                            IdCliente = item.IdCliente ?? 0,
                            IdAlumno = item.IdAlumno,
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
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {

                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            vt_TipoDoc = item.vt_tipoDoc,
                            ReferenciaDet = item.vt_NunDocumento,
                            Observacion = item.Referencia,
                            IdCbteVtaNota = item.IdComprobante,
                            vt_fecha = item.vt_fecha,
                            vt_total = item.vt_total,
                            Saldo = item.Saldo,
                            vt_Subtotal = item.vt_Subtotal,
                            vt_iva = item.vt_iva,
                            vt_fech_venc = item.vt_fech_venc,
                            NomCliente = item.NomCliente,
                            ValorProntoPago = item.ValorProntoPago ?? 0,
                            FechaProntoPago = item.FechaProntoPago,

                            IdAnio = item.IdAnio,
                            IdPlantilla = item.IdPlantilla,
                            IdPuntoVta = item.IdPuntoVta,
                            IdCliente = item.IdCliente,
                            IdAlumno = item.IdAlumno,
                            secuencia = item.vt_tipoDoc + "-" + item.IdBodega.ToString() + "-" + item.IdComprobante.ToString()
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
