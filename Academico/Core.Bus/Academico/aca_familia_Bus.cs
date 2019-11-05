using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_familia_Bus
    {
        aca_familia_Data odata = new aca_familia_Data();
        public List<aca_familia_Info> GetList(int IdEmpresa, int IdAlumno)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_familia_Info GetListTipo(int IdEmpresa, int IdAlumno, int IdCatalogoPAREN)
        {
            try
            {
                return odata.getListTipo(IdEmpresa, IdAlumno, IdCatalogoPAREN);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_familia_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                return odata.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
