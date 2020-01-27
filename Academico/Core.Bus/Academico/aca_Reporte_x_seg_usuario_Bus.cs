using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Reporte_x_seg_usuario_Bus
    {
        aca_Reporte_x_seg_usuario_Data odata = new aca_Reporte_x_seg_usuario_Data();

        public List<aca_Reporte_x_seg_usuario_Info> get_list(int IdEmpresa, string IdUsuario, bool MostrarNoAsignados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdUsuario, MostrarNoAsignados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.eliminarDB(IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<aca_Reporte_x_seg_usuario_Info> Lista, int IdEmpresa, string IdUsuario)
        {
            try
            {
                return odata.guardarDB(Lista, IdEmpresa, IdUsuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
