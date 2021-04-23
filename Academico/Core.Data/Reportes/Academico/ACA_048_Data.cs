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
    public class ACA_048_Data
    {
        public List<ACA_048_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria)
        {
            try
            {
                List<ACA_048_Info> Lista = new List<ACA_048_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT pp.IdEmpresa, pp.IdAnio, pp.IdSede, pp.IdNivel, pp.IdJornada, pp.IdCurso, pp.IdParalelo, a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada,  "
                    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, pp.IdMateria, pp.IdProfesor, cma.NomMateria, p.pe_nombreCompleto NombreProfesor "
                    + " FROM dbo.aca_AnioLectivo_Paralelo_Profesor AS pp WITH (nolock) INNER JOIN "
                        + " dbo.aca_AnioLectivo AS a WITH (nolock) ON pp.IdEmpresa = a.IdEmpresa AND pp.IdAnio = a.IdAnio  LEFT OUTER JOIN "
                        + " dbo.aca_AnioLectivo_Curso_Materia AS cma WITH (nolock) ON pp.IdEmpresa = cma.IdEmpresa AND pp.IdAnio = cma.IdAnio AND pp.IdSede = cma.IdSede AND pp.IdNivel = cma.IdNivel AND pp.IdJornada = cma.IdJornada AND "
                        + " pp.IdCurso = cma.IdCurso AND pp.IdMateria = cma.IdMateria LEFT OUTER JOIN "
                        + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON pp.IdEmpresa = cp.IdEmpresa AND pp.IdAnio = cp.IdAnio AND pp.IdSede = cp.IdSede AND pp.IdNivel = cp.IdNivel AND pp.IdJornada = cp.IdJornada AND pp.IdCurso = cp.IdCurso AND "
                        + " pp.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                        + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada AND cp.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                        + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON jc.IdEmpresa = nj.IdEmpresa AND jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel AND jc.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                        + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) ON nj.IdEmpresa = sn.IdEmpresa AND nj.IdAnio = sn.IdAnio AND nj.IdSede = sn.IdSede AND nj.IdNivel = sn.IdNivel "
                    + " left outer join aca_Profesor pro WITH (nolock) on pro.IdEmpresa = pp.IdEmpresa and pro.IdProfesor = pp.IdProfesor "
                    + " left outer join tb_persona p WITH (nolock) on pro.IdPersona = p.IdPersona "
                    + " where cp.IdEmpresa = " + IdEmpresa
                    + " and cp.IdAnio = " + IdAnio
                    + " and cp.IdSede = " + IdSede
                    + " and cp.IdNivel = " + IdNivel
                    + " and cp.IdJornada = " + IdJornada
                    + " and cp.IdCurso = " + IdCurso
                    + " and cp.IdParalelo = " + IdParalelo
                    + " and cma.IdMateria = " + IdMateria;
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_048_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
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
                            NombreProfesor = reader["NombreProfesor"].ToString(),
                            NomMateria = reader["NomMateria"].ToString()

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
