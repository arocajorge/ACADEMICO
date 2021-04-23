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
    public class ACA_040_Data
    {
        public List<ACA_040_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, int IdCatalogoParcialTipo, int IdCatalogoParcial, bool MostrarRetirados)
        {
            try
            {
                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                List<ACA_040_Info> Lista = new List<ACA_040_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    string query = "";

                    #region Query
                    if (IdCatalogoParcial==0)
                    {
                    query = "SELECT a.IdEmpresa, a.IdAlumno, e.IdMatricula, b.pe_nombreCompleto AS NombreAlumno,  b.pe_cedulaRuc, e.IdAnio, e.IdSede, e.IdNivel, "
                    + " e.IdJornada, e.IdCurso, e.IdParalelo, f.Descripcion, sn.NomSede, sn.NomNivel,  nj.NomJornada,  jc.NomCurso, cp.NomParalelo, nj.OrdenJornada, "
                    + " sn.OrdenNivel, jc.OrdenCurso, cp.OrdenParalelo, "
                     + " case when " + IdCatalogoParcialTipo + " = 6 then equivq1.Equivalencia else "
                        + " case when " + IdCatalogoParcialTipo + "  = 7 then equivq2.Equivalencia else "
                            + " case when " + IdCatalogoParcialTipo + "  = 0 then equivpf.Equivalencia else null "
                            + " end "
                        + " end "
                    + " end Equivalencia, "
                    + " case when " + IdCatalogoParcialTipo + "  = 6 then equivq1.Letra else "
                        + " case when " + IdCatalogoParcialTipo + "  = 7 then equivq2.Letra else "
                            + " case when " + IdCatalogoParcialTipo + "  = 0 then equivpf.Letra else null "
                            + " end "
                        + " end "
                    + " end Letra, "
                    + " cat.NomCatalogoTipo,  ca.NomCatalogo "
                    + " FROM dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) "
                    + " RIGHT OUTER JOIN  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio  AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel "
                    + " RIGHT OUTER JOIN dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede  AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada "
                    + " RIGHT OUTER JOIN dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) RIGHT OUTER JOIN  dbo.aca_AnioLectivo AS f INNER JOIN dbo.aca_Matricula AS e ON f.IdEmpresa = e.IdEmpresa AND f.IdAnio = e.IdAnio "
                    + " INNER JOIN  dbo.aca_Alumno AS a WITH (nolock) "
                    + " INNER JOIN dbo.tb_persona AS b WITH (nolock) ON a.IdPersona = b.IdPersona  ON e.IdEmpresa = a.IdEmpresa  AND e.IdAlumno = a.IdAlumno "
                    + " ON cp.IdEmpresa = e.IdEmpresa AND cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede  AND cp.IdNivel = e.IdNivel  AND cp.IdJornada = e.IdJornada AND cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo "
                    + " ON jc.IdEmpresa = cp.IdEmpresa  AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel  AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso "
                    + " LEFT OUTER JOIN(select r.IdEmpresa, r.IdMatricula  from aca_AlumnoRetiro as r WITH (nolock) where r.Estado = 1  ) as ret  on e.IdEmpresa = ret.IdEmpresa  and e.IdMatricula = ret.IdMatricula "
                    + " LEFT OUTER JOIN aca_MatriculaConducta mco WITH (nolock) on mco.IdEmpresa = e.IdEmpresa and mco.IdMatricula = e.IdMatricula "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq1 WITH (nolock) on equivq1.IdEmpresa = mco.IdEmpresa and equivq1.IdAnio = e.IdAnio and equivq1.Secuencia = mco.SecuenciaPromedioFinalQ1 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivq2 WITH (nolock) on equivq2.IdEmpresa = mco.IdEmpresa and equivq2.IdAnio = e.IdAnio and equivq2.Secuencia = mco.SecuenciaPromedioFinalQ2 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equivpf WITH (nolock) on equivpf.IdEmpresa = mco.IdEmpresa and equivpf.IdAnio = e.IdAnio and equivpf.Secuencia = mco.SecuenciaPromedioFinal "
                    + " LEFT OUTER JOIN aca_Catalogo ca WITH (nolock) on ca.IdCatalogo = " + IdCatalogoParcial
                    + " LEFT OUTER JOIN aca_CatalogoTipo cat WITH (nolock) on cat.IdCatalogoTipo = " + IdCatalogoParcialTipo
                      + " WHERE "
                      + " e.IdEmpresa = " + IdEmpresa.ToString()
                      + " and e.IdAnio = " + IdAnio.ToString()
                      + " and e.IdSede = " + IdSede.ToString()
                      + " and e.IdJornada = " + IdJornada.ToString()
                      + " and e.IdNivel = " + IdNivel.ToString()
                      + " and e.IdCurso = " + IdCurso.ToString()
                      + " and e.IdParalelo = " + IdParalelo.ToString()
                      + " and e.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString()
                      + " and(f.Estado = 1) AND(a.Estado = 1) "
                      + " and isnull(ret.IdMatricula,0) = case when " + (MostrarRetirados == false ? 0 : 1) + " = 1 then isnull(ret.IdMatricula,0) else 0 end";
                    }
                    else
                    {
                    query = " SELECT a.IdEmpresa, a.IdAlumno, e.IdMatricula, b.pe_nombreCompleto AS NombreAlumno,  b.pe_cedulaRuc, e.IdAnio, e.IdSede, e.IdNivel, "
                    + " e.IdJornada, e.IdCurso, e.IdParalelo, f.Descripcion, sn.NomSede, sn.NomNivel,  nj.NomJornada,  jc.NomCurso, cp.NomParalelo, nj.OrdenJornada, "
                    + " sn.OrdenNivel, jc.OrdenCurso, cp.OrdenParalelo, "
                    + " case when " + IdCatalogoParcial + " = 28 then equiv1.Equivalencia else  "
                        + " case when " + IdCatalogoParcial + " = 29 then equiv2.Equivalencia else "
                            + " case when " + IdCatalogoParcial + " = 30 then equiv3.Equivalencia else "
                                + " case when " + IdCatalogoParcial + " = 31 then equiv4.Equivalencia else "
                                    + " case when " + IdCatalogoParcial + " = 32 then equiv5.Equivalencia else "
                                        + " case when " + IdCatalogoParcial + " = 33 then equiv6.Equivalencia else null end "
                                        + " end "
                                + " end "
                            + " end "
                        + " end "
                    + " end Equivalencia, "
                    + " case when " + IdCatalogoParcial + " = 28 then equiv1.Letra else "
                        + " case when " + IdCatalogoParcial + " = 29 then equiv2.Letra else "
                            + " case when " + IdCatalogoParcial + " = 30 then equiv3.Letra else "
                                + " case when " + IdCatalogoParcial + " = 31 then equiv4.Letra else "
                                    + " case when " + IdCatalogoParcial + " = 32 then equiv5.Letra else "
                                        + " case when " + IdCatalogoParcial + " = 33 then equiv6.Letra else null end "
                                        + " end "
                                + " end "
                            + " end "
                        + " end "
                    + " end Letra, "
                    + " cat.NomCatalogoTipo, "
                    + " ca.NomCatalogo "
                    + " FROM dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) RIGHT OUTER JOIN  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) "
                    + " ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio  AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel "
                    + " RIGHT OUTER JOIN dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede "
                    + " AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada  RIGHT OUTER JOIN dbo.aca_AnioLectivo_Curso_Paralelo AS cp "
                    + " RIGHT OUTER JOIN  dbo.aca_AnioLectivo AS f WITH (nolock) INNER JOIN dbo.aca_Matricula AS e ON f.IdEmpresa = e.IdEmpresa AND f.IdAnio = e.IdAnio "
                    + " INNER JOIN  dbo.aca_Alumno AS a WITH (nolock) INNER JOIN dbo.tb_persona AS b ON a.IdPersona = b.IdPersona  ON e.IdEmpresa = a.IdEmpresa "
                    + " AND e.IdAlumno = a.IdAlumno  ON cp.IdEmpresa = e.IdEmpresa AND cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede  AND cp.IdNivel = e.IdNivel "
                    + " AND cp.IdJornada = e.IdJornada AND cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo  ON jc.IdEmpresa = cp.IdEmpresa "
                    + " AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel  AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso "
                    + " LEFT OUTER JOIN(select r.IdEmpresa, r.IdMatricula  from aca_AlumnoRetiro as r WITH (nolock) where r.Estado = 1  ) as ret  on e.IdEmpresa = ret.IdEmpresa "
                    + " and e.IdMatricula = ret.IdMatricula "
                    + " LEFT OUTER JOIN aca_MatriculaConducta mco WITH (nolock) on mco.IdEmpresa = e.IdEmpresa and mco.IdMatricula = e.IdMatricula "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv1 WITH (nolock) on equiv1.IdEmpresa = mco.IdEmpresa and equiv1.IdAnio = e.IdAnio and equiv1.Secuencia = mco.SecuenciaPromedioFinalP1 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv2 WITH (nolock) on equiv2.IdEmpresa = mco.IdEmpresa and equiv2.IdAnio = e.IdAnio and equiv2.Secuencia = mco.SecuenciaPromedioFinalP2 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv3 WITH (nolock) on equiv3.IdEmpresa = mco.IdEmpresa and equiv3.IdAnio = e.IdAnio and equiv3.Secuencia = mco.SecuenciaPromedioFinalP3 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv4 WITH (nolock) on equiv4.IdEmpresa = mco.IdEmpresa and equiv4.IdAnio = e.IdAnio and equiv4.Secuencia = mco.SecuenciaPromedioFinalP4 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv5 WITH (nolock) on equiv5.IdEmpresa = mco.IdEmpresa and equiv5.IdAnio = e.IdAnio and equiv5.Secuencia = mco.SecuenciaPromedioFinalP5 "
                    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv6 WITH (nolock) on equiv6.IdEmpresa = mco.IdEmpresa and equiv6.IdAnio = e.IdAnio and equiv6.Secuencia = mco.SecuenciaPromedioFinalP6 "
                    + " LEFT OUTER JOIN aca_Catalogo ca WITH (nolock) on ca.IdCatalogo = " + IdCatalogoParcial
                    + " LEFT OUTER JOIN aca_CatalogoTipo cat WITH (nolock) on cat.IdCatalogoTipo = " + IdCatalogoParcialTipo
                      + " WHERE "
                      + " e.IdEmpresa = " + IdEmpresa.ToString()
                      + " and e.IdAnio = " + IdAnio.ToString()
                      + " and e.IdSede = " + IdSede.ToString()
                      + " and e.IdJornada = " + IdJornada.ToString()
                      + " and e.IdNivel = " + IdNivel.ToString()
                      + " and e.IdCurso = " + IdCurso.ToString()
                      + " and e.IdParalelo = " + IdParalelo.ToString()
                      + " and e.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString()
                      + " and(f.Estado = 1) AND(a.Estado = 1) "
                      + " and isnull(ret.IdMatricula,0) = case when " + (MostrarRetirados == false ? 0 : 1) + " = 1 then isnull(ret.IdMatricula,0) else 0 end";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_040_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenNivel = Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"]),
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
                            Letra = reader["Letra"].ToString(),
                            Equivalencia = reader["Equivalencia"].ToString(),
                            NomCatalogo = reader["NomCatalogo"].ToString(),
                            NomCatalogoTipo = reader["NomCatalogoTipo"].ToString(),
                            DescripcionParcial = (IdCatalogoParcial==0 ? reader["NomCatalogoTipo"].ToString() : reader["NomCatalogo"].ToString() + " - " + reader["NomCatalogoTipo"].ToString())
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
