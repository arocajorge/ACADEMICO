using Core.Data.Base;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Facturacion
{
   public class FAC_003_aplicaciones_Data
    {
        public List<FAC_003_aplicaciones_Info> get_list(int IdEmpresa_nt, int IdSucursal_nt, int IdBodega_nt, decimal IdNota_nt)
        {
            try
            {
                List<FAC_003_aplicaciones_Info> Lista = new List<FAC_003_aplicaciones_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = (from q in Context.VWFAC_003_aplicaciones
                             where q.IdEmpresa_nt == IdEmpresa_nt
                             && q.IdSucursal_nt == IdSucursal_nt
                             && q.IdBodega_nt == IdBodega_nt
                             && q.IdNota_nt == IdNota_nt
                             select new FAC_003_aplicaciones_Info
                             {
                                 IdEmpresa_nt = q.IdEmpresa_nt,
                                 IdSucursal_nt = q.IdSucursal_nt,
                                 IdBodega_nt = q.IdBodega_nt,
                                 IdNota_nt = q.IdNota_nt,
                                 secuencia = q.secuencia,
                                 IdCbteVta_fac_nd_doc_mod = q.IdCbteVta_fac_nd_doc_mod,
                                 NumFactura = q.NumFactura,
                                 Valor_Aplicado = q.Valor_Aplicado,
                                 vt_fecha = q.vt_fecha
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
