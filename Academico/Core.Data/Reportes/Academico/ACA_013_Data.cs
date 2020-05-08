using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_013_Data
    {
        public List<ACA_013_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial)
        {
            try
            {

                List<ACA_013_Info> Lista = new List<ACA_013_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_013(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            NombreAlumno = q.NombreAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomMateria = q.NomMateria,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                            OrdenMateriaArea = q.OrdenMateriaArea,
                            OrdenMateria = q.OrdenMateria,
                            Calificacion = q.Calificacion,
                            EsObligatorio = q.EsObligatorio,
                            NomCatalogo = q.NomCatalogo,
                            NomMateriaArea = q.NomMateriaArea,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Remedial1 =q.Remedial1,
                            Remedial2=q.Remedial2,
                            Evaluacion = q.Evaluacion,
                            Letra = q.Letra,
                            PromedioParcial = q.PromedioParcial
                        });
                    }
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
