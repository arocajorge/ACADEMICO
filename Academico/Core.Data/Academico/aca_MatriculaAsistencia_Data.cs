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
    public class aca_MatriculaAsistencia_Data
    {
        public List<aca_MatriculaAsistencia_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo)
        {
            try
            {
                List<aca_MatriculaAsistencia_Info> Lista = new List<aca_MatriculaAsistencia_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT ma.IdEmpresa, ma.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, ma.FInjustificadaP1, ma.FJustificadaP1, ma.AtrasosP1, ma.FInjustificadaP2,  "
                    + " ma.FJustificadaP2, ma.AtrasosP2, ma.FInjustificadaP3, ma.FJustificadaP3, ma.AtrasosP3, ma.FInjustificadaP4, ma.FJustificadaP4, ma.AtrasosP4, ma.FInjustificadaP5, ma.FJustificadaP5, ma.AtrasosP5, ma.FInjustificadaP6, "
                    + " ma.FJustificadaP6, ma.AtrasosP6, cp.IdProfesorTutor, cp.IdProfesorInspector "
                    + " FROM dbo.aca_MatriculaAsistencia AS ma INNER JOIN "
                    + " dbo.aca_Matricula AS m ON ma.IdEmpresa = m.IdEmpresa AND ma.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f "
                    + " WHERE(IdEmpresa = ma.IdEmpresa) AND(IdMatricula = ma.IdMatricula) AND(Estado = 1))) "
                    + " AND ma.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdSede = " + IdSede.ToString() + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdNivel = " + IdNivel.ToString() + " and m.IdJornada = " + IdJornada.ToString() + " and m.IdCurso = " + IdCurso.ToString()
                    + " and m.IdParalelo = " + IdParalelo.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaAsistencia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            FJustificadaP1 = string.IsNullOrEmpty(reader["FJustificadaP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP1"]),
                            FInjustificadaP1 = string.IsNullOrEmpty(reader["FInjustificadaP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP1"]),
                            AtrasosP1 = string.IsNullOrEmpty(reader["AtrasosP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP1"]),
                            FJustificadaP2 = string.IsNullOrEmpty(reader["FJustificadaP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP2"]),
                            FInjustificadaP2 = string.IsNullOrEmpty(reader["FInjustificadaP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP2"]),
                            AtrasosP2 = string.IsNullOrEmpty(reader["AtrasosP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP2"]),
                            FInjustificadaP3 = string.IsNullOrEmpty(reader["FInjustificadaP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP3"]),
                            FJustificadaP3 = string.IsNullOrEmpty(reader["FJustificadaP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP3"]),
                            AtrasosP3 = string.IsNullOrEmpty(reader["AtrasosP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP3"]),
                            FJustificadaP4 = string.IsNullOrEmpty(reader["FJustificadaP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP4"]),
                            FInjustificadaP4 = string.IsNullOrEmpty(reader["FInjustificadaP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP4"]),
                            AtrasosP4 = string.IsNullOrEmpty(reader["AtrasosP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP4"]),
                            FJustificadaP5 = string.IsNullOrEmpty(reader["FJustificadaP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP5"]),
                            FInjustificadaP5 = string.IsNullOrEmpty(reader["FInjustificadaP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP5"]),
                            AtrasosP5 = string.IsNullOrEmpty(reader["AtrasosP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP5"]),
                            FJustificadaP6 = string.IsNullOrEmpty(reader["FJustificadaP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP6"]),
                            FInjustificadaP6 = string.IsNullOrEmpty(reader["FInjustificadaP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP6"]),
                            AtrasosP6 = string.IsNullOrEmpty(reader["AtrasosP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP6"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString()
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_MatriculaAsistencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio==IdAnio && q.IdNivel==IdNivel && q.IdJornada==IdJornada && q.IdCurso==IdCurso && q.IdParalelo==IdParalelo).OrderBy(q=>q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaAsistencia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            FJustificadaP1 = q.FJustificadaP1,
                            FInjustificadaP1 = q.FInjustificadaP1,
                            AtrasosP1 = q.AtrasosP1,
                            FJustificadaP2 = q.FJustificadaP2,
                            FInjustificadaP2 = q.FInjustificadaP2,
                            AtrasosP2 = q.AtrasosP2,
                            FInjustificadaP3 = q.FInjustificadaP3,
                            FJustificadaP3 = q.FJustificadaP3,
                            AtrasosP3 = q.AtrasosP3,
                            FJustificadaP4 = q.FJustificadaP4,
                            FInjustificadaP4 = q.FInjustificadaP4,
                            AtrasosP4 = q.AtrasosP4,
                            FJustificadaP5 = q.FJustificadaP5,
                            FInjustificadaP5 = q.FInjustificadaP5,
                            AtrasosP5 = q.AtrasosP5,
                            FJustificadaP6 = q.FJustificadaP6,
                            FInjustificadaP6 = q.FInjustificadaP6,
                            AtrasosP6 = q.AtrasosP6,
                            pe_nombreCompleto = q.pe_nombreCompleto
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

        public List<aca_MatriculaAsistencia_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaAsistencia_Info> Lista = new List<aca_MatriculaAsistencia_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT ma.IdEmpresa, ma.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, ma.FInjustificadaP1, ma.FJustificadaP1, ma.AtrasosP1, ma.FInjustificadaP2,  "
                    + " ma.FJustificadaP2, ma.AtrasosP2, ma.FInjustificadaP3, ma.FJustificadaP3, ma.AtrasosP3, ma.FInjustificadaP4, ma.FJustificadaP4, ma.AtrasosP4, ma.FInjustificadaP5, ma.FJustificadaP5, ma.AtrasosP5, ma.FInjustificadaP6, "
                    + " ma.FJustificadaP6, ma.AtrasosP6, cp.IdProfesorTutor, cp.IdProfesorInspector "
                    + " FROM dbo.aca_MatriculaAsistencia AS ma INNER JOIN "
                    + " dbo.aca_Matricula AS m ON ma.IdEmpresa = m.IdEmpresa AND ma.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo "
                    + " WHERE ma.IdEmpresa = " + IdEmpresa.ToString() + " and ma.IdMatricula = " + IdMatricula.ToString(); ;
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaAsistencia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            FJustificadaP1 = string.IsNullOrEmpty(reader["FJustificadaP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP1"]),
                            FInjustificadaP1 = string.IsNullOrEmpty(reader["FInjustificadaP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP1"]),
                            AtrasosP1 = string.IsNullOrEmpty(reader["AtrasosP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP1"]),
                            FJustificadaP2 = string.IsNullOrEmpty(reader["FJustificadaP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP2"]),
                            FInjustificadaP2 = string.IsNullOrEmpty(reader["FInjustificadaP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP2"]),
                            AtrasosP2 = string.IsNullOrEmpty(reader["AtrasosP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP2"]),
                            FInjustificadaP3 = string.IsNullOrEmpty(reader["FInjustificadaP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP3"]),
                            FJustificadaP3 = string.IsNullOrEmpty(reader["FJustificadaP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP3"]),
                            AtrasosP3 = string.IsNullOrEmpty(reader["AtrasosP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP3"]),
                            FJustificadaP4 = string.IsNullOrEmpty(reader["FJustificadaP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP4"]),
                            FInjustificadaP4 = string.IsNullOrEmpty(reader["FInjustificadaP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP4"]),
                            AtrasosP4 = string.IsNullOrEmpty(reader["AtrasosP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP4"]),
                            FJustificadaP5 = string.IsNullOrEmpty(reader["FJustificadaP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP5"]),
                            FInjustificadaP5 = string.IsNullOrEmpty(reader["FInjustificadaP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP5"]),
                            AtrasosP5 = string.IsNullOrEmpty(reader["AtrasosP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP5"]),
                            FJustificadaP6 = string.IsNullOrEmpty(reader["FJustificadaP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP6"]),
                            FInjustificadaP6 = string.IsNullOrEmpty(reader["FInjustificadaP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP6"]),
                            AtrasosP6 = string.IsNullOrEmpty(reader["AtrasosP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP6"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString()
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
        public aca_MatriculaAsistencia_Info getInfo(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaAsistencia_Info info = new aca_MatriculaAsistencia_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT ma.IdEmpresa, ma.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, ma.FInjustificadaP1, ma.FJustificadaP1, ma.AtrasosP1, ma.FInjustificadaP2,  "
                    + " ma.FJustificadaP2, ma.AtrasosP2, ma.FInjustificadaP3, ma.FJustificadaP3, ma.AtrasosP3, ma.FInjustificadaP4, ma.FJustificadaP4, ma.AtrasosP4, ma.FInjustificadaP5, ma.FJustificadaP5, ma.AtrasosP5, ma.FInjustificadaP6, "
                    + " ma.FJustificadaP6, ma.AtrasosP6, cp.IdProfesorTutor, cp.IdProfesorInspector "
                    + " FROM dbo.aca_MatriculaAsistencia AS ma INNER JOIN "
                    + " dbo.aca_Matricula AS m ON ma.IdEmpresa = m.IdEmpresa AND ma.IdMatricula = m.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo "
                    + " WHERE(NOT EXISTS "
                    + " (SELECT IdEmpresa "
                    + " FROM      dbo.aca_AlumnoRetiro AS f "
                    + " WHERE(IdEmpresa = ma.IdEmpresa) AND(IdMatricula = ma.IdMatricula) AND(Estado = 1))) "
                    + " AND ma.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdMatricula = " + IdMatricula.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_MatriculaAsistencia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            FJustificadaP1 = string.IsNullOrEmpty(reader["FJustificadaP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP1"]),
                            FInjustificadaP1 = string.IsNullOrEmpty(reader["FInjustificadaP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP1"]),
                            AtrasosP1 = string.IsNullOrEmpty(reader["AtrasosP1"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP1"]),
                            FJustificadaP2 = string.IsNullOrEmpty(reader["FJustificadaP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP2"]),
                            FInjustificadaP2 = string.IsNullOrEmpty(reader["FInjustificadaP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP2"]),
                            AtrasosP2 = string.IsNullOrEmpty(reader["AtrasosP2"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP2"]),
                            FInjustificadaP3 = string.IsNullOrEmpty(reader["FInjustificadaP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP3"]),
                            FJustificadaP3 = string.IsNullOrEmpty(reader["FJustificadaP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP3"]),
                            AtrasosP3 = string.IsNullOrEmpty(reader["AtrasosP3"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP3"]),
                            FJustificadaP4 = string.IsNullOrEmpty(reader["FJustificadaP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP4"]),
                            FInjustificadaP4 = string.IsNullOrEmpty(reader["FInjustificadaP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP4"]),
                            AtrasosP4 = string.IsNullOrEmpty(reader["AtrasosP4"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP4"]),
                            FJustificadaP5 = string.IsNullOrEmpty(reader["FJustificadaP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP5"]),
                            FInjustificadaP5 = string.IsNullOrEmpty(reader["FInjustificadaP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP5"]),
                            AtrasosP5 = string.IsNullOrEmpty(reader["AtrasosP5"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP5"]),
                            FJustificadaP6 = string.IsNullOrEmpty(reader["FJustificadaP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["FJustificadaP6"]),
                            FInjustificadaP6 = string.IsNullOrEmpty(reader["FInjustificadaP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["FInjustificadaP6"]),
                            AtrasosP6 = string.IsNullOrEmpty(reader["AtrasosP6"].ToString()) ? (int?)null : Convert.ToInt32(reader["AtrasosP6"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString()
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_MatriculaAsistencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaAsistencia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        Codigo = Entity.Codigo,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        FJustificadaP1 = Entity.FJustificadaP1,
                        FInjustificadaP1 = Entity.FInjustificadaP1,
                        AtrasosP1 = Entity.AtrasosP1,
                        FJustificadaP2 = Entity.FJustificadaP2,
                        FInjustificadaP2 = Entity.FInjustificadaP2,
                        AtrasosP2 = Entity.AtrasosP2,
                        FInjustificadaP3 = Entity.FInjustificadaP3,
                        FJustificadaP3 = Entity.FJustificadaP3,
                        AtrasosP3 = Entity.AtrasosP3,
                        FJustificadaP4 = Entity.FJustificadaP4,
                        FInjustificadaP4 = Entity.FInjustificadaP4,
                        AtrasosP4 = Entity.AtrasosP4,
                        FJustificadaP5 = Entity.FJustificadaP5,
                        FInjustificadaP5 = Entity.FInjustificadaP5,
                        AtrasosP5 = Entity.AtrasosP5,
                        FJustificadaP6 = Entity.FJustificadaP6,
                        FInjustificadaP6 = Entity.FInjustificadaP6,
                        AtrasosP6 = Entity.AtrasosP6
                    };
                }
                */
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificar(aca_MatriculaAsistencia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaAsistencia EntityAsistencia = Context.aca_MatriculaAsistencia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula);
                    if (EntityAsistencia == null)
                        return false;

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P1))
                    {
                        EntityAsistencia.FInjustificadaP1 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP1 = info.FJustificada;
                        EntityAsistencia.AtrasosP1 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P2))
                    {
                        EntityAsistencia.FInjustificadaP2 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP2 = info.FJustificada;
                        EntityAsistencia.AtrasosP2 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P3))
                    {
                        EntityAsistencia.FInjustificadaP3 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP3 = info.FJustificada;
                        EntityAsistencia.AtrasosP3 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P4))
                    {
                        EntityAsistencia.FInjustificadaP4 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP4 = info.FJustificada;
                        EntityAsistencia.AtrasosP4 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P5))
                    {
                        EntityAsistencia.FInjustificadaP5 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP5 = info.FJustificada;
                        EntityAsistencia.AtrasosP5 = info.Atrasos;
                    }

                    if (info.IdCatalogoParcial == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademicoParcial.P6))
                    {
                        EntityAsistencia.FInjustificadaP6 = info.FInjustificada;
                        EntityAsistencia.FJustificadaP6 = info.FJustificada;
                        EntityAsistencia.AtrasosP6 = info.Atrasos;
                    }

                    EntityAsistencia.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    EntityAsistencia.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool generarCalificacion(List<aca_MatriculaAsistencia_Info> lst_asistencia)
        {
            try
            {
                List<aca_MatriculaAsistencia> Lista = new List<aca_MatriculaAsistencia>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var info in lst_asistencia)
                    {
                        var lista_calificacion_asistencia = Context.aca_MatriculaAsistencia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatricula == info.IdMatricula).ToList();
                        Context.aca_MatriculaAsistencia.RemoveRange(lista_calificacion_asistencia);

                        aca_MatriculaAsistencia Entity = new aca_MatriculaAsistencia
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdMatricula = info.IdMatricula,
                            FInjustificadaP1 = info.FInjustificadaP1,
                            FJustificadaP1 = info.FJustificadaP1,
                            AtrasosP1 = info.AtrasosP1,
                            FInjustificadaP2 = info.FInjustificadaP2,
                            FJustificadaP2 = info.FJustificadaP2,
                            AtrasosP2 = info.AtrasosP2,
                            FInjustificadaP3 = info.FInjustificadaP3,
                            FJustificadaP3 = info.FJustificadaP3,
                            AtrasosP3 = info.AtrasosP3,
                            FInjustificadaP4 = info.FInjustificadaP4,
                            FJustificadaP4 = info.FJustificadaP4,
                            AtrasosP4 = info.AtrasosP4,
                            FInjustificadaP5 = info.FInjustificadaP5,
                            FJustificadaP5 = info.FJustificadaP5,
                            AtrasosP5 = info.AtrasosP5,
                            FInjustificadaP6 = info.FInjustificadaP6,
                            FJustificadaP6 = info.FJustificadaP6,
                            AtrasosP6 = info.AtrasosP6,
                            IdUsuarioCreacion = info.IdUsuarioCreacion,
                            FechaCreacion = info.FechaCreacion,
                            IdUsuarioModificacion = info.IdUsuarioModificacion,
                            FechaModificacion = info.FechaModificacion
                        };

                        Context.aca_MatriculaAsistencia.Add(Entity);
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
