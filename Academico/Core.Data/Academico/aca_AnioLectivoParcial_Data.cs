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
    public class aca_AnioLectivoParcial_Data
    {
        public List<aca_AnioLectivoParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT p.IdEmpresa, p.IdAnio, p.IdSede, p.IdCatalogoParcial, c.NomCatalogo, p.FechaInicio, p.FechaFin, p.Orden, p.EsExamen, c.IdCatalogoTipo, ct.NomCatalogoTipo, p.ValidaEstadoAlumno "
                    + " FROM dbo.aca_AnioLectivoParcial AS p INNER JOIN "
                    + " dbo.aca_Catalogo AS c ON p.IdCatalogoParcial = c.IdCatalogo INNER JOIN "
                    + " dbo.aca_CatalogoTipo AS ct ON c.IdCatalogoTipo = ct.IdCatalogoTipo "
                    + " WHERE p.IdEmpresa = " + IdEmpresa.ToString() + "and p.IdSede = " + IdSede.ToString() + "and p.IdAnio = " + IdAnio.ToString()
                    + " order by p.Orden";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            NomCatalogo = reader["NomCatalogo"].ToString(),
                            Orden = string.IsNullOrEmpty(reader["Orden"].ToString()) ? (int?)null : Convert.ToInt32(reader["Orden"]),
                            ValidaEstadoAlumno = Convert.ToBoolean(reader["ValidaEstadoAlumno"]),
                            EsExamen = string.IsNullOrEmpty(reader["EsExamen"].ToString()) ? false :Convert.ToBoolean(reader["EsExamen"]),
                            FechaInicio = string.IsNullOrEmpty(reader["FechaInicio"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFin = string.IsNullOrEmpty(reader["FechaFin"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaFin"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio).OrderBy(q => q.Orden).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdAnio = q.IdAnio,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            NomCatalogo = q.NomCatalogo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden = q.Orden
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivoParcial_Info> getList_x_Tipo(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT p.IdEmpresa, p.IdAnio, p.IdSede, p.IdCatalogoParcial, c.NomCatalogo, p.FechaInicio, p.FechaFin, p.Orden, p.EsExamen, c.IdCatalogoTipo, ct.NomCatalogoTipo, p.ValidaEstadoAlumno "
                    + " FROM dbo.aca_AnioLectivoParcial AS p INNER JOIN "
                    + " dbo.aca_Catalogo AS c ON p.IdCatalogoParcial = c.IdCatalogo INNER JOIN "
                    + " dbo.aca_CatalogoTipo AS ct ON c.IdCatalogoTipo = ct.IdCatalogoTipo "
                    + " WHERE p.IdEmpresa = " + IdEmpresa.ToString() + "and p.IdSede = " + IdSede.ToString() + "and p.IdAnio = " + IdAnio.ToString() + "and c.IdCatalogoTipo = " + IdCatalogoTipo.ToString()
                    + " order by p.Orden";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            NomCatalogo = reader["NomCatalogo"].ToString(),
                            Orden = string.IsNullOrEmpty(reader["Orden"].ToString()) ? (int?)null : Convert.ToInt32(reader["Orden"]),
                            ValidaEstadoAlumno = Convert.ToBoolean(reader["ValidaEstadoAlumno"]),
                            EsExamen = string.IsNullOrEmpty(reader["EsExamen"].ToString()) ? false : Convert.ToBoolean(reader["EsExamen"]),
                            FechaInicio = string.IsNullOrEmpty(reader["FechaInicio"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFin = string.IsNullOrEmpty(reader["FechaFin"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaFin"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoTipo == IdCatalogoTipo).OrderBy(q=>q.Orden).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdAnio = q.IdAnio,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            NomCatalogo = q.NomCatalogo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden = q.Orden
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoParcial_Info getInfo(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoParcial)
        {
            try
            {
                aca_AnioLectivoParcial_Info info = new aca_AnioLectivoParcial_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_AnioLectivoParcial "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdSede = " + IdSede.ToString() + " and IdAnio = " + IdAnio.ToString() + " and IdCatalogoParcial = " + IdCatalogoParcial.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Orden = string.IsNullOrEmpty(reader["Orden"].ToString()) ? (int?)null : Convert.ToInt32(reader["Orden"]),
                            ValidaEstadoAlumno = Convert.ToBoolean(reader["ValidaEstadoAlumno"]),
                            EsExamen = string.IsNullOrEmpty(reader["EsExamen"].ToString()) ? false : Convert.ToBoolean(reader["EsExamen"]),
                            FechaInicio = string.IsNullOrEmpty(reader["FechaInicio"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFin = string.IsNullOrEmpty(reader["FechaFin"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaFin"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.aca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoParcial == IdCatalogoParcial).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoParcial_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdCatalogoParcial = Entity.IdCatalogoParcial,
                        EsExamen = Entity.EsExamen,
                        ValidaEstadoAlumno = Entity.ValidaEstadoAlumno,
                        Orden = Entity.Orden
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

        public aca_AnioLectivoParcial_Info getInfo_X_Orden(int IdEmpresa, int IdSede, int IdAnio, int Orden)
        {
            try
            {
                aca_AnioLectivoParcial_Info info = new aca_AnioLectivoParcial_Info();
                
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.aca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.Orden == Orden).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoParcial_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdSede = Entity.IdSede,
                        IdCatalogoParcial = Entity.IdCatalogoParcial,
                        EsExamen = Entity.EsExamen,
                        ValidaEstadoAlumno = Entity.ValidaEstadoAlumno,
                        Orden = Entity.Orden
                    };

                }
                
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivoParcial_Info> getList(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo, DateTime FechaActual)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoTipo == IdCatalogoTipo && FechaActual >= q.FechaInicio && FechaActual <= q.FechaFin).OrderBy(q => q.Orden).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdAnio = q.IdAnio,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            NomCatalogo = q.NomCatalogo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden=q.Orden
                        });
                    });
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AnioLectivoParcial_Info> getList_Reportes(int IdEmpresa, int IdSede, int IdAnio, int IdCatalogoTipo)
        {
            try
            {
                List<aca_AnioLectivoParcial_Info> Lista = new List<aca_AnioLectivoParcial_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT p.IdEmpresa, p.IdAnio, p.IdSede, p.IdCatalogoParcial, c.NomCatalogo, p.FechaInicio, p.FechaFin, p.Orden, p.EsExamen, c.IdCatalogoTipo, ct.NomCatalogoTipo, p.ValidaEstadoAlumno "
                    + " FROM dbo.aca_AnioLectivoParcial AS p INNER JOIN "
                    + " dbo.aca_Catalogo AS c ON p.IdCatalogoParcial = c.IdCatalogo INNER JOIN "
                    + " dbo.aca_CatalogoTipo AS ct ON c.IdCatalogoTipo = ct.IdCatalogoTipo "
                    + " WHERE p.IdEmpresa = " + IdEmpresa.ToString() + "and p.IdSede = " + IdSede.ToString() + "and p.IdAnio = " + IdAnio.ToString() + "and c.IdCatalogoTipo = " + IdCatalogoTipo.ToString()
                    + " order by p.Orden";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            NomCatalogo = reader["NomCatalogo"].ToString(),
                            Orden = string.IsNullOrEmpty(reader["Orden"].ToString()) ? (int?)null : Convert.ToInt32(reader["Orden"]),
                            ValidaEstadoAlumno = Convert.ToBoolean(reader["ValidaEstadoAlumno"]),
                            EsExamen = string.IsNullOrEmpty(reader["EsExamen"].ToString()) ? false : Convert.ToBoolean(reader["EsExamen"]),
                            FechaInicio = string.IsNullOrEmpty(reader["FechaInicio"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFin = string.IsNullOrEmpty(reader["FechaFin"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaFin"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoParcial.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdAnio == IdAnio
                    && q.IdCatalogoTipo == IdCatalogoTipo).OrderBy(q => q.Orden).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoParcial_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdAnio = q.IdAnio,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            NomCatalogo = q.NomCatalogo,
                            FechaInicio = q.FechaInicio,
                            FechaFin = q.FechaFin,
                            EsExamen = q.EsExamen,
                            ValidaEstadoAlumno = q.ValidaEstadoAlumno,
                            Orden = q.Orden
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(List<aca_AnioLectivoParcial_Info> lista)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    foreach (var item in lista)
                    {
                        aca_AnioLectivoParcial Entity = new aca_AnioLectivoParcial
                        {
                            IdEmpresa = item.IdEmpresa,
                            IdAnio = item.IdAnio,
                            IdSede = item.IdSede,
                            IdCatalogoParcial = item.IdCatalogoParcial,
                            FechaInicio = item.FechaInicio,
                            FechaFin = item.FechaFin,
                            EsExamen = item.EsExamen,
                            Orden = item.Orden,
                            ValidaEstadoAlumno = item.ValidaEstadoAlumno,
                            IdUsuarioCreacion = item.IdUsuarioCreacion,
                            FechaCreacion = DateTime.Now
                        };
                        Context.aca_AnioLectivoParcial.Add(Entity);
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

        public bool modificarDB(aca_AnioLectivoParcial_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoParcial Entity = Context.aca_AnioLectivoParcial.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSede == info.IdSede && q.IdCatalogoParcial== info.IdCatalogoParcial);
                    if (Entity == null)
                        return false;

                    Entity.FechaInicio = info.FechaInicio;
                    Entity.FechaFin = info.FechaFin;
                    Entity.EsExamen = info.EsExamen;
                    Entity.ValidaEstadoAlumno = info.ValidaEstadoAlumno;
                    Entity.Orden = info.Orden;
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
    }
}
