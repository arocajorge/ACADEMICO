using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoEquivalenciaPromedio_Data
    {
        public List<aca_AnioLectivoEquivalenciaPromedio_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivoEquivalenciaPromedio_Info> Lista = new List<aca_AnioLectivoEquivalenciaPromedio_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivoEquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoEquivalenciaPromedio_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdEquivalenciaPromedio = q.IdEquivalenciaPromedio,
                            Descripcion = q.Descripcion,
                            Codigo = q.Codigo,
                            ValorMinimo = q.ValorMinimo,
                            ValorMaximo = q.ValorMaximo
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
