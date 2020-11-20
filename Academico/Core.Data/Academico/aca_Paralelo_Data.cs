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
    public class aca_Paralelo_Data
    {
        public List<aca_Paralelo_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                var skip = args.BeginIndex;
                var take = args.EndIndex - args.BeginIndex + 1;
                List<aca_Paralelo_Info> Lista = new List<aca_Paralelo_Info>();
                Lista = getList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, skip, take, args.Filter);
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_Paralelo_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa)
        {
            int id;
            if (args.Value == null || !int.TryParse(args.Value.ToString(), out id))
                return null;
            return getInfo(IdEmpresa, (int)args.Value);
        }

        public List<aca_Paralelo_Info> getList(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel, int IdCurso)
        {
            try
            {
                List<aca_Paralelo_Info> Lista = new List<aca_Paralelo_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso==IdCurso).OrderBy(q => q.OrdenParalelo).GroupBy(q => new { q.IdParalelo, q.NomParalelo }).Select(q => new { q.Key.IdParalelo, q.Key.NomParalelo }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Paralelo_Info
                        {
                            IdParalelo = q.IdParalelo,
                            NomParalelo = q.NomParalelo,
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

        public List<aca_Paralelo_Info> getList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int skip, int take, string filter)
        {
            try
            {
                List<aca_Paralelo_Info> Lista = new List<aca_Paralelo_Info>();

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var lst = db.vwaca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso && (q.CodigoParalelo + " " + q.NomParalelo).Contains(filter)).OrderBy(q => q.OrdenParalelo).Skip(skip).Take(take);
                    foreach (var item in lst)
                    {
                        Lista.Add(new aca_Paralelo_Info
                        {
                            IdParalelo = item.IdParalelo,
                            NomParalelo = item.NomParalelo,
                            OrdenParalelo = item.OrdenParalelo,
                            CodigoParalelo = item.CodigoParalelo
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
        public List<aca_Paralelo_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Paralelo_Info> Lista = new List<aca_Paralelo_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_Paralelo p "
                    + " WHERE p.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and c.Estado = 1";
                    }
                    query += " order by p.OrdenParalelo";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Paralelo_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q => q.OrdenParalelo).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Paralelo_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdParalelo = q.IdParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            NomParalelo = q.NomParalelo,
                            OrdenParalelo = q.OrdenParalelo,
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
        //public List<aca_Paralelo_Info> getList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        //{
        //    try
        //    {
        //        List<aca_Paralelo_Info> Lista = new List<aca_Paralelo_Info>();

        //        using (EntitiesAcademico odata = new EntitiesAcademico())
        //        {
        //            var lst = odata.aca_AnioLectivo_Curso_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).OrderBy(q => q.OrdenParalelo).GroupBy(q => new { q.IdParalelo, q.NomParalelo }).Select(q => new { q.Key.IdParalelo, q.Key.NomParalelo }).ToList();

        //            lst.ForEach(q =>
        //            {
        //                Lista.Add(new aca_Paralelo_Info
        //                {
        //                    IdParalelo = q.IdParalelo,
        //                    NomParalelo = q.NomParalelo,
        //                });
        //            });
        //        }

        //        return Lista;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public aca_Paralelo_Info getInfo(int IdEmpresa, int IdParalelo)
        {
            try
            {
                aca_Paralelo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.IdParalelo == IdParalelo).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Paralelo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdParalelo = Entity.IdParalelo,
                        CodigoParalelo = Entity.CodigoParalelo,
                        NomParalelo = Entity.NomParalelo,
                        OrdenParalelo = Entity.OrdenParalelo,
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

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdParalelo) + 1;
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
                    var cont = Context.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenParalelo) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Paralelo_Info existeCodigo(int IdEmpresa, string CodigoParalelo)
        {
            try
            {
                aca_Paralelo_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_Paralelo.Where(q => q.IdEmpresa == IdEmpresa && q.CodigoParalelo.ToUpper() == CodigoParalelo.ToUpper()).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Paralelo_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdParalelo = Entity.IdParalelo,
                        CodigoParalelo = Entity.CodigoParalelo,
                        NomParalelo = Entity.NomParalelo,
                        OrdenParalelo = Entity.OrdenParalelo,
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

        public bool guardarDB(aca_Paralelo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Paralelo Entity = new aca_Paralelo
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdParalelo = info.IdParalelo = getId(info.IdEmpresa),
                        NomParalelo = info.NomParalelo,
                        CodigoParalelo = info.CodigoParalelo,
                        OrdenParalelo = info.OrdenParalelo,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Paralelo.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Paralelo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Paralelo Entity = Context.aca_Paralelo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdParalelo == info.IdParalelo);
                    if (Entity == null)
                        return false;
                    Entity.CodigoParalelo = info.CodigoParalelo;
                    Entity.NomParalelo = info.NomParalelo;
                    Entity.OrdenParalelo = info.OrdenParalelo;
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

        public bool anularDB(aca_Paralelo_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Paralelo Entity = Context.aca_Paralelo.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdParalelo == info.IdParalelo);
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
