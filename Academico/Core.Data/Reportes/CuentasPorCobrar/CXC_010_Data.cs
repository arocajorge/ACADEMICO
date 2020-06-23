using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_010_Data
    {
        public List<CXC_010_Info> get_list(int IdEmpresa, decimal IdAlumno, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<CXC_010_Info> Lista = new List<CXC_010_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    var lst = Context.SPCXC_010(IdEmpresa, IdAlumno, FechaIni, FechaFin).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new CXC_010_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            IdCbteVta = q.IdCbteVta,
                            vt_fecha = q.vt_fecha,
                            vt_Observacion = q.vt_Observacion,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Debe = q.Debe,
                            Haber=q.Haber,
                            IdMes=q.IdMes,
                            Orden=q.Orden,
                            Referencia=q.Referencia,
                            SaldoInicial = q.SaldoInicial,
                            smes = q.smes,
                            Tipo = q.Tipo,
                            Valor=q.Valor,
                            Saldo = q.Saldo,
                            Anio = q.Anio
                        });
                    }
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
