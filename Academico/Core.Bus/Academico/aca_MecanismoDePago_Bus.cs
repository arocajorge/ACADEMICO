using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MecanismoDePago_Bus
    {
        aca_MecanismoDePago_Data odata = new aca_MecanismoDePago_Data();
        public List<aca_MecanismoDePago_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public aca_MecanismoDePago_Info GetInfo(int IdEmpresa, decimal IdMecanismo)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdMecanismo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MecanismoDePago_Info GetInfo_ByTermino(int IdEmpresa, string IdTerminoPago)
        {
            try
            {
                return odata.getInfo_ByTermino(IdEmpresa, IdTerminoPago);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_MecanismoDePago_Info info)
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

        public bool ModificarDB(aca_MecanismoDePago_Info info)
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

        public bool AnularDB(aca_MecanismoDePago_Info info)
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
