using Core.Data.General;
using Core.Info.General;
using System;

namespace Core.Bus.General
{
    public class tb_ColaImpresionDirecta_Bus
    {
        tb_ColaImpresionDirecta_Data odata = new tb_ColaImpresionDirecta_Data();

        public bool GuardarDB(tb_ColaImpresionDirecta_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ModificarDB(tb_ColaImpresionDirecta_Info info)
        {
            try
            {
                return odata.ModificarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public tb_ColaImpresionDirecta_Info GetInfoPorImprimir(string IPUsuario)
        {
            try
            {
                return odata.GetInfoPorImprimir(IPUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
