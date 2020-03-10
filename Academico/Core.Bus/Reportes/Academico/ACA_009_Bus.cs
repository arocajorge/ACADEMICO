using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
   public class ACA_009_Bus
    {
        ACA_009_Data odata = new ACA_009_Data();
        public List<ACA_009_Info> GetList(int IdEmpresa, int IdAnio, int IdAlumno, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio, IdAlumno, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
