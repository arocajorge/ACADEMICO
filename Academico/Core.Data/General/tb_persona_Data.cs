using Core.Data.Base;
using Core.Info.General;
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
                pe_razonSocial = info.pe_nombreCompleto,

                //Campos opcionales
                pe_direccion = info.pe_direccion,
                pe_telfono_Contacto = info.pe_telfono_Contacto,
                pe_celular = info.pe_celular,
                pe_correo = info.pe_correo,
                pe_fechaNacimiento = info.pe_fechaNacimiento,

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

                    Entity.pe_fechaModificacion = DateTime.Now;
                    Entity.pe_UltUsuarioModi = info.pe_UltUsuarioModi;
                    Context.SaveChanges();
                }

                return true;
            }
            catch (Exception)
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
    }
}
