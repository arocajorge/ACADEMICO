using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_004_Bus
    {
        ACA_004_Data odata = new ACA_004_Data();
        public List<ACA_004_Info> Getlist(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
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
