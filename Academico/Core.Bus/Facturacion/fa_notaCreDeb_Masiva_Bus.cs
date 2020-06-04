using Core.Data.Facturacion;
using Core.Info.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Facturacion
{
    public class fa_notaCreDeb_Masiva_Bus
    {
        fa_notaCreDeb_Masiva_Data odata = new fa_notaCreDeb_Masiva_Data();
        public List<fa_notaCreDeb_Masiva_Info> Get_list(int IdEmpresa, int IdSucursal, DateTime Fecha_ini, DateTime Fecha_fin, bool MostrarAnulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, Fecha_ini, Fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
