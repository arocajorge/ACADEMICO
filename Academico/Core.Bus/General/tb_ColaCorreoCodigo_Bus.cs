using Core.Data.General;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.General
{
    public class tb_ColaCorreoCodigo_Bus
    {
        tb_ColaCorreoCodigo_Data odata = new tb_ColaCorreoCodigo_Data();

        public List<tb_ColaCorreoCodigo_Info> GetList(int IdEmpresa)
        {
            try
            {
                return odata.GetList(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_ColaCorreoCodigo_Info> GetList_Seguimiento(int IdEmpresa)
        {
            try
            {
                return odata.GetList_Seguimiento(IdEmpresa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_ColaCorreoCodigo_Info GetInfo(int IdEmpresa, string Codigo)
        {
            try
            {
                return odata.GetInfo(IdEmpresa, Codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Existe_codigo(int IdEmpresa, string Codigo)
        {
            try
            {
                return odata.Existe_codigo(IdEmpresa, Codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(tb_ColaCorreoCodigo_Info info)
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

        public bool modificarDB(tb_ColaCorreoCodigo_Info info)
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
