using Core.Data.Base;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_mes_Data
    {
        public List<tb_mes_Info> get_list()
        {
            try
            {
                List<tb_mes_Info> Lista;
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    Lista = (from q in Context.tb_mes
                             select new tb_mes_Info
                             {
                                 idMes = q.idMes,
                                 smes = q.smes,
                                 Nemonico = q.Nemonico,
                                 smesIngles = q.smesIngles
                             }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
