using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_030_Bus
    {
        ACA_030_Data odata = new ACA_030_Data();
        public List<ACA_030_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcialTipo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcialTipo, IdAlumno, MostrarRetirados);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
