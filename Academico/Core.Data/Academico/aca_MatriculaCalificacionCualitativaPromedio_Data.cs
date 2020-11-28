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
    public class aca_MatriculaCalificacionCualitativaPromedio_Data
    {
        public List<aca_MatriculaCalificacionCualitativaPromedio_Info> getList(int IdEmpresa, decimal IdMatricula)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativaPromedio_Info> Lista = new List<aca_MatriculaCalificacionCualitativaPromedio_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT * FROM aca_MatriculaCalificacionCualitativaPromedio "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdMatricula = " + IdMatricula.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativaPromedio_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdProfesor = string.IsNullOrEmpty(reader["IdProfesor"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["IdProfesor"]),
                            IdCalificacionCualitativaQ1 = string.IsNullOrEmpty(reader["IdCalificacionCualitativaQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativaQ1"]),
                            IdCalificacionCualitativaQ2 = string.IsNullOrEmpty(reader["IdCalificacionCualitativaQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativaQ2"]),
                            IdCalificacionCualitativaFinal = string.IsNullOrEmpty(reader["IdCalificacionCualitativaFinal"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativaFinal"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioQ2"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioFinal"])
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.aca_MatriculaCalificacionCualitativaPromedio.Where(q => q.IdEmpresa == IdEmpresa && q.IdMatricula == IdMatricula).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_MatriculaCalificacionCualitativaPromedio_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdProfesor = q.IdProfesor,
                            IdCalificacionCualitativaQ1 = q.IdCalificacionCualitativaQ1,
                            IdCalificacionCualitativaQ2 = q.IdCalificacionCualitativaQ2,
                            IdCalificacionCualitativaFinal=q.IdCalificacionCualitativaFinal,
                            PromedioQ1=q.PromedioQ1,
                            PromedioQ2 = q.PromedioQ2,
                            PromedioFinal = q.PromedioFinal
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

        public bool generarCalificacion(List<aca_MatriculaCalificacionCualitativaPromedio_Info> lst_calificacion_promedio)
        {
            try
            {
                List<aca_MatriculaCalificacionCualitativaPromedio_Info> Lista = new List<aca_MatriculaCalificacionCualitativaPromedio_Info>();

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_matricula = (from q in lst_calificacion_promedio
                                         group q by new
                                         {
                                             q.IdEmpresa,
                                             q.IdMatricula
                                         } into mat
                                         select new aca_Matricula_Info
                                         {
                                             IdEmpresa = mat.Key.IdEmpresa,
                                             IdMatricula = mat.Key.IdMatricula
                                         }).ToList();

                    foreach (var item in lst_matricula)
                    {
                        var lista_calificacion_promedio = Context.aca_MatriculaCalificacionCualitativaPromedio.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();
                        Context.aca_MatriculaCalificacionCualitativaPromedio.RemoveRange(lista_calificacion_promedio);

                        var lst_x_matricula = lst_calificacion_promedio.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).ToList();

                        if (lst_x_matricula != null)
                        {
                            foreach (var info in lst_x_matricula)
                            {
                                aca_MatriculaCalificacionCualitativaPromedio Entity = new aca_MatriculaCalificacionCualitativaPromedio
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdMatricula = info.IdMatricula,
                                    IdMateria = info.IdMateria,
                                    IdProfesor = info.IdProfesor,
                                    IdCalificacionCualitativaQ1 = info.IdCalificacionCualitativaQ1,
                                    IdCalificacionCualitativaQ2 = info.IdCalificacionCualitativaQ2,
                                    IdCalificacionCualitativaFinal = info.IdCalificacionCualitativaFinal,
                                    PromedioQ1 = info.PromedioQ1,
                                    PromedioQ2 = info.PromedioQ2,
                                    PromedioFinal = info.PromedioFinal
                                };

                                Context.aca_MatriculaCalificacionCualitativaPromedio.Add(Entity);
                            }
                        }
                        Context.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
