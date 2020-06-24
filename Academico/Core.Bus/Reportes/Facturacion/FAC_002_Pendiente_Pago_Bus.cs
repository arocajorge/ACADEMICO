using Core.Data.Reportes.Facturacion;
using Core.Info.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Facturacion
{
    public class FAC_002_Pendiente_Pago_Bus
    {
        FAC_002_Pendiente_Pago_Data odata = new FAC_002_Pendiente_Pago_Data();
        public List<FAC_002_Pendiente_Pago_Info> get_list(int IdEmpresa, int IdSucursal, decimal IdAlumno)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
