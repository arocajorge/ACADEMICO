using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Sede_NivelAcademico_Bus
    {
        aca_AnioLectivo_Sede_NivelAcademico_Data odata = new aca_AnioLectivo_Sede_NivelAcademico_Data();

        public List<aca_AnioLectivo_Sede_NivelAcademico_Info> GetListAsignacion(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                return odata.get_list_asignacion(IdEmpresa, IdSede, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<aca_AnioLectivo_Sede_NivelAcademico_Info> GetListNivel_x_Anio(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.GetListNivel_x_Anio(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_AnioLectivo_Sede_NivelAcademico_Info GetInfo(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdSede, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(int IdEmpresa, int IdSede, int IdAnio, List<aca_AnioLectivo_Sede_NivelAcademico_Info> lista)
        {
            try
            {
                return odata.guardarDB(IdEmpresa, IdSede, IdAnio, lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
