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
    public class ACA_050_Promedios_Data
    {
        public List<ACA_050_Promedios_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<ACA_050_Promedios_Info> Lista = new List<ACA_050_Promedios_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT a.IdEmpresa, a.IdAnio, a.IdMatricula, a.IdAlumno, a.Promedio, a.SecuenciaConducta, c.Letra, dbo.aca_Curso.NomCurso, dbo.aca_Curso.OrdenCurso, d.Descripcion "
                    + " FROM dbo.aca_AnioLectivoCalificacionHistorico AS a INNER JOIN "
                    + " dbo.aca_Matricula AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdMatricula = b.IdMatricula INNER JOIN "
                    + " dbo.aca_AnioLectivo AS d ON d.IdEmpresa = b.IdEmpresa AND d.IdAnio = b.IdAnio INNER JOIN "
                    + " dbo.aca_Curso ON a.IdEmpresa = dbo.aca_Curso.IdEmpresa AND a.IdCurso = dbo.aca_Curso.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS c ON a.IdEmpresa = c.IdEmpresa AND b.IdAnio = c.IdAnio AND c.Secuencia = a.SecuenciaConducta "
                    + " WHERE a.IdEmpresa = " + IdEmpresa + " AND a.IdAlumno = " + IdAlumno;
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_050_Promedios_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            Promedio = string.IsNullOrEmpty(reader["Promedio"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Promedio"]),
                            Letra = reader["Letra"].ToString()
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
