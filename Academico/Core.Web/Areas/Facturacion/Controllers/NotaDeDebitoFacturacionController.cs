﻿using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Bus.Inventario;
using Core.Info.Academico;
using Core.Info.Facturacion;
using Core.Info.General;
using Core.Info.Helps;
using Core.Info.Inventario;
using Core.Web.Areas.Facturacion.Controllers;
using Core.Web.Areas.Inventario.Controllers;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class NotaDeDebitoFacturacionController : Controller
    {
        #region Variables
        fa_notaCreDeb_Bus bus_nota = new fa_notaCreDeb_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        fa_cliente_contactos_Bus bus_contacto = new fa_cliente_contactos_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        in_Producto_List List_producto = new in_Producto_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        fa_cliente_x_fa_Vendedor_x_sucursal_Bus bus_v_x_c = new fa_cliente_x_fa_Vendedor_x_sucursal_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
        fa_TerminoPago_Distribucion_Bus bus_termino_pago_distribucion = new fa_TerminoPago_Distribucion_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        fa_notaCreDeb_det_List List_det = new fa_notaCreDeb_det_List();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        string mensaje = string.Empty;
        fa_notaCreDeb_det_Bus bus_det = new fa_notaCreDeb_det_Bus();
        fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
        fa_notaCreDeb_x_fa_factura_NotaDeb_Bus bus_cruce = new fa_notaCreDeb_x_fa_factura_NotaDeb_Bus();
        fa_notaCreDeb_x_fa_factura_NotaDeb_List List_cruce = new fa_notaCreDeb_x_fa_factura_NotaDeb_List();
        fa_TipoNota_x_Empresa_x_Sucursal_Bus bus_tipo_nota_x_sucursal = new fa_TipoNota_x_Empresa_x_Sucursal_Bus();
        fa_TipoNota_x_Empresa_x_Sucursal_Bus bus_nota_x_empresa_sucursal = new fa_TipoNota_x_Empresa_x_Sucursal_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        fa_notaCreDeb_List Lista_Factura = new fa_notaCreDeb_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";

        public object UploadControlSettingsND { get; private set; }
        #endregion
        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeDebitoFacturacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeDebitoFacturacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_NotaDebitoFacturacion(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_nota.get_list_academico(IdEmpresa, IdSucursal, ViewBag.Fecha_ini, ViewBag.Fecha_fin, "D");
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_NotaDebitoFacturacion", model);
        }
        #endregion
        #region Combo bajo demanda Alumno
        public ActionResult Cmb_AlumnoND()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumnoND", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        #endregion
        #region Metodos ComboBox bajo demanda cliente
        public ActionResult CmbCliente_NotaDebito()
        {
            decimal model = new decimal();
            return PartialView("_CmbCliente_NotaDebitoFacturacion", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.CLIENTE.ToString());
        }
        #endregion
        #region Metodos ComboBox bajo demanda producto
        public ActionResult ChangeValuePartial(decimal value = 0)
        {
            return PartialView("_CmbProducto_NotaDebitoFacturacion", value);
        }
        public ActionResult CmbProducto_NotaDebito()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_NotaDebitoFacturacion", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            List<in_Producto_Info> Lista = bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.FAC, 0, 0);
            return Lista;
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Json
        public JsonResult cargar_contactos(decimal IdCliente = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            fa_cliente_Info info_cliente = bus_cliente.get_info(IdEmpresa, IdCliente);
            fa_cliente_contactos_Info info_contacto = bus_contacto.get_info(IdEmpresa, IdCliente, info_cliente.IdContacto);
            var resultado = info_cliente.info_persona.pe_nombreCompleto +" "+ info_contacto.Direccion + " " + info_contacto.Correo + " " + info_contacto.Telefono + " " + info_contacto.Celular;
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarPuntosDeVenta(int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_punto_venta.get_list_x_tipo_doc(IdEmpresa, IdSucursal, cl_enumeradores.eTipoDocumento.NTDB.ToString());
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLotesPorProducto(int IdSucursal = 0, int IdPuntoVta = 0, decimal IdProducto = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var resultado = bus_producto.get_info(IdEmpresa, IdProducto);
            if (resultado == null)
                resultado = new in_Producto_Info();

            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (punto_venta != null)
            {
                if (resultado.IdProducto_padre > 0)
                    List_producto.set_list(bus_producto.get_list_stock_lotes(IdEmpresa, IdSucursal, Convert.ToInt32(punto_venta.IdBodega), Convert.ToDecimal(resultado.IdProducto_padre)));
            }
            else
                List_producto.set_list(new List<in_Producto_Info>());
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_info_cliente(decimal IdCliente = 0, int IdSucursal = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
            fa_cliente_Info resultado = bus_cliente.get_info(IdEmpresa, IdCliente);
            if (resultado == null)
            {
                resultado = new fa_cliente_Info
                {
                    info_persona = new tb_persona_Info()
                };
            }
            else
            {
                var vendedor = bus_v_x_c.get_info(IdEmpresa, IdCliente, IdSucursal);
                if (vendedor != null)
                    resultado.IdVendedor = vendedor.IdVendedor;
                else
                    resultado.IdVendedor = 1;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUltimoDocumento(int IdSucursal = 0, int IdPuntoVta = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_sis_Documento_Tipo_Talonario_Info resultado;
            var punto_venta = bus_punto_venta.get_info(IdEmpresa, IdSucursal, IdPuntoVta);
            if (punto_venta != null)
            {
                resultado = bus_talonario.GetUltimoNoUsado(IdEmpresa, cl_enumeradores.eTipoDocumento.NTDB.ToString(), punto_venta.Su_CodigoEstablecimiento, punto_venta.cod_PuntoVta, punto_venta.EsElectronico, false);
            }
            else
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            if (resultado == null)
                resultado = new tb_sis_Documento_Tipo_Talonario_Info();
            return Json(new { data_puntovta = punto_venta, data_talonario = resultado }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDocumentosPorCobrar(int IdSucursal = 0, decimal IdCliente = 0, decimal IdAlumno = 0, decimal IdTransaccionSession = 0)
        {
            bool resultado = true;
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var List = List_cruce.get_list(IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            var ListPorCruzar = bus_cruce.get_list_cartera_academico(IdEmpresa, IdSucursal, IdCliente, IdAlumno, false);

            foreach (var item in List)
            {
                ListPorCruzar.Remove(ListPorCruzar.Where(q => q.secuencial == item.secuencial).FirstOrDefault());
            }

            List.AddRange(ListPorCruzar);
            List_cruce.set_list(List, IdTransaccionSession);

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VaciarListas(decimal IdTransaccionSession = 0)
        {
            bool resultado = true;
            List_cruce.set_list(new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>(), IdTransaccionSession);
            List_det.set_list(new List<fa_notaCreDeb_det_Info>(), IdTransaccionSession);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutorizarSRI(int IdEmpresa, int IdSucursal, int IdBodega, decimal IdNota)
        {
            string retorno = string.Empty;

            if (bus_nota.modificarEstadoAutorizacion(IdEmpresa, IdSucursal, IdBodega, IdNota))
                retorno = "Autorización exitosa";


            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetCliente(int IdEmpresa = 0, decimal IdAlumno = 0)
        {
            decimal IdCliente = 0;
            var info_familia = bus_familia.GetInfo_Representante(IdEmpresa, IdAlumno, cl_enumeradores.eTipoRepresentante.ECON.ToString());
            var info_cliente = bus_cliente.get_info_x_num_cedula(IdEmpresa, (info_familia == null ? "" : info_familia.pe_cedulaRuc));
            IdCliente = (info_cliente == null ? 0 : info_cliente.IdCliente);

            return Json(IdCliente, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SetValores(double SubtotalConDscto = 0, string IdCod_Impuesto_IVA = "")
        {
            double ValorIVA = 0;
            double ValorTotal = 0;
            var info_impuesto = bus_impuesto.get_info(IdCod_Impuesto_IVA);

            ValorIVA = Math.Round((SubtotalConDscto * (info_impuesto.porcentaje / 100)), 2, MidpointRounding.AwayFromZero);
            ValorTotal = Math.Round((SubtotalConDscto + ValorIVA), 2, MidpointRounding.AwayFromZero);

            return Json(new { IVA = ValorIVA, Total = ValorTotal }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Grillas de cruce
        public ActionResult GridViewPartial_CruceND_x_cruzar()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == false).ToList();
            return PartialView("_GridViewPartial_CruceND_x_cruzar", model);
        }

        public ActionResult GridViewPartial_CruceND()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_CruceND", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNewFacturas(string IDs = "", decimal IdTransaccionSession = 0)
        {
            if (IDs != "")
            {
                string[] array = IDs.Split(',');
                foreach (var item in array)
                {
                    List_cruce.DeleteRow(item, IdTransaccionSession);
                }
            }
            var list = List_cruce.get_list(IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            var lst_det = new List<fa_notaCreDeb_det_Info>();
            foreach (var item in list)
            {
                lst_det.AddRange(bus_det.get_list(item.IdEmpresa_fac_nd_doc_mod, item.IdSucursal_fac_nd_doc_mod, item.IdBodega_fac_nd_doc_mod, item.IdCbteVta_fac_nd_doc_mod, item.vt_tipoDoc));
            }
            List_det.set_list(lst_det, IdTransaccionSession);
            var model = list;
            return PartialView("_GridViewPartial_CruceND", model);
        }

        public ActionResult EditingUpdateFactura([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_x_fa_factura_NotaDeb_Info info_det)
        {
            List_cruce.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_CruceND", model);
        }
        public ActionResult EditingDeleteFactura(string secuencial)
        {
            List_cruce.DeleteRow(secuencial, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_cruce.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)).Where(q => q.seleccionado == true).ToList();
            return PartialView("_GridViewPartial_CruceND", model);
        }

        #endregion
        #region funciones del detalle

        public ActionResult GridViewPartial_LoteDebitoFacturacion()
        {
            var model = List_producto.get_list();
            return PartialView("_GridViewPartial_LoteNotaDebitoFacturacion", model);
        }

        private void cargar_combos_detalle()
        {
            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_notaDebito_det()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (ModelState.IsValid)
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] fa_notaCreDeb_det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(Session["IdEmpresa"]);
            if (ModelState.IsValid)
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            List_det.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_NotaDebitoFacturacion_det", model);
        }
        #endregion
        #region Metodos
        private void cargar_combos(fa_notaCreDeb_Info model)
        {
            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_punto_venta = bus_punto_venta.get_list(model.IdEmpresa, model.IdSucursal, false);
            ViewBag.lst_punto_venta = lst_punto_venta;

            var lst_contacto = bus_contacto.get_list(model.IdEmpresa, model.IdCliente);
            ViewBag.lst_contacto = lst_contacto;

            Dictionary<string, string> lst_naturaleza = new Dictionary<string, string>();
            lst_naturaleza.Add("INT", "INTERNO");
            lst_naturaleza.Add("SRI", "SRI");
            ViewBag.lst_naturaleza = lst_naturaleza;

            var lst_tipo_nota = bus_tipo_nota.get_list(model.IdEmpresa, "D", false);
            ViewBag.lst_tipo_nota = lst_tipo_nota;

            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }
        private bool validar(fa_notaCreDeb_Info info, ref string msg)
        {
            info.lst_det = List_det.get_list(info.IdTransaccionSession);
            info.lst_cruce = List_cruce.get_list(info.IdTransaccionSession).Where(q => q.seleccionado == true).ToList();
            if (!bus_periodo.ValidarFechaTransaccion(info.IdEmpresa, info.no_fecha, cl_enumeradores.eModulo.FAC, info.IdSucursal, ref msg))
            {
                return false;
            }

            info.IdBodega = (int)bus_punto_venta.get_info(info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info.IdPuntoVta)).IdBodega;
            info.IdUsuario = SessionFixed.IdUsuario;
            info.IdUsuarioUltMod = SessionFixed.IdUsuario;
            var tipo_nota = bus_tipo_nota_x_sucursal.get_info(info.IdEmpresa, info.IdTipoNota, info.IdSucursal);
            if (tipo_nota != null)
                info.IdCtaCble_TipoNota = tipo_nota.IdCtaCble;

            if (info.IdNota == 0 && info.NaturalezaNota == "SRI")
            {
                var pto_vta = bus_punto_venta.get_info(info.IdEmpresa, info.IdSucursal, Convert.ToInt32(info.IdPuntoVta));
                if (pto_vta.EsElectronico == false)
                {
                    var talonario = bus_talonario.get_info(info.IdEmpresa, info.CodDocumentoTipo, info.Serie1, info.Serie2, info.NumNota_Impresa);
                    if (talonario == null)
                    {
                        msg = "No existe un talonario creado con la numeración: " + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa;
                        return false;
                    }
                    if (talonario.Usado == true)
                    {
                        msg = "El talonario: " + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa + " se encuentra utilizado.";
                        return false;
                    }
                    if (bus_nota.DocumentoExiste(info.IdEmpresa, info.CodDocumentoTipo, info.Serie1, info.Serie2, info.NumNota_Impresa))
                    {
                        msg = "Existe una nota de débito con el talonario: " + info.Serie1 + "-" + info.Serie2 + "-" + info.NumNota_Impresa + " utilizado.";
                        return false;
                    }
                    if (talonario.es_Documento_Electronico == false)
                    {
                        info.NumAutorizacion = talonario.NumAutorizacion;
                    }
                }
            }

            if (info.NaturalezaNota != "SRI")
            {
                info.Serie1 = null;
                info.Serie2 = null;
                info.NumNota_Impresa = null;
            }

            #region Resumen
            var info_anio = bus_anio.GetInfo_AnioEnCurso(info.IdEmpresa,0);
            var info_matricula = bus_matricula.GetInfo_ExisteMatricula(info.IdEmpresa,(info_anio==null ? 0 : info_anio.IdAnio),Convert.ToDecimal(info.IdAlumno));
            var info_ImpuestoIVA = bus_impuesto.get_info(info.info_resumen.IdCod_Impuesto_IVA);
            var Descuento = 0;
            var ValorIVA = Math.Round(info.info_resumen.ValorIVA, 2, MidpointRounding.AwayFromZero);
            var SubtotalIVASinDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
            var SubtotalSinIVASinDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
            var SubtotalIVAConDscto = Math.Round((info_ImpuestoIVA.porcentaje > 0 ? info.info_resumen.SubtotalConDscto : 0), 2, MidpointRounding.AwayFromZero);
            var SubtotalSinIVAConDscto = Math.Round((info_ImpuestoIVA.porcentaje == 0 ? info.info_resumen.SubtotalConDscto : 0), 2, MidpointRounding.AwayFromZero);
            var SubtotalSinDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
            var SubtotalConDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
            var Total = Math.Round(info.info_resumen.Total, 2, MidpointRounding.AwayFromZero);
            decimal PorIVA = Convert.ToDecimal(info_ImpuestoIVA.porcentaje);

            info.info_resumen = new fa_notaCreDeb_resumen_Info
            {
                IdEmpresa = info.IdEmpresa,
                IdSucursal = info.IdSucursal,
                IdBodega = info.IdBodega,
                IdNota = info.IdNota,
                SubtotalIVASinDscto = SubtotalIVASinDscto,
                SubtotalSinIVASinDscto = SubtotalSinIVASinDscto,
                SubtotalSinDscto = SubtotalSinDscto,
                Descuento = Descuento,
                SubtotalIVAConDscto = SubtotalIVAConDscto,
                SubtotalSinIVAConDscto = SubtotalSinIVAConDscto,
                SubtotalConDscto = SubtotalConDscto,
                PorIva = PorIVA,
                ValorIVA = ValorIVA,
                Total = Total,
                IdCod_Impuesto_IVA = info_ImpuestoIVA.IdCod_Impuesto,
                IdAnio=(info_anio==null ? (int?)null : info_anio.IdAnio),
                IdMatricula = (info_matricula==null ? (decimal?)null : info_matricula.IdMatricula)
            };

            #endregion

            var info_tipo_nota = bus_tipo_nota.get_info(info.IdEmpresa, info.IdTipoNota);

            if (info_tipo_nota != null && info_tipo_nota.IdCtaCble != null && info_tipo_nota.IdCtaCbleCXC != null && info_tipo_nota.IdProducto != null)
            {
                var info_detalle = new fa_notaCreDeb_det_Info
                {
                    IdEmpresa = info.IdEmpresa,
                    IdSucursal = info.IdSucursal,
                    IdBodega = info.IdBodega,
                    IdNota = info.IdNota,
                    Secuencia = 1,
                    IdProducto = Convert.ToDecimal(info_tipo_nota.IdProducto),
                    sc_cantidad = 1,
                    sc_cantidad_factura = 1,
                    sc_Precio = Convert.ToDouble(info.info_resumen.SubtotalConDscto),
                    sc_descUni = 0,
                    sc_PordescUni = 0,
                    sc_precioFinal = Convert.ToDouble(info.info_resumen.SubtotalConDscto),
                    vt_por_iva = Convert.ToDouble(info.info_resumen.PorIva),
                    sc_iva = Convert.ToDouble(info.info_resumen.ValorIVA),
                    IdCod_Impuesto_Iva = info.info_resumen.IdCod_Impuesto_IVA,
                    sc_subtotal = Convert.ToDouble(info.info_resumen.SubtotalConDscto),
                    sc_total = Convert.ToDouble(info.info_resumen.Total),
                    IdCentroCosto = null,
                    IdPunto_Cargo = null,
                    IdPunto_cargo_grupo = null
                };
                info.lst_det = new List<fa_notaCreDeb_det_Info>();
                info.lst_det.Add(info_detalle);
            }
            else
            {
                msg = "Faltan parámetros por configurar en el tipo de nota";
                return false;
            }

            #region Cliente
            var infoRepEconomico = bus_familia.GetInfo_Representante(info.IdEmpresa, Convert.ToDecimal(info.IdAlumno), cl_enumeradores.eTipoRepresentante.ECON.ToString());
            var info_cliente = bus_cliente.get_info_x_num_cedula(info.IdEmpresa, infoRepEconomico.pe_cedulaRuc);
            if (info_cliente == null || info_cliente.IdCliente == 0)
            {
                msg = "El alumno no tiene asigando un cliente (persona a la que se factura).";
                return false;
            }
            info.IdCliente = info_cliente.IdCliente;
            #endregion

            return true;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeDebitoFacturacion", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            fa_notaCreDeb_Info model = new fa_notaCreDeb_Info
            {
                IdEmpresa = IdEmpresa,
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                no_fecha = DateTime.Now,
                no_fecha_venc = DateTime.Now,
                lst_det = new List<fa_notaCreDeb_det_Info>(),
                lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>(),
                CodDocumentoTipo = "NTDB",
                CreDeb = "D",
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            model.info_resumen = new fa_notaCreDeb_resumen_Info();
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_notaCreDeb_Info model)
        {
            var nota = bus_nota_x_empresa_sucursal.get_info(model.IdEmpresa, model.IdTipoNota, model.IdSucursal);
            if (nota != null)
            {
                if (nota.IdCtaCble == null | nota.IdCtaCble == "")
                {
                    ViewBag.mensaje = "No existe cuenta contable para el tipo de nota de credito";
                    cargar_combos(model);
                    return View(model);
                }
            }
            if (!validar(model, ref mensaje))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_nota.guardarDB(model))
            {
                List_det.set_list(List_det.get_list(model.IdTransaccionSession), model.IdTransaccionSession);
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model);
                return View(model);
            };

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdBodega = model.IdBodega, IdNota = model.IdNota, Exito = true });
        }
        public ActionResult Consultar(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            fa_notaCreDeb_Info model = bus_nota.get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeDebitoFacturacion", "Index");
            if (model.Estado == "I")
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.lst_cruce = bus_cruce.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.no_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeDebitoFacturacion", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            fa_notaCreDeb_Info model = bus_nota.get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.lst_cruce = bus_cruce.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.no_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_notaCreDeb_Info model)
        {
            var nota = bus_nota_x_empresa_sucursal.get_info(model.IdEmpresa, model.IdTipoNota, model.IdSucursal);
            if (nota != null)
            {
                if (nota.IdCtaCble == null | nota.IdCtaCble == "")
                {
                    ViewBag.mensaje = "No existe cuenta contable para el tipo de nota de credito";
                    cargar_combos(model);
                    return View(model);
                }
            }
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_nota.modificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos(model);
                return View(model);
            };

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSucursal = model.IdSucursal, IdBodega = model.IdBodega, IdNota = model.IdNota, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdSucursal = 0, int IdBodega = 0, decimal IdNota = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeDebitoFacturacion", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            fa_notaCreDeb_Info model = bus_nota.get_info(IdEmpresa, IdSucursal, IdBodega, IdNota);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det = bus_det.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_det.set_list(model.lst_det, model.IdTransaccionSession);
            model.lst_cruce = bus_cruce.get_list(IdEmpresa, IdSucursal, IdBodega, IdNota);
            List_cruce.set_list(model.lst_cruce, model.IdTransaccionSession);
            cargar_combos(model);
            #region Validacion Periodo
            ViewBag.MostrarBoton = true;
            if (!bus_periodo.ValidarFechaTransaccion(IdEmpresa, model.no_fecha, cl_enumeradores.eModulo.FAC, model.IdSucursal, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                ViewBag.MostrarBoton = false;
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_notaCreDeb_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_nota.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos(model);
                return View(model);
            };
            return RedirectToAction("Index");
        }
        #endregion

        #region Importacion
        public ActionResult UploadControlUploadImp()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", Core.Web.Areas.Facturacion.Controllers.UploadControlSettingsND.UploadValidationSettings, Core.Web.Areas.Facturacion.Controllers.UploadControlSettingsND.FileUploadComplete);
            return null;
        }
        public ActionResult Importar(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            fa_notaCreDeb_Info model = new fa_notaCreDeb_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Importar(fa_notaCreDeb_Info model)
        {
            try
            {
                var ListaFactura = Lista_Factura.get_list(model.IdTransaccionSession);

                foreach (var item in ListaFactura)
                {
                    if (item.IdCliente != 0)
                    {
                        if (!bus_nota.guardarDB(item))
                        {
                            ViewBag.mensaje = "Error al importar el archivo";
                            return View(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //SisLogError.set_list((ex.InnerException) == null ? ex.Message.ToString() : ex.InnerException.ToString());
                ViewBag.error = ex.Message.ToString();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult GridViewPartial_Factura_importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_Factura.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Factura_importacion", model);
        }

        public JsonResult ActualizarVariablesSession(int IdEmpresa = 0, decimal IdTransaccionSession = 0)
        {
            string retorno = string.Empty;
            SessionFixed.IdEmpresa = IdEmpresa.ToString();
            SessionFixed.IdTransaccionSession = IdTransaccionSession.ToString();
            SessionFixed.IdTransaccionSessionActual = IdTransaccionSession.ToString();
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class UploadControlSettingsND
    {
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            fa_notaCreDeb_List ListaFactura = new fa_notaCreDeb_List();
            List<fa_notaCreDeb_Info> Lista_Factura = new List<fa_notaCreDeb_Info>();
            fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
            fa_cliente_contactos_Bus bus_cliente_contatos = new fa_cliente_contactos_Bus();
            tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
            fa_parametro_Bus bus_fa_parametro = new fa_parametro_Bus();
            fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
            tb_bodega_Bus bus_bodega = new tb_bodega_Bus();
            aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
            aca_Familia_Bus bus_familia = new aca_Familia_Bus();
            tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
            fa_PuntoVta_Bus bus_ptoventa = new fa_PuntoVta_Bus();
            ct_plancta_Bus bus_cuenta = new ct_plancta_Bus();
            int cont = 0;
            int IdNota = 1;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region Saldo Fact      
                var info_fa_parametro = bus_fa_parametro.get_info(IdEmpresa);
                var IdTipoNota = 2; //2 Saldo inicial NTDB -3 Saldo inicial NTCR
                var infoTipoNota = bus_tipo_nota.get_info(IdEmpresa, IdTipoNota);

                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        var CreDeb = Convert.ToString(reader.GetValue(10)).Trim();
                        var CedAlu = Convert.ToString(reader.GetValue(2)).Trim();
                        var CedCli = Convert.ToString(reader.GetValue(1)).Trim();
                        var Su_CodigoEstablecimiento = Convert.ToString(reader.GetValue(0)).Trim();
                        var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
                        var IdSucursal = lst_sucursal.Where(q => q.Su_CodigoEstablecimiento == Su_CodigoEstablecimiento).FirstOrDefault().IdSucursal;
                        var info_ptoventa = bus_ptoventa.get_list_x_tipo_doc(IdEmpresa, IdSucursal, (CreDeb=="D" ? "NTDB": "NTCR") ).FirstOrDefault();
                        var InfoCliente = bus_cliente.get_info_x_num_cedula(IdEmpresa, CedCli);
                        var info_Alumno = bus_alumno.get_info_x_num_cedula(IdEmpresa, CedAlu);
                        var infoBodega = bus_bodega.get_info(IdEmpresa, IdSucursal, 1);
                        var infoCtaCble = bus_cuenta.get_info(IdEmpresa, Convert.ToString(reader.GetValue(9)).Trim());                      
                        var InfoContactosCliente = bus_cliente_contatos.get_info(IdEmpresa, InfoCliente.IdCliente, 1);

                        fa_notaCreDeb_Info info = new fa_notaCreDeb_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdSucursal = IdSucursal,
                            IdBodega = infoBodega.IdBodega,
                            IdNota = IdNota++,
                            dev_IdEmpresa = null,
                            dev_IdDev_Inven = null,
                            CodNota = Convert.ToString(reader.GetValue(3)).Trim(),
                            CreDeb = CreDeb,
                            CodDocumentoTipo = (CreDeb == "D" ? "NTDB" : "NTCR"),
                            Serie1 = null,
                            Serie2 = null,
                            NumNota_Impresa = null,
                            NumAutorizacion = null,
                            Fecha_Autorizacion = null,
                            IdCliente = (InfoCliente.IdCliente == 0 ? 0 : InfoCliente.IdCliente),
                            no_fecha = Convert.ToDateTime(reader.GetValue(6)),
                            no_fecha_venc = Convert.ToDateTime(reader.GetValue(7)),
                            IdTipoNota = infoTipoNota.IdTipoNota,
                            sc_observacion = Convert.ToString(reader.GetValue(8)).Trim() == "" ? ("DOCUMENTO #" + Convert.ToString(reader.GetValue(3)) + " CLIENTE: " + InfoCliente.info_persona.pe_nombreCompleto) : Convert.ToString(reader.GetValue(8)),
                            IdUsuario = SessionFixed.IdUsuario,
                            NaturalezaNota = null,
                            //IdCtaCble_TipoNota = infoTipoNota.IdCtaCble,
                            IdCtaCble_TipoNota = (infoCtaCble == null ? null : infoCtaCble.IdCtaCble),
                            IdPuntoVta = info_ptoventa.IdPuntoVta,
                            aprobada_enviar_sri = false,
                            IdAlumno = (info_Alumno.IdAlumno == 0 ? 0 : info_Alumno.IdAlumno),
                            NomAlumno = (info_Alumno.IdAlumno==0 ? "" : info_Alumno.pe_nombreCompleto),
                            NomCliente = (InfoCliente.IdCliente == 0 ? "" : InfoCliente.info_persona.pe_nombreCompleto),
                            CedulaAlumno = (info_Alumno.IdAlumno == 0 ? CedAlu : info_Alumno.pe_cedulaRuc),
                            CedulaCliente = (InfoCliente.IdCliente == 0 ? CedCli : InfoCliente.info_persona.pe_cedulaRuc),
                            NomCuenta = (infoCtaCble == null ? "" : infoCtaCble.pc_Cuenta)
                        };

                        info.lst_det = new List<fa_notaCreDeb_det_Info>();
                        info.lst_cruce = new List<fa_notaCreDeb_x_fa_factura_NotaDeb_Info>();

                        fa_notaCreDeb_det_Info info_detalle = new fa_notaCreDeb_det_Info
                        {
                            IdEmpresa = IdEmpresa,
                            IdSucursal = IdSucursal,
                            IdBodega = info.IdBodega,
                            IdNota = info.IdNota,
                            IdProducto = 1,
                            sc_cantidad = 1,
                            sc_Precio = Convert.ToDouble(reader.GetValue(5)),
                            sc_descUni = 0,
                            sc_PordescUni = 0,
                            sc_precioFinal = Convert.ToDouble(reader.GetValue(5)),
                            sc_subtotal = Convert.ToDouble(reader.GetValue(5)),
                            sc_iva = 0,
                            sc_total = Convert.ToDouble(reader.GetValue(5)),
                            sc_costo = 0,
                            sc_observacion = Convert.ToString(reader.GetValue(8)).Trim(),
                            vt_por_iva = 0,
                            IdPunto_Cargo = null,
                            IdPunto_cargo_grupo = null,
                            IdCod_Impuesto_Iva = "IVA0",
                            IdCentroCosto = null,
                            sc_cantidad_factura = null
                        };

                        info.lst_det.Add(info_detalle);

                        #region Resumen
                        info.info_resumen = new fa_notaCreDeb_resumen_Info();
                        info.info_resumen.SubtotalConDscto = Convert.ToDecimal(info_detalle.sc_total);
                        var info_ImpuestoIVA = bus_impuesto.get_info("IVA0");
                        var Descuento = 0;
                        var ValorIVA = Math.Round(info.info_resumen.ValorIVA, 2, MidpointRounding.AwayFromZero);
                        var SubtotalIVASinDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
                        var SubtotalSinIVASinDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
                        var SubtotalIVAConDscto = Math.Round((info_ImpuestoIVA.porcentaje > 0 ? info.info_resumen.SubtotalConDscto : 0), 2, MidpointRounding.AwayFromZero);
                        var SubtotalSinIVAConDscto = Math.Round((info_ImpuestoIVA.porcentaje == 0 ? info.info_resumen.SubtotalConDscto : 0), 2, MidpointRounding.AwayFromZero);
                        var SubtotalSinDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
                        var SubtotalConDscto = Math.Round(info.info_resumen.SubtotalConDscto, 2, MidpointRounding.AwayFromZero);
                        var Total = Math.Round(Convert.ToDecimal(info_detalle.sc_total), 2, MidpointRounding.AwayFromZero);
                        decimal PorIVA = Convert.ToDecimal(info_ImpuestoIVA.porcentaje);

                        info.info_resumen = new fa_notaCreDeb_resumen_Info
                        {
                            IdEmpresa = info.IdEmpresa,
                            IdSucursal = info.IdSucursal,
                            IdBodega = info.IdBodega,
                            IdNota = info.IdNota,
                            SubtotalIVASinDscto = SubtotalIVASinDscto,
                            SubtotalSinIVASinDscto = SubtotalSinIVASinDscto,
                            SubtotalSinDscto = SubtotalSinDscto,
                            Descuento = Descuento,
                            SubtotalIVAConDscto = SubtotalIVAConDscto,
                            SubtotalSinIVAConDscto = SubtotalSinIVAConDscto,
                            SubtotalConDscto = SubtotalConDscto,
                            PorIva = PorIVA,
                            ValorIVA = ValorIVA,
                            Total = Total,
                            IdCod_Impuesto_IVA = info_ImpuestoIVA.IdCod_Impuesto
                        };
                        #endregion

                        Lista_Factura.Add(info);
                    }
                    else
                        cont++;
                }
                ListaFactura.set_list(Lista_Factura, IdTransaccionSession);
                #endregion
            }
        }
    }
    public class fa_notaCreDeb_List
    {
        string Variable = "fa_notaCreDeb_Info";
        public List<fa_notaCreDeb_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_notaCreDeb_Info> list = new List<fa_notaCreDeb_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_notaCreDeb_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_notaCreDeb_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}