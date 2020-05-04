using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.General;
using System;
using System.Collections.Generic;
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

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).OrderBy(q=>q.pe_nombreCompleto).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Profesor_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdProfesor = q.IdProfesor,
                            IdPersona = q.IdPersona,
                            Codigo = q.Codigo,
                            Estado = q.Estado,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            EsInspector = q.EsInspector,
                            EsProfesor = q.EsProfesor
                        });
                    });
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
                aca_Profesor_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdProfesor == IdProfesor).FirstOrDefault();
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
                        Telefonos = Entity.Telefonos,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        pe_celular = Entity.pe_celular,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        EsInspector = Entity.EsInspector,
                        EsProfesor = Entity.EsProfesor,
                        IdProfesion = Entity.IdProfesion??0,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        IdUsuario = Entity.IdUsuario,
                    };
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
                aca_Profesor_Info info;

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
                        Telefonos = Entity.Telefonos,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        pe_celular = Entity.pe_celular,
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
