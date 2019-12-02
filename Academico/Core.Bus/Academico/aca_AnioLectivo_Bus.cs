using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Bus
    {
        aca_AnioLectivo_Data odata = new aca_AnioLectivo_Data();
        public List<aca_AnioLectivo_Info> GetList(int IdEmpresa, bool MostrarAnulados)
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

        public aca_AnioLectivo_Info GetInfo(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public aca_AnioLectivo_Info GetInfo_AnioAnterior(int IdEmpresa, int Anio)
        {
            try
            {
                return odata.getInfo_AnioAnterior(IdEmpresa, Anio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_AnioLectivo_Info GetInfo_AnioEnCurso(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.getInfo_AnioEnCurso(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_AnioLectivo_Info info)
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

        public bool ModificarDB(aca_AnioLectivo_Info info)
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

        public bool AnularDB(aca_AnioLectivo_Info info)
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

        public bool GuardarAperturaDB(aca_AnioLectivo_Info info)
        {
            try
            {
                return odata.guardarAperturaDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
