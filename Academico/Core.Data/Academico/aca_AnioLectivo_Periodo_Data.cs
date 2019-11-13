using Core.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivo_Periodo_Data
    {
        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AnioLectivo_Periodo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPeriodo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
