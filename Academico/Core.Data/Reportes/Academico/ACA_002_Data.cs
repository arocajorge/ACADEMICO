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
    public class ACA_002_Data
    {
        public List<ACA_002_Info> get_list(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                List<ACA_002_Info> Lista;
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Lista = Context.VWACA_002.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdAnio == IdAnio).Select(q => new ACA_002_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        IdAnio = q.IdAnio,
                        IdAlumno = q.IdAlumno,
                        CodigoAlumno = q.CodigoAlumno,
                        NombreAlumno = q.NombreAlumno,
                        Descripcion = q.Descripcion,
                        NomSede = q.NomSede,
                        NomNivel = q.NomNivel,
                        NomJornada = q.NomJornada,
                        NomCurso = q.NomCurso,
                        NomParalelo = q.NomParalelo,
                        NombreRep = q.NombreRep,
                        CedulaRep = q.CedulaRep,
                        NomPlantilla = q.NomPlantilla,
                        IdUsuarioCreacion=q.IdUsuarioCreacion,
                        FechaCreacion=q.FechaCreacion

                    }).ToList();
                }
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ACA_002_Info get_info(int IdEmpresa, decimal IdAlumno, int IdAnio)
        {
            try
            {
                ACA_002_Info Info = new ACA_002_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT m.IdEmpresa, m.IdMatricula, m.IdAlumno, m.IdAnio, an.EnCurso, ISNULL(al.Codigo, '') AS CodigoAlumno, ISNULL(p.pe_nombreCompleto, '') AS NombreAlumno, rep.pe_cedulaRuc AS CedulaRep, ISNULL(rep.pe_nombreCompleto, '') AS NombreRep, "
                    + " ISNULL(pl.NomPlantilla, '') AS NomPlantilla, pt.NomPlantillaTipo, val.NomRubro, val.NumeroCuotas, val.Total, m.IdUsuarioCreacion, m.FechaCreacion, an.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, "
                    + " m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo "
                    + " FROM     dbo.aca_Matricula AS m INNER JOIN "
                    + " dbo.aca_Alumno AS al ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON al.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.tb_persona AS rep ON m.IdPersonaR = rep.IdPersona INNER JOIN "
                    + " dbo.aca_AnioLectivo AS an ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel "
                    + " LEFT OUTER JOIN "
                    + " dbo.aca_Plantilla AS pl ON m.IdEmpresa = pl.IdEmpresa AND m.IdAnio = pl.IdAnio AND m.IdPlantilla = pl.IdPlantilla "
                    + " LEFT OUTER JOIN aca_PlantillaTipo pt on pl.IdEmpresa = pt.IdEmpresa and pl.IdTipoPlantilla = pt.IdTipoPlantilla "
                    + " LEFT OUTER JOIN "
                    + " ("
                    + " select "
                    + " pr.IdEmpresa, pr.IdPlantilla, pr.IdAnio, r.NomRubro, ar.NumeroCuotas, pr.Total "
                    + " from aca_Plantilla_Rubro pr "
                    + " LEFT OUTER JOIN aca_Rubro r ON pr.IdEmpresa = r.IdEmpresa AND pr.IdRubro = r.IdRubro "
                    + " LEFT OUTER JOIN aca_AnioLectivo_Rubro ar on pr.IdEmpresa = ar.IdEmpresa and pr.IdRubro = ar.IdRubro and ar.IdAnio = pr.IdAnio "
                    + " ) val "
                    + " on pl.IdEmpresa = val.IdEmpresa and pl.IdPlantilla = val.IdPlantilla and val.IdAnio = m.IdAnio and val.NumeroCuotas > 1 "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString() + "and m.IdAlumno = " + IdAlumno.ToString() + "and m.IdAnio = " + IdAnio.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Info = new ACA_002_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            CodigoAlumno = string.IsNullOrEmpty(reader["CodigoAlumno"].ToString()) ? null : reader["CodigoAlumno"].ToString(),
                            NombreAlumno = string.IsNullOrEmpty(reader["NombreAlumno"].ToString()) ? null : reader["NombreAlumno"].ToString(),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            NombreRep = string.IsNullOrEmpty(reader["NombreRep"].ToString()) ? null : reader["NombreRep"].ToString(),
                            CedulaRep = string.IsNullOrEmpty(reader["CedulaRep"].ToString()) ? null : reader["CedulaRep"].ToString(),
                            NomPlantilla = string.IsNullOrEmpty(reader["NomPlantilla"].ToString()) ? null : reader["NomPlantilla"].ToString(),
                            NomPlantillaTipo = string.IsNullOrEmpty(reader["NomPlantillaTipo"].ToString()) ? null : reader["NomPlantillaTipo"].ToString(),
                            Total = string.IsNullOrEmpty(reader["Total"].ToString()) ? (Decimal?)null : Convert.ToDecimal(reader["Total"]),
                            IdUsuarioCreacion = string.IsNullOrEmpty(reader["IdUsuarioCreacion"].ToString()) ? null : reader["IdUsuarioCreacion"].ToString(),
                            FechaCreacion = string.IsNullOrEmpty(reader["FechaCreacion"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["FechaCreacion"])
                        };
                    }
                }
                /*
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Info = Context.VWACA_002.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdAnio == IdAnio).Select(q => new ACA_002_Info
                    {
                        IdEmpresa = q.IdEmpresa,
                        IdMatricula = q.IdMatricula,
                        IdAnio = q.IdAnio,
                        IdAlumno = q.IdAlumno,
                        CodigoAlumno = q.CodigoAlumno,
                        NombreAlumno = q.NombreAlumno,
                        Descripcion = q.Descripcion,
                        NomSede = q.NomSede,
                        NomNivel = q.NomNivel,
                        NomJornada = q.NomJornada,
                        NomCurso = q.NomCurso,
                        NomParalelo = q.NomParalelo,
                        NombreRep = q.NombreRep,
                        CedulaRep = q.CedulaRep,
                        NomPlantilla = q.NomPlantilla,
                        IdUsuarioCreacion = q.IdUsuarioCreacion,
                        FechaCreacion = q.FechaCreacion
                    }).FirstOrDefault();
                }
                */
                return Info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
