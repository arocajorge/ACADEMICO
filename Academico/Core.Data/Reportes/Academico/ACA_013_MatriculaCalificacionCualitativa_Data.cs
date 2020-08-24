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
    public class ACA_013_MatriculaCalificacionCualitativa_Data
    {
        public List<ACA_013_MatriculaCalificacionCualitativa_Info> get_list(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial)
        {
            try
            {
                List<ACA_013_MatriculaCalificacionCualitativa_Info> Lista = new List<ACA_013_MatriculaCalificacionCualitativa_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_013_MatriculaCalificacionCualitativa(IdEmpresa, IdMatricula, IdCatalogoParcial).ToList();


                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            IdCalificacionCualitativa = q.IdCalificacionCualitativa,
                            CodigoCalificacionCualitativa = q.CodigoCalificacionCualitativa,
                            IdAlumno = q.IdAlumno,
                            IdMateria = q.IdMateria,
                            //IdProfesor = q.IdProfesor,
                            NombreAlumno = q.NombreAlumno,
                            NombreProfesor = q.NombreProfesor,
                            Conducta = q.Conducta,
                            MotivoConducta = q.MotivoConducta,
                            OrdenMateriaArea = q.OrdenMateriaArea,
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                            OrdenMateria = q.OrdenMateria,
                            NomMateria = q.NomMateria,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            NomMateriaArea = q.NomMateriaArea,
                            OrdenCurso = q.OrdenCurso,
                            OrdenJornada=q.OrdenJornada,
                            OrdenNivel=q.OrdenNivel,
                            OrdenParalelo=q.OrdenParalelo,
                            DescripcionCorta = q.DescripcionCorta,
                            CodigoParalelo = q.CodigoParalelo,
                            EsObligatorio = q.EsObligatorio,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdCurso=q.IdCurso,
                            IdJornada=q.IdJornada,
                            IdNivel=q.IdNivel,
                            IdParalelo =q.IdParalelo,
                            NomSede = q.NomSede,
                            NomJornada=q.NomJornada,
                            NomNivel=q.NomNivel,
                            NomCurso=q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            Letra = q.Letra
                        });
                    }

                    return Lista;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
