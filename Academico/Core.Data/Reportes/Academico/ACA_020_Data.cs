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
    public class ACA_020_Data
    {
        public List<ACA_020_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {
                int IdNivelIni = IdNivel;
                int IdNivelFin = IdNivel == 0 ? 9999999 : IdNivel;

                int IdCursoIni = IdCurso;
                int IdCursoFin = IdCurso == 0 ? 9999999 : IdCurso;

                int IdParaleloIni = IdParalelo;
                int IdParaleloFin = IdParalelo == 0 ? 9999999 : IdParalelo;

                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                List<ACA_020_Info> Lista = new List<ACA_020_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = " DECLARE @MostrarRetirados int = " + (MostrarRetirados == true ? 1 : 0) + ";" 
                    + " SELECT m.IdEmpresa, m.IdMatricula, al.Codigo, m.IdAlumno, pa.IdPersona, pa.pe_nombreCompleto AS NombreAlumno, pa.pe_cedulaRuc AS CedulaAlumno, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, sn.NomSede,  "
                    + " a.Descripcion, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, m.IdPersonaR, prep.pe_cedulaRuc AS CedulaLegal,  "
                    + " prep.pe_nombreCompleto AS NombreLegal, m.Fecha, m.IdPersonaF, dbo.tb_persona.pe_cedulaRuc AS CedulaFactura, dbo.tb_persona.pe_nombreCompleto AS NombreFactura,  "
                    + " case when ret.IdMatricula is not null then cast(1 as bit) else cast(0 as bit) end as EsRetirado, "
                    + " case when ret.IdMatricula is not null THEN 'RETIRADO' else 'ACTIVO' end as EsRetiradoString, pension.Total "
                    + " FROM     dbo.aca_Matricula AS m INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio "
                    + " LEFT OUTER JOIN "
                    + " dbo.aca_Plantilla AS pl ON m.IdEmpresa = pl.IdEmpresa AND m.IdAnio = pl.IdAnio AND m.IdPlantilla = pl.IdPlantilla "
                    + " LEFT OUTER JOIN aca_PlantillaTipo pt on pl.IdEmpresa = pt.IdEmpresa and pl.IdTipoPlantilla = pt.IdTipoPlantilla "
                    + " LEFT OUTER JOIN "
                    + " ( "
                    + " select "
                    + " pr.IdEmpresa, pr.IdPlantilla, pr.IdAnio, r.NomRubro, ar.NumeroCuotas, pr.Total "
                    + " from aca_Plantilla_Rubro pr "
                    + " LEFT OUTER JOIN aca_Rubro r ON pr.IdEmpresa = r.IdEmpresa AND pr.IdRubro = r.IdRubro "
                    + " LEFT OUTER JOIN aca_AnioLectivo_Rubro ar on pr.IdEmpresa = ar.IdEmpresa and pr.IdRubro = ar.IdRubro and ar.IdAnio = pr.IdAnio "
                    + " ) pension "
                    + " on pl.IdEmpresa = pension.IdEmpresa and pl.IdPlantilla = pension.IdPlantilla and pension.IdAnio = m.IdAnio and pension.NumeroCuotas > 1 "
                    + " LEFT OUTER JOIN "
                    + " dbo.tb_persona ON m.IdPersonaF = dbo.tb_persona.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_persona AS prep ON m.IdPersonaR = prep.IdPersona LEFT OUTER JOIN "
                    + " dbo.tb_persona AS pa INNER JOIN "
                    + " dbo.aca_Alumno AS al ON pa.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada RIGHT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso ON "
                    + " m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND m.IdParalelo = cp.IdParalelo "
                    + " left join "
                    + " ( "
                        + " select r.IdEmpresa, r.IdMatricula "
                        + " from aca_AlumnoRetiro as r "
                        + " where r.Estado = 1 "
                    + " ) as ret on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                    + " WHERE m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel between " + IdNivelIni.ToString() + " and " + IdNivelFin.ToString()
                    + " and m.IdCurso between " + IdCursoIni.ToString() + " and " + IdCursoFin.ToString()
                    + " and m.IdParalelo between " + IdParaleloIni.ToString() + " and " + IdParaleloFin.ToString()
                    + " and m.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString()
                    + " and isnull(ret.IdMatricula,0) = case when @MostrarRetirados = 1 then isnull(ret.IdMatricula,0) else 0 end "
                    + " and al.Estado = 1 ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_020_Info
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
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            IdPersona = string.IsNullOrEmpty(reader["IdPersona"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdPersona"]),
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
                            Total = string.IsNullOrEmpty(reader["Total"].ToString()) ? 0 : Convert.ToDecimal(reader["Total"]),
                            TotalPension = string.IsNullOrEmpty(reader["Total"].ToString()) ? null :reader["Total"].ToString(),
                            IdPersonaR = string.IsNullOrEmpty(reader["IdPersonaR"].ToString()) ? 0 : Convert.ToInt32(reader["IdPersonaR"]),
                            CedulaFactura = string.IsNullOrEmpty(reader["CedulaFactura"].ToString()) ? null : reader["CedulaFactura"].ToString(),
                            NombreFactura = string.IsNullOrEmpty(reader["NombreFactura"].ToString()) ? null : reader["NombreFactura"].ToString(),
                            CedulaLegal = string.IsNullOrEmpty(reader["CedulaLegal"].ToString()) ? null : reader["CedulaLegal"].ToString(),
                            NombreLegal = string.IsNullOrEmpty(reader["NombreLegal"].ToString()) ? null : reader["NombreLegal"].ToString(),
                            IdPersonaF = string.IsNullOrEmpty(reader["IdPersonaF"].ToString()) ? 0 : Convert.ToInt32(reader["IdPersonaF"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            EsRetiradoString = string.IsNullOrEmpty(reader["EsRetiradoString"].ToString()) ? null : reader["EsRetiradoString"].ToString()

                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesReportes Context = new EntitiesReportes())
                {
                    Context.Database.CommandTimeout = 5000;
                    var lst = Context.SPACA_020(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdAlumno, MostrarRetirados).ToList();

                    foreach (var q in lst)
                    {
                        Lista.Add(new ACA_020_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdMatricula = q.IdMatricula,
                            IdAlumno = q.IdAlumno,
                            IdPersonaR = q.IdPersonaR,
                            CedulaAlumno = q.CedulaAlumno,
                            NombreAlumno = q.NombreAlumno,
                            CedulaFactura = q.CedulaFactura,
                            NombreFactura = q.NombreFactura,
                            CedulaLegal = q.CedulaLegal,
                            NombreLegal = q.NombreLegal,
                            IdPersonaF = q.IdPersonaF,
                            Codigo = q.Codigo,
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
                            FechaActual = DateTime.Now.ToString("d' de 'MMMM' de 'yyyy"),
                            Total = q.Total,
                            TotalPension = q.Total.ToString("C2")
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
