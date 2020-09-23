using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AnioLectivo_Curso_Paralelo_Dece_Bus
    {
        aca_AnioLectivo_Curso_Paralelo_Dece_Data odata = new aca_AnioLectivo_Curso_Paralelo_Dece_Data();

        public List<aca_AnioLectivo_Curso_Paralelo_Dece_Info> getList(int IdEmpresa, int IdAnio,int IdSede, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAnio, IdSede, MostrarAnulados);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivo_Curso_Paralelo_Dece_Info getInfo(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel, int IdCurso, int IdParalelo)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAnio, IdSede, IdJornada, IdNivel, IdCurso, IdParalelo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AnioLectivo_Curso_Paralelo_Dece_Info info)
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

        public bool modificarDB(aca_AnioLectivo_Curso_Paralelo_Dece_Info info)
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

        public bool anularDB(aca_AnioLectivo_Curso_Paralelo_Dece_Info info)
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
