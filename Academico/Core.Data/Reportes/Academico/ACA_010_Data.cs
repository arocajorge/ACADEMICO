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
                    var lst = Context.SPACA_010(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial, IdMateria).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new ACA_010_Info
                        {
                            RowNumber = q.RowNumber,
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_cedulaRuc=q.pe_cedulaRuc,
                            NombreAlumno = q.NombreAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            NombreProfesor = q.NombreProfesor,
                            IdAnio =q.IdAnio,
                            IdSede =q.IdSede,
                            IdNivel=q.IdNivel,
                            IdJornada=q.IdJornada,
                            IdCurso=q.IdCurso,
                            IdParalelo=q.IdParalelo,
                            CodigoParalelo=q.CodigoParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada =q.NomJornada,
                            NomMateria = q.NomMateria,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel=q.OrdenNivel,
                            OrdenJornada=q.OrdenJornada,
                            OrdenCurso=q.OrdenCurso,
                            OrdenParalelo=q.OrdenParalelo,
                            OrdenMateriaGrupo=q.OrdenMateriaGrupo,
                            OrdenMateriaArea=q.OrdenMateriaArea,
                            OrdenMateria=q.OrdenMateria,
                            Calificacion =q.Calificacion,
                            EsObligatorio=q.EsObligatorio,
                            NomCatalogo=q.NomCatalogo,
                            NomMateriaArea=q.NomMateriaArea,
                            NomMateriaGrupo=q.NomMateriaGrupo,  
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Letra = q.Letra,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial,
                            PromedioParcial = q.PromedioParcial
                            
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
