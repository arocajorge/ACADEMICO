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
    public class aca_Documento_Data
    {
        public List<aca_Documento_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<aca_Documento_Info> Lista = new List<aca_Documento_Info>();
            Lista = get_list(IdEmpresa, skip, take, args.Filter);
            return Lista;
        }

        public aca_Documento_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info_demanda(IdEmpresa, Convert.ToInt32(args.Value));
        }

        public aca_Documento_Info get_info_demanda(int IdEmpresa,int IdDocumento)
        {
            aca_Documento_Info info = new aca_Documento_Info();

            using (EntitiesAcademico Contex = new EntitiesAcademico())
            {
                info = (from q in Contex.aca_Documento
                        where q.IdEmpresa == IdEmpresa 
                        && q.IdDocumento==IdDocumento
                        select new aca_Documento_Info
                        {
                            IdDocumento = q.IdDocumento,
                            NomDocumento = q.NomDocumento,
                        }).FirstOrDefault();

            }

            return info;
        }

        public List<aca_Documento_Info> get_list(int IdEmpresa, int skip, int take, string filter)
        {
            try
            {
                List<aca_Documento_Info> Lista = new List<aca_Documento_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = (from a in Context.aca_Documento
                               where
                            a.IdEmpresa == IdEmpresa
                            && (a.IdDocumento.ToString() + " " + a.NomDocumento).Contains(filter)
                               select new
                               {
                                   a.IdDocumento,
                                   a.NomDocumento
                               })
                             .OrderBy(a => a.IdDocumento)
                             .Skip(skip)
                             .Take(take)
                             .ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new aca_Documento_Info
                        {
                            IdDocumento = q.IdDocumento,
                            NomDocumento = q.NomDocumento
                        });
                    }
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_Documento_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Documento_Info> Lista = new List<aca_Documento_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_Documento "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and Estado = 1";
                    }
                    query += " order by OrdenDocumento";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Documento_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdDocumento = Convert.ToInt32(reader["IdDocumento"]),
                            NomDocumento = reader["NomDocumento"].ToString(),
                            OrdenDocumento = Convert.ToInt32(reader["OrdenDocumento"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q => q.OrdenDocumento).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Documento_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdDocumento = q.IdDocumento,
                            NomDocumento = q.NomDocumento,
                            OrdenDocumento = q.OrdenDocumento,
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
        public aca_Documento_Info getInfo(int IdEmpresa, int IdDocumento)
        {
            try
            {
                aca_Documento_Info info = new aca_Documento_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_Documento "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdDocumento = " + IdDocumento.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Documento_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdDocumento = Convert.ToInt32(reader["IdDocumento"]),
                            NomDocumento = reader["NomDocumento"].ToString(),
                            OrdenDocumento = Convert.ToInt32(reader["OrdenDocumento"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.IdDocumento == IdDocumento).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Documento_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdDocumento = Entity.IdDocumento,
                        NomDocumento = Entity.NomDocumento,
                        OrdenDocumento = Entity.OrdenDocumento,
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
                    var cont = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdDocumento) + 1;
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
                    var cont = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_Documento.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenDocumento) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Documento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Documento Entity = new aca_Documento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdDocumento = info.IdDocumento = getId(info.IdEmpresa),
                        NomDocumento = info.NomDocumento,
                        OrdenDocumento = info.OrdenDocumento??0,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Documento.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Documento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Documento Entity = Context.aca_Documento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDocumento == info.IdDocumento);
                    if (Entity == null)
                        return false;

                    Entity.NomDocumento = info.NomDocumento;
                    Entity.OrdenDocumento = info.OrdenDocumento??0;
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

        public bool anularDB(aca_Documento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Documento Entity = Context.aca_Documento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdDocumento == info.IdDocumento);
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
