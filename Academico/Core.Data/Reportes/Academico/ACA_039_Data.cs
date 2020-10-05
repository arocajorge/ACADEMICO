﻿using Core.Data.Base;
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
    public class ACA_039_Data
    {
        public List<ACA_039_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, decimal IdAlumno, bool MostrarRetirados)
        {
            try
            {
                decimal IdAlumnoIni = IdAlumno;
                decimal IdAlumnoFin = IdAlumno == 0 ? 9999999 : IdAlumno;

                List<ACA_039_Info> Lista = new List<ACA_039_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, "
                    + " a.Descripcion, sn.NomSede, sn.NomNivel, sn.OrdenNivel, nj.NomJornada, nj.OrdenJornada, jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, "
                    + " dbo.tb_persona.pe_nombreCompleto NombreAlumno, dbo.tb_persona.pe_cedulaRuc, m.IdCatalogoESTMAT, "
                    + " cast(h.Promedio as varchar) Promedio, h.IdEquivalenciaPromedio, ep.Codigo, cast(h.Conducta as varchar) Conducta, h.SecuenciaConducta, ec.Letra, ec.Equivalencia "
                    + " FROM     dbo.aca_Matricula AS m LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdEmpresa = sn.IdEmpresa AND m.IdAnio = sn.IdAnio AND m.IdSede = sn.IdSede AND m.IdNivel = sn.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAnio = a.IdAnio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdEmpresa = nj.IdEmpresa AND m.IdAnio = nj.IdAnio AND m.IdSede = nj.IdSede AND m.IdNivel = nj.IdNivel AND m.IdJornada = nj.IdJornada LEFT OUTER JOIN "
                    + " dbo.tb_persona INNER JOIN "
                    + " dbo.aca_Alumno AS al ON dbo.tb_persona.IdPersona = al.IdPersona ON m.IdEmpresa = al.IdEmpresa AND m.IdAlumno = al.IdAlumno "
                    + " LEFT OUTER JOIN(select r.IdEmpresa, r.IdMatricula  from aca_AlumnoRetiro as r where r.Estado = 1  ) as ret "
                    + " on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                    + " left outer join aca_AnioLectivoCalificacionHistorico h on h.IdEmpresa = m.IdEmpresa and h.IdMatricula = m.IdMatricula and h.IdAnio = m.IdAnio and h.IdAlumno = m.IdAlumno "
                    + " left outer join aca_AnioLectivoEquivalenciaPromedio ep on ep.IdEmpresa = m.IdEmpresa and ep.IdEquivalenciaPromedio = h.IdEquivalenciaPromedio "
                    + " left outer join aca_AnioLectivoConductaEquivalencia ec on ec.IdEmpresa = m.IdEmpresa and ec.Secuencia = h.SecuenciaConducta "
                    + " WHERE "
                    + " m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel = " + IdNivel.ToString()
                    + " and m.IdCurso = " + IdCurso.ToString()
                    + " and m.IdParalelo = " + IdParalelo.ToString()
                    + " and m.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString()
                    + " and(a.Estado = 1) AND(al.Estado = 1) "
                    + " and isnull(ret.IdMatricula, 0) = case when " + (MostrarRetirados == false ? 0 : 1) + " = 1 then isnull(ret.IdMatricula, 0) else 0 end ";

                    //string query = "SELECT a.IdEmpresa, a.IdAlumno, e.IdMatricula, b.pe_nombreCompleto AS NombreAlumno, "
                    //+ " b.pe_cedulaRuc, e.IdAnio, e.IdSede, e.IdNivel, e.IdJornada, e.IdCurso, e.IdParalelo, f.Descripcion, sn.NomSede, sn.NomNivel, "
                    //+ " nj.NomJornada,  jc.NomCurso, cp.NomParalelo, nj.OrdenJornada, sn.OrdenNivel, jc.OrdenCurso, cp.OrdenParalelo, "
                    //+ " cast(h.Promedio as varchar) Promedio, h.IdEquivalenciaPromedio, ep.Codigo, cast(h.Conducta as varchar) Conducta, h.SecuenciaConducta, ec.Letra, ec.Equivalencia "
                    //+ " FROM dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn "
                    //+ " RIGHT OUTER JOIN  dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON sn.IdEmpresa = nj.IdEmpresa AND sn.IdAnio = nj.IdAnio "
                    //+ " AND sn.IdSede = nj.IdSede AND sn.IdNivel = nj.IdNivel RIGHT OUTER JOIN dbo.aca_AnioLectivo_Jornada_Curso AS jc "
                    //+ " ON nj.IdEmpresa = jc.IdEmpresa AND nj.IdAnio = jc.IdAnio AND nj.IdSede = jc.IdSede AND nj.IdNivel = jc.IdNivel AND nj.IdJornada = jc.IdJornada "
                    //+ " RIGHT OUTER JOIN dbo.aca_AnioLectivo_Curso_Paralelo AS cp RIGHT OUTER JOIN  dbo.aca_AnioLectivo AS f INNER JOIN dbo.aca_Matricula AS e "
                    //+ " ON f.IdEmpresa = e.IdEmpresa AND f.IdAnio = e.IdAnio INNER JOIN  dbo.aca_Alumno AS a INNER JOIN dbo.tb_persona AS b ON a.IdPersona = b.IdPersona "
                    //+ " ON e.IdEmpresa = a.IdEmpresa AND e.IdAlumno = a.IdAlumno  ON cp.IdEmpresa = e.IdEmpresa AND cp.IdAnio = e.IdAnio AND cp.IdSede = e.IdSede "
                    //+ " AND cp.IdNivel = e.IdNivel AND cp.IdJornada = e.IdJornada AND cp.IdCurso = e.IdCurso AND cp.IdParalelo = e.IdParalelo "
                    //+ " ON jc.IdEmpresa = cp.IdEmpresa AND jc.IdAnio = cp.IdAnio AND jc.IdSede = cp.IdSede AND jc.IdNivel = cp.IdNivel "
                    //+ " AND jc.IdJornada = cp.IdJornada AND jc.IdCurso = cp.IdCurso "
                    //+ " LEFT OUTER JOIN(select r.IdEmpresa, r.IdMatricula  from aca_AlumnoRetiro as r where r.Estado = 1  ) as ret "
                    //+ " on e.IdEmpresa = ret.IdEmpresa and e.IdMatricula = ret.IdMatricula "
                    //+ " left outer join aca_AnioLectivoCalificacionHistorico h on h.IdEmpresa = e.IdEmpresa and h.IdMatricula = e.IdMatricula and h.IdAnio = e.IdAnio and h.IdAlumno = e.IdAlumno "
                    //+ " left outer join aca_AnioLectivoEquivalenciaPromedio ep on ep.IdEmpresa = h.IdEmpresa and ep.IdEquivalenciaPromedio = h.IdEquivalenciaPromedio "
                    //+ " left outer join aca_AnioLectivoConductaEquivalencia ec on ec.IdEmpresa = h.IdEmpresa and ec.Secuencia = h.SecuenciaConducta "
                    //+ " WHERE "
                    //  + " e.IdEmpresa = " + IdEmpresa.ToString()
                    //  + " and e.IdAnio = " + IdAnio.ToString()
                    //  + " and e.IdSede = " + IdSede.ToString()
                    //  + " and e.IdJornada = " + IdJornada.ToString()
                    //  + " and e.IdNivel = " + IdNivel.ToString()
                    //  + " and e.IdCurso = " + IdCurso.ToString()
                    //  + " and e.IdParalelo = " + IdParalelo.ToString()
                    //  + " and e.IdAlumno between " + IdAlumnoIni.ToString() + " and " + IdAlumnoFin.ToString()
                    //  + " and(f.Estado = 1) AND(a.Estado = 1) "
                    //  + " and isnull(ret.IdMatricula,0) = case when " + (MostrarRetirados == false ? 0 : 1) + " = 1 then isnull(ret.IdMatricula,0) else 0 end";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_039_Info
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
                            Promedio = reader["Promedio"].ToString(),
                            Codigo = reader["Codigo"].ToString(),
                            Conducta = reader["Conducta"].ToString(),
                            Letra = reader["Letra"].ToString(),
                            Equivalencia = reader["Equivalencia"].ToString(),
                            IdCatalogoESTMAT = Convert.ToInt32(reader["IdCatalogoESTMAT"]),
                            EstadoMatriculacion = (Convert.ToInt32(reader["IdCatalogoESTMAT"]) == Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoMatricula.APROBADO) ? "y aprobó el" :
                                                  Convert.ToInt32(reader["IdCatalogoESTMAT"]) == Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoMatricula.REPROBADO) ? "y reprobó el" : "en")
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
