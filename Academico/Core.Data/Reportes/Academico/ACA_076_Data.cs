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
    public class ACA_076_Data
    {
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        public List<ACA_076_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, int IdCatalogoTipo,  bool MostrarRetirados)
        {
            try
            {
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                var info_anio = odata_anio.getInfo(IdEmpresa, IdAnio);
                List<ACA_076_Info> Lista = new List<ACA_076_Info>();
                List<ACA_076_Info> ListaFinal = new List<ACA_076_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "DECLARE @MostrarRetirados int = " + (MostrarRetirados == true ? 1 : 0) + ";"
                    + " SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, CASE WHEN r.IdRetiro IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END AS EsRetirado, sn.NomSede, sn.NomNivel, "
                    + " sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, mc.IdCampoAccion, mc.IdTematica, t.NombreCampoAccion, t.NombreTematica, p.pe_nombreCompleto,  p.pe_cedulaRuc, s.NombreRector, s.NombreSecretaria,an.Descripcion,  "
                    + " s.TelefonoRector, s.CelularRector, s.CorreoRector, mc.IdProfesor, per.pe_nombreCompleto NombreProfesor,per.pe_telfono_Contacto TelefonoProfesor, pro.Telefonos CelularProfesor, pro.Correo CorreoProfesor, "
                    + " mc.CalificacionP1, mc.CalificacionP2, mc.PromedioQ1, mc.CalificacionP3, mc.CalificacionP4, mc.PromedioQ2, mc.PromedioFinal"
                    + " FROM dbo.aca_Matricula AS m RIGHT OUTER JOIN "
                    + " dbo.aca_MatriculaCalificacionParticipacion AS mc ON m.IdEmpresa = mc.IdEmpresa AND m.IdAlumno = mc.IdAlumno AND m.IdMatricula = mc.IdMatricula "
                    + " LEFT OUTER JOIN dbo.aca_AnioLectivo_Tematica t on t.IdEmpresa= mc.IdEmpresa and t.IdAnio = m.IdAnio and t.IdCampoAccion=mc.IdCampoAccion and t.IdTematica=mc.IdTematica "
                    + " LEFT OUTER JOIN dbo.aca_Profesor pro on pro.IdEmpresa = mc.IdEmpresa and pro.IdProfesor = mc.IdProfesor "
                    + " LEFT OUTER JOIN dbo.tb_persona AS per ON per.IdPersona = pro.IdPersona LEFT OUTER JOIN"
                    + " dbo.aca_Alumno AS al ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON al.IdPersona = p.IdPersona LEFT OUTER JOIN "
                    + " dbo.aca_AlumnoRetiro AS r ON m.IdEmpresa = r.IdEmpresa AND m.IdMatricula = r.IdMatricula AND r.Estado = 1 INNER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS an ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio "
                    + " LEFT OUTER JOIN aca_Sede s on s.IdEmpresa=m.IdEmpresa and s.IdSede = m.IdSede "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " and isnull(r.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(r.IdMatricula,0) else 0 end ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_076_Info
                        {
                            Num = 1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenParalelo"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            NombreProfesor = string.IsNullOrEmpty(reader["NombreProfesor"].ToString()) ? null : reader["NombreProfesor"].ToString(),
                            TelefonoProfesor = string.IsNullOrEmpty(reader["TelefonoProfesor"].ToString()) ? null : reader["TelefonoProfesor"].ToString(),
                            CelularProfesor = string.IsNullOrEmpty(reader["CelularProfesor"].ToString()) ? null : reader["CelularProfesor"].ToString(),
                            CelularRector = string.IsNullOrEmpty(reader["CelularRector"].ToString()) ? null : reader["CelularRector"].ToString(),
                            CorreoProfesor = string.IsNullOrEmpty(reader["CorreoProfesor"].ToString()) ? null : reader["CorreoProfesor"].ToString(),
                            CorreoRector = string.IsNullOrEmpty(reader["CorreoRector"].ToString()) ? null : reader["CorreoRector"].ToString(),
                            NombreRector = string.IsNullOrEmpty(reader["NombreRector"].ToString()) ? null : reader["NombreRector"].ToString(),
                            TelefonoRector = string.IsNullOrEmpty(reader["TelefonoRector"].ToString()) ? null : reader["TelefonoRector"].ToString(),
                            NombreCampoAccion = string.IsNullOrEmpty(reader["NombreCampoAccion"].ToString()) ? null : reader["NombreCampoAccion"].ToString(),
                            NombreTematica = string.IsNullOrEmpty(reader["NombreTematica"].ToString()) ? null : reader["NombreTematica"].ToString(),
                            CalificacionP1 = string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"]),
                            CalificacionP2 = string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"]),
                            PromedioQ1 = string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ1"]),
                            CalificacionP3 = string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"]),
                            CalificacionP4 = string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"]),
                            PromedioQ2 = string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ2"]),
                            PromedioFinal = string.IsNullOrEmpty(reader["PromedioFinal"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioFinal"]),
                            Bitacora = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["CalificacionP1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP1"])
                                                : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["CalificacionP3"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP3"])
                                                    : (decimal?)null)),
                            Desarrollo = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["CalificacionP2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP2"])
                                                : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["CalificacionP4"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["CalificacionP4"])
                                                    : (decimal?)null)),
                            Total = (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1) ?
                                                string.IsNullOrEmpty(reader["PromedioQ1"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ1"])
                                                : (IdCatalogoTipo == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2) ?
                                                    string.IsNullOrEmpty(reader["PromedioQ2"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["PromedioQ2"])
                                                    : (decimal?)null))
                        });
                    }
                    reader.Close();
                }
                Lista.ForEach(q => q.Criterio = (q.Total == null ? null : (q.Total >= Convert.ToDecimal(info_anio.PromedioMinimoPromocion) ? "APROBADO" : "REPROBADO")));
                return Lista;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
