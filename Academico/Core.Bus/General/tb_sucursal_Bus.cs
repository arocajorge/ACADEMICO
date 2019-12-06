using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_sucursal_Bus
    {
        tb_sucursal_Data odata = new tb_sucursal_Data();
        public List<tb_sucursal_Info> GetList(int IdEmpresa, string IdUsuario, bool MostrarTodos)

        {
            try
            {
                return odata.GetList(IdEmpresa, IdUsuario, MostrarTodos);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<tb_sucursal_Info> get_list(int IdEmpresa, bool mostrar_anulados)
        {
            try
            {
                return odata.get_list(IdEmpresa, mostrar_anulados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
