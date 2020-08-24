using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_016_Bus
    {
        ACA_016_Data odata = new ACA_016_Data();
        public List<ACA_016_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial)
        {
            try
            {
                return odata.get_list(IdEmpresa,IdAnio,IdSede,IdNivel,IdJornada,IdCurso,IdParalelo,IdCatalogoParcial);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
