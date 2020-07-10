using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_011_Data
    {
        public List<CXC_011_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<CXC_011_Info> Lista = new List<CXC_011_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    var lst = Context.SPCXC_011(IdEmpresa, IdAlumno).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new CXC_011_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdCbteVta = q.IdCbteVta,
                            vt_fecha = q.vt_fecha,
                            vt_Observacion = q.vt_Observacion,
                            NomCurso = q.NomCurso,
                            NomJornada = q.NomJornada,
                            Saldo = q.Saldo,
                            NomNivel = q.NomNivel,
                            NomParalelo = q.NomParalelo,
                            Alumno = q.Alumno,
                            Codigo = q.Codigo,
                            dc_ValorPago =q.dc_ValorPago,
                            FechaProntoPago=q.FechaProntoPago,
                            Total=q.Total,
                            IdBodega=q.IdBodega,
                            IdSucursal=q.IdSucursal,
                            Representante=q.Representante,
                            ValorProntoPago=q.ValorProntoPago,
                            vt_tipoDoc=q.vt_tipoDoc,
                            
                        });
                    }

                    var FechaHasta = Lista.Where(q => q.FechaProntoPago > DateTime.Now.Date).Min(q => q.FechaProntoPago);
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


                    Lista.ForEach(q => {
                        q.ValorDesde = ValorDesde;
                        q.ValorHasta = ValorHasta;
                        q.ValorProntoPagoHasta = ValorProntoPagoHasta;
                        q.MostrarValoresDesdeHasta = (DateTime.Now > FechaHasta ? false : true);
                    });
                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
