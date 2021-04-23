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
    public class ACA_047_Data
    {
        public List<ACA_047_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdMateria, int IdCatalogoParcialTipo)
        {
            try
            {
                List<ACA_047_Info> Lista = new List<ACA_047_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, "
                    + " a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.OrdenMateria,a.IdCatalogoTipoCalificacion, "
                    + " a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, "
                    + " a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.IdProfesor, a.NombreProfesor, "
                    + " cast(max(a.CalificacionP1) as varchar) CalificacionP1,cast(max(a.CalificacionP2) as varchar) CalificacionP2,cast(max(a.CalificacionP3) as varchar) CalificacionP3, "
                    + " cast(max(a.PromedioFinalQ1) as varchar) PromedioFinalQ1, "
                    + " cast(max(a.CalificacionP4) as varchar) CalificacionP4,cast(max(a.CalificacionP5) as varchar) CalificacionP5,cast(max(a.CalificacionP6) as varchar) CalificacionP6, "
                    + " cast(max(a.PromedioFinalQ2) as varchar) PromedioFinalQ2, cast(max(a.PromedioFinal) as varchar) PromedioFinal "
                    + " from( "
                        + " SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaArea, MC.NomMateriaGrupo, MC.EsObligatorio, MC.OrdenMateria, MC.IdCatalogoTipoCalificacion, "
                        + " MC.OrdenMateriaGrupo, MC.OrdenMateriaArea, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                        + " cp.NomParalelo, cp.OrdenParalelo, a.IdProfesor, per.pe_nombreCompleto AS NombreProfesor, "
                        + " CASE WHEN IdCatalogoParcial = 28 THEN acc.Codigo END AS CalificacionP1, "
                        + " CASE WHEN IdCatalogoParcial = 29 THEN acc.Codigo END AS CalificacionP2, "
                        + " CASE WHEN IdCatalogoParcial = 30 THEN acc.Codigo END AS CalificacionP3, "
                        + " CASE WHEN IdCatalogoParcial = 31 THEN acc.Codigo END AS CalificacionP4, "
                        + " CASE WHEN IdCatalogoParcial = 32 THEN acc.Codigo END AS CalificacionP5, "
                        + " CASE WHEN IdCatalogoParcial = 33 THEN acc.Codigo END AS CalificacionP6, "
                        + " pq1.Codigo as PromedioFinalQ1, "
                        + " pq2.Codigo as PromedioFinalQ2, "
                        + " pf.Codigo as PromedioFinal "
                    + " FROM dbo.aca_MatriculaCalificacionCualitativa AS a WITH (nolock) INNER JOIN "
                    + " dbo.aca_Matricula AS m WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN "
                    + " dbo.aca_Alumno AS alu WITH (nolock) ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p WITH (nolock) ON alu.IdPersona = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoCalificacionCualitativa AS acc WITH (nolock) ON acc.IdEmpresa = a.IdEmpresa AND acc.IdAnio = m.IdAnio AND acc.IdCalificacionCualitativa = a.IdCalificacionCualitativa LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS AN WITH (nolock) ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) INNER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND "
                    + " jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Materia AS MC ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON "
                    + " cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND "
                    + " m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND "
                    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                    + " LEFT OUTER JOIN aca_Profesor AS pro WITH (nolock) ON cp.IdEmpresa = pro.IdEmpresa and a.IdProfesor = pro.IdProfesor "
                    + " LEFT OUTER JOIN tb_persona as per WITH (nolock) on per.IdPersona = pro.IdPersona "
                    + " left join aca_MatriculaCalificacionCualitativaPromedio pr WITH (nolock) on pr.IdEmpresa = a.IdEmpresa and pr.IdMatricula = a.IdMatricula and pr.IdMateria = a.IdMateria "
                    + " left join aca_AnioLectivoCalificacionCualitativa pq1 WITH (nolock) on pq1.IdEmpresa = pr.IdEmpresa and pq1.IdAnio = m.IdAnio and pq1.IdCalificacionCualitativa = pr.IdCalificacionCualitativaQ1 "
                    + " left join aca_AnioLectivoCalificacionCualitativa pq2 WITH (nolock) on pq2.IdEmpresa = pr.IdEmpresa and pq2.IdAnio = m.IdAnio and pq2.IdCalificacionCualitativa = pr.IdCalificacionCualitativaQ2 "
                    + " left join aca_AnioLectivoCalificacionCualitativa pf WITH (nolock) on pf.IdEmpresa = pr.IdEmpresa and pf.IdAnio = m.IdAnio and pf.IdCalificacionCualitativa = pr.IdCalificacionCualitativaFinal "
                    + " where mc.IdEmpresa = " + IdEmpresa
                    + " and m.IdAnio = " + IdAnio
                    + " and m.IdSede = " + IdSede
                    + " and m.IdJornada = " + IdJornada
                    + " and m.IdNivel = " + IdNivel
                    + " and m.IdCurso = " + IdCurso
                    + " and m.IdParalelo = " + IdParalelo
                    + " and a.IdMateria = " + IdMateria
                    + " and not exists( "
                        + " select f.IdEmpresa from aca_AlumnoRetiro as f "
                        + " where f.IdEmpresa = mc.IdEmpresa and f.IdMatricula = m.IdMatricula and f.Estado = 1 "
                    + " ) "
                    + " ) a "
                    + " group by "
                    + " a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, "
                    + " a.NomMateria, a.NomMateriaArea, a.NomMateriaGrupo, a.EsObligatorio, a.OrdenMateriaArea, a.OrdenMateriaGrupo, a.OrdenMateria, a.IdCatalogoTipoCalificacion, "
                    + " a.Codigo, a.NombreAlumno, a.Descripcion,a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, "
                    + " a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, a.IdProfesor, a.NombreProfesor";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_047_Info
                        { 
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            NombreAlumno = reader["NombreAlumno"].ToString(),
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
                            NombreProfesor = reader["NombreProfesor"].ToString(),
                            NomMateria = reader["NomMateria"].ToString(),
                            CalificacionP1 = reader["CalificacionP1"].ToString(),
                            CalificacionP2 = reader["CalificacionP2"].ToString(),
                            CalificacionP3 = reader["CalificacionP3"].ToString(),
                            CalificacionP4 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? reader["CalificacionP4"].ToString() : null),
                            CalificacionP5 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? reader["CalificacionP5"].ToString() : null),
                            CalificacionP6 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? reader["CalificacionP6"].ToString() : null),
                            PromedioFinalQ1 = reader["PromedioFinalQ1"].ToString(),
                            PromedioFinalQ2 = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? reader["PromedioFinalQ2"].ToString() : null),
                            PromedioFinal = (IdCatalogoParcialTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ? reader["PromedioFinal"].ToString() : null),
                            Codigo = reader["Codigo"].ToString()
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
