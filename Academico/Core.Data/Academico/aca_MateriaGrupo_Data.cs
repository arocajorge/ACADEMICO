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
    public class aca_MateriaGrupo_Data
    {
        public List<aca_MateriaGrupo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_MateriaGrupo_Info> Lista = new List<aca_MateriaGrupo_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_MateriaGrupo WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MateriaGrupo_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMateriaGrupo = Convert.ToInt32(reader["IdMateriaGrupo"]),
                            NomMateriaGrupo = reader["NomMateriaGrupo"].ToString(),
                            OrdenMateriaGrupo = Convert.ToInt32(reader["OrdenMateriaGrupo"]),
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

        public aca_MateriaGrupo_Info getInfo(int IdEmpresa, int IdMateriaGrupo)
        {
            try
            {
                aca_MateriaGrupo_Info info = new aca_MateriaGrupo_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_MateriaGrupo WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMateriaGrupo = "+IdMateriaGrupo.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_MateriaGrupo_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMateriaGrupo = Convert.ToInt32(reader["IdMateriaGrupo"]),
                            NomMateriaGrupo = reader["NomMateriaGrupo"].ToString(),
                            OrdenMateriaGrupo = Convert.ToInt32(reader["OrdenMateriaGrupo"]),
                            PromediarGrupo = Convert.ToBoolean(reader["PromediarGrupo"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
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

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMateriaGrupo) + 1;
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
                    var cont = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaGrupo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenMateriaGrupo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MateriaGrupo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaGrupo Entity = new aca_MateriaGrupo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMateriaGrupo = info.IdMateriaGrupo = getId(info.IdEmpresa),
                        NomMateriaGrupo = info.NomMateriaGrupo,
                        OrdenMateriaGrupo = info.OrdenMateriaGrupo,
                        PromediarGrupo = (info.PromediarGrupo==null ? false : info.PromediarGrupo),
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_MateriaGrupo.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_MateriaGrupo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaGrupo Entity = Context.aca_MateriaGrupo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaGrupo == info.IdMateriaGrupo);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomMateriaGrupo = info.NomMateriaGrupo;
                    Entity.OrdenMateriaGrupo = info.OrdenMateriaGrupo;
                    Entity.PromediarGrupo = (info.PromediarGrupo == null ? false : info.PromediarGrupo);
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

        public bool anularDB(aca_MateriaGrupo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaGrupo Entity = Context.aca_MateriaGrupo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaGrupo == info.IdMateriaGrupo);
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
