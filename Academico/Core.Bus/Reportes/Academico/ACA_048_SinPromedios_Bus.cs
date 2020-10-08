using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_048_SinPromedios_Bus
    {
        ACA_048_SinPromedios_Data odata = new ACA_048_SinPromedios_Data();
        public List<ACA_048_SinPromedios_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcialTipo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcialTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
