using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Reportes.Academico
{
    public class ACA_013_Data
    {
        public List<ACA_013_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoParcial, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {
                List<ACA_013_Info> Lista = new List<ACA_013_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "DECLARE @IdEmpresa int =" + IdEmpresa.ToString() + ", @IdAnio int = " + IdAnio.ToString() + ", @IdSede int = " + IdSede.ToString() + ", @IdNivel int = " + IdNivel.ToString() + ", @IdJornada int = " + IdJornada.ToString() + ", @IdCurso int= " + IdCurso.ToString() + ", @IdParalelo int = " + IdParalelo.ToString() + ", @IdAlumno numeric = " + IdAlumno.ToString() + " , @IdParcial int= " + IdCatalogoParcial.ToString() + " , @MostrarRetirados bit = " + (MostrarRetirados == false ? 0 : 1)
                    + " SELECT mp.IdEmpresa, mp.IdMatricula, mp.IdMateria, mp.IdCatalogoParcial, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, " 
                    + " jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, m.IdAlumno, p.pe_nombreCompleto AS NombreAlumno, a.Codigo AS CodigoAlumno, p.pe_cedulaRuc, cm.NomMateria, cm.NomMateriaArea, cm.NomMateriaGrupo, "
                    + " cm.EsObligatorio, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.OrdenMateriaArea,ct.NomCatalogoTipo, c.NomCatalogo, AN.Descripcion, mp.Calificacion1, mp.Calificacion2, mp.Calificacion3, mp.Calificacion4, mp.Remedial1, mp.Remedial2, mp.Evaluacion, "
                    + " equiv.Letra, equiv.Calificacion, mp.MotivoCalificacion, mp.AccionRemedial, "
                    + " CASE WHEN mp.IdCatalogoParcial = 28 THEN prom.CalificacionP1 WHEN mp.IdCatalogoParcial = 29 THEN prom.CalificacionP2 WHEN mp.IdCatalogoParcial = 30 THEN prom.CalificacionP3 WHEN mp.IdCatalogoParcial = 31 THEN prom.CalificacionP4 "
                    + " WHEN mp.IdCatalogoParcial = 32 THEN prom.CalificacionP5 WHEN mp.IdCatalogoParcial = 33 THEN prom.CalificacionP6 END AS PromedioParcial, EP.Codigo CodigoEquivalenciaPromedio, "
                    + " EquivM.Secuencia as SecuenciaPromedioConducta, EquivM.Letra as LetraPromedioConducta, cp.IdProfesorTutor,pp.pe_nombreCompleto as NombreTutor, pre.pe_nombreCompleto as NombreRepresentante "
                    + " FROM dbo.aca_MatriculaConducta AS mc RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS cm INNER JOIN "
                    + " dbo.tb_persona AS p INNER JOIN "
                    + " dbo.aca_Alumno AS a ON p.IdPersona = a.IdPersona INNER JOIN "
                    + " dbo.aca_Matricula AS m ON a.IdEmpresa = m.IdEmpresa AND a.IdAlumno = m.IdAlumno INNER JOIN "
                    + " dbo.aca_MatriculaCalificacionParcial AS mp ON m.IdEmpresa = mp.IdEmpresa AND m.IdMatricula = mp.IdMatricula ON cm.IdEmpresa = m.IdEmpresa AND cm.IdAnio = m.IdAnio AND cm.IdSede = m.IdSede AND cm.IdNivel = m.IdNivel AND "
                    + " cm.IdJornada = m.IdJornada AND cm.IdCurso = m.IdCurso AND cm.IdMateria = mp.IdMateria ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula LEFT OUTER JOIN "
                    + " dbo.aca_MatriculaCalificacion AS prom ON mp.IdEmpresa = prom.IdEmpresa AND mp.IdMatricula = prom.IdMatricula AND mp.IdMateria = prom.IdMateria LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS equiv ON m.IdEmpresa = equiv.IdEmpresa AND m.IdAnio = equiv.IdAnio AND mp.Conducta = equiv.Secuencia LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoEquivalenciaPromedio AS EP ON m.IdEmpresa = EP.IdEmpresa AND m.IdAnio = EP.IdAnio AND "
                    + " EP.IdEquivalenciaPromedio = CASE WHEN mp.IdCatalogoParcial = 28 THEN prom.IdEquivalenciaPromedioP1 ELSE CASE WHEN mp.IdCatalogoParcial = 29 THEN prom.IdEquivalenciaPromedioP2 ELSE CASE WHEN mp.IdCatalogoParcial = 30 "
                    + " THEN prom.IdEquivalenciaPromedioP3 ELSE CASE WHEN mp.IdCatalogoParcial = 31 THEN prom.IdEquivalenciaPromedioP4 ELSE CASE WHEN mp.IdCatalogoParcial = 32 THEN prom.IdEquivalenciaPromedioP5 ELSE CASE WHEN mp.IdCatalogoParcial= 33 THEN prom.IdEquivalenciaPromedioP6 ELSE NULL END END END END END END LEFT OUTER JOIN "
                    + " dbo.aca_Catalogo AS c ON mp.IdCatalogoParcial = c.IdCatalogo LEFT OUTER JOIN "
                    + " dbo.aca_CatalogoTipo AS ct ON c.IdCatalogoTipo = ct.IdCatalogoTipo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                    + " LEFT OUTER JOIN aca_Profesor AS pro ON cp.IdEmpresa = pro.IdEmpresa and cp.IdProfesorTutor = pro.IdProfesor "
                    + " LEFT OUTER JOIN tb_persona as pp on pp.IdPersona = pro.IdPersona "
                    + " LEFT OUTER JOIN tb_persona as pre on pre.IdPersona = m.IdPersonaR "
                    + " LEFT JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS EquivM ON m.IdEmpresa = EquivM.IdEmpresa AND m.IdAnio = EquivM.IdAnio "
                    + " AND EquivM.Secuencia = "
                    + " CASE "
                    + " WHEN mp.IdCatalogoParcial = 28 THEN mc.SecuenciaPromedioFinalP1 "
                    + " else "
                    + " case WHEN mp.IdCatalogoParcial = 29 THEN mc.SecuenciaPromedioFinalP2 else "
                        + " case WHEN mp.IdCatalogoParcial = 30 THEN mc.SecuenciaPromedioFinalP3 else "
                            + " case WHEN mp.IdCatalogoParcial = 31 THEN mc.SecuenciaPromedioFinalP4 else "
                                + " case WHEN mp.IdCatalogoParcial = 32 THEN mc.SecuenciaPromedioFinalP5 else "
                                    + " case WHEN mp.IdCatalogoParcial = 33 THEN mc.SecuenciaPromedioFinalP6 else null "
                                    + " end "
                                + " end "
                            + " end "
                        + " end "
                    + " end "
                    + " END "
                    + " LEFT JOIN "
                    + " ( "
                    + " select r.IdEmpresa, r.IdMatricula "
                    + " from aca_AlumnoRetiro as r "
                    + " where r.Estado = 1 "
                    + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                    + " where mp.IdEmpresa = @IdEmpresa "
                    + " and m.IdAnio = @IdAnio "
                    + " and mp.IdCatalogoParcial = @IdParcial ";
                    if (IdAlumno==0)
                    {
                        query += " and m.IdSede = case when @IdSede = 0 then m.IdSede else @IdSede end "
                        + " and m.IdNivel = case when @IdNivel = 0 then m.IdNivel else @IdNivel end "
                        + " and m.IdJornada = case when @IdJornada = 0 then m.IdJornada else @IdJornada end "
                        + " and m.IdCurso = case when @IdCurso = 0 then m.IdCurso else @IdCurso end "
                        + " and m.IdParalelo = case when @IdParalelo = 0 then m.IdParalelo else @IdParalelo end ";
                    }
                    else
                    {
                        query += " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end ";
                    }

                    query += " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 5000;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_013_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdMateria = Convert.ToInt32(reader["IdMateria"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            NombreAlumno = string.IsNullOrEmpty(reader["NombreAlumno"].ToString()) ? null : reader["NombreAlumno"].ToString(),
                            CodigoAlumno = string.IsNullOrEmpty(reader["CodigoAlumno"].ToString()) ? null : reader["CodigoAlumno"].ToString(),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenParalelo"]),
                            OrdenMateria = string.IsNullOrEmpty(reader["OrdenMateria"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateria"]),
                            OrdenMateriaArea = string.IsNullOrEmpty(reader["OrdenMateriaArea"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateriaArea"]),
                            OrdenMateriaGrupo = string.IsNullOrEmpty(reader["OrdenMateriaGrupo"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateriaGrupo"]),
                            NomMateriaGrupo = string.IsNullOrEmpty(reader["NomMateriaGrupo"].ToString()) ? null : reader["NomMateriaGrupo"].ToString(),
                            NomMateria = string.IsNullOrEmpty(reader["NomMateria"].ToString()) ? null : reader["NomMateria"].ToString(),
                            NomMateriaArea = string.IsNullOrEmpty(reader["NomMateriaArea"].ToString()) ? null : reader["NomMateriaArea"].ToString(),
                            Calificacion = string.IsNullOrEmpty(reader["Calificacion"].ToString()) ? (decimal?)null : Convert.ToInt32(reader["Calificacion"]),
                            EsObligatorio = string.IsNullOrEmpty(reader["EsObligatorio"].ToString()) ? false : Convert.ToBoolean(reader["OrdenCurso"]),
                            NomCatalogo = string.IsNullOrEmpty(reader["NomCatalogo"].ToString()) ? null : reader["NomCatalogo"].ToString(),
                            IdCatalogoParcial = Convert.ToInt32(reader["IdCatalogoParcial"]),
                            Calificacion1 = string.IsNullOrEmpty(reader["Calificacion1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion1"]),
                            Calificacion2 = string.IsNullOrEmpty(reader["Calificacion2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion2"]),
                            Calificacion3 = string.IsNullOrEmpty(reader["Calificacion3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion3"]),
                            Calificacion4 = string.IsNullOrEmpty(reader["Calificacion4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Calificacion4"]),
                            Remedial1 = string.IsNullOrEmpty(reader["Remedial1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial1"]),
                            Remedial2 = string.IsNullOrEmpty(reader["Remedial2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Remedial2"]),
                            Evaluacion = string.IsNullOrEmpty(reader["Evaluacion"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["Evaluacion"]),
                            Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : (reader["Letra"].ToString()),
                            PromedioParcial = string.IsNullOrEmpty(reader["PromedioParcial"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioParcial"]),
                            MotivoCalificacion = string.IsNullOrEmpty(reader["MotivoCalificacion"].ToString()) ? null : (reader["MotivoCalificacion"].ToString()),
                            AccionRemedial = string.IsNullOrEmpty(reader["AccionRemedial"].ToString()) ? null : (reader["AccionRemedial"].ToString()),
                            SecuenciaPromedioConducta = string.IsNullOrEmpty(reader["SecuenciaPromedioConducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaPromedioConducta"]),
                            LetraPromedioConducta = string.IsNullOrEmpty(reader["LetraPromedioConducta"].ToString()) ? null : (reader["LetraPromedioConducta"].ToString()),
                            NombreRepresentante = string.IsNullOrEmpty(reader["NombreRepresentante"].ToString()) ? null : (reader["NombreRepresentante"].ToString()),
                            NombreTutor = string.IsNullOrEmpty(reader["NombreTutor"].ToString()) ? null : (reader["NombreTutor"].ToString()),
                            IdProfesorTutor = string.IsNullOrEmpty(reader["IdProfesorTutor"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdProfesorTutor"]),
                            NoMostrarPromedio = string.IsNullOrEmpty(reader["PromedioParcial"].ToString()) ? 1 : 0,
                            CodigoEquivalenciaPromedio = string.IsNullOrEmpty(reader["CodigoEquivalenciaPromedio"].ToString()) ? null : (reader["CodigoEquivalenciaPromedio"].ToString()),
                            NomCatalogoTipo = string.IsNullOrEmpty(reader["NomCatalogoTipo"].ToString()) ? null : (reader["NomCatalogoTipo"].ToString()),
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout=5000;
                    var lst = Context.SPACA_013(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial, IdAlumno, MostrarRetirados).ToList();

                    int NoMostrarPromedio = 0;
                    decimal IdMatricula = 0;
                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            NombreAlumno = q.NombreAlumno,
                            CodigoAlumno = q.CodigoAlumno,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
                            CodigoParalelo = q.CodigoParalelo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomMateria = q.NomMateria,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateriaGrupo = q.OrdenMateriaGrupo,
                            OrdenMateriaArea = q.OrdenMateriaArea,
                            OrdenMateria = q.OrdenMateria,
                            Calificacion = q.Calificacion,
                            EsObligatorio = q.EsObligatorio,
                            NomCatalogo = q.NomCatalogo,
                            NomMateriaArea = q.NomMateriaArea,
                            NomMateriaGrupo = q.NomMateriaGrupo,
                            IdCatalogoParcial = q.IdCatalogoParcial,
                            Calificacion1 = q.Calificacion1,
                            Calificacion2 = q.Calificacion2,
                            Calificacion3 = q.Calificacion3,
                            Calificacion4 = q.Calificacion4,
                            Remedial1 =q.Remedial1,
                            Remedial2=q.Remedial2,
                            Evaluacion = q.Evaluacion,
                            Letra = q.Letra,
                            PromedioParcial = q.PromedioParcial,
                            MotivoCalificacion=q.MotivoCalificacion,
                            AccionRemedial = q.AccionRemedial,
                            SecuenciaPromedioConducta = q.SecuenciaPromedioConducta,
                            LetraPromedioConducta =q.LetraPromedioConducta,
                            NombreRepresentante = q.NombreRepresentante,
                            NombreTutor = q.NombreTutor,
                            IdProfesorTutor = q.IdProfesorTutor,
                            NoMostrarPromedio = q.PromedioParcial == null ? 1 : 0,
                            CodigoEquivalenciaPromedio = q.CodigoEquivalenciaPromedio,
                            NomCatalogoTipo = q.NomCatalogoTipo
                        });
                    }
                }
                */
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ACA_013_EquivalenciaPromedio_Info> get_list_equivalencia(int IdEmpresa, int IdAnio)
        {
            try
            {

                List<ACA_013_EquivalenciaPromedio_Info> Lista = new List<ACA_013_EquivalenciaPromedio_Info>();
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.VWACA_013_EquivalenciaPromedio.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_EquivalenciaPromedio_Info
                        {
                            IdEmpresa=q.IdEmpresa,
                            IdAnio=q.IdAnio,
                            Codigo=q.Codigo,
                            IdEquivalenciaPromedio=q.IdEquivalenciaPromedio,
                            Descripcion=q.Descripcion,
                            ValorMinimo=q.ValorMinimo,
                            ValorMaximo=q.ValorMaximo
                        });
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ACA_013_EquivalenciaCualitativa_Info> get_list_EquivalenciaCualitativa(int IdEmpresa, int IdAnio)
        {
            try
            {

                List<ACA_013_EquivalenciaCualitativa_Info> Lista = new List<ACA_013_EquivalenciaCualitativa_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.aca_AnioLectivoCalificacionCualitativa.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_EquivalenciaCualitativa_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Codigo = q.Codigo,
                            IdCalificacionCualitativa = q.IdCalificacionCualitativa,
                            DescripcionCorta = q.DescripcionCorta,
                            DescripcionLarga = q.DescripcionLarga
                        });
                    }
                }

                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ACA_013_EquivalenciaConducta_Info> get_list_EquivalenciaConducta(int IdEmpresa, int IdAnio)
        {
            try
            {

                List<ACA_013_EquivalenciaConducta_Info> Lista = new List<ACA_013_EquivalenciaConducta_Info>();
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst = Context.aca_AnioLectivoConductaEquivalencia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_013_EquivalenciaConducta_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            Letra = q.Letra,
                            Equivalencia = q.Equivalencia,
                            DescripcionEquivalencia = q.DescripcionEquivalencia
                        });
                    }
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
