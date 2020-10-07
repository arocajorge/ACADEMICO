﻿using Core.Data.Academico;
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
    public class ACA_043_Data
    {
        aca_AnioLectivo_Data odata_anio = new aca_AnioLectivo_Data();
        public List<ACA_043_Info> get_list(int IdEmpresa, int IdAnio, int IdSede, int IdNivel, int IdJornada, int IdCurso, int IdParalelo, bool MostrarRetirados)
        {
            try
            {
                List<ACA_043_Info> Lista = new List<ACA_043_Info>();

                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT m.IdEmpresa, m.IdMatricula, m.IdAnio, m.IdSede, m.IdNivel, m.IdJornada, m.IdCurso, m.IdParalelo, m.IdAlumno, a.Codigo, p.pe_nombreCompleto AS Nombrealumno, s.NombreRector, s.TelefonoRector, s.CelularRector, s.CorreoRector,  "
                    + " jc.NomCurso, jc.OrdenCurso, cp.NomParalelo, cp.OrdenParalelo, dbo.aca_Sede.NomSede "
                    + " FROM     dbo.aca_Matricula AS m INNER JOIN "
                    + " dbo.aca_Sede ON m.IdEmpresa = dbo.aca_Sede.IdEmpresa AND m.IdSede = dbo.aca_Sede.IdSede LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Jornada_Curso AS jc ON m.IdEmpresa = jc.IdEmpresa AND m.IdAnio = jc.IdAnio AND m.IdSede = jc.IdSede AND m.IdNivel = jc.IdNivel AND m.IdJornada = jc.IdJornada AND m.IdCurso = jc.IdCurso LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                    + " m.IdParalelo = cp.IdParalelo LEFT OUTER JOIN "
                    + " dbo.aca_Sede AS s ON m.IdEmpresa = s.IdEmpresa AND m.IdSede = s.IdSede LEFT OUTER JOIN "
                    + " dbo.aca_Alumno AS a ON m.IdAlumno = a.IdAlumno AND m.IdEmpresa = a.IdEmpresa LEFT OUTER JOIN "
                    + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona "
                    + " LEFT OUTER JOIN(select r.IdEmpresa, r.IdMatricula  from aca_AlumnoRetiro as r where r.Estado = 1  ) as ret "
                    + " on m.IdEmpresa = ret.IdEmpresa and m.IdMatricula = ret.IdMatricula "
                    + " WHERE "
                    + " m.IdEmpresa = " + IdEmpresa.ToString()
                    + " and m.IdAnio = " + IdAnio.ToString()
                    + " and m.IdSede = " + IdSede.ToString()
                    + " and m.IdJornada = " + IdJornada.ToString()
                    + " and m.IdNivel = " + IdNivel.ToString()
                    + " and m.IdCurso = " + IdCurso.ToString()
                    + " and m.IdParalelo = " + IdParalelo.ToString()
                    + " and(a.Estado = 1) "
                    + " and isnull(ret.IdMatricula, 0) = case when " + (MostrarRetirados == false ? 0 : 1) + " = 1 then isnull(ret.IdMatricula, 0) else 0 end ";
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new ACA_043_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            Codigo = reader["Codigo"].ToString(),
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
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            OrdenCurso = Convert.ToInt32(reader["OrdenCurso"]),
                            OrdenParalelo = Convert.ToInt32(reader["OrdenParalelo"])
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
