using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_032_Bus
    {
        ACA_032_Data odata = new ACA_032_Data();
        public List<ACA_032_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, string IdCatalogoParcialTipo, bool MostrarRetirados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcialTipo, MostrarRetirados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
