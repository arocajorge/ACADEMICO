using Core.Data.Base;
using Core.Info.General;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.General
{
    public class tb_persona_Data
    {
        public decimal validar_existe_cedula(string pe_CedulaRuc)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    pe_CedulaRuc = pe_CedulaRuc == null ? "" : pe_CedulaRuc.Trim();

                    var lst = from q in Context.tb_persona
                              where q.pe_cedulaRuc == pe_CedulaRuc
                              select q;

                    if (lst.Count() > 0)
                        return lst.FirstOrDefault().IdPersona;
                    else
                        return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public tb_persona_Info armar_info(tb_persona_Info info)
        {
            tb_persona_Info info_retorno = new tb_persona_Info
            {
                //Campos obligatorios en toda pantalla
                pe_nombre = info.pe_nombre,
                pe_apellido = info.pe_apellido,
                pe_nombreCompleto = info.pe_nombreCompleto,
                pe_cedulaRuc = info.pe_cedulaRuc,
                pe_Naturaleza = info.pe_Naturaleza,
                IdTipoDocumento = info.IdTipoDocumento,
                pe_razonSocial = info.pe_razonSocial,

                //Campos opcionales
                pe_direccion = info.pe_direccion,
                pe_telfono_Contacto = info.pe_telfono_Contacto,
                pe_celular = info.pe_celular,
                pe_correo = info.pe_correo,
                pe_fechaNacimiento = info.pe_fechaNacimiento,
                CodCatalogoSangre = (info.CodCatalogoSangre=="" ? null : info.CodCatalogoSangre),

                CodCatalogoCONADIS = (info.CodCatalogoCONADIS=="" ? null : info.CodCatalogoCONADIS),
                NumeroCarnetConadis = info.NumeroCarnetConadis,
                PorcentajeDiscapacidad = info.PorcentajeDiscapacidad,

                //Si vienen null se pone un valor default
                IdEstadoCivil = string.IsNullOrEmpty(info.IdEstadoCivil) ? "SOLTE" : info.IdEstadoCivil,
                pe_sexo = string.IsNullOrEmpty(info.pe_sexo) ? "SEXO_MAS" : info.pe_sexo,
            };
            return info_retorno;
        }

        private decimal get_id()
        {
            try
            {
                decimal ID = 1;

                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    var lst = from q in Context.tb_persona
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdPersona) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool guardarDB(tb_persona_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_persona Entity = new tb_persona
                    {
                        IdPersona = info.IdPersona = get_id(),
                        CodPersona = info.CodPersona,
                        pe_Naturaleza = info.pe_Naturaleza,
                        pe_nombreCompleto = info.pe_nombreCompleto,
                        pe_razonSocial = info.pe_razonSocial,
                        pe_apellido = info.pe_apellido,
                        pe_nombre = info.pe_nombre,
                        IdTipoDocumento = info.IdTipoDocumento,
                        pe_cedulaRuc = info.pe_cedulaRuc.Trim(),
                        pe_direccion = info.pe_direccion,
                        pe_telfono_Contacto = info.pe_telfono_Contacto,
                        pe_celular = info.pe_celular,
                        pe_correo = info.pe_correo,
                        pe_sexo = info.pe_sexo,
                        IdEstadoCivil = info.IdEstadoCivil,
                        pe_fechaNacimiento = info.pe_fechaNacimiento,
                        pe_estado = info.pe_estado = "A",
                        pe_fechaCreacion = info.pe_fechaCreacion = DateTime.Now,
                        IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat,
                        num_cta_acreditacion = info.num_cta_acreditacion,
                        IdBanco_acreditacion = info.IdBanco_acreditacion,
                        CodCatalogoSangre = info.CodCatalogoSangre,
                        CodCatalogoCONADIS = info.CodCatalogoCONADIS,
                        NumeroCarnetConadis = info.NumeroCarnetConadis,
                        PorcentajeDiscapacidad = info.PorcentajeDiscapacidad
                };
                    Context.tb_persona.Add(Entity);
                    Context.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool modificarDB(tb_persona_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_persona Entity = Context.tb_persona.FirstOrDefault(q => q.IdPersona == info.IdPersona);
                    if (Entity == null) return false;
                    Entity.pe_Naturaleza = info.pe_Naturaleza;
                    Entity.pe_nombreCompleto = info.pe_nombreCompleto;
                    Entity.pe_razonSocial = info.pe_razonSocial;
                    Entity.pe_apellido = info.pe_apellido;
                    Entity.pe_nombre = info.pe_nombre;
                    Entity.IdTipoDocumento = info.IdTipoDocumento;
                    Entity.pe_cedulaRuc = info.pe_cedulaRuc;
                    Entity.pe_direccion = info.pe_direccion;
                    Entity.pe_telfono_Contacto = info.pe_telfono_Contacto;
                    Entity.pe_celular = info.pe_celular;
                    Entity.pe_correo = info.pe_correo;
                    Entity.pe_sexo = info.pe_sexo;
                    Entity.IdEstadoCivil = info.IdEstadoCivil;
                    Entity.pe_fechaNacimiento = info.pe_fechaNacimiento;
                    Entity.IdTipoCta_acreditacion_cat = info.IdTipoCta_acreditacion_cat;
                    Entity.num_cta_acreditacion = info.num_cta_acreditacion;
                    Entity.IdBanco_acreditacion = info.IdBanco_acreditacion;
                    Entity.CodCatalogoSangre = info.CodCatalogoSangre;
                    Entity.CodCatalogoCONADIS = info.CodCatalogoCONADIS;
                    Entity.NumeroCarnetConadis = info.NumeroCarnetConadis;
                    Entity.PorcentajeDiscapacidad = info.PorcentajeDiscapacidad;

                    Entity.pe_fechaModificacion = DateTime.Now;
                    Entity.pe_UltUsuarioModi = info.pe_UltUsuarioModi;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool anularDB(tb_persona_Info info)
        {
            try
            {
                using (EntitiesGeneral Context = new EntitiesGeneral())
                {
                    tb_persona Entity = Context.tb_persona.FirstOrDefault(q => q.IdPersona == info.IdPersona);
                    if (Entity == null) return false;
                    Entity.pe_estado = "I";
                    Entity.Fecha_UltAnu = info.Fecha_UltAnu = DateTime.Now;
                    Entity.IdUsuarioUltAnu = info.IdUsuarioUltAnu;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            var skip = args.BeginIndex;
            var take = args.EndIndex - args.BeginIndex + 1;
            List<tb_persona_Info> Lista = new List<tb_persona_Info>();
            Lista = get_list(IdEmpresa, IdTipoPersona, skip, take, args.Filter);
            return Lista;
        }

        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args, int IdEmpresa, string IdTipoPersona)
        {
            decimal id;
            if (args.Value == null || !decimal.TryParse(args.Value.ToString(), out id))
                return null;
            return get_info(IdEmpresa, IdTipoPersona, (decimal)args.Value);
        }

        public List<tb_persona_Info> get_list(int IdEmpresa, string IdTipo_persona, int skip, int take, string filter)
        {
            try
            {
                List<tb_persona_Info> Lista = new List<tb_persona_Info>();

                EntitiesGeneral context_g = new EntitiesGeneral();
                switch (IdTipo_persona)
                {
                    case "PERSONA":
                        var lstg = context_g.tb_persona.Where(q => q.pe_estado == "A" && (q.IdPersona.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdPersona).Skip(skip).Take(take);
                        foreach (var q in lstg)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdPersona
                            });
                        }
                        break;
                    case "ALUMNO":
                        EntitiesAcademico context_f = new EntitiesAcademico();
                        var lstf = context_f.vwaca_Alumno.Where(q => q.IdEmpresa == IdEmpresa && q.Estado == true && (q.IdAlumno.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdAlumno).Skip(skip).Take(take);
                        foreach (var q in lstf)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdAlumno
                            });
                        }
                        context_f.Dispose();
                        break;
                    case "TUTOR":
                        EntitiesAcademico context_t = new EntitiesAcademico();
                        var lst_tutor = context_t.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.EsProfesor == true && q.Estado == true && (q.IdProfesor.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdProfesor).Skip(skip).Take(take);
                        foreach (var q in lst_tutor)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdProfesor
                            });
                        }
                        context_t.Dispose();
                        break;
                    case "INSPECTOR":
                        EntitiesAcademico context_i = new EntitiesAcademico();
                        var lst_inspector = context_i.vwaca_Profesor.Where(q => q.IdEmpresa == IdEmpresa && q.EsInspector == true && q.Estado == true && (q.IdProfesor.ToString() + " " + q.pe_cedulaRuc + " " + q.pe_nombreCompleto).Contains(filter)).OrderBy(q => q.IdProfesor).Skip(skip).Take(take);
                        foreach (var q in lst_inspector)
                        {
                            Lista.Add(new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdProfesor
                            });
                        }
                        context_i.Dispose();
                        break;
                }

                context_g.Dispose();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public tb_persona_Info get_info(int IdEmpresa, string IdTipoPersona, decimal IdEntidad)
        {
            tb_persona_Info info = new tb_persona_Info();

            EntitiesGeneral context_g = new EntitiesGeneral();
            switch (IdTipoPersona)
            {
                case "PERSONA":
                    info = (from q in context_g.tb_persona
                            where q.pe_estado == "A"
                            && q.IdPersona == IdEntidad
                            select new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdPersona
                            }).FirstOrDefault();
                    break;
                case "ALUMNO":
                    EntitiesAcademico context_aca = new EntitiesAcademico();
                    info = (from q in context_aca.vwaca_Alumno
                            where q.Estado == true
                            && q.IdEmpresa == IdEmpresa
                            && q.IdAlumno == IdEntidad
                            select new tb_persona_Info
                            {
                                IdPersona = q.IdPersona,
                                pe_nombreCompleto = q.pe_nombreCompleto,
                                pe_cedulaRuc = q.pe_cedulaRuc,
                                IdEntidad = q.IdAlumno
                            }).FirstOrDefault();
                    context_aca.Dispose();
                    break;
            }

            context_g.Dispose();

            return info;
        }

    }
}
