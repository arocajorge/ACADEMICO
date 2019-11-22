using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_PermisoMatricula_Bus
    {
        aca_PermisoMatricula_Data odata = new aca_PermisoMatricula_Data();

        public List<aca_PermisoMatricula_Info> GetList(int IdEmpresa, int IdAnio, int IdCatalogoPERNEG, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdCatalogoPERNEG, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_PermisoMatricula_Info> GetList_Validacion(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoPERNEG, decimal IdPermiso)
        {
            try
            {
                return odata.getList_Validacion(IdEmpresa, IdAnio, IdAlumno, IdCatalogoPERNEG, IdPermiso);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_PermisoMatricula_Info GetInfo(int IdEmpresa, int IdPermiso)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdPermiso);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_PermisoMatricula_Info GetInfo_ByMatricula(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoPERNEG)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdAlumno, IdCatalogoPERNEG);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GuardarDB(aca_PermisoMatricula_Info info)
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

        public bool ModificarDB(aca_PermisoMatricula_Info info)
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

        public bool AnularDB(aca_PermisoMatricula_Info info)
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
