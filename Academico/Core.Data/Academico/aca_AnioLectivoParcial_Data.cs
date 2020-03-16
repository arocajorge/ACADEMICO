using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoParcial_Data
    {
        public List<aca_AnioLectivoParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdAnio = q.IdAnio,
                            Parcial = q.Parcial,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin
                        });
                    });
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
