using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
   public class FAC_005_Bus
    {
        FAC_005_Data odata = new FAC_005_Data();
        public List<FAC_005_Info> GetList(int IdEmpresa, DateTime FechaDesde, DateTime FechaHasta, int IdTipoNota, string CreDeb, string NaturalezaNota)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaDesde, FechaHasta, IdTipoNota, CreDeb, NaturalezaNota);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
