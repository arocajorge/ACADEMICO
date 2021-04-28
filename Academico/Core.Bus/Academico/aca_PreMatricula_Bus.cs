using Core.Bus.General;
using Core.Data.Academico;
using Core.Data.Facturacion;
using Core.Data.General;
using Core.Info.Academico;
using Core.Info.Facturacion;
using Core.Info.General;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_PreMatricula_Bus
    {
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_PreMatricula_Data odata_prematricula = new aca_PreMatricula_Data();
        aca_Alumno_Data odata = new aca_Alumno_Data();
        tb_persona_Data odata_per = new tb_persona_Data();
        aca_Familia_Data odata_fam = new aca_Familia_Data();
        fa_cliente_Data odata_cliente = new fa_cliente_Data();
        fa_TerminoPago_Data odata_terminopago = new fa_TerminoPago_Data();
        fa_formaPago_Data odata_formapago = new fa_formaPago_Data();
        fa_Vendedor_Data odata_vendedor = new fa_Vendedor_Data();
        aca_SocioEconomico_Data odata_socioeconomico = new aca_SocioEconomico_Data();
        fa_cliente_x_fa_Vendedor_x_sucursal_Data odata_vendedor_sucursal = new fa_cliente_x_fa_Vendedor_x_sucursal_Data();
        tb_ColaCorreo_Data odata_correo = new tb_ColaCorreo_Data();
        aca_Catalogo_Data odata_catalogo = new aca_Catalogo_Data();

        public bool GuardarDB(aca_PreMatricula_Info info)
        {
            try
            {
                var grabar_alumno = false;
                var grabar_padre = false;
                var grabar_madre = false;
                var grabar_fichaSocioeconomica = false;
                var grabar_prematricula = false;

                if (info.ExisteAlumno==true)
                {
                    if (odata_per.modificarDB(info.info_alumno.info_persona_alumno))
                    {
                        grabar_alumno = true;
                    }

                    if (grabar_alumno == true)
                    {
                        if (odata.modificarDB(info.info_alumno))
                        {
                            info.IdAlumno = info.info_alumno.IdAlumno;
                            grabar_padre = true;
                        }
                    }
                }
                else
                {
                    if (bus_persona.validar_existe_cedula(info.info_alumno.pe_cedulaRuc) == 0)
                    {
                        info.info_alumno.info_persona_alumno = odata_per.armar_info(info.info_alumno.info_persona_alumno);
                        if (odata_per.guardarDB(info.info_alumno.info_persona_alumno))
                        {
                            info.info_alumno.IdPersona = info.info_alumno.info_persona_alumno.IdPersona;
                            grabar_alumno = true;
                        }
                    }
                    else
                    {
                        var data_persona = new tb_persona_Info();
                        if (info.info_alumno.IdPersona == 0)
                        {
                            data_persona = odata_per.get_info_x_num_cedula(info.info_alumno.info_persona_alumno.pe_cedulaRuc);
                            info.info_alumno.IdPersona = data_persona.IdPersona;
                        }

                        odata_per.modificarDB(info.info_alumno.info_persona_alumno);
                        grabar_alumno = true;
                    }

                    if (grabar_alumno == true)
                    {
                        if (odata.guardarDB(info.info_alumno))
                        {
                            grabar_padre = true;
                            info.IdAlumno = info.info_alumno.IdAlumno;
                        }
                    }
                }

                if (grabar_padre == true)
                {
                    if (info.info_alumno.info_valido_padre == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_alumno.info_persona_padre.pe_cedulaRuc) == 0)
                        {
                            info.info_alumno.info_persona_padre = odata_per.armar_info(info.info_alumno.info_persona_padre);
                            if (odata_per.guardarDB(info.info_alumno.info_persona_padre))
                            {
                                info.info_alumno.info_persona_padre.IdPersona = info.info_alumno.info_persona_padre.IdPersona;
                            }
                        }
                        else
                        {
                            var data_persona = new tb_persona_Info();
                            if (info.info_alumno.info_persona_padre.IdPersona == 0)
                            {
                                data_persona = odata_per.get_info_x_num_cedula(info.info_alumno.info_persona_padre.pe_cedulaRuc);
                                info.info_alumno.info_persona_padre.IdPersona = data_persona.IdPersona;
                            }

                            odata_per.modificarDB(info.info_alumno.info_persona_padre);
                        }

                        var info_fam_padre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA),
                            IdPersona = info.info_alumno.info_persona_padre.IdPersona,
                            Direccion = info.info_alumno.Direccion_padre,
                            Telefono = info.info_alumno.pe_telfono_Contacto_padre,
                            Celular = info.info_alumno.Celular_padre,
                            Correo = info.info_alumno.Correo_padre,
                            SeFactura = info.info_alumno.SeFactura_padre,
                            EsRepresentante = info.info_alumno.EsRepresentante_padre,
                            IdCatalogoFichaInst = (info.info_alumno.IdCatalogoFichaInst_padre == 0 ? null : info.info_alumno.IdCatalogoFichaInst_padre),
                            EmpresaTrabajo = info.info_alumno.EmpresaTrabajo_padre,
                            DireccionTrabajo = info.info_alumno.DireccionTrabajo_padre,
                            TelefonoTrabajo = info.info_alumno.TelefonoTrabajo_padre,
                            CargoTrabajo = info.info_alumno.CargoTrabajo_padre,
                            AniosServicio = info.info_alumno.AniosServicio_padre,
                            IngresoMensual = info.info_alumno.IngresoMensual_padre,
                            VehiculoPropio = info.info_alumno.VehiculoPropio_padre,
                            Marca = info.info_alumno.Marca_padre,
                            Modelo = info.info_alumno.Modelo_padre,
                            AnioVehiculo = info.info_alumno.AnioVehiculo_padre,
                            CasaPropia = info.info_alumno.CasaPropia_padre,
                            EstaFallecido = info.info_alumno.EstaFallecido_padre,
                            IdPais = info.info_alumno.IdPais_padre,
                            Cod_Region = info.info_alumno.Cod_Region_padre,
                            IdProvincia = info.info_alumno.IdProvincia_padre,
                            IdCiudad = info.info_alumno.IdCiudad_padre,
                            IdParroquia = info.info_alumno.IdParroquia_padre,
                            Sector = info.info_alumno.Sector_padre,
                            IdUsuarioCreacion = info.info_alumno.IdUsuario,
                            FechaCreacion = info.FechaCreacion = DateTime.Now
                        };

                        var info_padre_familia = odata_fam.getInfo_ExistePersonaParentezco(info_fam_padre.IdEmpresa, info_fam_padre.IdAlumno, info_fam_padre.IdPersona, info_fam_padre.IdCatalogoPAREN);
                        if (info_padre_familia == null)
                        {
                            if (odata_fam.guardarDB(info_fam_padre))
                            {
                                grabar_madre = true;
                            }
                        }
                        else
                        {
                            info_fam_padre.Secuencia = info_padre_familia.Secuencia;
                            info_fam_padre.IdUsuarioModificacion = info.IdUsuarioModificacion;
                            if (odata_fam.modificarDB(info_fam_padre))
                            {
                                grabar_madre = true;
                            }
                        }

                        /*CLIENTE*/
                        if (info_fam_padre.SeFactura == true)
                        {
                            var info_credito = odata_terminopago.get_info(info.info_alumno.IdTipoCredito_padre);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.info_alumno.info_persona_padre.pe_cedulaRuc);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);
                            info.IdPersonaF = info.info_alumno.info_persona_padre.IdPersona;
                            if (cliente == null)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_alumno.info_persona_padre.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.info_alumno.IdTipoCredito_padre,
                                    Idtipo_cliente = info.info_alumno.Idtipo_cliente_padre,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.info_alumno.IdCiudad_padre_fact,
                                    IdParroquia = info.info_alumno.IdParroquia_padre_fact,
                                    Celular = info.info_alumno.Celular_padre,
                                    Direccion = info.info_alumno.Direccion_padre,
                                    Correo = info.info_alumno.Correo_padre,
                                    Telefono = info.info_alumno.pe_telfono_Contacto_padre,
                                    info_persona = info.info_alumno.info_persona_padre,
                                    IdUsuario = info.IdUsuarioCreacion
                                };

                                var info_vendedor = odata_vendedor.get_list(info.IdEmpresa, false).FirstOrDefault();
                                var IdVendedor = info_vendedor.IdVendedor;

                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal.Add(new fa_cliente_x_fa_Vendedor_x_sucursal_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = info.info_alumno.IdSucursal,
                                    IdVendedor = IdVendedor,
                                    observacion = ""
                                });

                                if (odata_cliente.guardarDB(info_cliente))
                                {
                                    grabar_madre = true;
                                }
                            }
                            else
                            {
                                cliente.Idtipo_cliente = info.info_alumno.Idtipo_cliente_padre;
                                cliente.IdTipoCredito = info.info_alumno.IdTipoCredito_padre;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.info_alumno.IdCiudad_padre_fact;
                                cliente.IdParroquia = info.info_alumno.IdParroquia_padre_fact;
                                cliente.Celular = info.info_alumno.Celular_padre;
                                cliente.Direccion = info.info_alumno.Direccion_padre;
                                cliente.Correo = info.info_alumno.Correo_padre;
                                cliente.Telefono = info.info_alumno.pe_telfono_Contacto_padre;
                                cliente.info_persona = info.info_alumno.info_persona_padre;
                                cliente.IdUsuarioUltMod = info.IdUsuarioCreacion;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);

                                if (odata_cliente.modificarDB(cliente))
                                {
                                    grabar_madre = true;
                                }
                            }
                        }
                        else
                        {
                            grabar_madre = true;
                        }
                    }
                    else
                    {
                        grabar_madre = true;
                    }
                }

                if (grabar_madre == true)
                {
                    if (info.info_alumno.info_valido_madre == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_alumno.info_persona_madre.pe_cedulaRuc) == 0)
                        {
                            info.info_alumno.info_persona_madre = odata_per.armar_info(info.info_alumno.info_persona_madre);
                            if (odata_per.guardarDB(info.info_alumno.info_persona_madre))
                            {
                                info.info_alumno.info_persona_madre.IdPersona = info.info_alumno.info_persona_madre.IdPersona;
                            }
                        }
                        else
                        {
                            var data_persona = new tb_persona_Info();
                            if (info.info_alumno.info_persona_madre.IdPersona == 0)
                            {
                                data_persona = odata_per.get_info_x_num_cedula(info.info_alumno.info_persona_madre.pe_cedulaRuc);
                                info.info_alumno.info_persona_madre.IdPersona = data_persona.IdPersona;
                            }

                            odata_per.modificarDB(info.info_alumno.info_persona_madre);
                        }

                        var info_fam_madre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA),
                            IdPersona = info.info_alumno.info_persona_madre.IdPersona,
                            Direccion = info.info_alumno.Direccion_madre,
                            Telefono = info.info_alumno.pe_telfono_Contacto_madre,
                            Celular = info.info_alumno.Celular_madre,
                            Correo = info.info_alumno.Correo_madre,
                            SeFactura = info.info_alumno.SeFactura_madre,
                            IdCatalogoFichaInst = (info.info_alumno.IdCatalogoFichaInst_madre == 0 ? null : info.info_alumno.IdCatalogoFichaInst_madre),
                            EmpresaTrabajo = info.info_alumno.EmpresaTrabajo_madre,
                            DireccionTrabajo = info.info_alumno.DireccionTrabajo_madre,
                            TelefonoTrabajo = info.info_alumno.TelefonoTrabajo_madre,
                            CargoTrabajo = info.info_alumno.CargoTrabajo_madre,
                            AniosServicio = info.info_alumno.AniosServicio_madre,
                            IngresoMensual = info.info_alumno.IngresoMensual_madre,
                            VehiculoPropio = info.info_alumno.VehiculoPropio_madre,
                            Marca = info.info_alumno.Marca_madre,
                            Modelo = info.info_alumno.Modelo_madre,
                            AnioVehiculo = info.info_alumno.AnioVehiculo_madre,
                            CasaPropia = info.info_alumno.CasaPropia_madre,
                            EsRepresentante = info.info_alumno.EsRepresentante_madre,
                            EstaFallecido = info.info_alumno.EstaFallecido_madre,
                            IdPais = info.info_alumno.IdPais_madre,
                            Cod_Region = info.info_alumno.Cod_Region_madre,
                            IdProvincia = info.info_alumno.IdProvincia_madre,
                            IdCiudad = info.info_alumno.IdCiudad_madre,
                            IdParroquia = info.info_alumno.IdParroquia_madre,
                            Sector = info.info_alumno.Sector_madre,
                            IdUsuarioCreacion = info.IdUsuarioCreacion,
                            FechaCreacion = info.FechaCreacion = DateTime.Now
                        };

                        var info_madre_familia = odata_fam.getInfo_ExistePersonaParentezco(info_fam_madre.IdEmpresa, info_fam_madre.IdAlumno, info_fam_madre.IdPersona, info_fam_madre.IdCatalogoPAREN);
                        if (info_madre_familia == null)
                        {
                            odata_fam.guardarDB(info_fam_madre);
                        }
                        else
                        {
                            info_fam_madre.Secuencia = info_madre_familia.Secuencia;
                            info_fam_madre.IdUsuarioModificacion = info.IdUsuarioModificacion;

                            odata_fam.modificarDB(info_fam_madre);
                        }

                        /*CLIENTE*/
                        if (info_fam_madre.SeFactura == true)
                        {
                            var info_credito = odata_terminopago.get_info(info.info_alumno.IdTipoCredito_madre);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.info_alumno.pe_cedulaRuc_padre);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);
                            info.IdPersonaF = info.info_alumno.info_persona_madre.IdPersona;

                            if (cliente == null)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_alumno.info_persona_madre.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.info_alumno.IdTipoCredito_madre,
                                    Idtipo_cliente = info.info_alumno.Idtipo_cliente_madre,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.info_alumno.IdCiudad_madre_fact,
                                    IdParroquia = info.info_alumno.IdParroquia_madre_fact,
                                    Celular = info.info_alumno.Celular_madre,
                                    Direccion = info.info_alumno.Direccion_madre,
                                    Correo = info.info_alumno.Correo_madre,
                                    Telefono = info.info_alumno.pe_telfono_Contacto_madre,
                                    IdUsuario = info.IdUsuarioCreacion,
                                    info_persona = info.info_alumno.info_persona_madre
                                };

                                var info_vendedor = odata_vendedor.get_list(info.IdEmpresa, false).FirstOrDefault();
                                var IdVendedor = info_vendedor.IdVendedor;
                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal.Add(new fa_cliente_x_fa_Vendedor_x_sucursal_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = info.info_alumno.IdSucursal,
                                    IdVendedor = IdVendedor,
                                    observacion = ""
                                });

                                //return odata_cliente.guardarDB(info_cliente);
                                if (odata_cliente.guardarDB(info_cliente))
                                {
                                    grabar_fichaSocioeconomica = true;
                                }
                            }
                            else
                            {
                                cliente.Idtipo_cliente = info.info_alumno.Idtipo_cliente_madre;
                                cliente.IdTipoCredito = info.info_alumno.IdTipoCredito_madre;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.info_alumno.IdCiudad_madre_fact;
                                cliente.IdParroquia = info.info_alumno.IdParroquia_madre_fact;
                                cliente.Celular = info.info_alumno.Celular_madre;
                                cliente.Direccion = info.info_alumno.Direccion_madre;
                                cliente.Correo = info.info_alumno.Correo_madre;
                                cliente.Telefono = info.info_alumno.pe_telfono_Contacto_madre;
                                cliente.info_persona = info.info_alumno.info_persona_madre;
                                cliente.IdUsuarioUltMod = info.info_alumno.IdUsuario;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);
                                //return odata_cliente.modificarDB(cliente);
                                if (odata_cliente.modificarDB(cliente))
                                {
                                    grabar_fichaSocioeconomica = true;
                                }
                            }
                        }
                        else
                        {
                            grabar_fichaSocioeconomica = true;
                        }
                    }
                    else
                    {
                        grabar_fichaSocioeconomica = true;
                    }
                }

                if (info.OtraPersonaFamiliar==true)
                {
                    if (info.info_alumno.info_valido_representante == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_alumno.info_persona_representante.pe_cedulaRuc) == 0)
                        {
                            info.info_alumno.info_persona_representante = odata_per.armar_info(info.info_alumno.info_persona_representante);
                            if (odata_per.guardarDB(info.info_alumno.info_persona_representante))
                            {
                                info.info_alumno.info_persona_representante.IdPersona = info.info_alumno.info_persona_representante.IdPersona;
                            }
                        }
                        else
                        {
                            var data_persona = new tb_persona_Info();
                            if (info.info_alumno.info_persona_representante.IdPersona == 0)
                            {
                                data_persona = odata_per.get_info_x_num_cedula(info.info_alumno.info_persona_representante.pe_cedulaRuc);
                                info.info_alumno.info_persona_representante.IdPersona = data_persona.IdPersona;
                            }

                            odata_per.modificarDB(info.info_alumno.info_persona_representante);
                        }

                        var info_fam_otro_familiar = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = info.IdCatalogoPAREN_OtroFamiliar,
                            IdPersona = info.info_alumno.info_persona_representante.IdPersona,
                            Direccion = info.info_alumno.DireccionTrabajo_representante,
                            Telefono = info.info_alumno.TelefonoRepresentante,
                            Celular = info.info_alumno.Celular_representante,
                            Correo = info.info_alumno.Correo_representante,
                            SeFactura = info.info_alumno.SeFactura_representante,
                            IdCatalogoFichaInst = (info.info_alumno.IdCatalogoFichaInst_representante == 0 ? null : info.info_alumno.IdCatalogoFichaInst_representante),
                            EmpresaTrabajo = info.info_alumno.DireccionTrabajo_representante,
                            DireccionTrabajo = info.info_alumno.DireccionTrabajo_representante,
                            TelefonoTrabajo = info.info_alumno.TelefonoTrabajo_representante,
                            CargoTrabajo = info.info_alumno.CargoTrabajo_representante,
                            AniosServicio = info.info_alumno.AniosServicio_representante,
                            IngresoMensual = info.info_alumno.IngresoMensual_representante,
                            VehiculoPropio = info.info_alumno.VehiculoPropio_representante,
                            Marca = info.info_alumno.Marca_representante,
                            Modelo = info.info_alumno.Modelo_representante,
                            AnioVehiculo = info.info_alumno.AnioVehiculo_representante,
                            CasaPropia = info.info_alumno.CasaPropia_representante,
                            EsRepresentante = info.info_alumno.EsRepresentante_representante,
                            EstaFallecido = info.info_alumno.EstaFallecido_representante,
                            IdPais = info.info_alumno.IdPais_representante,
                            Cod_Region = info.info_alumno.Cod_Region_representante,
                            IdProvincia = info.info_alumno.IdProvincia_representante,
                            IdCiudad = info.info_alumno.IdCiudad_representante,
                            IdParroquia = info.info_alumno.IdParroquia_representante,
                            Sector = info.info_alumno.Sector_representante,
                            IdUsuarioCreacion = info.IdUsuarioCreacion,
                            FechaCreacion = info.FechaCreacion = DateTime.Now,
                        };

                        var info_otro_familia = odata_fam.getInfo_ExistePersonaParentezco(info_fam_otro_familiar.IdEmpresa, info_fam_otro_familiar.IdAlumno, info_fam_otro_familiar.IdPersona, info.IdCatalogoPAREN_OtroFamiliar);
                        if (info_otro_familia == null)
                        {
                            odata_fam.guardarDB(info_fam_otro_familiar);
                        }
                        else
                        {
                            info_fam_otro_familiar.Secuencia = info_otro_familia.Secuencia;
                            info_fam_otro_familiar.IdUsuarioModificacion = info.IdUsuarioModificacion;

                            odata_fam.modificarDB(info_fam_otro_familiar);
                        }

                        /*CLIENTE*/
                        if (info_fam_otro_familiar.SeFactura == true)
                        {
                            var info_credito = odata_terminopago.get_info(info.info_alumno.IdTipoCredito_representante);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.info_alumno.pe_cedulaRuc_representante);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente == null)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_alumno.info_persona_representante.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.info_alumno.IdTipoCredito_representante,
                                    Idtipo_cliente = info.info_alumno.Idtipo_cliente_representante,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.info_alumno.IdCiudad_representante_fact,
                                    IdParroquia = info.info_alumno.IdParroquia_representante_fact,
                                    Celular = info.info_alumno.Celular_representante,
                                    Direccion = info.info_alumno.Direccion_representante,
                                    Correo = info.info_alumno.Correo_representante,
                                    Telefono = info.info_alumno.pe_telfono_Contacto_representante,
                                    IdUsuario = info.IdUsuarioCreacion,
                                    info_persona = info.info_alumno.info_persona_madre
                                };

                                var info_vendedor = odata_vendedor.get_list(info.IdEmpresa, false).FirstOrDefault();
                                var IdVendedor = info_vendedor.IdVendedor;
                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal.Add(new fa_cliente_x_fa_Vendedor_x_sucursal_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdSucursal = info.info_alumno.IdSucursal,
                                    IdVendedor = IdVendedor,
                                    observacion = ""
                                });

                                if (odata_cliente.guardarDB(info_cliente))
                                {
                                    grabar_fichaSocioeconomica = true;
                                }

                            }
                            else
                            {
                                cliente.Idtipo_cliente = info.info_alumno.Idtipo_cliente_representante;
                                cliente.IdTipoCredito = info.info_alumno.IdTipoCredito_representante;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.info_alumno.IdCiudad_representante_fact;
                                cliente.IdParroquia = info.info_alumno.IdParroquia_representante_fact;
                                cliente.Celular = info.info_alumno.Celular_representante;
                                cliente.Direccion = info.info_alumno.Direccion_representante;
                                cliente.Correo = info.info_alumno.Correo_representante;
                                cliente.Telefono = info.info_alumno.pe_telfono_Contacto_representante;
                                cliente.info_persona = info.info_alumno.info_persona_representante;
                                cliente.IdUsuarioUltMod = info.info_alumno.IdUsuario;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);
                                //return odata_cliente.modificarDB(cliente);
                                if (odata_cliente.modificarDB(cliente))
                                {
                                    grabar_fichaSocioeconomica = true;
                                }
                            }
                        }
                        else
                        {
                            grabar_fichaSocioeconomica = true;
                        }
                    }
                }

                if (info.OtraPersonaFamiliar==true)
                {
                    if (info.info_alumno.SeFactura_representante==true)
                    {
                        info.IdPersonaF = info.info_alumno.info_persona_representante.IdPersona;
                    }
                    info.IdPersonaR = info.info_alumno.info_persona_representante.IdPersona;
                }
                else
                {
                    if (info.info_alumno.info_persona_representante.pe_cedulaRuc ==info.info_alumno.info_persona_padre.pe_cedulaRuc)
                    {
                        info.IdPersonaR = info.info_alumno.info_persona_padre.IdPersona;
                        if (info.info_alumno.SeFactura_padre)
                        {
                            info.IdPersonaF = info.info_alumno.info_persona_padre.IdPersona;
                        }
                    }

                    if (info.info_alumno.info_persona_representante.pe_cedulaRuc == info.info_alumno.info_persona_madre.pe_cedulaRuc)
                    {
                        info.IdPersonaR = info.info_alumno.info_persona_madre.IdPersona;
                        if (info.info_alumno.SeFactura_madre)
                        {
                            info.IdPersonaF = info.info_alumno.info_persona_madre.IdPersona;
                        }
                    }
                }

                if (grabar_fichaSocioeconomica==true)
                {
                    var info_socio_economico = odata_socioeconomico.getInfo_by_Alumno(info.IdEmpresa, info.IdAlumno);

                    if (info_socio_economico == null)
                    {
                        info.info_socioeconomico.IdAlumno = info.IdAlumno;
                        if (odata_socioeconomico.guardarDB(info.info_socioeconomico))
                        {
                            grabar_prematricula = true;
                        }
                    }
                    else
                    {
                        info_socio_economico.IdCatalogoFichaVi = info.info_socioeconomico.IdCatalogoFichaVi;
                        info_socio_economico.IdCatalogoFichaTVi = info.info_socioeconomico.IdCatalogoFichaTVi;
                        info_socio_economico.IdCatalogoFichaAg = info.info_socioeconomico.IdCatalogoFichaAg;
                        info_socio_economico.TieneElectricidad = info.info_socioeconomico.TieneElectricidad;
                        info_socio_economico.TieneHermanos = info.info_socioeconomico.TieneHermanos;
                        info_socio_economico.CantidadHermanos = info.info_socioeconomico.CantidadHermanos;
                        info_socio_economico.SueldoPadre = info.info_socioeconomico.SueldoPadre;
                        info_socio_economico.SueldoMadre = info.info_socioeconomico.SueldoMadre;
                        info_socio_economico.OtroIngresoMadre = info.info_socioeconomico.OtroIngresoMadre;
                        info_socio_economico.OtroIngresoPadre = info.info_socioeconomico.OtroIngresoPadre;
                        info_socio_economico.GastoAlimentacion = info.info_socioeconomico.GastoAlimentacion;
                        info_socio_economico.GastoEducacion = info.info_socioeconomico.GastoEducacion;
                        info_socio_economico.GastoServicioBasico = info.info_socioeconomico.GastoServicioBasico;
                        info_socio_economico.GastoSalud = info.info_socioeconomico.GastoSalud;
                        info_socio_economico.GastoArriendo = info.info_socioeconomico.GastoArriendo;
                        info_socio_economico.GastoPrestamo = info.info_socioeconomico.GastoPrestamo;
                        info_socio_economico.OtroGasto = info.info_socioeconomico.OtroGasto;
                        info_socio_economico.IdCatalogoFichaMot = info.info_socioeconomico.IdCatalogoFichaMot;
                        info_socio_economico.IdCatalogoFichaIns = info.info_socioeconomico.IdCatalogoFichaIns;
                        info_socio_economico.IdCatalogoFichaFin = info.info_socioeconomico.IdCatalogoFichaFin;
                        info_socio_economico.IdCatalogoFichaVive = info.info_socioeconomico.IdCatalogoFichaVive;
                        info_socio_economico.OtroFinanciamiento = info.info_socioeconomico.OtroFinanciamiento;
                        info_socio_economico.OtroInformacionInst = info.info_socioeconomico.OtroInformacionInst;
                        info_socio_economico.OtroMotivoIngreso = info.info_socioeconomico.OtroMotivoIngreso;
                        info_socio_economico.IdUsuarioModificacion = info.info_socioeconomico.IdUsuarioModificacion;


                        if(odata_socioeconomico.modificarDB(info_socio_economico))
                        {
                            grabar_prematricula = true;
                        }
                    }
                }


                if (grabar_prematricula==true)
                {
                    return odata_prematricula.guardarDB(info);
                }
                else
                {
                    return false;
                }
                
                //return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public aca_PreMatricula_Info GetInfo(int IdEmpresa, decimal IdPreMatricula)
        {
            try
            {
                return odata_prematricula.getInfo(IdEmpresa, IdPreMatricula);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public aca_PreMatricula_Info GetInfo_PorIdAdmision(int IdEmpresa, decimal IdAdmision)
        {
            try
            {
                return odata_prematricula.getInfo_PorIdAdmision(IdEmpresa, IdAdmision);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_PreMatricula_Info GetInfo_ProcesarPorAlumno(int IdEmpresa, int IdSede, int IdAnio, decimal IdAdmision)
        {
            try
            {
                return odata_prematricula.getInfo_ProcesarPorAlumno(IdEmpresa, IdSede, IdAnio, IdAdmision);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public aca_PreMatricula_Info GetInfo_PorIdAlumno(int IdEmpresa, int IdSede, int IdAnio, decimal IdAlumno)
        {
            try
            {
                return odata_prematricula.getInfo_PorIdAlumno(IdEmpresa, IdSede, IdAnio, IdAlumno);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<aca_PreMatricula_Info> GetList_Procesar(int IdEmpresa, int IdSede, int IdAnio)
        {
            try
            {
                return odata_prematricula.getList_Procesar(IdEmpresa, IdSede, IdAnio);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ModificarEstado(aca_PreMatricula_Info info)
        {
            try
            {
                return odata_prematricula.modificarEstado(info);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
