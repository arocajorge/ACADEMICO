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
    public class ACA_029_Rendimiento_Data
    {
        public List<ACA_029_Rendimiento_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoTipo)
        {
            try
            {
                List<ACA_029_Rendimiento_Info> Lista = new List<ACA_029_Rendimiento_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "declare @IdQuimestre int = "+IdCatalogoTipo
                    +" select A.IdEmpresa, A.IdAnio, A.Codigo, A.Descripcion, A.ValorMinimo, A.ValorMaximo, A.IdSede, A.IdNivel, A.IdJornada, A.IdCurso,"
                    +" A.IdParalelo, A.IdMateria, A.CantAlumnos, b.Promedio as CantidadEnRango, dbo.BankersRounding((cast(b.Promedio as float) / cast(A.CantAlumnos as float)) * 100.00, 2) Porcentaje"
                    +" from("
                    +" select A.IdEmpresa, A.IdAnio, a.Codigo, a.Descripcion, a.ValorMinimo, a.ValorMaximo, B.IdSede, B.IdNivel, B.IdJornada, B.IdCurso, B.IdParalelo, c.IdMateria, COUNT(B.IdMatricula) CantAlumnos, a.IdEquivalenciaPromedio"
                    + " from aca_AnioLectivoEquivalenciaPromedio as a with (nolock) LEFT JOIN"
                    + " aca_Matricula AS B with (nolock) ON A.IdEmpresa = B.IdEmpresa AND A.IdAnio = B.IdAnio left join"
                    + " aca_MatriculaCalificacion as C with (nolock) on b.IdEmpresa = c.IdEmpresa and b.IdMatricula = c.IdMatricula"
                    + " where not exists("
                    + " select f.IdEmpresa from aca_AlumnoRetiro as f with (nolock) where b.IdEmpresa = f.IdEmpresa and b.IdMatricula = f.IdMatricula and f.Estado = 1"
                    + " )"
                    + " and B.IdEmpresa =" + IdEmpresa+ " and b.IdAnio = " + IdAnio + "and b.IdSede = " + IdSede + " and b.IdNIvel = " + IdNivel 
                    + " and b.IdJornada = " + IdJornada + " and b.idcurso = " + IdCurso + " and b.IdParalelo = " + IdParalelo + " and c.IdMateria = " + IdMateria
                    + " GROUP BY A.IdEmpresa, A.IdAnio, a.Codigo, a.Descripcion, a.ValorMinimo, a.ValorMaximo, B.IdSede, B.IdNivel, B.IdJornada, B.IdCurso, B.IdParalelo, c.IdMateria, a.IdEquivalenciaPromedio"
                    + " ) A LEFT JOIN"
                    + " ("
                    + " SELECT A.IdEmpresa, A.IdAnio, A.IdSede, A.IdNivel, A.IdJornada, A.IdCurso, A.IdParalelo, B.IdMateria, c.IdEquivalenciaPromedio, count(1) as Promedio"
                    + " FROM aca_Matricula AS A with (nolock) INNER JOIN"
                    + " aca_MatriculaCalificacion AS B with (nolock) ON A.IdEmpresa = B.IdEmpresa AND A.IdMatricula = B.IdMatricula inner join"
                    + " aca_AnioLectivoEquivalenciaPromedio as c with (nolock) on c.IdEmpresa = a.IdEmpresa and c.IdAnio = a.IdAnio and CASE WHEN @IdQuimestre = 6 then B.PromedioFinalQ1 else B.PromedioFinalQ2 END between c.ValorMinimo and c.ValorMaximo"
                    + " where not exists("
                    + " select f.IdEmpresa from aca_AlumnoRetiro as f with (nolock) "
                    + " where b.IdEmpresa = f.IdEmpresa and b.IdMatricula = f.IdMatricula and f.Estado = 1"
                    + " ) AND A.IdEmpresa = " + IdEmpresa + " and A.IdAnio = " + IdAnio + " and A.IdSede = " + IdSede + " and A.IdNIvel = " +IdNivel 
                    + " and A.idjornada = " + IdJornada + " and A.IdCurso = " + IdCurso + " and A.IdParalelo = " + IdParalelo + " and B.IdMateria = " + IdMateria
                    + " group by A.IdEmpresa, A.IdAnio, A.IdSede, A.IdNivel, A.IdJornada, A.IdCurso, A.IdParalelo, B.IdMateria, c.IdEquivalenciaPromedio"
                    + " ) B ON A.IdEmpresa = B.IdEmpresa AND A.IdAnio = B.IdAnio AND A.IdSede = B.IdSede AND A.IdNivel = B.IdNivel AND A.IdJornada = B.IdJornada AND A.IdCurso = B.IdCurso AND A.IdParalelo = B.IdParalelo AND A.IdMateria = B.IdMateria and a.IdEquivalenciaPromedio = b.IdEquivalenciaPromedio"
                    + " ORDER BY A.ValorMinimo DESC";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_029_Rendimiento_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            Codigo = reader["Codigo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            ValorMinimo = Convert.ToDecimal(reader["ValorMinimo"]),
                            ValorMaximo = Convert.ToDecimal(reader["ValorMaximo"]),
                            CantAlumnos = Convert.ToInt32(reader["CantAlumnos"]),
                            CantidadEnRango = string.IsNullOrEmpty(reader["CantidadEnRango"].ToString()) ? null : (int?)reader["CantidadEnRango"],
                            Porcentaje = string.IsNullOrEmpty(reader["Porcentaje"].ToString()) ? null : (decimal?)reader["Porcentaje"],
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
