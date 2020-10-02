using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_040_Bus
    {
        ACA_040_Data odata = new ACA_040_Data();
        public List<ACA_040_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, int IdCatalogoParcialTipo, int IdCatalogoParcial, bool MostrarRetirados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, IdCatalogoParcialTipo, IdCatalogoParcial, MostrarRetirados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
