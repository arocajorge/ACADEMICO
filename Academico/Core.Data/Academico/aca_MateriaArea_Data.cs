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
    public class aca_MateriaArea_Data
    {
        public List<aca_MateriaArea_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_MateriaArea_Info> Lista = new List<aca_MateriaArea_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_MateriaArea WITH (nolock) "
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
                        Lista.Add(new aca_MateriaArea_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMateriaArea = Convert.ToInt32(reader["IdMateriaArea"]),
                            NomMateriaArea = reader["NomMateriaArea"].ToString(),
                            OrdenMateriaArea = Convert.ToInt32(reader["OrdenMateriaArea"]),
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

        public aca_MateriaArea_Info getInfo(int IdEmpresa, int IdMateriaArea)
        {
            try
            {
                aca_MateriaArea_Info info = new aca_MateriaArea_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_MateriaArea WITH (nolock) "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMateriaArea = " + IdMateriaArea.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_MateriaArea_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMateriaArea = Convert.ToInt32(reader["IdMateriaArea"]),
                            NomMateriaArea = reader["NomMateriaArea"].ToString(),
                            OrdenMateriaArea = Convert.ToInt32(reader["OrdenMateriaArea"]),
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
                    var cont = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMateriaArea) + 1;
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
                    var cont = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_MateriaArea.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenMateriaArea) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MateriaArea_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaArea Entity = new aca_MateriaArea
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMateriaArea = info.IdMateriaArea = getId(info.IdEmpresa),
                        NomMateriaArea = info.NomMateriaArea,
                        OrdenMateriaArea = info.OrdenMateriaArea,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_MateriaArea.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_MateriaArea_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaArea Entity = Context.aca_MateriaArea.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaArea == info.IdMateriaArea);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomMateriaArea = info.NomMateriaArea;
                    Entity.OrdenMateriaArea = info.OrdenMateriaArea;
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

        public bool anularDB(aca_MateriaArea_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MateriaArea Entity = Context.aca_MateriaArea.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateriaArea == info.IdMateriaArea);
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
