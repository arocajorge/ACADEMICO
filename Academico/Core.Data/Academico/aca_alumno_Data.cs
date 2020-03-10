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
    public class aca_Alumno_Data
    {
        public List<aca_Alumno_Info> getList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                List<aca_Alumno_Info> Lista = new List<aca_Alumno_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == (MostrarAnulados ? q.Estado : true)).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_Alumno_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdPersona = q.IdPersona,
                            Codigo = q.Codigo,
                            IdTipoDocumento = q.IdTipoDocumento,
                            pe_Naturaleza = q.pe_Naturaleza,
                            pe_cedulaRuc = q.pe_cedulaRuc,
                            pe_nombre = q.pe_nombre,
                            pe_apellido = q.pe_apellido,
                            pe_nombreCompleto = q.pe_nombreCompleto,
                            pe_sexo = q.pe_sexo,
                            FechaIngreso = q.FechaIngreso,
                            pe_fechaNacimiento = q.pe_fechaNacimiento,
                            CodCatalogoSangre = q.CodCatalogoSangre,
                            CodCatalogoCONADIS = q.CodCatalogoCONADIS,
                            NumeroCarnetConadis = q.NumeroCarnetConadis,
                            PorcentajeDiscapacidad =q.PorcentajeDiscapacidad,
                            NomCatalogoESTALU = q.NomCatalogoESTALU,
                            NomCatalogoESTMAT = q.NomCatalogoESTMAT,
                            Estado = q.Estado
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

        public aca_Alumno_Info getInfo(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                aca_Alumno_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Alumno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        IdCatalogoESTALU = Entity.IdCatalogoESTALU,
                        IdCatalogoESTMAT = Entity.IdCatalogoESTMAT,
                        pe_telfono_Contacto = Entity.pe_telfono_Contacto,
                        IdTipoDocumento = Entity.IdTipoDocumento,
                        pe_Naturaleza = Entity.pe_Naturaleza,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        Codigo = Entity.Codigo,
                        Estado = Entity.Estado,
                        Correo = Entity.Correo,
                        Direccion = Entity.Direccion,
                        Celular = Entity.Celular,
                        pe_sexo = Entity.pe_sexo,
                        FechaIngreso = Entity.FechaIngreso,
                        pe_fechaNacimiento = Entity.pe_fechaNacimiento,
                        CodCatalogoSangre = Entity.CodCatalogoSangre,
                        CodCatalogoCONADIS = Entity.CodCatalogoCONADIS,
                        NumeroCarnetConadis = Entity.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = Entity.PorcentajeDiscapacidad,
                        IdPais = Entity.IdPais,
                        Cod_Region = Entity.Cod_Region,
                        IdProvincia = Entity.IdProvincia,
                        IdCiudad = Entity.IdCiudad,
                        IdParroquia = Entity.IdParroquia,
                        Sector = Entity.Sector,
                        LugarNacimiento = Entity.LugarNacimiento,
                        IdReligion = Entity.IdReligion,
                        AsisteCentroCristiano = Entity.AsisteCentroCristiano,
                        IdGrupoEtnico = Entity.IdGrupoEtnico
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_Alumno_Info getInfo(int IdEmpresa, decimal IdAlumno, string pe_cedulaRucFamiliar)
        {
            try
            {
                aca_Alumno_Info info;

                using (EntitiesAcademico db = new EntitiesAcademico())
                {
                    var Entity = db.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno && q.pe_cedulaRuc== pe_cedulaRucFamiliar).FirstOrDefault();
                    if (Entity == null)
                        return null;

                    info = new aca_Alumno_Info
                    {
                        IdEmpresa = Entity.IdEmpresa,
                        pe_nombre = Entity.pe_nombre,
                        pe_apellido = Entity.pe_apellido,
                        pe_nombreCompleto = Entity.pe_nombreCompleto,
                        IdAlumno = Entity.IdAlumno,
                        IdPersona = Entity.IdPersona,
                        pe_cedulaRuc = Entity.pe_cedulaRuc,
                        Codigo = Entity.Codigo
                    };
                }

                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_Alumno_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                aca_Alumno_Info info = new aca_Alumno_Info();

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_per = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_per == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_aca = Context_academico.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_per.IdPersona).FirstOrDefault();
                if (Entity_aca == null)
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
                    info.pe_telfono_Contacto = Entity_per.pe_telfono_Contacto;
                    info.CodCatalogoSangre = Entity_per.CodCatalogoSangre;
                    info.CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS;
                    info.NumeroCarnetConadis = Entity_per.NumeroCarnetConadis;
                    info.PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad;
                    info.pe_fechaNacimiento = Entity_per.pe_fechaNacimiento;
                    info.IdReligion = Entity_per.IdReligion;
                    info.AsisteCentroCristiano = Entity_per.AsisteCentroCristiano;
                    info.IdGrupoEtnico = Entity_per.IdGrupoEtnico;

                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_Alumno_Info
                {
                    IdEmpresa = Entity_aca.IdEmpresa,
                    Codigo = Entity_aca.Codigo,
                    IdAlumno = Entity_aca.IdAlumno,
                    Direccion = Entity_aca.Direccion,
                    Correo = Entity_aca.Correo,
                    Celular = Entity_aca.Celular,
                    IdPersona = Entity_aca.IdPersona,
                    pe_apellido = Entity_aca.pe_apellido,
                    pe_nombre = Entity_aca.pe_nombre,
                    pe_Naturaleza = Entity_aca.pe_Naturaleza,
                    IdTipoDocumento = Entity_aca.IdTipoDocumento,
                    pe_cedulaRuc = Entity_aca.pe_cedulaRuc,
                    pe_nombreCompleto = Entity_aca.pe_nombreCompleto,
                    pe_telfono_Contacto = Entity_aca.pe_telfono_Contacto,
                    CodCatalogoSangre = Entity_aca.CodCatalogoSangre,
                    CodCatalogoCONADIS = Entity_aca.CodCatalogoCONADIS,
                    NumeroCarnetConadis = Entity_aca.NumeroCarnetConadis,
                    PorcentajeDiscapacidad = Entity_aca.PorcentajeDiscapacidad
            };

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
                    var cont = Context.aca_Alumno.Where(q => q.IdEmpresa == IdEmpresa).Count();
                    if (cont > 0)
                        ID = Context.aca_Alumno.Where(q => q.IdEmpresa == IdEmpresa).Max(q => q.IdAlumno) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    var anio = info.FechaIngreso.Year;
                    var lista = getList(info.IdEmpresa, true);
                    var ListaAnio = lista.Where(q => q.FechaIngreso.Year == anio).ToList();
                    var NumEstudiante = 1;
                    var Codigo = "";

                    if (ListaAnio.Count == 0)
                    {
                        Codigo = anio + NumEstudiante.ToString("0000");
                    }
                    else
                    {
                        NumEstudiante = Convert.ToInt32(ListaAnio.Max(q => q.Codigo)) + 1;
                        Codigo = NumEstudiante.ToString("0000");
                    }

                    if (info.Codigo=="" || info.Codigo==null)
                    {
                        info.Codigo = Codigo;
                    }

                    aca_Alumno Entity = new aca_Alumno
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno = getId(info.IdEmpresa),
                        IdPersona = info.IdPersona,
                        Codigo = info.Codigo,
                        Estado = true,
                        Correo = info.Correo,
                        Direccion = info.Direccion,
                        Celular = info.Celular,
                        FechaIngreso = info.FechaIngreso,
                        IdCatalogoESTALU = info.IdCatalogoESTALU,
                        IdCatalogoESTMAT = info.IdCatalogoESTMAT,
                        IdPais = info.IdPais,
                        Cod_Region = info.Cod_Region,
                        IdProvincia = info.IdProvincia,
                        IdCiudad = info.IdCiudad,
                        IdParroquia = info.IdParroquia,
                        Sector = info.Sector,
                        LugarNacimiento = info.LugarNacimiento,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_Alumno.Add(Entity);

                    Context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool modificarDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Alumno Entity = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
                    if (Entity == null)
                        return false;
                    Entity.IdUsuarioModificacion = info.IdUsuarioModificacion;
                    Entity.FechaModificacion = info.FechaModificacion = DateTime.Now;
                    Entity.Correo = info.Correo;
                    Entity.Direccion = info.Direccion;
                    Entity.Celular = info.Celular;
                    Entity.IdPais = info.IdPais;
                    Entity.Cod_Region = info.Cod_Region;
                    Entity.IdProvincia = info.IdProvincia;
                    Entity.IdCiudad = info.IdCiudad;
                    Entity.IdParroquia = info.IdParroquia;
                    Entity.Sector = info.Sector;
                    Entity.LugarNacimiento = info.LugarNacimiento;

                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool anularDB(aca_Alumno_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_Alumno Entity = Context.aca_Alumno.FirstOrDefault(q => q.IdEmpresa == info.IdEmpresa && q.IdAlumno == info.IdAlumno);
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
    }
}
