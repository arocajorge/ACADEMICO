using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaCalificacionParcial_Data
    {
        public List<aca_MatriculaCalificacionParcial_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            Parcial = q.Parcial,
                            IdProfesor = q.IdProfesor,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_MatriculaCalificacionParcial_Info> generarCalificacion(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula==IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
