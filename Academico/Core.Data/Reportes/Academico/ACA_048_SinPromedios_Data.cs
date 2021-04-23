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
    public class ACA_048_SinPromedios_Data
    {
        public List<ACA_048_SinPromedios_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcialTipo)
        {
            try
            {
                List<ACA_048_SinPromedios_Info> Lista = new List<ACA_048_SinPromedios_Info>();
                List<ACA_048_SinPromedios_Info> ListaFinal = new List<ACA_048_SinPromedios_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "select mc.IdEmpresa, mc.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, "
                    + " mc.IdMateria, cm.NomMateria, cm.OrdenMateriaGrupo, cm.OrdenMateria, mc.IdCalificacionCualitativaQ1, mc.PromedioQ1, mc.IdCalificacionCualitativaQ2, mc.PromedioQ2 "
                    + " from aca_MatriculaCalificacionCualitativaPromedio mc WITH (nolock) "
                    + " inner join aca_Matricula m WITH (nolock) on m.IdEmpresa = mc.IdEmpresa and m.IdMatricula = mc.IdMatricula "
                    + " inner join aca_Alumno a WITH (nolock) on a.IdEmpresa = m.IdEmpresa and a.IdAlumno = m.IdAlumno "
                    + " LEFT OUTER JOIN dbo.tb_persona AS p WITH (nolock) ON a.IdPersona = p.IdPersona "
                    + " LEFT OUTER JOIN dbo.aca_AnioLectivo_Curso_Materia AS cm WITH (nolock) ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede "
                    + " AND m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND mc.IdMateria = cm.IdMateria "
                    + " where mc.IdEmpresa = " + IdEmpresa
                    + " and m.IdAnio = " + IdAnio
                    + " and m.IdSede = " + IdSede
                    + " and m.IdNivel = " + IdNivel
                    + " and m.IdJornada = " + IdJornada
                    + " and m.IdCurso = " + IdCurso
                    + " and m.IdParalelo = " + IdParalelo
                    + " and mc.IdMateria = " + IdMateria
                    + " and a.Estado = 1 "
                    + " AND NOT EXISTS( "
                        + " SELECT f.IdEmpresa FROM aca_AlumnoRetiro AS F WITH (nolock) "
                        + " where mc.IdEmpresa = f.IdEmpresa and m.IdMatricula = f.IdMatricula and f.Estado = 1 "
                        + " )";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_048_SinPromedios_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            Codigo = reader["Codigo"].ToString(),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            OrdenMateria = string.IsNullOrEmpty(reader["OrdenMateria"].ToString())? (int?)null: Convert.ToInt32(reader["OrdenMateria"]),
                            OrdenMateriaGrupo = string.IsNullOrEmpty(reader["OrdenMateriaGrupo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenMateriaGrupo"]),
                            NomMateria = reader["NomMateria"].ToString(),
                            IdCalificacionCualitativaQ1 = string.IsNullOrEmpty(reader["IdCalificacionCualitativaQ1"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativaQ1"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            IdCalificacionCualitativaQ2 = string.IsNullOrEmpty(reader["IdCalificacionCualitativaQ2"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCalificacionCualitativaQ2"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioQ1"]),
                            PromedioQuimestral = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ? 
                                (string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioQ1"])) :
                                ((IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)) ?
                                   string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["PromedioQ1"]) :
                                   null )
                                )
                        });
                    }
                    reader.Close();
                }
                ListaFinal = Lista.Where(q => q.PromedioQuimestral == null).ToList();

                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
