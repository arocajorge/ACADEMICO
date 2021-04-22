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
    public class aca_MatriculaCondicional_Data
    {
        public List<aca_MatriculaCondicional_Info> getList(int IdEmpresa, int IdAnio, bool MostrarAnulados)
        {
            try
            {
                List<aca_MatriculaCondicional_Info> Lista = new List<aca_MatriculaCondicional_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mc.IdEmpresa, mc.IdMatriculaCondicional, mc.IdAlumno, al.Codigo, p.pe_nombreCompleto, mc.IdAnio, a.Descripcion, "
                    + " mc.Fecha, mc.IdCatalogoCONDIC, c.NomCatalogo, mc.Observacion, mc.Estado "
                    + " FROM dbo.aca_MatriculaCondicional AS mc WITH (nolock) INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH(nolock) ON mc.IdEmpresa = a.IdEmpresa AND mc.IdAnio = a.IdAnio INNER JOIN "
                    + " dbo.aca_Alumno AS al WITH(nolock) ON mc.IdEmpresa = al.IdEmpresa AND mc.IdAlumno = al.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p WITH(nolock) ON al.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_Catalogo AS c WITH(nolock) ON mc.IdCatalogoCONDIC = c.IdCatalogo"
                    +" WHERE mc.IdEmpresa = " + IdEmpresa.ToString() + " and mc.IdAnio = " + IdAnio.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and mc.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCondicional_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdMatriculaCondicional = Convert.ToDecimal(reader["IdMatriculaCondicional"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdCatalogoCONDIC = Convert.ToInt32(reader["IdCatalogoCONDIC"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = string.IsNullOrEmpty(reader["Observacion"].ToString()) ? null : reader["Observacion"].ToString(),
                            AnioLectivo = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            Alumno = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
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

        public List<aca_MatriculaCondicional_Info> getList_ExisteCondicional(int IdEmpresa, int IdAnio, decimal IdAlumno, int IdCatalogoCONDIC)
        {
            try
            {
                List<aca_MatriculaCondicional_Info> Lista = new List<aca_MatriculaCondicional_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_MatriculaCondicional.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno 
                    && q.IdCatalogoCONDIC == IdCatalogoCONDIC  && q.Estado == true).Select(q => new aca_MatriculaCondicional_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdMatriculaCondicional = q.IdMatriculaCondicional,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoCONDIC = q.IdCatalogoCONDIC,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
                        Estado = q.Estado
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<aca_MatriculaCondicional_Info> getList_Matricula(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                List<aca_MatriculaCondicional_Info> Lista = new List<aca_MatriculaCondicional_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    Lista = Context.aca_MatriculaCondicional.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno == IdAlumno
                    && q.Estado == true).Select(q => new aca_MatriculaCondicional_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdAnio = q.IdAnio,
                        IdMatriculaCondicional = q.IdMatriculaCondicional,
                        IdAlumno = q.IdAlumno,
                        IdCatalogoCONDIC = q.IdCatalogoCONDIC,
                        Fecha = q.Fecha,
                        Observacion = q.Observacion,
                        Estado = q.Estado
                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public aca_MatriculaCondicional_Info getInfo(int IdEmpresa, int IdMatriculaCondicional)
        {
            try
            {
                aca_MatriculaCondicional_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MatriculaCondicional.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatriculaCondicional == IdMatriculaCondicional).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MatriculaCondicional_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMatriculaCondicional = Entity.IdMatriculaCondicional,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        IdCatalogoCONDIC = Entity.IdCatalogoCONDIC,
                        Fecha = Entity.Fecha,
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

        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_MatriculaCondicional.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MatriculaCondicional.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMatriculaCondicional) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MatriculaCondicional_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaCondicional Entity = new aca_MatriculaCondicional
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdMatriculaCondicional = info.IdMatriculaCondicional = getId(info.IdEmpresa),
                        IdAlumno = info.IdAlumno,
                        IdCatalogoCONDIC = info.IdCatalogoCONDIC,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_MatriculaCondicional.Add(Entity);

                    if (info.lst_detalle != null || info.lst_detalle.Count > 0)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.lst_detalle)
                        {
                            Context.aca_MatriculaCondicional_Det.Add(new aca_MatriculaCondicional_Det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatriculaCondicional = info.IdMatriculaCondicional,
                                Secuencia = Secuencia++,
                                IdParrafo = item.IdParrafo
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

        public bool modificarDB(aca_MatriculaCondicional_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaCondicional Entity = Context.aca_MatriculaCondicional.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdMatriculaCondicional == info.IdMatriculaCondicional);
                    if (Entity == null)
                        return false;

                    Entity.Fecha = info.Fecha;
                    Entity.Observacion = info.Observacion;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = DateTime.Now;

                    var lst_Detalle = Context.aca_MatriculaCondicional_Det.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdMatriculaCondicional == info.IdMatriculaCondicional).ToList();
                    Context.aca_MatriculaCondicional_Det.RemoveRange(lst_Detalle);

                    if (info.lst_detalle != null || info.lst_detalle.Count > 0)
                    {
                        int Secuencia = 1;

                        foreach (var item in info.lst_detalle)
                        {
                            Context.aca_MatriculaCondicional_Det.Add(new aca_MatriculaCondicional_Det
                            {
                                IdEmpresa = info.IdEmpresa,
                                IdMatriculaCondicional = info.IdMatriculaCondicional,
                                Secuencia = Secuencia++,
                                IdParrafo = item.IdParrafo
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

        public bool anularDB(aca_MatriculaCondicional_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MatriculaCondicional Entity = Context.aca_MatriculaCondicional.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdMatriculaCondicional == info.IdMatriculaCondicional);
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
