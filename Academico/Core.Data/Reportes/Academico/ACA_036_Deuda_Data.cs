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
                    string query = "SELECT cabfac.IdEmpresa, cabfac.IdSucursal, cabfac.IdBodega, cabfac.vt_tipoDoc, cabfac.vt_tipoDoc + '-' + CAST(CAST(cabfac.vt_NumFactura AS int) AS varchar(20)) AS vt_NunDocumento, cabfac.vt_Observacion AS Referencia, "
                    + " cabfac.IdCbteVta AS IdComprobante, cabfac.CodCbteVta AS CodComprobante, Sucu.Su_Descripcion, cabfac.IdCliente, cabfac.IdAlumno, cabfac.vt_fecha, "
                    + " CAST(detfac.Total AS FLOAT) AS vt_total, ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) AS Saldo, "
                    + " ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0) AS TotalxCobrado, Bod.bo_Descripcion AS Bodega, CAST(detfac.SubtotalConDscto AS FLOAT) AS vt_Subtotal, CAST(detfac.ValorIVA AS FLOAT) AS vt_iva, "
                    + " cabfac.vt_fech_venc, ROUND(0, 2) AS dc_ValorRetFu, ROUND(0, 2) AS dc_ValorRetIva, Cli.Codigo AS CodCliente, "
                    + " tb_persona.pe_nombreCompleto AS NomCliente, tb_empresa.em_nombre, cabfac.Estado, CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() AS DATE) "
                    + " THEN detfac.ValorProntoPago ELSE detfac.Total END AS ValorProntoPago, aca_AnioLectivo_Periodo.FechaProntoPago AS FechaProntoPago, detfac.IdAnio, detfac.IdPlantilla, cabfac.IdPuntoVta, "
                    + " CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() AS DATE) THEN detfac.ValorProntoPago - round(isnull(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2) "
                    + " ELSE ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) END AS SaldoProntoPago "
                    + " FROM     fa_factura_resumen AS detfac WITH (nolock)INNER JOIN "
                    + " fa_factura AS cabfac WITH(nolock) ON detfac.IdBodega = cabfac.IdBodega AND detfac.IdSucursal = cabfac.IdSucursal AND detfac.IdEmpresa = cabfac.IdEmpresa AND detfac.IdCbteVta = cabfac.IdCbteVta INNER JOIN "
                    + " tb_sucursal AS Sucu WITH(nolock) ON cabfac.IdEmpresa = Sucu.IdEmpresa AND cabfac.IdSucursal = Sucu.IdSucursal INNER JOIN "
                    + " tb_bodega AS Bod WITH(nolock) ON cabfac.IdEmpresa = Bod.IdEmpresa AND cabfac.IdSucursal = Bod.IdSucursal AND cabfac.IdBodega = Bod.IdBodega AND Sucu.IdEmpresa = Bod.IdEmpresa AND Sucu.IdSucursal = Bod.IdSucursal INNER JOIN "
                    + " fa_cliente AS Cli WITH(nolock) ON cabfac.IdEmpresa = Cli.IdEmpresa AND cabfac.IdCliente = Cli.IdCliente INNER JOIN "
                    + " tb_persona WITH(nolock) ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN "
                    + " tb_empresa WITH(nolock) ON cabfac.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN "
                    + " aca_AnioLectivo_Rubro WITH(nolock) ON detfac.IdEmpresa = aca_AnioLectivo_Rubro.IdEmpresa AND detfac.IdAnio = aca_AnioLectivo_Rubro.IdAnio AND detfac.IdRubro = aca_AnioLectivo_Rubro.IdRubro LEFT OUTER JOIN "
                    + " aca_AnioLectivo_Periodo WITH(nolock) ON detfac.IdPeriodo = aca_AnioLectivo_Periodo.IdPeriodo AND detfac.IdEmpresa = aca_AnioLectivo_Periodo.IdEmpresa LEFT OUTER JOIN "
                    + " vwcxc_total_cobros_x_Docu WITH(nolock) ON cabfac.IdEmpresa = vwcxc_total_cobros_x_Docu.IdEmpresa AND cabfac.IdSucursal = vwcxc_total_cobros_x_Docu.IdSucursal AND cabfac.IdBodega = vwcxc_total_cobros_x_Docu.IdBodega_Cbte AND "
                    + " cabfac.vt_tipoDoc = vwcxc_total_cobros_x_Docu.dc_TipoDocumento AND cabfac.IdCbteVta = vwcxc_total_cobros_x_Docu.IdCbte_vta_nota "
                    + " WHERE(cabfac.Estado = 'A') AND ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) > 0 "
                    + " and cabfac.IdEmpresa=" + IdEmpresa.ToString() + " and cabfac.IdAlumno = " + IdAlumno.ToString() + " and detfac.IdAnio= " + IdAnio.ToString()
                    + " UNION "
                    + " SELECT A.IdEmpresa, A.IdSucursal, A.IdBodega, 'NTDB' AS CreDeb, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/D#' + CAST(A.IdNota AS varchar(20)) ELSE 'N/D#' + A.Serie1 + '-' + A.Serie2 + '' + A.NumNota_Impresa END AS Documento, "
                    + " A.sc_observacion, A.IdNota, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, A.no_fecha, ROUND(SUM(B.sc_total), 2) AS sc_total, ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) AS Saldo, "
                    + " ISNULL(SUM(CB.dc_ValorPago), 0) AS totalCobrado, Bo.bo_Descripcion, ROUND(SUM(B.sc_subtotal), 2) AS sc_subtotal, ROUND(SUM(B.sc_iva), 2) AS sc_iva, A.no_fecha_venc, cast(0 AS float) AS RtFT, cast(0 AS float) AS RtIVA, "
                    + " Cli.Codigo AS CodCliente, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre, A.Estado, ROUND(SUM(B.sc_total), 2), NULL, NULL IdAnio, NULL IdPlantilla, A.IdPuntoVta, ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), "
                    + " 2) SaldoProntoPago "
                    + " FROM     fa_notaCreDeb AS A WITH(nolock) INNER JOIN "
                    + " fa_notaCreDeb_det AS B WITH(nolock) ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN "
                    + " tb_bodega AS Bo WITH(nolock) ON A.IdEmpresa = Bo.IdEmpresa AND A.IdSucursal = Bo.IdSucursal AND A.IdBodega = Bo.IdBodega INNER JOIN "
                    + " tb_sucursal AS su WITH(nolock) ON Bo.IdEmpresa = su.IdEmpresa AND Bo.IdSucursal = su.IdSucursal INNER JOIN "
                    + " fa_cliente AS Cli WITH(nolock) ON A.IdEmpresa = Cli.IdEmpresa AND A.IdCliente = Cli.IdCliente INNER JOIN "
                    + " tb_persona WITH(nolock) ON Cli.IdPersona = tb_persona.IdPersona INNER JOIN "
                    + " tb_empresa WITH(nolock) ON A.IdEmpresa = tb_empresa.IdEmpresa LEFT OUTER JOIN "
                    + " vwcxc_total_cobros_x_Docu AS CB WITH(nolock) ON A.IdEmpresa = CB.IdEmpresa AND A.IdSucursal = CB.IdSucursal AND A.IdBodega = CB.IdBodega_Cbte AND A.IdNota = CB.IdCbte_vta_nota AND "
                    + " A.CodDocumentoTipo = CB.dc_TipoDocumento "
                    + " WHERE  A.Estado = 'A' and A.IdEmpresa = " + IdEmpresa.ToString() +" and A.IdAlumno = " + IdAlumno.ToString()
                      + " AND NOT EXISTS "
                    + "     (SELECT * "
                    + "     FROM      fa_notaCreDeb_x_fa_factura_NotaDeb Cruce WITH(nolock) "
                    + "     WHERE   Cruce.IdEmpresa_nt = A.IdEmpresa AND Cruce.IdSucursal_nt = A.IdSucursal AND Cruce.IdBodega_nt = A.IdBodega AND Cruce.IdNota_nt = A.IdNota AND Cruce.Valor_Aplicado <> 0) "
                    + " GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.no_fecha, A.CreDeb, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.sc_observacion, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, Bo.bo_Descripcion, A.no_fecha_venc, "
                    + " Cli.Codigo, tb_persona.pe_nombreCompleto, tb_empresa.em_nombre, A.Estado, A.IdPuntoVta "
                    + " HAVING(A.CreDeb = 'D') AND ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) > 0 ";
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
