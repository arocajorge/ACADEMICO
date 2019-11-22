using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Matricula_Rubro_Bus
    {
        aca_Matricula_Rubro_Data odata = new aca_Matricula_Rubro_Data();

        public List<aca_Matricula_Rubro_Info> GetList_Matricula(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                return odata.getListMatricula(IdEmpresa, IdAnio, IdPlantilla);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Matricula_Rubro_Info> GetList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getList(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
