using Core.Bus.Academico;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Bus.Inventario;
using Core.Data.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Info.Inventario;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class PlantillaController : Controller
    {
        #region Variables
        aca_Rubro_Bus bus_rubro = new aca_Rubro_Bus();
        aca_AnioLectivo_Rubro_Bus bus_rubro_anio = new aca_AnioLectivo_Rubro_Bus();
        aca_AnioLectivo_Rubro_Periodo_Bus bus_rubro_anio_periodo = new aca_AnioLectivo_Rubro_Periodo_Bus();
        aca_Plantilla_List Lista_Plantilla = new aca_Plantilla_List();
        aca_Plantilla_Rubro_List Lista_PlantillaRubro = new aca_Plantilla_Rubro_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Plantilla_Bus bus_plantilla = new aca_Plantilla_Bus();
        aca_Plantilla_Rubro_Bus bus_plantilla_rubro = new aca_Plantilla_Rubro_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
        aca_PlantillaTipo_Bus bus_tipo_plantilla = new aca_PlantillaTipo_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult ChangeValuePartial(decimal value = 0)
        {
            return PartialView("_CmbProducto_Plantilla", value);
        }
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_Plantilla_Info());
        }

        public ActionResult CmbProducto_Plantilla()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_Plantilla", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.ACA, Convert.ToInt32(SessionFixed.IdSucursal));
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }

        public ActionResult CmbRubro_Plantilla()
        {
            decimal model = new decimal();
            return PartialView("_CmbRubro_Plantilla", model);
        }

        public List<aca_AnioLectivo_Rubro_Info> get_list_bajo_demandaRubro(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            int IdAnio = Convert.ToInt32(SessionFixed.IdAnio);
            return bus_rubro_anio.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), IdAnio);
        }
        public aca_AnioLectivo_Rubro_Info get_info_bajo_demandaRubro(ListEditItemRequestedByValueEventArgs args)
        {
            int IdAnio = Convert.ToInt32(SessionFixed.IdAnio);
            return bus_rubro_anio.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), IdAnio);
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_Plantilla_Info model = new aca_Plantilla_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>()
            };

            List<aca_Plantilla_Info> lista = bus_plantilla.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_Plantilla.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Plantilla", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Plantilla_Info model)
        {
            List<aca_Plantilla_Info> lista = bus_plantilla.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_Plantilla.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Plantilla", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Plantilla(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Plantilla_Info> model = Lista_Plantilla.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_Plantilla", model);
        }
        #endregion

        #region Json
        public JsonResult Set_Anio(int IdAnio = 0)
        {
            SessionFixed.IdAnio = IdAnio.ToString();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcularValores(double Valor = 0, string IdCodImpuesto = "")
        {
            double iva_porc = 0;
            double iva = 0;
            double total = 0;

            var impuesto = bus_impuesto.get_info(IdCodImpuesto);
            if (impuesto != null)
                iva_porc = impuesto.porcentaje;

            iva = Math.Round((Valor * (iva_porc / 100)), 2);
            total = Math.Round((Valor + iva), 2);

            return Json(new { iva = iva, total = total }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetPrecioProducto(decimal IdProducto = 0)
        {
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var producto = bus_producto.get_info(IdEmpresa, IdProducto);
            string IdCod_Impuesto_Iva = "";
            double iva_porc = 0;
            double iva = 0;
            double PrecioProd = 0;
            double TotalIProd = 0;

            if (producto == null)
                producto = new in_Producto_Info();

            IdCod_Impuesto_Iva = producto.IdCod_Impuesto_Iva;
            PrecioProd = producto.precio_1;

            var impuesto = bus_impuesto.get_info(producto.IdCod_Impuesto_Iva);
            if (impuesto != null)
                iva_porc = impuesto.porcentaje;

            iva = Math.Round((PrecioProd * (iva_porc / 100)), 2);
            TotalIProd = Math.Round((PrecioProd + iva), 2);

            return Json(new { Precio = PrecioProd, IdCodImpuesto = IdCod_Impuesto_Iva, Total = TotalIProd }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetProductoPorRubro(int IdAnio=0, int IdRubro = 0)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var rubro_periodo = bus_rubro_anio.GetInfo(IdEmpresa,IdAnio,IdRubro);
            decimal data = 0;
            if (rubro_periodo!= null)
            {
                data = rubro_periodo.IdProducto;
            }
            return Json( data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Metodos
        private bool validar(aca_Plantilla_Info info, ref string msg)
        {
            if (info.lst_Plantilla_Rubro.Count == 0)
            {
                msg = "Debe ingresar al menos un registro en el detalle";
                return false;
            }

            return true;
        }

        private void cargar_combos()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_anio = bus_anio.GetList(IdEmpresa, false);
            ViewBag.lst_anio = lst_anio;

            Dictionary<string, string> lst_tipo_desc = new Dictionary<string, string>();
            lst_tipo_desc.Add("$", "$ Monto");
            lst_tipo_desc.Add("%", "% Porcentaje");
            ViewBag.lst_tipo_desc = lst_tipo_desc;

            var lst_tipo_nota = bus_tipo_nota.get_list(IdEmpresa, "C", false);
            ViewBag.lst_tipo_nota = lst_tipo_nota;

            var lst_tipo_plantilla = bus_tipo_plantilla.GetList(IdEmpresa, false);
            ViewBag.lst_tipo_plantilla = lst_tipo_plantilla;
        }

        #endregion

        #region funciones del detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_PlantillaRubro()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Plantilla_Rubro_Info> model = Lista_PlantillaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_PlantillaRubro", model);
        }

        private void cargar_combos_detalle()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            Dictionary<string, string> lst_tipo_desc = new Dictionary<string, string>();
            lst_tipo_desc.Add("$", "$ Monto");
            lst_tipo_desc.Add("%", "% Porcentaje");
            ViewBag.lst_tipo_desc = lst_tipo_desc;

            var lst_tipo_nota = bus_tipo_nota.get_list(IdEmpresa, "C", false);
            ViewBag.lst_tipo_nota = lst_tipo_nota;

            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] aca_Plantilla_Rubro_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdAnio = Convert.ToInt32(SessionFixed.IdAnio);
            var Lista = Lista_PlantillaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            if (Lista.Where(q => q.IdRubro == info_det.IdRubro).ToList().Count == 0)
            {
                var info_impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto_Iva);
                var info_rubro = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, info_det.IdRubro);
                if (info_rubro != null)
                {
                    info_det.IdAnio = IdAnio;
                    info_det.IdEmpresa = IdEmpresa;
                    info_det.NomRubro = info_rubro.NomRubro;
                    //info_det.IdProducto = info_rubro.IdProducto;
                    //info_det.Subtotal = info_rubro.Subtotal;
                    //info_det.IdCod_Impuesto_Iva = info_rubro.IdCod_Impuesto_Iva;
                    //info_det.ValorIVA = info_rubro.ValorIVA;
                    //info_det.Porcentaje = info_rubro.Porcentaje;
                    //info_det.Total = info_rubro.Total;
                }

                if (info_det.IdProducto != 0)
                {
                    var info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto != null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion;
                    }
                }

                if(info_impuesto!=null)
                    info_det.Porcentaje = Convert.ToDecimal(info_impuesto.porcentaje);
                info_det.ValorIVA = (info_det.Subtotal * (info_det.Porcentaje/100));
                Lista_PlantillaRubro.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            
            var model = Lista_PlantillaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_PlantillaRubro", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_Plantilla_Rubro_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdAnio = Convert.ToInt32(SessionFixed.IdAnio);
            if (info_det != null)
            {
                var info_impuesto = bus_impuesto.get_info(info_det.IdCod_Impuesto_Iva);

                if (info_det.IdRubro != 0)
                {
                    var info_rubro = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, info_det.IdRubro);
                    if (info_rubro != null)
                    {
                        info_det.IdAnio = IdAnio;
                        info_det.IdEmpresa = IdEmpresa;
                        info_det.NomRubro = info_rubro.NomRubro;
                        //info_det.IdProducto = info_rubro.IdProducto;
                        //info_det.Subtotal = info_rubro.Subtotal;
                        //info_det.IdCod_Impuesto_Iva = info_rubro.IdCod_Impuesto_Iva;
                        //info_det.ValorIVA = info_rubro.ValorIVA;
                        //info_det.Porcentaje = info_rubro.Porcentaje;
                        //info_det.Total = info_rubro.Total;
                    }
                }
                if (info_det.IdProducto != 0)
                {
                    var info_producto = bus_producto.get_info(IdEmpresa, info_det.IdProducto);
                    if (info_producto!=null)
                    {
                        info_det.pr_descripcion = info_producto.pr_descripcion;
                    }
                }

                if (info_impuesto != null)
                    info_det.Porcentaje = Convert.ToDecimal(info_impuesto.porcentaje);
                info_det.ValorIVA = (info_det.Subtotal * (info_det.Porcentaje / 100));
            }

            Lista_PlantillaRubro.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_PlantillaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_PlantillaRubro", model);
        }

        public ActionResult EditingDelete(int IdRubro)
        {
            Lista_PlantillaRubro.DeleteRow(IdRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_PlantillaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_PlantillaRubro", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_Plantilla_Info model = new aca_Plantilla_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Plantilla", "Index");
            if (!inf.Nuevo)
                return RedirectToAction("Index");
            #endregion
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_Plantilla_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.AplicaParaTodo = (model.AplicaParaTodo==null ? false : model.AplicaParaTodo);
            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = Lista_PlantillaRubro.get_list(Convert.ToDecimal(model.IdTransaccionSession));

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            if (model.AplicaParaTodo==true)
            {
                foreach (var item in model.lst_Plantilla_Rubro)
                {
                    item.IdTipoNota_descuentoDet = model.IdTipoNota;
                    item.Valor_descuentoDet = model.Valor;
                    item.TipoDescuento_descuentoDet = model.TipoDescuento;
                }
            }

            if (!bus_plantilla.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, IdPlantilla = model.IdPlantilla, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio=0, int IdPlantilla = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Plantilla_Info model = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
            
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Plantilla", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetList(model.IdEmpresa, model.IdAnio, model.IdPlantilla);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_Plantilla_Info model)
        {
            model.AplicaParaTodo = (model.AplicaParaTodo == null ? false : model.AplicaParaTodo);
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = Lista_PlantillaRubro.get_list(Convert.ToDecimal(model.IdTransaccionSession));

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            if (model.AplicaParaTodo == true)
            {
                foreach (var item in model.lst_Plantilla_Rubro)
                {
                    item.IdTipoNota_descuentoDet = model.IdTipoNota;
                    item.Valor_descuentoDet = model.Valor;
                    item.TipoDescuento_descuentoDet = model.TipoDescuento;
                }
            }

            if (!bus_plantilla.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, IdPlantilla = model.IdPlantilla, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAnio = 0, int IdPlantilla=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Plantilla_Info model = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Plantilla", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetList(model.IdEmpresa, model.IdAnio, model.IdPlantilla);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Plantilla_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_plantilla.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class aca_Plantilla_List
    {
        string Variable = "aca_Plantilla_Info";
        public List<aca_Plantilla_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Plantilla_Info> list = new List<aca_Plantilla_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Plantilla_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Plantilla_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class aca_Plantilla_Rubro_List
    {
        string Variable = "aca_Plantilla_Rubro_Info";
        aca_AnioLectivo_Rubro_Bus bus_rubro_anio = new aca_AnioLectivo_Rubro_Bus();
        public List<aca_Plantilla_Rubro_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Plantilla_Rubro_Info> list = new List<aca_Plantilla_Rubro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Plantilla_Rubro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Plantilla_Rubro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(aca_Plantilla_Rubro_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            List<aca_Plantilla_Rubro_Info> list = get_list(IdTransaccionSession);
            list.Add(info_det); 
        }

        public void UpdateRow(aca_Plantilla_Rubro_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_Plantilla_Rubro_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdRubro == info_det.IdRubro).FirstOrDefault();
            edited_info.IdRubro = info_det.IdRubro;
            edited_info.Porcentaje = info_det.Porcentaje;
            edited_info.ValorIVA = info_det.ValorIVA;
            edited_info.Total = info_det.Total;
            edited_info.IdCod_Impuesto_Iva = info_det.IdCod_Impuesto_Iva;
            edited_info.Subtotal = info_det.Subtotal;
            edited_info.IdTipoNota_descuentoDet = info_det.IdTipoNota_descuentoDet;
            edited_info.TipoDescuento_descuentoDet = info_det.TipoDescuento_descuentoDet;
            edited_info.Valor_descuentoDet = info_det.Valor_descuentoDet;

            var info_rubro_anio = bus_rubro_anio.GetInfo(info_det.IdEmpresa, info_det.IdAnio, info_det.IdRubro);
        }

        public void DeleteRow(int IdRubro, decimal IdTransaccionSession)
        {
            List<aca_Plantilla_Rubro_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(q => q.IdRubro == IdRubro).FirstOrDefault());
        }
    }
}