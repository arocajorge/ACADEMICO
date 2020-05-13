using Core.Bus.General;
using Core.Data.General;
using Core.Data.RRHH;
using Core.Info.RRHH;
using System;
using System.Collections.Generic;

namespace Core.Bus.RRHH
{
    public class ro_empleado_Bus
    {
        ro_empleado_Data odata = new ro_empleado_Data();
        tb_persona_Data odata_per = new tb_persona_Data();

        tb_persona_Bus bus_persona = new tb_persona_Bus();
        public List<ro_empleado_Info> get_list_combo(int IdEmpresa)
        {
            try
            {
                return odata.get_list_combo(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ro_empleado_Info> get_list(int IdEmpresa, int IdSucursal, string em_status, bool estado)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdSucursal, em_status, estado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_empleado_Info get_info(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdEmpleado);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
