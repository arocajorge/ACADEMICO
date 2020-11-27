using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_PermisoMatricula_Data
    {
        public List<aca_PermisoMatricula_Info> getList(int IdEmpresa, int IdAnio, int IdCatalogoPERNEG, bool MostrarAnulados)
        {
            try
            {
                List<aca_PermisoMatricula_Info> Lista = new List<aca_PermisoMatricula_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT pm.IdEmpresa, pm.IdPermiso, pm.IdAlumno, a.IdPersona, p.pe_nombreCompleto, pm.IdAnio, an.Descripcion, pm.IdCatalogoPERNEG, pm.Fecha, pm.Observacion, pm.Estado, pm.IdUsuarioCreacion "
                    + " FROM dbo.aca_Alumno AS a INNER JOIN "
                    + " dbo.aca_PermisoMatricula AS pm ON a.IdEmpresa = pm.IdEmpresa AND a.IdAlumno = pm.IdAlumno INNER JOIN "
                    + " dbo.aca_AnioLectivo AS an ON pm.IdEmpresa = an.IdEmpresa AND pm.IdAnio = an.IdAnio INNER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona "
                    + " WHERE pm.IdEmpresa = " + IdEmpresa.ToString() + " and pm.IdAnio = " + IdAnio.ToString() + " and pm.IdCatalogoPERNEG = " + IdCatalogoPERNEG.ToString();
                    if (MostrarAnulados==false)
                    {
                        query += " and pm.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_PermisoMatricula_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPermiso = Convert.ToDecimal(reader["IdPermiso"]),
                            IdCatalogoPERNEG = Convert.ToInt32(reader["IdCatalogoPERNEG"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            AnioLectivo = reader["Descripcion"].ToString(),
                            Alumno = reader["pe_nombreCompleto"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            IdUsuarioCreacion = string.IsNullOrEmpty(reader["IdUsuarioCreacion"].ToString()) ? null : reader["IdUsuarioCreacion"].ToString()
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.vwaca_PermisoMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio== IdAnio && q.IdCatalogoPERNEG == IdCatalogoPERNEG 
                    && q.Estado == (MostrarAnulados == true ? q.Estado : true)).Select(q => new aca_PermisoMatricula_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdPermiso = q.IdPermiso,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoPERNEG = q.IdCatalogoPERNEG,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
                        AnioLectivo = q.Descripcion,
                        Alumno = q.pe_nombreCompleto,
                        Estado = q.Estado,
                        IdUsuarioCreacion = q.IdUsuarioCreacion
                    }).ToList();
                }
                */
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_PermisoMatricula_Info> getList_Validacion(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoPERNEG, decimal IdPermiso)
        {
            try
            {
                List<aca_PermisoMatricula_Info> Lista = new List<aca_PermisoMatricula_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_PermisoMatricula "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdAnio = " + IdAnio.ToString() + " and IdAlumno = " + IdAlumno.ToString()
                    + " and IdCatalogoPERNEG = " + IdCatalogoPERNEG.ToString() + " and IdPermiso != " + IdPermiso.ToString() + " and Estado = 1";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_PermisoMatricula_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPermiso = Convert.ToDecimal(reader["IdPermiso"]),
                            IdCatalogoPERNEG = Convert.ToInt32(reader["IdCatalogoPERNEG"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_PermisoMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno
                    && q.IdCatalogoPERNEG == IdCatalogoPERNEG && q.IdPermiso != IdPermiso && q.Estado == true).Select(q => new aca_PermisoMatricula_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdPermiso = q.IdPermiso,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoPERNEG = q.IdCatalogoPERNEG,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
                        Estado = q.Estado
                    }).ToList();
                }
                */
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_PermisoMatricula_Info getInfo(int IdEmpresa, int IdPermiso)
        {
            try
            {
                aca_PermisoMatricula_Info info =  new aca_PermisoMatricula_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_PermisoMatricula "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdPermiso = " + IdPermiso.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_PermisoMatricula_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPermiso = Convert.ToDecimal(reader["IdPermiso"]),
                            IdCatalogoPERNEG = Convert.ToInt32(reader["IdCatalogoPERNEG"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_PermisoMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdPermiso == IdPermiso).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_PermisoMatricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPermiso = Entity.IdPermiso,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoPERNEG = Entity.IdCatalogoPERNEG,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        Estado = Entity.Estado
                    };
                }
                */
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_PermisoMatricula_Info getInfo(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoPERNEG)
        {
            try
            {
                aca_PermisoMatricula_Info info = new aca_PermisoMatricula_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_PermisoMatricula "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdAnio = " + IdAnio.ToString() + " and IdAlumno = " + IdAlumno.ToString() + " and IdCatalogoPERNEG = " + IdCatalogoPERNEG.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_PermisoMatricula_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdPermiso = Convert.ToDecimal(reader["IdPermiso"]),
                            IdCatalogoPERNEG = Convert.ToInt32(reader["IdCatalogoPERNEG"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_PermisoMatricula.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno 
                    && q.IdCatalogoPERNEG == IdCatalogoPERNEG && q.Estado == true).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_PermisoMatricula_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdPermiso = Entity.IdPermiso,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoPERNEG = Entity.IdCatalogoPERNEG,
                        Fecha = Entity.Fecha,
                        IdUsuarioCreacion = Entity.IdUsuarioCreacion,
                        IdUsuarioModificacion = Entity.IdUsuarioModificacion,
                        Observacion = Entity.Observacion
                    };
                }
                */
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_PermisoMatricula.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_PermisoMatricula.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdPermiso) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_PermisoMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PermisoMatricula Entity = new aca_PermisoMatricula
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdPermiso = info.IdPermiso = getId(info.IdEmpresa),
                        IdAlumno = info.IdAlumno,
                        IdCatalogoPERNEG = info.IdCatalogoPERNEG,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_PermisoMatricula.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_PermisoMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PermisoMatricula Entity = Context.aca_PermisoMatricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPermiso == info.IdPermiso);
                    if (Entity == null)
                        return false;

                    Entity.Fecha = info.Fecha;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_PermisoMatricula_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_PermisoMatricula Entity = Context.aca_PermisoMatricula.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdPermiso == info.IdPermiso);
                    if (Entity == null)
                        return false;

                    Entity.Estado = false;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.FechaAnulacion = DateTime.Now;

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
