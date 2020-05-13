using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_009_Data
    {
        public List<CXC_009_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                List<CXC_009_Info> Lista = new List<CXC_009_Info>();
                FechaIni = FechaIni.Date;
                FechaFin = FechaFin.Date;
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    var lst = db.SPCXC_009(IdEmpresa, FechaIni, FechaFin).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new CXC_009_Info
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdCobro = item.IdCobro,
                            secuencial = item.secuencial,
                            IdBodega_Cbte = item.IdBodega_Cbte,
                            IdCbte_vta_nota = item.IdCbte_vta_nota,
                            dc_TipoDocumento = item.dc_TipoDocumento,
                            dc_ValorPago = item.dc_ValorPago,
                            cr_fecha = item.cr_fecha,
                            vt_fecha = item.vt_fecha,
                            Periodo = item.Periodo,
                            pe_nombreCompleto = item.pe_nombreCompleto,
                            Codigo = item.Codigo,
                            IdCobro_tipo = item.IdCobro_tipo,
                            Tipo = item.Tipo,
                            Orden = item.Orden,
                            OrdenRubros = item.OrdenRubros,
                            Observacion = item.Observacion
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
