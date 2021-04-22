using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Profesor_Data
    {
        public List<aca_Profesor_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Profesor_Info> Lista = new List<aca_Profesor_Info>();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();

                    #region Query
                    string query = "SELECT pro.IdEmpresa, pro.IdProfesor, pro.IdPersona, pro.Codigo, pro.Estado, p.CodPersona, p.pe_Naturaleza, p.pe_nombreCompleto, "
                    + " p.pe_razonSocial, p.pe_apellido, p.pe_nombre, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_direccion, "
                    + " p.pe_telfono_Contacto, p.pe_celular, p.pe_sexo, p.IdEstadoCivil, p.pe_fechaNacimiento, p.pe_estado, p.pe_correo, pro.Correo, pro.Direccion, "
                    + " pro.Telefonos, pro.EsProfesor, pro.EsInspector, p.CodCatalogoSangre, "
                    + " p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.IdProfesion, pro.IdUsuario "
                    + " FROM     dbo.aca_Profesor AS pro WITH (nolock) INNER JOIN "
                    + " dbo.tb_persona AS p WITH(nolock) ON pro.IdPersona = p.IdPersona "
                    + " WHERE pro.IdEmpresa = " + IdEmpresa.ToString();
                    if (MostrarAnulados == false)
                    {
                        query += " and pro.Estado = 1";
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.CommandTimeout = 0;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Lista.Add(new aca_Profesor_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            EsInspector = string.IsNullOrEmpty(reader["EsInspector"].ToString()) ? false : Convert.ToBoolean(reader["EsInspector"]),
                            EsProfesor = string.IsNullOrEmpty(reader["EsProfesor"].ToString()) ? false : Convert.ToBoolean(reader["EsProfesor"])
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

        public aca_Profesor_Info getInfo(int IdEmpresa, decimal IdProfesor)
        {
            try
            {
                aca_Profesor_Info info = new aca_Profesor_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT pro.IdEmpresa, pro.IdProfesor, pro.IdPersona, pro.Codigo, pro.Estado, p.CodPersona, p.pe_Naturaleza, p.pe_nombreCompleto, "
                    + " p.pe_razonSocial, p.pe_apellido, p.pe_nombre, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_direccion, "
                    + " p.pe_telfono_Contacto, p.pe_celular, p.pe_sexo, p.IdEstadoCivil, p.pe_fechaNacimiento, p.pe_estado, p.pe_correo, pro.Correo, pro.Direccion, "
                    + " pro.Telefonos, pro.EsProfesor, pro.EsInspector, p.CodCatalogoSangre, "
                    + " p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.IdProfesion, pro.IdUsuario "
                    + " FROM     dbo.aca_Profesor AS pro WITH (nolock) INNER JOIN "
                    + " dbo.tb_persona AS p WITH(nolock) ON pro.IdPersona = p.IdPersona "
                    + " WHERE pro.IdEmpresa = " + IdEmpresa.ToString() + " and pro.IdProfesor = " + IdProfesor.ToString();
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Profesor_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefonos = string.IsNullOrEmpty(reader["Telefonos"].ToString()) ? null : reader["Telefonos"].ToString(),
                            pe_Naturaleza = string.IsNullOrEmpty(reader["pe_Naturaleza"].ToString()) ? null : reader["pe_Naturaleza"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_razonSocial = string.IsNullOrEmpty(reader["pe_razonSocial"].ToString()) ? null : reader["pe_razonSocial"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            EsInspector = string.IsNullOrEmpty(reader["EsInspector"].ToString()) ? false : Convert.ToBoolean(reader["EsInspector"]),
                            EsProfesor = string.IsNullOrEmpty(reader["EsProfesor"].ToString()) ? false : Convert.ToBoolean(reader["EsProfesor"]),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            pe_celular = string.IsNullOrEmpty(reader["pe_celular"].ToString()) ? null : reader["pe_celular"].ToString(),
                            IdTipoDocumento = string.IsNullOrEmpty(reader["IdTipoDocumento"].ToString()) ? null : reader["IdTipoDocumento"].ToString(),
                            IdProfesion = string.IsNullOrEmpty(reader["IdProfesion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdProfesion"]),
                            CodCatalogoCONADIS = string.IsNullOrEmpty(reader["CodCatalogoCONADIS"].ToString()) ? null : reader["CodCatalogoCONADIS"].ToString(),
                            NumeroCarnetConadis = string.IsNullOrEmpty(reader["NumeroCarnetConadis"].ToString()) ? null : reader["NumeroCarnetConadis"].ToString(),
                            PorcentajeDiscapacidad = string.IsNullOrEmpty(reader["PorcentajeDiscapacidad"].ToString()) ? (double?)null : Convert.ToDouble(reader["PorcentajeDiscapacidad"]),
                            IdUsuario = string.IsNullOrEmpty(reader["IdUsuario"].ToString()) ? null : reader["IdUsuario"].ToString(),
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

        public aca_Profesor_Info getInfo_x_Usuario(int IdEmpresa, string IdUsuario)
        {
            try
            {
                aca_Profesor_Info info = new aca_Profesor_Info();
                using (SqlConnection connection = new SqlConnection(CadenaDeConexion.GetConnectionString()))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("", connection);
                    command.CommandText = "SELECT pro.IdEmpresa, pro.IdProfesor, pro.IdPersona, pro.Codigo, pro.Estado, p.CodPersona, p.pe_Naturaleza, p.pe_nombreCompleto, "
                    + " p.pe_razonSocial, p.pe_apellido, p.pe_nombre, p.IdTipoDocumento, p.pe_cedulaRuc, p.pe_direccion, "
                    + " p.pe_telfono_Contacto, p.pe_celular, p.pe_sexo, p.IdEstadoCivil, p.pe_fechaNacimiento, p.pe_estado, p.pe_correo, pro.Correo, pro.Direccion, "
                    + " pro.Telefonos, pro.EsProfesor, pro.EsInspector, p.CodCatalogoSangre, "
                    + " p.CodCatalogoCONADIS, p.PorcentajeDiscapacidad, p.NumeroCarnetConadis, p.IdProfesion, pro.IdUsuario "
                    + " FROM     dbo.aca_Profesor AS pro WITH (nolock) INNER JOIN "
                    + " dbo.tb_persona AS p WITH(nolock) ON pro.IdPersona = p.IdPersona "
                    + " WHERE pro.IdEmpresa = " + IdEmpresa.ToString() + " and pro.IdUsuario = '" + IdUsuario.ToString()+"'";
                    var ResultValue = command.ExecuteScalar();

                    if (ResultValue == null)
                        return null;

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        info = new aca_Profesor_Info
                        {
                            IdEmpresa = Convert.ToInt32(reader["IdEmpresa"]),
                            IdProfesor = Convert.ToDecimal(reader["IdProfesor"]),
                            IdPersona = Convert.ToDecimal(reader["IdPersona"]),
                            Codigo = string.IsNullOrEmpty(reader["Codigo"].ToString()) ? null : reader["Codigo"].ToString(),
                            Estado = string.IsNullOrEmpty(reader["Estado"].ToString()) ? false : Convert.ToBoolean(reader["Estado"]),
                            Correo = string.IsNullOrEmpty(reader["Correo"].ToString()) ? null : reader["Correo"].ToString(),
                            Direccion = string.IsNullOrEmpty(reader["Direccion"].ToString()) ? null : reader["Direccion"].ToString(),
                            Telefonos = string.IsNullOrEmpty(reader["Telefonos"].ToString()) ? null : reader["Telefonos"].ToString(),
                            pe_Naturaleza = string.IsNullOrEmpty(reader["pe_Naturaleza"].ToString()) ? null : reader["pe_Naturaleza"].ToString(),
                            pe_nombre = string.IsNullOrEmpty(reader["pe_nombre"].ToString()) ? null : reader["pe_nombre"].ToString(),
                            pe_apellido = string.IsNullOrEmpty(reader["pe_apellido"].ToString()) ? null : reader["pe_apellido"].ToString(),
                            pe_nombreCompleto = string.IsNullOrEmpty(reader["pe_nombreCompleto"].ToString()) ? null : reader["pe_nombreCompleto"].ToString(),
                            pe_razonSocial = string.IsNullOrEmpty(reader["pe_razonSocial"].ToString()) ? null : reader["pe_razonSocial"].ToString(),
                            pe_cedulaRuc = string.IsNullOrEmpty(reader["pe_cedulaRuc"].ToString()) ? null : reader["pe_cedulaRuc"].ToString(),
                            pe_sexo = string.IsNullOrEmpty(reader["pe_sexo"].ToString()) ? null : reader["pe_sexo"].ToString(),
                            IdEstadoCivil = string.IsNullOrEmpty(reader["IdEstadoCivil"].ToString()) ? null : reader["IdEstadoCivil"].ToString(),
                            EsInspector = string.IsNullOrEmpty(reader["EsInspector"].ToString()) ? false : Convert.ToBoolean(reader["EsInspector"]),
                            EsProfesor = string.IsNullOrEmpty(reader["EsProfesor"].ToString()) ? false : Convert.ToBoolean(reader["EsProfesor"]),
                            pe_fechaNacimiento = string.IsNullOrEmpty(reader["pe_fechaNacimiento"].ToString()) ? (DateTime?)null : Convert.ToDateTime(reader["pe_fechaNacimiento"]),
                            pe_celular = string.IsNullOrEmpty(reader["pe_celular"].ToString()) ? null : reader["pe_celular"].ToString(),
                            IdTipoDocumento = string.IsNullOrEmpty(reader["IdTipoDocumento"].ToString()) ? null : reader["IdTipoDocumento"].ToString(),
                            IdProfesion = string.IsNullOrEmpty(reader["IdProfesion"].ToString()) ? (int?)null : Convert.ToInt32(reader["IdProfesion"]),
                            CodCatalogoCONADIS = string.IsNullOrEmpty(reader["CodCatalogoCONADIS"].ToString()) ? null : reader["CodCatalogoCONADIS"].ToString(),
                            NumeroCarnetConadis = string.IsNullOrEmpty(reader["NumeroCarnetConadis"].ToString()) ? null : reader["NumeroCarnetConadis"].ToString(),
                            PorcentajeDiscapacidad = string.IsNullOrEmpty(reader["PorcentajeDiscapacidad"].ToString()) ? (double?)null : Convert.ToDouble(reader["PorcentajeDiscapacidad"]),
                            IdUsuario = string.IsNullOrEmpty(reader["IdUsuario"].ToString()) ? null : reader["IdUsuario"].ToString(),
                        };
                    }
                }
                /*
                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdUsuario == IdUsuario).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Profesor_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdProfesor = Entity.IdProfesor,
                        IdPersona = Entity.IdPersona,
                        Codigo = Entity.Codigo,
                        Estado = Entity.Estado,
                        Correo = Entity.Correo,
                        Direccion = Entity.Direccion,
                        Telefonos = Entity.pe_telfono_Contacto,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        pe_celular = Entity.Telefonos,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        EsInspector = Entity.EsInspector,
                        EsProfesor = Entity.EsProfesor,
                        IdProfesion = Entity.IdProfesion ?? 0,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        IdUsuarioAnulacion = Entity.IdUsuario,
                        IdUsuario = Entity.IdUsuario
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
        public decimal getId(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var cont = Context.aca_Profesor.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Profesor.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdProfesor) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Profesor_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Profesor Entity = new aca_Profesor
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdProfesor = info.IdProfesor = getId(info.IdEmpresa),
                        IdPersona = info.IdPersona,
                        Codigo = info.Codigo,
                        Estado = true,
                        Correo = info.Correo,
                        Direccion = info.Direccion,
                        Telefonos = info.Telefonos,
                        EsProfesor = info.EsProfesor,
                        EsInspector = info.EsInspector,
                        IdUsuario = info.IdUsuario,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Profesor.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Profesor_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Profesor Entity = Context.aca_Profesor.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProfesor == info.IdProfesor);
                    if (Entity == null)
                        return false;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;
                    Entity.Correo = info.Correo;
                    Entity.Direccion = info.Direccion;
                    Entity.Telefonos = info.Telefonos;
                    Entity.Codigo = info.Codigo;
                    Entity.EsInspector = info.EsInspector;
                    Entity.EsProfesor= info.EsProfesor;
                    Entity.IdUsuario = info.IdUsuario;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_Profesor_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Profesor Entity = Context.aca_Profesor.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdProfesor == info.IdProfesor);
                    if (Entity == null)
                        return false;
                    Entity.Estado = info.Estado = false;
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

        public aca_Profesor_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                aca_Profesor_Info info = new aca_Profesor_Info
                {
                    IdEmpresa = IdEmpresa
                };

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_per = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_per == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_aca = Context_academico.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_per.IdPersona).FirstOrDefault();
                if (Entity_aca == null)
                {
                    info.IdPersona = Entity_per.IdPersona;
                    info.Correo = Entity_per.pe_correo;
                    info.Direccion = Entity_per.pe_direccion;
                    info.Telefonos = Entity_per.pe_telfono_Contacto;
                    info.pe_apellido = Entity_per.pe_apellido;
                    info.pe_nombre = Entity_per.pe_nombre;
                    info.pe_cedulaRuc = Entity_per.pe_cedulaRuc;
                    info.pe_nombreCompleto = Entity_per.pe_nombreCompleto;
                    info.pe_razonSocial = Entity_per.pe_razonSocial;
                    info.pe_celular = Entity_per.pe_celular;
                    info.pe_sexo = Entity_per.pe_sexo;
                    info.IdEstadoCivil = Entity_per.IdEstadoCivil;
                    info.pe_fechaNacimiento = Entity_per.pe_fechaNacimiento;
                    info.IdProfesion = Entity_per.IdProfesion??0;
                    info.CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS;
                    info.NumeroCarnetConadis = Entity_per.NumeroCarnetConadis;
                    info.PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad;
                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_Profesor_Info
                {
                    IdEmpresa = Entity_aca.IdEmpresa,
                    IdProfesor = Entity_aca.IdProfesor,
                    IdPersona = Entity_aca.IdPersona,
                    Correo = Entity_aca.pe_correo,
                    Direccion = Entity_aca.pe_direccion,
                    Telefonos = Entity_aca.pe_telfono_Contacto,
                    pe_apellido = Entity_aca.pe_apellido,
                    pe_nombre = Entity_aca.pe_nombre,
                    pe_cedulaRuc = Entity_aca.pe_cedulaRuc,
                    pe_nombreCompleto = Entity_aca.pe_nombreCompleto,
                    pe_razonSocial = Entity_aca.pe_razonSocial,
                    pe_celular = Entity_aca.pe_celular,
                    pe_sexo = Entity_aca.pe_sexo,
                    EsInspector = Entity_aca.EsInspector,
                    EsProfesor = Entity_aca.EsProfesor,
                    IdUsuario = Entity_aca.IdUsuario,
                    IdEstadoCivil = Entity_aca.IdEstadoCivil,
                    pe_fechaNacimiento = Entity_aca.pe_fechaNacimiento,
                    IdProfesion = Entity_per.IdProfesion??0,
                    CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS,
                    NumeroCarnetConadis = Entity_per.NumeroCarnetConadis,
                    PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad
            };

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
