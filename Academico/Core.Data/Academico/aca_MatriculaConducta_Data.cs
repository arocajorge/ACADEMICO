using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_MatriculaConducta_Data
    {
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta_equivalencia = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_AnioLectivoParcial_Data odata_parcial = new aca_AnioLectivoParcial_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();

        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_MatriculaConducta WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMatricula = " + IdMatricula.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            SecuenciaPromedioP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP1"]),
                            PromedioP1 = string.IsNullOrEmpty(reader["PromedioP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP1"]),
                            SecuenciaPromedioFinalP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP1"]),
                            PromedioFinalP1 = string.IsNullOrEmpty(reader["PromedioFinalP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP1"]),
                            SecuenciaPromedioP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP2"]),
                            PromedioP2 = string.IsNullOrEmpty(reader["PromedioP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP2"]),
                            SecuenciaPromedioFinalP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP2"]),
                            PromedioFinalP2 = string.IsNullOrEmpty(reader["PromedioFinalP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP2"]),
                            SecuenciaPromedioP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP3"]),
                            PromedioP3 = string.IsNullOrEmpty(reader["PromedioP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP3"]),
                            SecuenciaPromedioFinalP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP3"]),
                            PromedioFinalP3 = string.IsNullOrEmpty(reader["PromedioFinalP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP3"]),
                            SecuenciaPromedioP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP4"]),
                            PromedioP4 = string.IsNullOrEmpty(reader["PromedioP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP4"]),
                            SecuenciaPromedioFinalP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP4"]),
                            PromedioFinalP4 = string.IsNullOrEmpty(reader["PromedioFinalP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP4"]),
                            SecuenciaPromedioP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP5"]),
                            PromedioP5 = string.IsNullOrEmpty(reader["PromedioP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP5"]),
                            SecuenciaPromedioFinalP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP5"]),
                            PromedioFinalP5 = string.IsNullOrEmpty(reader["PromedioFinalP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP5"]),
                            SecuenciaPromedioP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP6"]),
                            PromedioP6 = string.IsNullOrEmpty(reader["PromedioP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP6"]),
                            SecuenciaPromedioFinalP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP6"]),
                            PromedioFinalP6 = string.IsNullOrEmpty(reader["PromedioFinalP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP6"]),
                            SecuenciaPromedioQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ1"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            SecuenciaPromedioFinalQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ1"]),
                            PromedioFinalQ1 = string.IsNullOrEmpty(reader["PromedioFinalQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ1"]),
                            SecuenciaPromedioQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ2"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ2"]),
                            SecuenciaPromedioFinalQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ2"]),
                            PromedioFinalQ2 = string.IsNullOrEmpty(reader["PromedioFinalQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ2"]),
                            SecuenciaPromedioGeneral = string.IsNullOrEmpty(reader["SecuenciaPromedioGeneral"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioGeneral"]),
                            PromedioGeneral = string.IsNullOrEmpty(reader["PromedioGeneral"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioGeneral"]),
                            SecuenciaPromedioFinal = string.IsNullOrEmpty(reader["SecuenciaPromedioFinal"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinal"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinal"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaConducta.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            SecuenciaPromedioP1 = q.SecuenciaPromedioP1,
                            PromedioP1 = q.PromedioP1,
                            SecuenciaPromedioFinalP1 = q.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = q.PromedioFinalP1,
                            SecuenciaPromedioP2 = q.SecuenciaPromedioP2,
                            PromedioP2 = q.PromedioP2,
                            SecuenciaPromedioFinalP2 = q.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = q.PromedioFinalP2,
                            SecuenciaPromedioP3 = q.SecuenciaPromedioP3,
                            PromedioP3 = q.PromedioP3,
                            SecuenciaPromedioFinalP3 = q.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = q.PromedioFinalP3,
                            SecuenciaPromedioP4 = q.SecuenciaPromedioP4,
                            PromedioP4 = q.PromedioP4,
                            SecuenciaPromedioFinalP4 = q.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = q.PromedioFinalP4,
                            SecuenciaPromedioP5 = q.SecuenciaPromedioP5,
                            PromedioP5 = q.PromedioP5,
                            SecuenciaPromedioFinalP5 = q.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = q.PromedioFinalP5,
                            SecuenciaPromedioP6 = q.SecuenciaPromedioP6,
                            PromedioP6 = q.PromedioP6,
                            SecuenciaPromedioFinalP6 = q.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = q.PromedioFinalP6,
                            SecuenciaPromedioQ1 = q.SecuenciaPromedioQ1,
                            PromedioQ1 = q.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = q.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = q.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = q.SecuenciaPromedioQ2,
                            PromedioQ2 = q.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = q.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = q.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = q.SecuenciaPromedioGeneral,
                            PromedioGeneral = q.PromedioGeneral,
                            SecuenciaPromedioFinal = q.SecuenciaPromedioFinal,
                            PromedioFinal = q.PromedioFinal
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public aca_MatriculaConducta_Info getInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaConducta_Info info = new aca_MatriculaConducta_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_MatriculaConducta WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMatricula = " + IdMatricula.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            SecuenciaPromedioP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP1"]),
                            PromedioP1 = string.IsNullOrEmpty(reader["PromedioP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP1"]),
                            SecuenciaPromedioFinalP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP1"]),
                            PromedioFinalP1 = string.IsNullOrEmpty(reader["PromedioFinalP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP1"]),
                            SecuenciaPromedioP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP2"]),
                            PromedioP2 = string.IsNullOrEmpty(reader["PromedioP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP2"]),
                            SecuenciaPromedioFinalP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP2"]),
                            PromedioFinalP2 = string.IsNullOrEmpty(reader["PromedioFinalP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP2"]),
                            SecuenciaPromedioP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP3"]),
                            PromedioP3 = string.IsNullOrEmpty(reader["PromedioP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP3"]),
                            SecuenciaPromedioFinalP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP3"]),
                            PromedioFinalP3 = string.IsNullOrEmpty(reader["PromedioFinalP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP3"]),
                            SecuenciaPromedioP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP4"]),
                            PromedioP4 = string.IsNullOrEmpty(reader["PromedioP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP4"]),
                            SecuenciaPromedioFinalP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP4"]),
                            PromedioFinalP4 = string.IsNullOrEmpty(reader["PromedioFinalP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP4"]),
                            SecuenciaPromedioP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP5"]),
                            PromedioP5 = string.IsNullOrEmpty(reader["PromedioP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP5"]),
                            SecuenciaPromedioFinalP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP5"]),
                            PromedioFinalP5 = string.IsNullOrEmpty(reader["PromedioFinalP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP5"]),
                            SecuenciaPromedioP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP6"]),
                            PromedioP6 = string.IsNullOrEmpty(reader["PromedioP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP6"]),
                            SecuenciaPromedioFinalP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP6"]),
                            PromedioFinalP6 = string.IsNullOrEmpty(reader["PromedioFinalP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP6"]),
                            SecuenciaPromedioQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ1"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            SecuenciaPromedioFinalQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ1"]),
                            PromedioFinalQ1 = string.IsNullOrEmpty(reader["PromedioFinalQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ1"]),
                            SecuenciaPromedioQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ2"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ2"]),
                            SecuenciaPromedioFinalQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ2"]),
                            PromedioFinalQ2 = string.IsNullOrEmpty(reader["PromedioFinalQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ2"]),
                            SecuenciaPromedioGeneral = string.IsNullOrEmpty(reader["SecuenciaPromedioGeneral"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioGeneral"]),
                            PromedioGeneral = string.IsNullOrEmpty(reader["PromedioGeneral"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioGeneral"]),
                            SecuenciaPromedioFinal = string.IsNullOrEmpty(reader["SecuenciaPromedioFinal"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinal"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinal"])
                        };
                    }
                }
                
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_MatriculaConducta_Info> getList_PaseAnio(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, p.pe_nombreCompleto, mc.SecuenciaPromedioP1, mc.PromedioP1, mc.SecuenciaPromedioFinalP1, mc.PromedioFinalP1, "
                    + " mc.SecuenciaPromedioP2, mc.PromedioP2, mc.SecuenciaPromedioFinalP2, mc.PromedioFinalP2, mc.SecuenciaPromedioP3, mc.PromedioP3, mc.SecuenciaPromedioFinalP3, mc.PromedioFinalP3, mc.SecuenciaPromedioQ1, "
                    + " mc.PromedioQ1, mc.SecuenciaPromedioFinalQ1, mc.PromedioFinalQ1, mc.SecuenciaPromedioP4, mc.PromedioP4, mc.SecuenciaPromedioFinalP4, mc.PromedioFinalP4, mc.SecuenciaPromedioP5, mc.PromedioP5, "
                    + " mc.SecuenciaPromedioFinalP5, mc.PromedioFinalP5, mc.SecuenciaPromedioP6, mc.PromedioP6, mc.SecuenciaPromedioFinalP6, mc.PromedioFinalP6, mc.SecuenciaPromedioQ2, mc.PromedioQ2, mc.SecuenciaPromedioFinalQ2, "
                    + " mc.PromedioFinalQ2, mc.SecuenciaPromedioGeneral, mc.PromedioGeneral, mc.SecuenciaPromedioFinal, mc.PromedioFinal, mc.MotivoPromedioFinalP1, mc.MotivoPromedioFinalP2, mc.MotivoPromedioFinalP3, mc.MotivoPromedioFinalQ1, "
                    + " mc.MotivoPromedioFinalP4, mc.MotivoPromedioFinalP5, mc.MotivoPromedioFinalP6, mc.MotivoPromedioFinalQ2, mc.MotivoPromedioFinal, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo "
                    + " FROM dbo.aca_Matricula AS m WITH (nolock) INNER JOIN "
                    + " dbo.aca_MatriculaConducta AS mc WITH (nolock) ON m.IdEmpresa = mc.IdEmpresa AND m.IdMatricula = mc.IdMatricula INNER JOIN "
                    + " dbo.tb_persona AS p WITH (nolock) INNER JOIN "
                    + " dbo.aca_Alumno AS a WITH (nolock) ON p.IdPersona = a.IdPersona ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f WITH (nolock) "
                    + " WHERE(IdEmpresa = mc.IdEmpresa) AND(IdMatricula = mc.IdMatricula) AND(Estado = 1))) "
                    + " AND m.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdSede = " + IdSede.ToString() + " and m.IdAnio = " + IdAnio.ToString()
                    + " AND m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " AND m.IdJornada between " + IdJornadaIni.ToString() + " and " + IdJornadaFin.ToString()
                    + " AND m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " AND m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " AND m.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToInt32(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            SecuenciaPromedioP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP1"]),
                            PromedioP1 = string.IsNullOrEmpty(reader["PromedioP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP1"]),
                            SecuenciaPromedioFinalP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP1"]),
                            PromedioFinalP1 = string.IsNullOrEmpty(reader["PromedioFinalP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP1"]),
                            SecuenciaPromedioP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP2"]),
                            PromedioP2 = string.IsNullOrEmpty(reader["PromedioP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP2"]),
                            SecuenciaPromedioFinalP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP2"]),
                            PromedioFinalP2 = string.IsNullOrEmpty(reader["PromedioFinalP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP2"]),
                            SecuenciaPromedioP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP3"]),
                            PromedioP3 = string.IsNullOrEmpty(reader["PromedioP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP3"]),
                            SecuenciaPromedioFinalP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP3"]),
                            PromedioFinalP3 = string.IsNullOrEmpty(reader["PromedioFinalP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP3"]),
                            SecuenciaPromedioP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP4"]),
                            PromedioP4 = string.IsNullOrEmpty(reader["PromedioP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP4"]),
                            SecuenciaPromedioFinalP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP4"]),
                            PromedioFinalP4 = string.IsNullOrEmpty(reader["PromedioFinalP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP4"]),
                            SecuenciaPromedioP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP5"]),
                            PromedioP5 = string.IsNullOrEmpty(reader["PromedioP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP5"]),
                            SecuenciaPromedioFinalP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP5"]),
                            PromedioFinalP5 = string.IsNullOrEmpty(reader["PromedioFinalP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP5"]),
                            SecuenciaPromedioP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP6"]),
                            PromedioP6 = string.IsNullOrEmpty(reader["PromedioP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP6"]),
                            SecuenciaPromedioFinalP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP6"]),
                            PromedioFinalP6 = string.IsNullOrEmpty(reader["PromedioFinalP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP6"]),
                            SecuenciaPromedioQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ1"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            SecuenciaPromedioFinalQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ1"]),
                            PromedioFinalQ1 = string.IsNullOrEmpty(reader["PromedioFinalQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ1"]),
                            SecuenciaPromedioQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ2"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ2"]),
                            SecuenciaPromedioFinalQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ2"]),
                            PromedioFinalQ2 = string.IsNullOrEmpty(reader["PromedioFinalQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ2"]),
                            SecuenciaPromedioGeneral = string.IsNullOrEmpty(reader["SecuenciaPromedioGeneral"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioGeneral"]),
                            PromedioGeneral = string.IsNullOrEmpty(reader["PromedioGeneral"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioGeneral"]),
                            SecuenciaPromedioFinal = string.IsNullOrEmpty(reader["SecuenciaPromedioFinal"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinal"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinal"])
                        });
                    }
                    reader.Close();
                }
                
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaConducta_Info> getList_Combos(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
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

                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, "
                    + " cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector "
                    + " FROM     dbo.aca_MatriculaConducta AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS m WITH (nolock) ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON m.IdAnio = a.IdAnio AND m.IdEmpresa = a.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel "
                    + " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdSede = " + IdSede.ToString() + " and m.IdAnio = " + IdAnio.ToString()
                    + " AND m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " AND m.IdJornada between " + IdJornadaIni.ToString() + " and " + IdJornadaFin.ToString()
                    + " AND m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " AND m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " GROUP BY mc.IdEmpresa, mc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, "
                    + " cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToInt32(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            IdProfesorTutor = Convert.ToInt32(reader["IdProfesorTutor"]),
                            IdProfesorInspector = Convert.ToInt32(reader["IdProfesorInspector"])
                        });
                    }
                    reader.Close();
                }
               
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaConducta_Info> getList_Combos_Inspector(int IdEmpresa, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.EsObligatorio, cm.OrdenMateria "
                    + " FROM     dbo.aca_MatriculaCalificacion AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS c WITH (nolock) ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm WITH (nolock) ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND "
                    + " mc.IdMateria = cm.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso AND c.IdParalelo = cp.IdParalelo ";
                    if (EsSuperAdmin == false)
                    {
                        query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and cp.IdProfesorInspector = " + IdProfesor.ToString();
                    }
                    else
                    {
                        query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString();
                    }
                    query += " GROUP BY mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.OrdenMateria, cm.EsObligatorio ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            IdProfesorInspector = string.IsNullOrEmpty(reader["IdProfesorInspector"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesorInspector"])
                        });
                    }
                    reader.Close();
                }
                
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<aca_MatriculaConducta_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, m.IdAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, p.pe_nombreCompleto, mc.SecuenciaPromedioP1, mc.PromedioP1, mc.SecuenciaPromedioFinalP1, mc.PromedioFinalP1, "
                    + " mc.SecuenciaPromedioP2, mc.PromedioP2, mc.SecuenciaPromedioFinalP2, mc.PromedioFinalP2, mc.SecuenciaPromedioP3, mc.PromedioP3, mc.SecuenciaPromedioFinalP3, mc.PromedioFinalP3, mc.SecuenciaPromedioQ1, "
                    + " mc.PromedioQ1, mc.SecuenciaPromedioFinalQ1, mc.PromedioFinalQ1, mc.SecuenciaPromedioP4, mc.PromedioP4, mc.SecuenciaPromedioFinalP4, mc.PromedioFinalP4, mc.SecuenciaPromedioP5, mc.PromedioP5, "
                    + " mc.SecuenciaPromedioFinalP5, mc.PromedioFinalP5, mc.SecuenciaPromedioP6, mc.PromedioP6, mc.SecuenciaPromedioFinalP6, mc.PromedioFinalP6, mc.SecuenciaPromedioQ2, mc.PromedioQ2, mc.SecuenciaPromedioFinalQ2, "
                    + " mc.PromedioFinalQ2, mc.SecuenciaPromedioGeneral, mc.PromedioGeneral, mc.SecuenciaPromedioFinal, mc.PromedioFinal, mc.MotivoPromedioFinalP1, mc.MotivoPromedioFinalP2, mc.MotivoPromedioFinalP3, mc.MotivoPromedioFinalQ1, "
                    + " mc.MotivoPromedioFinalP4, mc.MotivoPromedioFinalP5, mc.MotivoPromedioFinalP6, mc.MotivoPromedioFinalQ2, mc.MotivoPromedioFinal, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo "
                    + " FROM dbo.aca_Matricula AS m WITH (nolock) INNER JOIN "
                    + " dbo.aca_MatriculaConducta AS mc WITH (nolock) ON m.IdEmpresa = mc.IdEmpresa AND m.IdMatricula = mc.IdMatricula INNER JOIN "
                    + " dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Alumno AS a WITH (nolock) ON p.IdPersona = a.IdPersona ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f "
                    + " WHERE(IdEmpresa = mc.IdEmpresa) AND(IdMatricula = mc.IdMatricula) AND(Estado = 1))) "
                    + " AND m.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdSede = " + IdSede.ToString() + " and m.IdAnio = " + IdAnio.ToString()
                    + " AND m.IdNivel = " + IdNivel.ToString()
                    + " AND m.IdJornada = " + IdJornada.ToString()
                    + " AND m.IdCurso = " + IdCurso.ToString()
                    + " AND m.IdParalelo = " + IdParalelo.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaConducta_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToInt32(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            SecuenciaPromedioP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP1"]),
                            PromedioP1 = string.IsNullOrEmpty(reader["PromedioP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP1"]),
                            SecuenciaPromedioFinalP1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP1"]),
                            PromedioFinalP1 = string.IsNullOrEmpty(reader["PromedioFinalP1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP1"]),
                            SecuenciaPromedioP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP2"]),
                            PromedioP2 = string.IsNullOrEmpty(reader["PromedioP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP2"]),
                            SecuenciaPromedioFinalP2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP2"]),
                            PromedioFinalP2 = string.IsNullOrEmpty(reader["PromedioFinalP2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP2"]),
                            SecuenciaPromedioP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP3"]),
                            PromedioP3 = string.IsNullOrEmpty(reader["PromedioP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP3"]),
                            SecuenciaPromedioFinalP3 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP3"]),
                            PromedioFinalP3 = string.IsNullOrEmpty(reader["PromedioFinalP3"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP3"]),
                            SecuenciaPromedioP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP4"]),
                            PromedioP4 = string.IsNullOrEmpty(reader["PromedioP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP4"]),
                            SecuenciaPromedioFinalP4 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP4"]),
                            PromedioFinalP4 = string.IsNullOrEmpty(reader["PromedioFinalP4"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP4"]),
                            SecuenciaPromedioP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP5"]),
                            PromedioP5 = string.IsNullOrEmpty(reader["PromedioP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP5"]),
                            SecuenciaPromedioFinalP5 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP5"]),
                            PromedioFinalP5 = string.IsNullOrEmpty(reader["PromedioFinalP5"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP5"]),
                            SecuenciaPromedioP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioP6"]),
                            PromedioP6 = string.IsNullOrEmpty(reader["PromedioP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioP6"]),
                            SecuenciaPromedioFinalP6 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalP6"]),
                            PromedioFinalP6 = string.IsNullOrEmpty(reader["PromedioFinalP6"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalP6"]),
                            SecuenciaPromedioQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ1"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            SecuenciaPromedioFinalQ1 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ1"]),
                            PromedioFinalQ1 = string.IsNullOrEmpty(reader["PromedioFinalQ1"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ1"]),
                            SecuenciaPromedioQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioQ2"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioQ2"]),
                            SecuenciaPromedioFinalQ2 = string.IsNullOrEmpty(reader["SecuenciaPromedioFinalQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinalQ2"]),
                            PromedioFinalQ2 = string.IsNullOrEmpty(reader["PromedioFinalQ2"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinalQ2"]),
                            SecuenciaPromedioGeneral = string.IsNullOrEmpty(reader["SecuenciaPromedioGeneral"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioGeneral"]),
                            PromedioGeneral = string.IsNullOrEmpty(reader["PromedioGeneral"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioGeneral"]),
                            SecuenciaPromedioFinal = string.IsNullOrEmpty(reader["SecuenciaPromedioFinal"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioFinal"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (double?)null : Convert.ToInt32(reader["PromedioFinal"]),
                            MotivoPromedioFinalP1 = string.IsNullOrEmpty(reader["MotivoPromedioFinalP1"].ToString()) ? null : reader["MotivoPromedioFinalP1"].ToString(),
                            MotivoPromedioFinalP2 = string.IsNullOrEmpty(reader["MotivoPromedioFinalP2"].ToString()) ? null : reader["MotivoPromedioFinalP2"].ToString(),
                            MotivoPromedioFinalP3 = string.IsNullOrEmpty(reader["MotivoPromedioFinalP3"].ToString()) ? null : reader["MotivoPromedioFinalP3"].ToString(),
                            MotivoPromedioFinalQ1 = string.IsNullOrEmpty(reader["MotivoPromedioFinalQ1"].ToString()) ? null : reader["MotivoPromedioFinalQ1"].ToString(),
                            MotivoPromedioFinalP4 = string.IsNullOrEmpty(reader["MotivoPromedioFinalP4"].ToString()) ? null : reader["MotivoPromedioFinalP4"].ToString(),
                            MotivoPromedioFinalP5 = string.IsNullOrEmpty(reader["MotivoPromedioFinalP5"].ToString()) ? null : reader["MotivoPromedioFinalP5"].ToString(),
                            MotivoPromedioFinalP6 = string.IsNullOrEmpty(reader["MotivoPromedioFinalP6"].ToString()) ? null : reader["MotivoPromedioFinalP6"].ToString(),
                            MotivoPromedioFinalQ2 = string.IsNullOrEmpty(reader["MotivoPromedioFinalQ2"].ToString()) ? null : reader["MotivoPromedioFinalQ2"].ToString(),
                            MotivoPromedioFinal = string.IsNullOrEmpty(reader["MotivoPromedioFinal"].ToString()) ? null : reader["MotivoPromedioFinal"].ToString(),
                            ValidoImportacion = true
                        });
                    }
                    reader.Close();
                }
                
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool generarCalificacion(List<aca_MatriculaConducta_Info> lst_conducta)
        {
            try
            {
                List<aca_MatriculaConducta_Info> Lista = new List<aca_MatriculaConducta_Info>();
                
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var info in lst_conducta)
                    {
                        var lista_calificacion_conducta = Context.aca_MatriculaConducta.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                        Context.aca_MatriculaConducta.RemoveRange(lista_calificacion_conducta);

                        aca_MatriculaConducta Entity = new aca_MatriculaConducta
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdMatricula = info.IdMatricula,
                            SecuenciaPromedioP1 = info.SecuenciaPromedioP1,
                            PromedioP1 = info.PromedioP1,
                            SecuenciaPromedioFinalP1 = info.SecuenciaPromedioFinalP1,
                            PromedioFinalP1 = info.PromedioFinalP1,
                            SecuenciaPromedioP2 = info.SecuenciaPromedioP2,
                            PromedioP2 = info.PromedioP2,
                            SecuenciaPromedioFinalP2 = info.SecuenciaPromedioFinalP2,
                            PromedioFinalP2 = info.PromedioFinalP2,
                            SecuenciaPromedioP3 = info.SecuenciaPromedioP3,
                            PromedioP3 = info.PromedioP3,
                            SecuenciaPromedioFinalP3 = info.SecuenciaPromedioFinalP3,
                            PromedioFinalP3 = info.PromedioFinalP3,
                            SecuenciaPromedioP4 = info.SecuenciaPromedioP4,
                            PromedioP4 = info.PromedioP4,
                            SecuenciaPromedioFinalP4 = info.SecuenciaPromedioFinalP4,
                            PromedioFinalP4 = info.PromedioFinalP4,
                            SecuenciaPromedioP5 = info.SecuenciaPromedioP5,
                            PromedioP5 = info.PromedioP5,
                            SecuenciaPromedioFinalP5 = info.SecuenciaPromedioFinalP5,
                            PromedioFinalP5 = info.PromedioFinalP5,
                            SecuenciaPromedioP6 = info.SecuenciaPromedioP6,
                            PromedioP6 = info.PromedioP6,
                            SecuenciaPromedioFinalP6 = info.SecuenciaPromedioFinalP6,
                            PromedioFinalP6 = info.PromedioFinalP6,
                            SecuenciaPromedioQ1 = info.SecuenciaPromedioQ1,
                            PromedioQ1 = info.PromedioQ1,
                            SecuenciaPromedioFinalQ1 = info.SecuenciaPromedioFinalQ1,
                            PromedioFinalQ1 = info.PromedioFinalQ1,
                            SecuenciaPromedioQ2 = info.SecuenciaPromedioQ2,
                            PromedioQ2 = info.PromedioQ2,
                            SecuenciaPromedioFinalQ2 = info.SecuenciaPromedioFinalQ2,
                            PromedioFinalQ2 = info.PromedioFinalQ2,
                            SecuenciaPromedioGeneral = info.SecuenciaPromedioGeneral,
                            PromedioGeneral = info.PromedioGeneral,
                            SecuenciaPromedioFinal = info.SecuenciaPromedioFinal,
                            PromedioFinal = info.PromedioFinal
                        };

                        Context.aca_MatriculaConducta.Add(Entity);
                        Context.SaveChanges();
                    }
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool modicarPromedioFinal(aca_MatriculaConducta_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa  && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        EntityConducta.SecuenciaPromedioFinalP1 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP1 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP1 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        EntityConducta.SecuenciaPromedioFinalP2 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP2 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP2 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        EntityConducta.SecuenciaPromedioFinalP3 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP3 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP3 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        EntityConducta.SecuenciaPromedioFinalP4 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP4 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP4 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        EntityConducta.SecuenciaPromedioFinalP5 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP5 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP5 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        EntityConducta.SecuenciaPromedioFinalP6 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalP6 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalP6 = info.MotivoPromedioParcialFinal;
                    }

                    Context.SaveChanges();

                    aca_MatriculaConducta EntityConductaPromedioQuim = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConductaPromedioQuim == null)
                        return false;

                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    var info_conducta = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(info.ConductaPromedioParcialFinal));
                    var infoConductaMinima = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    var SecuenciaConductaMinima = infoConductaMinima.Secuencia;

                    double SumaPromedioQuimestre = 0;
                    double PromedioQuimestre = 0;
                    var SecuenciaConducta = (int?)null;
                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        var lst_pacial_quim1 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1));

                        SumaPromedioQuimestre = Convert.ToDouble((EntityConductaPromedioQuim.PromedioFinalP1>0 ? EntityConductaPromedioQuim.PromedioFinalP1 : EntityConductaPromedioQuim.PromedioP1) + (EntityConductaPromedioQuim.PromedioFinalP2 > 0 ? EntityConductaPromedioQuim.PromedioFinalP2 : EntityConductaPromedioQuim.PromedioP2) + (EntityConductaPromedioQuim.PromedioFinalP3 > 0 ? EntityConductaPromedioQuim.PromedioFinalP3 : EntityConductaPromedioQuim.PromedioP3));
                        PromedioQuimestre = Convert.ToDouble(SumaPromedioQuimestre / lst_pacial_quim1.Count());
                        var info_conductaQ1 = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioQuimestre));
                        var infoMinimaConductaQ1 = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                        SecuenciaConducta = infoMinimaConductaQ1.Secuencia;

                        EntityConductaPromedioQuim.PromedioQ1 = PromedioQuimestre;
                        EntityConductaPromedioQuim.SecuenciaPromedioQ1 = info_conductaQ1 == null ? SecuenciaConducta : info_conductaQ1.Secuencia;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        var lst_pacial_quim2 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2));

                        SumaPromedioQuimestre = Convert.ToDouble((EntityConductaPromedioQuim.PromedioFinalP4 > 0 ? EntityConductaPromedioQuim.PromedioFinalP4 : EntityConductaPromedioQuim.PromedioP4) + (EntityConductaPromedioQuim.PromedioFinalP5 > 0 ? EntityConductaPromedioQuim.PromedioFinalP5 : EntityConductaPromedioQuim.PromedioP5) + (EntityConductaPromedioQuim.PromedioFinalP6 > 0 ? EntityConductaPromedioQuim.PromedioFinalP6 : EntityConductaPromedioQuim.PromedioP6));
                        PromedioQuimestre = Convert.ToDouble(SumaPromedioQuimestre / lst_pacial_quim2.Count());
                        var info_conductaQ2 = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioQuimestre));
                        var infoMinimaConductaQ2 = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                        SecuenciaConducta = infoMinimaConductaQ2.Secuencia;

                        EntityConductaPromedioQuim.PromedioQ2 = PromedioQuimestre;
                        EntityConductaPromedioQuim.SecuenciaPromedioQ2 = info_conductaQ2 == null ? SecuenciaConducta : info_conductaQ2.Secuencia;
                    }

                    double SumaPromedioGeneral = 0;
                    double PromedioGeneral = 0;
                    var SecuenciaConductaGeneral = (int?)null;
                    SumaPromedioGeneral = Convert.ToDouble((EntityConductaPromedioQuim.PromedioFinalQ1 > 0 ? EntityConductaPromedioQuim.PromedioFinalQ1 : EntityConductaPromedioQuim.PromedioQ1) + (EntityConductaPromedioQuim.PromedioFinalQ2 > 0 ? EntityConductaPromedioQuim.PromedioFinalQ2 : EntityConductaPromedioQuim.PromedioQ2));
                    PromedioGeneral = Convert.ToDouble(SumaPromedioGeneral / 2);
                    var info_conducta_general = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioGeneral));
                    var infoMinimaConductaGeneral = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    SecuenciaConductaGeneral = infoMinimaConductaGeneral.Secuencia;

                    EntityConductaPromedioQuim.PromedioGeneral = PromedioGeneral;
                    EntityConductaPromedioQuim.SecuenciaPromedioGeneral = info_conducta_general == null ? SecuenciaConductaGeneral : info_conducta_general.Secuencia;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool modicarPromedioFinalQuimestre(aca_MatriculaConducta_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    if (info.IdPromedioFinal == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_1.ToString())
                    {
                        EntityConducta.SecuenciaPromedioFinalQ1 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalQ1 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalQ1 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdPromedioFinal == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_2.ToString())
                    {
                        EntityConducta.SecuenciaPromedioFinalQ2 = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinalQ2 = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinalQ2 = info.MotivoPromedioParcialFinal;
                    }

                    if (info.IdPromedioFinal == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.PROMEDIOFINAL.ToString())
                    {
                        EntityConducta.SecuenciaPromedioFinal = info.SecuenciaConductaPromedioParcialFinal;
                        EntityConducta.PromedioFinal = info.ConductaPromedioParcialFinal;
                        EntityConducta.MotivoPromedioFinal = info.MotivoPromedioParcialFinal;
                    }
                    Context.SaveChanges();

                    double SumaPromedioGeneral = 0;
                    double PromedioGeneral = 0;
                    var SecuenciaConductaGeneral = (int?)null;
                    SumaPromedioGeneral = Convert.ToDouble((EntityConducta.PromedioFinalQ1 > 0 ? EntityConducta.PromedioFinalQ1 : EntityConducta.PromedioQ1) + (EntityConducta.PromedioFinalQ2 > 0 ? EntityConducta.PromedioFinalQ2 : EntityConducta.PromedioQ2));
                    PromedioGeneral = Convert.ToDouble(SumaPromedioGeneral / 2);
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);
                    var info_conducta_general = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioGeneral));
                    var infoMinimaConductaGeneral = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    SecuenciaConductaGeneral = infoMinimaConductaGeneral.Secuencia;

                    EntityConducta.PromedioGeneral = PromedioGeneral;
                    EntityConducta.SecuenciaPromedioGeneral = info_conducta_general == null ? SecuenciaConductaGeneral : info_conducta_general.Secuencia;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool modicarPromedioPaseAnio(aca_MatriculaConducta_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaConducta EntityConducta = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConducta == null)
                        return false;

                    EntityConducta.SecuenciaPromedioGeneral = info.SecuenciaPromedioGeneral;
                    EntityConducta.PromedioGeneral = info.PromedioGeneral;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
