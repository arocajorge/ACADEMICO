using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_001_Bus
    {
        ACA_001_Data odata = new ACA_001_Data();
        public List<ACA_001_Info> GetList(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAlumno, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ACA_001_Info> GetListPadres(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.get_list_padres(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
