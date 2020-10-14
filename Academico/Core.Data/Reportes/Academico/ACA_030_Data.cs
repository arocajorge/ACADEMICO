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
    public class ACA_030_Data
    {
        public List<ACA_030_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcialTipo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_030_Info> Lista = new List<ACA_030_Info>();
                List<ACA_030_Info> ListaFinal = new List<ACA_030_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_030(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados, IdCatalogoParcialTipo).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_030_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
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
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo??0,
                            OrdenMateriaArea = q.OrdenMateriaArea,
                            OrdenMateria = q.OrdenMateria,
                            NomMateriaArea = q.NomMateriaArea,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            NombreTutor = q.NombreTutor,
                            NombreInspector = q.NombreInspector,
                            Calificacion =q.Calificacion,
                            Columna=q.Columna,
                            OrdenColumna = q.OrdenColumna
                        });
                    }
                }

                //var lstAgrupada = Lista.Where(q=>q.Columna=="PROM").GroupBy(q => new
                //{
                //    q.IdEmpresa,
                //    q.IdMatricula,
                //    q.IdAlumno,
                //    q.pe_nombreCompleto,
                //    q.Codigo,
                //    q.IdAnio,
                //    q.IdSede,
                //    q.IdNivel,
                //    q.IdJornada,
                //    q.IdCurso,
                //    q.IdParalelo,
                //    q.CodigoParalelo,
                //    q.Descripcion,
                //    q.NomSede,
                //    q.NomNivel,
                //    q.NomJornada,
                //    q.NomMateria,
                //    q.NomCurso,
                //    q.NomParalelo,
                //    q.OrdenNivel,
                //    q.OrdenJornada,
                //    q.OrdenCurso,
                //    q.OrdenParalelo,
                //    q.OrdenMateriaGrupo,
                //    q.NomMateriaGrupo,
                //    q.NombreTutor,
                //    q.NombreInspector,
                //}).Select(q => new ACA_030_Info
                //{
                //    IdEmpresa = q.Key.IdEmpresa,
                //    IdMatricula = q.Key.IdMatricula,
                //    IdAlumno = q.Key.IdAlumno,
                //    pe_nombreCompleto = q.Key.pe_nombreCompleto,
                //    Codigo = q.Key.Codigo,
                //    IdAnio = q.Key.IdAnio,
                //    IdSede = q.Key.IdSede,
                //    IdNivel = q.Key.IdNivel,
                //    IdJornada = q.Key.IdJornada,
                //    IdCurso = q.Key.IdCurso,
                //    IdParalelo = q.Key.IdParalelo,
                //    CodigoParalelo = q.Key.CodigoParalelo,
                //    Descripcion = q.Key.Descripcion,
                //    NomSede = q.Key.NomSede,
                //    NomNivel = q.Key.NomNivel,
                //    NomJornada = q.Key.NomJornada,
                //    NomCurso = q.Key.NomCurso,
                //    NomParalelo = q.Key.NomParalelo,
                //    OrdenNivel =  q.Key.OrdenNivel,
                //    OrdenJornada = q.Key.OrdenJornada,
                //    OrdenCurso = q.Key.OrdenCurso,
                //    OrdenParalelo = q.Key.OrdenParalelo,
                //    NomMateriaGrupo = q.Key.NomMateriaGrupo,
                //    OrdenMateriaGrupo = q.Key.OrdenMateriaGrupo,
                //    NombreTutor = q.Key.NombreTutor,
                //    NombreInspector = q.Key.NombreInspector,
                //    PromedioFinalGrupoDouble = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion)),
                //}).ToList();

                //Lista.AddRange(lstAgrupada);


                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
