using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Rubro_Bus
    {
        aca_AnioLectivo_Rubro_Data odata = new aca_AnioLectivo_Rubro_Data();

        public List<aca_AnioLectivo_Rubro_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
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

        public aca_AnioLectivo_Rubro_Info GetInfo(int IdEmpresa, int IdAnio, int IdRubro)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_AnioLectivo_Rubro_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_AnioLectivo_Rubro_Info info)
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
    }
}
