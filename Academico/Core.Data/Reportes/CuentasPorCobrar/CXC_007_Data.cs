using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_007_Data
    {
        public List<CXC_007_Info> get_list(int IdEmpresa, DateTime fechaCorte)
        {
            try
            {
                fechaCorte = fechaCorte.Date;

                List<CXC_007_Info> Lista = new List<CXC_007_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    var lst = Context.SPCXC_007(IdEmpresa, fechaCorte).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new CXC_007_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdBodega = q.IdBodega,
                            IdAlumno = q.IdAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            IdCbteVta = q.IdCbteVta,
                            vt_fecha = q.vt_fecha,
                            vt_fech_venc = q.vt_fech_venc,
                            vt_NumFactura = q.vt_NumFactura,
                            vt_Observacion = q.vt_Observacion,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            NomCurso=q.NomCurso,
                            NomJornada=q.NomJornada,
                            NomNivel = q.NomNivel,
                            NomParalelo = q.NomParalelo,
                            OrdenCurso = q.OrdenCurso,
                            OrdenJornada=q.OrdenJornada,
                            OrdenNivel=q.OrdenNivel,
                            OrdenParalelo=q.OrdenParalelo,
                            Periodo =q.Periodo,
                            Plazo=q.Plazo,
                            Saldo=q.Saldo,
                            smes=q.smes,
                            Total=q.Total,
                            TotalPagado=q.TotalPagado,
                            VENCIDO_0_30 =q.VENCIDO_0_30,
                            VENCIDO_181=q.VENCIDO_181,
                            VENCIDO_31_60=q.VENCIDO_31_60,
                            VENCIDO_61_90=q.VENCIDO_61_90,
                            VENCIDO_91_180 =q.VENCIDO_91_180
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
