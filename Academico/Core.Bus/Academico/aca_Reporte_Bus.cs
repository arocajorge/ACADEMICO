using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Reporte_Bus
    {
        aca_Reporte_Data odata = new aca_Reporte_Data();

        public List<aca_Reporte_Info> get_list()
        {
            try
            {
                return odata.get_list();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Reporte_Info get_info(string CodReporte)
        {
            try
            {
                return odata.get_info(CodReporte);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool validar_existe_CodReporte(string CodReporte)
        {
            try
            {
                return odata.validar_existe_CodReporte(CodReporte);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string get_id(string CodModulo)
        {
            try
            {
                return odata.get_id(CodModulo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Reporte_Info info)
        {
            try
            {
                return odata.guardarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Reporte_Info info)
        {
            try
            {
                return odata.modificarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
