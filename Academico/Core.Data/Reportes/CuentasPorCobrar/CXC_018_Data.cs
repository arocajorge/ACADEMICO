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
                    string query = "SELECT pag.IdEmpresa, pag.IdPagare, pag.IdAlumno, pag.IdMatricula, pag.IdPersonaPagare, p.pe_nombreCompleto AS PersonaPagare, "
                    + " pag.FechaAPagar, pag.Valor, pag.Estado,a.Codigo, p_al.pe_nombreCompleto AS Alumno, pag.Observacion, pag.IdUsuarioCreacion as IdUsuario, al.Descripcion "
                    + " FROM dbo.cxc_Pagare AS pag INNER JOIN "
                    + " dbo.aca_Alumno AS a ON pag.IdEmpresa = a.IdEmpresa AND pag.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON pag.IdPersonaPagare = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p_al ON a.IdPersona = p_al.IdPersona "
                    + " LEFT OUTER JOIN aca_Matricula m on m.IdEmpresa = pag.IdEmpresa and m.IdMatricula =  pag.IdMatricula " 
                    + " LEFT OUTER JOIN aca_AnioLectivo al on al.IdEmpresa = m.IdEmpresa and al.IdAnio = m.IdAnio"
                    + " WHERE pag.IdEmpresa = " + IdEmpresa + " and pag.FechaAPagar between DATEFROMPARTS(" + fecha_ini.Year.ToString() + "," + fecha_ini.Month.ToString() + "," + fecha_ini.Day.ToString() + ") and DATEFROMPARTS(" + fecha_fin.Year.ToString() + "," + fecha_fin.Month.ToString() + "," + fecha_fin.Day.ToString() + ")";
                    if (IdAlumno!=0)
                    {
                        query += " and pag.IdAlumno  = " + IdAlumno;
                    }
                    if (mostrarAnulados ==false)
                    {
                        query += " and pag.Estado  = 1";
                    }
                    else
                    {
                        query += " and pag.Estado  = 0";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new CXC_018_Info
                        {
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPagare = Convert.ToInt32(reader["IdPagare"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            IdPersonaPagare = Convert.ToDecimal(reader["IdPersonaPagare"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            FechaAPagar = Convert.ToDateTime(reader["FechaAPagar"]),
                            Alumno = reader["Alumno"].ToString(),
                            PersonaPagare = reader["PersonaPagare"].ToString(),
                            Descripcion  = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? "" : reader["Descripcion"].ToString(),
                            IdUsuario = string.IsNullOrEmpty(reader["IdUsuario"].ToString()) ? "" : reader["IdUsuario"].ToString(),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? "" : reader["Observacion"].ToString(),
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
