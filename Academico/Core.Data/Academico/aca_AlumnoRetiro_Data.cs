using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AlumnoRetiro_Data
    {
        public List<aca_AlumnoRetiro_Info> getList(int IdEmpresa)
        {
            try
            {
                List<aca_AlumnoRetiro_Info> Lista = new List<aca_AlumnoRetiro_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT ar.IdEmpresa, ar.IdRetiro, ar.IdMatricula, m.IdAnio, m.IdSede, ar.Fecha, ar.Observacion, ar.IdCatalogoESTALU, ar.IdUsuarioAnulacion, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, p.pe_cedulaRuc, ar.Estado, cp.NomParalelo, "
                      + " jc.NomCurso, nj.NomJornada, sn.NomNivel, sn.NomSede, an.Descripcion "
                      + " FROM dbo.aca_AlumnoRetiro AS ar INNER JOIN "
                      + " dbo.aca_Matricula AS m ON ar.IdEmpresa = m.IdEmpresa AND ar.IdMatricula = m.IdMatricula INNER JOIN "
                      + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                      + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo AS an ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdNivel = sn.IdNivel AND m.IdSede = sn.IdSede AND m.IdAnio = sn.IdAnio AND m.IdEmpresa = sn.IdEmpresa LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdJornada = nj.IdJornada AND m.IdNivel = nj.IdNivel AND m.IdSede = nj.IdSede AND m.IdAnio = nj.IdAnio AND m.IdEmpresa = nj.IdEmpresa LEFT OUTER JOIN "
                      + " .aca_AnioLectivo_Jornada_Curso AS jc ON m.IdCurso = jc.IdCurso AND m.IdJornada = jc.IdJornada AND m.IdNivel = jc.IdNivel AND m.IdSede = jc.IdSede AND m.IdAnio = jc.IdAnio AND m.IdEmpresa = jc.IdEmpresa LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                      + " m.IdParalelo = cp.IdParalelo "
                      + " WHERE ar.IdEmpresa = " + IdEmpresa.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AlumnoRetiro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdRetiro = Convert.ToDecimal(reader["IdRetiro"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = reader["Observacion"].ToString(),
                            IdCatalogoESTALU = Convert.ToInt32(reader["IdCatalogoESTALU"]),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            Codigo = reader["Codigo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            IdUsuarioAnulacion = reader["IdUsuarioAnulacion"].ToString()
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa).OrderBy(q => q.Fecha).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AlumnoRetiro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdRetiro = q.IdRetiro,
                            IdMatricula = q.IdMatricula,
                            IdAnio = q.IdAnio,
                            Fecha = q.Fecha,
                            Observacion = q.Observacion,
                            IdCatalogoESTALU = q.IdCatalogoESTALU,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            Estado = q.Estado,
                            IdUsuarioAnulacion = q.IdUsuarioAnulacion
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AlumnoRetiro_Info> getList(int IdEmpresa, int IdAnio, int IdSede, bool MostrarAnulados)
        {
            try
            {
                List<aca_AlumnoRetiro_Info> Lista = new List<aca_AlumnoRetiro_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT ar.IdEmpresa, ar.IdRetiro, ar.IdMatricula, m.IdAnio, m.IdSede, ar.Fecha, ar.Observacion, ar.IdCatalogoESTALU, ar.IdUsuarioAnulacion, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, p.pe_cedulaRuc, ar.Estado, cp.NomParalelo, "
                      + " jc.NomCurso, nj.NomJornada, sn.NomNivel, sn.NomSede, an.Descripcion "
                      + " FROM dbo.aca_AlumnoRetiro AS ar INNER JOIN "
                      + " dbo.aca_Matricula AS m ON ar.IdEmpresa = m.IdEmpresa AND ar.IdMatricula = m.IdMatricula INNER JOIN "
                      + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                      + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo AS an ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdNivel = sn.IdNivel AND m.IdSede = sn.IdSede AND m.IdAnio = sn.IdAnio AND m.IdEmpresa = sn.IdEmpresa LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdJornada = nj.IdJornada AND m.IdNivel = nj.IdNivel AND m.IdSede = nj.IdSede AND m.IdAnio = nj.IdAnio AND m.IdEmpresa = nj.IdEmpresa LEFT OUTER JOIN "
                      + " .aca_AnioLectivo_Jornada_Curso AS jc ON m.IdCurso = jc.IdCurso AND m.IdJornada = jc.IdJornada AND m.IdNivel = jc.IdNivel AND m.IdSede = jc.IdSede AND m.IdAnio = jc.IdAnio AND m.IdEmpresa = jc.IdEmpresa LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                      + " m.IdParalelo = cp.IdParalelo "
                    + " WHERE ar.IdEmpresa = " + IdEmpresa.ToString() + " and m.IdAnio = "+ IdAnio.ToString() + " and m.IdSede = " + IdSede.ToString();
                    if (MostrarAnulados==false)
                    {
                        query += "ar.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AlumnoRetiro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdRetiro = Convert.ToDecimal(reader["IdRetiro"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = reader["Observacion"].ToString(),
                            IdCatalogoESTALU = Convert.ToInt32(reader["IdCatalogoESTALU"]),
                            pe_cedulaRuc = reader["pe_cedulaRuc"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            Codigo = reader["Codigo"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomSede = reader["NomSede"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomJornada = reader["NomJornada"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            NomParalelo = reader["NomParalelo"].ToString(),
                            Estado = Convert.ToBoolean(reader["Estado"]),
                            IdUsuarioAnulacion = reader["IdUsuarioAnulacion"].ToString()
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio==IdAnio && q.IdSede ==IdSede && q.Estado ==(MostrarAnulados==true ? q.Estado : true)).OrderBy(q => q.Fecha).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AlumnoRetiro_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdRetiro = q.IdRetiro,
                            IdMatricula = q.IdMatricula,
                            IdAnio = q.IdAnio,
                            Fecha = q.Fecha,
                            Observacion = q.Observacion,
                            IdCatalogoESTALU = q.IdCatalogoESTALU,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            Codigo = q.Codigo,
                            Descripcion = q.Descripcion,
                            NomSede = q.NomSede,
                            NomNivel = q.NomNivel,
                            NomJornada = q.NomJornada,
                            NomCurso = q.NomCurso,
                            NomParalelo = q.NomParalelo,
                            Estado = q.Estado,
                            IdUsuarioAnulacion = q.IdUsuarioAnulacion
                        });
                    });
                }
                */
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_AlumnoRetiro_Info getList(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_AlumnoRetiro_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdUsuarioAnulacion==null).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AlumnoRetiro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRetiro = Entity.IdRetiro,
                        IdMatricula = Entity.IdMatricula,
                        IdAlumno = Entity.IdAlumno,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        IdCatalogoESTALU = Entity.IdCatalogoESTALU,
                        Estado = Entity.Estado
                    };
                }
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdRetiro) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AlumnoRetiro_Info getInfo(int IdEmpresa, int IdRetiro)
        {
            try
            {
                aca_AlumnoRetiro_Info info= new aca_AlumnoRetiro_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT ar.IdEmpresa, ar.IdRetiro, ar.IdMatricula, m.IdAnio, m.IdSede, ar.Fecha, ar.Observacion, ar.IdCatalogoESTALU, ar.IdUsuarioAnulacion, m.IdAlumno, a.Codigo, p.pe_nombreCompleto, p.pe_cedulaRuc, ar.Estado, cp.NomParalelo, "
                      + " jc.NomCurso, nj.NomJornada, sn.NomNivel, sn.NomSede, an.Descripcion "
                      + " FROM dbo.aca_AlumnoRetiro AS ar INNER JOIN "
                      + " dbo.aca_Matricula AS m ON ar.IdEmpresa = m.IdEmpresa AND ar.IdMatricula = m.IdMatricula INNER JOIN "
                      + " dbo.aca_Alumno AS a ON m.IdEmpresa = a.IdEmpresa AND m.IdAlumno = a.IdAlumno INNER JOIN "
                      + " dbo.tb_persona AS p ON a.IdPersona = p.IdPersona LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo AS an ON m.IdEmpresa = an.IdEmpresa AND m.IdAnio = an.IdAnio LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_Sede_NivelAcademico AS sn ON m.IdNivel = sn.IdNivel AND m.IdSede = sn.IdSede AND m.IdAnio = sn.IdAnio AND m.IdEmpresa = sn.IdEmpresa LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_NivelAcademico_Jornada AS nj ON m.IdJornada = nj.IdJornada AND m.IdNivel = nj.IdNivel AND m.IdSede = nj.IdSede AND m.IdAnio = nj.IdAnio AND m.IdEmpresa = nj.IdEmpresa LEFT OUTER JOIN "
                      + " .aca_AnioLectivo_Jornada_Curso AS jc ON m.IdCurso = jc.IdCurso AND m.IdJornada = jc.IdJornada AND m.IdNivel = jc.IdNivel AND m.IdSede = jc.IdSede AND m.IdAnio = jc.IdAnio AND m.IdEmpresa = jc.IdEmpresa LEFT OUTER JOIN "
                      + " dbo.aca_AnioLectivo_Curso_Paralelo AS cp ON m.IdEmpresa = cp.IdEmpresa AND m.IdAnio = cp.IdAnio AND m.IdSede = cp.IdSede AND m.IdNivel = cp.IdNivel AND m.IdJornada = cp.IdJornada AND m.IdCurso = cp.IdCurso AND "
                      + " m.IdParalelo = cp.IdParalelo "
                    + " WHERE ar.IdEmpresa = " + IdEmpresa.ToString() + " and ar.IdRetiro = " + IdRetiro.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_AlumnoRetiro_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdRetiro = Convert.ToDecimal(reader["IdRetiro"]),
                            IdMatricula = Convert.ToDecimal(reader["IdMatricula"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            Fecha = Convert.ToDateTime(reader["Fecha"]),
                            Observacion = reader["Observacion"].ToString(),
                            IdCatalogoESTALU = Convert.ToInt32(reader["IdCatalogoESTALU"]),
                            Estado = Convert.ToBoolean(reader["Estado"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_AlumnoRetiro.Where(q => q.IdEmpresa == IdEmpresa && q.IdRetiro == IdRetiro).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AlumnoRetiro_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdRetiro = Entity.IdRetiro,
                        IdMatricula = Entity.IdMatricula,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        Fecha = Entity.Fecha,
                        Observacion = Entity.Observacion,
                        IdCatalogoESTALU = Entity.IdCatalogoESTALU,
                        Estado = Entity.Estado
                    };
                }
                */
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_AlumnoRetiro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AlumnoRetiro Entity = new aca_AlumnoRetiro
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdRetiro = info.IdRetiro = getId(info.IdEmpresa),
                        IdMatricula = info.IdMatricula,
                        IdCatalogoESTALU = info.IdCatalogoESTALU,
                        Fecha = info.Fecha,
                        Observacion = info.Observacion,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now,
                        Estado = true
                    };
                    Context.aca_AlumnoRetiro.Add(Entity);

                    aca_Alumno Entity_Alumno = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    Entity_Alumno.IdCatalogoESTALU = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.RETIRADO);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_AlumnoRetiro_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AlumnoRetiro Entity = Context.aca_AlumnoRetiro.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdRetiro == info.IdRetiro);
                    if (Entity == null)
                        return false;

                    aca_Alumno Entity_Alumno = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    Entity_Alumno.IdCatalogoESTALU = info.IdCatalogoESTALU;

                    Entity.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;
                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
