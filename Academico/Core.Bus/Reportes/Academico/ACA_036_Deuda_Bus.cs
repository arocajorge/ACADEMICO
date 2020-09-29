using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_036_Deuda_Bus
    {
        ACA_036_Deuda_Data odata = new ACA_036_Deuda_Data();
        public List<ACA_036_Deuda_Info> get_list(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
