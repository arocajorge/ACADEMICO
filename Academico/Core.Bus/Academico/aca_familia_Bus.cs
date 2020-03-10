using Core.Bus.General;
using Core.Data.Academico;
using Core.Data.Facturacion;
using Core.Data.General;
using Core.Info.Academico;
using Core.Info.Facturacion;
using Core.Info.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Familia_Bus
    {
        aca_Familia_Data odata = new aca_Familia_Data();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        tb_persona_Data odata_per = new tb_persona_Data();
        fa_cliente_Data odata_cliente = new fa_cliente_Data();
        fa_cliente_contactos_Data odata_cliente_contactos = new fa_cliente_contactos_Data();
        fa_TerminoPago_Data odata_terminopago = new fa_TerminoPago_Data();
        fa_formaPago_Data odata_formapago = new fa_formaPago_Data();
        fa_Vendedor_Data odata_vendedor = new fa_Vendedor_Data();
        fa_cliente_x_fa_Vendedor_x_sucursal_Data odata_vendedor_sucursal = new fa_cliente_x_fa_Vendedor_x_sucursal_Data();
        public List<aca_Familia_Info> GetList(int IdEmpresa, int IdAlumno)
        {
            try
            {
                return odata.getList(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Familia_Info GetListTipo(int IdEmpresa, decimal IdAlumno, int IdCatalogoPAREN)
        {
            try
            {
                return odata.getListTipo(IdEmpresa, IdAlumno, IdCatalogoPAREN);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Familia_Info GetInfo(int IdEmpresa, int IdAlumno, int Secuencia)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAlumno, Secuencia);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Familia_Info GetInfo_Representante(int IdEmpresa, decimal IdAlumno, string Tipo)
        {
            try
            {
                return odata.getInfo_Representante(IdEmpresa, IdAlumno, Tipo);
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
                return odata.get_info_x_num_cedula(IdEmpresa, IdAlumno, pe_cedulaRuc);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public aca_Familia_Info existe_familia(int IdEmpresa, decimal IdAlumno, string pe_cedulaRuc)
        {
            try
            {
                return odata.existe_familia(IdEmpresa, IdAlumno, pe_cedulaRuc);
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
                var grabar = false;

                if (bus_persona.validar_existe_cedula(info.info_persona.pe_cedulaRuc) == 0)
                {
                    info.info_persona = odata_per.armar_info(info.info_persona);
                    if (odata_per.guardarDB(info.info_persona))
                    {
                        info.IdPersona = info.info_persona.IdPersona;
                        grabar = true;
                    }
                }
                else
                {
                    var data_persona = new tb_persona_Info();
                    if (info.IdPersona ==0)
                    {
                        data_persona = odata_per.get_info_x_num_cedula(info.info_persona.pe_cedulaRuc);
                        info.IdPersona = data_persona.IdPersona;
                    }

                    odata_per.modificarDB(info.info_persona);
                    grabar = true;
                }

                if (grabar == true)
                {
                    if (odata.guardarDB(info))
                    {
                        /*CLIENTE*/
                        if (info.SeFactura == true)
                        {
                            var info_credito = odata_terminopago.get_info("CON");
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.info_persona.pe_cedulaRuc);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente == null || cliente.IdCliente == 0)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = "CON",
                                    Idtipo_cliente = 1,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = "09",
                                    IdParroquia = "09",
                                    Celular = info.Celular,
                                    Direccion = info.Direccion,
                                    Correo = info.Correo,
                                    Telefono = info.pe_telfono_Contacto,
                                    info_persona = info.info_persona,
                                    IdUsuario = info.IdUsuarioCreacion,
                                    Fecha_Transac = DateTime.Now
                                };

                                var info_vendedor = odata_vendedor.get_list(info.IdEmpresa, false).FirstOrDefault();
                                var IdVendedor = info_vendedor.IdVendedor;
                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal.Add(new fa_cliente_x_fa_Vendedor_x_sucursal_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = 1,
                                    IdVendedor = IdVendedor,
                                    observacion = ""
                                });

                                return odata_cliente.guardarDB(info_cliente);
                            }
                            else
                            {
                                //cliente.Idtipo_cliente = info.Idtipo_cliente;
                                //cliente.IdTipoCredito = info.IdTipoCredito;
                                //cliente.cl_plazo = info_credito.Dias_Vct;
                                //cliente.IdCiudad = info.IdCiudad;
                                //cliente.IdParroquia = info.IdParroquia;
                                cliente.Celular = info.Celular;
                                cliente.Direccion = info.Direccion;
                                cliente.Correo = info.Correo;
                                cliente.Telefono = info.pe_telfono_Contacto;
                                cliente.info_persona = info.info_persona;
                                cliente.IdUsuarioUltMod = info.IdUsuarioCreacion;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);
                                return odata_cliente.modificarDB(cliente);
                            }
                        }
                    }
                    return true;
                }

                return false;
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
                var grabar = false;

                if (bus_persona.validar_existe_cedula(info.pe_cedulaRuc) == 0)
                {
                    info.info_persona = odata_per.armar_info(info.info_persona);
                    if (odata_per.guardarDB(info.info_persona))
                    {
                        info.IdPersona = info.info_persona.IdPersona;
                        grabar = true;
                    }
                }
                else
                {
                    odata_per.modificarDB(info.info_persona);
                    grabar = true;
                }

                if (grabar == true)
                {
                    if (odata.modificarDB(info))
                    {
                        /*CLIENTE*/
                        if (info.SeFactura == true)
                        {
                            var info_credito = odata_terminopago.get_info(info.IdTipoCredito);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.pe_cedulaRuc);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente==null || cliente.IdCliente == 0)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.IdTipoCredito,
                                    Idtipo_cliente = info.Idtipo_cliente,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.IdCiudad_fact,
                                    IdParroquia = info.IdParroquia_fact,
                                    Celular = info.Celular,
                                    Direccion = info.Direccion,
                                    Correo = info.Correo,
                                    Telefono = info.pe_telfono_Contacto,
                                    info_persona = info.info_persona,
                                    IdUsuario = info.IdUsuarioCreacion
                                };

                                var info_vendedor = odata_vendedor.get_list(info.IdEmpresa, false).FirstOrDefault();
                                var IdVendedor = info_vendedor.IdVendedor;
                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal.Add(new fa_cliente_x_fa_Vendedor_x_sucursal_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = info.IdSucursal,
                                    IdVendedor = IdVendedor,
                                    observacion = ""
                                });

                                return odata_cliente.guardarDB(info_cliente);
                            }
                            else
                            {
                                cliente.Idtipo_cliente = info.Idtipo_cliente;
                                cliente.IdTipoCredito = info.IdTipoCredito;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.IdCiudad_fact;
                                cliente.IdParroquia = info.IdParroquia_fact;
                                cliente.Celular = info.Celular;
                                cliente.Direccion = info.Direccion;
                                cliente.Correo = info.Correo;
                                cliente.Telefono = info.pe_telfono_Contacto;
                                cliente.info_persona = info.info_persona;
                                cliente.IdUsuarioUltMod = info.IdUsuarioCreacion;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);

                                return odata_cliente.modificarDB(cliente);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool anularDB(aca_Familia_Info info)
        {
            try
            {
                return odata.anularDB(info);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
