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
    public class aca_Curso_Data
    {
        aca_Matricula_Data odataMatricula = new aca_Matricula_Data();
        public List<aca_Curso_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Curso_Info> Lista = new List<aca_Curso_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_Curso c WITH (nolock) "
                    + " WHERE c.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and c.Estado = 1";
                    }
                    query += " order by c.OrdenCurso";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Curso_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdCursoAPromover = string.IsNullOrEmpty(reader["IdCursoAPromover"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCursoAPromover"]),
                            NomCurso = reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
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
        public List<aca_Curso_Info> getList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada)
        {
            try
            {
                List<aca_Curso_Info> Lista = new List<aca_Curso_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada).OrderBy(q => q.OrdenCurso).GroupBy(q => new { q.IdCurso, q.NomCurso }).Select(q => new { q.Key.IdCurso, q.Key.NomCurso }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Curso_Info
                        {
                            IdCurso = q.IdCurso,
                            NomCurso = q.NomCurso,
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

        public List<aca_Curso_Info> getList_Combos(int IdEmpresa, int IdAnio, int IdSede, int IdJornada, int IdNivel)
        {
            try
            {
                List<aca_Curso_Info> Lista = new List<aca_Curso_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel ).OrderBy(q => q.OrdenCurso).GroupBy(q => new { q.IdCurso, q.NomCurso }).Select(q => new { q.Key.IdCurso, q.Key.NomCurso }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Curso_Info
                        {
                            IdCurso = q.IdCurso,
                            NomCurso = q.NomCurso,
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

        public List<aca_Curso_Info> getList_CambioCurso(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, decimal IdMatricula)
        {
            try
            {
                List<aca_Curso_Info> Lista = new List<aca_Curso_Info>();
                //var info_matricula = odataMatricula.getInfo(IdEmpresa, IdMatricula);
                //var IdCursoActualMatricula = (info_matricula == null ? 0 : info_matricula.IdCurso);
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Jornada_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel 
                    && q.IdJornada == IdJornada).OrderBy(q => q.OrdenCurso).GroupBy(q => new { q.IdCurso, q.NomCurso }).Select(q => new { q.Key.IdCurso, q.Key.NomCurso }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Curso_Info
                        {
                            IdCurso = q.IdCurso,
                            NomCurso = q.NomCurso,
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
        public aca_Curso_Info getInfo(int IdEmpresa, int IdCurso)
        {
            try
            {
                aca_Curso_Info info = new aca_Curso_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_Curso c WITH (nolock) "
                    + " WHERE c.IdEmpresa = " + IdEmpresa.ToString() + " and c.IdCurso = " + IdCurso.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Curso_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdCursoAPromover = string.IsNullOrEmpty(reader["IdCursoAPromover"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCursoAPromover"]),
                            NomCurso = reader["NomCurso"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
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
                    var cont = Context.aca_Curso.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Curso.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdCurso) + 1;
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
                    var cont = Context.aca_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.Estado== true).Count();
                    if (cont > 0)
                        ID = Context.aca_Curso.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenCurso) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Curso_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Curso Entity = new aca_Curso
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdCurso = info.IdCurso = getId(info.IdEmpresa),
                        IdCursoAPromover = info.IdCursoAPromover,
                        NomCurso = info.NomCurso,
                        OrdenCurso = info.OrdenCurso,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Curso.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Curso_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Curso Entity = Context.aca_Curso.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCurso == info.IdCurso);
                    if (Entity == null)
                        return false;

                    Entity.IdCursoAPromover = info.IdCursoAPromover;
                    Entity.NomCurso = info.NomCurso;
                    Entity.OrdenCurso = info.OrdenCurso;
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

        public bool anularDB(aca_Curso_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Curso Entity = Context.aca_Curso.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdCurso == info.IdCurso);
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
