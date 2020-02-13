using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_006__Bus
    {
        ACA_006_Data odata = new ACA_006_Data();
        public List<ACA_006_Info> Getlist(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                return odata.Getlist(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso);
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
