using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_008_Data
    {
        public List<CXC_008_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, DateTime FechaCorte)
        {
            try
            {
                List<CXC_008_Info> Lista = new List<CXC_008_Info>();

                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.SPCXC_008(IdEmpresa, FechaCorte, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno).ToList();
                    foreach (var q in lst)
                    {
                        Lista.Add(new CXC_008_Info
                        {
                            Num = 1,
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdBodega = q.IdBodega,
                            IdCbteVta = q.IdCbteVta,
                            vt_fecha = q.vt_fecha,
                            vt_Observacion = q.vt_Observacion,
                            vt_NumFactura = q.vt_NumFactura,
                            IdAlumno = q.IdAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdAnio = q.IdAnio,
                            Periodo = q.Periodo,
                            Total = q.Total,
                            TotalPagado = q.TotalPagado,
                            Saldo = q.Saldo,
                            vt_fech_venc = q.vt_fech_venc,
                            Plazo = q.Plazo,
                            IdMatricula = q.IdMatricula,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo
                        });
                    }
                    if (Lista.Count > 0)
                    {
                        Lista = Lista.Where(q => q.IdMatricula != null).ToList();
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
