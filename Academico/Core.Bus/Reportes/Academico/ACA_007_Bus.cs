using Core.Data.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.Academico
{
    public class ACA_007_Bus
    {
        ACA_007_Data odata = new ACA_007_Data();
        public List<ACA_007_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                return odata.get_list(IdEmpresa, fecha_ini, fecha_fin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ACA_007_Info> Getlist(int idEmpresa, int idAnio, int idSede, int idNivel, int idJornada, int idCurso)
        {
            throw new NotImplementedException();
        }
    }
}
