using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Info.RRHH;
using Core.Info.Helps;
using Core.Data.General;
using Core.Data.Base;

namespace Core.Data.RRHH
{
   public class ro_empleado_Data
    {
        #region Variables
        tb_persona_Data data_persona = new tb_persona_Data();
        #endregion

        public List<ro_empleado_Info> get_list_combo(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_Info> Lista;

                using (EntitiesRRHH Context = new EntitiesRRHH())
                {
                        Lista = (from q in Context.vwro_empleado_combo
                                 where q.IdEmpresa == IdEmpresa
                                 select new ro_empleado_Info
                                 {
                                     IdEmpresa = q.IdEmpresa,
                                     IdEmpleado = q.IdEmpleado,
                                     Empleado=q.Empleado,
                                     pe_cedulaRuc=q.pe_cedulaRuc,
                                     IdTipoNomina= q.IdNomina,
                                     IdSucursal=q.IdSucursal
                                 }).ToList();
                  
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_empleado_Info> get_list_combo_liquidar(int IdEmpresa)
        {
            try
            {
                List<ro_empleado_Info> Lista;
                string estado = cl_enumeradores.eEstadoEmpleadoRRHH.EST_LIQ.ToString()+","+cl_enumeradores.eEstadoEmpleadoRRHH.EST_PLQ.ToString();
                using (EntitiesRRHH Context = new EntitiesRRHH())
                {
                    Lista = (from q in Context.vwro_empleado_combo
                             where q.IdEmpresa == IdEmpresa
                             && (q.em_status== "EST_LIQ" || q.em_status== "EST_PLQ")
                             select new ro_empleado_Info
                             {
                                 IdEmpresa = q.IdEmpresa,
                                 IdEmpleado = q.IdEmpleado,
                                 Empleado = q.Empleado,
                                 pe_cedulaRuc = q.pe_cedulaRuc
                             }).ToList();

                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<ro_empleado_Info> get_list(int IdEmpresa, int IdSucursal, string em_status, bool mostrar_anulados)
        {
            try
            {

                List<ro_empleado_Info> Lista;

                using (EntitiesRRHH Context = new EntitiesRRHH())
                {
                    if (em_status=="")
                    {
                        if (mostrar_anulados)
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,
                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                        else
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     && q.em_estado == "A"
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,

                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                    }
                    else
                    {
                        if (mostrar_anulados)
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     && q.em_status == em_status
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,
                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                        else
                            Lista = (from q in Context.vwro_empleados_consulta
                                     where q.IdEmpresa == IdEmpresa
                                     && q.IdSucursal == IdSucursal
                                     && q.em_status == em_status
                                     && q.em_estado == "A"
                                     select new ro_empleado_Info
                                     {
                                         IdEmpresa = q.IdEmpresa,
                                         IdEmpleado = q.IdEmpleado,
                                         IdPersona = q.IdPersona,
                                         pe_cedulaRuc = q.pe_cedulaRuc,
                                         em_estado = q.em_estado,
                                         em_status = q.em_status,
                                         Empleado = q.Empleado,
                                         em_codigo = q.em_codigo,
                                         em_fechaIngaRol = q.em_fechaIngaRol,

                                         EstadoBool = q.em_estado == "A" ? true : false
                                     }).ToList();
                    }
                    
                }

                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ro_empleado_Info get_info(int IdEmpresa, decimal IdEmpleado)
        {
            try
            {
                ro_empleado_Info info_ = new ro_empleado_Info();

                using (EntitiesRRHH Context = new EntitiesRRHH())
                {
                    vwro_empleado_datos_generales info = Context.vwro_empleado_datos_generales.FirstOrDefault(q => q.IdEmpresa == IdEmpresa && q.IdEmpleado == IdEmpleado);
                    if (info == null)
                        return null;

                    info_ = new ro_empleado_Info
                    {
                        IdEmpresa = info.IdEmpresa,
                        IdEmpleado = info.IdEmpleado,
                        IdEmpleado_Supervisor = info.IdEmpleado_Supervisor,
                        IdPersona = info.IdPersona,
                        IdSucursal = info.IdSucursal,
                        IdTipoEmpleado = info.IdTipoEmpleado,
                        em_codigo = (info.em_codigo) == null ? info.IdEmpleado.ToString() : info.em_codigo,
                        Codigo_Biometrico = info.Codigo_Biometrico,
                        em_lugarNacimiento = info.em_lugarNacimiento,
                        em_CarnetIees = info.em_CarnetIees,
                        em_cedulaMil = info.em_cedulaMil,
                        em_fechaIngaRol = info.em_fechaIngaRol,
                        em_tipoCta = info.em_tipoCta,
                        em_NumCta = info.em_NumCta,
                        em_estado = info.em_estado,
                        IdCodSectorial = info.IdCodSectorial,
                        IdDepartamento = info.IdDepartamento,
                        IdTipoSangre = info.IdTipoSangre,
                        IdCargo = info.IdCargo,
                        IdCtaCble_Emplea = info.IdCtaCble_Emplea,
                        IdCiudad = info.IdCiudad,
                        em_mail = info.em_mail,
                        IdTipoLicencia = info.IdTipoLicencia,
                        IdBanco = info.IdBanco,
                        IdArea = info.IdArea,
                        IdDivision = info.IdDivision,
                        por_discapacidad = info.por_discapacidad,
                        carnet_conadis = info.carnet_conadis,
                        talla_pant = info.talla_pant,
                        talla_camisa = info.talla_camisa,
                        talla_zapato = info.talla_zapato,
                        em_status = info.em_status,
                        IdCondicionDiscapacidadSRI = info.IdCondicionDiscapacidadSRI,
                        IdTipoIdentDiscapacitadoSustitutoSRI = info.IdTipoIdentDiscapacitadoSustitutoSRI,
                        IdentDiscapacitadoSustitutoSRI = info.IdentDiscapacitadoSustitutoSRI,
                        IdAplicaConvenioDobleImposicionSRI = info.IdAplicaConvenioDobleImposicionSRI,
                        IdTipoResidenciaSRI = info.IdTipoResidenciaSRI,
                        IdTipoSistemaSalarioNetoSRI = info.IdTipoSistemaSalarioNetoSRI,
                        es_AcreditaHorasExtras = info.es_AcreditaHorasExtras,
                        IdTipoAnticipo = info.IdTipoAnticipo,
                        ValorAnticipo = info.ValorAnticipo,
                        CodigoSectorial = info.CodigoSectorial,
                        em_AnticipoSueldo = info.em_AnticipoSueldo,
                        Marca_Biometrico = info.Marca_Biometrico,
                        IdHorario = info.IdHorario,
                        Tiene_ingresos_compartidos = info.Tiene_ingresos_compartidos,
                        pe_cedulaRuc = info.pe_cedulaRuc,
                        pe_nombre = info.pe_nombre,
                        pe_apellido = info.pe_apellido,
                        pe_sexo = info.pe_sexo,
                        IdEstadoCivil = info.IdEstadoCivil,
                        pe_direccion = info.pe_direccion,
                        pe_telfono_Contacto = info.pe_telfono_Contacto,
                        pe_celular = info.pe_celular,
                        IdTipoDocumento = info.IdTipoDocumento,
                        pe_correo = info.pe_correo,
                        pe_fechaNacimiento = info.pe_fechaNacimiento,
                        Pago_por_horas = info.Pago_por_horas,
                        Valor_horas_vespertina = info.Valor_horas_vespertina,
                        Valor_horas_matutino = info.Valor_horas_matutino,
                        Valor_horas_brigada = info.Valor_horas_brigada,
                        Valor_hora_adicionales = info.Valor_hora_adicionales,
                        Valor_hora_control_salida=info.Valor_hora_control_salida,
                        Valor_maximo_horas_mat = info.Valor_maximo_horas_mat,
                        Valor_maximo_horas_vesp=info.Valor_maximo_horas_vesp,
                        DiasVacaciones = info.DiasVacaciones,
                        GozaMasDeQuinceDiasVaciones = info.GozaMasDeQuinceDiasVaciones,
                        CodCatalogo_Ubicacion = info.CodCatalogo_Ubicacion,
                        IdCtaCble_x_pagar_empleado=info.IdCtaCble_x_pagar_empleado,
                        IdSucursalContabilizacion = info.IdSucursalContabilizacion
                    };
                }

                return info_;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal get_id(int IdEmpresa)
        {
            try
            {
                decimal ID = 1;

                using (EntitiesRRHH Context = new EntitiesRRHH())
                {
                    var lst = from q in Context.ro_empleado
                              where q.IdEmpresa == IdEmpresa
                              select q;

                    if (lst.Count() > 0)
                        ID = lst.Max(q => q.IdEmpleado) + 1;
                }

                return ID;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
