using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_002_Bus
    {
        FAC_002_Data odata = new FAC_002_Data();
        public List<FAC_002_Info> get_list(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdCbteVta, int IdAnio, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo, DateTime FechaIni, DateTime FechaFin)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdBodega, IdCbteVta, IdAnio, IdSede, IdJornada, IdNivel, IdCurso, IdParalelo, FechaIni, FechaFin);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
