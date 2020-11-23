using Core.Data.Base;
using Core.Info.Academico;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Sede_Data
    {
        public List<aca_Sede_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Sede_Info> Lista = new List<aca_Sede_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_Sede s "
                    + " WHERE s.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados==false)
                    {
                        query += " and s.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Sede_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            NomSede = reader["NomSede"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            NombreRector = reader["NombreRector"].ToString(),
                            NombreSecretaria = reader["NombreSecretaria"].ToString(),
                            CelularRector = reader["CelularRector"].ToString(),
                            TelefonoRector = reader["TelefonoRector"].ToString(),
                            CorreoRector = reader["CorreoRector"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }

                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Sede.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Sede_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdSucursal = q.IdSucursal,
                            NomSede = q.NomSede,
                            Direccion = q.Direccion,
                            NombreRector = q.NombreRector,
                            TelefonoRector = q.TelefonoRector,
                            CelularRector=q.CelularRector,
                            CorreoRector = q.CorreoRector,
                            NombreSecretaria = q.NombreSecretaria,
                            Estado = q.Estado
                        });
                    });
                }*/

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Sede_Info> GetList(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<aca_Sede_Info> Lista = new List<aca_Sede_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT IdSede, NomSede FROM aca_AnioLectivo_Sede_NivelAcademico "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdAnio = "+IdAnio.ToString() + " GROUP BY IdSede, NomSede";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Sede_Info
                        {
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            NomSede = reader["NomSede"].ToString()
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Sede_NivelAcademico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).GroupBy(q => new { q.IdSede, q.NomSede }).Select(q => new { q.Key.IdSede, q.Key.NomSede }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Sede_Info
                        {
                            IdSede = q.IdSede,
                            NomSede = q.NomSede,
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

        public aca_Sede_Info GetInfo(int IdEmpresa, int IdSede)
        {
            try
            {
                aca_Sede_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Sede.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Sede_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdSede = Entity.IdSede,
                        IdSucursal = Entity.IdSucursal,
                        NomSede = Entity.NomSede,
                        Direccion = Entity.Direccion,
                        NombreRector = Entity.NombreRector,
                        NombreSecretaria = Entity.NombreSecretaria,
                        TelefonoRector = Entity.TelefonoRector,
                        CelularRector = Entity.CelularRector,
                        CorreoRector = Entity.CorreoRector,
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

        public List<aca_Sede_Info> GetListSinEmpresa(bool mostrar_anulados)
        {
            try
            {
                List<aca_Sede_Info> Lista = new List<aca_Sede_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_Sede s ";
                    if (mostrar_anulados == false)
                    {
                        query += " WHERE s.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Sede_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdSucursal = Convert.ToInt32(reader["IdSucursal"]),
                            NomSede = reader["NomSede"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            NombreRector = reader["NombreRector"].ToString(),
                            NombreSecretaria = reader["NombreSecretaria"].ToString(),
                            CelularRector = reader["CelularRector"].ToString(),
                            TelefonoRector = reader["TelefonoRector"].ToString(),
                            CorreoRector = reader["CorreoRector"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.aca_Sede.Where(q => q.Estado == (mostrar_anulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Sede_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSede = q.IdSede,
                            IdSucursal = q.IdSucursal,
                            NomSede = q.NomSede,
                            Direccion = q.Direccion,
                            NombreRector = q.NombreRector,
                            NombreSecretaria = q.NombreSecretaria,
                            TelefonoRector = q.TelefonoRector,
                            CelularRector = q.CelularRector,
                            CorreoRector = q.CorreoRector,
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

        public List<aca_Sede_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<aca_Sede_Info> Lista = new List<aca_Sede_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter);
            return Lista;
        }

        public aca_Sede_Info get_info_bajo_demanda(int IdEmpresa, ListEditItemRequestedByValueEventArgs args)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, (int)args.Value);
        }

        public List<aca_Sede_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<aca_Sede_Info> Lista = new List<aca_Sede_Info>();

                EntitiesAcademico Context = new EntitiesAcademico();

                var lstg = Context.aca_Sede.Where(q => q.Estado == true && q.IdEmpresa == IdEmpresa && (q.IdSede.ToString() + " " + q.NomSede).Contains(filter)).OrderBy(q => q.IdSucursal).Skip(skip).Take(take);
                foreach (var q in lstg)
                {
                    Lista.Add(new aca_Sede_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdSucursal = q.IdSucursal,
                        IdSede = q.IdSede,
                        NomSede = q.NomSede
                    });
                }

                Context.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Sede_Info get_info_demanda(int IdEmpresa, int value)
        {
            aca_Sede_Info info = new aca_Sede_Info();
            using (EntitiesAcademico Contex = new EntitiesAcademico())
            {
                info = (from q in Contex.aca_Sede
                        where q.IdEmpresa == IdEmpresa
                        && q.IdSede == value
                        select new aca_Sede_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdSucursal = q.IdSucursal,
                            IdSede = q.IdSede,
                            NomSede = q.NomSede
                        }).FirstOrDefault();
            }
            return info;
        }

        public int get_id(int IdEmpresa)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Sede.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Sede.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdSede) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Sede_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Sede Entity = new aca_Sede
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdSucursal = info.IdSucursal,
                        IdSede = info.IdSede = get_id(info.IdEmpresa),
                        NomSede = info.NomSede,
                        NombreRector = info.NombreRector,
                        TelefonoRector = info.TelefonoRector,
                        CelularRector = info.CelularRector,
                        CorreoRector = info.CorreoRector,
                        NombreSecretaria = info.NombreSecretaria,
                        Direccion = info.Direccion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Sede.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Sede_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Sede Entity = Context.aca_Sede.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSede == info.IdSede);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.IdSucursal = info.IdSucursal;
                    Entity.NomSede = info.NomSede;
                    Entity.NombreRector = info.NombreRector;
                    Entity.TelefonoRector = info.TelefonoRector;
                    Entity.CelularRector = info.CelularRector;
                    Entity.CorreoRector = info.CorreoRector;
                    Entity.NombreSecretaria = info.NombreSecretaria;
                    Entity.Direccion = info.Direccion;

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

        public bool anularDB(aca_Sede_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Sede Entity = Context.aca_Sede.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdSede == info.IdSede);
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
