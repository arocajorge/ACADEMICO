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
    public class aca_MatriculaCalificacionCualitativa_Data
    {
        aca_AnioLectivoParcial_Data odata_parcial = new aca_AnioLectivoParcial_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();
        aca_AnioLectivoCalificacionCualitativa_Data odata_equivalencia = new aca_AnioLectivoCalificacionCualitativa_Data();
        public List<aca_MatriculaCalificacionCualitativa_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_MatriculaCalificacionCualitativa WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMatricula = " + IdMatricula.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdCalificacionCualitativa = string.IsNullOrEmpty(reader["IdCalificacionCualitativa"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativa"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString()
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

        public aca_MatriculaCalificacionCualitativa_Info getInfo_X_Matricula(int IdEmpresa, decimal IdMatricula, decimal IdMateria, int IdCatalogoParcial)
        {
            try
            {
                aca_MatriculaCalificacionCualitativa_Info info = new aca_MatriculaCalificacionCualitativa_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_MatriculaCalificacionCualitativa WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMatricula = " + IdMatricula.ToString() + " and IdMateria = " + IdMateria.ToString()
                    + " and IdCatalogoParcial = " + IdCatalogoParcial.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            IdCalificacionCualitativa = string.IsNullOrEmpty(reader["IdCalificacionCualitativa"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativa"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString()
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

        public List<aca_MatriculaCalificacionCualitativa_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial, decimal IdProfesor)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT cal.IdEmpresa, cal.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, cal.IdMateria, cal.IdCatalogoParcial, cal.IdProfesor, m.IdAlumno, al.Codigo, p.pe_nombreCompleto, cal.IdCalificacionCualitativa, "
                    + " cal.Conducta, cal.MotivoConducta, alc.Codigo AS CodigoCalificacion, alc.DescripcionCorta, aco.Letra "
                    + " FROM     dbo.aca_MatriculaCalificacionCualitativa AS cal WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS m WITH (nolock) ON cal.IdEmpresa = m.IdEmpresa AND cal.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS al WITH (nolock) ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p WITH (nolock) ON al.IdPersona = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS aco WITH (nolock) ON m.IdAnio = aco.IdAnio AND cal.IdEmpresa = aco.IdEmpresa AND cal.Conducta = aco.Secuencia LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoCalificacionCualitativa AS alc WITH (nolock) ON m.IdAnio = alc.IdAnio AND cal.IdEmpresa = alc.IdEmpresa AND cal.IdCalificacionCualitativa = alc.IdCalificacionCualitativa LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS aca_AnioLectivoConductaEquivalencia_1 WITH (nolock) ON m.IdAnio = aca_AnioLectivoConductaEquivalencia_1.IdAnio AND cal.IdEmpresa = aca_AnioLectivoConductaEquivalencia_1.IdEmpresa AND "
                    + " cal.Conducta = aca_AnioLectivoConductaEquivalencia_1.Secuencia "
                    + " WHERE cal.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString() + " and m.IdNivel = " + IdNivel.ToString()
                    + " and m.IdCurso = " + IdCurso.ToString() + " and m.IdParalelo = " + IdParalelo.ToString()
                    + " and cal.IdMateria = " + IdMateria.ToString() + " and cal.IdCatalogoParcial = " + IdCatalogoParcial.ToString()
                    + " and cal.IdProfesor = " + IdProfesor.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            IdCalificacionCualitativa = string.IsNullOrEmpty(reader["IdCalificacionCualitativa"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativa"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString(),
                            Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : reader["Letra"].ToString(),
                            CodigoCalificacion = string.IsNullOrEmpty(reader["CodigoCalificacion"].ToString()) ? null : reader["CodigoCalificacion"].ToString(),
                            DescripcionCorta = string.IsNullOrEmpty(reader["DescripcionCorta"].ToString()) ? null : reader["DescripcionCorta"].ToString(),
                            RegistroValido = true,
                            RegistroValidoCalificacion = true,
                            RegistroValidoConducta = true,
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

        public List<aca_MatriculaCalificacionCualitativa_Info> GetList_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT cal.IdEmpresa, cal.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, cal.IdMateria, cal.IdCatalogoParcial, cal.IdProfesor, m.IdAlumno, al.Codigo, p.pe_nombreCompleto, cal.IdCalificacionCualitativa, "
                + " cal.Conducta, cal.MotivoConducta, alc.Codigo AS CodigoCalificacion, alc.DescripcionCorta, aco.Letra "
                + " FROM     dbo.aca_MatriculaCalificacionCualitativa AS cal WITH (nolock) INNER JOIN "
                + " dbo.aca_Matricula AS m WITH (nolock) ON cal.IdEmpresa = m.IdEmpresa AND cal.IdMatricula = m.IdMatricula INNER JOIN "
                + " dbo.aca_Alumno AS al WITH (nolock) ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno INNER JOIN "
                + " dbo.tb_persona AS p WITH (nolock) ON al.IdPersona = p.IdPersona LEFT OUTER JOIN "
                + " dbo.aca_AnioLectivoConductaEquivalencia AS aco WITH (nolock) ON m.IdAnio = aco.IdAnio AND cal.IdEmpresa = aco.IdEmpresa AND cal.Conducta = aco.Secuencia LEFT OUTER JOIN "
                + " dbo.aca_AnioLectivoCalificacionCualitativa AS alc WITH (nolock) ON m.IdAnio = alc.IdAnio AND cal.IdEmpresa = alc.IdEmpresa AND cal.IdCalificacionCualitativa = alc.IdCalificacionCualitativa LEFT OUTER JOIN "
                + " dbo.aca_AnioLectivoConductaEquivalencia AS aca_AnioLectivoConductaEquivalencia_1 WITH (nolock) ON m.IdAnio = aca_AnioLectivoConductaEquivalencia_1.IdAnio AND cal.IdEmpresa = aca_AnioLectivoConductaEquivalencia_1.IdEmpresa AND "
                + " cal.Conducta = aca_AnioLectivoConductaEquivalencia_1.Secuencia "
                + " WHERE cal.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdSede = " + IdSede.ToString()
                + " and m.IdAnio = " + IdAnio.ToString() + " and m.IdNivel = " + IdNivel.ToString()
                + " and m.IdCurso = " + IdCurso.ToString() + " and m.IdParalelo = " + IdParalelo.ToString()
                + " and cal.IdMateria = " + IdMateria.ToString() + " and cal.IdCatalogoParcial = " + IdCatalogoParcial.ToString();
                #endregion

                SqlCommand command = new SqlCommand(query, connection);
                command.CommandTimeout = 0;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Lista.Add(new aca_MatriculaCalificacionCualitativa_Info
                    {
                        IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                        IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                        IdAnio = Convert.ToInt32(reader["IdAnio"]),
                        IdSede = Convert.ToInt32(reader["IdSede"]),
                        IdNivel = Convert.ToInt32(reader["IdNivel"]),
                        IdJornada = Convert.ToInt32(reader["IdJornada"]),
                        IdCurso = Convert.ToInt32(reader["IdCurso"]),
                        IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                        IdMateria = Convert.ToInt32(reader["IdMateria"]),
                        IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                        IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                        Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                        pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                        IdCalificacionCualitativa = string.IsNullOrEmpty(reader["IdCalificacionCualitativa"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativa"]),
                        IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                        Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                        MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString(),
                        Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : reader["Letra"].ToString(),
                        CodigoCalificacion = string.IsNullOrEmpty(reader["CodigoCalificacion"].ToString()) ? null : reader["CodigoCalificacion"].ToString(),
                        DescripcionCorta = string.IsNullOrEmpty(reader["DescripcionCorta"].ToString()) ? null : reader["DescripcionCorta"].ToString(),
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

        public aca_MatriculaCalificacionCualitativa_Info get_Info(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial, int IdMateria, decimal IdProfesor)
        {
            try
            {
                aca_MatriculaCalificacionCualitativa_Info info = new aca_MatriculaCalificacionCualitativa_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_MatriculaCalificacionCualitativa WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMatricula = " + IdMatricula.ToString() + " and IdMateria = " + IdMateria.ToString()
                    + " and IdCatalogoParcial = " + IdCatalogoParcial.ToString() + " and IdProfesor = " + IdProfesor.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_MatriculaCalificacionCualitativa_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            IdCalificacionCualitativa = string.IsNullOrEmpty(reader["IdCalificacionCualitativa"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativa"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString()
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
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.EsObligatorio, cm.OrdenMateria "
                    + " FROM     dbo.aca_MatriculaCalificacionCualitativa AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS c WITH (nolock) ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm WITH (nolock) ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND "
                    + " mc.IdMateria = cm.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso AND c.IdParalelo = cp.IdParalelo ";
                    query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdSede = " + IdSede.ToString() + " and c.IdAnio = " + IdAnio.ToString();
                    query += " GROUP BY mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso,  ";
                    query += " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.OrdenMateria, cm.EsObligatorio ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            IdProfesorInspector = string.IsNullOrEmpty(reader["IdProfesorInspector"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorInspector"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            OrdenMateria = Convert.ToInt32(reader["OrdenMateria"]),
                            EsObligatorio = Convert.ToBoolean(reader["EsObligatorio"])
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
        public List<aca_MatriculaCalificacion_Info> getList_CombosCualitativa(int IdEmpresa, int IdSede)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.EsObligatorio, cm.OrdenMateria "
                    + " FROM     dbo.aca_MatriculaCalificacionCualitativa AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS c WITH (nolock) ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm WITH (nolock) ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND "
                    + " mc.IdMateria = cm.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso AND c.IdParalelo = cp.IdParalelo ";
                    query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdSede = " + IdSede.ToString();
                    query += " GROUP BY mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso,  ";
                    query += " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.OrdenMateria, cm.EsObligatorio ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            IdProfesorInspector = string.IsNullOrEmpty(reader["IdProfesorInspector"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorInspector"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            OrdenMateria = Convert.ToInt32(reader["OrdenMateria"]),
                            EsObligatorio = Convert.ToBoolean(reader["EsObligatorio"])
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
        public List<aca_MatriculaCalificacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.EsObligatorio, cm.OrdenMateria "
                    + " FROM     dbo.aca_MatriculaCalificacionCualitativa AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS c WITH (nolock) ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm WITH (nolock) ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND "
                    + " mc.IdMateria = cm.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso AND c.IdParalelo = cp.IdParalelo ";
                    if (EsSuperAdmin == true)
                    {
                        query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdSede = " + IdSede.ToString()
                        + " and c.IdAnio = " + IdAnio.ToString();
                    }
                    else
                    {
                        query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdSede = " + IdSede.ToString()
                                + " and c.IdAnio = " + IdAnio.ToString() + " and mc.IdProfesor = " + IdProfesor.ToString();
                    }
                    query += " GROUP BY mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso,  "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.OrdenMateria, cm.EsObligatorio ";
                    
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            IdProfesorInspector = string.IsNullOrEmpty(reader["IdProfesorInspector"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorInspector"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            OrdenMateria = Convert.ToInt32(reader["OrdenMateria"]),
                            EsObligatorio = Convert.ToBoolean(reader["EsObligatorio"])
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
        public List<aca_MatriculaCalificacion_Info> getList_CombosCualitativa(int IdEmpresa, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacion_Info> Lista = new List<aca_MatriculaCalificacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.EsObligatorio, cm.OrdenMateria "
                    + " FROM     dbo.aca_MatriculaCalificacionCualitativa AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS c WITH (nolock) ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm WITH (nolock) ON c.IdEmpresa = cm.IdEmpresa AND c.IdAnio = cm.IdAnio AND c.IdSede = cm.IdSede AND c.IdNivel = cm.IdNivel AND c.IdJornada = cm.IdJornada AND c.IdCurso = cm.IdCurso AND "
                    + " mc.IdMateria = cm.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso AND c.IdParalelo = cp.IdParalelo ";
                    if (EsSuperAdmin == true)
                    {
                        query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdSede = " + IdSede.ToString();
                    }
                    else
                    {
                        query += " WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdSede = " + IdSede.ToString()
                                + " and mc.IdProfesor = " + IdProfesor.ToString();
                    }
                    query += " GROUP BY mc.IdEmpresa, mc.IdMatricula, mc.IdMateria, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso,  "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, cp.IdProfesorTutor, cp.IdProfesorInspector, cm.NomMateria, cm.OrdenMateria, cm.EsObligatorio ";

                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenNivel"]),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenJornada"]),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenCurso"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            IdProfesorInspector = string.IsNullOrEmpty(reader["IdProfesorInspector"].ToString()) ? 0 : Convert.ToDecimal(reader["IdProfesorInspector"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            OrdenMateria = Convert.ToInt32(reader["OrdenMateria"]),
                            EsObligatorio = Convert.ToBoolean(reader["EsObligatorio"])
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
        public bool generarCalificacion(List<aca_MatriculaCalificacionCualitativa_Info> lst_parcial)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativa_Info> Lista = new List<aca_MatriculaCalificacionCualitativa_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_matricula = (from q in lst_parcial
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdMatricula
                                         } into mat
                                         select new aca_Matricula_Info
                                         {
                                             IdEmpresa = mat.Key.IdEmpresa,
                                             IdMatricula = mat.Key.IdMatricula
                                         }).ToList();

                    foreach (var item in lst_matricula)
                    {
                        var lst_calificacion_cualitativa = Context.aca_MatriculaCalificacionCualitativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacionCualitativa.RemoveRange(lst_calificacion_cualitativa);

                        var lst_x_matricula = lst_parcial.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();

                        if (lst_x_matricula != null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaCalificacionCualitativa Entity = new aca_MatriculaCalificacionCualitativa
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdCatalogoParcial = info.IdCatalogoParcial,
                                    IdProfesor = info.IdProfesor,
                                    IdCalificacionCualitativa = info.IdCalificacionCualitativa,
                                    Conducta = info.Conducta,
                                    MotivoConducta = info.MotivoConducta,
                                    IdUsuarioCreacion = info.IdUsuarioCreacion,
                                    FechaCreacion = info.FechaCreacion,
                                    IdUsuarioModificacion = info.IdUsuarioModificacion,
                                    FechaModificacion = info.FechaModificacion
                                };

                                Context.aca_MatriculaCalificacionCualitativa.Add(Entity);
                            }
                        }
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

        public bool modificarDB(aca_MatriculaCalificacionCualitativa_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);

                    aca_MatriculaCalificacionCualitativa Entity = Context.aca_MatriculaCalificacionCualitativa.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula && q.IdMateria == info.IdMateria && q.IdProfesor == info.IdProfesor && q.IdCatalogoParcial == info.IdCatalogoParcial);
                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Entity.IdCalificacionCualitativa = info.IdCalificacionCualitativa;
                    Entity.Conducta = info.Conducta;
                    Entity.MotivoConducta = info.MotivoConducta;

                    Context.SaveChanges();

                    aca_MatriculaCalificacionCualitativaPromedio EntityCalificacionPromedio = Context.aca_MatriculaCalificacionCualitativaPromedio.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula && q.IdProfesor == info.IdProfesor && q.IdMateria == info.IdMateria);
                    if (EntityCalificacionPromedio == null)
                        return false;

                    decimal SumaPromedio = 0;
                    decimal Promedio = 0;
                    decimal PromedioFinal = 0;
                    //var IdEquivalenciaPromedio = (int?)null;
                    var lst_pacial_quim1 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1));
                    var lst_pacial_quim2 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2));
                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        var count_calificaciones = lst_pacial_quim1.Count();
                        var contador = 0;
                        foreach (var item in lst_pacial_quim1)
                        {
                            var registro_calificacion = getInfo_X_Matricula(info.IdEmpresa, info.IdMatricula, info.IdMateria, item.IdCatalogoParcial);
                            if (registro_calificacion!=null && registro_calificacion.IdCalificacionCualitativa!=null)
                            {
                                contador++;
                                var info_equivalencia_parcial = odata_equivalencia.getInfo(info.IdEmpresa, info.IdAnio, Convert.ToInt32(registro_calificacion.IdCalificacionCualitativa));
                                SumaPromedio = Convert.ToDecimal(SumaPromedio + (info_equivalencia_parcial.Calificacion == null ? 0 : info_equivalencia_parcial.Calificacion));
                            }
                        }

                        if (contador== count_calificaciones)
                        {
                            Promedio = SumaPromedio / count_calificaciones;
                            var info_equivalencia = odata_equivalencia.getInfo_x_Promedio( info.IdEmpresa,info.IdAnio,Promedio);
                            EntityCalificacionPromedio.PromedioQ1 = (info_equivalencia == null ? (int?)null : info_equivalencia.Calificacion);
                            EntityCalificacionPromedio.IdCalificacionCualitativaQ1 = (info_equivalencia == null ? (int?)null : info_equivalencia.IdCalificacionCualitativa);
                        }

                        if (EntityCalificacionPromedio.PromedioQ1!=null && EntityCalificacionPromedio.PromedioQ2!=null)
                        {
                            PromedioFinal = Convert.ToDecimal((EntityCalificacionPromedio.PromedioQ1 + EntityCalificacionPromedio.PromedioQ2) / 2);
                            EntityCalificacionPromedio.PromedioFinal = PromedioFinal;
                            var info_equivalencia_pf = odata_equivalencia.getInfo_x_Promedio(info.IdEmpresa, info.IdAnio, PromedioFinal);
                            EntityCalificacionPromedio.IdCalificacionCualitativaFinal = (info_equivalencia_pf == null ? (int?)null : info_equivalencia_pf.IdCalificacionCualitativa); 
                        }

                        Context.SaveChanges();
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        var count_calificaciones = lst_pacial_quim2.Count();
                        var contador = 0;
                        foreach (var item in lst_pacial_quim2)
                        {
                            var registro_calificacion = getInfo_X_Matricula(info.IdEmpresa, info.IdMatricula, info.IdMateria, item.IdCatalogoParcial);
                            if (registro_calificacion != null && registro_calificacion.IdCalificacionCualitativa != null)
                            {
                                contador++;
                                var info_equivalencia_parcial = odata_equivalencia.getInfo(info.IdEmpresa, info.IdAnio, Convert.ToInt32(registro_calificacion.IdCalificacionCualitativa));
                                SumaPromedio = Convert.ToDecimal(SumaPromedio + (info_equivalencia_parcial.Calificacion == null ? 0 : info_equivalencia_parcial.Calificacion));
                            }
                        }

                        if (contador == count_calificaciones)
                        {
                            Promedio = SumaPromedio / count_calificaciones;
                            var info_equivalencia = odata_equivalencia.getInfo_x_Promedio(info.IdEmpresa, info.IdAnio, Promedio);
                            EntityCalificacionPromedio.PromedioQ2 = (info_equivalencia == null ? (int?)null : info_equivalencia.Calificacion); ;
                            EntityCalificacionPromedio.IdCalificacionCualitativaQ2 = (info_equivalencia == null ? (int?)null : info_equivalencia.IdCalificacionCualitativa);
                        }

                        if (EntityCalificacionPromedio.PromedioQ1 != null && EntityCalificacionPromedio.PromedioQ2 != null)
                        {
                            PromedioFinal = Convert.ToDecimal((EntityCalificacionPromedio.PromedioQ1 + EntityCalificacionPromedio.PromedioQ2) / 2);
                            var info_equivalencia_pf = odata_equivalencia.getInfo_x_Promedio(info.IdEmpresa, info.IdAnio, PromedioFinal);
                            EntityCalificacionPromedio.PromedioFinal = (info_equivalencia_pf == null ? (int?)null : info_equivalencia_pf.Calificacion); ;
                            EntityCalificacionPromedio.IdCalificacionCualitativaFinal = (info_equivalencia_pf == null ? (int?)null : info_equivalencia_pf.IdCalificacionCualitativa);
                        }
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
    }
}
