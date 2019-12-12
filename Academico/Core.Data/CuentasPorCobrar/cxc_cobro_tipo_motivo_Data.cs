using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_cobro_tipo_motivo_Data
    {
        public List<cxc_cobro_tipo_motivo_Info> get_list()
        {
            try
            {
                List<cxc_cobro_tipo_motivo_Info> Lista;
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    Lista = (from q in Context.cxc_cobro_tipo_motivo
                             select new cxc_cobro_tipo_motivo_Info
                             {
                                 IdMotivo_tipo_cobro = q.IdMotivo_tipo_cobro,
                                 nom_Motivo_tipo_cobro = q.nom_Motivo_tipo_cobro
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
