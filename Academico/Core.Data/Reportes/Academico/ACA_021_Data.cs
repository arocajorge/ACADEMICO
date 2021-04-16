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
    public class ACA_021_Data
    {
        public List<ACA_021_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {
                int IdSedeIni = IdSede;
                int IdSedeFin = IdSede == 0 ? 9999999 : IdSede;

                int IdJornadaIni = IdJornada;
                int IdJornadaFin = IdJornada == 0 ? 9999999 : IdJornada;

                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 999999999999 : IdAlumno;

                List<ACA_021_Info> Lista = new List<ACA_021_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = " DECLARE @MostrarRetirados int = " + (MostrarRetirados == true ? 1 : 0) + ";"
                    + " SELECT m.IdEmpresa, m.IdMatricula, al.Codigo, m.IdAlumno, pa.IdPersona, pa.pe_nombreCompleto AS NombreAlumno, pa.pe_cedulaRuc AS CedulaAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede, "
                    + " a.Descripcion, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, m.Fecha, CASE WHEN r.IdRetiro IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT)  "
                    + " END AS EsRetirado, m.IdPlantilla, dbo.aca_Plantilla.NomPlantilla, dbo.aca_PlantillaTipo.NomPlantillaTipo "
                    + " FROM     dbo.aca_Matricula AS m WITH (nolock) INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a WITH (nolock) ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio INNER JOIN "
                    + " dbo.aca_Plantilla WITH (nolock) ON m.IdEmpresa = dbo.aca_Plantilla.IdEmpresa AND m.IdAnio = dbo.aca_Plantilla.IdAnio AND m.IdPlantilla = dbo.aca_Plantilla.IdPlantilla INNER JOIN "
                    + " dbo.aca_PlantillaTipo WITH (nolock) ON dbo.aca_Plantilla.IdEmpresa = dbo.aca_PlantillaTipo.IdEmpresa AND dbo.aca_Plantilla.IdTipoPlantilla = dbo.aca_PlantillaTipo.IdTipoPlantilla LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pa WITH (nolock) INNER JOIN "
                    + " dbo.aca_Alumno AS al WITH (nolock) ON pa.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn WITH (nolock) RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj WITH (nolock) ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc WITH (nolock) ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp WITH (nolock) ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AlumnoRetiro AS r WITH (nolock) ON m.IdEmpresa = r.IdEmpresa AND m.IdMatricula = r.IdMatricula AND r.Estado = 1 "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " and al.Estado = 1 "
                    + " and isnull(r.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(r.IdMatricula,0) else 0 end ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_021_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdSede = Convert.ToInt32(reader["IdSede"]),
                            IdJornada = Convert.ToInt32(reader["IdJornada"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdParalelo = Convert.ToInt32(reader["IdParalelo"]),
                            EsRetirado = string.IsNullOrEmpty(reader["EsRetirado"].ToString()) ? false : Convert.ToBoolean(reader["EsRetirado"]),
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
                            NombreAlumno = string.IsNullOrEmpty(reader["NombreAlumno"].ToString()) ? null : reader["NombreAlumno"].ToString(),
                            CedulaAlumno = string.IsNullOrEmpty(reader["CedulaAlumno"].ToString()) ? null : reader["CedulaAlumno"].ToString(),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            IdPlantilla = Convert.ToInt32(reader["IdPlantilla"]),
                            NomPlantilla = string.IsNullOrEmpty(reader["NomPlantilla"].ToString()) ? null : reader["NomPlantilla"].ToString(),
                            NomPlantillaTipo = string.IsNullOrEmpty(reader["NomPlantillaTipo"].ToString()) ? null : reader["NomPlantillaTipo"].ToString(),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy")
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.VWACA_021.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio
                    && q.IdSede == IdSede
                    && q.IdNivel == IdNivel
                    && q.IdCurso == IdCurso &&
                    IdParaleloIni <= q.IdParalelo && q.IdParalelo <= IdParaleloFin &&
                    IdAlumnoIni <= q.IdAlumno && q.IdAlumno <= IdAlumnoFin &&
                    q.EsRetirado == (MostrarRetirados == true ? q.EsRetirado : false)).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_021_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            CedulaAlumno = q.CedulaAlumno,
                            NombreAlumno = q.NombreAlumno,
                            Codigo = q.Codigo,
                            IdPlantilla = q.IdPlantilla,
                            NomPlantilla = q.NomPlantilla,
                            NomPlantillaTipo = q.NomPlantillaTipo,
                            IdAnio = q.IdAnio,
                            IdSede = q.IdSede,
                            IdNivel = q.IdNivel,
                            IdJornada = q.IdJornada,
                            IdCurso = q.IdCurso,
                            IdParalelo = q.IdParalelo,
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
                            EsRetirado = q.EsRetirado,
                            Fecha = q.Fecha,
                            IdPersona = q.IdPersona,
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy")
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
    }
}
