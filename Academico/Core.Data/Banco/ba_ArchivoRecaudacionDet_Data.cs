using Core.Data.Base;
using Core.Info.Banco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Banco
{
    public class ba_ArchivoRecaudacionDet_Data
    {
        public List<ba_ArchivoRecaudacionDet_Info> GetList(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    Lista = Context.vwba_ArchivoRecaudacionDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_ArchivoRecaudacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        Secuencia = q.Secuencia,
                        IdMatricula = q.IdMatricula,
                        IdAlumno =q.IdAlumno,
                        CodigoAlumno = q.Codigo,
                        pe_nombreCompleto = q.pe_nombreCompleto,
                        Valor =q.Valor,
                        ValorProntoPago =q.ValorProntoPago,
                        FechaProceso = q.FechaProceso,
                        FechaProntoPago = q.FechaProntoPago
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_ArchivoRecaudacionDet_Info> getList_ConSaldo(int IdEmpresa)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista = new List<ba_ArchivoRecaudacionDet_Info>();
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    var lst = Context.vwba_ArchivoRecaudacionDet_Saldos.Where(q => q.IdEmpresa == IdEmpresa).ToList();

                    foreach (var q in lst)
                    {
                        var info = new ba_ArchivoRecaudacionDet_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            CodigoAlumno = q.CodigoAlumno,
                            IdAlumno = q.IdAlumno ?? 0,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Saldo = q.Saldo ?? 0,
                            SaldoProntoPago = q.SaldoProntoPago ?? 0,
                            FechaProntoPago = (DateTime) q.FechaProntoPago
                        };
                        Lista.Add(info);
                    }
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_ArchivoRecaudacionDet_Info> getList_Archivo(int IdEmpresa, decimal IdArchivo)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista;
                using (EntitiesBanco Context = new EntitiesBanco())
                {
                    Lista = Context.vwba_ArchivoRecaudacion_Archivo.Where(q => q.IdEmpresa == IdEmpresa && q.IdArchivo == IdArchivo).Select(q => new ba_ArchivoRecaudacionDet_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdArchivo = q.IdArchivo,
                        Secuencia = q.Secuencia,
                        IdMatricula = q.IdMatricula,
                        IdAlumno = q.IdAlumno,
                        CodigoAlumno = q.CodigoAlumno,
                        pe_nombreCompleto = q.NomAlumno,
                        pe_cedulaRuc = q.pe_cedulaRuc,
                        Nom_Archivo = q.Nom_Archivo,
                        Valor = q.Valor,
                        ValorProntoPago = q.ValorProntoPago,
                        Observacion = q.Observacion,
                        ba_Num_Cuenta = q.ba_Num_Cuenta,
                        CodigoLegal=q.CodigoLegal,
                        IdTipoDocumento=q.IdTipoDocumento,
                        Fecha = q.Fecha,
                        FechaProntoPago = q.FechaProntoPago
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ba_ArchivoRecaudacionDet_Info> GetList(int IdEmpresa, string Codigo)
        {
            try
            {
                List<ba_ArchivoRecaudacionDet_Info> Lista = new List<ba_ArchivoRecaudacionDet_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DECLARE @IdEmpresa int = "+IdEmpresa.ToString()
                                        +" SELECT IdEmpresa, IdAlumno, IdMatricula, CodigoAlumno, pe_nombreCompleto, SUM(Saldo) AS Saldo, SUM(SaldoProntoPago) AS SaldoProntoPago, MAX(FechaProntoPago) AS FechaProntoPago"
                                        +" FROM(SELECT a.IdEmpresa, a.IdAlumno, d.IdMatricula, c.Codigo AS CodigoAlumno, f.pe_nombreCompleto, dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) AS Saldo,"
                                        +" CASE WHEN dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) > 0 THEN CASE WHEN ap.FechaProntoPago >= CAST(getdate() AS date) THEN dbo.BankersRounding(B.ValorProntoPago - ISNULL(g.dc_ValorPago, 0), 2)"
                                        +" ELSE dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) END ELSE 0 END AS SaldoProntoPago, CASE WHEN dbo.BankersRounding(b.Total - b.ValorProntoPago, 2)"
                                        +" <> 0 THEN ap.FechaProntoPago ELSE a.vt_fecha END AS FechaProntoPago"
                                        +" FROM(SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS dc_ValorPago"
                                        +" FROM      dbo.cxc_cobro_det AS a"
                                        +" WHERE(dc_TipoDocumento = 'FACT') AND(estado = 'A')"
                                        +" GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS g RIGHT OUTER JOIN"
                                        +" dbo.fa_factura AS a INNER JOIN"
                                        +" dbo.fa_factura_resumen AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta INNER JOIN"
                                        +" dbo.aca_Alumno AS c ON a.IdEmpresa = c.IdEmpresa AND a.IdAlumno = c.IdAlumno INNER JOIN"
                                        +" dbo.aca_Matricula AS d ON d.IdEmpresa = c.IdEmpresa AND d.IdAlumno = c.IdAlumno INNER JOIN"
                                        +" dbo.aca_AnioLectivo AS e ON d.IdEmpresa = e.IdEmpresa AND d.IdAnio = e.IdAnio INNER JOIN"
                                        + " dbo.tb_persona AS f ON c.IdPersona = f.IdPersona LEFT OUTER JOIN"
                                        + " dbo.aca_AnioLectivo_Periodo AS ap ON b.IdPeriodo = ap.IdPeriodo AND b.IdEmpresa = ap.IdEmpresa ON g.IdEmpresa = a.IdEmpresa AND g.IdSucursal = a.IdSucursal AND g.IdBodega_Cbte = a.IdBodega AND"
                                        + " g.IdCbte_vta_nota = a.IdCbteVta AND g.dc_TipoDocumento = a.vt_tipoDoc"
                                        + " WHERE(e.EnCurso = 1) AND(dbo.BankersRounding(b.Total - ISNULL(g.dc_ValorPago, 0), 2) > 0) AND(a.Estado = 'A')"
                                        + " and c.IdEmpresa = @IdEmpresa and c.Codigo  in ("+Codigo+")"
                                        + " UNION ALL"
                                        + " SELECT A.IdEmpresa, A.IdAlumno, G.IdMatricula, C.Codigo, D.pe_nombreCompleto, dbo.BankersRounding(B.Total - ISNULL(E.dc_ValorPago, 0), 2) AS Expr1, dbo.BankersRounding(B.Total - ISNULL(E.dc_ValorPago, 0), 2) AS Expr2,"
                                        +" A.no_fecha"
                                        +" FROM     dbo.fa_notaCreDeb AS A INNER JOIN"
                                        +" dbo.fa_notaCreDeb_resumen AS B ON A.IdEmpresa = B.IdEmpresa AND A.IdSucursal = B.IdSucursal AND A.IdBodega = B.IdBodega AND A.IdNota = B.IdNota INNER JOIN"
                                        +" dbo.aca_Alumno AS C ON A.IdEmpresa = C.IdEmpresa AND A.IdAlumno = C.IdAlumno INNER JOIN"
                                        +" dbo.tb_persona AS D ON C.IdPersona = D.IdPersona LEFT OUTER JOIN"
                                        +" (SELECT IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento, SUM(dc_ValorPago) AS dc_ValorPago"
                                        +" FROM      dbo.cxc_cobro_det AS a"
                                        +" WHERE(dc_TipoDocumento = 'NTDB') AND(estado = 'A')"
                                        +" GROUP BY IdEmpresa, IdSucursal, IdBodega_Cbte, IdCbte_vta_nota, dc_TipoDocumento) AS E ON E.IdEmpresa = A.IdEmpresa AND A.IdSucursal = E.IdSucursal AND A.IdBodega = E.IdBodega_Cbte AND"
                                        +" A.IdNota = E.IdCbte_vta_nota AND A.CodDocumentoTipo = E.dc_TipoDocumento INNER JOIN"
                                        +" dbo.aca_Matricula AS G ON A.IdEmpresa = G.IdEmpresa AND A.IdAlumno = G.IdAlumno INNER JOIN"
                                        +" dbo.aca_AnioLectivo AS H ON G.IdEmpresa = H.IdEmpresa AND G.IdAnio = H.IdAnio"
                                        +" WHERE(H.EnCurso = 1) AND(dbo.BankersRounding(B.Total - ISNULL(E.dc_ValorPago, 0), 2) > 0) AND(A.Estado = 'A') AND(A.CreDeb = 'D')"
                                        + " and c.IdEmpresa = @IdEmpresa and c.Codigo in (" + Codigo + ")) AS A_1"
                                        + " GROUP BY IdEmpresa, IdAlumno, IdMatricula, CodigoAlumno, pe_nombreCompleto"
                                        + " HAVING(dbo.BankersRounding(SUM(SaldoProntoPago), 2) > 0) AND(NOT EXISTS"
                                        + " (SELECT IdEmpresa"
                                        + " FROM      dbo.aca_AlumnoRetiro AS f"
                                        + " WHERE(IdEmpresa = A_1.IdEmpresa) AND(IdMatricula = A_1.IdMatricula) AND(Estado = 1)))";
                    
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ba_ArchivoRecaudacionDet_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            CodigoAlumno = Convert.ToString(reader["CodigoAlumno"]),
                            pe_nombreCompleto = Convert.ToString(reader["pe_nombreCompleto"]),
                            Saldo = Convert.ToDecimal(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDecimal(reader["SaldoProntoPago"]),
                            FechaProntoPago = Convert.ToDateTime(reader["FechaProntoPago"])
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
