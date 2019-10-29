using Core.Data.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_persona_Bus
    {
        tb_persona_Data odata = new tb_persona_Data();
        public decimal validar_existe_cedula(string pe_CedulaRuc)
        {
            try
            {
                return odata.validar_existe_cedula(pe_CedulaRuc);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
