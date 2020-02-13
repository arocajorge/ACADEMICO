using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_CondicionalMatricula_Bus
    {
        aca_CondicionalMatricula_Data odata = new aca_CondicionalMatricula_Data();

        public List<aca_CondicionalMatricula_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
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

        public List<aca_CondicionalMatricula_Info> GetList_Matricula(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                return odata.getList_Matricula(IdEmpresa, IdAnio, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_CondicionalMatricula_Info> GetList_ExisteCondicional(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoCONDIC)
        {
            try
            {
                return odata.getList_ExisteCondicional(IdEmpresa, IdAnio, IdAlumno, IdCatalogoCONDIC);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_CondicionalMatricula_Info GetInfo(int IdEmpresa, int IdCondicional)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdCondicional);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_CondicionalMatricula_Info info)
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

        public bool ModificarDB(aca_CondicionalMatricula_Info info)
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

        public bool AnularDB(aca_CondicionalMatricula_Info info)
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
