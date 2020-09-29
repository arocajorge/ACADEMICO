using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_036_Deuda_Data
    {
        public List<ACA_036_Deuda_Info> get_list(int IdEmpresa,int IdAnio, decimal IdAlumno)
        {
            try
            {
                List<ACA_036_Deuda_Info> Lista = new List<ACA_036_Deuda_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT IdEmpresa, IdAnio, IdCliente, IdAlumno, Referencia, Saldo, SaldoProntoPago, FechaProntoPago"
                    + " FROM vwcxc_cartera_x_cobrar"
                    + " WHERE "
                    + " IdEmpresa = " + IdEmpresa.ToString()
                    + " and IdAnio = " + IdAnio.ToString()
                    + " and IdAlumno = " + IdAlumno.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_036_Deuda_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdCliente = Convert.ToDecimal(reader["IdCliente"]),
                            Saldo = Convert.ToDouble(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDouble(reader["SaldoProntoPago"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            Referencia = reader["Referencia"].ToString(),
                            FechaProntoPago = Convert.ToDateTime(reader["FechaProntoPago"])
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
