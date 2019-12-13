using Core.Data.Banco;
using Core.Info.Banco;
using System;

namespace Core.Bus.Banco
{
    public class ba_parametros_Bus
    {
        ba_parametros_Data odata = new ba_parametros_Data();
    
        public ba_parametros_Info get_info(int IdEmpresa)
        {
            try
            {
                return odata.get_info(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(ba_parametros_Info info)
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
    }
}
