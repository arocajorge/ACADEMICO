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
    public class cxc_Convenio_Data
    {
        public List<cxc_Convenio_Info> getList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
        {
            try
            {
                List<cxc_Convenio_Info> Lista = new List<cxc_Convenio_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT Conv.IdEmpresa, Conv.IdConvenio, Conv.IdAlumno, al.Codigo, pa.pe_nombreCompleto AS Alumno, Conv.IdMatricula, Conv.IdPersonaConvenio, pg.pe_nombreCompleto AS PersonaConvenio, Conv.Fecha, Conv.FechaPrimerPago, "
                    + " Conv.Valor, Conv.NumCuotas, Conv.Estado, Conv.Observacion "
                    + " FROM     dbo.cxc_Convenio AS Conv "
                    + " INNER JOIN dbo.aca_Alumno AS al ON al.IdEmpresa = Conv.IdEmpresa and al.IdAlumno = Conv.IdAlumno "
                    + " LEFT JOIN dbo.tb_persona AS pa ON pa.IdPersona = al.IdPersona "
                    + " LEFT JOIN dbo.tb_persona AS pg ON Conv.IdPersonaConvenio = pg.IdPersona "
                    + " WHERE Conv.IdEmpresa = " + IdEmpresa + " and Conv.Fecha between " + "'" + fecha_ini.ToString("dd/MM/yyyy") + "'" + " and " + "'" + fecha_fin.Date.ToString("dd/MM/yyyy") + "'";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_Convenio_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdConvenio = Convert.ToInt32(reader["IdConvenio"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            IdPersonaConvenio = Convert.ToDecimal(reader["IdPersonaConvenio"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            FechaPrimerPago = Convert.ToDateTime(reader["FechaPrimerPago"]),
                            Alumno = reader["Alumno"].ToString(),
                            PersonaConvenio = reader["PersonaConvenio"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
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

        public cxc_Convenio_Info getInfo(int IdEmpresa, int IdConvenio)
        {
            try
            {
                cxc_Convenio_Info info;

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var Entity = db.cxc_Convenio.Where(q => q.IdEmpresa == IdEmpresa && q.IdConvenio == IdConvenio).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new cxc_Convenio_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdConvenio = Entity.IdConvenio,
                        IdAlumno = Entity.IdAlumno,
                        IdMatricula = Entity.IdMatricula,
                        IdPersonaConvenio = Entity.IdPersonaConvenio,
                        Fecha = Entity.Fecha,
                        FechaPrimerPago = Entity.FechaPrimerPago,
                        Valor = Entity.Valor,
                        NumCuotas = Entity.NumCuotas,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    var cont = Context.cxc_Convenio.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.cxc_Convenio.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdConvenio) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cxc_Convenio_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Convenio Entity = new cxc_Convenio
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdConvenio = info.IdConvenio = getId(info.IdEmpresa),
                        IdPersonaConvenio = info.IdPersonaConvenio,
                        IdMatricula = info.IdMatricula,
                        Fecha = info.Fecha,
                        NumCuotas = info.NumCuotas,
                        FechaPrimerPago = info.FechaPrimerPago,
                        IdAlumno = info.IdAlumno,
                        Valor = info.Valor,
                        Observacion=info.Observacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.cxc_Convenio.Add(Entity);

                    if (info.lst_detalle != null || info.lst_detalle.Count > 0)
                    {
                        int NumCuota = 1;

                        foreach (var item in info.lst_detalle)
                        {
                            Context.cxc_Convenio_Det.Add(new cxc_Convenio_Det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConvenio = info.IdConvenio,
                                NumCuota = NumCuota++,
                                SaldoInicial = item.SaldoInicial,
                                Saldo = item.Saldo,
                                TotalCuota = item.TotalCuota,
                                FechaPago=item.FechaPago,
                                Observacion_det=item.Observacion_det,
                                IdCatalogoEstadoPago = item.IdCatalogoEstadoPago
                            });
                        }
                    }

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(cxc_Convenio_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Convenio Entity = Context.cxc_Convenio.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdConvenio == info.IdConvenio);
                    if (Entity == null)
                        return false;

                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    var lst_Detalle = Context.cxc_Convenio_Det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdConvenio == info.IdConvenio).ToList();
                    Context.cxc_Convenio_Det.RemoveRange(lst_Detalle);

                    if (info.lst_detalle != null || info.lst_detalle.Count > 0)
                    {
                        int NumCuota = 1;

                        foreach (var item in info.lst_detalle)
                        {
                            Context.cxc_Convenio_Det.Add(new cxc_Convenio_Det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdConvenio = info.IdConvenio,
                                NumCuota = NumCuota++,
                                SaldoInicial = item.SaldoInicial,
                                Saldo = item.Saldo,
                                TotalCuota = item.TotalCuota,
                                FechaPago = item.FechaPago,
                                Observacion_det = item.Observacion_det,
                                IdCatalogoEstadoPago = item.IdCatalogoEstadoPago
                            });
                        }
                    }

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cxc_Convenio_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Convenio Entity = Context.cxc_Convenio.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdConvenio == info.IdConvenio);
                    if (Entity == null)
                        return false;

                    Entity.Estado = info.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
