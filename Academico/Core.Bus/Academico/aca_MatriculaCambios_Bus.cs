using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_MatriculaCambios_Bus
    {
        aca_MatriculaCambios_Data odata = new aca_MatriculaCambios_Data();
        public aca_MatriculaCambios_Info getInfo_UltimoCambioParalelo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getInfo_UltimoCambioParalelo(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MatriculaCambios_Info getInfo_UltimoCambioPlantilla(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                return odata.getInfo_UltimoCambioPlantilla(IdEmpresa, IdMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
