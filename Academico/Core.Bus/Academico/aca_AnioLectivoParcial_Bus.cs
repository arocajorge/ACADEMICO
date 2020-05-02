using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivoParcial_Bus
    {
        aca_AnioLectivoParcial_Data odata = new aca_AnioLectivoParcial_Data();
        public List<aca_AnioLectivoParcial_Info> GetList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_AnioLectivoParcial_Info GetInfo(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoParcial)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdSede, IdAnio, IdCatalogoParcial);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<aca_AnioLectivoParcial_Info> GetList_x_Tipo(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo)
        {
            try
            {
                return odata.getList_x_Tipo(IdEmpresa, IdSede, IdAnio, IdCatalogoTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_AnioLectivoParcial_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo, DateTime FechaActual)
        {
            try
            {
                return odata.getList(IdEmpresa, IdSede, IdAnio, IdCatalogoTipo, FechaActual);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(List<aca_AnioLectivoParcial_Info> lista)
        {
            try
            {
                return odata.guardarDB(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_AnioLectivoParcial_Info info)
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
