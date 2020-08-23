using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivoCalificacionCualitativa_Bus
    {
        aca_AnioLectivoCalificacionCualitativa_Data odata = new aca_AnioLectivoCalificacionCualitativa_Data();

        public List<aca_AnioLectivoCalificacionCualitativa_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoCalificacionCualitativa_Info getInfo(int IdEmpresa, int IdAnio, int IdCalificacionCualitativa)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdCalificacionCualitativa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivoCalificacionCualitativa_Info info)
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

        public bool modificarDB(aca_AnioLectivoCalificacionCualitativa_Info info)
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

        public bool anularDB(aca_AnioLectivoCalificacionCualitativa_Info info)
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
