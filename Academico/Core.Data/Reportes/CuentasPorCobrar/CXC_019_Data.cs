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
    public class CXC_019_Data
    {
        public List<CXC_019_Info> GetList(int IdEmpresa, decimal IdAlumno, DateTime fecha_ini, DateTime fecha_fin, bool mostrarAnulados)
        {
            try
            {
                List<CXC_019_Info> Lista = new List<CXC_019_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "SELECT conv.IdEmpresa, conv.IdConvenio, conv.IdAlumno, conv.IdMatricula, conv.IdPersonaConvenio, p.pe_nombreCompleto AS PersonaConvenio, "
                    + " conv.Fecha, conv.FechaPrimerPago, conv.Valor, conv.Estado,conv.Observacion, a.Codigo, conv.NumCuotas, p_al.pe_nombreCompleto AS Alumno, conv.IdUsuarioCreacion as IdUsuario, al.Descripcion "
                    + " FROM dbo.cxc_Convenio AS conv "
                    + " INNER JOIN dbo.aca_Alumno AS a ON conv.IdEmpresa = a.IdEmpresa AND conv.IdAlumno = a.IdAlumno "
                    + " INNER JOIN  dbo.tb_persona AS p ON conv.IdPersonaConvenio = p.IdPersona "
                    + " LEFT OUTER JOIN dbo.tb_persona AS p_al ON a.IdPersona = p_al.IdPersona "
                    + " LEFT OUTER JOIN aca_Matricula m on m.IdEmpresa = conv.IdEmpresa and m.IdMatricula = conv.IdMatricula "
                    + " LEFT OUTER JOIN aca_AnioLectivo al on al.IdEmpresa = m.IdEmpresa and al.IdAnio = m.IdAnio "
                    + " WHERE conv.IdEmpresa = " + IdEmpresa + " and conv.Fecha between " + "'" + fecha_ini.ToString("dd/MM/yyyy") + "'" + " and " + "'" + fecha_fin.Date.ToString("dd/MM/yyyy") + "'";
                    if (IdAlumno!=0)
                    {
                        query += " and conv.IdAlumno  = " + IdAlumno;
                    }
                    if (mostrarAnulados ==false)
                    {
                        query += " and conv.Estado  = 1";
                    }
                    else
                    {
                        query += " and conv.Estado  = 0";
                    }

                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new CXC_019_Info
                        {
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdConvenio = Convert.ToInt32(reader["IdConvenio"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            IdPersonaConvenio = Convert.ToDecimal(reader["IdPersonaConvenio"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            NumCuotas = Convert.ToInt32(reader["NumCuotas"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            FechaPrimerPago = Convert.ToDateTime(reader["FechaPrimerPago"]),
                            Alumno = reader["Alumno"].ToString(),
                            PersonaConvenio = reader["PersonaConvenio"].ToString(),
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
