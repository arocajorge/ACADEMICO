using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaConducta_Data
    {
        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            PromedioQ1 = q.PromedioQ1,
                            PromedioQ2 = q.PromedioQ2,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            PromedioFinal = q.PromedioFinal,
                            PromedioGeneral = q.PromedioGeneral
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
