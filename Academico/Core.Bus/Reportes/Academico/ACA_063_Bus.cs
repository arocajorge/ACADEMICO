using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_063_Bus
    {
        ACA_063_Data odata = new ACA_063_Data();
        public ACA_063_Info GetInfo(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdAlumno, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
