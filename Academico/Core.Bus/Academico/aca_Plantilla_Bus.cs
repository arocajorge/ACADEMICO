using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Plantilla_Bus
    {
        aca_Plantilla_Data odata = new aca_Plantilla_Data();

        public List<aca_Plantilla_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
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

        public aca_Plantilla_Info GetInfo(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdPlantilla);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_Plantilla_Info info)
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

        public bool ModificarDB(aca_Plantilla_Info info)
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

        public bool AnularDB(aca_Plantilla_Info info)
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
