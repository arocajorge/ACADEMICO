using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_038_Rubros_Data
    {
        public List<ACA_038_Rubros_Info> get_list(int IdEmpresa,int IdAnio, decimal IdMatricula)
        {
            try
            {
                List<ACA_038_Rubros_Info> Lista = new List<ACA_038_Rubros_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdJornada, a.IdNivel, a.IdCurso, a.IdParalelo, a.IdMatricula, a.IdRubro, "
                        + " case when(a.MaximaSecuencia = a.NumeroCuotas) then a.NomRubro "
                        + " else a.NomRubro + ' ' + CAST(a.MaximaSecuencia as varchar) + '/' + CAST(a.NumeroCuotas as varchar) end Pension, "
                         + " Total, cast (dbo.BankersRounding((total * MaximaSecuencia), 2) as numeric(18, 2)) as TotalReporte "
                         + " from "
                        + " ( "
                        + " SELECT mr.IdEmpresa, mr.IdAnio, mr.IdSede, mr.IdJornada, mr.IdNivel, mr.IdCurso, mr.IdParalelo, mr.IdMatricula, mr.IdRubro, "
                        + " ar.NomRubro, max(rp.Secuencia) MaximaSecuencia, ar.NumeroCuotas, mr.Total "
                        + " FROM     dbo.aca_Matricula_Rubro AS mr INNER JOIN "
                                          + " dbo.aca_AnioLectivo_Rubro_Periodo AS rp ON mr.IdEmpresa = rp.IdEmpresa AND mr.IdAnio = rp.IdAnio AND mr.IdPeriodo = rp.IdPeriodo AND mr.IdRubro = rp.IdRubro INNER JOIN "
                                          + " dbo.aca_AnioLectivo_Rubro AS ar ON rp.IdEmpresa = ar.IdEmpresa AND rp.IdAnio = ar.IdAnio AND rp.IdRubro = ar.IdRubro INNER JOIN "
                                          + " dbo.aca_AnioLectivo_Periodo AS ap ON rp.IdEmpresa = ap.IdEmpresa AND rp.IdPeriodo = ap.IdPeriodo "
                        + " WHERE(mr.FechaFacturacion IS NOT NULL) "
                        + " group by "
                        + " mr.IdEmpresa, mr.IdAnio, mr.IdSede, mr.IdJornada, mr.IdNivel, mr.IdCurso, mr.IdParalelo, mr.IdMatricula, mr.IdRubro, ar.NomRubro, ar.NumeroCuotas, mr.Total "
                        + " ) a"
                    + " WHERE "
                    + " a.IdEmpresa = " + IdEmpresa.ToString()
                    + " and a.IdAnio = " + IdAnio.ToString()
                    + " and a.IdMatricula = " + IdMatricula.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_038_Rubros_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            TotalReporte = Convert.ToDouble(reader["TotalReporte"]),
                            Total = Convert.ToDouble(reader["Total"]),
                            Pension = reader["Pension"].ToString()
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
