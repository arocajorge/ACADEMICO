using Core.Info.Reportes.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Contabilidad
{
    public class CONTA_002_Data
    {
        public List<CONTA_002_Info> GetList(int IdEmpresa, DateTime FechaIni, DateTime FechaFin, string Tipo)
        {
            try
            {
                List<CONTA_002_Info> Lista = new List<CONTA_002_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    string query = string.Empty;
                    switch (Tipo)
                    {
                        case "FACTURAS":
                            query = "select 'FACTURA' AS Tipo, a.vt_serie1+'-'+a.vt_serie2+'-'+a.vt_NumFactura as Referencia,"
                                + " a.IdEmpresa, c.IdTipoCbte, c.IdCbteCble, c.secuencia, c.IdCtaCble, d.pc_Cuenta, c.dc_Observacion, isnull(e.cb_Fecha,a.vt_fecha) cb_Fecha, e.cb_Observacion, f.Su_Descripcion,"
                                + " case when c.dc_Valor > 0 then c.dc_Valor else 0 end as dc_ValorDebe,case when c.dc_Valor < 0 then abs(c.dc_Valor) else 0 end as dc_ValorHaber, isnull(c.dc_Valor,0) dc_Valor"
                                + " from fa_factura as a inner join"
                                +" fa_factura_x_ct_cbtecble as b on a.IdEmpresa = b.vt_IdEmpresa and a.IdSucursal = b.vt_IdSucursal and a.IdBodega = b.vt_IdBodega and a.IdCbteVta = b.vt_IdCbteVta inner join"
                                +" ct_cbtecble_det as c on b.ct_IdEmpresa = c.IdEmpresa and b.ct_IdTipoCbte = c.IdTipoCbte and b.ct_IdCbteCble = c.IdCbteCble inner join"
                                +" ct_plancta as d on c.IdEmpresa = d.IdEmpresa and c.IdCtaCble = d.IdCtaCble inner join"
                                +" ct_cbtecble as e on c.IdEmpresa = e.IdEmpresa and c.IdTipoCbte = e.IdTipoCbte and c.IdCbteCble = e.IdCbteCble inner join"
                                +" tb_sucursal as f on e.IdEmpresa = f.IdEmpresa and e.IdSucursal = f.IdSucursal"
                                +" where a.IdEmpresa = "+IdEmpresa.ToString()+" and a.IdSucursal = 1 and a.Estado = 'A' and a.vt_fecha between DATEFROMPARTS("+FechaIni.Year.ToString()+", "+ FechaIni.Month.ToString() + ", "+ FechaIni.Day.ToString()+ ") and DATEFROMPARTS("+FechaFin.Year.ToString()+", "+FechaFin.Month.ToString()+", "+FechaFin.Day.ToString()+") ";
                            break;
                    }

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new CONTA_002_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Referencia = Convert.ToString(reader["Referencia"]),
                            IdTipoCbte = string.IsNullOrEmpty(reader["IdTipoCbte"].ToString()) ? 0 : (int?)(reader["IdTipoCbte"]),
                            IdCbteCble = string.IsNullOrEmpty(reader["IdCbteCble"].ToString()) ? 0 : (decimal?)(reader["IdCbteCble"]),
                            Secuencia = string.IsNullOrEmpty(reader["secuencia"].ToString()) ? 0 : (int?)(reader["secuencia"]),
                            IdCtaCble = Convert.ToString(reader["IdCtaCble"]),
                            pc_Cuenta = Convert.ToString(reader["pc_Cuenta"]),
                            dc_Observacion = Convert.ToString(reader["dc_Observacion"]),
                            cb_Fecha = Convert.ToDateTime(reader["cb_Fecha"]),
                            cb_Observacion = Convert.ToString(reader["cb_Observacion"]),
                            Su_Descripcion = Convert.ToString(reader["Su_Descripcion"]),
                            dc_ValorDebe = Convert.ToInt32(reader["dc_ValorDebe"]),
                            dc_ValorHaber = Convert.ToInt32(reader["dc_ValorHaber"]),
                            dc_Valor = Convert.ToInt32(reader["dc_Valor"])
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
