using Core.Data.Base;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.CuentasPorCobrar
{
    public class CXC_003_Data
    {
        public List<CXC_003_Info> get_list(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, string IdUsuario)
        {
            try
            {
                List<CXC_003_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.VWCXC_003
                             where q.IdEmpresa == IdEmpresa
                             && q.cr_fecha >= fecha_ini
                             && q.cr_fecha <= fecha_fin
                             && q.IdUsuario == IdUsuario
                             select new CXC_003_Info
                             {
                                IdEmpresa = q.IdEmpresa,
                                AnioFactura = q.AnioFactura,
                                CodigAlumno = q.CodigAlumno,
                                cr_Banco = q.cr_Banco,
                                cr_fecha = q.cr_fecha,
                                dc_TipoDocumento = q.dc_TipoDocumento,
                                dc_ValorPago = q.dc_ValorPago,
                                dc_ValorPagoNC = q.dc_ValorPagoNC,
                                IdAlumno = q.IdAlumno,
                                IdBodega_Cbte = q.IdBodega_Cbte,
                                IdCbte_vta_nota = q.IdCbte_vta_nota,
                                IdCobro = q.IdCobro,
                                IdCobro_tipo = q.IdCobro_tipo,
                                IdSucursal = q.IdSucursal,
                                IdTarjeta = q.IdTarjeta,
                                IdUsuario = q.IdUsuario,
                                NombreTarjeta = q.NombreTarjeta,
                                NombreUsuario = q.NombreUsuario,
                                NumFactura = q.NumFactura,
                                pe_nombreCompleto =q.pe_nombreCompleto,
                                secuencial = q.secuencial,
                                GrupoTipoCobro = q.GrupoTipoCobro,
                                NombreTipoCobro = q.NombreTipoCobro,
                                TotalPago = q.TotalPago
                             }).ToList();
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
