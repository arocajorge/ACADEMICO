using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_CampoAccion_Bus
    {
        aca_CampoAccion_Data odata = new aca_CampoAccion_Data();
        public List<aca_CampoAccion_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_CampoAccion_Info GetInfo(int IdEmpresa, int IdTematica)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdTematica);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetOrden(int IdEmpresa)
        {
            try
            {
                return odata.getOrden(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_CampoAccion_Info info)
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

        public bool ModificarDB(aca_CampoAccion_Info info)
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

        public bool AnularDB(aca_CampoAccion_Info info)
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
