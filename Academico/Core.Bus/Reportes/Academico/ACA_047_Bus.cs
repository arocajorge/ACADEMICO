using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_047_Bus
    {
        ACA_047_Data odata = new ACA_047_Data();
        public List<ACA_047_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcialTipo)
        {
            try
            {
                return odata.get_list(IdEmpresa,IdAnio,IdSede,IdNivel,IdJornada,IdCurso,IdParalelo, IdMateria, IdCatalogoParcialTipo);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
