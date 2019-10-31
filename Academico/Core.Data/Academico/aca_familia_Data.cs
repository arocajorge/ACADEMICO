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
    public class aca_familia_Data
    {
        public List<aca_familia_Info> getList(int IdEmpresa, int IdAlumno)
        {
            try
            {
                List<aca_familia_Info> Lista = new List<aca_familia_Info>();

                using (EntitiesAcademico odata = new EntitiesAcademico())
                {
                    var lst = odata.vwaca_familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == IdAlumno).ToList();

                    lst.ForEach(q =>
                    {
                        Lista.Add(new aca_familia_Info
                        {
                            IdEmpresa = q.IdEmpresa,
                            IdAlumno = q.IdAlumno,
                            IdPersona = q.IdPersona,
                            IdCatalogoPAREN = q.IdCatalogoPAREN,
                            Direccion = q.Direccion,
                            Celular =q.Celular,
                            Correo =q.Correo,
                            SeFactura = q.SeFactura,
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

        public aca_familia_Info get_info_x_num_cedula(int IdEmpresa, string pe_cedulaRuc)
        {
            try
            {
                aca_familia_Info info = new aca_familia_Info();

                EntitiesGeneral Context_general = new EntitiesGeneral();
                tb_persona Entity_per = Context_general.tb_persona.Where(q => q.pe_cedulaRuc == pe_cedulaRuc).FirstOrDefault();
                if (Entity_per == null)
                {
                    Context_general.Dispose();
                    return info;
                }

                EntitiesAcademico Context_academico = new EntitiesAcademico();
                var Entity_fam = Context_academico.vwaca_familia.Where(q => q.IdEmpresa == IdEmpresa && q.IdPersona == Entity_per.IdPersona).FirstOrDefault();
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
                    info.pe_telfono_Contacto = Entity_per.pe_telfono_Contacto;
                    info.CodCatalogoSangre = Entity_per.CodCatalogoSangre;
                    info.CodCatalogoCONADIS = Entity_per.CodCatalogoCONADIS;
                    info.NumeroCarnetConadis = Entity_per.NumeroCarnetConadis;
                    info.PorcentajeDiscapacidad = Entity_per.PorcentajeDiscapacidad;
                    info.pe_fechaNacimiento = Entity_per.pe_fechaNacimiento;
                    info.IdEstadoCivil = Entity_per.IdEstadoCivil;
                    Context_general.Dispose();
                    Context_academico.Dispose();
                    return info;
                }

                info = new aca_familia_Info
                {
                    IdEmpresa = Entity_fam.IdEmpresa,
                    IdAlumno = Entity_fam.IdAlumno,
                    IdCatalogoPAREN = Entity_fam.IdCatalogoPAREN,
                    Direccion = Entity_fam.Direccion,
                    Correo = Entity_fam.Correo,
                    Celular = Entity_fam.Celular,
                    SeFactura = Entity_fam.SeFactura,
                    IdPersona = Entity_fam.IdPersona,
                    pe_apellido = Entity_fam.pe_apellido,
                    pe_nombre = Entity_fam.pe_nombre,
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
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(aca_familia_Info info)
        {
            try
            {
                using (EntitiesAcademico Context = new EntitiesAcademico())
                {
                    aca_familia Entity = new aca_familia
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdAlumno = info.IdAlumno,
                        IdCatalogoPAREN = info.IdCatalogoPAREN,
                        IdPersona = info.IdPersona,
                        Direccion = info.Direccion,
                        Celular = info.Celular,
                        Correo = info.Correo,
                        SeFactura = info.SeFactura,
                        IdUsuarioCreacion = info.IdUsuarioCreacion,
                        FechaCreacion = info.FechaCreacion = DateTime.Now
                    };
                    Context.aca_familia.Add(Entity);

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
