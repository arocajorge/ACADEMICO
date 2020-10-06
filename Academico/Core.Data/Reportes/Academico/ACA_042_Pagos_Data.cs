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
    public class ACA_042_Pagos_Data
    {
        public List<ACA_042_Pagos_Info> get_list(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                List<ACA_042_Pagos_Info> Lista = new List<ACA_042_Pagos_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT A.IdEmpresa, a.IdAlumno, A.IdMatricula, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso, SUM(A.TotalFacturado) TotalFacturado, SUM(A.dc_ValorPago) TotalCobrado "
                    + " FROM( "
                        + " SELECT a.IdEmpresa, m.IdAlumno, a.IdMatricula, d.IdSucursal, d.IdBodega, d.IdCbteVta, d.Total AS TotalFacturado, ISNULL(SUM(c.dc_ValorPago), 0) AS dc_ValorPago, al.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, "
                        + " jc.NomCurso, jc.OrdenCurso "
                        + " FROM aca_Matricula_Rubro AS a INNER JOIN "
                        + " fa_factura AS b ON a.IdEmpresa = b.IdEmpresa AND a.IdSucursal = b.IdSucursal AND a.IdBodega = b.IdBodega AND a.IdCbteVta = b.IdCbteVta LEFT OUTER JOIN "
                        + " cxc_cobro_det AS c ON b.IdEmpresa = c.IdEmpresa AND b.IdSucursal = c.IdSucursal AND b.IdBodega = c.IdBodega_Cbte AND b.IdCbteVta = c.IdCbte_vta_nota AND b.vt_tipoDoc = c.dc_TipoDocumento AND "
                        + " c.estado = 'A' INNER JOIN "
                        + " fa_factura_resumen AS d ON b.IdEmpresa = d.IdEmpresa AND b.IdSucursal = d.IdSucursal AND b.IdBodega = d.IdBodega AND b.IdCbteVta = d.IdCbteVta INNER JOIN "
                        + " aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdMatricula = m.IdMatricula "
                        + " LEFT OUTER JOIN aca_AnioLectivo al on al.IdEmpresa = m.IdEmpresa and al.IdAnio = m.IdAnio "
                        + " LEFT OUTER JOIN "
                        + " aca_AnioLectivo_Jornada_Curso AS jc ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND "
                        + " m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                        + " aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel LEFT OUTER JOIN "
                        + " aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada "
                        + " WHERE m.IdEmpresa = " + IdEmpresa + " and m.IdAlumno = " + IdAlumno
                        + " GROUP BY a.IdEmpresa, a.IdMatricula, d.IdSucursal, d.IdBodega, d.IdCbteVta, d.Total, al.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, m.IdAlumno "
                    + " )A "
                    + " group by A.IdEmpresa, a.IdAlumno, A.IdMatricula, a.Descripcion, a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, a.OrdenJornada, a.NomCurso, a.OrdenCurso ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_042_Pagos_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            TotalFacturado = Convert.ToDouble(reader["TotalFacturado"]),
                            TotalCobrado = Convert.ToDouble(reader["TotalCobrado"])
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
