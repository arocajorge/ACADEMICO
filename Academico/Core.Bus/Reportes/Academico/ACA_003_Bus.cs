using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_003_Bus
    {
        ACA_003_Data odata = new ACA_003_Data();
        public List<ACA_003_Info> GetList(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ACA_003_Info GetInfo(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.get_info(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ACA_003_Info GetInfo(int IdEmpresa, int IdAnio,  decimal IdAlumno)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
