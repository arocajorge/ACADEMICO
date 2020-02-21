using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaCondicional_Det_Data
    {
        public List<aca_MatriculaCondicional_Det_Info> getList(int IdEmpresa, decimal IdCondicional)
        {
            try
            {
                List<aca_MatriculaCondicional_Det_Info> Lista = new List<aca_MatriculaCondicional_Det_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCondicional_Det.Where(q => q.IdEmpresa == IdEmpresa && q.IdCondicional == IdCondicional).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCondicional_Det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdCondicional = q.IdCondicional,
                            Secuencia=q.Secuencia,
                            IdParrafo = q.IdParrafo,
                            Nombre = q.Nombre,
                            NomCatalogo = q.NomCatalogo
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
