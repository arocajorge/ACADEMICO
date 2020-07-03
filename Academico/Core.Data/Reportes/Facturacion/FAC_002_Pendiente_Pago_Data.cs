using Core.Data.Base;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Facturacion
{
    public class FAC_002_Pendiente_Pago_Data
    {
        public List<FAC_002_Pendiente_Pago_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdAlumno)
        {
            try
            {
                List<FAC_002_Pendiente_Pago_Info> Lista = new List<FAC_002_Pendiente_Pago_Info>();
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.vwcxc_cartera_x_cobrar
                             where q.IdEmpresa == IdEmpresa
                             && q.IdSucursal == IdSucursal
                             && q.IdAlumno == IdAlumno
                             select new FAC_002_Pendiente_Pago_Info
                             {
                                 Referencia = q.Referencia,
                                 vt_total = q.vt_total,
                                 ValorProntoPago = q.ValorProntoPago,
                                 FechaProntoPago = q.FechaProntoPago,
                                 TotalxCobrado = q.TotalxCobrado,
                                 Saldo = q.Saldo,
                                 vt_fecha = q.vt_fecha
                             }).ToList();

                    var FechaHasta = Lista.Where(q => q.FechaProntoPago > DateTime.Now.Date).Min(q=> q.FechaProntoPago);
                    var ValorHasta = "VALOR A PAGAR HASTA ";
                    FechaHasta = FechaHasta ?? DateTime.Now.Date;
                    ValorHasta += Convert.ToDateTime(FechaHasta).ToString("dd/MM/yyyy");

                    var FechaDesde = Lista.Where(q => q.FechaProntoPago > DateTime.Now.Date).Max(q => q.FechaProntoPago);
                    var ValorDesde = "VALOR A PAGAR DESDE ";
                    FechaDesde = FechaDesde ?? DateTime.Now.Date;
                    FechaDesde = Convert.ToDateTime(FechaDesde).AddDays(1);
                    ValorDesde += Convert.ToDateTime(FechaDesde).ToString("dd/MM/yyyy");

                    var ValorProntoPagoHasta = "(-) PRONTO PAGO HASTA ";
                    ValorProntoPagoHasta += Convert.ToDateTime(FechaHasta).ToString("dd/MM/yyyy");

                    Lista.ForEach(q => { q.ValorDesde = ValorDesde; q.ValorHasta = ValorHasta; q.ValorProntoPagoHasta = ValorProntoPagoHasta; });
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
