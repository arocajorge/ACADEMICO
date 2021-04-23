﻿using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_068_Data
    {
        cl_funciones funciones = new cl_funciones();
        public List<ACA_068_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, bool MostrarRetirados)
        {
            try
            {
                List<ACA_068_Info> Lista = new List<ACA_068_Info>();
                List<ACA_068_Info> ListaFinal = new List<ACA_068_Info>();
                List<ACA_068_Info> ListaObligatorias = new List<ACA_068_Info>();
                List<ACA_068_Info> ListaComplementarias = new List<ACA_068_Info>();
                List<ACA_068_Info> ListaPromediadaComplementarias = new List<ACA_068_Info>();

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "DECLARE @IdEmpresa int =" + IdEmpresa.ToString() + ", @IdAnio int = " + IdAnio.ToString() + ", @IdSede int = " + IdSede.ToString() + ", @IdNivel int = " + IdNivel.ToString() + ", @IdJornada int = " + IdJornada.ToString() + ", @IdCurso int= " + IdCurso.ToString() + ", @IdParalelo int = " + IdParalelo.ToString() + ", @MostrarRetirados bit = " + (MostrarRetirados == false ? 0 : 1)
                    + " /*PROMEDIO FINAL*/ "
                    + " SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, nj.NomJornada, nj.OrdenJornada, sn.NomNivel, sn.OrdenNivel, jc.NomCurso, jc.OrdenCurso, "
                    + " cp.NomParalelo, cp.OrdenParalelo, alu.Codigo, pa.pe_nombreCompleto AS NombreAlumno, mc.IdMateria, mc.NomMateria NombreMateria, mc.NomMateriaGrupo NombreGrupo, mc.OrdenMateria, mc.OrdenMateriaGrupo OrdenGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, "
                    + " CAST(mco.PromedioFinal AS varchar) AS Calificacion, CAST(mco.PromedioFinal AS numeric(18, 2)) AS CalificacionNumerica, 'PROMEDIO FINAL' AS Columna, 7 AS OrdenColumna, pp.pe_nombreCompleto AS NombreTutor "
                    + " FROM     dbo.aca_Matricula AS m WITH (nolock) INNER JOIN "
                    + " dbo.aca_MatriculaCalificacion AS mco WITH (nolock) ON m.IdEmpresa = mco.IdEmpresa AND m.IdMatricula = mco.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AN WITH (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON sn.IdEmpresa = m.IdEmpresa AND sn.IdSede = m.IdSede AND sn.IdAnio = m.IdAnio AND sn.IdNivel = m.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON nj.IdEmpresa = m.IdEmpresa AND nj.IdAnio = m.IdAnio AND nj.IdSede = m.IdSede AND nj.IdNivel = m.IdNivel AND nj.IdJornada = m.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON jc.IdEmpresa = m.IdEmpresa AND jc.IdAnio = m.IdAnio AND jc.IdSede = m.IdSede AND jc.IdNivel = m.IdNivel AND jc.IdJornada = m.IdJornada AND jc.IdCurso = m.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND cp.IdCurso = m.IdCurso AND "
                    + " cp.IdParalelo = m.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS mc WITH (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdAnio = m.IdAnio AND mc.IdSede = m.IdSede AND mc.IdNivel = m.IdNivel AND mc.IdJornada = m.IdJornada AND mc.IdCurso = m.IdCurso AND "
                    + " mc.IdMateria = mco.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS alu WITH (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pa WITH (nolock) ON alu.IdPersona = pa.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Profesor AS pro WITH (nolock) ON pro.IdEmpresa = m.IdEmpresa AND pro.IdProfesor = cp.IdProfesorTutor LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pp WITH (nolock) ON pro.IdPersona = pp.IdPersona LEFT OUTER JOIN "
                    + " (SELECT IdEmpresa, IdMatricula "
                    + " FROM      dbo.aca_AlumnoRetiro AS r WITH (nolock) "
                    + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                    //+ " WHERE mco.IdEmpresa = @IdEmpresa "
                    //+ " and m.IdAnio = @IdAnio "
                    //+ " and m.IdSede = @IdSede "
                    //+ " and m.IdNivel = @IdNivel "
                    //+ " and m.IdJornada = @IdJornada "
                    //+ " and m.IdCurso = @IdCurso "
                    //+ " and m.IdParalelo = @IdParalelo "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                    + " and mc.PromediarGrupo = 0 "
                    + " and mc.IdCatalogoTipoCalificacion = 40 "
                    + " UNION ALL "
                    + " ( "
                    + " /*MATERIAS QUE SE PROMEDIAN*/ "
                    + " /*PROMEDIO FINAL*/ "
                    + " SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, AN.Descripcion, sn.NomSede, nj.NomJornada, nj.OrdenJornada, sn.NomNivel, sn.OrdenNivel, jc.NomCurso, jc.OrdenCurso, "
                    + " cp.NomParalelo, cp.OrdenParalelo, alu.Codigo, pa.pe_nombreCompleto AS NombreAlumno, mc.IdMateria, mc.NomMateria, mc.NomMateriaGrupo, mc.OrdenMateria, mc.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, "
                    + " CAST(mco.PromedioFinal AS varchar) AS Calificacion, CAST(mco.PromedioFinal AS numeric(18, 2)) AS CalificacionNumerica, 'PROMEDIO FINAL' AS Columna, 7 AS OrdenColumna, pp.pe_nombreCompleto AS NombreTutor "
                    + " FROM     dbo.aca_Matricula AS m WITH (nolock) INNER JOIN "
                    + " dbo.aca_MatriculaCalificacion AS mco WITH (nolock) ON m.IdEmpresa = mco.IdEmpresa AND m.IdMatricula = mco.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AN WITH (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON sn.IdEmpresa = m.IdEmpresa AND sn.IdSede = m.IdSede AND sn.IdAnio = m.IdAnio AND sn.IdNivel = m.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON nj.IdEmpresa = m.IdEmpresa AND nj.IdAnio = m.IdAnio AND nj.IdSede = m.IdSede AND nj.IdNivel = m.IdNivel AND nj.IdJornada = m.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON jc.IdEmpresa = m.IdEmpresa AND jc.IdAnio = m.IdAnio AND jc.IdSede = m.IdSede AND jc.IdNivel = m.IdNivel AND jc.IdJornada = m.IdJornada AND jc.IdCurso = m.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON cp.IdEmpresa = m.IdEmpresa AND cp.IdAnio = m.IdAnio AND cp.IdSede = m.IdSede AND cp.IdNivel = m.IdNivel AND cp.IdJornada = m.IdJornada AND cp.IdCurso = m.IdCurso AND "
                    + " cp.IdParalelo = m.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS mc WITH (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdAnio = m.IdAnio AND mc.IdSede = m.IdSede AND mc.IdNivel = m.IdNivel AND mc.IdJornada = m.IdJornada AND mc.IdCurso = m.IdCurso AND "
                    + " mc.IdMateria = mco.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS alu WITH (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pa WITH (nolock) ON alu.IdPersona = pa.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Profesor AS pro WITH (nolock) ON pro.IdEmpresa = m.IdEmpresa AND pro.IdProfesor = cp.IdProfesorTutor LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pp WITH (nolock) ON pro.IdPersona = pp.IdPersona LEFT OUTER JOIN "
                    + " (SELECT IdEmpresa, IdMatricula "
                    + " FROM      dbo.aca_AlumnoRetiro AS r WITH (nolock) "
                    + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                    //+ " WHERE mco.IdEmpresa = @IdEmpresa "
                    //+ " and m.IdAnio = @IdAnio "
                    //+ " and m.IdSede = @IdSede "
                    //+ " and m.IdNivel = @IdNivel "
                    //+ " and m.IdJornada = @IdJornada "
                    //+ " and m.IdCurso = @IdCurso "
                    //+ " and m.IdParalelo = @IdParalelo "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                    + " and mc.PromediarGrupo = 1 "
                    + " and mc.IdCatalogoTipoCalificacion = 40 "
                    + " ) ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_068_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = string.IsNullOrEmpty(reader["IdMateria"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdMateria"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            NombreAlumno = string.IsNullOrEmpty(reader["NombreAlumno"].ToString()) ? null : reader["NombreAlumno"].ToString(),
                            NombreTutor = string.IsNullOrEmpty(reader["NombreTutor"].ToString()) ? null : reader["NombreTutor"].ToString(),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenParalelo"]),
                            OrdenMateria = string.IsNullOrEmpty(reader["OrdenMateria"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateria"]),
                            Calificacion = string.IsNullOrEmpty(reader["Calificacion"].ToString()) ? null : reader["Calificacion"].ToString(),
                            CalificacionNumerica = string.IsNullOrEmpty(reader["CalificacionNumerica"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionNumerica"]),
                            IdCatalogoTipoCalificacion = string.IsNullOrEmpty(reader["IdCatalogoTipoCalificacion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoTipoCalificacion"]),
                            Columna = string.IsNullOrEmpty(reader["Columna"].ToString()) ? null : reader["Columna"].ToString(),
                            NombreGrupo = string.IsNullOrEmpty(reader["NombreGrupo"].ToString()) ? null : reader["NombreGrupo"].ToString(),
                            NombreMateria = string.IsNullOrEmpty(reader["NombreMateria"].ToString()) ? null : reader["NombreMateria"].ToString(),
                            OrdenColumna = Convert.ToInt32(reader["OrdenColumna"]),
                            OrdenGrupo = string.IsNullOrEmpty(reader["OrdenGrupo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenGrupo"]),
                            PromediarGrupo = string.IsNullOrEmpty(reader["PromediarGrupo"].ToString()) ? 0 : Convert.ToInt32(reader["PromediarGrupo"]),
                        });
                    }
                    reader.Close();
                }

                ListaObligatorias = Lista.Where(q => q.PromediarGrupo == 0 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                var ListaObligatorias_ValidaCalificaciones = ListaObligatorias.Where(q => q.Columna == "PROMEDIO FINAL").ToList();
                ListaObligatorias_ValidaCalificaciones.ForEach(q => q.NoTieneCalificacion = (q.Calificacion == null ? 1 : 0));
                ListaFinal.AddRange(ListaObligatorias);

                ListaComplementarias = Lista.Where(q => q.PromediarGrupo == 1 && q.IdMateria != null && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                var ListaComplementarias_PromedioFinal = ListaComplementarias.Where(q => q.Columna == "PROMEDIO FINAL").ToList();

                #region PromedioFinal
                ListaComplementarias_PromedioFinal.ForEach(q => q.NoTieneCalificacion = (q.Calificacion == null ? 1 : 0));
                var ListaComplementariasProm_Final = ListaComplementarias_PromedioFinal.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdAlumno,
                    q.NombreAlumno,
                    q.Codigo,
                    q.IdAnio,
                    q.IdSede,
                    q.IdJornada,
                    q.IdCurso,
                    q.IdParalelo,
                    q.IdNivel,
                    q.Descripcion,
                    q.NomSede,
                    q.NomNivel,
                    q.NomJornada,
                    q.NomCurso,
                    q.NomParalelo,
                    q.OrdenNivel,
                    q.OrdenJornada,
                    q.OrdenCurso,
                    q.OrdenParalelo,
                }).Select(q => new ACA_068_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdAlumno = q.Key.IdAlumno,
                    NombreAlumno = q.Key.NombreAlumno,
                    Codigo = q.Key.Codigo,
                    IdAnio = q.Key.IdAnio,
                    IdSede = q.Key.IdSede,
                    IdJornada = q.Key.IdJornada,
                    IdCurso = q.Key.IdCurso,
                    IdParalelo = q.Key.IdParalelo,
                    IdNivel = q.Key.IdNivel,
                    Descripcion = q.Key.Descripcion,
                    NomSede = q.Key.NomSede,
                    NomNivel = q.Key.NomNivel,
                    NomJornada = q.Key.NomJornada,
                    NomCurso = q.Key.NomCurso,
                    NomParalelo = q.Key.NomParalelo,
                    OrdenNivel = q.Key.OrdenNivel,
                    OrdenJornada = q.Key.OrdenJornada,
                    OrdenCurso = q.Key.OrdenCurso,
                    OrdenParalelo = q.Key.OrdenParalelo,
                    NoTieneCalificacion = q.Sum(g => g.NoTieneCalificacion),
                    SumaGeneral = q.Sum(g => Convert.ToDecimal(g.Calificacion)),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();
                ListaComplementariasProm_Final.ForEach(q => { q.PromedioCalculado = (q.NoTieneCalificacion == 0 ? q.PromedioCalculado : (decimal?)null); q.SumaGeneral = (q.NoTieneCalificacion == 0 ? q.SumaGeneral : (decimal?)null); });

                var lst_promedio_complementarias_PromFinal = new List<ACA_068_Info>();
                foreach (var item in ListaComplementariasProm_Final)
                {
                    lst_promedio_complementarias_PromFinal.Add(new ACA_068_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdMateria = 0,
                        IdAlumno = item.IdAlumno,
                        NombreAlumno = item.NombreAlumno,
                        Codigo = item.Codigo,
                        IdAnio = item.IdAnio,
                        IdSede = item.IdSede,
                        IdJornada = item.IdJornada,
                        IdCurso = item.IdCurso,
                        IdParalelo = item.IdParalelo,
                        IdNivel = item.IdNivel,
                        Descripcion = item.Descripcion,
                        NomSede = item.NomSede,
                        NomNivel = item.NomNivel,
                        NomJornada = item.NomJornada,
                        NomCurso = item.NomCurso,
                        NomParalelo = item.NomParalelo,
                        OrdenNivel = item.OrdenNivel,
                        OrdenJornada = item.OrdenJornada,
                        OrdenCurso = item.OrdenCurso,
                        OrdenParalelo = item.OrdenParalelo,
                        Calificacion = (item.PromedioCalculado == null ? null : Convert.ToString(Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero))),
                        CalificacionNumerica = (item.PromedioCalculado == null ? (decimal?)null : Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero)),
                        IdCatalogoTipoCalificacion = null,
                        Columna = "PROMEDIO FINAL",
                        NombreGrupo = "OPTATIVA",
                        NombreMateria = "OPTATIVA",
                        OrdenGrupo = 99,
                        OrdenMateria = 99,
                        OrdenColumna = 9,
                        PromediarGrupo = 0
                    });
                }

                ListaFinal.AddRange(lst_promedio_complementarias_PromFinal);
                ListaFinal.ForEach(q => q.NoTieneCalificacion = (q.Calificacion == null ? 1 : 0));
                #endregion

                var ListaPromediar = ListaFinal.Where(q => q.Columna == "PROMEDIO FINAL").ToList();
                var ListaPromedioGeneral = ListaPromediar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdAlumno,
                    q.NombreAlumno,
                    q.Codigo,
                    q.IdAnio,
                    q.IdSede,
                    q.IdJornada,
                    q.IdCurso,
                    q.IdParalelo,
                    q.IdNivel,
                    q.Descripcion,
                    q.NomSede,
                    q.NomNivel,
                    q.NomJornada,
                    q.NomCurso,
                    q.NomParalelo,
                    q.OrdenNivel,
                    q.OrdenJornada,
                    q.OrdenCurso,
                    q.OrdenParalelo,
                }).Select(q => new ACA_068_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdAlumno = q.Key.IdAlumno,
                    NombreAlumno = q.Key.NombreAlumno,
                    Codigo = q.Key.Codigo,
                    IdAnio = q.Key.IdAnio,
                    IdSede = q.Key.IdSede,
                    IdJornada = q.Key.IdJornada,
                    IdCurso = q.Key.IdCurso,
                    IdParalelo = q.Key.IdParalelo,
                    IdNivel = q.Key.IdNivel,
                    Descripcion = q.Key.Descripcion,
                    NomSede = q.Key.NomSede,
                    NomNivel = q.Key.NomNivel,
                    NomJornada = q.Key.NomJornada,
                    NomCurso = q.Key.NomCurso,
                    NomParalelo = q.Key.NomParalelo,
                    OrdenNivel = q.Key.OrdenNivel,
                    OrdenJornada = q.Key.OrdenJornada,
                    OrdenCurso = q.Key.OrdenCurso,
                    OrdenParalelo = q.Key.OrdenParalelo,
                    NoTieneCalificacion = q.Sum(g => g.NoTieneCalificacion),
                    SumaGeneral = q.Sum(g => Convert.ToDecimal(g.Calificacion)),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();
                ListaPromedioGeneral.ForEach(q => { q.PromedioCalculado = (q.NoTieneCalificacion == 0 ? q.PromedioCalculado : (decimal?)null); q.SumaGeneral = (q.NoTieneCalificacion == 0 ? q.SumaGeneral : (decimal?)null); });

                var lst_promedio_general = new List<ACA_068_Info>();
                foreach (var item in ListaPromedioGeneral)
                {
                    lst_promedio_general.Add(new ACA_068_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdMateria = 0,
                        IdAlumno = item.IdAlumno,
                        NombreAlumno = item.NombreAlumno,
                        Codigo = item.Codigo,
                        IdAnio = item.IdAnio,
                        IdSede = item.IdSede,
                        IdJornada = item.IdJornada,
                        IdCurso = item.IdCurso,
                        IdParalelo = item.IdParalelo,
                        IdNivel = item.IdNivel,
                        Descripcion = item.Descripcion,
                        NomSede = item.NomSede,
                        NomNivel = item.NomNivel,
                        NomJornada = item.NomJornada,
                        NomCurso = item.NomCurso,
                        NomParalelo = item.NomParalelo,
                        OrdenNivel = item.OrdenNivel,
                        OrdenJornada = item.OrdenJornada,
                        OrdenCurso = item.OrdenCurso,
                        OrdenParalelo = item.OrdenParalelo,
                        Calificacion = (item.PromedioCalculado == null ? null : Convert.ToString(Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero))),
                        CalificacionNumerica = (item.PromedioCalculado == null ? (decimal?)null : Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero)),
                        IdCatalogoTipoCalificacion = null,
                        Columna = "PROMEDIO GENERAL",
                        NombreGrupo = "PROMEDIO GENERAL",
                        NombreMateria = "",
                        OrdenGrupo = 9999,
                        OrdenMateria = 9999,
                        OrdenColumna = 1,
                        PromediarGrupo = 0
                    });
                }
                ListaFinal.AddRange(lst_promedio_general);
                var ListaAlumnos = lst_promedio_general.ToList();
                ListaAlumnos.ForEach(q=> q.Num=1);
                return ListaAlumnos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
