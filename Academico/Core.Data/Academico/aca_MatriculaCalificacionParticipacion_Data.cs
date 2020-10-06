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
    public class aca_MatriculaCalificacionParticipacion_Data
    {
        public List<aca_MatriculaCalificacionParticipacion_Info> getListParalelo(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_MatriculaCalificacionParticipacion_Info> Lista = new List<aca_MatriculaCalificacionParticipacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT m.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, cp.NomParalelo, cp.OrdenParalelo, mp.IdCampoAccion, mp.IdTematica, t.NombreCampoAccion, t.NombreTematica, mp.IdProfesor, pe.pe_nombreCompleto NombreProfesor "
                    + " FROM     dbo.aca_Matricula AS m INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_MatriculaCalificacionParticipacion AS mp ON m.IdEmpresa = mp.IdEmpresa AND m.IdEmpresa = mp.IdEmpresa AND m.IdMatricula = mp.IdMatricula AND m.IdMatricula = mp.IdMatricula "
                    + " LEFT OUTER JOIN aca_AnioLectivo_Tematica t on t.IdEmpresa=mp.IdEmpresa and t.IdCampoAccion=mp.IdCampoAccion and t.IdTematica=mp.IdTematica "
                    + " LEFT OUTER JOIN aca_Profesor p on p.IdEmpresa=mp.IdEmpresa and p.IdProfesor=mp.IdProfesor "
                    + " LEFT OUTER JOIN tb_persona pe on pe.IdPersona = p.IdPersona "
                    + " where m.IdEmpresa = " + IdEmpresa + " and m.IdAnio = " + IdAnio + " and m.IdSede = " + IdSede + " and m.IdNivel = " + IdNivel + " and m.IdJornada = " + IdJornada + " and m.IdCurso = " + IdCurso
                    + " group by m.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, cp.NomParalelo, cp.OrdenParalelo, mp.IdCampoAccion, mp.IdTematica, t.NombreCampoAccion, t.NombreTematica, mp.IdProfesor, pe.pe_nombreCompleto ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParticipacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesor"]),
                            IdTematicaParticipacion = string.IsNullOrEmpty(reader["IdTematica"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdTematica"]),
                            NombreCampoAccion = reader["NombreCampoAccion"].ToString(),
                            NombreTematica = reader["NombreTematica"].ToString(),
                            NombreProfesor = reader["NombreProfesor"].ToString()
                        });
                    }
                    reader.Close();
                }
                Lista.ForEach(q=>q.IdString = q.IdEmpresa.ToString("000")+ q.IdAnio.ToString("000")+ q.IdSede.ToString("000")+ q.IdJornada.ToString("000")+ q.IdNivel.ToString("000")+ q.IdCurso.ToString("000")+ q.IdParalelo.ToString("000"));
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public aca_MatriculaCalificacionParticipacion_Info getInfo_X_Matricula(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                aca_MatriculaCalificacionParticipacion_Info info = new aca_MatriculaCalificacionParticipacion_Info();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MatriculaCalificacionParticipacion.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).FirstOrDefault();

                    if (Entity == null)
                        return null;
                    info = new aca_MatriculaCalificacionParticipacion_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatricula = Entity.IdMatricula,
                        IdTematica = Entity.IdTematica,
                        IdCampoAccion = Entity.IdCampoAccion,
                        IdProfesor = Entity.IdProfesor,
                        CalificacionP1 = Entity.CalificacionP1,
                        CalificacionP2 = Entity.CalificacionP2,
                        CalificacionP3 = Entity.CalificacionP3,
                        CalificacionP4 = Entity.CalificacionP4,
                        PromedioQ1 = Entity.PromedioQ1,
                        PromedioQ2 = Entity.PromedioQ2,
                        PromedioFinal = Entity.PromedioFinal,
                        IdUsuarioCreacion = Entity.IdUsuarioCreacion,
                        IdUsuarioModificacion = Entity.IdUsuarioModificacion
                    };
                }

                return info;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool guardar(List<aca_MatriculaCalificacionParticipacion_Info> lst_calificacion_participacion)
        {
            try
            {
                List<aca_MatriculaCalificacionParticipacion_Info> Lista = new List<aca_MatriculaCalificacionParticipacion_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in lst_calificacion_participacion)
                    {
                        var lst_calificacion= Context.aca_MatriculaCalificacionParticipacion.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacionParticipacion.RemoveRange(lst_calificacion);

                        aca_MatriculaCalificacionParticipacion Entity = new aca_MatriculaCalificacionParticipacion
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdMatricula = item.IdMatricula,
                            IdAlumno = item.IdAlumno,
                            IdCampoAccion = item.IdCampoAccion,
                            IdTematica = item.IdTematica,
                            IdProfesor = item.IdProfesor,
                            CalificacionP1 = item.CalificacionP1,
                            CalificacionP2 = item.CalificacionP2,
                            CalificacionP3 = item.CalificacionP3,
                            CalificacionP4 = item.CalificacionP4,
                            PromedioQ1 = item.PromedioQ1,
                            PromedioQ2 = item.PromedioQ2,
                            PromedioFinal = item.PromedioFinal,
                            IdUsuarioCreacion = item.IdUsuarioCreacion,
                            FechaCreacion = item.FechaCreacion,
                            IdUsuarioModificacion = item.IdUsuarioModificacion,
                            FechaModificacion = item.FechaModificacion
                        };
                        Context.aca_MatriculaCalificacionParticipacion.Add(Entity);
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

        public List<aca_MatriculaCalificacionParticipacion_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede, decimal IdProfesor, bool EsSuperAdmin)
        {
            try
            {
                List<aca_MatriculaCalificacionParticipacion_Info> Lista = new List<aca_MatriculaCalificacionParticipacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdCampoAccion, mc.IdTematica, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, "
                    + " sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, t.NombreCampoAccion,t.NombreTematica, t.OrdenCampoAccion, t.OrdenTematica "
                    + " FROM     dbo.aca_MatriculaCalificacionParticipacion AS mc INNER JOIN "
                    + " dbo.aca_Matricula AS c ON mc.IdEmpresa = c.IdEmpresa AND mc.IdMatricula = c.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a ON c.IdAnio = a.IdAnio AND c.IdEmpresa = a.IdEmpresa "
                    + " left join aca_AnioLectivo_Curso_Paralelo cp on c.IdEmpresa = cp.IdEmpresa AND c.IdAnio = cp.IdAnio AND c.IdSede = cp.IdSede "
                    + " AND c.IdNivel = cp.IdNivel AND c.IdJornada = cp.IdJornada AND c.IdCurso = cp.IdCurso and c.IdParalelo = cp.IdParalelo "
                    + " left join aca_AnioLectivo_Jornada_Curso jc on c.IdEmpresa = jc.IdEmpresa AND c.IdAnio = jc.IdAnio AND c.IdSede = jc.IdSede "
                    + " AND c.IdNivel = jc.IdNivel AND c.IdJornada = jc.IdJornada AND c.IdCurso = jc.IdCurso "
                    + " left join aca_AnioLectivo_NivelAcademico_Jornada nj on c.IdEmpresa = nj.IdEmpresa AND c.IdAnio = nj.IdAnio AND c.IdSede = nj.IdSede "
                    + " AND c.IdNivel = nj.IdNivel AND c.IdJornada = nj.IdJornada "
                    + " left join aca_AnioLectivo_Sede_NivelAcademico sn on c.IdEmpresa = sn.IdEmpresa AND c.IdAnio = sn.IdAnio AND c.IdSede = sn.IdSede "
                    + " AND c.IdNivel = sn.IdNivel "
                    + " left join aca_AnioLectivo_Tematica t on t.IdEmpresa = c.IdEmpresa and t.IdAnio = c.IdAnio and t.IdCampoAccion = mc.IdCampoAccion and t.IdTematica = mc.IdTematica "
                    + " WHERE mc.IdEmpresa= " + IdEmpresa + " and c.IdSede= " + IdSede + " and c.IdAnio= " + IdAnio + (EsSuperAdmin==true ? "" : " and mc.IdProfesor = " + IdProfesor)
                    + " GROUP BY mc.IdEmpresa, mc.IdCampoAccion, mc.IdTematica, mc.IdProfesor, c.IdAnio, c.IdSede, c.IdNivel, c.IdJornada, c.IdCurso, c.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, "
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, t.NombreCampoAccion,t.NombreTematica, t.OrdenCampoAccion, t.OrdenTematica ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParticipacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesor"]),
                            IdTematica = Convert.ToInt32(reader["IdTematica"]),
                            IdCampoAccion = Convert.ToInt32(reader["IdCampoAccion"]),
                            NombreCampoAccion = reader["NombreCampoAccion"].ToString(),
                            NombreTematica = reader["NombreTematica"].ToString(),
                            OrdenCampoAccion = Convert.ToInt32(reader["OrdenCampoAccion"]),
                            OrdenTematica = Convert.ToInt32(reader["OrdenTematica"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"])

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

        public List<aca_MatriculaCalificacionParticipacion_Info> getList_Calificaciones(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCampoAccion, int IdTematica, int IdCatalogoParcialTipo, decimal IdProfesor)
        {
            try
            {
                List<aca_MatriculaCalificacionParticipacion_Info> Lista = new List<aca_MatriculaCalificacionParticipacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mp.IdEmpresa, mp.IdAlumno, mp.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, "
                    + " mp.IdCampoAccion, mp.IdTematica, mp.IdProfesor,p.pe_nombreCompleto NombreAlumno, mp.CalificacionP1, mp.CalificacionP2, mp.PromedioQ1, "
                    + " mp.CalificacionP3, mp.CalificacionP4, mp.PromedioQ2, mp.PromedioFinal "
                    + " FROM dbo.aca_MatriculaCalificacionParticipacion AS mp LEFT OUTER JOIN "
                    + " dbo.aca_Matricula AS m ON mp.IdEmpresa = m.IdEmpresa AND mp.IdEmpresa = m.IdEmpresa AND mp.IdMatricula = m.IdMatricula AND mp.IdMatricula = m.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona "
                    + " WHERE mp.IdEmpresa = " + IdEmpresa + " and m.IdSede = " + IdSede + " and m.IdAnio = " + IdAnio + " and m.IdNivel = " + IdNivel
                    + " and m.IdJornada = " + IdJornada + " and m.IdCurso = " + IdCurso + " and m.IdParalelo = " + IdParalelo + " and mc.IdCampoAccion = " + IdCampoAccion
                    + " and mc.IdTematica = " + IdTematica + " and mc.IdProfesor = " + IdProfesor;
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParticipacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToInt32(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesor"]),
                            IdTematica = Convert.ToInt32(reader["IdTematica"]),
                            IdCampoAccion = Convert.ToInt32(reader["IdCampoAccion"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
                            CalificacionP1 = string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"]),
                            CalificacionP2 = string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"]),
                            CalificacionP3 = string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"]),
                            CalificacionP4 = string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ1"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ2"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioFinal"]),
                            Calificacion1 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"])
                                                : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"])
                                                    : (decimal?)null)),
                            Calificacion2 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"])
                                                : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"])
                                                    : (decimal?)null)),
                            Promedio = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ1"])
                                                : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ2"])
                                                    : (decimal?)null))
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
        public List<aca_MatriculaCalificacionParticipacion_Info> getList_Calificaciones_SuperAdmin(int IdEmpresa, int IdSede, int IdAnio, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCampoAccion, int IdTematica, int IdCatalogoParcialTipo)
        {
            try
            {
                List<aca_MatriculaCalificacionParticipacion_Info> Lista = new List<aca_MatriculaCalificacionParticipacion_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT mp.IdEmpresa, mp.IdAlumno, mp.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, "
                    + " mp.IdCampoAccion, mp.IdTematica, mp.IdProfesor,p.pe_nombreCompleto NombreAlumno, mp.CalificacionP1, mp.CalificacionP2, mp.PromedioQ1, "
                    + " mp.CalificacionP3, mp.CalificacionP4, mp.PromedioQ2, mp.PromedioFinal "
                    + " FROM dbo.aca_MatriculaCalificacionParticipacion AS mp LEFT OUTER JOIN "
                    + " dbo.aca_Matricula AS m ON mp.IdEmpresa = m.IdEmpresa AND mp.IdEmpresa = m.IdEmpresa AND mp.IdMatricula = m.IdMatricula AND mp.IdMatricula = m.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona "
                                        + " WHERE mp.IdEmpresa = " + IdEmpresa + " and m.IdSede = " + IdSede + " and m.IdAnio = " + IdAnio + " and m.IdNivel = " + IdNivel
                    + " and m.IdJornada = " + IdJornada + " and m.IdCurso = " + IdCurso + " and m.IdParalelo = " + IdParalelo + " and mc.IdCampoAccion = " + IdCampoAccion
                    + " and mc.IdTematica = " + IdTematica;
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionParticipacion_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToInt32(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesor"]),
                            IdTematica = Convert.ToInt32(reader["IdTematica"]),
                            IdCampoAccion = Convert.ToInt32(reader["IdCampoAccion"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
                            CalificacionP1 = string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"]),
                            CalificacionP2 = string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"]),
                            CalificacionP3 = string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"]),
                            CalificacionP4 = string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ1"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ2"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioFinal"]),
                            Calificacion1 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"])
                                                : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"])
                                                    : (decimal?)null)),
                            Calificacion2 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"])
                                                : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"])
                                                    : (decimal?)null)),
                            Promedio = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ1"])
                                                : (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ2"])
                                                    : (decimal?)null))
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
    }
}
