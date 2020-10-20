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
    public class ACA_034_Data
    {
        public List<ACA_034_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_034_Info> Lista = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaFinal = new List<ACA_034_Info>();

                List<ACA_034_Info> ListaInicial = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaObligatorias = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaOptativas = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaOptativasIndividuales = new List<ACA_034_Info>();


                List<ACA_034_Info> ListaPromedioObligatorias = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaPromedioOptativas = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaPromedioProyectos = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaPromedioGeneral = new List<ACA_034_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_034(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_034_Info
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
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateria = q.OrdenMateria,
                            Calificacion = q.Calificacion,
                            CalificacionNumerica = q.CalificacionNumerica,
                            IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion,
                            Columna = q.Columna,
                            NombreGrupo = q.NombreGrupo,
                            NombreMateria = q.NombreMateria,
                            OrdenColumna = q.OrdenColumna,
                            OrdenGrupo = q.OrdenGrupo,
                            PromediarGrupo = q.PromediarGrupo
                        });
                    }
                }

                ListaInicial = Lista.Where(q => q.IdMateria == 0 ).ToList();
                ListaFinal.AddRange(ListaInicial);

                ListaObligatorias = Lista.Where(q => q.PromediarGrupo == 0 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativasIndividuales = Lista.Where(q => q.PromediarGrupo == 1 && q.NombreGrupo == "OPTATIVAS  INDIVIDUAL" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativas = Lista.Where(q => q.PromediarGrupo == 1 && q.NombreGrupo == "OPTATIVAS" && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();

                var lstPromediosNull_Obligatorias = ListaObligatorias.Where(q => (q.Columna == "I QUIMESTRE" || q.Columna == "II QUIMESTRE") && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdMateria
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdMateria = q.Key.IdMateria
                }).ToList();

                var lstPromediosNull_Optativas = ListaOptativasIndividuales.Where(q => (q.Columna == "I QUIMESTRE" || q.Columna == "II QUIMESTRE") && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdMateria
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdMateria = q.Key.IdMateria
                }).ToList();

                ListaFinal.AddRange(ListaObligatorias.Where(q=>q.Columna!= "PROMEDIO"));

                var lstLeftJoin_PromediosObligatorias =
                  (from a in ListaObligatorias
                   join b in lstPromediosNull_Obligatorias on new { a.IdMatricula, a.IdMateria } equals new { b.IdMatricula, b.IdMateria } into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna == "PROMEDIO"
                   select new ACA_034_Info
                   {
                       IdEmpresa = a.IdEmpresa,
                       IdMatricula = a.IdMatricula,
                       IdMateria = a.IdMateria,
                       IdAlumno = a.IdAlumno,
                       pe_nombreCompleto = a.pe_nombreCompleto,
                       Codigo = a.Codigo,
                       IdAnio = a.IdAnio,
                       IdSede = a.IdSede,
                       IdNivel = a.IdNivel,
                       IdJornada = a.IdJornada,
                       IdCurso = a.IdCurso,
                       IdParalelo = a.IdParalelo,
                       CodigoParalelo = a.CodigoParalelo,
                       Descripcion = a.Descripcion,
                       NomSede = a.NomSede,
                       NomNivel = a.NomNivel,
                       NomJornada = a.NomJornada,
                       NomCurso = a.NomCurso,
                       NomParalelo = a.NomParalelo,
                       OrdenNivel = a.OrdenNivel,
                       OrdenJornada = a.OrdenJornada,
                       OrdenCurso = a.OrdenCurso,
                       OrdenParalelo = a.OrdenParalelo,
                       OrdenMateria = a.OrdenMateria,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn != null) ? a.CalificacionNumerica : null,
                       NombreGrupo=a.NombreGrupo,
                       NombreMateria=a.NombreMateria,
                       OrdenGrupo=a.OrdenGrupo
                   }
                  ).ToList();

                ListaFinal.AddRange(lstLeftJoin_PromediosObligatorias);

                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
