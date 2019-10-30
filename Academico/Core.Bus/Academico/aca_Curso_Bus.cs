using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Curso_Bus
    {
        aca_Curso_Data odata = new aca_Curso_Data();
        public List<aca_Curso_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, MostrarAnulados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_Curso_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_Curso_Info GetInfo(int IdEmpresa, int IdCurso)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdCurso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetOrden(int IdEmpresa)
        {
            try
            {
                return odata.getOrden(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Curso_Info info)
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

        public bool ModificarDB(aca_Curso_Info info)
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

        public bool AnularDB(aca_Curso_Info info)
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
