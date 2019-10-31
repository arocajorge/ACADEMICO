using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_CatalogoTipo_Bus
    {
        aca_CatalogoTipo_Data odata = new aca_CatalogoTipo_Data();
        public List<aca_CatalogoTipo_Info> GetList(bool MostrarAnulados)
        {
            try
            {
                return odata.getList( MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_CatalogoTipo_Info GetInfo(int IdCatalogoTipo)
        {
            try
            {
                return odata.getInfo(IdCatalogoTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_CatalogoTipo_Info info)
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

        public bool ModificarDB(aca_CatalogoTipo_Info info)
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

        public bool AnularDB(aca_CatalogoTipo_Info info)
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
