using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AnioLectivoCalificacionHistorico_Data
    {
        public List<aca_AnioLectivoCalificacionHistorico_Info> getList(int IdEmpresa, decimal IdAlumno, bool MostrarAnulados)
        {
            try
            {
                List<aca_AnioLectivoCalificacionHistorico_Info> Lista = new List<aca_AnioLectivoCalificacionHistorico_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT h.IdEmpresa, h.IdAnio, a.Descripcion, h.IdAlumno, p.pe_nombreCompleto, h.IdCurso, h.Promedio, h.IdEquivalenciaPromedio, "
                    + " h.Conducta, h.SecuenciaConducta, ce.Letra, n.NomNivel, c.NomCurso, h.AntiguaInstitucion, h.IdNivel, ep.Codigo, h.IdMatricula "
                    + " FROM dbo.aca_AnioLectivoCalificacionHistorico AS h INNER JOIN "
                    + " dbo.aca_AnioLectivo AS a ON h.IdEmpresa = a.IdEmpresa AND h.IdAnio = a.IdAnio INNER JOIN "
                    + " dbo.aca_Alumno AS al ON h.IdEmpresa = al.IdEmpresa AND h.IdAlumno = al.IdAlumno INNER JOIN "
                    + " dbo.tb_persona AS p ON al.IdPersona = p.IdPersona INNER JOIN "
                    + " dbo.aca_Curso AS c ON h.IdEmpresa = c.IdEmpresa AND h.IdCurso = c.IdCurso INNER JOIN "
                    + " dbo.aca_NivelAcademico AS n ON h.IdEmpresa = n.IdEmpresa AND h.IdNivel = n.IdNivel LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoEquivalenciaPromedio AS ep ON h.IdEmpresa = ep.IdEmpresa AND h.IdAnio = ep.IdAnio AND h.IdEquivalenciaPromedio = ep.IdEquivalenciaPromedio LEFT OUTER JOIN "
                    + " dbo.aca_AnioLectivoConductaEquivalencia AS ce ON h.IdEmpresa = ce.IdEmpresa AND h.IdAnio = ce.IdAnio AND h.SecuenciaConducta = ce.Secuencia "
                    + " WHERE h.IdEmpresa = " + IdEmpresa.ToString() + " and h.IdAlumno = " + IdAlumno.ToString();
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AnioLectivoCalificacionHistorico_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            Promedio = Convert.ToDecimal(reader["Promedio"]),
                            Conducta = Convert.ToDecimal(reader["Conducta"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            NomNivel = reader["NomNivel"].ToString(),
                            NomCurso = reader["NomCurso"].ToString(),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            Letra = string.IsNullOrEmpty(reader["Letra"].ToString()) ? null : reader["Letra"].ToString(),
                            AntiguaInstitucion = reader["AntiguaInstitucion"].ToString(),
                            pe_nombreCompleto = reader["pe_nombreCompleto"].ToString(),
                            IdEquivalenciaPromedio = string.IsNullOrEmpty(reader["IdEquivalenciaPromedio"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdEquivalenciaPromedio"]),
                            SecuenciaConducta = string.IsNullOrEmpty(reader["SecuenciaConducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaConducta"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                        });
                    }
                    reader.Close();
                }
                /*
                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_AnioLectivoCalificacionHistorico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_AnioLectivoCalificacionHistorico_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAnio = q.IdAnio,
                            IdAlumno = q.IdAlumno,
                            IdCurso = q.IdCurso,
                            IdNivel = q.IdNivel,
                            Promedio = q.Promedio,
                            Conducta = q.Conducta,
                            Descripcion = q.Descripcion,
                            NomNivel=q.NomNivel,
                            NomCurso= q.NomCurso,
                            IdMatricula = q.IdMatricula,
                            Letra = q.Letra,
                            AntiguaInstitucion = q.AntiguaInstitucion,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            IdEquivalenciaPromedio = q.IdEquivalenciaPromedio,
                            SecuenciaConducta = q.SecuenciaConducta,
                            Codigo = q.Codigo
                        });
                    });
                }
                */
                Lista.ForEach(q=>q.IdString = IdEmpresa.ToString("000")+ q.IdAnio.ToString("0000") + q.IdAlumno.ToString("000000") + q.IdNivel.ToString("000") + q.IdCurso.ToString("000") );
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AnioLectivoCalificacionHistorico_Info getInfo(int IdEmpresa, int IdAnio, decimal IdAlumno)
        {
            try
            {
                aca_AnioLectivoCalificacionHistorico_Info info = new aca_AnioLectivoCalificacionHistorico_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT * FROM aca_AnioLectivoCalificacionHistorico "
                    + " WHERE IdEmpresa = " + IdEmpresa.ToString() + " and IdAnio = " + IdAnio.ToString() + " and IdAnio = " + IdAnio.ToString() + " and IdAlumno = " + IdAlumno.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_AnioLectivoCalificacionHistorico_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAnio = Convert.ToInt32(reader["IdAnio"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdMatricula = string.IsNullOrEmpty(reader["IdMatricula"].ToString()) ? (decimal?)null : Convert.ToDecimal(reader["IdMatricula"]),
                            IdCurso = Convert.ToInt32(reader["IdCurso"]),
                            IdNivel = Convert.ToInt32(reader["IdNivel"]),
                            Promedio = Convert.ToDecimal(reader["Promedio"]),
                            Conducta = Convert.ToDecimal(reader["Conducta"]),
                            AntiguaInstitucion = reader["AntiguaInstitucion"].ToString(),
                            IdEquivalenciaPromedio = string.IsNullOrEmpty(reader["IdEquivalenciaPromedio"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdEquivalenciaPromedio"]),
                            SecuenciaConducta = string.IsNullOrEmpty(reader["SecuenciaConducta"].ToString()) ? (int?)null : Convert.ToInt32(reader["SecuenciaConducta"])
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.aca_AnioLectivoCalificacionHistorico.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdAlumno== IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_AnioLectivoCalificacionHistorico_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAnio = Entity.IdAnio,
                        IdAlumno = Entity.IdAlumno,
                        IdMatricula = Entity.IdMatricula,
                        IdNivel = Entity.IdNivel,
                        IdCurso = Entity.IdCurso,
                        AntiguaInstitucion = Entity.AntiguaInstitucion,
                        Promedio = Entity.Promedio,
                        IdEquivalenciaPromedio = Entity.IdEquivalenciaPromedio,
                        Conducta = Entity.Conducta,
                        SecuenciaConducta = Entity.SecuenciaConducta
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

        public bool guardarDB(aca_AnioLectivoCalificacionHistorico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoCalificacionHistorico Entity = new aca_AnioLectivoCalificacionHistorico
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAnio = info.IdAnio,
                        IdAlumno = info.IdAlumno,
                        IdMatricula = info.IdMatricula,
                        IdNivel =info.IdNivel,
                        IdCurso = info.IdCurso,
                        AntiguaInstitucion= info.AntiguaInstitucion,
                        Promedio = info.Promedio,
                        Conducta = info.Conducta,
                        IdEquivalenciaPromedio = info.IdEquivalenciaPromedio,
                        SecuenciaConducta = info.SecuenciaConducta
                    };
                    Context.aca_AnioLectivoCalificacionHistorico.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AnioLectivoCalificacionHistorico_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AnioLectivoCalificacionHistorico Entity = Context.aca_AnioLectivoCalificacionHistorico.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAnio == info.IdAnio && q.IdAlumno == info.IdAlumno);
                    if (Entity == null)
                        return false;

                    Entity.IdNivel = info.IdNivel;
                    Entity.IdCurso = info.IdCurso;
                    Entity.AntiguaInstitucion = info.AntiguaInstitucion;
                    Entity.Promedio = info.Promedio;
                    Entity.IdEquivalenciaPromedio = info.IdEquivalenciaPromedio;
                    Entity.Conducta = info.Conducta;
                    Entity.SecuenciaConducta = info.SecuenciaConducta;

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
