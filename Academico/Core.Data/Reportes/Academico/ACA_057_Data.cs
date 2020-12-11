using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_057_Data
    {
        public List<ACA_057_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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

                List<ACA_057_Info> Lista = new List<ACA_057_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT cp.IdEmpresa, cp.IdAnio, cp.IdSede, cp.IdNivel, cp.IdJornada, cp.IdCurso, cp.IdParalelo, cm.NomMateria, cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cp.NomParalelo, cp.OrdenParalelo, jc.NomCurso, jc.OrdenCurso,  "
                    + " nj.NomJornada, nj.OrdenJornada, sn.NomSede, sn.NomNivel, sn.OrdenNivel, pp.IdProfesor, p.pe_nombreCompleto, p.pe_cedulaRuc,al.Descripcion "
                    + " FROM     dbo.aca_AnioLectivo_Curso_Paralelo AS cp LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada ON cp.IdEmpresa = jc.IdEmpresa AND "
                    + " cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Paralelo_Profesor AS pp ON cm.IdMateria = pp.IdMateria ON cp.IdEmpresa = pp.IdEmpresa AND cp.IdAnio = pp.IdAnio AND cp.IdSede = pp.IdSede AND cp.IdNivel = pp.IdNivel AND cp.IdJornada = pp.IdJornada AND "
                    + " cp.IdCurso = pp.IdCurso AND cp.IdParalelo = pp.IdParalelo AND cp.IdEmpresa = cm.IdEmpresa AND cp.IdAnio = cm.IdAnio AND cp.IdSede = cm.IdSede AND cp.IdNivel = cm.IdNivel AND cp.IdJornada = cm.IdJornada AND "
                    + " cp.IdCurso = cm.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_Profesor AS pr ON pr.IdEmpresa = pp.IdEmpresa AND pr.IdProfesor = pp.IdProfesor LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON p.IdPersona = pr.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS al ON al.IdEmpresa = cp.IdEmpresa AND al.IdAnio = cp.IdAnio "
                    + " WHERE cp.IdEmpresa = " + IdEmpresa.ToString()
                    + " and cp.IdAnio = " + IdAnio.ToString()
                    + " and cp.IdSede = " + IdSede.ToString()
                    + " and cp.IdJornada = " + IdJornada.ToString()
                    + " and cp.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and cp.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and cp.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_057_Info
                        {
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesor"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
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
                            NomMateria = string.IsNullOrEmpty(reader["NomMateria"].ToString()) ? null : reader["NomMateria"].ToString(),
                            OrdenMateria = string.IsNullOrEmpty(reader["OrdenMateria"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateria"]),
                            NomMateriaGrupo = string.IsNullOrEmpty(reader["NomMateriaGrupo"].ToString()) ? null : reader["NomMateriaGrupo"].ToString(),
                            OrdenMateriaGrupo = string.IsNullOrEmpty(reader["OrdenMateriaGrupo"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateriaGrupo"])
                        });
                    }
                    reader.Close();
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
