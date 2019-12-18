using Core.Data.Caja;
using Core.Info.Caja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Caja
{
    public class cp_conciliacion_Caja_det_Ing_Caja_Bus
    {
        cp_conciliacion_Caja_det_Ing_Caja_Data odata = new cp_conciliacion_Caja_det_Ing_Caja_Data();

        public List<cp_conciliacion_Caja_det_Ing_Caja_Info> get_list(int IdEmpresa, decimal IdConciliacion_caja)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdConciliacion_caja);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cp_conciliacion_Caja_det_Ing_Caja_Info> get_list_ingresos_x_conciliar(int IdEmpresa, DateTime Fecha_fin, int IdCaja)
        {
            try
            {
                return odata.get_list_ingresos_x_conciliar(IdEmpresa, Fecha_fin, IdCaja);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
