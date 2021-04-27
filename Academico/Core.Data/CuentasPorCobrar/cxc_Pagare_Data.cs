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
    public class cxc_Pagare_Data
    {
        public List<cxc_Pagare_Info> getList(int IdEmpresa, DateTime fecha_ini, DateTime fecha_fin, bool MostrarAnulados)
        {
            try
            {
                List<cxc_Pagare_Info> Lista = new List<cxc_Pagare_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT pag.IdEmpresa, pag.IdPagare, pag.IdAlumno, pag.IdMatricula, pag.IdPersonaPagare, p.pe_nombreCompleto AS PersonaPagare, pag.FechaAPagar, pag.Valor, pag.Estado, p_al.pe_nombreCompleto AS Alumno "
                    + " FROM dbo.cxc_Pagare AS pag WITH (nolock) INNER JOIN "
                    + " dbo.aca_Alumno AS a WITH (nolock) ON pag.IdEmpresa = a.IdEmpresa AND pag.IdAlumno = a.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p WITH (nolock) ON pag.IdPersonaPagare = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p_al WITH (nolock) ON a.IdPersona = p_al.IdPersona "
                    + " WHERE pag.IdEmpresa = " + IdEmpresa + " and pag.FechaAPagar between DATEFROMPARTS("+fecha_ini.Year.ToString()+","+fecha_ini.Month.ToString()+","+fecha_ini.Day.ToString()+ ") and DATEFROMPARTS(" + fecha_fin.Year.ToString() + "," + fecha_fin.Month.ToString() + "," + fecha_fin.Day.ToString() + ")";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new cxc_Pagare_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdPagare = Convert.ToInt32(reader["IdPagare"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            IdPersonaPagare = Convert.ToDecimal(reader["IdPersonaPagare"]),
                            Valor = Convert.ToDouble(reader["Valor"]),
                            FechaAPagar = Convert.ToDateTime(reader["FechaAPagar"]),
                            Alumno = reader["Alumno"].ToString(),
                            PersonaPagare = reader["PersonaPagare"].ToString(),
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

        public cxc_Pagare_Info getInfo(int IdEmpresa, int IdPagare)
        {
            try
            {
                cxc_Pagare_Info info;

                using (EntitiesCuentasPorCobrar db = new EntitiesCuentasPorCobrar())
                {
                    var Entity = db.cxc_Pagare.Where(q => q.IdEmpresa == IdEmpresa && q.IdPagare == IdPagare).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new cxc_Pagare_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPagare = Entity.IdPagare,
                        IdAlumno = Entity.IdAlumno,
                        IdMatricula = Entity.IdMatricula,
                        IdPersonaPagare = Entity.IdPersonaPagare,
                        FechaAPagar = Entity.FechaAPagar,
                        Valor = Entity.Valor,
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
                    var cont = Context.cxc_Pagare.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.cxc_Pagare.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPagare) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(cxc_Pagare_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Pagare Entity = new cxc_Pagare
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdPagare = info.IdPagare = getId(info.IdEmpresa),
                        IdPersonaPagare = info.IdPersonaPagare,
                        IdMatricula = info.IdMatricula,
                        FechaAPagar = info.FechaAPagar,
                        IdAlumno = info.IdAlumno,
                        Valor = info.Valor,
                        Observacion=info.Observacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.cxc_Pagare.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(cxc_Pagare_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Pagare Entity = Context.cxc_Pagare.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPagare == info.IdPagare);
                    if (Entity == null)
                        return false;

                    Entity.IdPersonaPagare = info.IdPersonaPagare;
                    Entity.FechaAPagar = info.FechaAPagar;
                    Entity.Valor = info.Valor;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(cxc_Pagare_Info info)
        {
            try
            {
                using (EntitiesCuentasPorCobrar Context = new EntitiesCuentasPorCobrar())
                {
                    cxc_Pagare Entity = Context.cxc_Pagare.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdPagare == info.IdPagare);
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
