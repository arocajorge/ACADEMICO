using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_ColaCorreoCodigo_Bus
    {
        tb_ColaCorreoCodigo_Data odata = new tb_ColaCorreoCodigo_Data();

        public List<tb_ColaCorreoCodigo_Info> GetList(int IdEmpresa)
        {
            try
            {
                return odata.GetList(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_ColaCorreoCodigo_Info GetInfo(int IdEmpresa, string Codigo)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, Codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
