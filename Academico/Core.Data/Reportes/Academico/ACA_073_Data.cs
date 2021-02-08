﻿using Core.Data.Academico;
using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_073_Data
    {
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        public List<ACA_073_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, bool MostrarRetirados)
        {
            try
            {
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                List<ACA_073_Info> Lista = new List<ACA_073_Info>();
                List<ACA_073_Info> ListaPromedioPorNivel = new List<ACA_073_Info>();
                List<ACA_073_Info> ListaFinal = new List<ACA_073_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "DECLARE @MostrarRetirados int = " + (MostrarRetirados == true ? 1 : 0) + ";"
                    + " SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, p.pe_cedulaRuc, "
                    + " an.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, "
                    + " anc.Descripcion AnioCal, n.NomNivel NivelCal, n.Orden OrdenNivelCal, c.NomCurso CursoCal, c.OrdenCurso OrdenCursoCal, h.Promedio "
                    + " FROM     dbo.aca_Matricula AS m LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON p.IdPersona = a.IdPersona  LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoCalificacionHistorico AS h ON a.IdEmpresa = h.IdEmpresa AND a.IdAlumno = h.IdAlumno LEFT OUTER JOIN "
                    + " dbo.aca_Curso AS c ON h.IdEmpresa = c.IdEmpresa AND h.IdCurso = c.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_NivelAcademico AS n ON h.IdEmpresa = n.IdEmpresa AND h.IdNivel = n.IdNivel "
                    + " left join aca_AnioLectivo anc on h.IdEmpresa = anc.IdEmpresa and h.IdAnio = anc.IdAnio "
                    + " left join aca_AnioLectivo an on m.IdEmpresa = an.IdEmpresa and m.IdAnio = an.IdAnio "
                    + " left join aca_AnioLectivo_Sede_NivelAcademico sn on m.IdEmpresa = sn.IdEmpresa and m.IdSede = sn.IdSede and m.IdAnio = sn.IdAnio and m.IdNivel = sn.IdNivel "
                    + " left join aca_AnioLectivo_NivelAcademico_Jornada nj on m.IdEmpresa = nj.IdEmpresa and m.IdSede = nj.IdSede and m.IdAnio = nj.IdAnio and m.IdNivel = nj.IdNivel and m.IdJornada = nj.IdJornada "
                    + " left join aca_AnioLectivo_Jornada_Curso jc on m.IdEmpresa = jc.IdEmpresa and m.IdSede = jc.IdSede and m.IdAnio = jc.IdAnio and m.IdNivel = jc.IdNivel and m.IdJornada = jc.IdJornada and m.IdCurso = jc.IdCurso "
                    + " left join aca_AnioLectivo_Curso_Paralelo cp on m.IdEmpresa = cp.IdEmpresa and m.IdSede = cp.IdSede and m.IdAnio = cp.IdAnio and m.IdNivel = cp.IdNivel and m.IdJornada = cp.IdJornada and m.IdCurso = cp.IdCurso and m.IdParalelo = cp.IdParalelo "
                    + " left join aca_AlumnoRetiro AS r ON m.IdEmpresa = r.IdEmpresa AND m.IdMatricula = r.IdMatricula AND r.Estado = 1 "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " and isnull(r.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(r.IdMatricula,0) else 0 end ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 5000;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_073_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenParalelo"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            AnioCal = string.IsNullOrEmpty(reader["AnioCal"].ToString()) ? null : reader["AnioCal"].ToString(),
                            NivelCal = string.IsNullOrEmpty(reader["NivelCal"].ToString()) ? null : reader["NivelCal"].ToString(),
                            CursoCal = string.IsNullOrEmpty(reader["CursoCal"].ToString()) ? null : reader["CursoCal"].ToString(),
                            OrdenNivelCal = string.IsNullOrEmpty(reader["OrdenNivelCal"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenNivelCal"]),
                            OrdenCursoCal = string.IsNullOrEmpty(reader["OrdenCursoCal"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCursoCal"]),
                            Promedio = string.IsNullOrEmpty(reader["Promedio"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Promedio"]),
                        });
                    }
                    reader.Close();
                }
                Lista.ForEach(q=>q.CalificacionNull = (q.Promedio==null ? 0 : 1));

                var ListaPromedioNivel = Lista.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdAlumno,
                    q.pe_cedulaRuc,
                    q.pe_nombreCompleto,
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
                    q.NivelCal,
                    q.OrdenNivelCal
                }).Select(q => new ACA_073_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdAlumno = q.Key.IdAlumno,
                    pe_cedulaRuc = q.Key.pe_cedulaRuc,
                    pe_nombreCompleto = q.Key.pe_nombreCompleto,
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
                    NivelCal = q.Key.NivelCal,
                    OrdenNivelCal = q.Key.OrdenNivelCal,
                    CalificacionNull = q.Sum(g => g.CalificacionNull),
                    SumaGeneral = q.Sum(g => Convert.ToDecimal(g.Promedio)),
                    PromedioCalculado = q.Max(g => g.Promedio) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Promedio)) / q.Count(g => !string.IsNullOrEmpty(g.Promedio.ToString()))
                }).ToList();
                ListaPromedioNivel.ForEach(q => { q.PromedioCalculado = (q.CalificacionNull == 0 ? q.PromedioCalculado : (decimal?)null); q.SumaGeneral = (q.CalificacionNull == 0 ? q.SumaGeneral : (decimal?)null); });

                var lst_Promedio= new List<ACA_073_Info>();
                foreach (var item in ListaPromedioNivel)
                {
                    lst_Promedio.Add(new ACA_073_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdAlumno = item.IdAlumno,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        pe_cedulaRuc = item.pe_cedulaRuc,
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
                        Promedio = (item.PromedioCalculado == null ? (decimal?)null : Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero)),
                        AnioCal = "",
                        NivelCal = item.NivelCal,
                        OrdenNivelCal = item.OrdenNivelCal,
                        CursoCal = "PROMEDIO",
                        OrdenCursoCal = 999,
                    });
                }

                Lista.AddRange(lst_Promedio);
                Lista.ForEach(q => q.CalificacionNull = (q.Promedio == null ? 1 : 0));


                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}