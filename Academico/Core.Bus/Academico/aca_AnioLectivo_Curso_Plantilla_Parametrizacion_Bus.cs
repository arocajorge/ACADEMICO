using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Bus
    {
        aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Data odata = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Data();

        public List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info> GetList(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
