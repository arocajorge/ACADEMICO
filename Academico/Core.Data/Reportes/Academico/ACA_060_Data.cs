using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_060_Data
    {
        public List<ACA_060_Info> GetList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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

                List<ACA_060_Info> Lista = new List<ACA_060_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mp.IdEmpresa, m.IdSede, m.IdAnio, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, cp.NomParalelo, cp.OrdenParalelo, mp.IdCampoAccion, mp.IdTematica, t.NombreCampoAccion, t.NombreTematica, mp.IdProfesor, "
                    + " pe.pe_nombreCompleto AS NombreProfesor, jc.NomCurso, jc.OrdenCurso, an.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada,pe.pe_cedulaRuc, p.Correo "
                    + " FROM     dbo.aca_MatriculaCalificacionParticipacion AS mp WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_Matricula AS m WITH (nolock) ON m.IdEmpresa = mp.IdEmpresa AND m.IdMatricula = mp.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo "
                    + " LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Tematica AS t WITH (nolock) ON mp.IdEmpresa = t.IdEmpresa AND mp.IdCampoAccion = t.IdCampoAccion AND mp.IdTematica = t.IdTematica LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS an WITH (nolock) ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio "
                    + " LEFT OUTER JOIN "
                    + " dbo.aca_Profesor AS p WITH (nolock) ON mp.IdEmpresa = p.IdEmpresa AND mp.IdProfesor = p.IdProfesor LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pe WITH (nolock) ON pe.IdPersona = p.IdPersona "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " GROUP BY mp.IdEmpresa, m.IdSede, m.IdAnio, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, cp.NomParalelo, cp.OrdenParalelo, mp.IdCampoAccion, mp.IdTematica, t.NombreCampoAccion, t.NombreTematica, mp.IdProfesor, pe.pe_nombreCompleto, "
                    + " jc.NomCurso, jc.OrdenCurso, an.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, pe.pe_cedulaRuc, p.Correo ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_060_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdTematica = Convert.ToInt32(reader["IdParalelo"]),
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
                            NombreProfesor = string.IsNullOrEmpty(reader["NombreProfesor"].ToString()) ? null : reader["NombreProfesor"].ToString(),
                            NombreTematica = string.IsNullOrEmpty(reader["NombreTematica"].ToString()) ? null : reader["NombreTematica"].ToString(),
                            NombreCampoAccion = string.IsNullOrEmpty(reader["NombreCampoAccion"].ToString()) ? null : reader["NombreCampoAccion"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString()
                        });
                    }
                    reader.Close();
                }
                var SecuenciaInicial = 1;
                var SecuenciaBasica = 1;
                Lista.ForEach(q=>q.Num = (q.IdNivel==1 ? SecuenciaInicial++ : SecuenciaBasica++));

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
