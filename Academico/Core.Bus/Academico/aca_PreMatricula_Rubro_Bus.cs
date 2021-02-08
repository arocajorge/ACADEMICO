using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_PrePreMatricula_Rubro_Bus
    {
        aca_PreMatricula_Rubro_Data odata = new aca_PreMatricula_Rubro_Data();

        public List<aca_PreMatricula_Rubro_Info> GetList_PreMatricula(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                return odata.getListPreMatricula(IdEmpresa, IdAnio, IdPlantilla);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_PreMatricula_Rubro_Info> GetList(int IdEmpresa, decimal IdPreMatricula)
        {
            try
            {
                return odata.getList(IdEmpresa, IdPreMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_PreMatricula_Rubro_Info GetInfo(int IdEmpresa, decimal IdPreMatricula, int IdPeriodo, int IdRubro)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdPreMatricula, IdPeriodo, IdRubro);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarDB(aca_PreMatricula_Rubro_Info info)
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

        public List<aca_PreMatricula_Rubro_Info> getList_FactMasiva(int IdEmpresa, int IdAnio, int IdPeriodo, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.getList_FactMasiva(IdEmpresa, IdAnio, IdPeriodo, IdSede, IdJornada, IdNivel, IdCurso, IdParalelo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
