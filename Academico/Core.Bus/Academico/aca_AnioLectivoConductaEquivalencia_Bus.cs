using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivoConductaEquivalencia_Bus
    {
        aca_AnioLectivoConductaEquivalencia_Data odata = new aca_AnioLectivoConductaEquivalencia_Data();
        public List<aca_AnioLectivoConductaEquivalencia_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
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

        public aca_AnioLectivoConductaEquivalencia_Info GetInfo(int IdEmpresa, int IdAnio, int Secuencia)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, Secuencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_AnioLectivoConductaEquivalencia_Info info)
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

        public bool ModificarDB(aca_AnioLectivoConductaEquivalencia_Info info)
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
