using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_028_PromedioXSexo_Bus
    {
        ACA_028_PromedioXSexo_Data odata = new ACA_028_PromedioXSexo_Data();
        public List<ACA_028_PromedioXSexo_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoTipo)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoTipo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
