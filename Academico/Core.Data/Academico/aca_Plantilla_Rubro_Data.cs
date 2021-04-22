using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Plantilla_Rubro_Data
    {
        public List<aca_Plantilla_Rubro_Info> getList(int IdEmpresa, int IdAnio, int IdPlantilla)
        {
            try
            {
                List<aca_Plantilla_Rubro_Info> Lista = new List<aca_Plantilla_Rubro_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT pr.IdEmpresa, pr.IdAnio, pr.IdPlantilla, pr.IdRubro, ar.NomRubro, pr.IdProducto, p.pr_descripcion, pr.Subtotal, "
                    + " pr.IdCod_Impuesto_Iva, pr.Porcentaje, pr.ValorIVA, pr.Total, pr.TipoDescuento_descuentoDet, pr.Valor_descuentoDet, "
                    + " pr.IdTipoNota_descuentoDet "
                    + " FROM     dbo.aca_Plantilla_Rubro AS pr WITH (nolock) INNER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro AS ar WITH(nolock) ON pr.IdEmpresa = ar.IdEmpresa AND pr.IdAnio = ar.IdAnio AND pr.IdRubro = ar.IdRubro INNER JOIN "
                    + " dbo.in_Producto AS p WITH(nolock) ON ar.IdEmpresa = p.IdEmpresa AND ar.IdProducto = p.IdProducto "
                    + " WHERE pr.IdEmpresa = " + IdEmpresa.ToString() + " and pr.IdAnio = " + IdAnio.ToString() + " and pr.IdPlantilla= " + IdPlantilla.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Plantilla_Rubro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            IdRubro = Convert.ToInt32(reader["IdRubro"]),
                            IdProducto = Convert.ToDecimal(reader["IdProducto"]),
                            Subtotal = Convert.ToDecimal(reader["Subtotal"]),
                            IdCod_Impuesto_Iva = string.IsNullOrEmpty(reader["IdCod_Impuesto_Iva"].ToString()) ? null : reader["IdCod_Impuesto_Iva"].ToString(),
                            ValorIVA = Convert.ToDecimal(reader["ValorIVA"]),
                            Porcentaje = Convert.ToDecimal(reader["Porcentaje"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            pr_descripcion = string.IsNullOrEmpty(reader["pr_descripcion"].ToString()) ? null : reader["pr_descripcion"].ToString(),
                            NomRubro = string.IsNullOrEmpty(reader["NomRubro"].ToString()) ? null : reader["NomRubro"].ToString(),
                            IdTipoNota_descuentoDet = string.IsNullOrEmpty(reader["IdTipoNota_descuentoDet"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdTipoNota_descuentoDet"]),
                            Valor_descuentoDet = string.IsNullOrEmpty(reader["Valor_descuentoDet"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["Valor_descuentoDet"]),
                            TipoDescuento_descuentoDet = string.IsNullOrEmpty(reader["TipoDescuento_descuentoDet"].ToString()) ? null : reader["TipoDescuento_descuentoDet"].ToString(),
                        });
                    }
                    reader.Close();
                }
                Lista.ForEach(q => q.IdString = q.IdPlantilla.ToString("000000") + q.IdRubro.ToString("000000"));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
