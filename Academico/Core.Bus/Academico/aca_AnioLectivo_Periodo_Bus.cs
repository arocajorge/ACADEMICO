using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Periodo_Bus
    {
        aca_AnioLectivo_Periodo_Data odata = new aca_AnioLectivo_Periodo_Data();
        public List<aca_AnioLectivo_Periodo_Info> GetList(int IdEmpresa)
        {
            try
            {
                return odata.getList(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_AnioLectivo_Periodo_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
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

        public bool ModificarDB(List<aca_AnioLectivo_Periodo_Info> info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AnularDB(aca_AnioLectivo_Periodo_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
