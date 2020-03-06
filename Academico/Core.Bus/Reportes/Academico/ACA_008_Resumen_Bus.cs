using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
   public class ACA_008_Resumen_Bus
    {
        ACA_008_Resumen_Data odata = new ACA_008_Resumen_Data();
        public List<ACA_008_Resumen_Info> GetList(int IdEmpresa, int IdSede, int IdAnio)
        {
            return odata.GetList(IdEmpresa, IdSede, IdAnio);
        }
    }
}
