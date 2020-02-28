using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_006_Bus
    {
        ACA_006_Data odata = new ACA_006_Data();
        public List<ACA_006_Info> Getlist(int IdEmpresa, int IdSede, int IdAnio, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.Getlist(IdEmpresa, IdSede, IdAnio, fecha_ini, fecha_fin);
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
