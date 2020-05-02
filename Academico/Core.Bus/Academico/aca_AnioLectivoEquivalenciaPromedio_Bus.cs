using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivoEquivalenciaPromedio_Bus
    {
        aca_AnioLectivoEquivalenciaPromedio_Data odata = new aca_AnioLectivoEquivalenciaPromedio_Data();
        public List<aca_AnioLectivoEquivalenciaPromedio_Info> GetList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
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

        public aca_AnioLectivoEquivalenciaPromedio_Info GetInfo(int IdEmpresa, int IdAnio, int IdEquivalenciaPromedio)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdEquivalenciaPromedio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_AnioLectivoEquivalenciaPromedio_Info GetInfo_x_Promedio(int IdEmpresa, int IdAnio, decimal PromedioFinal)
        {
            try
            {
                return odata.getInfo_x_Promedio(IdEmpresa, IdAnio, PromedioFinal);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_AnioLectivoEquivalenciaPromedio_Info info)
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

        public bool ModificarDB(aca_AnioLectivoEquivalenciaPromedio_Info info)
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

        public bool AnularDB(aca_AnioLectivoEquivalenciaPromedio_Info info)
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
