using Core.Data.Reportes.Contabilidad;
using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_019_Bus
    {
        CXC_019_Data odata = new CXC_019_Data();
        public List<CXC_019_Info> get_list(int IdEmpresa, decimal IdAlumno, DateTime fecha_ini, DateTime fecha_fin, bool mostrarAnulados)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAlumno, fecha_ini, fecha_fin, mostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
