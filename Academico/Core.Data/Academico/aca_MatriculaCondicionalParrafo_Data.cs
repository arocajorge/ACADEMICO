using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;

namespace Core.Data.Academico
{
    public class aca_MatriculaCondicionalParrafo_Data
    {
      public List<aca_MatriculaCondicionalParrafo_Info> GetList()
        {
            try
            {
                List<aca_MatriculaCondicionalParrafo_Info> Lista = new List<aca_MatriculaCondicionalParrafo_Info>();
                using (EntitiesAcademico db = new EntitiesAcademico())

                return Lista;
            }
            
            catch (Exception)
            {

                throw;
            }
        }
     
	
	}
    
}
