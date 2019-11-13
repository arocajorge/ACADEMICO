using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Rubro_Periodo_Bus
    {
        aca_AnioLectivo_Rubro_Periodo_Data odata = new aca_AnioLectivo_Rubro_Periodo_Data();
        public List<aca_AnioLectivo_Rubro_Periodo_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivo_Rubro_Periodo_Info> GetListAsignacion(int IdEmpresa, int IdAnio, int IdRubro)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdAnio, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
