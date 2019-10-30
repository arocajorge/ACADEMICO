using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Jornada_Bus
    {
        aca_Jornada_Data odata = new aca_Jornada_Data();
        public List<aca_Jornada_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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
        public List<aca_Jornada_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, IdNivel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Jornada_Info GetInfo(int IdEmpresa, int IdJornada)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdJornada);
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

        public bool GuardarDB(aca_Jornada_Info info)
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

        public bool ModificarDB(aca_Jornada_Info info)
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

        public bool AnularDB(aca_Jornada_Info info)
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
