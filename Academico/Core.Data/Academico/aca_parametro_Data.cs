using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Parametro_Data
    {
        public aca_Parametro_Info getInfo(int IdEmpresa)
        {
            try
            {
                aca_Parametro_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Parametro.Where(q => q.IdEmpresa == IdEmpresa).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Parametro_Info
                    {
                        PromedioMinimoParcial = Entity.PromedioMinimoParcial,
                        PromedioMinimoPromocion = Entity.PromedioMinimoPromocion
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
