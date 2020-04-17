using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_012_Bus
    {
        ACA_012_Data odata = new ACA_012_Data();

        public List<ACA_012_Info> GetList(int IdEmpresa, int IdAnio, DateTime FechaIni, DateTime FechaFin, decimal IdRubro)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio, FechaIni, FechaFin, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
