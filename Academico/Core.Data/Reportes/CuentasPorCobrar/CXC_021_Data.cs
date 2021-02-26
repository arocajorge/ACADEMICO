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
    public class CXC_021_Data
    {
        public List<CXC_021_Info> GetList(int IdEmpresa, int IdSede, int IdAnio, DateTime fecha_ini, DateTime fecha_fin)
        {
            try
            {
                List<CXC_021_Info> Lista = new List<CXC_021_Info>();
                //DATEFROMPARTS(" + fecha_ini.Year.ToString() + "," + fecha_ini.Month.ToString() + "," + fecha_ini.Day.ToString() + ")
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    #region Query
                    string query = "select m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede,m.IdPreMatricula, m.IdAlumno, m.Fecha, deuda.pe_nombreCompleto NomAlumno, deuda.Referencia,  "
                    + " deuda.vt_Subtotal, deuda.vt_total, deuda.ValorProntoPago, deuda.FechaProntoPago, deuda.Saldo, deuda.SaldoProntoPago, deuda.vt_NunDocumento,deuda.IdComprobante "
                    + " from aca_Matricula m "
                    + " LEFT JOIN "
                    + " ("
                    + " SELECT cabfac.IdEmpresa, cabfac.IdSucursal, cabfac.IdBodega, cabfac.vt_tipoDoc, cabfac.vt_tipoDoc + '-' + CAST(CAST(cabfac.vt_NumFactura AS int) AS varchar(20)) AS vt_NunDocumento, cabfac.vt_Observacion AS Referencia, "
                    + " cabfac.IdCbteVta AS IdComprobante, cabfac.CodCbteVta AS CodComprobante, Sucu.Su_Descripcion, cabfac.IdCliente, cabfac.IdAlumno, dbo.aca_Alumno.Codigo, pal.pe_nombreCompleto, cabfac.vt_fecha, CAST(detfac.Total AS FLOAT) AS vt_total, "
                    + " ROUND(detfac.Total - ROUND(ISNULL(dbo.vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) AS Saldo, ISNULL(dbo.vwcxc_total_cobros_x_Docu.dc_ValorPago, 0) AS TotalxCobrado, Bod.bo_Descripcion AS Bodega, "
                    + " CAST(detfac.SubtotalConDscto AS FLOAT) AS vt_Subtotal, CAST(detfac.ValorIVA AS FLOAT) AS vt_iva, cabfac.vt_fech_venc, ROUND(0, 2) AS dc_ValorRetFu, ROUND(0, 2) AS dc_ValorRetIva, Cli.Codigo AS CodCliente, "
                    + " dbo.tb_persona.pe_nombreCompleto AS NomCliente, dbo.tb_empresa.em_nombre, cabfac.Estado, CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() "
                    + " AS DATE) THEN detfac.ValorProntoPago ELSE detfac.Total END AS ValorProntoPago, dbo.aca_AnioLectivo_Periodo.FechaProntoPago, detfac.IdAnio, detfac.IdPlantilla, cabfac.IdPuntoVta, "
                    + " CASE WHEN aca_AnioLectivo_Rubro.AplicaProntoPago = 1 AND aca_AnioLectivo_Periodo.FechaProntoPago >= CAST(GETDATE() AS DATE) THEN detfac.ValorProntoPago - round(isnull(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2) "
                    + " ELSE ROUND(detfac.Total - ROUND(ISNULL(vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) END AS SaldoProntoPago "
                    + " FROM     dbo.fa_factura_resumen AS detfac INNER JOIN "
                    + " dbo.fa_factura AS cabfac ON detfac.IdBodega = cabfac.IdBodega AND detfac.IdSucursal = cabfac.IdSucursal AND detfac.IdEmpresa = cabfac.IdEmpresa AND detfac.IdCbteVta = cabfac.IdCbteVta INNER JOIN "
                    + " dbo.tb_sucursal AS Sucu ON cabfac.IdEmpresa = Sucu.IdEmpresa AND cabfac.IdSucursal = Sucu.IdSucursal INNER JOIN "
                    + " dbo.tb_bodega AS Bod ON cabfac.IdEmpresa = Bod.IdEmpresa AND cabfac.IdSucursal = Bod.IdSucursal AND cabfac.IdBodega = Bod.IdBodega AND Sucu.IdEmpresa = Bod.IdEmpresa AND Sucu.IdSucursal = Bod.IdSucursal INNER JOIN "
                    + " dbo.fa_cliente AS Cli ON cabfac.IdEmpresa = Cli.IdEmpresa AND cabfac.IdCliente = Cli.IdCliente INNER JOIN "
                    + " dbo.tb_persona ON Cli.IdPersona = dbo.tb_persona.IdPersona INNER JOIN "
                    + " dbo.aca_Alumno ON cabfac.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN "
                    + " dbo.tb_persona pal ON pal.IdPersona = dbo.aca_Alumno.IdPersona INNER JOIN "
                    + " dbo.tb_empresa ON cabfac.IdEmpresa = dbo.tb_empresa.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Rubro ON detfac.IdEmpresa = dbo.aca_AnioLectivo_Rubro.IdEmpresa AND detfac.IdAnio = dbo.aca_AnioLectivo_Rubro.IdAnio AND detfac.IdRubro = dbo.aca_AnioLectivo_Rubro.IdRubro LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Periodo ON detfac.IdPeriodo = dbo.aca_AnioLectivo_Periodo.IdPeriodo AND detfac.IdEmpresa = dbo.aca_AnioLectivo_Periodo.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.vwcxc_total_cobros_x_Docu ON cabfac.IdEmpresa = dbo.vwcxc_total_cobros_x_Docu.IdEmpresa AND cabfac.IdSucursal = dbo.vwcxc_total_cobros_x_Docu.IdSucursal AND "
                    + " cabfac.IdBodega = dbo.vwcxc_total_cobros_x_Docu.IdBodega_Cbte AND cabfac.vt_tipoDoc = dbo.vwcxc_total_cobros_x_Docu.dc_TipoDocumento AND cabfac.IdCbteVta = dbo.vwcxc_total_cobros_x_Docu.IdCbte_vta_nota "
                    + " WHERE(cabfac.Estado = 'A') AND(ROUND(detfac.Total - ROUND(ISNULL(dbo.vwcxc_total_cobros_x_Docu.dc_ValorPago, 0), 2), 2) > 0) "
                    + " AND(cabfac.IdEmpresa = "+IdEmpresa.ToString()+") "
                    + " UNION "
                    + " SELECT A.IdEmpresa, A.IdSucursal, A.IdBodega, 'NTDB' AS CreDeb, CASE WHEN A.NumNota_Impresa IS NULL THEN 'N/D#' + CAST(A.IdNota AS varchar(20)) ELSE 'N/D#' + A.Serie1 + '-' + A.Serie2 + '' + A.NumNota_Impresa END AS Documento, "
                    + " A.sc_observacion, A.IdNota, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, dbo.aca_Alumno.Codigo, pal.pe_nombreCompleto, A.no_fecha, ROUND(SUM(B.sc_total), 2) AS sc_total, ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) AS Saldo, "
                    + " ISNULL(SUM(CB.dc_ValorPago), 0) AS totalCobrado, Bo.bo_Descripcion, ROUND(SUM(B.sc_subtotal), 2) AS sc_subtotal, ROUND(SUM(B.sc_iva), 2) AS sc_iva, A.no_fecha_venc, CAST(0 AS float) AS RtFT, CAST(0 AS float) AS RtIVA, "
                    + " Cli.Codigo AS CodCliente, dbo.tb_persona.pe_nombreCompleto, dbo.tb_empresa.em_nombre, A.Estado, ROUND(SUM(B.sc_total), 2) AS Expr1, NULL AS Expr2, NULL AS Expr3, NULL AS Expr4, A.IdPuntoVta, ROUND(SUM(B.sc_total) "
                    + " - ISNULL(SUM(CB.dc_ValorPago), 0), 2) AS SaldoProntoPago "
                    + " FROM     dbo.fa_notaCreDeb AS A INNER JOIN "
                    + " dbo.fa_notaCreDeb_det AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN "
                    + " dbo.tb_bodega AS Bo ON A.IdEmpresa = Bo.IdEmpresa AND A.IdSucursal = Bo.IdSucursal AND A.IdBodega = Bo.IdBodega INNER JOIN "
                    + " dbo.tb_sucursal AS su ON Bo.IdEmpresa = su.IdEmpresa AND Bo.IdSucursal = su.IdSucursal INNER JOIN "
                    + " dbo.fa_cliente AS Cli ON A.IdEmpresa = Cli.IdEmpresa AND A.IdCliente = Cli.IdCliente INNER JOIN "
                    + " dbo.tb_persona ON Cli.IdPersona = dbo.tb_persona.IdPersona INNER JOIN "
                    + " dbo.aca_Alumno ON A.IdAlumno = dbo.aca_Alumno.IdAlumno INNER JOIN "
                    + " dbo.tb_persona pal ON pal.IdPersona = dbo.aca_Alumno.IdPersona INNER JOIN "
                    + " dbo.tb_empresa ON A.IdEmpresa = dbo.tb_empresa.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.vwcxc_total_cobros_x_Docu AS CB ON A.IdEmpresa = CB.IdEmpresa AND A.IdSucursal = CB.IdSucursal AND A.IdBodega = CB.IdBodega_Cbte AND A.IdNota = CB.IdCbte_vta_nota AND "
                    + " A.CodDocumentoTipo = CB.dc_TipoDocumento "
                    + " WHERE(A.Estado = 'A') AND(A.IdEmpresa = " + IdEmpresa.ToString() + ") AND(NOT EXISTS "
                        + " (SELECT IdEmpresa_nt, IdSucursal_nt, IdBodega_nt, IdNota_nt, secuencia, IdEmpresa_fac_nd_doc_mod, IdSucursal_fac_nd_doc_mod, IdBodega_fac_nd_doc_mod, IdCbteVta_fac_nd_doc_mod, vt_tipoDoc, NumDocumento, "
                        + " Valor_Aplicado, fecha_cruce, ValorProntoPago, TieneSaldo0 "
                        + " FROM      dbo.fa_notaCreDeb_x_fa_factura_NotaDeb AS Cruce "
                        + " WHERE(IdEmpresa_nt = A.IdEmpresa) AND(IdSucursal_nt = A.IdSucursal) AND(IdBodega_nt = A.IdBodega) AND(IdNota_nt = A.IdNota) AND(Valor_Aplicado <> 0))) "
                    + " GROUP BY A.IdEmpresa, A.IdSucursal, A.IdBodega, A.no_fecha, A.CreDeb, A.IdNota, A.Serie1, A.Serie2, A.NumNota_Impresa, A.sc_observacion, A.CodNota, su.Su_Descripcion, A.IdCliente, A.IdAlumno, dbo.aca_Alumno.Codigo, pal.pe_nombreCompleto, Bo.bo_Descripcion, A.no_fecha_venc, "
                    + " Cli.Codigo, dbo.tb_persona.pe_nombreCompleto, dbo.tb_empresa.em_nombre, A.Estado, A.IdPuntoVta "
                    + " HAVING(A.CreDeb = 'D') AND(ROUND(SUM(B.sc_total) - ISNULL(SUM(CB.dc_ValorPago), 0), 2) > 0) "
                    + " ) deuda "
                    + " ON deuda.IdEmpresa = " + IdEmpresa.ToString() + " and deuda.IdAlumno = m.IdAlumno "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdAnio = " + IdAnio.ToString() + " and m.IdSede = " + IdSede.ToString()
                    + " and Fecha between DATEFROMPARTS(" + fecha_ini.Year.ToString() + ", " + fecha_ini.Month.ToString() + ", " + fecha_ini.Day.ToString() + ") and DATEFROMPARTS(" + fecha_fin.Year.ToString() + ", " + fecha_fin.Month.ToString() + ", " + fecha_fin.Day.ToString() + ") and m.IdPreMatricula is not null ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new CXC_021_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdPreMatricula = string.IsNullOrEmpty(reader["IdPreMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdPreMatricula"]),
                            NomAlumno = string.IsNullOrEmpty(reader["NomAlumno"].ToString()) ? null : reader["NomAlumno"].ToString(),
                            Referencia = string.IsNullOrEmpty(reader["Referencia"].ToString()) ? null : reader["Referencia"].ToString(),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            vt_total = string.IsNullOrEmpty(reader["vt_total"].ToString()) ? (double?)null : Convert.ToDouble(reader["vt_total"])
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
