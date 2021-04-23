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
    public class ACA_036_Data
    {
        public List<ACA_036_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                List<ACA_036_Info> Lista = new List<ACA_036_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT a.IdEmpresa, a.Codigo, a.IdAlumno, e.IdMatricula, b.pe_nombreCompleto AS NombreAlumno, b.pe_cedulaRuc, e.IdAnio, e.IdSede, e.IdNivel, e.IdJornada, e.IdCurso, e.IdParalelo, f.Descripcion, sn.NomSede, sn.NomNivel, nj.NomJornada, "
                  + " jc.NomCurso, cp.NomParalelo, nj.OrdenJornada, sn.OrdenNivel, jc.OrdenCurso, cp.OrdenParalelo "
                   + " FROM dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) RIGHT OUTER JOIN "
                  + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                  + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                  + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) RIGHT OUTER JOIN "
                  + " dbo.aca_AnioLectivo AS f WITH (nolock) INNER JOIN "
                  + " dbo.aca_Matricula AS e WITH (nolock) ON f.IdEmpresa = e.IdEmpresa AND f.IdAnio = e.IdAnio INNER JOIN "
                  + " dbo.aca_Alumno AS a WITH (nolock) INNER JOIN "
                  + " dbo.tb_persona AS b WITH (nolock) ON a.IdPersona = b.IdPersona ON e.IdEmpresa = a.IdEmpresa AND e.IdAlumno = a.IdAlumno "
                  + " ON cp.IdEmpresa = e.IdEmpresa AND "
                  + " cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede AND cp.IdNivel = e.IdNivel AND cp.IdJornada = e.IdJornada AND cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND "
                  + " jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso "
                  + " LEFT OUTER JOIN "
                  + " ( "
                  + " select r.IdEmpresa, r.IdMatricula "
                  + " from aca_AlumnoRetiro as r WITH (nolock) "
                  + " where r.Estado = 1 "
                  + " ) as ret on e.IdEmpresa = ret.IdEmpresa and e.IdMatricula = ret.IdMatricula "
                  + " WHERE "
                  + " e.IdEmpresa = " + IdEmpresa.ToString()
                  + " and e.IdAnio = " + IdAnio.ToString()
                  + " and e.IdSede = " + IdSede.ToString()
                  + " and e.IdJornada = " + IdJornada.ToString()
                  + " and e.IdNivel = " + IdNivel.ToString()
                  + " and e.IdCurso = " + IdCurso.ToString()
                  + " and e.IdParalelo = " + IdParalelo.ToString()
                  + " and e.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString()
                  + " and(f.Estado = 1) AND(a.Estado = 1) AND(NOT EXISTS "
                        + " (SELECT IdEmpresa "
                            + " FROM      dbo.aca_AlumnoRetiro AS xx WITH (nolock) "
                            + " WHERE(e.IdEmpresa = IdEmpresa) AND(e.IdMatricula = IdMatricula) AND(Estado = 1)))";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_036_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Codigo = reader["Codigo"].ToString(),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
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
