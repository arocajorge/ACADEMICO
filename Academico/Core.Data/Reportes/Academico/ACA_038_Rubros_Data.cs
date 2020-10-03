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
                    + " case when(max(a.MaximaSecuencia) = a.NumeroCuotas) then a.NomRubro  else  "
                    + " a.NomRubro + ' ' + CAST(max(a.MaximaSecuencia) as varchar) + '/' + CAST(a.NumeroCuotas as varchar) end Pension, sum(a.TotalFacturado)TotalFacturado, sum(a.ValorCobrado) TotalCobrado "
                    + " from( "
                    + " SELECT mr.IdEmpresa, mr.IdAnio, mr.IdSede, mr.IdJornada, mr.IdNivel, mr.IdCurso, mr.IdParalelo, mr.IdMatricula, mr.IdRubro, ar.NomRubro, "
                    + " max(rp.Secuencia) MaximaSecuencia, ar.NumeroCuotas, d.Total AS TotalFacturado, ISNULL(SUM(c.dc_ValorPago), 0) AS ValorCobrado "
                    + " FROM     dbo.aca_Matricula_Rubro AS mr "
                    + " INNER JOIN  dbo.aca_AnioLectivo_Rubro_Periodo AS rp ON mr.IdEmpresa = rp.IdEmpresa AND mr.IdAnio = rp.IdAnio AND mr.IdPeriodo = rp.IdPeriodo "
                    + " AND mr.IdRubro = rp.IdRubro INNER JOIN  dbo.aca_AnioLectivo_Rubro AS ar ON rp.IdEmpresa = ar.IdEmpresa AND rp.IdAnio = ar.IdAnio "
                    + " AND rp.IdRubro = ar.IdRubro INNER JOIN  dbo.aca_AnioLectivo_Periodo AS ap ON rp.IdEmpresa = ap.IdEmpresa AND rp.IdPeriodo = ap.IdPeriodo "
                    + " INNER JOIN "
                    + " fa_factura AS b ON mr.IdEmpresa = b.IdEmpresa AND mr.IdSucursal = b.IdSucursal AND mr.IdBodega = b.IdBodega AND mr.IdCbteVta = b.IdCbteVta "
                    + " LEFT OUTER JOIN "
                    + " cxc_cobro_det AS c ON b.IdEmpresa = c.IdEmpresa AND b.IdSucursal = c.IdSucursal AND b.IdBodega = c.IdBodega_Cbte AND b.IdCbteVta = c.IdCbte_vta_nota AND b.vt_tipoDoc = c.dc_TipoDocumento AND "
                    + " c.estado = 'A' INNER JOIN "
                    + " fa_factura_resumen AS d ON b.IdEmpresa = d.IdEmpresa AND b.IdSucursal = d.IdSucursal AND b.IdBodega = d.IdBodega AND b.IdCbteVta = d.IdCbteVta "
                    + " WHERE(mr.FechaFacturacion IS NOT NULL) and "
                    + " mr.IdEmpresa = " + IdEmpresa + " and mr.IdAnio = " + IdAnio + " and mr.IdMatricula = " + IdMatricula
                    + " group by  mr.IdEmpresa, mr.IdSucursal, mr.IdBodega, mr.IdCbteVta, mr.IdAnio, mr.IdSede, mr.IdJornada, mr.IdNivel, mr.IdCurso, mr.IdParalelo, "
                    + " mr.IdMatricula, mr.IdRubro, ar.NomRubro, ar.NumeroCuotas, d.Total "
                    + " ) a "
                    + " group by  a.IdEmpresa, a.IdAnio, a.IdSede, a.IdJornada, a.IdNivel, a.IdCurso, a.IdParalelo, a.IdMatricula, a.IdRubro, a.NumeroCuotas, a.NomRubro ";
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
                            TotalCobrado = Convert.ToDouble(reader["TotalCobrado"]),
                            TotalFacturado = Convert.ToDouble(reader["TotalFacturado"]),
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
