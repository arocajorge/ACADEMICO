using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_010_Data
    {
        public List<ACA_010_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                List<ACA_010_Info> Lista = new List<ACA_010_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    //Context.SetCommandTimeOut(5000);
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.VWACA_010.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede &&
                    q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdCatalogoParcial == IdCatalogoParcial).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new ACA_010_Info
                        {
                            RowNumber = q.RowNumber,
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Alumno = q.Alumno,
                            Codigo = q.Codigo,
                            Profesor = q.Profesor,
                            NomNivel = q.NomNivel,
                            NomJornada =q.NomJornada,
                            NomMateria = q.NomMateria,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            IdCatalogoParcial = q.IdCatalogoParcial,
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
                            AccionRemedial = q.AccionRemedial,
                            //CalificacionP1 = q.CalificacionP1,
                            //CalificacionP2 = q.CalificacionP2,
                            //CalificacionP3 = q.CalificacionP3,
                            //CalificacionP4 = q.CalificacionP4,
                            //CalificacionP5 = q.CalificacionP5,
                            //CalificacionP6 = q.CalificacionP6,
                            //Rendimiento = (IdCatalogoParcial== Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1) ? q.CalificacionP1 : 
                            //IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2) ? q.CalificacionP2 : 
                            //IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3)?q.CalificacionP3 : 
                            //IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4)?q.CalificacionP4 : 
                            //IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5)?q.CalificacionP5 : q.CalificacionP6)
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
