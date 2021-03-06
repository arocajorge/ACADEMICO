﻿using Core.Data.Base;
using Core.Info.Academico;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_AlumnoDocumento_Data
    {
        public List<aca_AlumnoDocumento_Info> getList(int IdEmpresa, decimal IdAlumno, bool MostrarEnArchivo)
        {
            try
            {
                List<aca_AlumnoDocumento_Info> Lista = new List<aca_AlumnoDocumento_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT ad.IdEmpresa, ad.IdAlumno, ad.IdDocumento, d.NomDocumento, ad.EnArchivo, d.OrdenDocumento, ad.Secuencia "
                    + " FROM dbo.aca_AlumnoDocumento AS ad WITH (nolock) INNER JOIN "
                    + " dbo.aca_Documento AS d WITH (nolock) ON ad.IdEmpresa = d.IdEmpresa AND ad.IdDocumento = d.IdDocumento "
                    + " WHERE ad.IdEmpresa = " + IdEmpresa.ToString() + " and ad.IdAlumno = " + IdAlumno.ToString();
                    if (MostrarEnArchivo == true)
                    {
                        query += " and ad.EnArchivo = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_AlumnoDocumento_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdDocumento = Convert.ToInt32(reader["IdDocumento"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            EnArchivo = Convert.ToBoolean(reader["EnArchivo"]),
                            NomDocumento = reader["NomDocumento"].ToString()

                        });
                    }
                    reader.Close();
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_AlumnoDocumento_Info getInfo(int IdEmpresa, decimal IdAlumno, int IdDocumento)
        {
            try
            {
                aca_AlumnoDocumento_Info info = new aca_AlumnoDocumento_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT ad.IdEmpresa, ad.IdAlumno, ad.IdDocumento, d.NomDocumento, ad.EnArchivo, d.OrdenDocumento, ad.Secuencia "
                    + " FROM dbo.aca_AlumnoDocumento AS ad WITH (nolock) INNER JOIN "
                    + " dbo.aca_Documento AS d WITH (nolock) ON ad.IdEmpresa = d.IdEmpresa AND ad.IdDocumento = d.IdDocumento "
                    + " WHERE ad.IdEmpresa = " + IdEmpresa.ToString() + " and ad.IdAlumno = " + IdAlumno.ToString() + " and ad.IdDocumento = " + IdDocumento.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_AlumnoDocumento_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdAlumno = Convert.ToDecimal(reader["IdAlumno"]),
                            IdDocumento = Convert.ToInt32(reader["IdDocumento"]),
                            Secuencia = Convert.ToInt32(reader["Secuencia"]),
                            EnArchivo = Convert.ToBoolean(reader["EnArchivo"]),
                            NomDocumento = reader["Secuencia"].ToString()
                        };
                    }
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_AlumnoDocumento_Info> get_list_matricula(int IdEmpresa, int IdAnio, string IdCurso, decimal IdAlumno)
        {
            try
            {
                List<aca_AlumnoDocumento_Info> Lista;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var IdSede = Convert.ToInt32(IdCurso.Substring(8, 4));
                    var IdNivel = Convert.ToInt32(IdCurso.Substring(12, 4));
                    var IdJornada = Convert.ToInt32(IdCurso.Substring(16, 4));
                    var IdCursoMat = Convert.ToInt32(IdCurso.Substring(20, 4));

                    Lista = (from q in Context.vwaca_AlumnoDocumento
                             join c in Context.aca_Documento
                             on new { q.IdEmpresa, q.IdDocumento } equals new { c.IdEmpresa, c.IdDocumento }
                             where q.IdEmpresa == IdEmpresa
                             && c.Estado == true
                             && q.EnArchivo ==true
                             select new aca_AlumnoDocumento_Info
                             {
                                 seleccionado = true,
                                 IdEmpresa = q.IdEmpresa,
                                 IdAlumno = q.IdAlumno,
                                 IdDocumento = q.IdDocumento,
                                 Secuencia = q.Secuencia,
                                 NomDocumento = q.NomDocumento
                             }).ToList();

                    var ListaDet = ((from j in Context.vwaca_AlumnoDocumento
                                     where !Context.aca_AnioLectivo_Curso_Documento.Any(n => n.IdDocumento == j.IdDocumento && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCursoMat)
                                     select new aca_AlumnoDocumento_Info
                                     {
                                         seleccionado = false,
                                         IdEmpresa = IdEmpresa,
                                         IdAlumno = IdAlumno,
                                         IdDocumento = j.IdDocumento,
                                         NomDocumento = j.NomDocumento
                                     }).ToList());

                    Lista.AddRange((from j in Context.vwaca_AlumnoDocumento
                                    where !Context.aca_AnioLectivo_Curso_Documento.Any(n => n.IdDocumento == j.IdDocumento && n.IdEmpresa == IdEmpresa && n.IdSede == IdSede && n.IdAnio == IdAnio && n.IdNivel == IdNivel && n.IdJornada == IdJornada && n.IdCurso == IdCursoMat)
                                    select new aca_AlumnoDocumento_Info
                                    {
                                        seleccionado = false,
                                        IdEmpresa = IdEmpresa,
                                        IdAlumno = IdAlumno,
                                        IdDocumento = j.IdDocumento,
                                        NomDocumento = j.NomDocumento
                                    }).ToList());
                }

                Lista.ForEach(q=> q.IdStringDoc = Convert.ToString(q.IdDocumento));
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getSecuencia(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                int ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_AlumnoDocumento.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Count();
                    if (cont > 0)
                        ID = Context.aca_AlumnoDocumento.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Max(q => q.Secuencia) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool guardarDB(aca_AlumnoDocumento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AlumnoDocumento Entity = new aca_AlumnoDocumento
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        IdDocumento = info.IdDocumento,
                        Secuencia = info.Secuencia = getSecuencia(info.IdEmpresa, info.IdAlumno),
                        EnArchivo = info.EnArchivo
                    };
                    Context.aca_AlumnoDocumento.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_AlumnoDocumento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_AlumnoDocumento Entity = Context.aca_AlumnoDocumento.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.Secuencia == info.Secuencia && q.IdAlumno==info.IdAlumno);
                    if (Entity == null)
                        return false;

                    Entity.IdDocumento = info.IdDocumento;
                    Entity.EnArchivo = info.EnArchivo;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool eliminarDB(aca_AlumnoDocumento_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var sql = "delete from aca_AlumnoDocumento where IdEmpresa =" + info.IdEmpresa + " and IdAlumno = " + info.IdAlumno + " and Secuencia = " + info.Secuencia;
                    Context.Database.ExecuteSqlCommand(sql);
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
