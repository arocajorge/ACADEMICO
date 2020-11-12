using Core.Info.Helps;
using Core.Info.Reportes.Contabilidad;
using Core.Info.Reportes.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Contabilidad
{
    public class CXC_018_Data
    {
        public List<CXC_018_Info> GetList(int IdEmpresa, decimal IdAlumno, DateTime fecha_ini, DateTime fecha_fin, bool mostrarAnulados)
        {
            try
            {
                List<CXC_018_Info> Lista = new List<CXC_018_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT pag.IdEmpresa, pag.IdPagare, pag.IdAlumno, pag.IdMatricula, pag.IdPersonaPagare, p.pe_nombreCompleto AS PersonaPagare, pag.FechaAPagar, pag.Valor, pag.Estado,a.Codigo, p_al.pe_nombreCompleto AS Alumno "
                    + " FROM dbo.cxc_Pagare AS pag INNER JOIN "
                    + " dbo.aca_Alumno AS a ON pag.IdEmpresa = a.IdEmpresa AND pag.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON pag.IdPersonaPagare = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p_al ON a.IdPersona = p_al.IdPersona "
                    + " WHERE pag.IdEmpresa = " + IdEmpresa + " and pag.FechaAPagar between " + "'" + fecha_ini.ToString("dd/MM/yyyy") + "'" + " and " + "'" + fecha_fin.Date.ToString("dd/MM/yyyy") + "'";
                    if (IdAlumno!=0)
                    {
                        query += " and pag.IdAlumno  = " + IdAlumno;
                    }
                    if (mostrarAnulados ==false)
                    {
                        query += " and pag.Estado  = 1";
                    }

                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new CXC_018_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPagare = Convert.ToInt32(reader["IdPagare"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            IdPersonaPagare = Convert.ToDecimal(reader["IdPersonaPagare"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            FechaAPagar = Convert.ToDateTime(reader["FechaAPagar"]),
                            Alumno = reader["Alumno"].ToString(),
                            PersonaPagare = reader["PersonaPagare"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
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
