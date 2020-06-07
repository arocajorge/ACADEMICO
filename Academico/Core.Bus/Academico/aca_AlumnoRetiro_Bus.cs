using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AlumnoRetiro_Bus
    {
        aca_AlumnoRetiro_Data odata = new aca_AlumnoRetiro_Data();
        public List<aca_AlumnoRetiro_Info> GetList(int IdEmpresa)
        {
            try
            {
                return odata.getList(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_AlumnoRetiro_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_AlumnoRetiro_Info GetList(int IdEmpresa, decimal IdAlumno)
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

        public aca_AlumnoRetiro_Info GetInfo(int IdEmpresa, int IdRetiro)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdRetiro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_AlumnoRetiro_Info info)
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

        public bool AnularDB(aca_AlumnoRetiro_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
