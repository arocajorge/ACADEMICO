using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Familia_Bus
    {
        aca_Familia_Data odata = new aca_Familia_Data();
        public List<aca_Familia_Info> GetList(int IdEmpresa, int IdAlumno)
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

        public aca_Familia_Info GetListTipo(int IdEmpresa, int IdAlumno, int IdCatalogoPAREN)
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

        public aca_Familia_Info GetInfo(int IdEmpresa, int IdAlumno, int Secuencia)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAlumno, Secuencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Familia_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
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

        public bool guardarDB(aca_Familia_Info info)
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

        public bool modificarDB(aca_Familia_Info info)
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

        public bool eliminarDB(aca_Familia_Info info)
        {
            try
            {
                return odata.eliminarDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
