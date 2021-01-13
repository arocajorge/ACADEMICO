using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_014_Bus
    {
        ACA_014_Data odata = new ACA_014_Data();
        public List<ACA_014_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial, decimal IdAlumno, bool MostrarRetirados, bool MostrarPromedios)
        {
            try
            {
                return odata.get_list(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial, IdAlumno, MostrarRetirados, MostrarPromedios);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ACA_014_EquivalenciaCualitativa_Info> get_list_EquivalenciaCualitativa(int IdEmpresa, int IdAnio)
        {
            try
            {
                return odata.get_list_EquivalenciaCualitativa(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ACA_014_EquivalenciaConducta_Info> get_list_EquivalenciaConducta(int IdEmpresa, int IdAnio)
        {
            try
            {

                return odata.get_list_EquivalenciaConducta(IdEmpresa, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
