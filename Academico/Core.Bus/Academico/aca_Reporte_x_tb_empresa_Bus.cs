using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Reporte_x_tb_empresa_Bus
    {
        aca_Reporte_x_tb_empresa_Data odata = new aca_Reporte_x_tb_empresa_Data();

        public aca_Reporte_x_tb_empresa_Info GetInfo(int IdEmpresa, string CodReporte)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, CodReporte);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Reporte_x_tb_empresa_Info info)
        {
            try
            {
                return odata.GuardarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
