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
        public List<aca_MatriculaCondicional_Det_Info> getList(int IdEmpresa, decimal IdMatriculaCondicional)
        {
            try
            {
                List<aca_MatriculaCondicional_Det_Info> Lista = new List<aca_MatriculaCondicional_Det_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCondicional_Det.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatriculaCondicional == IdMatriculaCondicional).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCondicional_Det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatriculaCondicional = q.IdMatriculaCondicional,
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

        public List<aca_MatriculaCondicional_Det_Info> getList_x_Tipo(int IdEmpresa, int IdCatalogoCONDIC)
        {
            try
            {
                List<aca_MatriculaCondicional_Det_Info> Lista = new List<aca_MatriculaCondicional_Det_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCondicionalParrafo.Where(q => q.IdEmpresa == IdEmpresa && q.IdCatalogoCONDIC == IdCatalogoCONDIC).ToList();
                    var Secuencia = 1;
                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCondicional_Det_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            Secuencia = Secuencia++,
                            IdParrafo = q.IdParrafo,
                            Nombre = q.Nombre
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
