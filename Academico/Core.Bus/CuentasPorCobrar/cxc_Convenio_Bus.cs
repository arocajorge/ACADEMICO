using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_Convenio_Bus
    {
        cxc_Convenio_Data odata = new cxc_Convenio_Data();
        public List<cxc_Convenio_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, fecha_ini, fecha_fin, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public cxc_Convenio_Info GetInfo(int IdEmpresa, int IdConvenio)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdConvenio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(cxc_Convenio_Info info)
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

        public bool ModificarDB(cxc_Convenio_Info info)
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

        public bool AnularDB(cxc_Convenio_Info info)
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
