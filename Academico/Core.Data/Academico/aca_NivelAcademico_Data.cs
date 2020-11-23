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
    public class aca_NivelAcademico_Data
    {
        public List<aca_NivelAcademico_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_NivelAcademico_Info> Lista = new List<aca_NivelAcademico_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_NivelAcademico n "
                    + " WHERE n.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and n.Estado = 1";
                    }
                    query += " order by n.Orden";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_NivelAcademico_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            NomNivel = reader["NomNivel"].ToString(),
                            Orden = Convert.ToInt32(reader["Orden"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q=>q.Orden).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_NivelAcademico_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdNivel = q.IdNivel,
                            NomNivel = q.NomNivel,
                            Orden = q.Orden,
                            Estado = q.Estado
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

        public List<aca_NivelAcademico_Info> getList(int IdEmpresa, int IdAnio, int IdSede)
        {
            try
            {
                List<aca_NivelAcademico_Info> Lista = new List<aca_NivelAcademico_Info>();
                
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Sede_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede).GroupBy(q=> new { q.IdNivel, q.NomNivel}).Select(q=> new { q.Key.IdNivel, q.Key.NomNivel}).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_NivelAcademico_Info
                        {
                            IdNivel = q.IdNivel,
                            NomNivel = q.NomNivel,
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

        public aca_NivelAcademico_Info getInfo(int IdEmpresa, int IdNivel)
        {
            try
            {
                aca_NivelAcademico_Info info = new aca_NivelAcademico_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT* FROM aca_NivelAcademico n "
                   + " WHERE n.IdEmpresa = " + IdEmpresa.ToString() + " and n.IdNivel = " + IdNivel.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_NivelAcademico_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            NomNivel = reader["NomNivel"].ToString(),
                            Orden = Convert.ToInt32(reader["Orden"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdNivel == IdNivel).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_NivelAcademico_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdNivel = Entity.IdNivel,
                        NomNivel = Entity.NomNivel,
                        Orden = Entity.Orden,
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

        public int getId(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdNivel) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int getOrden(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.Orden) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_NivelAcademico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_NivelAcademico Entity = new aca_NivelAcademico
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdNivel = info.IdNivel = getId(info.IdEmpresa),
                        NomNivel = info.NomNivel,
                        Orden = info.Orden,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_NivelAcademico.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_NivelAcademico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_NivelAcademico Entity = Context.aca_NivelAcademico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomNivel = info.NomNivel;
                    Entity.Orden = info.Orden;
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

        public bool anularDB(aca_NivelAcademico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_NivelAcademico Entity = Context.aca_NivelAcademico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdNivel == info.IdNivel);
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
