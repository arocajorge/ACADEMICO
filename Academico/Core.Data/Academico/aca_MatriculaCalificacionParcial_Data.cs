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
    public class aca_MatriculaCalificacionParcial_Data
    {
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_AnioLectivoConductaEquivalencia_Data odata_conducta_equivalencia = new aca_AnioLectivoConductaEquivalencia_Data();
        aca_AnioLectivoEquivalenciaPromedio_Data odata_promedio_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Data();
        aca_AnioLectivoParcial_Data odata_parcial = new aca_AnioLectivoParcial_Data();
        aca_Matricula_Data odata_matricula = new aca_Matricula_Data();
        public List<aca_MatriculaCalificacionParcial_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            IdProfesor = q.IdProfesor,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_MatriculaCalificacionParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial, decimal IdProfesor)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdCatalogoParcial == IdCatalogoParcial && q.IdProfesor == IdProfesor).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            RegistroValido = true,
                            RegistroconPromedioBajo = false
                        });
                    });
                }*/

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mcp.IdEmpresa, mcp.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, p.pe_nombreCompleto, mcp.IdCatalogoParcial, mcp.IdProfesor, mcp.IdMateria, mcp.Calificacion1, mcp.Calificacion2, "
                    + " mcp.Calificacion3, mcp.Calificacion4, mcp.Evaluacion, mcp.Remedial1, mcp.Remedial2, mcp.Conducta, mcp.MotivoCalificacion, mcp.MotivoConducta, mcp.AccionRemedial, a.Codigo, mc.CalificacionP1, mc.CalificacionP2, mc.CalificacionP3, "
                    + " mc.CalificacionP4, mc.CalificacionP5, mc.CalificacionP6, c.Letra "
                    + " FROM     dbo.aca_MatriculaCalificacionParcial AS mcp INNER JOIN "
                    + " dbo.aca_Matricula AS m ON mcp.IdEmpresa = m.IdEmpresa AND mcp.IdMatricula = m.IdMatricula AND mcp.IdEmpresa = m.IdEmpresa AND mcp.IdEmpresa = m.IdEmpresa INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_MatriculaCalificacion AS mc ON mcp.IdEmpresa = mc.IdEmpresa AND mcp.IdMatricula = mc.IdMatricula AND mcp.IdMateria = mc.IdMateria AND mcp.IdProfesor = mc.IdProfesor LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS c ON m.IdAnio = c.IdAnio AND c.IdEmpresa = m.IdEmpresa AND mcp.Conducta = c.Secuencia "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f "
                    + " WHERE(IdEmpresa = mcp.IdEmpresa) AND(IdMatricula = mcp.IdMatricula) AND(Estado = 1))) "
                    + " AND mcp.IdEmpresa = " + IdEmpresa.ToString() + " AND m.IdAnio =" + IdAnio.ToString() + " AND m.IdSede = " + IdSede.ToString()
                    + " AND m.IdNivel = " + IdNivel.ToString() + " AND m.IdJornada = " + IdJornada.ToString() + " AND m.IdCurso = " + IdCurso.ToString()
                    + " AND m.IdParalelo = " + IdParalelo.ToString() + " AND mcp.IdMateria = " + IdMateria.ToString() + " AND mcp.IdCatalogoParcial = " + IdCatalogoParcial.ToString() + "AND mcp.IdProfesor = " + IdProfesor.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Calificacion1 = string.IsNullOrEmpty(reader["Calificacion1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion1"]),
                            Calificacion2 = string.IsNullOrEmpty(reader["Calificacion2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion2"]),
                            Calificacion3 = string.IsNullOrEmpty(reader["Calificacion3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion3"]),
                            Calificacion4 = string.IsNullOrEmpty(reader["Calificacion4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion4"]),
                            Evaluacion = string.IsNullOrEmpty(reader["Evaluacion"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Evaluacion"]),
                            Remedial1 = string.IsNullOrEmpty(reader["Remedial1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial1"]),
                            Remedial2 = string.IsNullOrEmpty(reader["Remedial2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial2"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoCalificacion = string.IsNullOrEmpty(reader["MotivoCalificacion"].ToString()) ? null : reader["MotivoCalificacion"].ToString(),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString(),
                            AccionRemedial = string.IsNullOrEmpty(reader["AccionRemedial"].ToString()) ? null : reader["AccionRemedial"].ToString(),
                            CalificacionP1 = string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"]),
                            CalificacionP2 = string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"]),
                            CalificacionP3 = string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"]),
                            CalificacionP4 = string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"]),
                            CalificacionP5 = string.IsNullOrEmpty(reader["CalificacionP5"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP5"]),
                            CalificacionP6 = string.IsNullOrEmpty(reader["CalificacionP6"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP6"]),
                            Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : reader["Letra"].ToString(),
                            RegistroValido = true,
                            RegistroconPromedioBajo = false
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
        
        public List<aca_MatriculaCalificacionParcial_Info> GetList_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mcp.IdEmpresa, mcp.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, p.pe_nombreCompleto, mcp.IdCatalogoParcial, mcp.IdProfesor, mcp.IdMateria, mcp.Calificacion1, mcp.Calificacion2, "
                    + " mcp.Calificacion3, mcp.Calificacion4, mcp.Evaluacion, mcp.Remedial1, mcp.Remedial2, mcp.Conducta, mcp.MotivoCalificacion, mcp.MotivoConducta, mcp.AccionRemedial, a.Codigo, mc.CalificacionP1, mc.CalificacionP2, mc.CalificacionP3, "
                    + " mc.CalificacionP4, mc.CalificacionP5, mc.CalificacionP6, c.Letra "
                    + " FROM     dbo.aca_MatriculaCalificacionParcial AS mcp INNER JOIN "
                    + " dbo.aca_Matricula AS m ON mcp.IdEmpresa = m.IdEmpresa AND mcp.IdMatricula = m.IdMatricula AND mcp.IdEmpresa = m.IdEmpresa AND mcp.IdEmpresa = m.IdEmpresa INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_MatriculaCalificacion AS mc ON mcp.IdEmpresa = mc.IdEmpresa AND mcp.IdMatricula = mc.IdMatricula AND mcp.IdMateria = mc.IdMateria AND mcp.IdProfesor = mc.IdProfesor LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS c ON m.IdAnio = c.IdAnio AND c.IdEmpresa = m.IdEmpresa AND mcp.Conducta = c.Secuencia "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f "
                    + " WHERE(IdEmpresa = mcp.IdEmpresa) AND(IdMatricula = mcp.IdMatricula) AND(Estado = 1))) "
                    + " AND mcp.IdEmpresa = " + IdEmpresa.ToString() + " AND m.IdAnio =" + IdAnio.ToString() + " AND m.IdSede = " + IdSede.ToString()
                    + " AND m.IdNivel = " + IdNivel.ToString() + " AND m.IdJornada = " + IdJornada.ToString() + " AND m.IdCurso = " + IdCurso.ToString()
                    +" AND m.IdParalelo = " + IdParalelo.ToString() + " AND mcp.IdMateria = " + IdMateria.ToString() + " AND mcp.IdCatalogoParcial = " + IdCatalogoParcial.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Calificacion1 = string.IsNullOrEmpty(reader["Calificacion1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion1"]),
                            Calificacion2 = string.IsNullOrEmpty(reader["Calificacion2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion2"]),
                            Calificacion3 = string.IsNullOrEmpty(reader["Calificacion3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion3"]),
                            Calificacion4 = string.IsNullOrEmpty(reader["Calificacion4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion4"]),
                            Evaluacion = string.IsNullOrEmpty(reader["Evaluacion"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Evaluacion"]),
                            Remedial1 = string.IsNullOrEmpty(reader["Remedial1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial1"]),
                            Remedial2 = string.IsNullOrEmpty(reader["Remedial2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial2"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoCalificacion = string.IsNullOrEmpty(reader["MotivoCalificacion"].ToString()) ? null : reader["MotivoCalificacion"].ToString(),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString(),
                            AccionRemedial = string.IsNullOrEmpty(reader["AccionRemedial"].ToString()) ? null : reader["AccionRemedial"].ToString(),
                            CalificacionP1 = string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"]),
                            CalificacionP2 = string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"]),
                            CalificacionP3 = string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"]),
                            CalificacionP4 = string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"]),
                            CalificacionP5 = string.IsNullOrEmpty(reader["CalificacionP5"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP5"]),
                            CalificacionP6 = string.IsNullOrEmpty(reader["CalificacionP6"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP6"]),
                            Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : reader["Letra"].ToString(),
                            RegistroValido = true,
                            RegistroconPromedioBajo = false
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede
                    && q.IdAnio == IdAnio && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo
                    && q.IdMateria == IdMateria && q.IdCatalogoParcial == IdCatalogoParcial).OrderBy(q => q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial,
                            CalificacionP1 = q.CalificacionP1,
                            CalificacionP2 = q.CalificacionP2,
                            CalificacionP3 = q.CalificacionP3,
                            CalificacionP4 = q.CalificacionP4,
                            CalificacionP5 = q.CalificacionP5,
                            CalificacionP6 = q.CalificacionP6,
                            RegistroValido = true,
                            RegistroconPromedioBajo = false
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCalificacionParcial_Info> getList_x_Parcial(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula
                    && q.IdCatalogoParcial == IdCatalogoParcial).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdAlumno = q.IdAlumno,
                            Codigo = q.Codigo,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Evaluacion = q.Evaluacion,
                            Remedial1 = q.Remedial1,
                            Remedial2 = q.Remedial2,
                            Conducta = q.Conducta,
                            MotivoCalificacion = q.MotivoCalificacion,
                            MotivoConducta = q.MotivoConducta,
                            AccionRemedial = q.AccionRemedial
                        });
                    });
                }
                */
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mcp.IdEmpresa, mcp.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, p.pe_nombreCompleto, mcp.IdCatalogoParcial, mcp.IdProfesor, mcp.IdMateria, mcp.Calificacion1, mcp.Calificacion2, "
                    + " mcp.Calificacion3, mcp.Calificacion4, mcp.Evaluacion, mcp.Remedial1, mcp.Remedial2, mcp.Conducta, mcp.MotivoCalificacion, mcp.MotivoConducta, mcp.AccionRemedial, a.Codigo, mc.CalificacionP1, mc.CalificacionP2, mc.CalificacionP3, "
                    + " mc.CalificacionP4, mc.CalificacionP5, mc.CalificacionP6, c.Letra "
                    + " FROM     dbo.aca_MatriculaCalificacionParcial AS mcp INNER JOIN "
                    + " dbo.aca_Matricula AS m ON mcp.IdEmpresa = m.IdEmpresa AND mcp.IdMatricula = m.IdMatricula AND mcp.IdEmpresa = m.IdEmpresa AND mcp.IdEmpresa = m.IdEmpresa INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_MatriculaCalificacion AS mc ON mcp.IdEmpresa = mc.IdEmpresa AND mcp.IdMatricula = mc.IdMatricula AND mcp.IdMateria = mc.IdMateria AND mcp.IdProfesor = mc.IdProfesor LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS c ON m.IdAnio = c.IdAnio AND c.IdEmpresa = m.IdEmpresa AND mcp.Conducta = c.Secuencia "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f "
                    + " WHERE(IdEmpresa = mcp.IdEmpresa) AND(IdMatricula = mcp.IdMatricula) AND(Estado = 1))) "
                    + " AND mcp.IdEmpresa = " + IdEmpresa.ToString() + " AND m.IdMatricula =" + IdMatricula.ToString() + " AND mcp.IdCatalogoParcial = " + IdCatalogoParcial.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Calificacion1 = string.IsNullOrEmpty(reader["Calificacion1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion1"]),
                            Calificacion2 = string.IsNullOrEmpty(reader["Calificacion2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion2"]),
                            Calificacion3 = string.IsNullOrEmpty(reader["Calificacion3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion3"]),
                            Calificacion4 = string.IsNullOrEmpty(reader["Calificacion4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion4"]),
                            Evaluacion = string.IsNullOrEmpty(reader["Evaluacion"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Evaluacion"]),
                            Remedial1 = string.IsNullOrEmpty(reader["Remedial1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial1"]),
                            Remedial2 = string.IsNullOrEmpty(reader["Remedial2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial2"]),
                            Conducta = string.IsNullOrEmpty(reader["Conducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["Conducta"]),
                            MotivoCalificacion = string.IsNullOrEmpty(reader["MotivoCalificacion"].ToString()) ? null : reader["MotivoCalificacion"].ToString(),
                            MotivoConducta = string.IsNullOrEmpty(reader["MotivoConducta"].ToString()) ? null : reader["MotivoConducta"].ToString(),
                            AccionRemedial = string.IsNullOrEmpty(reader["AccionRemedial"].ToString()) ? null : reader["AccionRemedial"].ToString(),
                            CalificacionP1 = string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"]),
                            CalificacionP2 = string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"]),
                            CalificacionP3 = string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"]),
                            CalificacionP4 = string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"]),
                            CalificacionP5 = string.IsNullOrEmpty(reader["CalificacionP5"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP5"]),
                            CalificacionP6 = string.IsNullOrEmpty(reader["CalificacionP6"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP6"]),
                            Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : reader["Letra"].ToString(),
                            RegistroValido = true,
                            RegistroconPromedioBajo = false
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

        public aca_MatriculaCalificacionParcial_Info get_Info(int IdEmpresa, decimal IdMatricula, int IdCatalogoParcial, int IdMateria, decimal IdProfesor)
        {
            try
            {
                aca_MatriculaCalificacionParcial_Info info = new aca_MatriculaCalificacionParcial_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula && q.IdMateria == IdMateria
                    && q.IdProfesor == IdProfesor && q.IdCatalogoParcial== IdCatalogoParcial).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaCalificacionParcial_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdMateria = Entity.IdMateria,
                        IdProfesor = Entity.IdProfesor,
                        IdCatalogoParcial = Entity.IdCatalogoParcial,
                        Calificacion1 = Entity.Calificacion1,
                        Calificacion2 = Entity.Calificacion2,
                        Calificacion3 = Entity.Calificacion3,
                        Calificacion4 = Entity.Calificacion4,
                        Evaluacion = Entity.Evaluacion,
                        Remedial1 = Entity.Remedial1,
                        Remedial2 = Entity.Remedial2,
                        Conducta = Entity.Conducta,
                        MotivoCalificacion = Entity.MotivoCalificacion,
                        MotivoConducta = Entity.MotivoConducta,
                        AccionRemedial = Entity.AccionRemedial
                    };

                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool generarCalificacion(List<aca_MatriculaCalificacionParcial_Info> lst_parcial)
        {
            try
            {
                List<aca_MatriculaCalificacionParcial_Info> Lista = new List<aca_MatriculaCalificacionParcial_Info>();

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
                        var lst_calificacion_parcial = Context.aca_MatriculaCalificacionParcial.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacionParcial.RemoveRange(lst_calificacion_parcial);

                        var lst_x_matricula = lst_parcial.Where(q=> q.IdEmpresa == item.IdEmpresa && q.IdMatricula ==item.IdMatricula).ToList();

                        if (lst_x_matricula!=null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaCalificacionParcial Entity = new aca_MatriculaCalificacionParcial
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdCatalogoParcial = info.IdCatalogoParcial,
                                    IdProfesor = info.IdProfesor,
                                    Calificacion1 = info.Calificacion1,
                                    Calificacion2 = info.Calificacion2,
                                    Calificacion3 = info.Calificacion3,
                                    Calificacion4 = info.Calificacion4,
                                    Remedial1 = info.Remedial1,
                                    Remedial2 = info.Remedial2,
                                    Evaluacion = info.Evaluacion,
                                    Conducta = info.Conducta,
                                    MotivoCalificacion = info.MotivoCalificacion,
                                    MotivoConducta = info.MotivoConducta,
                                    AccionRemedial = info.AccionRemedial,
                                    IdUsuarioCreacion= info.IdUsuarioCreacion,
                                    FechaCreacion = info.FechaCreacion,
                                    IdUsuarioModificacion = info.IdUsuarioModificacion,
                                    FechaModificacion = info.FechaModificacion
                                };

                                Context.aca_MatriculaCalificacionParcial.Add(Entity);
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

        public bool modificarDB(aca_MatriculaCalificacionParcial_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var info_matricula = odata_matricula.getInfo(info.IdEmpresa, info.IdMatricula);

                    aca_MatriculaCalificacionParcial Entity = Context.aca_MatriculaCalificacionParcial.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdMatricula == info.IdMatricula && q.IdMateria== info.IdMateria && q.IdProfesor== info.IdProfesor && q.IdCatalogoParcial == info.IdCatalogoParcial);
                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;
                    Entity.Calificacion1 = info.Calificacion1;
                    Entity.Calificacion2 = info.Calificacion2;
                    Entity.Calificacion3 = info.Calificacion3;
                    Entity.Calificacion4 = info.Calificacion4;
                    Entity.Remedial1 = info.Remedial1;
                    Entity.Remedial2 = info.Remedial2;
                    Entity.Evaluacion = info.Evaluacion;
                    Entity.Conducta = info.Conducta;
                    Entity.MotivoCalificacion = info.MotivoCalificacion;
                    Entity.MotivoConducta = info.MotivoConducta;
                    Entity.AccionRemedial = info.AccionRemedial;

                    aca_MatriculaCalificacion EntityCalificacion = Context.aca_MatriculaCalificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa 
                    && q.IdMatricula == info.IdMatricula && q.IdProfesor == info.IdProfesor && q.IdMateria == info.IdMateria);
                    if (EntityCalificacion == null)
                        return false;
                    
                    if(info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        EntityCalificacion.CalificacionP1 = info.PromedioParcial;
                        EntityCalificacion.IdEquivalenciaPromedioP1 = info.IdEquivalenciaPromedioParcial;
                    } 

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        EntityCalificacion.CalificacionP2 = info.PromedioParcial;
                        EntityCalificacion.IdEquivalenciaPromedioP2 = info.IdEquivalenciaPromedioParcial;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        EntityCalificacion.CalificacionP3 = info.PromedioParcial;
                        EntityCalificacion.IdEquivalenciaPromedioP3 = info.IdEquivalenciaPromedioParcial;
                    }                        

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        EntityCalificacion.CalificacionP4 = info.PromedioParcial;
                        EntityCalificacion.IdEquivalenciaPromedioP4 = info.IdEquivalenciaPromedioParcial;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        EntityCalificacion.CalificacionP5 = info.PromedioParcial;
                        EntityCalificacion.IdEquivalenciaPromedioP5 = info.IdEquivalenciaPromedioParcial;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        EntityCalificacion.CalificacionP6 = info.PromedioParcial;
                        EntityCalificacion.IdEquivalenciaPromedioP6 = info.IdEquivalenciaPromedioParcial;
                    }

                    Context.SaveChanges();

                    aca_MatriculaCalificacion EntityCalificacionPromedio = Context.aca_MatriculaCalificacion.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa
                    && q.IdMatricula == info.IdMatricula && q.IdProfesor == info.IdProfesor && q.IdMateria == info.IdMateria);
                    if (EntityCalificacionPromedio == null)
                        return false;

                    decimal SumaPromedio = 0;
                    decimal Promedio = 0;
                    var IdEquivalenciaPromedio = (int?)null;
                    var lst_pacial_quim1 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1));
                    var lst_pacial_quim2 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2));
                    var GenerarPromedio = false;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        if (lst_pacial_quim1.Count()>0)
                        {
                            if (lst_pacial_quim1.Count()==1)
                            {
                                if (EntityCalificacionPromedio.CalificacionP1 != null)
                                {
                                    GenerarPromedio = true;
                                }
                            }
                            else if(lst_pacial_quim1.Count()==2)
                            {
                                if (EntityCalificacionPromedio.CalificacionP1 != null && EntityCalificacionPromedio.CalificacionP2 != null)
                                {
                                    GenerarPromedio = true;
                                }
                            }
                            else
                            {
                                if (EntityCalificacionPromedio.CalificacionP1 != null && EntityCalificacionPromedio.CalificacionP2 != null && EntityCalificacionPromedio.CalificacionP3 != null)
                                {
                                    GenerarPromedio = true;
                                }
                            }
                        }
                        else
                        {
                            GenerarPromedio = false;
                        }

                        if (GenerarPromedio == true)
                        {
                            SumaPromedio = Convert.ToDecimal(EntityCalificacionPromedio.CalificacionP1) + Convert.ToDecimal(EntityCalificacionPromedio.CalificacionP2) + Convert.ToDecimal(EntityCalificacionPromedio.CalificacionP3);
                            Promedio = Math.Round((SumaPromedio / lst_pacial_quim1.Count), 2, MidpointRounding.AwayFromZero);
                            EntityCalificacionPromedio.PromedioQ1 = Promedio;

                            if (EntityCalificacionPromedio.ExamenQ1!=null)
                            {
                                var PromedioFinalQ1 = ((EntityCalificacionPromedio.PromedioQ1) * Convert.ToDecimal(0.80)) + ((EntityCalificacionPromedio.ExamenQ1) * Convert.ToDecimal(0.20));
                                var info_equivalencia = odata_promedio_equivalencia.getInfo_x_Promedio(info_matricula.IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(PromedioFinalQ1));
                                EntityCalificacionPromedio.PromedioFinalQ1 = PromedioFinalQ1;
                                EntityCalificacionPromedio.IdEquivalenciaPromedioQ1 = (info_equivalencia==null ? (int?)null : info_equivalencia.IdEquivalenciaPromedio);
                            }

                            var PromedioQuimestral = (decimal?)null;
                            if (EntityCalificacion.PromedioFinalQ1 != null && EntityCalificacion.PromedioFinalQ2 != null)
                            {
                                PromedioQuimestral = Math.Round(((Convert.ToDecimal(EntityCalificacion.PromedioFinalQ1 + EntityCalificacion.PromedioFinalQ2)) / 2), 2, MidpointRounding.AwayFromZero);
                            }

                            EntityCalificacion.PromedioQuimestres = PromedioQuimestral;
                        }
                        else
                        {
                            EntityCalificacionPromedio.PromedioQ1 = null;
                            EntityCalificacionPromedio.PromedioFinalQ1 = null;
                            EntityCalificacionPromedio.IdEquivalenciaPromedioQ1 = null;

                            EntityCalificacionPromedio.PromedioQuimestres = null;
                            EntityCalificacionPromedio.PromedioFinal = null;
                            EntityCalificacionPromedio.IdEquivalenciaPromedioPF = null;
                        }
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        GenerarPromedio = false;
                        if (lst_pacial_quim2.Count() > 0)
                        {
                            if (lst_pacial_quim2.Count() == 1)
                            {
                                if (EntityCalificacionPromedio.CalificacionP4 != null)
                                {
                                    GenerarPromedio = true;
                                }
                            }
                            else if (lst_pacial_quim2.Count() == 2)
                            {
                                if (EntityCalificacionPromedio.CalificacionP4 != null && EntityCalificacionPromedio.CalificacionP5 != null)
                                {
                                    GenerarPromedio = true;
                                }
                            }
                            else
                            {
                                if (EntityCalificacionPromedio.CalificacionP4 != null && EntityCalificacionPromedio.CalificacionP5 != null && EntityCalificacionPromedio.CalificacionP6 != null)
                                {
                                    GenerarPromedio = true;
                                }
                            }
                        }
                        else
                        {
                            GenerarPromedio = false;
                        }

                        if (GenerarPromedio== true)
                        {
                            SumaPromedio = Convert.ToDecimal(EntityCalificacionPromedio.CalificacionP4) + Convert.ToDecimal(EntityCalificacionPromedio.CalificacionP5) + Convert.ToDecimal(EntityCalificacionPromedio.CalificacionP6);
                            Promedio = Math.Round((SumaPromedio / lst_pacial_quim2.Count), 2, MidpointRounding.AwayFromZero);
                            EntityCalificacionPromedio.PromedioQ2 = Promedio;

                            if (EntityCalificacionPromedio.ExamenQ2 != null)
                            {
                                var PromedioFinalQ2 = ((EntityCalificacionPromedio.PromedioQ2) * Convert.ToDecimal(0.80)) + ((EntityCalificacionPromedio.ExamenQ2) * Convert.ToDecimal(0.20));
                                var info_equivalencia = odata_promedio_equivalencia.getInfo_x_Promedio(info_matricula.IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(PromedioFinalQ2));
                                EntityCalificacionPromedio.PromedioFinalQ2 = PromedioFinalQ2;
                                EntityCalificacionPromedio.IdEquivalenciaPromedioQ2 = (info_equivalencia == null ? (int?)null : info_equivalencia.IdEquivalenciaPromedio);
                            }

                            var PromedioQuimestral = (decimal?)null;
                            if (EntityCalificacion.PromedioFinalQ1 != null && EntityCalificacion.PromedioFinalQ2 != null)
                            {
                                PromedioQuimestral = Math.Round(((Convert.ToDecimal(EntityCalificacion.PromedioFinalQ1 + EntityCalificacion.PromedioFinalQ2)) / 2), 2, MidpointRounding.AwayFromZero);
                            }

                            EntityCalificacion.PromedioQuimestres = PromedioQuimestral;
                        }
                        else
                        {
                            EntityCalificacionPromedio.PromedioQ2 = null;
                            EntityCalificacionPromedio.PromedioFinalQ2 = null;
                            EntityCalificacionPromedio.IdEquivalenciaPromedioQ2 = null;

                            EntityCalificacionPromedio.PromedioQuimestres = null;
                            EntityCalificacionPromedio.PromedioFinal = null;
                            EntityCalificacionPromedio.IdEquivalenciaPromedioPF = null;
                        }
                    }

                    Context.SaveChanges();

                    aca_MatriculaConducta EntityConductaPromedio = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConductaPromedio == null)
                        return false;

                    decimal SumaConducta = 0;
                    var lst_calificaciones_parciales = getList_x_Parcial(info.IdEmpresa, info.IdMatricula, info.IdCatalogoParcial);
                    foreach (var item in lst_calificaciones_parciales)
                    {
                        if (item.Conducta!=null)
                        {
                            var info_equivalencia = odata_conducta.getInfo(info.IdEmpresa, info_matricula.IdAnio, Convert.ToInt32(item.Conducta));
                            SumaConducta = SumaConducta + info_equivalencia.Calificacion;
                        }
                    }

                    double PromedioParcialConducta = Math.Round(Convert.ToDouble(SumaConducta/ lst_calificaciones_parciales.Count()),2,MidpointRounding.AwayFromZero);
                    var info_conducta = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info_matricula.IdAnio, Convert.ToDecimal(PromedioParcialConducta));
                    var infoConductaMinima = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                    var SecuenciaConductaMinima = infoConductaMinima.Secuencia;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        EntityConductaPromedio.PromedioP1 = PromedioParcialConducta;
                        EntityConductaPromedio.SecuenciaPromedioP1 = (info_conducta == null ? SecuenciaConductaMinima : info_conducta.Secuencia);
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        EntityConductaPromedio.PromedioP2 = PromedioParcialConducta;
                        EntityConductaPromedio.SecuenciaPromedioP2 = (info_conducta == null ? SecuenciaConductaMinima : info_conducta.Secuencia);
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        EntityConductaPromedio.PromedioP3 = PromedioParcialConducta;
                        EntityConductaPromedio.SecuenciaPromedioP3 = (info_conducta == null ? SecuenciaConductaMinima : info_conducta.Secuencia);
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        EntityConductaPromedio.PromedioP4 = PromedioParcialConducta;
                        EntityConductaPromedio.SecuenciaPromedioP4 = (info_conducta == null ? SecuenciaConductaMinima : info_conducta.Secuencia);
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        EntityConductaPromedio.PromedioP5 = PromedioParcialConducta;
                        EntityConductaPromedio.SecuenciaPromedioP5 = (info_conducta == null ? SecuenciaConductaMinima : info_conducta.Secuencia);
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        EntityConductaPromedio.PromedioP6 = PromedioParcialConducta;
                        EntityConductaPromedio.SecuenciaPromedioP6 = (info_conducta == null ? SecuenciaConductaMinima : info_conducta.Secuencia);
                    }

                    Context.SaveChanges();

                    aca_MatriculaConducta EntityConductaPromedioQuim = Context.aca_MatriculaConducta.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityConductaPromedioQuim == null)
                        return false;

                    double SumaQuimestre = 0;
                    double PromedioQuimestre = 0;
                    var SecuenciaConducta = (int?)null;
                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        //var lst_pacial_quim1 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1));
                        SumaQuimestre = Convert.ToDouble(EntityConductaPromedioQuim.PromedioP1) + Convert.ToDouble(EntityConductaPromedioQuim.PromedioP2) + Convert.ToDouble(EntityConductaPromedioQuim.PromedioP3);
                        PromedioQuimestre = Math.Round((SumaQuimestre / lst_pacial_quim1.Count()), 2, MidpointRounding.AwayFromZero);
                        var info_conductaQ1= odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioQuimestre));
                        var infoMinimaConductaQ1 = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                        SecuenciaConducta = infoMinimaConductaQ1.Secuencia;

                        EntityConductaPromedioQuim.PromedioQ1 = PromedioQuimestre;
                        EntityConductaPromedioQuim.SecuenciaPromedioQ1 = info_conductaQ1 == null ? SecuenciaConducta : info_conductaQ1.Secuencia;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5) || info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        //var lst_pacial_quim2 = odata_parcial.getList_x_Tipo(info.IdEmpresa, info_matricula.IdSede, info_matricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2));
                        SumaQuimestre = Convert.ToDouble(EntityConductaPromedioQuim.PromedioP4) + Convert.ToDouble(EntityConductaPromedioQuim.PromedioP5) + Convert.ToDouble(EntityConductaPromedioQuim.PromedioP6);
                        PromedioQuimestre = Math.Round((SumaQuimestre / lst_pacial_quim2.Count()), 2, MidpointRounding.AwayFromZero);
                        var info_conductaQ2 = odata_conducta_equivalencia.getInfoXPromedioConducta(info.IdEmpresa, info.IdAnio, Convert.ToDecimal(PromedioQuimestre));
                        var infoMinimaConductaQ2 = odata_conducta_equivalencia.getInfo_MinimaConducta(info.IdEmpresa, info_matricula.IdAnio);
                        SecuenciaConducta = infoMinimaConductaQ2.Secuencia;

                        EntityConductaPromedioQuim.PromedioQ2 = PromedioQuimestre;
                        EntityConductaPromedioQuim.SecuenciaPromedioQ2 = info_conductaQ2 == null ? SecuenciaConducta : info_conductaQ2.Secuencia;
                    }

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
