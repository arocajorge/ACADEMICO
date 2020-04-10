using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_007_Bus
    {
        FAC_007_Data odata = new FAC_007_Data();

        public List<FAC_007_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin, int IdEmpresa_rol)
        {
            try
            {
                return odata.GetList(IdEmpresa, FechaIni, FechaFin, IdEmpresa_rol);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
