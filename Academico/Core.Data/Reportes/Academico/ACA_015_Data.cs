using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_015_Data
    {
        public List<ACA_015_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                List<ACA_015_Info> Lista = new List<ACA_015_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    //Context.SetCommandTimeOut(5000);
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_015(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new ACA_015_Info
                        {
                            Num = 1,
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            IdentificacionAlumno = q.IdentificacionAlumno,
                            NombreAlumno = q.NombreAlumno,
                            FechaNacAlumno = q.FechaNacAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Direccion = q.Direccion,
                            Celular=q.Celular,
                            NomAnioLectivo =q.NomAnioLectivo,
                            NomPlantilla=q.NomPlantilla,
                            pe_telfono_Contacto = q.pe_telfono_Contacto,
                            Correo = q.Correo,
                            IdPlantilla=q.IdPlantilla

                        });
                    });
                }
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
