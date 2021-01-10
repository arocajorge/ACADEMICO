using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Admision_Bus
    {
        aca_Admision_Data odata = new aca_Admision_Data();

        public bool GuardarDB(aca_Admision_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Admision_Info GetInfo_CedulaAspirante(int IdEmpresa, string CedulaRuc_Aspirante)
        {
            try
            {
                return odata.getInfo_CedulaAspirante(IdEmpresa, CedulaRuc_Aspirante);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
