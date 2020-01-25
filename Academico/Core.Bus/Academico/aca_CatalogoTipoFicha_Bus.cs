using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_CatalogoTipoFicha_Bus
    {
        aca_CatalogoTipoFicha_Data odata = new aca_CatalogoTipoFicha_Data();

        public List<aca_CatalogoTipoFicha_Info> get_list()
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

        public aca_CatalogoTipoFicha_Info get_info(int IdTipoCatalogoFicha)
        {
            try
            {
                return odata.get_info(IdTipoCatalogoFicha);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool guardarDB(aca_CatalogoTipoFicha_Info info)
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



        public bool modificarDB(aca_CatalogoTipoFicha_Info info)
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
