

using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;

namespace Core.Bus.Academico
{
    public class aca_CatalogoFicha_Bus
    {
        aca_CatalogoFicha_Data odata = new aca_CatalogoFicha_Data();


        public List<aca_CatalogoFicha_Info> GetList(bool MostrarAnulados)
        {
            try
            {
                return odata.getList(MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_CatalogoFicha_Info> GetList_x_Tipo(int IdCatalogoTipoFicha, bool MostrarAnulados)
        {
            try
            {
                return odata.getList_x_Tipo(IdCatalogoTipoFicha, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_CatalogoFicha_Info GetInfo(int IdCatalogoFicha)
        {
            try
            {
                return odata.getInfo(IdCatalogoFicha);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetOrden(int IdCatalogoTipoFicha)
        {
            try
            {
                return odata.getOrden(IdCatalogoTipoFicha);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool validar_existe_CodCatalogo(string Codigo)
        {
            try
            {
                return odata.validar_existe_CodCatalogo(Codigo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GuardarDB(aca_CatalogoFicha_Info info)
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

        public bool ModificarDB(aca_CatalogoFicha_Info info)
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

        public bool AnularDB(aca_CatalogoFicha_Info info)
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
