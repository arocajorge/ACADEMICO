using Core.Data.Base;
using Core.Info.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Core.Data.Reportes.Academico
{
    public class ACA_066_Data
    {
        public List<ACA_066_Info> GetList(int IdEmpresa, int IdAnio)
        {
            try
            {
                List<ACA_066_Info> Lista = new List<ACA_066_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "declare @IdEmpresa int = " + IdEmpresa.ToString() + ", @IdAnio int = " + IdAnio.ToString()
                    + " select a.IdEmpresa, a.IdAlumno, a.Codigo, b.IdPersona, a.Codigo, b.pe_nombreCompleto, nj.OrdenJornada, nj.NomJornada, jc.OrdenCurso, jc.NomCurso,cp.NomParalelo, cp.OrdenParalelo, pt.NomPlantillaTipo, al.Descripcion "
                    + " from aca_Alumno as a WITH (nolock) join "
                    + " tb_persona as b WITH (nolock) on a.IdPersona = b.IdPersona join "
                    + " aca_Familia as c WITH (nolock) on a.IdEmpresa = c.IdEmpresa and a.IdAlumno = c.IdAlumno join "
                    + " aca_Matricula as d WITH (nolock) on a.IdEmpresa = d.IdEmpresa and a.IdAlumno = d.IdAlumno left join "
                    + " aca_AnioLectivo_NivelAcademico_Jornada as nj WITH (nolock) on d.IdEmpresa = nj.IdEmpresa and d.IdAnio = nj.IdAnio and d.IdSede = nj.IdSede and d.IdJornada = nj.IdJornada and d.IdNivel = nj.IdNivel left join "
                    + " aca_AnioLectivo_Jornada_Curso as jc WITH (nolock) on jc.IdEmpresa = d.IdEmpresa and jc.IdAnio = d.IdAnio and jc.IdSede = d.IdSede and jc.IdJornada = d.IdJornada and jc.IdNivel = d.IdNivel and jc.IdCurso = d.IdCurso left join "
                    + " aca_AnioLectivo_Curso_Paralelo as cp WITH (nolock) on cp.IdEmpresa = d.IdEmpresa and cp.IdAnio = d.IdAnio and cp.IdSede = d.IdSede and cp.IdJornada = d.IdJornada and cp.IdNivel = d.IdNivel and cp.IdCurso = d.IdCurso and cp.IdParalelo = d.IdParalelo left join "
                    + " aca_Plantilla as p WITH (nolock) on d.IdEmpresa = p.IdEmpresa and d.IdAnio = p.IdAnio and d.IdPlantilla = p.IdPlantilla left join "
                    + " aca_PlantillaTipo as pt WITH (nolock) on pt.IdTipoPlantilla = p.IdTipoPlantilla and pt.IdEmpresa = p.IdEmpresa left join "
                    + " aca_AnioLectivo as al WITH (nolock) on d.IdEmpresa = al.IdEmpresa and d.IdAnio = al.IdAnio "
                    + " where a.IdEmpresa = @IdEmpresa and d.IdAnio = @IdAnio and c.IdCatalogoPAREN in (10,11) and exists("
                        + " select f.IdEmpresa "
                        + " from aca_Familia as f WITH (nolock) "
                        + " join "
                    + " aca_Alumno as f1 WITH (nolock) on f.IdEmpresa = f1.IdEmpresa and f.IdAlumno = f1.IdAlumno join "
                        + " aca_Matricula as f2 WITH (nolock) on f2.IdEmpresa = f1.IdEmpresa and f2.IdAlumno = f1.IdAlumno "
                        + " where f.Estado = 1 AND f.IdEmpresa = @IdEmpresa and f2.IdAnio = @IdAnio and c.IdEmpresa = f.IdEmpresa and c.IdPersona = f.IdPersona "
                        + " and f.IdCatalogoPAREN in (10, 11) and c.IdAlumno<> f.IdAlumno and not exists("
                            + " select f.IdEmpresa "
                            + " from aca_AlumnoRetiro as f3 WITH (nolock) "
                            + " where f3.Estado = 1 and f3.IdEmpresa = f2.IdEmpresa and f3.IdMatricula = f2.IdMatricula "
                            + " ) "
                    + " ) and not exists("
                    + " select f.IdEmpresa "
                    + " from aca_AlumnoRetiro as f WITH (nolock) "
                    + " where f.Estado = 1 and f.IdEmpresa = d.IdEmpresa and f.IdMatricula = d.IdMatricula) AND C.Estado = 1 "
                    + " group by a.IdEmpresa, a.IdAlumno, b.IdPersona, a.Codigo, b.pe_nombreCompleto,nj.OrdenJornada, nj.NomJornada,jc.OrdenCurso, jc.NomCurso, cp.NomParalelo, cp.OrdenParalelo, pt.NomPlantillaTipo, al.Descripcion "
                    + " order by b.pe_nombrecompleto";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_066_Info
                        {
                            Num=1,
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdPersona = Convert.ToInt32(reader["IdPersona"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Descripcion = string.IsNullOrEmpty(reader["Descripcion"].ToString()) ? null : reader["Descripcion"].ToString(),
                            //NomSede = string.IsNullOrEmpty(reader["NomSede"].ToString()) ? null : reader["NomSede"].ToString(),
                            //NomNivel = string.IsNullOrEmpty(reader["NomNivel"].ToString()) ? null : reader["NomNivel"].ToString(),
                            NomJornada = string.IsNullOrEmpty(reader["NomJornada"].ToString()) ? null : reader["NomJornada"].ToString(),
                            NomCurso = string.IsNullOrEmpty(reader["NomCurso"].ToString()) ? null : reader["NomCurso"].ToString(),
                            NomParalelo = string.IsNullOrEmpty(reader["NomParalelo"].ToString()) ? null : reader["NomParalelo"].ToString(),
                            //OrdenNivel = string.IsNullOrEmpty(reader["OrdenNivel"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenNivel"]),
                            OrdenJornada = string.IsNullOrEmpty(reader["OrdenJornada"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenJornada"]),
                            OrdenCurso = string.IsNullOrEmpty(reader["OrdenCurso"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = string.IsNullOrEmpty(reader["OrdenParalelo"].ToString()) ? (int?)null : Convert.ToInt32(reader["OrdenParalelo"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            NomPlantillaTipo = string.IsNullOrEmpty(reader["NomPlantillaTipo"].ToString()) ? null : reader["NomPlantillaTipo"].ToString(),
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
