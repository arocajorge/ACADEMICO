using Core.Data.Base;
using Core.Info.Academico;
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
    }
}
