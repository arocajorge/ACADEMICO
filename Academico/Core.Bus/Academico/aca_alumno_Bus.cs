using Core.Bus.General;
using Core.Data.Academico;
using Core.Data.Facturacion;
using Core.Data.General;
using Core.Info.Academico;
using Core.Info.Facturacion;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Bus.Academico
{
    public class aca_Alumno_Bus
    {
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_Alumno_Data odata = new aca_Alumno_Data();
        tb_persona_Data odata_per = new tb_persona_Data();
        aca_Familia_Data odata_fam = new aca_Familia_Data();
        fa_cliente_Data odata_cliente = new fa_cliente_Data();
        fa_TerminoPago_Data odata_terminopago = new fa_TerminoPago_Data();
        fa_formaPago_Data odata_formapago = new fa_formaPago_Data();
        fa_Vendedor_Data odata_vendedor = new fa_Vendedor_Data();
        fa_cliente_x_fa_Vendedor_x_sucursal_Data odata_vendedor_sucursal = new fa_cliente_x_fa_Vendedor_x_sucursal_Data();
        public List<aca_Alumno_Info> GetList(int IdEmpresa, bool MostrarAnulados)
        {
            try
            {
                return odata.getList(IdEmpresa, MostrarAnulados);
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
                return odata.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public aca_Alumno_Info GetInfo(int IdEmpresa, decimal IdAlumno)
        {
            try
            {
                return odata.getInfo(IdEmpresa, IdAlumno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetId(int IdEmpresa)
        {
            try
            {
                return odata.getId(IdEmpresa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool GuardarDB(aca_Alumno_Info info)
        {
            try
            {
                var grabar_alumno = false;
                var grabar_padre = false;
                var grabar_madre = false;

                if (bus_persona.validar_existe_cedula(info.pe_cedulaRuc) == 0)
                {
                    info.info_persona_alumno = odata_per.armar_info(info.info_persona_alumno);
                    if (odata_per.guardarDB(info.info_persona_alumno))
                    {
                        info.IdPersona = info.info_persona_alumno.IdPersona;
                        grabar_alumno = true;
                    }
                }
                else
                {
                    odata_per.modificarDB(info.info_persona_alumno);
                    grabar_alumno = true;
                }

                if (grabar_alumno == true)
                {
                    if (odata.guardarDB(info))
                    {
                        grabar_padre = true;

                    }
                }
                
                if (grabar_padre == true)
                {
                    if (info.info_valido_padre == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_persona_padre.pe_cedulaRuc) == 0)
                        {
                            info.info_persona_padre = odata_per.armar_info(info.info_persona_padre);
                            if (odata_per.guardarDB(info.info_persona_padre))
                            {
                                info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                            }
                        }
                        else
                        {
                            if (odata_per.modificarDB(info.info_persona_padre))
                            {
                                info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                            }
                        }


                        var info_fam_padre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA),
                            IdPersona = info.info_persona_padre.IdPersona,
                            Direccion = info.Direccion_padre,
                            Celular = info.Celular_padre,
                            Correo = info.Correo_padre,
                            SeFactura = info.SeFactura_padre,
                            EsRepresentante = info.EsRepresentante_padre,
                            IdCatalogoFichaInst = (info.IdCatalogoFichaInst_padre==0 ? null : info.IdCatalogoFichaInst_padre),
                            EmpresaTrabajo = info.EmpresaTrabajo_padre,
                            DireccionTrabajo = info.DireccionTrabajo_padre,
                            TelefonoTrabajo = info.TelefonoTrabajo_padre,
                            CargoTrabajo = info.CargoTrabajo_padre,
                            AniosServicio = info.AniosServicio_padre,
                            IngresoMensual = info.IngresoMensual_padre,
                            VehiculoPropio = info.VehiculoPropio_padre,
                            Marca = info.Marca_padre,
                            Modelo = info.Modelo_padre,
                            CasaPropia = info.CasaPropia_padre,
                            IdUsuarioCreacion = info.IdUsuario,
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
                            var info_credito = odata_terminopago.get_info(info.IdTipoCredito_padre);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.pe_cedulaRuc_padre);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente == null || cliente.IdCliente== 0)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_persona_padre.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.IdTipoCredito_padre,
                                    Idtipo_cliente = info.Idtipo_cliente_padre,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.IdCiudad_padre,
                                    IdParroquia = info.IdParroquia_padre,
                                    Celular = info.Celular_padre,
                                    Direccion = info.Direccion_padre,
                                    Correo = info.Correo_padre,
                                    Telefono = info.pe_telfono_Contacto_padre,
                                    info_persona = info.info_persona_padre,
                                    IdUsuario = info.IdUsuario
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

                                if (odata_cliente.guardarDB(info_cliente))
                                {
                                    grabar_madre = true;
                                }
                            }
                            else
                            {
                                cliente.Idtipo_cliente = info.Idtipo_cliente_padre;
                                cliente.IdTipoCredito = info.IdTipoCredito_padre;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.IdCiudad_padre;
                                cliente.IdParroquia = info.IdParroquia_padre;
                                cliente.Celular = info.Celular_padre;
                                cliente.Direccion = info.Direccion_padre;
                                cliente.Correo = info.Correo_padre;
                                cliente.Telefono = info.pe_telfono_Contacto_padre;
                                cliente.info_persona = info.info_persona_padre;
                                cliente.IdUsuarioUltMod = info.IdUsuario;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);

                                if(odata_cliente.modificarDB(cliente))
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
                    if (info.info_valido_madre == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_persona_madre.pe_cedulaRuc) == 0)
                        {
                            info.info_persona_madre = odata_per.armar_info(info.info_persona_madre);
                            if (odata_per.guardarDB(info.info_persona_madre))
                            {
                                info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                            }
                        }
                        else
                        {
                            if (odata_per.modificarDB(info.info_persona_padre))
                            {
                                info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                            }
                        }

                        var info_fam_madre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA),
                            IdPersona = info.info_persona_madre.IdPersona,
                            Direccion = info.Direccion_madre,
                            Celular = info.Celular_madre,
                            Correo = info.Correo_madre,
                            SeFactura = info.SeFactura_madre,
                            IdCatalogoFichaInst = (info.IdCatalogoFichaInst_madre == 0 ? null : info.IdCatalogoFichaInst_madre),
                            EmpresaTrabajo = info.EmpresaTrabajo_madre,
                            DireccionTrabajo = info.DireccionTrabajo_madre,
                            TelefonoTrabajo = info.TelefonoTrabajo_madre,
                            CargoTrabajo = info.CargoTrabajo_madre,
                            AniosServicio = info.AniosServicio_madre,
                            IngresoMensual = info.IngresoMensual_madre,
                            VehiculoPropio = info.VehiculoPropio_madre,
                            Marca = info.Marca_madre,
                            Modelo = info.Modelo_madre,
                            CasaPropia = info.CasaPropia_madre,
                            EsRepresentante = info.EsRepresentante_madre,
                            IdUsuarioCreacion = info.IdUsuario,
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
                            var info_credito = odata_terminopago.get_info(info.IdTipoCredito_madre);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.pe_cedulaRuc_padre);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente == null || cliente.IdCliente == 0)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_persona_madre.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.IdTipoCredito_madre,
                                    Idtipo_cliente = info.Idtipo_cliente_madre,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.IdCiudad_madre,
                                    IdParroquia = info.IdParroquia_madre,
                                    Celular = info.Celular_madre,
                                    Direccion = info.Direccion_madre,
                                    Correo = info.Correo_madre,
                                    Telefono = info.pe_telfono_Contacto_madre,
                                    IdUsuario = info.IdUsuario,
                                    info_persona = info.info_persona_madre
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
                                cliente.Idtipo_cliente = info.Idtipo_cliente_madre;
                                cliente.IdTipoCredito = info.IdTipoCredito_madre;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.IdCiudad_madre;
                                cliente.IdParroquia = info.IdParroquia_madre;
                                cliente.Celular = info.Celular_madre;
                                cliente.Direccion = info.Direccion_madre;
                                cliente.Correo = info.Correo_madre;
                                cliente.Telefono = info.pe_telfono_Contacto_madre;
                                cliente.info_persona = info.info_persona_madre;
                                cliente.IdUsuarioUltMod = info.IdUsuario;

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

        public bool ModificarDB(aca_Alumno_Info info)
        {
            try
            {
                var grabar_alumno = false;
                var grabar_padre = false;
                var grabar_madre = false;

                if (odata_per.modificarDB(info.info_persona_alumno))
                {
                    grabar_alumno = true;
                }

                if (grabar_alumno == true)
                {
                    if (odata.modificarDB(info))
                    {
                        grabar_padre = true;
                    }
                }

                if (grabar_padre == true)
                {
                    if (info.info_valido_padre == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_persona_padre.pe_cedulaRuc) == 0)
                        {
                            info.info_persona_padre = odata_per.armar_info(info.info_persona_padre);
                            if (odata_per.guardarDB(info.info_persona_padre))
                            {
                                info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                            }
                        }
                        else
                        {
                            if (odata_per.modificarDB(info.info_persona_padre))
                            {
                                info.info_persona_padre.IdPersona = info.info_persona_padre.IdPersona;
                            }
                        }

                        var info_fam_padre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA),
                            IdPersona = info.info_persona_padre.IdPersona,
                            Direccion = info.Direccion_padre,
                            Celular = info.Celular_padre,
                            Correo = info.Correo_padre,
                            SeFactura = info.SeFactura_padre,
                            EsRepresentante = info.EsRepresentante_padre,
                            IdCatalogoFichaInst = (info.IdCatalogoFichaInst_padre == 0 ? null : info.IdCatalogoFichaInst_padre),
                            EmpresaTrabajo = info.EmpresaTrabajo_padre,
                            DireccionTrabajo = info.DireccionTrabajo_padre,
                            TelefonoTrabajo = info.TelefonoTrabajo_padre,
                            CargoTrabajo = info.CargoTrabajo_padre,
                            AniosServicio = info.AniosServicio_padre,
                            IngresoMensual = info.IngresoMensual_padre,
                            VehiculoPropio = info.VehiculoPropio_padre,
                            Marca = info.Marca_padre,
                            Modelo = info.Modelo_padre,
                            CasaPropia = info.CasaPropia_padre,
                            IdUsuarioCreacion = info.IdUsuario,
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

                        if (info_fam_padre.SeFactura == true)
                        {
                            var info_credito = odata_terminopago.get_info(info.IdTipoCredito_padre);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.pe_cedulaRuc_padre);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente == null || cliente.IdCliente == 0)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_persona_padre.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.IdTipoCredito_padre,
                                    Idtipo_cliente = info.Idtipo_cliente_padre,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.IdCiudad_padre,
                                    IdParroquia = info.IdParroquia_padre,
                                    Celular = info.Celular_padre,
                                    Direccion = info.Direccion_padre,
                                    Correo = info.Correo_padre,
                                    Telefono = info.pe_telfono_Contacto_padre,
                                    info_persona = info.info_persona_padre,
                                    IdUsuario = info.IdUsuario
                                };

                                var info_vendedor = odata_vendedor.get_list(info.IdEmpresa, false).FirstOrDefault();
                                var IdVendedor = info_vendedor.IdVendedor;
                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();

                                info_cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal.Add(new fa_cliente_x_fa_Vendedor_x_sucursal_Info {
                                        IdEmpresa = info.IdEmpresa,
                                        IdSucursal = info.IdSucursal,
                                        IdVendedor = IdVendedor,
                                        observacion = ""
                                    });

                                if(odata_cliente.guardarDB(info_cliente))
                                {
                                    grabar_madre = true;
                                }
                            }
                            else
                            {
                                cliente.Idtipo_cliente = info.Idtipo_cliente_padre;
                                cliente.IdTipoCredito = info.IdTipoCredito_padre;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.IdCiudad_padre;
                                cliente.IdParroquia = info.IdParroquia_padre;
                                cliente.Celular = info.Celular_padre;
                                cliente.Direccion = info.Direccion_padre;
                                cliente.Correo = info.Correo_padre;
                                cliente.Telefono = info.pe_telfono_Contacto_padre;
                                cliente.info_persona = info.info_persona_padre;
                                cliente.IdUsuarioUltMod = info.IdUsuario;

                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = new List<fa_cliente_x_fa_Vendedor_x_sucursal_Info>();
                                cliente.Lst_fa_cliente_x_fa_Vendedor_x_sucursal = odata_vendedor_sucursal.get_list(cliente.IdEmpresa, cliente.IdCliente);

                                if (odata_cliente.modificarDB(cliente))
                                {
                                    grabar_madre = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        grabar_madre = true;
                    }
                }

                if (grabar_madre == true)
                {
                    if (info.info_valido_madre == true)
                    {
                        if (bus_persona.validar_existe_cedula(info.info_persona_madre.pe_cedulaRuc) == 0)
                        {
                            info.info_persona_madre = odata_per.armar_info(info.info_persona_madre);
                            if (odata_per.guardarDB(info.info_persona_madre))
                            {
                                info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                            }
                        }
                        else
                        {
                            if (odata_per.modificarDB(info.info_persona_madre))
                            {
                                info.info_persona_madre.IdPersona = info.info_persona_madre.IdPersona;
                            }
                        }

                        var info_fam_madre = new aca_Familia_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdAlumno = info.IdAlumno,
                            IdCatalogoPAREN = Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA),
                            IdPersona = info.info_persona_madre.IdPersona,
                            Direccion = info.Direccion_madre,
                            Celular = info.Celular_madre,
                            Correo = info.Correo_madre,
                            SeFactura = info.SeFactura_madre,
                            IdCatalogoFichaInst = (info.IdCatalogoFichaInst_madre == 0 ? null : info.IdCatalogoFichaInst_madre),
                            EmpresaTrabajo = info.EmpresaTrabajo_madre,
                            DireccionTrabajo = info.DireccionTrabajo_madre,
                            TelefonoTrabajo = info.TelefonoTrabajo_madre,
                            CargoTrabajo = info.CargoTrabajo_madre,
                            AniosServicio = info.AniosServicio_madre,
                            IngresoMensual = info.IngresoMensual_madre,
                            VehiculoPropio = info.VehiculoPropio_madre,
                            Marca = info.Marca_madre,
                            Modelo = info.Modelo_madre,
                            CasaPropia = info.CasaPropia_madre,
                            EsRepresentante = info.EsRepresentante_madre,
                            IdUsuarioCreacion = info.IdUsuario,
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
                            var info_credito = odata_terminopago.get_info(info.IdTipoCredito_madre);
                            var existe_cliente = odata_cliente.get_info_x_num_cedula(info.IdEmpresa, info.pe_cedulaRuc_madre);
                            var cliente = odata_cliente.get_info(info.IdEmpresa, existe_cliente.IdCliente);

                            if (cliente == null || cliente.IdCliente == 0)
                            {
                                fa_cliente_Info info_cliente = new fa_cliente_Info
                                {
                                    IdEmpresa = info.IdEmpresa,
                                    IdPersona = info.info_persona_madre.IdPersona,
                                    cl_Cupo = 0,
                                    cl_plazo = info_credito.Dias_Vct,
                                    Codigo = "",
                                    Estado = "A",
                                    es_empresa_relacionada = false,
                                    FormaPago = "01",
                                    IdCtaCble_cxc_Credito = null,
                                    IdTipoCredito = info.IdTipoCredito_madre,
                                    Idtipo_cliente = info.Idtipo_cliente_madre,
                                    IdNivel = 1,
                                    EsClienteExportador = false,
                                    IdCiudad = info.IdCiudad_madre,
                                    IdParroquia = info.IdParroquia_madre,
                                    Celular = info.Celular_madre,
                                    Direccion = info.Direccion_madre,
                                    Correo = info.Correo_madre,
                                    Telefono = info.pe_telfono_Contacto_madre,
                                    info_persona = info.info_persona_madre,
                                    IdUsuario = info.IdUsuario
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
                                cliente.Idtipo_cliente = info.Idtipo_cliente_madre;
                                cliente.IdTipoCredito = info.IdTipoCredito_madre;
                                cliente.cl_plazo = info_credito.Dias_Vct;
                                cliente.IdCiudad = info.IdCiudad_madre;
                                cliente.IdParroquia = info.IdParroquia_madre;
                                cliente.Celular = info.Celular_madre;
                                cliente.Direccion = info.Direccion_madre;
                                cliente.Correo = info.Correo_madre;
                                cliente.Telefono = info.pe_telfono_Contacto_madre;
                                cliente.info_persona = info.info_persona_madre;
                                cliente.IdUsuarioUltMod = info.IdUsuario;

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

        public bool AnularDB(aca_Alumno_Info info)
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
