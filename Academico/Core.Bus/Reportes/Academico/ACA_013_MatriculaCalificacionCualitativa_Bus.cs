using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_013_MatriculaCalificacionCualitativa_Bus
    {
        ACA_013_MatriculaCalificacionCualitativa_Data odata = new ACA_013_MatriculaCalificacionCualitativa_Data();
        public List<ACA_013_MatriculaCalificacionCualitativa_Info> GetList(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdMatricula, IdCatalogoParcial);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
