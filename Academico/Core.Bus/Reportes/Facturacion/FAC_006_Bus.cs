using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_006_Bus
    {
        FAC_006_Data odata = new FAC_006_Data();
        public List<FAC_006_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdAlumno, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdAlumno, fecha_ini, fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
