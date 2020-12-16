using Core.Data.Academico;
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
    public class ACA_034_Data
    {
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        aca_AnioLectivoEquivalenciaPromedio_Data odata_equivalencia = new aca_AnioLectivoEquivalenciaPromedio_Data();
        public List<ACA_034_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {

                List<ACA_034_Info> Lista = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaFinal = new List<ACA_034_Info>();

                List<ACA_034_Info> ListaInicial = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaObligatorias = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaOptativas = new List<ACA_034_Info>();
                List<ACA_034_Info> ListaOptativasIndividuales = new List<ACA_034_Info>();
                
                //using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                //{
                //    connection.Open();

                //    #region Query
                //    string query = "DECLARE @IdEmpresa int =1, @IdAnio int = " + IdAnio.ToString() + ", @IdSede int = " + IdSede.ToString() + ", @IdNivel int = " + IdNivel.ToString() + ", @IdJornada int = " + IdJornada.ToString() + ", @IdCurso int= " + IdCurso.ToString() + ", @IdParalelo int = " + IdParalelo.ToString() + ", @IdAlumno numeric = " + IdAlumno.ToString()+ " , @MostrarRetirados bit = " + (MostrarRetirados==false ? 0 : 1)
                //    + " /*EVALUCACION COMPORTAMIENTO*/ "
                //    + " SELECT mco.IdEmpresa, m.IdAnio, m.IdSede,m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo,m.IdAlumno, mco.IdMatricula, "
                //    + " 0 IdMateria, null NombreMateria, null NombreGrupo, "
                //    + " 0 OrdenMateria, 0 OrdenGrupo,0 PromediarGrupo, NULL IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, "
                //    + " cast(equiv.Letra as varchar) as Calificacion, CAST(equiv.Calificacion AS numeric(18, 2)) CalificacionNumerica,'EVALUACION DEL COMPORTAMIENTO' Columna, 1 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaConducta AS mco "
                //    + " INNER JOIN dbo.aca_Matricula AS m ON mco.IdEmpresa = m.IdEmpresa AND mco.IdMatricula = m.IdMatricula "
                //    + " inner join dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno "
                //    + " INNER JOIN dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona "
                //    + " LEFT OUTER JOIN aca_AnioLectivoConductaEquivalencia equiv on equiv.IdEmpresa = mco.IdEmpresa and equiv.IdAnio = m.IdAnio and equiv.Secuencia = mco.SecuenciaPromedioFinal "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mco.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " /*PROMEDIO MATERIAS CUALITATIVAS*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT "
                //    + " PROY.IdEmpresa, PROY.IdAnio, PROY.IdSede, PROY.IdNivel, PROY.IdJornada, PROY.IdCurso, PROY.IdParalelo, PROY.IdAlumno, PROY.IdMatricula, 0 IdMateria, "
                //    + " null NomMateria, PROY.NomMateriaGrupo, PROY.OrdenMateria, PROY.OrdenMateriaGrupo, PROY.PromediarGrupo, PROY.IdCatalogoTipoCalificacion, "
                //    + " PROY.Codigo, PROY.NombreAlumno, PROY.Descripcion, PROY.NomSede, PROY.NomNivel, PROY.OrdenNivel, PROY.NomJornada, "
                //    + " PROY.OrdenJornada, PROY.NomCurso, PROY.OrdenCurso, PROY.CodigoParalelo, PROY.NomParalelo, PROY.OrdenParalelo, "
                //    + " LPROM.Codigo Calificacion, PROY.CalificacionNumerica, 'EVALUACION DE PROYECTOS ESCOLARES' Columna, 2 OrdenColumna "
                //    + " FROM( "
                //    + " SELECT "
                //    + " PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, null IdMateria, "
                //    + " null NomMateria, null NomMateriaGrupo, PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion, "
                //    + " PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion, PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, "
                //    + " PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo, "
                //    + " dbo.BankersRounding(AVG(PROM.CalificacionNumerica), 2) CalificacionNumerica "
                //    + " FROM( "
                //    + " select a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, "
                //    + " a.NomMateria, null NomMateriaGrupo, 1 OrdenMateria, 1 OrdenMateriaGrupo, 0 PromediarGrupo, a.IdCatalogoTipoCalificacion, "
                //    + " a.Codigo, a.NombreAlumno, a.Descripcion, a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, "
                //    + " a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo, "
                //    + " cast(max(a.CalificacionQUIM) as varchar)  Calificacion, CAST(max(a.CalificacionNumericaQUIM) AS numeric(18, 2)) CalificacionNumerica "
                //    + " from( "
                //        + " SELECT a.IdEmpresa, a.IdMatricula, a.IdMateria, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, MC.NomMateria, MC.NomMateriaGrupo, MC.OrdenMateria, "
                //        + " MC.OrdenMateriaGrupo, mc.PromediarGrupo, mc.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //        + " cp.NomParalelo, cp.OrdenParalelo, epQ.Codigo CalificacionQUIM, epQ.Calificacion CalificacionNumericaQUIM "
                //    + " FROM  dbo.aca_MatriculaCalificacionCualitativa AS a "
                //    + " INNER JOIN aca_MatriculaCalificacionCualitativaPromedio b on a.IdEmpresa = b.IdEmpresa and a.IdMatricula = b.IdMatricula and a.IdMateria = b.IdMateria "
                //    + " INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON m.IdEmpresa = a.IdEmpresa AND m.IdMatricula = a.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN  "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona  "
                //    + " LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS epQ ON m.IdAnio = epQ.IdAnio AND b.IdEmpresa = epQ.IdEmpresa AND b.IdCalificacionCualitativaQ2 = epQ.IdCalificacionCualitativa "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn INNER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel ON jc.IdJornada = nj.IdJornada AND jc.IdEmpresa = nj.IdEmpresa AND "
                //    + " jc.IdAnio = nj.IdAnio AND jc.IdSede = nj.IdSede AND jc.IdNivel = nj.IdNivel LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia AS MC ON jc.IdEmpresa = MC.IdEmpresa AND jc.IdAnio = MC.IdAnio AND jc.IdSede = MC.IdSede AND jc.IdNivel = MC.IdNivel AND jc.IdJornada = MC.IdJornada AND jc.IdCurso = MC.IdCurso ON "
                //    + " cp.IdEmpresa = jc.IdEmpresa AND cp.IdAnio = jc.IdAnio AND cp.IdSede = jc.IdSede AND cp.IdNivel = jc.IdNivel AND cp.IdJornada = jc.IdJornada ON m.IdEmpresa = MC.IdEmpresa AND m.IdAnio = MC.IdAnio AND m.IdSede = MC.IdSede AND "
                //    + " m.IdNivel = MC.IdNivel AND m.IdJornada = MC.IdJornada AND m.IdCurso = MC.IdCurso AND a.IdMateria = MC.IdMateria AND m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel AND "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT JOIN "
                //    + " ( "
                //    + " select r.IdEmpresa, r.IdMatricula "
                //    + " from aca_AlumnoRetiro as r "
                //    + " where r.Estado = 1 "
                //    + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " ) a "
                //    + " group by "
                //    + " a.IdEmpresa, a.IdAnio, a.IdSede, a.IdNivel, a.IdJornada, a.IdCurso, a.IdParalelo, a.IdAlumno, a.IdMatricula, a.IdMateria, "
                //    + " a.NomMateria, a.NomMateriaGrupo, a.OrdenMateria, a.OrdenMateriaGrupo, a.IdCatalogoTipoCalificacion, "
                //    + " a.Codigo, a.NombreAlumno, a.Descripcion, a.NomSede, a.NomNivel, a.OrdenNivel, a.NomJornada, "
                //    + " a.OrdenJornada, a.NomCurso, a.OrdenCurso, a.CodigoParalelo, a.NomParalelo, a.OrdenParalelo "
                //    + " )PROM "
                //    + " group by "
                //    + " PROM.IdEmpresa, PROM.IdAnio, PROM.IdSede, PROM.IdNivel, PROM.IdJornada, PROM.IdCurso, PROM.IdParalelo, PROM.IdAlumno, PROM.IdMatricula, PROM.IdMateria, "
                //    + " PROM.NomMateria, PROM.NomMateriaGrupo, PROM.OrdenMateria, PROM.OrdenMateriaGrupo, PROM.PromediarGrupo, PROM.IdCatalogoTipoCalificacion, "
                //    + " PROM.Codigo, PROM.NombreAlumno, PROM.Descripcion, PROM.NomSede, PROM.NomNivel, PROM.OrdenNivel, PROM.NomJornada, "
                //    + " PROM.OrdenJornada, PROM.NomCurso, PROM.OrdenCurso, PROM.CodigoParalelo, PROM.NomParalelo, PROM.OrdenParalelo "
                //    + " )PROY "
                //    + " LEFT OUTER JOIN dbo.aca_AnioLectivoCalificacionCualitativa AS LPROM ON PROY.IdAnio = LPROM.IdAnio AND PROY.IdEmpresa = LPROM.IdEmpresa "
                //    + " AND PROY.CalificacionNumerica = LPROM.Calificacion "
                //    + " ) "
                //    + " /*MATERIAS QUE NO SE PROMEDIAN*/ "
                //    + " /*QUIMESTRE I*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, "
                //    + " CAST(mc.PromedioFinalQ1 as varchar) Calificacion, CAST(mc.PromedioFinalQ1 AS numeric(18, 2)) AS CalificacionNumerica, 'I QUIMESTRE' as Columna, 1 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + ") "
                //    + " /*QUIMESTRE II*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, "
                //    + " CAST(mc.PromedioFinalQ2 as varchar) Calificacion, CAST(mc.PromedioFinalQ2 AS numeric(18, 2)) AS CalificacionNumerica, 'II QUIMESTRE' as Columna, 2 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*PROMEDIO DE LOS 2 QUIMESTRES*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, "
                //    + " CAST(mc.PromedioQuimestres as varchar) Calificacion, CAST(mc.PromedioQuimestres AS numeric(18, 2)) AS CalificacionNumerica, 'PROMEDIO' as Columna, 3 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*MEJORAMIENTO*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenMejoramiento as varchar) Calificacion, "
                //    + " CAST(mc.ExamenMejoramiento as numeric(18, 2)) CalificacionNumerica,'MEJORAMIENTO' as Columna, 4 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*PROMEDIO MEJORAMIENTO*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinal as varchar) Calificacion, "
                //    + " CAST(mc.PromedioFinal as numeric(18, 2)) CalificacionNumerica,'PROMEDIO MEJORAMIENTO' as Columna, 5 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*SUPLETORIO*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenSupletorio as varchar) Calificacion, "
                //    + " CAST(mc.ExamenSupletorio as numeric(18, 2)) CalificacionNumerica,'SUPLETORIO' as Columna, 6 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*REMEDIAL*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenRemedial as varchar) Calificacion, "
                //    + " CAST(mc.ExamenRemedial as numeric(18, 2)) CalificacionNumerica,'REMEDIAL' as Columna, 7 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*GRACIA*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenGracia as varchar) Calificacion, "
                //    + " CAST(mc.ExamenGracia as numeric(18, 2)) CalificacionNumerica,'GRACIA' as Columna, 8 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*PROMEDIO FINAL DE MATERIAS QUE NO SE PROMEDIAN*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, "
                //    + " CM.NomMateria, CM.NomMateriaGrupo, CM.OrdenMateria, CM.OrdenMateriaGrupo, CM.PromediarGrupo, CM.IdCatalogoTipoCalificacion, "
                //    + " alu.Codigo, p.pe_nombreCompleto, AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, "
                //    + " nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, cp.NomParalelo, cp.OrdenParalelo, "
                //    + " CAST(mc.PromedioFinal as varchar) Calificacion, CAST(mc.PromedioFinal AS numeric(18, 2)) AS CalificacionNumerica, 'PROMEDIO FINAL' as Columna, 9 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 0 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*MATERIAS QUE SE PROMEDIAN*/ "
                //    + " /*QUIMESTRE I*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinalQ1 as varchar) Calificacion, "
                //    + " PromedioFinalQ1 as CalificacionNumerica, "
                //    + " 'I QUIMESTRE' as Columna, 1 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*QUIMESTRE II*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinalQ2 as varchar) Calificacion, "
                //    + " PromedioFinalQ2 as CalificacionNumerica, "
                //    + " 'II QUIMESTRE' as Columna, 2 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + ") "
                //    + " /*PROMEDIOS DE LOS 2 QUIMESTRES*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioQuimestres as varchar) Calificacion, "
                //    + " PromedioQuimestres as CalificacionNumerica, "
                //    + " 'PROMEDIO' as Columna, 2 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*MEJORAMIENTO*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenMejoramiento as varchar) Calificacion, "
                //    + " mc.ExamenMejoramiento as CalificacionNumerica, "
                //    + " 'MEJORAMIENTO' as Columna, 2 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*PROMEDIOS FINAL MEJORAMIENTO*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinal as varchar) Calificacion, "
                //    + " PromedioFinal as CalificacionNumerica, "
                //    + " 'PROMEDIO MEJORAMIENTO' as Columna, 2 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*SUPLETORIO*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenSupletorio as varchar) Calificacion, CAST(mc.ExamenSupletorio as numeric(18, 2)) CalificacionNumerica, "
                //    + " 'SUPLETORIO' as Columna, 3 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*REMEDIAL*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenRemedial as varchar) Calificacion, CAST(mc.ExamenRemedial as numeric(18, 2)) CalificacionNumerica, "
                //    + " 'REMEDIAL' as Columna, 3 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*GRACIA*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.ExamenGracia as varchar) Calificacion, CAST(mc.ExamenGracia as numeric(18, 2)) CalificacionNumerica, "
                //    + " 'GRACIA' as Columna, 3 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*PROMEDIO X MATERIA OPTATIVA*/ "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, mc.IdMateria, cm.NomMateria, "
                //    + " cm.NomMateriaGrupo, cm.OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, CAST(mc.PromedioFinal as varchar) Calificacion, "
                //    + " mc.PromedioFinal as CalificacionNumerica, "
                //    + " 'PROMEDIO FINAL' as Columna, 6 OrdenColumna "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia CM ON m.IdEmpresa = CM.IdEmpresa AND m.IdAnio = CM.IdAnio AND m.IdSede = CM.IdSede AND "
                //    + " m.IdNivel = CM.IdNivel AND m.IdJornada = CM.IdJornada AND m.IdCurso = CM.IdCurso AND "
                //    + " mc.IdMateria = CM.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT OUTER JOIN "
                //        + " (SELECT IdEmpresa, IdMatricula "
                //        + " FROM      dbo.aca_AlumnoRetiro AS r "
                //        + " WHERE(Estado = 1)) AS ret ON m.IdEmpresa = ret.IdEmpresa AND m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " ) "
                //    + " /*MATERIA PROMEDIADA*/ "
                //    + " /*I QUIMESTRE*/ "
                //    + " UNION ALL "
                //    + " ("
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria, "
                //    + " 'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'I QUIMESTRE' Columna, 1 OrdenColuma "
                //    + " FROM dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND "
                //    + " m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND "
                //    + " mc.IdMateria = cm.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //    + " LEFT JOIN "
                //    + " ( "
                //        + " select r.IdEmpresa, r.IdMatricula "
                //        + " from aca_AlumnoRetiro as r "
                //        + " where r.Estado = 1 "
                //    + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " group by "
                //    + " mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo "
                //    + " ) "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria, "
                //    + " 'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'II QUIMESTRE' Columna, 2 OrdenColuma "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND "
                //    + " m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND "
                //    + " mc.IdMateria = cm.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //        + " LEFT JOIN "
                //        + " ("
                //        + " select r.IdEmpresa, r.IdMatricula "
                //        + " from aca_AlumnoRetiro as r "
                //        + " where r.Estado = 1 "
                //        + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " group by "
                //    + " mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo "
                //    + " ) "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria, "
                //    + " 'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'PROMEDIO' Columna, 3 OrdenColuma "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND "
                //    + " m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND "
                //    + " mc.IdMateria = cm.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //        + " LEFT JOIN "
                //        + " ("
                //        + " select r.IdEmpresa, r.IdMatricula "
                //        + " from aca_AlumnoRetiro as r "
                //        + " where r.Estado = 1 "
                //        + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " group by "
                //    + " mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo "
                //    + " ) "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria, "
                //    + "'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'SUPLETORIO' Columna, 4 OrdenColuma "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND "
                //    + " m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND "
                //    + " mc.IdMateria = cm.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //        + " LEFT JOIN "
                //        + " ("
                //        + " select r.IdEmpresa, r.IdMatricula "
                //        + " from aca_AlumnoRetiro as r "
                //        + " where r.Estado = 1 "
                //        + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " group by "
                //    + " mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo "
                //    + " ) "
                //    + " UNION ALL "
                //    + " ( "
                //    + " SELECT mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, null IdMateria, "
                //    + " 'OPTATIVA' NomMateria, cm.NomMateriaGrupo, 999 OrdenMateria, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto NombreAlumno, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo, null Calificacion, null CalificacionNumerica, 'PROMEDIO FINAL' Columna, 5 OrdenColuma "
                //    + " FROM     dbo.aca_MatriculaCalificacion AS mc INNER JOIN "
                //    + " dbo.aca_Matricula AS m ON mc.IdEmpresa = m.IdEmpresa AND mc.IdMatricula = m.IdMatricula INNER JOIN "
                //    + " dbo.aca_Alumno AS alu ON m.IdEmpresa = alu.IdEmpresa AND m.IdAlumno = alu.IdAlumno INNER JOIN "
                //    + " dbo.tb_persona AS p ON alu.IdPersona = p.IdPersona INNER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Materia AS cm ON m.IdEmpresa = cm.IdEmpresa AND m.IdAnio = cm.IdAnio AND m.IdSede = cm.IdSede AND "
                //    + " m.IdNivel = cm.IdNivel AND m.IdJornada = cm.IdJornada AND m.IdCurso = cm.IdCurso AND "
                //    + " mc.IdMateria = cm.IdMateria "
                //    + " LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo AS AN ON m.IdEmpresa = AN.IdEmpresa AND m.IdAnio = AN.IdAnio LEFT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                //    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                //    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                //        + " LEFT JOIN "
                //        + " ("
                //        + " select r.IdEmpresa, r.IdMatricula "
                //        + " from aca_AlumnoRetiro as r "
                //        + " where r.Estado = 1 "
                //        + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                //    + " where mc.IdEmpresa = @IdEmpresa "
                //    + " and m.IdAnio = @IdAnio "
                //    + " and m.IdSede = @IdSede "
                //    + " and m.IdJornada = @IdJornada "
                //    + " and m.IdNivel = @IdNivel "
                //    + " and m.IdCurso = @IdCurso "
                //    + " and m.IdParalelo = @IdParalelo "
                //    + " and m.IdAlumno = case when @IdAlumno = 0 then m.IdAlumno else @IdAlumno end "
                //    + " and isnull(ret.IdMatricula, 0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula, 0) else 0 end "
                //    + " and cm.PromediarGrupo = 1 "
                //    + " and cm.IdCatalogoTipoCalificacion = 40 "
                //    + " group by "
                //    + " mc.IdEmpresa, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, mc.IdMatricula, cm.NomMateriaGrupo, cm.OrdenMateriaGrupo, cm.PromediarGrupo, cm.IdCatalogoTipoCalificacion, alu.Codigo, p.pe_nombreCompleto, "
                //    + " AN.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.CodigoParalelo, "
                //    + " cp.NomParalelo, cp.OrdenParalelo "
                //    + " ) ";
                //    #endregion

                //    SqlCommand command = new SqlCommand(query, connection);
                //    command.CommandTimeout = 0;
                //    SqlDataReader reader = command.ExecuteReader();
                //    while (reader.Read())
                //    {
                //        Lista.Add(new ACA_034_Info
                //        {
                //            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                //            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                //            IdMateria = string.IsNullOrEmpty(reader["IdMateria"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdMateria"]),
                //            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                //            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                //            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                //            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                //            IdSede = Convert.ToInt32(reader["IdSede"]),
                //            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                //            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                //            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                //            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                //            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                //            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                //            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                //            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                //            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                //            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                //            CodigoParalelo = string.IsNullOrEmpty(reader["CodigoParalelo"].ToString()) ? null : reader["CodigoParalelo"].ToString(),
                //            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenNivel"]),
                //            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenJornada"]),
                //            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenCurso"]),
                //            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenParalelo"]),
                //            OrdenMateria = string.IsNullOrEmpty(reader["OrdenMateria"].ToString()) ? 0 : Convert.ToInt32(reader["OrdenMateria"]),
                //            Calificacion = string.IsNullOrEmpty(reader["Calificacion"].ToString()) ? null : reader["Calificacion"].ToString(),
                //            CalificacionNumerica = string.IsNullOrEmpty(reader["CalificacionNumerica"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionNumerica"]),
                //            IdCatalogoTipoCalificacion = string.IsNullOrEmpty(reader["IdCatalogoTipoCalificacion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdCatalogoTipoCalificacion"]),
                //            Columna = string.IsNullOrEmpty(reader["Columna"].ToString()) ? null : reader["Columna"].ToString(),
                //            NombreGrupo = string.IsNullOrEmpty(reader["NombreGrupo"].ToString()) ? null : reader["NombreGrupo"].ToString(),
                //            NombreMateria = string.IsNullOrEmpty(reader["NombreMateria"].ToString()) ? null : reader["NombreMateria"].ToString(),
                //            OrdenColumna = Convert.ToInt32(reader["OrdenColumna"]),
                //            OrdenGrupo = string.IsNullOrEmpty(reader["OrdenGrupo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenGrupo"]),
                //            PromediarGrupo = string.IsNullOrEmpty(reader["PromediarGrupo"].ToString()) ? 0 : Convert.ToInt32(reader["PromediarGrupo"]),
                //        });
                //    }
                //    reader.Close();
                //}
                
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_034(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_034_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdMateria = q.IdMateria,
                            IdAlumno = q.IdAlumno,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
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
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            OrdenNivel = q.OrdenNivel,
                            OrdenJornada = q.OrdenJornada,
                            OrdenCurso = q.OrdenCurso,
                            OrdenParalelo = q.OrdenParalelo,
                            OrdenMateria = q.OrdenMateria,
                            Calificacion = q.Calificacion,
                            CalificacionNumerica = q.CalificacionNumerica,
                            IdCatalogoTipoCalificacion = q.IdCatalogoTipoCalificacion,
                            Columna = q.Columna,
                            NombreGrupo = q.NombreGrupo,
                            NombreMateria = q.NombreMateria,
                            OrdenColumna = q.OrdenColumna,
                            OrdenGrupo = q.OrdenGrupo,
                            PromediarGrupo = q.PromediarGrupo
                        });
                    }
                }
                */
                ListaInicial = Lista.Where(q => q.IdMateria == 0 ).ToList();
                ListaFinal.AddRange(ListaInicial);

                ListaObligatorias = Lista.Where(q => q.PromediarGrupo == 0 && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativasIndividuales = Lista.Where(q => q.PromediarGrupo == 1 && q.IdMateria != null && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();
                ListaOptativas= Lista.Where(q => q.PromediarGrupo == 1 && q.IdMateria == null && q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();

                var lstPromediosNull_Obligatorias = ListaObligatorias.Where(q => (q.Columna == "I QUIMESTRE" || q.Columna == "II QUIMESTRE") && q.Calificacion == null).GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdMateria
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdMateria = q.Key.IdMateria
                }).ToList();

                ListaFinal.AddRange(ListaObligatorias.Where(q=>q.Columna!= "PROMEDIO FINAL"));

                var lstLeftJoin_PromediosObligatorias =
                  (from a in ListaObligatorias
                   join b in lstPromediosNull_Obligatorias on new { a.IdMatricula, a.IdMateria } equals new { b.IdMatricula, b.IdMateria } into PromNulos
                   from pn in PromNulos.DefaultIfEmpty()
                   where a.Columna == "PROMEDIO FINAL"
                   select new ACA_034_Info
                   {
                       IdEmpresa = a.IdEmpresa,
                       IdMatricula = a.IdMatricula,
                       IdMateria = a.IdMateria,
                       IdAlumno = a.IdAlumno,
                       pe_nombreCompleto = a.pe_nombreCompleto,
                       Codigo = a.Codigo,
                       IdAnio = a.IdAnio,
                       IdSede = a.IdSede,
                       IdNivel = a.IdNivel,
                       IdJornada = a.IdJornada,
                       IdCurso = a.IdCurso,
                       IdParalelo = a.IdParalelo,
                       CodigoParalelo = a.CodigoParalelo,
                       Descripcion = a.Descripcion,
                       NomSede = a.NomSede,
                       NomNivel = a.NomNivel,
                       NomJornada = a.NomJornada,
                       NomCurso = a.NomCurso,
                       NomParalelo = a.NomParalelo,
                       OrdenNivel = a.OrdenNivel,
                       OrdenJornada = a.OrdenJornada,
                       OrdenCurso = a.OrdenCurso,
                       OrdenParalelo = a.OrdenParalelo,
                       OrdenMateria = a.OrdenMateria,
                       NombreTutor = a.NombreTutor,
                       NombreInspector = a.NombreInspector,
                       Columna = a.Columna,
                       OrdenColumna = a.OrdenColumna,
                       PromediarGrupo = a.PromediarGrupo ?? 0,
                       IdCatalogoTipoCalificacion = a.IdCatalogoTipoCalificacion,
                       Calificacion = (pn == null) ? a.Calificacion : null,
                       CalificacionNumerica = (pn != null) ? a.CalificacionNumerica : null,
                       NombreGrupo=a.NombreGrupo,
                       NombreMateria=a.NombreMateria,
                       OrdenGrupo=a.OrdenGrupo
                   }
                  ).ToList();

                ListaFinal.AddRange(lstLeftJoin_PromediosObligatorias);

                //ListaFinal.AddRange(ListaOptativasIndividuales);
                var Lista_Validar_PromediosOptativa = new List<ACA_034_Info>();

                var IQuim_ParaValidar = new List<ACA_034_Info>();
                IQuim_ParaValidar = ListaOptativasIndividuales.Where(q => q.Columna == "I QUIMESTRE").ToList();
                IQuim_ParaValidar.ForEach(q=>q.OptativasNulas = (q.Calificacion==null ? 1 : 0));

                var lstPromedio_IQuimestre = IQuim_ParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var IQuim_Optativa = ListaOptativas.Where(q => q.Columna == "I QUIMESTRE").ToList();

                foreach (var item in lstPromedio_IQuimestre)
                {
                    var Promedio = (decimal?)null;
                    if (item.OptativasNulas==0)
                    {
                        Promedio = Math.Round(Convert.ToDecimal(item.PromedioCalculado),2,MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Promedio = (decimal?)null;
                    }

                    IQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = Convert.ToString(Promedio);
                    IQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica = Promedio;
                }

                
                ListaFinal.AddRange(IQuim_Optativa);
                Lista_Validar_PromediosOptativa.AddRange(IQuim_Optativa);

                var IIQuim_ParaValidar = new List<ACA_034_Info>();
                IQuim_ParaValidar = ListaOptativasIndividuales.Where(q => q.Columna == "II QUIMESTRE").ToList();
                IQuim_ParaValidar.ForEach(q => q.OptativasNulas = (q.Calificacion == null ? 1 : 0));

                var lstPromedio_IIQuimestre = IQuim_ParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var IIQuim_Optativa = ListaOptativas.Where(q => q.Columna == "II QUIMESTRE").ToList();

                foreach (var item in lstPromedio_IIQuimestre)
                {
                    var Promedio = (decimal?)null;
                    if (item.OptativasNulas == 0)
                    {
                        Promedio = Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Promedio = (decimal?)null;
                    }

                    IIQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = Convert.ToString(Promedio);
                    IIQuim_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica = Promedio;
                }


                ListaFinal.AddRange(IIQuim_Optativa);
                Lista_Validar_PromediosOptativa.AddRange(IIQuim_Optativa);

                var Lista_CalificacionesQuimestrales = new List<ACA_034_Info>();
                Lista_CalificacionesQuimestrales.AddRange(IQuim_Optativa);
                Lista_CalificacionesQuimestrales.AddRange(IIQuim_Optativa);

                var PromedioQuim_ParaValidar = new List<ACA_034_Info>();
                PromedioQuim_ParaValidar = Lista_CalificacionesQuimestrales;
                PromedioQuim_ParaValidar.ForEach(q => { q.Calificacion = (string.IsNullOrEmpty(q.Calificacion) ? null : q.Calificacion); q.OptativasNulas = (string.IsNullOrEmpty(q.Calificacion) ? 1 : 0); });

                var lstPromedio_Promedio = PromedioQuim_ParaValidar.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    PromedioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                var PromedioQuim = ListaOptativas.Where(q => q.Columna == "PROMEDIO").ToList();

                foreach (var item in lstPromedio_Promedio)
                {
                    var Promedio = (decimal?)null;
                    if (item.OptativasNulas == 0)
                    {
                        Promedio = Math.Round(Convert.ToDecimal(item.PromedioCalculado), 2, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        Promedio = (decimal?)null;
                    }

                    PromedioQuim.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = Convert.ToString(Promedio);
                    PromedioQuim.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica= Promedio;
                }

                ListaFinal.AddRange(PromedioQuim);
                Lista_Validar_PromediosOptativa.AddRange(PromedioQuim);

                var Lista_Validar_Promedios = Lista_Validar_PromediosOptativa.Where(q => q.Columna != "PROMEDIO").ToList();
                Lista_Validar_Promedios.ForEach(q => q.OptativasNulas = (q.Calificacion == null ? 1 : 0));
                var info_anio = odata_anio.getInfo(IdEmpresa, IdAnio);
                if (info_anio != null)
                {
                    Lista_Validar_Promedios.ForEach(q => q.MuestraSupletorio = (string.IsNullOrEmpty(q.Calificacion) ? 0 : (Convert.ToDecimal(q.CalificacionNumerica) < Convert.ToDecimal(info_anio.PromedioMinimoPromocion) ? 1 : 0)));
                }

                var Lista_Agrupada_Validar_PromediosOptativa = Lista_Validar_Promedios.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    MuestraSupletorio = q.Sum(g => g.MuestraSupletorio),
                 }).ToList();

                var Supletorio = new List<ACA_034_Info>();
                Supletorio = ListaOptativasIndividuales.Where(q => q.Columna == "SUPLETORIO").ToList();
                Supletorio.ForEach(q => q.OptativasNulas = (q.Calificacion == null ? 1 : 0));

                var lsT_Supletorio = Supletorio.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    SupletorioCalculado = q.Max(g => g.Calificacion) == null ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                //para agregar a la lista final
                var Lista_Supletorio_Optativa = ListaOptativas.Where(q => q.Columna == "SUPLETORIO").ToList();
                foreach (var item in Lista_Supletorio_Optativa)
                {
                    var PromedioSupletorio = (decimal?)null;

                    var Muestra_Supletorio = 0;
                    Muestra_Supletorio = Lista_Agrupada_Validar_PromediosOptativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().MuestraSupletorio;

                    if (Muestra_Supletorio > 0)
                    {
                        var OptativasNulas = 0;
                        OptativasNulas = lsT_Supletorio.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().OptativasNulas;
                        PromedioSupletorio = (OptativasNulas == 0 ? lsT_Supletorio.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().SupletorioCalculado : (decimal?)null);
                    }

                    item.CalificacionNumerica = PromedioSupletorio;
                    item.Calificacion = Convert.ToString(PromedioSupletorio) == "" ? null : Convert.ToString(PromedioSupletorio);
                }

                foreach (var item in Lista_Supletorio_Optativa)
                {
                    Lista_Supletorio_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().Calificacion = item.Calificacion;
                    Lista_Supletorio_Optativa.Where(q => q.IdEmpresa == item.IdEmpresa && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica = item.CalificacionNumerica;
                }

                ListaFinal.AddRange(Lista_Supletorio_Optativa);
                Lista_Validar_PromediosOptativa.AddRange(Lista_Supletorio_Optativa);

                //para agregar a la lista final
                var Lista_PromedioFinal_Optativa = ListaOptativas.Where(q => q.Columna == "PROMEDIO FINAL").ToList();

                foreach (var item in Lista_PromedioFinal_Optativa)
                {
                    //var PromedioFinal = (decimal?)null;
                    var SupletorioOptativa = Lista_Validar_PromediosOptativa.Where(q => q.Columna == "SUPLETORIO" && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica;
                    var PromedioFinal = Lista_Validar_PromediosOptativa.Where(q => q.Columna == "PROMEDIO" && q.IdMatricula == item.IdMatricula).FirstOrDefault().CalificacionNumerica;
                    if (SupletorioOptativa==null)
                    {
                        item.Calificacion = Convert.ToString(PromedioFinal);
                        item.CalificacionNumerica = PromedioFinal;
                    }
                    else
                    {
                        item.Calificacion = (info_anio == null ? null : Convert.ToDecimal(info_anio.PromedioMinimoPromocion).ToString("n2"));
                        item.CalificacionNumerica = (info_anio == null ? (decimal?)null : Convert.ToDecimal(info_anio.PromedioMinimoPromocion));
                    }
                }

                ListaFinal.AddRange(Lista_PromedioFinal_Optativa);

                var ListaPromedioGeneral = new List<ACA_034_Info>();
                var ListaPromedioGeneral_Agregar = new List<ACA_034_Info>();
                ListaPromedioGeneral = ListaFinal.Where(q => q.Columna == "PROMEDIO FINAL").ToList();
                ListaPromedioGeneral.ForEach(q => q.OptativasNulas = (string.IsNullOrEmpty(q.Calificacion) ? 1 : 0));
                var ListaPromedioGeneral_Validar = ListaPromedioGeneral.GroupBy(q => new
                {
                    q.IdEmpresa,
                    q.IdMatricula,
                    q.IdAnio,
                    q.IdSede,
                    q.IdJornada,
                    q.IdNivel,
                    q.IdCurso,
                    q.IdParalelo,
                    q.OrdenJornada,
                    q.OrdenNivel,
                    q.OrdenCurso,
                    q.OrdenParalelo,
                    q.NomSede,
                    q.Descripcion,
                    q.NomJornada,
                    q.NomCurso,
                    q.NomNivel,
                    q.NomParalelo,
                    q.IdAlumno,
                    q.pe_nombreCompleto
                }).Select(q => new ACA_034_Info
                {
                    IdEmpresa = q.Key.IdEmpresa,
                    IdMatricula = q.Key.IdMatricula,
                    IdAnio = q.Key.IdAnio,
                    IdSede = q.Key.IdSede,
                    IdJornada = q.Key.IdJornada,
                    IdNivel = q.Key.IdNivel,
                    IdCurso = q.Key.IdCurso,
                    IdParalelo = q.Key.IdParalelo,
                    OrdenJornada = q.Key.OrdenJornada,
                    OrdenNivel = q.Key.OrdenNivel,
                    OrdenCurso = q.Key.OrdenCurso,
                    OrdenParalelo = q.Key.OrdenParalelo,
                    NomSede = q.Key.NomSede,
                    Descripcion = q.Key.Descripcion,
                    NomJornada = q.Key.NomJornada,
                    NomCurso = q.Key.NomCurso,
                    NomNivel = q.Key.NomNivel,
                    NomParalelo = q.Key.NomParalelo,
                    IdAlumno = q.Key.IdAlumno,
                    pe_nombreCompleto = q.Key.pe_nombreCompleto,
                    OptativasNulas = q.Sum(g => g.OptativasNulas),
                    SumaGeneral = q.Max(g => (g.Calificacion == null || g.Calificacion=="")) ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)),
                    PromedioFinalCalculado = q.Max(g => (g.Calificacion == null || g.Calificacion == "")) ? (decimal?)null : q.Sum(g => Convert.ToDecimal(g.Calificacion)) / q.Count(g => !string.IsNullOrEmpty(g.Calificacion))
                }).ToList();

                foreach (var item in ListaPromedioGeneral_Validar)
                {
                    var suma = new ACA_034_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdAnio = item.IdAnio,
                        IdSede = item.IdSede,
                        IdJornada = item.IdJornada,
                        IdNivel = item.IdNivel,
                        IdCurso = item.IdCurso,
                        IdParalelo = item.IdParalelo,
                        OrdenJornada = item.OrdenJornada,
                        OrdenNivel = item.OrdenNivel,
                        OrdenCurso = item.OrdenCurso,
                        OrdenParalelo = item.OrdenParalelo,
                        NomSede = item.NomSede,
                        Descripcion = item.Descripcion,
                        NomJornada = item.NomJornada,
                        NomCurso = item.NomCurso,
                        NomNivel = item.NomNivel,
                        NomParalelo = item.NomParalelo,
                        IdAlumno = item.IdAlumno,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        Columna = "SUMA TOTAL",
                        NombreGrupo = null,
                        OrdenMateria = 999,
                        OrdenGrupo = 999,
                        Calificacion = (item.OptativasNulas > 0 ? null : Convert.ToString(item.SumaGeneral)),
                        CalificacionNumerica = (item.OptativasNulas > 0 ? (decimal?)null : item.SumaGeneral),
                    };
                    ListaPromedioGeneral_Agregar.Add(suma);

                    var promedio = new ACA_034_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdAnio = item.IdAnio,
                        IdSede = item.IdSede,
                        IdJornada = item.IdJornada,
                        IdNivel = item.IdNivel,
                        IdCurso = item.IdCurso,
                        IdParalelo = item.IdParalelo,
                        OrdenJornada = item.OrdenJornada,
                        OrdenNivel = item.OrdenNivel,
                        OrdenCurso = item.OrdenCurso,
                        OrdenParalelo = item.OrdenParalelo,
                        NomSede = item.NomSede,
                        Descripcion = item.Descripcion,
                        NomJornada = item.NomJornada,
                        NomCurso = item.NomCurso,
                        NomNivel = item.NomNivel,
                        NomParalelo = item.NomParalelo,
                        IdAlumno = item.IdAlumno,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        Columna = "PROMEDIO GENERAL",
                        NombreGrupo = null,
                        OrdenMateria = 999,
                        OrdenGrupo = 999,
                        Calificacion = (item.OptativasNulas > 0 ? null : Convert.ToString(Math.Round(Convert.ToDecimal(item.PromedioFinalCalculado),2,MidpointRounding.AwayFromZero))),
                        CalificacionNumerica = (item.OptativasNulas > 0 ? (decimal?)null : Math.Round(Convert.ToDecimal(item.PromedioFinalCalculado), 2, MidpointRounding.AwayFromZero)),
                    };
                    ListaPromedioGeneral_Agregar.Add(promedio);

                    var PromedioGeneral = (promedio.CalificacionNumerica == null ? (decimal?)null : promedio.CalificacionNumerica);
                    var info_equivalencia = odata_equivalencia.getInfo_x_Promedio(IdEmpresa,IdAnio, PromedioGeneral);
                    var equivalencia = new ACA_034_Info
                    {
                        IdEmpresa = item.IdEmpresa,
                        IdMatricula = item.IdMatricula,
                        IdAnio = item.IdAnio,
                        IdSede = item.IdSede,
                        IdJornada = item.IdJornada,
                        IdNivel = item.IdNivel,
                        IdCurso = item.IdCurso,
                        IdParalelo = item.IdParalelo,
                        OrdenJornada = item.OrdenJornada,
                        OrdenNivel = item.OrdenNivel,
                        OrdenCurso = item.OrdenCurso,
                        OrdenParalelo = item.OrdenParalelo,
                        NomSede = item.NomSede,
                        Descripcion = item.Descripcion,
                        NomJornada = item.NomJornada,
                        NomCurso = item.NomCurso,
                        NomNivel = item.NomNivel,
                        NomParalelo = item.NomParalelo,
                        IdAlumno = item.IdAlumno,
                        pe_nombreCompleto = item.pe_nombreCompleto,
                        Columna = "EQUIVALENCIA",
                        NombreGrupo = null,
                        OrdenMateria = 999,
                        OrdenGrupo = 999,
                        Calificacion = (info_equivalencia == null ? null :info_equivalencia.Codigo),
                        CalificacionNumerica = (decimal?)null,
                    };
                    ListaPromedioGeneral_Agregar.Add(equivalencia);
                }

                ListaFinal.AddRange(ListaPromedioGeneral_Agregar);
                return ListaFinal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
