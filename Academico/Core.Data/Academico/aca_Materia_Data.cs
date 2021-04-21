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
    public class aca_Materia_Data
    {
        public List<aca_Materia_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Materia_Info> Lista = new List<aca_Materia_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT m.IdEmpresa, m.IdMateria, m.IdMateriaArea, m.IdMateriaGrupo, m.OrdenMateria, ma.OrdenMateriaArea, mg.OrdenMateriaGrupo, m.NomMateria, ma.NomMateriaArea, mg.NomMateriaGrupo, mg.PromediarGrupo, m.EsObligatorio,  "
                    + " m.Estado, m.IdCatalogoTipoCalificacion, c.NomCatalogo "
                    + " FROM dbo.aca_Materia AS m WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c WITH (nolock) ON m.IdCatalogoTipoCalificacion = c.IdCatalogo LEFT OUTER JOIN "
                    + " dbo.aca_MateriaArea AS ma WITH (nolock) ON m.IdEmpresa = ma.IdEmpresa AND m.IdMateriaArea = ma.IdMateriaArea LEFT OUTER JOIN "
                    + " dbo.aca_MateriaGrupo AS mg WITH (nolock) ON m.IdEmpresa = mg.IdEmpresa AND m.IdMateriaGrupo = mg.IdMateriaGrupo "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and m.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Materia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            NomMateriaGrupo = reader["NomMateriaGrupo"].ToString(),
                            OrdenMateria = Convert.ToInt32(reader["OrdenMateria"]),
                            EsObligatorio = Convert.ToBoolean(reader["EsObligatorio"]),
                            IdMateriaGrupo = string.IsNullOrEmpty(reader["IdMateriaGrupo"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdMateriaGrupo"]),
                            IdMateriaArea = string.IsNullOrEmpty(reader["IdMateriaArea"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdMateriaArea"]),
                            IdCatalogoTipoCalificacion = Convert.ToInt32(reader["IdCatalogoTipoCalificacion"]),
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

        public List<aca_Materia_Info> getList(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso)
        {
            try
            {
                List<aca_Materia_Info> Lista = new List<aca_Materia_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_AnioLectivo_Curso_Materia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdSede == IdSede && q.IdNivel == IdNivel && q.IdJornada == IdJornada && q.IdCurso == IdCurso).OrderBy(q => q.OrdenMateria).GroupBy(q => new { q.IdMateria, q.NomMateria }).Select(q => new { q.Key.IdMateria, q.Key.NomMateria }).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Materia_Info
                        {
                            IdMateria = q.IdMateria,
                            NomMateria = q.NomMateria
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
        public aca_Materia_Info getInfo(int IdEmpresa, int IdMateria)
        {
            try
            {
                aca_Materia_Info info = new aca_Materia_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT m.IdEmpresa, m.IdMateria, m.IdMateriaArea, m.IdMateriaGrupo, m.OrdenMateria, ma.OrdenMateriaArea, mg.OrdenMateriaGrupo, m.NomMateria, ma.NomMateriaArea, mg.NomMateriaGrupo, mg.PromediarGrupo, m.EsObligatorio,  "
                    + " m.Estado, m.IdCatalogoTipoCalificacion, c.NomCatalogo "
                    + " FROM dbo.aca_Materia AS m WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c WITH (nolock) ON m.IdCatalogoTipoCalificacion = c.IdCatalogo LEFT OUTER JOIN "
                    + " dbo.aca_MateriaArea AS ma WITH (nolock) ON m.IdEmpresa = ma.IdEmpresa AND m.IdMateriaArea = ma.IdMateriaArea LEFT OUTER JOIN "
                    + " dbo.aca_MateriaGrupo AS mg WITH (nolock) ON m.IdEmpresa = mg.IdEmpresa AND m.IdMateriaGrupo = mg.IdMateriaGrupo "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdMateria = "+ IdMateria.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Materia_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdMateriaGrupo = string.IsNullOrEmpty(reader["IdMateriaGrupo"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdMateriaGrupo"]),
                            IdMateriaArea = string.IsNullOrEmpty(reader["IdMateriaArea"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdMateriaArea"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            NomMateriaGrupo = string.IsNullOrEmpty(reader["NomMateriaGrupo"].ToString()) ? null : reader["NomMateriaGrupo"].ToString(),
                            NomMateriaArea = string.IsNullOrEmpty(reader["NomMateriaArea"].ToString()) ? null : reader["NomMateriaArea"].ToString(),
                            OrdenMateria = Convert.ToInt32(reader["OrdenMateria"]),
                            OrdenMateriaGrupo = string.IsNullOrEmpty(reader["OrdenMateriaGrupo"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateriaGrupo"]),
                            OrdenMateriaArea = string.IsNullOrEmpty(reader["OrdenMateriaArea"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateriaArea"]),
                            PromediarGrupo = string.IsNullOrEmpty(reader["PromediarGrupo"].ToString()) ? false : Convert.ToBoolean(reader["PromediarGrupo"]),
                            EsObligatorio = Convert.ToBoolean(reader["EsObligatorio"]),
                            IdCatalogoTipoCalificacion = string.IsNullOrEmpty(reader["IdCatalogoTipoCalificacion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoTipoCalificacion"]),
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
                    var cont = Context.aca_Materia.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Materia.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdMateria) + 1;
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
                    var cont = Context.aca_Materia.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Count();
                    if (cont > 0)
                        ID = Context.aca_Materia.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true).Max(q => q.OrdenMateria) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Materia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Materia Entity = new aca_Materia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdMateria = info.IdMateria = getId(info.IdEmpresa),
                        NomMateria = info.NomMateria,
                        IdMateriaGrupo = (info.IdMateriaGrupo==0 ? null : info.IdMateriaGrupo),
                        IdMateriaArea = (info.IdMateriaArea == 0 ? null : info.IdMateriaArea),
                        OrdenMateria = info.OrdenMateria,
                        EsObligatorio = info.EsObligatorio,
                        IdCatalogoTipoCalificacion = info.IdCatalogoTipoCalificacion,
                        Estado = true,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Materia.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Materia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Materia Entity = Context.aca_Materia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateria == info.IdMateria);
                    if (Entity == null)
                        return false;
                    Entity.IdEmpresa = info.IdEmpresa;
                    Entity.NomMateria = info.NomMateria;
                    Entity.OrdenMateria = info.OrdenMateria;
                    Entity.IdMateriaGrupo = (info.IdMateriaGrupo == 0 ? null : info.IdMateriaGrupo);
                    Entity.IdMateriaArea = (info.IdMateriaArea == 0 ? null : info.IdMateriaArea);
                    Entity.EsObligatorio = info.EsObligatorio;
                    Entity.IdCatalogoTipoCalificacion = info.IdCatalogoTipoCalificacion;
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

        public bool anularDB(aca_Materia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Materia Entity = Context.aca_Materia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdMateria == info.IdMateria);
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
