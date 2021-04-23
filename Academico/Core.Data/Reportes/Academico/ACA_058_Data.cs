using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_058_Data
    {
        public List<ACA_058_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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

                List<ACA_058_Info> Lista = new List<ACA_058_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT cp.IdEmpresa, cp.IdAnio, cp.IdSede, cp.IdNivel, cp.IdJornada, cp.IdCurso, cp.IdParalelo, cp.NomParalelo, cp.OrdenParalelo, jc.NomCurso, jc.OrdenCurso, nj.NomJornada, nj.OrdenJornada, sn.NomSede, sn.NomNivel, sn.OrdenNivel, "
                    + " al.Descripcion, ptu.pe_nombreCompleto AS Tutor, pin.pe_nombreCompleto AS Inspector, cp.IdProfesorTutor, cp.IdProfesorInspector "
                    + " FROM     dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND "
                    + " jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS al WITH (nolock) ON al.IdEmpresa = cp.IdEmpresa AND al.IdAnio = cp.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_Profesor AS pt WITH (nolock) ON cp.IdEmpresa = pt.IdEmpresa AND pt.IdProfesor = cp.IdProfesorTutor LEFT OUTER JOIN "
                    + " dbo.tb_persona AS ptu WITH (nolock) ON ptu.IdPersona = pt.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_Profesor AS pi WITH (nolock) ON cp.IdEmpresa = pi.IdEmpresa AND pi.IdProfesor = cp.IdProfesorInspector LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pin WITH (nolock) ON pin.IdPersona = pi.IdPersona "
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
                        Lista.Add(new ACA_058_Info
                        {
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            IdProfesorInspector = string.IsNullOrEmpty(reader["IdProfesorInspector"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesorInspector"]),
                            Tutor = string.IsNullOrEmpty(reader["Tutor"].ToString()) ? null : reader["Tutor"].ToString(),
                            Inspector = string.IsNullOrEmpty(reader["Inspector"].ToString()) ? null : reader["Inspector"].ToString(),
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
