using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_056_Data
    {
        public List<ACA_056_Info> GetList(int IdEmpresa)
        {
            try
            {
                List<ACA_056_Info> Lista = new List<ACA_056_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT pr.IdEmpresa, pr.IdProfesor, pr.IdPersona, p.pe_cedulaRuc, p.pe_nombreCompleto, p.pe_fechaNacimiento, p.pe_sexo, p.IdProfesion, pr.Direccion, pr.Telefonos, pr.Correo, p.pe_celular, pr.EsProfesor, pr.EsInspector, "
                    + " c.ca_descripcion AS Sexo, pf.Descripcion AS Profesion "
                    + " FROM     dbo.aca_Profesor AS pr INNER JOIN "
                    + " dbo.tb_persona AS p ON pr.IdPersona = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_Catalogo AS c ON c.CodCatalogo = p.pe_sexo LEFT OUTER JOIN "
                    + " dbo.tb_profesion AS pf ON pf.IdProfesion = p.IdProfesion "
                    + " WHERE pr.IdEmpresa = " + IdEmpresa.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_056_Info
                        {
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdPersona = Convert.ToInt32(reader["IdPersona"]),
                            IdProfesion = string.IsNullOrEmpty(reader["IdProfesion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdProfesion"]),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefonos = string.IsNullOrEmpty(reader["Telefonos"].ToString()) ? null : reader["Telefonos"].ToString(),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            pe_celular = string.IsNullOrEmpty(reader["pe_celular"].ToString()) ? null : reader["pe_celular"].ToString(),
                            EsProfesor = string.IsNullOrEmpty(reader["EsProfesor"].ToString()) ? false : Convert.ToBoolean(reader["EsProfesor"]),
                            EsInspector = string.IsNullOrEmpty(reader["EsInspector"].ToString()) ? false : Convert.ToBoolean(reader["EsInspector"]),
                            Sexo = string.IsNullOrEmpty(reader["Sexo"].ToString()) ? null : reader["Sexo"].ToString(),
                            Profesion = string.IsNullOrEmpty(reader["Profesion"].ToString()) ? null : reader["Profesion"].ToString(),
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
