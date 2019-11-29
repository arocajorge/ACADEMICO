using Core.Data.Base;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Academico
{
    public class aca_Familia_Data
    {
        public List<aca_Familia_Info> getList(int IdEmpresa, int IdAlumno)
        {
            try
            {
                List<aca_Familia_Info> Lista = new List<aca_Familia_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Familia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            Secuencia = q.Secuencia,
                            IdPersona = q.IdPersona,
                            IdCatalogoPAREN = q.IdCatalogoPAREN,
                            NomCatalogo = q.NomCatalogo,
                            Direccion = q.Direccion,
                            Celular =q.Celular,
                            Correo =q.Correo,
                            SeFactura = q.SeFactura,
                            EsRepresentante =q.EsRepresentante,
                            IdTipoDocumento = q.IdTipoDocumento,
                            pe_Naturaleza = q.pe_Naturaleza,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombre = q.pe_nombre,
                            pe_apellido = q.pe_apellido,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            pe_sexo = q.pe_sexo,
                            IdEstadoCivil = q.IdEstadoCivil,
                            pe_fechaNacimiento = q.pe_fechaNacimiento
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

        public aca_Familia_Info getListTipo(int IdEmpresa, int IdAlumno, int IdCatalogoPAREN)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdCatalogoPAREN == IdCatalogoPAREN).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        EsRepresentante = Entity.EsRepresentante,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto
                    };
                }

                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info getInfo_Representante(int IdEmpresa, decimal IdAlumno, string Tipo)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno 
                    && ((Tipo == cl_enumeradores.eTipoRepresentante.ECON.ToString() ? q.SeFactura == true : q.EsRepresentante==true))).FirstOrDefault();

                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        Secuencia = Entity.Secuencia,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto
                    };
                }

                return info_familia;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public aca_Familia_Info getInfo(int IdEmpresa, int IdAlumno, int Secuencia)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.Secuencia == Secuencia).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        Secuencia = Entity.Secuencia,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial = Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto
                    };
                }

                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info getInfo_ExistePersonaParentezco(int IdEmpresa, decimal IdAlumno, decimal IdPersona, int IdCatalogoPAREN)
        {
            try
            {
                aca_Familia_Info info_familia = new aca_Familia_Info();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var Entity = odata.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.IdPersona == IdPersona && q.IdCatalogoPAREN == IdCatalogoPAREN).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info_familia = new aca_Familia_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        IdAlumno = Entity.IdAlumno,
                        Secuencia = Entity.Secuencia,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoPAREN = Entity.IdCatalogoPAREN,
                        Direccion = Entity.Direccion,
                        Celular = Entity.Celular,
                        Correo = Entity.Correo,
                        SeFactura = Entity.SeFactura,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        pe_razonSocial =Entity.pe_razonSocial,
                        pe_sexo = Entity.pe_sexo,
                        IdEstadoCivil = Entity.IdEstadoCivil,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto
                    };
                }

                return info_familia;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Familia_Info get_info_x_num_cedula(int IdEmpresa, decimal IdAlumno, string pe_cedulaRuc)
        {
            try
            {
                aca_Familia_Info info = new aca_Familia_Info();

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_per = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_per == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_fam = Context_academico.vwaca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno  && q.IdPersona == Entity_per.IdPersona).FirstOrDefault();
                if (Entity_fam == null)
                {
                    info.IdPersona = Entity_per.IdPersona;
                    info.Direccion = Entity_per.pe_direccion;
                    info.Correo = Entity_per.pe_correo;
                    info.Celular = Entity_per.pe_celular;
                    info.pe_sexo = Entity_per.pe_sexo;
                    info.pe_Naturaleza = Entity_per.pe_Naturaleza;
                    info.IdTipoDocumento = Entity_per.IdTipoDocumento;
                    info.pe_apellido = Entity_per.pe_apellido;
                    info.pe_nombre = Entity_per.pe_nombre;
                    info.pe_nombreCompleto = Entity_per.pe_nombreCompleto;
                    info.pe_razonSocial = Entity_per.pe_razonSocial;
                    info.pe_telfono_Contacto = Entity_per.pe_telfono_Contacto;
                    info.CodCatalogoSangre = Entity_per.CodCatalogoSangre;
                    info.CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS;
                    info.NumeroCarnetConadis = Entity_per.NumeroCarnetConadis;
                    info.PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad;
                    info.pe_fechaNacimiento = Entity_per.pe_fechaNacimiento;
                    info.IdEstadoCivil = Entity_per.IdEstadoCivil;
                    info.Secuencia = 0;
                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_Familia_Info
                {
                    IdEmpresa = Entity_fam.IdEmpresa,
                    IdAlumno = Entity_fam.IdAlumno,
                    Secuencia = Entity_fam.Secuencia,
                    IdCatalogoPAREN = Entity_fam.IdCatalogoPAREN,
                    Direccion = Entity_fam.Direccion,
                    Correo = Entity_fam.Correo,
                    Celular = Entity_fam.Celular,
                    SeFactura = Entity_fam.SeFactura,
                    IdPersona = Entity_fam.IdPersona,
                    pe_apellido = Entity_fam.pe_apellido,
                    pe_nombre = Entity_fam.pe_nombre,
                    pe_razonSocial = Entity_fam.pe_razonSocial,
                    pe_Naturaleza = Entity_fam.pe_Naturaleza,
                    IdTipoDocumento = Entity_fam.IdTipoDocumento,
                    pe_cedulaRuc = Entity_fam.pe_cedulaRuc,
                    pe_sexo = Entity_fam.pe_sexo,
                    pe_fechaNacimiento = Entity_fam.pe_fechaNacimiento,
                    IdEstadoCivil = Entity_fam.IdEstadoCivil,
                    pe_nombreCompleto = Entity_fam.pe_nombreCompleto,
                    pe_telfono_Contacto = Entity_fam.pe_telfono_Contacto,
                    CodCatalogoSangre = Entity_fam.CodCatalogoSangre,
                    CodCatalogoCONADIS = Entity_fam.CodCatalogoCONADIS,
                    NumeroCarnetConadis = Entity_fam.NumeroCarnetConadis,
                    PorcentajeDiscapacidad = Entity_fam.PorcentajeDiscapacidad
                };

                return info;
            }
            catch (Exception ex)
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
                    var cont = Context.aca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno==IdAlumno).Count();
                    if (cont > 0)
                        ID = Context.aca_Familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).Max(q => q.Secuencia) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    //var info_familia = Context.aca_Familia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.IdCatalogoPAREN == info.IdCatalogoPAREN).FirstOrDefault();
                    //if (info_familia != null)
                    //{
                    //    Context.aca_Familia.Remove(info_familia);
                    //}

                    var lst_familia = Context.aca_Familia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).ToList();
                    if (info.SeFactura == true)
                    {                       
                        if (lst_familia.Count>0)
                        {
                            foreach (var item in lst_familia)
                            {
                                item.SeFactura = false;
                            }
                            Context.SaveChanges();
                        }      
                    }

                    if (info.EsRepresentante == true)
                    {
                        if (lst_familia.Count > 0)
                        {
                            foreach (var item in lst_familia)
                            {
                                item.EsRepresentante = false;
                            }
                            Context.SaveChanges();
                        }
                    }

                    aca_Familia Entity = new aca_Familia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        Secuencia = info.Secuencia=getSecuencia(info.IdEmpresa, info.IdAlumno),
                        IdCatalogoPAREN = info.IdCatalogoPAREN,
                        IdPersona = info.IdPersona,
                        Direccion = info.Direccion,
                        Celular = info.Celular,
                        Correo = info.Correo,
                        SeFactura = info.SeFactura,
                        EsRepresentante = info.EsRepresentante,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Familia.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var lst_familia = Context.aca_Familia.Where(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno).ToList();
                    if (info.SeFactura == true)
                    {
                        if (lst_familia.Count > 0)
                        {
                            foreach (var item in lst_familia)
                            {
                                aca_Familia Entity_Update = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == item.Secuencia);
                                Entity_Update.SeFactura = false;
                            }

                        }
                    }

                    if (info.EsRepresentante == true)
                    {
                        if (lst_familia.Count > 0)
                        {
                            foreach (var item in lst_familia)
                            {
                                aca_Familia Entity_Update = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == item.Secuencia);
                                Entity_Update.EsRepresentante = false;
                            }
                        }
                    }

                    aca_Familia Entity = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == info.Secuencia);

                    if (Entity == null)
                    return false;

                    Entity.IdCatalogoPAREN = info.IdCatalogoPAREN;
                    Entity.IdPersona = info.IdPersona;
                    Entity.Direccion = info.Direccion;
                    Entity.Celular = info.Celular;
                    Entity.Correo = info.Correo;
                    Entity.SeFactura = info.SeFactura;
                    Entity.EsRepresentante = info.EsRepresentante;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool anularDB(aca_Familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Familia Entity = Context.aca_Familia.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno && q.Secuencia == info.Secuencia);

                    if (Entity == null)
                        return false;

                    Entity.IdUsuarioAnulacion = info.IdUsuarioAnulacion;
                    Entity.FechaAnulacion = info.FechaAnulacion = DateTime.Now;
                    //Entity.Estado = false;
                    Entity.MotivoAnulacion = info.MotivoAnulacion;

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
