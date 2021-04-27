using Core.Data.Base;
using Core.Info.CuentasPorCobrar;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.CuentasPorCobrar
{
    public class cxc_CobroMasivoDet_Data
    {
        public List<cxc_CobroMasivoDet_Info> get_list(int IdEmpresa, decimal IdCobroMasivo)
        {
            try
            {
                List<cxc_CobroMasivoDet_Info> Lista = new List<cxc_CobroMasivoDet_Info>(); ;
                /*using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT Conv.IdEmpresa, Conv.IdConvenio, Conv.IdAlumno, al.Codigo, pa.pe_nombreCompleto AS Alumno, Conv.IdMatricula, Conv.IdPersonaConvenio, pg.pe_nombreCompleto AS PersonaConvenio, Conv.Fecha, Conv.FechaPrimerPago, "
                    + " Conv.Valor, Conv.NumCuotas, Conv.Estado, Conv.Observacion "
                    + " FROM     dbo.cxc_Convenio AS Conv WITH (nolock) "
                    + " INNER JOIN dbo.aca_Alumno AS al WITH (nolock) ON al.IdEmpresa = Conv.IdEmpresa and al.IdAlumno = Conv.IdAlumno "
                    + " LEFT JOIN dbo.tb_persona AS pa WITH (nolock) ON pa.IdPersona = al.IdPersona "
                    + " LEFT JOIN dbo.tb_persona AS pg WITH (nolock) ON Conv.IdPersonaConvenio = pg.IdPersona "
                    + " WHERE Conv.IdEmpresa = " + IdEmpresa + " and Conv.Fecha between DATEFROMPARTS(" + fecha_ini.Year.ToString() + "," + fecha_ini.Month.ToString() + "," + fecha_ini.Day.ToString() + ") and DATEFROMPARTS(" + fecha_fin.Year.ToString() + "," + fecha_fin.Month.ToString() + "," + fecha_fin.Day.ToString() + ")";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_CobroMasivoDet_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSucursal = string.IsNullOrEmpty(reader["IdSucursal"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdSucursal"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            NombreAlumno = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            CodigoAlumno = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            IdCobro = string.IsNullOrEmpty(reader["IdCobro"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdCobro"]),
                            IdCobroMasivo = Convert.ToDecimal(reader["IdCobroMasivo"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Repetido = false,
                            ExisteAlumno = true,
                            ValorIgual = true                          
                        });
                    }
                    reader.Close();
                }*/
                
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var lst = Context.vwcxc_CobroMasivoDet.Where(q => q.IdEmpresa == IdEmpresa && q.IdCobroMasivo == IdCobroMasivo).ToList();

                    foreach (var q in lst)
                    {
                        var info = new cxc_CobroMasivoDet_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdAlumno = q.IdAlumno,
                            NombreAlumno = q.pe_nombreCompleto,
                            CodigoAlumno = q.Codigo,
                            Fecha = q.Fecha,
                            Valor = q.Valor,
                            IdCobro = q.IdCobro,
                            IdCobroMasivo = q.IdCobroMasivo,
                            Secuencia = q.Secuencia,
                            Repetido=false,
                            ExisteAlumno =true,
                            ValorIgual = true
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

        public bool modificarDB(cxc_CobroMasivoDet_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar db_f = new EntitiesCuentasPorCobrar())
                {
                    #region Detalle
                    var entity = db_f.cxc_CobroMasivoDet.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdCobroMasivo == info.IdCobroMasivo && q.Secuencia == info.Secuencia).FirstOrDefault();
                    if (entity == null) return false;

                    entity.IdSucursal = info.IdSucursal;
                    entity.IdCobro = info.IdCobro;

                    #endregion

                    db_f.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
