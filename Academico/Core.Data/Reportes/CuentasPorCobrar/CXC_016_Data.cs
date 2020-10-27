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
    public class CXC_016_Data
    {
        public List<CXC_016_Info> GetList(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<CXC_016_Info> Lista = new List<CXC_016_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    string query = string.Empty;
                    query = "select pag.IdEmpresa, pag.IdPagare, pag.IdAlumno, pag.FechaAPagar, pag.IdPersonaPagare, p.pe_nombreCompleto, pag.Valor, p.pe_cedulaRuc, "
                    + " p.pe_celular, p.pe_direccion, p.pe_correo from cxc_Pagare pag "
                    + " left join tb_persona p on pag.IdPersonaPagare = p.IdPersona "
                    + " where pag.IdEmpresa= " + IdEmpresa + " and pag.IdAlumno = "+ IdAlumno + " and pag.Estado = 1 ";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var entero = Convert.ToInt32(Math.Truncate(Convert.ToDouble(reader["Valor"])));
                        var decimales = Convert.ToInt32(Math.Round((Convert.ToDouble(reader["Valor"]) - entero) * 100, 2));

                        Lista.Add(new CXC_016_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPagare = Convert.ToInt32(reader["IdPagare"]),
                            IdAlumno = Convert.ToInt32(reader["IdAlumno"]),
                            IdPersonaPagare = Convert.ToDecimal(reader["IdPersonaPagare"]),
                            FechaAPagar = Convert.ToDateTime(reader["FechaAPagar"]),
                            pe_nombreCompleto = Convert.ToString(reader["pe_nombreCompleto"]),
                            pe_cedulaRuc = Convert.ToString(reader["pe_cedulaRuc"]),
                            pe_celular = Convert.ToString(reader["pe_celular"]),
                            pe_direccion = Convert.ToString(reader["pe_direccion"]),
                            pe_correo = Convert.ToString(reader["pe_correo"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            ValorString = Convert.ToDouble(reader["Valor"]).ToString("C2"),
                            ValorTexto = entero.ToString() + " con " + decimales.ToString() +"/100",
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
                            dia = Convert.ToDateTime(reader["FechaAPagar"]).ToString("dd"),
                            mes = Convert.ToDateTime(reader["FechaAPagar"]).ToString("MMMM"),
                            anio = Convert.ToDateTime(reader["FechaAPagar"]).ToString("yyyy")
                        });
                    }
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
