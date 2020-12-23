using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_070_Data
    {
        cl_funciones funciones = new cl_funciones();
        public List<ACA_070_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, bool MostrarRetirados)
        {
            try
            {
                List<ACA_070_Info> Lista = new List<ACA_070_Info>();
                List<ACA_070_Info> ListaObligatorias = new List<ACA_070_Info>();
                List<ACA_070_Info> ListaComplementarias = new List<ACA_070_Info>();
                List<ACA_070_Info> ListaPromediadaComplementarias = new List<ACA_070_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "DECLARE @IdEmpresa int =" + IdEmpresa.ToString()+ ","
                    + " @IdAnio int = " + IdAnio.ToString() + ","
                    + " @IdSede int = " + IdSede.ToString() + ","
                    + " @IdNivel int = " + IdNivel.ToString() + ","
                    + " @IdJornada int = " + IdJornada.ToString() + ","
                    + " @IdCurso int= " + IdCurso.ToString() + ","
                    + " @IdParalelo int = " + IdParalelo.ToString() + ","
                    + " @MostrarRetirados bit = " + (MostrarRetirados==true ? 1 : 0) + ";"
                    + " /*MATERIAS QUE NO SE PROMEDIAN*/ "
                    + " /*PROMEDIO FINAL DE MATERIAS QUE NO SE PROMEDIAN*/ "
                    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno,  mc.IdMatricula,mc.IdMateria, "
                    + " CM.NomMateria,CM.NomMateriaGrupo,CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo,CM.IdCatalogoTipoCalificacion, "
                    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, "
                    + " CAST(mc.PromedioFinal as varchar) Calificacion, CAST(mc.PromedioFinal AS numeric(18, 2)) AS CalificacionNumerica "
                    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                    + " mc.IdMateria = CM.IdMateria "
                    + " LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                    + " LEFT OUTER JOIN "
                        + " (SELECT IdEmpresa, IdMatricula "
                        + " FROM      dbo.aca_AlumnoRetiro AS r "
                        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                    + " where mc.IdEmpresa = @IdEmpresa "
                    + " and m.IdAnio = @IdAnio "
                    + " and m.IdSede = @IdSede "
                    + " and m.IdNivel = @IdNivel "
                    + " and m.IdJornada = @IdJornada "
                    + " and m.IdCurso = @IdCurso "
                    + " and m.IdParalelo = @IdParalelo "
                    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                    + " and cm.PromediarGrupo = 0 "
                    + " and cm.IdCatalogoTipoCalificacion = 40 "
                    + " /*MATERIAS QUE SE PROMEDIAN*/ "
                    + " /*PROMEDIO X MATERIA OPTATIVA*/ "
                    + " UNION ALL "
                    + " ( "
                    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinal as varchar) Calificacion, "
                    + " mc.PromedioFinal as CalificacionNumerica "
                    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                    + " mc.IdMateria = CM.IdMateria "
                    + " LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                    + " LEFT OUTER JOIN "
                        + " (SELECT IdEmpresa, IdMatricula "
                        + " FROM      dbo.aca_AlumnoRetiro AS r "
                        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                    + " where mc.IdEmpresa = @IdEmpresa "
                    + " and m.IdAnio = @IdAnio "
                    + " and m.IdSede = @IdSede "
                    + " and m.IdJornada = @IdJornada "
                    + " and m.IdNivel = @IdNivel "
                    + " and m.IdCurso = @IdCurso "
                    + " and m.IdParalelo = @IdParalelo "
                    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                    + " and cm.PromediarGrupo = 1 "
                    + " and cm.IdCatalogoTipoCalificacion = 40 "
                    + " ) ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_070_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenParalelo"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            PromediarGrupo = string.IsNullOrEmpty(reader["PromediarGrupo"].ToString()) ? false : Convert.ToBoolean(reader["PromediarGrupo"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Calificacion = string.IsNullOrEmpty(reader["Calificacion"].ToString()) ? null : reader["Calificacion"].ToString(),
                            CalificacionNumerica = string.IsNullOrEmpty(reader["CalificacionNumerica"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionNumerica"]),
                        });
                    }
                    reader.Close();
                }

                ListaObligatorias = Lista.Where(q => q.PromediarGrupo == false).ToList();
                ListaObligatorias.ForEach(q => q.tieneCalificacionNula = (string.IsNullOrEmpty(q.Calificacion) ? 1 : 0));
                var lstPromedioObligatorias = ListaObligatorias.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.NomMateriaGrupo,
                }).Select(q => new ACA_070_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    tieneCalificacionNula = q.Sum(g => g.tieneCalificacionNula),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();
                lstPromedioObligatorias.ForEach(q => q.PromedioCalculado = (q.tieneCalificacionNula == 0 ? q.PromedioCalculado : (decimal?)null));

                ListaComplementarias = Lista.Where(q => q.PromediarGrupo == true).ToList();
                ListaComplementarias.ForEach(q => q.tieneCalificacionNula = (string.IsNullOrEmpty(q.Calificacion) ? 1 : 0));
                var lstPromedioComplementarias = ListaComplementarias.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.NomMateriaGrupo,
                }).Select(q => new ACA_070_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    tieneCalificacionNula = q.Sum(g => g.tieneCalificacionNula),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();
                lstPromedioComplementarias.ForEach(q => q.PromedioCalculado = (q.tieneCalificacionNula == 0 ? q.PromedioCalculado : (decimal?)null));

                var ListaPromediar = new List<ACA_070_Info>();
                ListaPromediar.AddRange(lstPromedioObligatorias);
                ListaPromediar.AddRange(lstPromedioComplementarias);

                var lstFinal = ListaPromediar.GroupBy(q => new { q.IdEmpresa, q.IdMatricula }).Select(q => new ACA_070_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    PromedioCalculado = q.Max(g => g.PromedioCalculado) == null ? (decimal?)null : (q.Sum(g => g.PromedioCalculado) / q.Count(g => g.PromedioCalculado != null)),
                }).ToList();

                var ListaAlumnos = ListaComplementarias.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdAlumno,
                    q.pe_nombreCompleto,
                }).Select(q => new ACA_070_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdAlumno = q.Key.IdAlumno,
                    pe_nombreCompleto = q.Key.pe_nombreCompleto
                }).ToList();

                ListaPromediar = (from a in ListaAlumnos
                                  join b in lstFinal
                                  on a.IdMatricula equals b.IdMatricula
                                  select new ACA_070_Info
                                  {
                                      IdEmpresa = a.IdEmpresa,
                                      IdMatricula = a.IdMatricula,
                                      IdAlumno = a.IdAlumno,
                                      pe_nombreCompleto = a.pe_nombreCompleto,
                                      PromedioCalculado = b.PromedioCalculado == null ? (decimal?)null : Math.Round(b.PromedioCalculado ?? 0, 2, MidpointRounding.AwayFromZero)
                                  }).ToList();

                var ListaFinal = ListaPromediar.Where(q => q.PromedioCalculado != null && q.PromedioCalculado >= Convert.ToDecimal(9.50)).ToList();
                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
