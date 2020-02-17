using Core.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaCambios_Data
    {
        public int getSecuenciaByMatricula(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                int Secuencia = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula==IdMatricula).Count();
                    if (cont > 0)
                        Secuencia = Context.aca_MatriculaCambios.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.Secuencia) + 1;
                }

                return Secuencia;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
