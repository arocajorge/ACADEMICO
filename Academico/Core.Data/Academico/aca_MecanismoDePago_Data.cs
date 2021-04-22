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
    public class aca_MecanismoDePago_Data
    {
        public List<aca_MecanismoDePago_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_MecanismoDePago_Info> Lista = new List<aca_MecanismoDePago_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT mp.IdEmpresa, mp.IdMecanismo, mp.NombreMecanismo, mp.IdTerminoPago, tp.nom_TerminoPago, mp.Estado, tn.No_Descripcion "
                    + " FROM dbo.aca_MecanismoDePago AS mp WITH (nolock) INNER JOIN "
                    + " dbo.fa_TerminoPago AS tp WITH(nolock) ON mp.IdTerminoPago = tp.IdTerminoPago LEFT OUTER JOIN "
                    + " dbo.fa_TipoNota AS tn WITH(nolock) ON mp.IdEmpresa = tn.IdEmpresa AND mp.IdTipoNotaDescuentoPorRol = tn.IdTipoNota "
                    + " WHERE mp.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    { 
                        query += " and mp.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MecanismoDePago_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMecanismo = Convert.ToDecimal(reader["IdMecanismo"]),
                            NombreMecanismo = string.IsNullOrEmpty(reader["NombreMecanismo"].ToString()) ? null : reader["NombreMecanismo"].ToString(),
                            IdTerminoPago = string.IsNullOrEmpty(reader["IdTerminoPago"].ToString()) ? null : reader["IdTerminoPago"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            nom_TerminoPago = string.IsNullOrEmpty(reader["nom_TerminoPago"].ToString()) ? null : reader["nom_TerminoPago"].ToString(),
                            No_Descripcion = string.IsNullOrEmpty(reader["No_Descripcion"].ToString()) ? null : reader["No_Descripcion"].ToString(),
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

        public aca_MecanismoDePago_Info getInfo(int IdEmpresa, decimal IdMecanismo)
        {
            try
            {
                aca_MecanismoDePago_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa && q.IdMecanismo == IdMecanismo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MecanismoDePago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMecanismo = Entity.IdMecanismo,
                        NombreMecanismo = Entity.NombreMecanismo,
                        IdTerminoPago = Entity.IdTerminoPago,
                        IdTipoNotaDescuentoPorRol = Entity.IdTipoNotaDescuentoPorRol,
                        IdEmpresa_rol = Entity.IdEmpresa_rol,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_MecanismoDePago_Info getInfo_ByTermino(int IdEmpresa, string IdTerminoPago)
        {
            try
            {
                aca_MecanismoDePago_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa && q.IdTerminoPago == IdTerminoPago).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MecanismoDePago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMecanismo = Entity.IdMecanismo,
                        NombreMecanismo = Entity.NombreMecanismo,
                        IdTerminoPago = Entity.IdTerminoPago,
                        IdTipoNotaDescuentoPorRol = Entity.IdTipoNotaDescuentoPorRol,
                        IdEmpresa_rol = Entity.IdEmpresa_rol,
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_MecanismoDePago_Info getInfo_by_IdTermino(int IdEmpresa, string IdTerminoPago)
        {
            try
            {
                aca_MecanismoDePago_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa && q.IdTerminoPago == IdTerminoPago).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_MecanismoDePago_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdMecanismo = Entity.IdMecanismo,
                        NombreMecanismo = Entity.NombreMecanismo,
                        IdTerminoPago = Entity.IdTerminoPago,
                        IdTipoNotaDescuentoPorRol = Entity.IdTipoNotaDescuentoPorRol,
                        IdEmpresa_rol = Entity.IdEmpresa_rol
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
                    var cont = Context.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_MecanismoDePago.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMecanismo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_MecanismoDePago_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MecanismoDePago Entity = new aca_MecanismoDePago
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMecanismo = info.IdMecanismo = getId(info.IdEmpresa),
                        NombreMecanismo = info.NombreMecanismo,
                        IdTerminoPago = info.IdTerminoPago,
                        IdTipoNotaDescuentoPorRol = info.IdTipoNotaDescuentoPorRol,
                        IdEmpresa_rol = info.IdEmpresa_rol,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = DateTime.Now
                    };
                    Context.aca_MecanismoDePago.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_MecanismoDePago_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MecanismoDePago Entity = Context.aca_MecanismoDePago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMecanismo == info.IdMecanismo);
                    if (Entity == null)
                        return false;

                    Entity.NombreMecanismo = info.NombreMecanismo;
                    Entity.IdTerminoPago = info.IdTerminoPago;
                    Entity.IdTipoNotaDescuentoPorRol = info.IdTipoNotaDescuentoPorRol;
                    Entity.IdEmpresa_rol = info.IdEmpresa_rol;
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

        public bool anularDB(aca_MecanismoDePago_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_MecanismoDePago Entity = Context.aca_MecanismoDePago.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMecanismo == info.IdMecanismo);
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
