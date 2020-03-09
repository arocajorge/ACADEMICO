using Core.Data.Base;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Facturacion
{
   public class FAC_005_Data
    {
      public List< FAC_005_Info> GetList(int IdEmpresa, DateTime FechaDesde, DateTime FechaHasta, int IdTipoNota, string CreDeb, string NaturalezaNota)
        {
            try
            {
                int IdTipoNotaini = IdTipoNota;
                int IdTipoNotafin = IdTipoNota == 0 ? 99999999 : IdTipoNota;
                
                FechaDesde = FechaDesde.Date;
                FechaHasta = FechaHasta.Date;

                List<FAC_005_Info> Lista = new List<FAC_005_Info>();
                using (EntitiesReportes db = new EntitiesReportes())
                {
                    Lista = (from q in db.SPFAC_005(IdEmpresa, FechaDesde, FechaHasta, CreDeb, NaturalezaNota)
                             select new FAC_005_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdSucursal = q.IdSucursal,
                                 IdBodega = q.IdBodega,
                                 IdNota = q.IdNota,
                                 no_fecha = q.no_fecha,
                                 CreDeb = q.CreDeb,
                                 IdTipoNota = q.IdTipoNota,
                                 NaturalezaNota = q.NaturalezaNota,
                                 Estado = q.Estado,
                                 NumeroNota = q.NumeroNota,
                                 SubtotalConDscto = q.SubtotalConDscto,
                                 ValorIVA = q.ValorIVA,
                                 Total = q.Total,
                                 Valor_Aplicado = q.Valor_Aplicado,
                                 Saldo = q.Saldo,
                                 NombreCliente = q.NombreCliente,
                                 NombreAlumno = q.NombreAlumno,
                                 IdTipoNota1 = q.IdTipoNota1,
                                 No_Descripcion = q.No_Descripcion
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
