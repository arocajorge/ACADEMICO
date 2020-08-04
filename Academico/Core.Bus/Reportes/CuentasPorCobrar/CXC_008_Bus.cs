using Core.Data.Reportes.CuentasPorCobrar;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Reportes.CuentasPorCobrar
{
    public class CXC_008_Bus
    {
        CXC_008_Data odata = new CXC_008_Data();
        public List<CXC_008_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, DateTime FechaCorte, int CantMin, int CantMax)
        {
            try
            {
                return odata.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, FechaCorte, CantMin, CantMax);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
