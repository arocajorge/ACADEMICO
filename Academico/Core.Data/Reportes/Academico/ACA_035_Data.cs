using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_035_Data
    {
        public List<ACA_035_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                List<ACA_035_Info> Lista = new List<ACA_035_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "select a.IdEmpresa, a.Codigo, a.IdAlumno, b.IdMatricula, c.pe_nombreCompleto NombreAlumno, c.pe_cedulaRuc,"
                    + " b.IdAnio, b.IdSede, b.IdNivel, b.IdJornada, b.IdCurso, b.IdParalelo, d.Descripcion,  sn.NomSede, sn.NomNivel, sn.OrdenNivel, "
                    + " nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, isnull(e.Saldo, 0) Saldo, "
                    + " isnull(e.SaldoProntoPago, 0)SaldoProntoPago, isnull(e.CantidadDeudas, 0) CantDeudas"
                    + " from aca_Alumno as a"
                    + " inner join aca_Matricula as b on a.IdEmpresa = b.IdEmpresa and a.IdAlumno = b.IdAlumno"
                    + " left join tb_persona as c on a.IdPersona = c.IdPersona"
                    + " inner join aca_AnioLectivo as d on b.IdEmpresa = d.IdEmpresa and b.IdAnio = d.IdAnio"
                    + " left join (select az.IdEmpresa, az.IdAlumno, dbo.bankersrounding(sum(az.Saldo), 2) Saldo, sum(az.SaldoProntoPago) SaldoProntoPago,"
                    + " sum(CantidadFacturas) as CantidadDeudas from(SELECT ay.IdEmpresa, ay.IdAlumno, sum(ay.Saldo) Saldo, sum(ay.SaldoProntoPago) SaldoProntoPago,"
                    + " count(*) as CantidadFacturas FROM(select a1.IdEmpresa, a2.IdAlumno, dbo.bankersrounding(a1.Total - ISNULL(A3.dc_ValorPago, 0), 2) as Saldo,"
                    + " case when a5.FechaProntoPago >= CAST(GETDATE() AS DATE) THEN dbo.bankersrounding(a1.ValorProntoPago - ISNULL(A3.dc_ValorPago, 0), 2)"
                    + " ELSE dbo.bankersrounding(a1.Total - ISNULL(A3.dc_ValorPago, 0), 2) end as SaldoProntoPago from fa_factura_resumen as a1"
                    + " inner join fa_factura as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdBodega = a2.IdBodega"
                    + " and a1.IdCbteVta = a2.IdCbteVta left join(select x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento,"
                    + " sum(x1.dc_ValorPago) dc_ValorPago from cxc_cobro_det as x1 where x1.IdEmpresa = 1 and x1.estado = 'A'"
                    + " group by x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento) as a3"
                    + " on a2.IdEmpresa = a3.IdEmpresa and a2.IdSucursal = a3.IdSucursal and a2.IdBodega = a3.IdBodega_Cbte and a2.IdCbteVta = a3.IdCbte_vta_nota"
                    + " and a2.vt_tipoDoc = a3.dc_TipoDocumento left join aca_Matricula_Rubro as a4 on a2.IdEmpresa = a4.IdEmpresa and a2.IdSucursal = a4.IdSucursal"
                    + " and a2.IdBodega = a4.IdBodega and a2.IdCbteVta = a4.IdCbteVta left join aca_AnioLectivo_Periodo as a5 on a5.IdEmpresa = a4.IdEmpresa"
                    + " and a5.IdAnio = a4.IdAnio and a5.IdPeriodo = a4.IdPeriodo where a2.IdEmpresa = 1 and a2.Estado = 'A'"
                    + " and dbo.BankersRounding(a1.Total - isnull(a3.dc_ValorPago, 0), 2) > 0) ay group by ay.IdEmpresa, ay.IdAlumno"
                    + " UNION ALL select a1.IdEmpresa, a2.IdAlumno, dbo.bankersrounding(sum(a1.Total) - ISNULL(SUM(A3.dc_ValorPago), 0), 2) as Saldo, "
                    + " dbo.bankersrounding(sum(a1.Total) - ISNULL(SUM(A3.dc_ValorPago), 0), 2), count(*) CantidadFacturas from fa_notaCreDeb_resumen as a1 "
                    + " inner join fa_notaCreDeb as a2 on a1.IdEmpresa = a2.IdEmpresa and a1.IdSucursal = a2.IdSucursal and a1.IdBodega = a2.IdBodega "
                    + " and a1.IdNota = a2.IdNota left join(select x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento, "
                    + " sum(x1.dc_ValorPago) dc_ValorPago from cxc_cobro_det as x1 where x1.IdEmpresa = 1 and x1.estado = 'A' "
                    + " group by x1.IdEmpresa, x1.IdSucursal, x1.IdBodega_Cbte, x1.IdCbte_vta_nota, x1.dc_TipoDocumento) as a3 "
                    + " on a2.IdEmpresa = a3.IdEmpresa and a2.IdSucursal = a3.IdSucursal and a2.IdBodega = a3.IdBodega_Cbte and a2.IdNota = a3.IdCbte_vta_nota "
                    + " and a2.CodDocumentoTipo = a3.dc_TipoDocumento where a2.IdEmpresa = 1 and a2.Estado = 'A' and a2.CreDeb = 'D' "
                    + " and dbo.BankersRounding(a1.Total - isnull(a3.dc_ValorPago, 0), 2) > 0 group by a1.IdEmpresa, a2.IdAlumno) az "
                    + " group by az.IdEmpresa, az.IdAlumno ) as e on a.IdEmpresa = e.IdEmpresa and a.IdAlumno = e.IdAlumno "
                    + " left join aca_AnioLectivo_Curso_Paralelo as cp on b.IdEmpresa = cp.IdEmpresa and b.IdAnio = cp.IdAnio and b.IdSede = cp.IdSede "
                    + " and b.IdNivel = cp.IdNivel and b.IdJornada = cp.IdJornada and b.IdCurso = cp.IdCurso and b.IdParalelo = cp.IdParalelo "
                    + " left join aca_AnioLectivo_Jornada_Curso as jc on b.IdEmpresa = jc.IdEmpresa and b.IdAnio = jc.IdAnio and b.IdSede = jc.IdSede "
                    + " and b.IdNivel = jc.IdNivel and b.IdJornada = jc.IdJornada and b.IdCurso = jc.IdCurso left join aca_AnioLectivo_NivelAcademico_Jornada as nj "
                    + " on nj.IdEmpresa = b.IdEmpresa and nj.IdAnio = b.IdAnio and nj.IdSede = b.IdSede and nj.IdNivel = b.IdNivel and nj.IdJornada = b.IdJornada "
                    + " left join aca_AnioLectivo_Sede_NivelAcademico as sn on sn.IdEmpresa = b.IdEmpresa and sn.IdAnio = b.IdAnio and sn.IdSede = b.IdSede "
                    + " and sn.IdNivel = b.IdNivel "
                    + " WHERE "
                    + " a.IdEmpresa = " + IdEmpresa.ToString()
                    + " and not exists( select x1.IdEmpresa from aca_AlumnoRetiro as x1 "
                    + " where x1.IdEmpresa = b.IdEmpresa and x1.IdMatricula = b.IdMatricula and x1.Estado = 1)"
                    + " and b.IdAnio = " + IdAnio.ToString()
                    + " and b.IdSede = " + IdSede.ToString()
                    + " and b.IdJornada = " + IdJornada.ToString()
                    + " and b.IdNivel = " + IdNivel.ToString()
                    + " and b.IdCurso = " + IdCurso.ToString()
                    + " and b.IdParalelo = " + IdParalelo.ToString()
                    + " and b.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_035_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Codigo = reader["Codigo"].ToString(),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            Saldo = Convert.ToDouble(reader["Saldo"]),
                            SaldoProntoPago = Convert.ToDouble(reader["SaldoProntoPago"]),
                            CantDeudas = Convert.ToInt32(reader["CantDeudas"]),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
                            DescripcionAdeudar = (Convert.ToInt32(reader["CantDeudas"])==0 ? "no adeuda valor alguno ": "tiene valores pendientes de pago")
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
