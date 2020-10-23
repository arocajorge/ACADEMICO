using Core.Data.CuentasPorCobrar;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.CuentasPorCobrar
{
    public class cxc_Pagare_Bus
    {
        cxc_Pagare_Data odata = new cxc_Pagare_Data();
        public List<cxc_Pagare_Info> GetList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
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

        public cxc_Pagare_Info GetInfo(int IdEmpresa, int IdPagare)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdPagare);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(cxc_Pagare_Info info)
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

        public bool ModificarDB(cxc_Pagare_Info info)
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

        public bool AnularDB(cxc_Pagare_Info info)
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
