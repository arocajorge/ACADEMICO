using Core.Data.Base;
using Core.Info.Helps;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_048_Promedios_Data
    {
        public List<ACA_048_Promedios_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcialTipo)
        {
            try
            {
                List<ACA_048_Promedios_Info> Lista = new List<ACA_048_Promedios_Info>();
                List<ACA_048_Promedios_Info> ListaFinal = new List<ACA_048_Promedios_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "select  x.IdEmpresa, x.IdAnio, x.IdSede, x.IdNivel, x.IdJornada, x.IdCurso, x.IdParalelo, x.IdMateria, "
                    + " x.PromedioFinalQ1, q1.Codigo CodigoQ1, x.PromedioFinalQ2, q2.Codigo CodigoQ2 "
                    + " from( "
                    + " select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, d.IdMateria, "
                    + " dbo.BankersRounding(AVG(d.PromedioQ1), 2) PromedioFinalQ1, dbo.BankersRounding(AVG(d.PromedioQ2), 2) PromedioFinalQ2 "
                    + " from aca_Matricula as a WITH (nolock) inner join  aca_Alumno as b WITH (nolock) on a.IdEmpresa = b.IdEmpresa and a.IdAlumno = b.IdAlumno "
                    + " inner join  tb_persona as c WITH (nolock) on b.IdPersona = c.IdPersona left join  aca_MatriculaCalificacionCualitativaPromedio as d WITH (nolock) "
                    + " on a.IdEmpresa = d.IdEmpresa and a.IdMatricula = d.IdMatricula "
                    + " where a.IdEmpresa = " + IdEmpresa
                    + " and a.IdAnio = " + IdAnio
                    + " and a.IdSede = " + IdSede
                    + " and a.IdNivel = " + IdNivel
                    + " and a.IdJornada = " + IdJornada
                    + " and a.IdCurso = " + IdCurso
                    + " and a.IdParalelo = " + IdParalelo
                    + " and d.IdMateria = " + IdMateria
                    + " and b.Estado = 1 "
                    + " AND NOT EXISTS(SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F WITH (nolock) where a.IdEmpresa = f.IdEmpresa and a.IdMatricula = f.IdMatricula and f.Estado = 1) "
                    + " group by a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, d.IdMateria "
                    + " ) x "
                    + " left join aca_AnioLectivoCalificacionCualitativa q1 WITH (nolock) on q1.IdEmpresa = x.IdEmpresa and q1.IdAnio = x.IdAnio and q1.Calificacion >= x.PromedioFinalQ1 "
                    + " left join aca_AnioLectivoCalificacionCualitativa q2 WITH (nolock) on q2.IdEmpresa = x.IdEmpresa and q2.IdAnio = x.IdAnio and q2.Calificacion >= x.PromedioFinalQ2";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_048_Promedios_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            PromedioFinalQ1 = string.IsNullOrEmpty(reader["PromedioFinalQ1"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioFinalQ1"]),
                            PromedioFinalQ2 = string.IsNullOrEmpty(reader["PromedioFinalQ2"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioFinalQ2"]),
                            CodigoQ1 = string.IsNullOrEmpty(reader["CodigoQ1"].ToString()) ? null : reader["CodigoQ1"].ToString(),
                            CodigoQ2 = string.IsNullOrEmpty(reader["CodigoQ2"].ToString()) ? null : reader["CodigoQ2"].ToString(),
                            PromedioFinal = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                (string.IsNullOrEmpty(reader["PromedioFinalQ1"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioFinalQ1"])) :
                                ((IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)) ?
                                   string.IsNullOrEmpty(reader["PromedioFinalQ2"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioFinalQ2"]) :
                                   null)
                                ),
                            Codigo = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                        (string.IsNullOrEmpty(reader["CodigoQ1"].ToString()) ? null : reader["CodigoQ1"].ToString()) :
                                            ((IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)) ?
                                            string.IsNullOrEmpty(reader["CodigoQ2"].ToString()) ? null : reader["CodigoQ2"].ToString() :
                                       null)
                                   )
                        });
                    }
                    reader.Close();
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
