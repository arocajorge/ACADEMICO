using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data.Base;
using System.Data.SqlClient;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCreditoDet_Data
    {
        public List<cxc_ConciliacionNotaCreditoDet_Info> GetList(int IdEmpresa,decimal IdConciliacion)
        {
            try
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> Lista = new List<cxc_ConciliacionNotaCreditoDet_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT dbo.cxc_ConciliacionNotaCreditoDet.IdEmpresa, dbo.cxc_ConciliacionNotaCreditoDet.IdConciliacion, dbo.cxc_ConciliacionNotaCreditoDet.Secuencia, dbo.cxc_ConciliacionNotaCreditoDet.IdSucursal, "
                    + " dbo.cxc_ConciliacionNotaCreditoDet.IdBodega, dbo.cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota, dbo.cxc_ConciliacionNotaCreditoDet.vt_TipoDoc, dbo.cxc_ConciliacionNotaCreditoDet.Valor, "
                    + " CASE WHEN cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = 'FACT' THEN fa_factura.vt_serie1 + '-' + fa_factura.vt_serie2 + '-' + fa_factura.vt_NumFactura ELSE CASE WHEN fa_notaCreDeb.NaturalezaNota = 'SRI' THEN fa_notaCreDeb.Serie1 "
                    + " + '-' + fa_notaCreDeb.Serie2 + '-' + fa_notaCreDeb.NumNota_Impresa ELSE fa_notaCreDeb.CodNota END END AS ReferenciaDet, dbo.cxc_ConciliacionNotaCreditoDet.secuencia_nt, ISNULL(dbo.fa_factura.vt_fecha, "
                    + " dbo.fa_notaCreDeb.no_fecha) AS vt_fecha, dbo.fa_factura_resumen.IdAnio, dbo.fa_factura_resumen.IdPlantilla, ISNULL(dbo.fa_factura.IdPuntoVta, dbo.fa_notaCreDeb.IdPuntoVta) AS IdPuntoVta, ISNULL(dbo.fa_factura.IdAlumno, "
                    + " dbo.fa_notaCreDeb.IdAlumno) AS IdAlumno, ISNULL(dbo.fa_factura.IdCliente, dbo.fa_notaCreDeb.IdCliente) AS IdCliente, dbo.cxc_ConciliacionNotaCreditoDet.ValorProntoPago, ISNULL(dbo.fa_factura.vt_Observacion, "
                    + " dbo.fa_notaCreDeb.sc_observacion) AS vt_Observacion "
                    + " FROM dbo.cxc_ConciliacionNotaCreditoDet WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.fa_notaCreDeb ON dbo.cxc_ConciliacionNotaCreditoDet.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.cxc_ConciliacionNotaCreditoDet.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND "
                    + " dbo.cxc_ConciliacionNotaCreditoDet.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota = dbo.fa_notaCreDeb.IdNota AND "
                    + " dbo.cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = dbo.fa_notaCreDeb.CodDocumentoTipo LEFT OUTER JOIN "
                    + " dbo.fa_factura WITH (nolock) ON dbo.cxc_ConciliacionNotaCreditoDet.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.cxc_ConciliacionNotaCreditoDet.IdSucursal = dbo.fa_factura.IdSucursal AND "
                    + " dbo.cxc_ConciliacionNotaCreditoDet.IdBodega = dbo.fa_factura.IdBodega AND dbo.cxc_ConciliacionNotaCreditoDet.IdCbteVtaNota = dbo.fa_factura.IdCbteVta AND "
                    + " dbo.cxc_ConciliacionNotaCreditoDet.vt_TipoDoc = dbo.fa_factura.vt_tipoDoc LEFT OUTER JOIN "
                    + " dbo.fa_factura_resumen WITH (nolock) ON dbo.fa_factura_resumen.IdEmpresa = dbo.fa_factura.IdEmpresa AND dbo.fa_factura_resumen.IdSucursal = dbo.fa_factura.IdSucursal AND dbo.fa_factura_resumen.IdBodega = dbo.fa_factura.IdBodega AND "
                    + " dbo.fa_factura_resumen.IdCbteVta = dbo.fa_factura.IdCbteVta LEFT OUTER JOIN "
                    + " dbo.fa_notaCreDeb_resumen WITH (nolock) ON dbo.fa_notaCreDeb_resumen.IdEmpresa = dbo.fa_notaCreDeb.IdEmpresa AND dbo.fa_notaCreDeb_resumen.IdSucursal = dbo.fa_notaCreDeb.IdSucursal AND "
                    + " dbo.fa_notaCreDeb_resumen.IdBodega = dbo.fa_notaCreDeb.IdBodega AND dbo.fa_notaCreDeb_resumen.IdNota = dbo.fa_notaCreDeb.IdNota "
                    + " WHERE cxc_ConciliacionNotaCreditoDet.IdEmpresa = " + IdEmpresa + " and cxc_ConciliacionNotaCreditoDet.IdConciliacion =" + IdConciliacion.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdConciliacion = Convert.ToDecimal(reader["IdConciliacion"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            IdBodega = Convert.ToInt32(reader["IdBodega"]),
                            IdCbteVtaNota = Convert.ToDecimal(reader["IdCbteVtaNota"]),
                            vt_TipoDoc = string.IsNullOrEmpty(reader["vt_TipoDoc"].ToString()) ? null : reader["vt_TipoDoc"].ToString(),
                            ReferenciaDet = string.IsNullOrEmpty(reader["ReferenciaDet"].ToString()) ? null : reader["ReferenciaDet"].ToString(),
                            secuencia_nt = string.IsNullOrEmpty(reader["secuencia_nt"].ToString()) ? (int?)null : Convert.ToInt32(reader["secuencia_nt"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            IdCliente = string.IsNullOrEmpty(reader["IdCliente"].ToString()) ? 0 : Convert.ToDecimal(reader["IdCliente"]),
                            vt_fecha = string.IsNullOrEmpty(reader["vt_fecha"].ToString()) ? DateTime.Now.Date : Convert.ToDateTime(reader["vt_fecha"]),
                            vt_total = string.IsNullOrEmpty(reader["vt_total"].ToString()) ? (double?)null : Convert.ToDouble(reader["vt_total"]),
                            Saldo = 0,
                            vt_Subtotal = 0,
                            vt_iva = 0,
                            ValorProntoPago = Convert.ToDouble(reader["ValorProntoPago"]),
                            //FechaProntoPago = item.FechaProntoPago,

                            IdAnio = string.IsNullOrEmpty(reader["IdAnio"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdAnio"]),
                            IdPlantilla = string.IsNullOrEmpty(reader["IdPlantilla"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdPlantilla"]),
                            IdPuntoVta = string.IsNullOrEmpty(reader["IdPuntoVta"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdPuntoVta"]),
                            IdAlumno = string.IsNullOrEmpty(reader["IdAlumno"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdAlumno"]),
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_ConciliacionNotaCreditoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdConciliacion == IdConciliacion).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {

                            IdEmpresa = item.IdEmpresa,
                            IdConciliacion = item.IdConciliacion,
                            Secuencia = item.Secuencia,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            IdCbteVtaNota = item.IdCbteVtaNota,
                            vt_TipoDoc = item.vt_TipoDoc,
                            Valor = item.Valor,
                            ReferenciaDet = item.ReferenciaDet,
                            secuencia_nt = item.secuencia_nt,
                            Observacion = item.vt_Observacion,
                            
                            vt_fecha = item.vt_fecha ?? DateTime.Now.Date,
                            vt_total = item.Valor,
                            Saldo = 0,
                            vt_Subtotal = 0,
                            vt_iva = 0,
                            ValorProntoPago = item.ValorProntoPago,
                            //FechaProntoPago = item.FechaProntoPago,

                            IdAnio = item.IdAnio,
                            IdPlantilla = item.IdPlantilla,
                            IdPuntoVta = item.IdPuntoVta,
                            IdCliente = item.IdCliente ?? 0,
                            IdAlumno = item.IdAlumno,
                        });
                    }
                }
                */
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<cxc_ConciliacionNotaCreditoDet_Info> GetListPorCruzar(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<cxc_ConciliacionNotaCreditoDet_Info> Lista = new List<cxc_ConciliacionNotaCreditoDet_Info>();

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var lst = db.vwcxc_cartera_x_cobrar.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();
                    foreach (var item in lst)
                    {
                        Lista.Add(new cxc_ConciliacionNotaCreditoDet_Info
                        {

                            IdEmpresa = item.IdEmpresa,
                            IdSucursal = item.IdSucursal,
                            IdBodega = item.IdBodega,
                            vt_TipoDoc = item.vt_tipoDoc,
                            ReferenciaDet = item.vt_NunDocumento,
                            Observacion = item.Referencia,
                            IdCbteVtaNota = item.IdComprobante,
                            vt_fecha = item.vt_fecha,
                            vt_total = item.vt_total,
                            Saldo = item.Saldo,
                            vt_Subtotal = item.vt_Subtotal,
                            vt_iva = item.vt_iva,
                            vt_fech_venc = item.vt_fech_venc,
                            NomCliente = item.NomCliente,
                            ValorProntoPago = item.ValorProntoPago ?? 0,
                            FechaProntoPago = item.FechaProntoPago,

                            IdAnio = item.IdAnio,
                            IdPlantilla = item.IdPlantilla,
                            IdPuntoVta = item.IdPuntoVta,
                            IdCliente = item.IdCliente,
                            IdAlumno = item.IdAlumno,
                            secuencia = item.vt_tipoDoc + "-" + item.IdBodega.ToString() + "-" + item.IdComprobante.ToString(),
                            SaldoProntoPago = item.SaldoProntoPago??0
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
