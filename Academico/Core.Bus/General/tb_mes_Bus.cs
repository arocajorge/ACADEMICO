using Core.Data.General;
using Core.Info.General;
using System.Collections.Generic;

namespace Core.Bus.General
{
    public class tb_mes_Bus
    {
        tb_mes_Data odata = new tb_mes_Data();
        public List<tb_mes_Info> get_list()
        {
            return odata.get_list();
        }
    }
}
