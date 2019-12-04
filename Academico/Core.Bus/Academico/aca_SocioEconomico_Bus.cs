using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_SocioEconomico_Bus
    {
        aca_SocioEconomico_Data odata = new aca_SocioEconomico_Data();
        //public List<aca_SocioEconomico_Info> GetList(int IdEmpresa, int IdAnio, decimal IdAlumno, bool MostrarAnulados)
        //{
        //    try
        //    {
        //        return odata.getList(IdEmpresa, IdAnio, IdAlumno, MostrarAnulados);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public aca_SocioEconomico_Info GetInfo(int IdEmpresa, int IdSocioEconomico)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdSocioEconomico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_SocioEconomico_Info GetInfo_by_Alumno(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.getInfo_by_Alumno(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool GuardarDB(aca_SocioEconomico_Info info)
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

        public bool ModificarDB(aca_SocioEconomico_Info info)
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
