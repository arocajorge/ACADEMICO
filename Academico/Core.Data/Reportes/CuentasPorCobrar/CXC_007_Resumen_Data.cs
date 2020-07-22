using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_007_Resumen_Data
    {
        public List<CXC_007_Resumen_Info> GetList(int IdEmpresa, DateTime FechaCorte)
        {
            try
            {
                List<CXC_007_Resumen_Info> Lista = new List<CXC_007_Resumen_Info>();
                FechaCorte = FechaCorte.Date;

                using (EntitiesReportes db = new EntitiesReportes())
                {
                    db.Database.CommandTimeout = 3000;
                    var lst = db.SPCXC_007_Resumen(IdEmpresa, FechaCorte).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_007_Resumen_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVta = item.IdCbteVta,
                            vt_tipoDoc = item.vt_tipoDoc,
                            Total = item.Total,
                            dc_ValorPago = item.dc_ValorPago,
                            Saldo = item.Saldo,
                            IdAnio = item.IdAnio,
                            IdSede = item.IdSede,
                            IdJornada = item.IdJornada,
                            IdNivel = item.IdNivel,
                            IdCurso = item.IdCurso,
                            IdParalelo = item.IdParalelo,
                            NomSede = item.NomSede,
                            NomJornada = item.NomJornada,
                            NomNivel = item.NomNivel,
                            Anio = item.Anio,
                            NomRubro = item.NomRubro,
                            Cantidad = item.Cantidad
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
