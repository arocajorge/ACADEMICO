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
                    var lst = odata.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Profesor_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdProfesor = q.IdProfesor,
                            IdPersona = q.IdPersona,
                            Codigo = q.Codigo,
                            Estado = q.Estado,
                            info_persona = new tb_persona_Info
                            {
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc
                            }
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

        public aca_Profesor_Info getInfo(int IdEmpresa, int IdProfesor)
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
                        info_persona = new tb_persona_Info
                        {
                            pe_Naturaleza = Entity.pe_Naturaleza,
                            pe_cedulaRuc = Entity.pe_cedulaRuc,
                            pe_nombre = Entity.pe_nombre,
                            pe_apellido = Entity.pe_apellido,
                            pe_nombreCompleto = Entity.pe_nombreCompleto,
                            pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                            pe_correo = Entity.pe_correo,
                            pe_sexo = Entity.pe_sexo,
                            IdEstadoCivil = Entity.IdEstadoCivil,
                            pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                            pe_celular = Entity.pe_celular,
                            pe_direccion = Entity.pe_direccion
                        }
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
                    var lst = from q in Context.aca_Profesor
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdProfesor) + 1;
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
                    info_persona = new tb_persona_Info()
                };

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_p = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_p == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                aca_Profesor Entity_aca = Context_academico.aca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_p.IdPersona).FirstOrDefault();
                if (Entity_aca == null)
                {
                    info.IdPersona = Entity_p.IdPersona;
                    info.info_persona = new Info.General.tb_persona_Info
                    {
                        IdPersona = Entity_p.IdPersona,
                        pe_apellido = Entity_p.pe_apellido,
                        pe_nombre = Entity_p.pe_nombre,
                        pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                        pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                        pe_razonSocial = Entity_p.pe_razonSocial,
                        pe_celular = Entity_p.pe_celular,
                        pe_telfono_Contacto = Entity_p.pe_telfono_Contacto,
                        pe_correo = Entity_p.pe_correo,
                        pe_direccion = Entity_p.pe_direccion
                    };
                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_Profesor_Info
                {
                    IdEmpresa = Entity_aca.IdEmpresa,
                    IdProfesor = Entity_aca.IdProfesor,
                    IdPersona = Entity_p.IdPersona,
                    info_persona = new Info.General.tb_persona_Info
                    {
                        IdPersona = Entity_p.IdPersona,
                        pe_apellido = Entity_p.pe_apellido,
                        pe_nombre = Entity_p.pe_nombre,
                        pe_cedulaRuc = Entity_p.pe_cedulaRuc,
                        pe_nombreCompleto = Entity_p.pe_nombreCompleto,
                        pe_razonSocial = Entity_p.pe_razonSocial,
                        pe_celular = Entity_p.pe_celular,
                        pe_telfono_Contacto = Entity_p.pe_telfono_Contacto,
                        pe_correo = Entity_p.pe_correo,
                        pe_direccion = Entity_p.pe_direccion
                    }
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
