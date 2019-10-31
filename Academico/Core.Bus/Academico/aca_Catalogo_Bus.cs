using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Catalogo_Bus
    {
        aca_Catalogo_Data odata = new aca_Catalogo_Data();
        public List<aca_Catalogo_Info> GetList(bool MostrarAnulados)
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

        public aca_Catalogo_Info GetInfo(int IdCatalogo)
        {
            try
            {
                return odata.getInfo(IdCatalogo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetOrden(int IdCatalogoTipo)
        {
            try
            {
                return odata.getOrden(IdCatalogoTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Catalogo_Info info)
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

        public bool ModificarDB(aca_Catalogo_Info info)
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

        public bool AnularDB(aca_Catalogo_Info info)
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
