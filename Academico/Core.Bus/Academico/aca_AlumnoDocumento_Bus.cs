using Core.Data.Academico;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_AlumnoDocumento_Bus
    {
        aca_AlumnoDocumento_Data odata = new aca_AlumnoDocumento_Data();

        public List<aca_AlumnoDocumento_Info> GetList(int IdEmpresa, decimal IdAlumno, bool MostrarEnArchivo)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAlumno, MostrarEnArchivo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_AlumnoDocumento_Info GetInfo(int IdEmpresa, decimal IdAlumno, int IdDocumento)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAlumno, IdDocumento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_AlumnoDocumento_Info> GetList_Matricula(int IdEmpresa, int IdAnio, string IdCurso, decimal IdAlumno)
        {
            try
            {
                return odata.get_list_matricula(IdEmpresa, IdAnio, IdCurso, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_AlumnoDocumento_Info info)
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

        public bool ModificarDB(aca_AlumnoDocumento_Info info)
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

        public bool EliminarDB(aca_AlumnoDocumento_Info info)
        {
            try
            {
                return odata.eliminarDB(info);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
