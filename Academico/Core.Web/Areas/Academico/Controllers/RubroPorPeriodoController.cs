using Core.Bus.Academico;
using Core.Bus.General;
using Core.Bus.Inventario;
using Core.Data.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Info.Inventario;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class RubroPorPeriodoController : Controller
    {
        #region Variables
        aca_Rubro_Bus bus_rubro = new aca_Rubro_Bus();
        aca_AnioLectivo_Rubro_Bus bus_rubro_anio = new aca_AnioLectivo_Rubro_Bus();
        aca_AnioLectivo_Rubro_Periodo_Bus bus_rubro_anio_periodo = new aca_AnioLectivo_Rubro_Periodo_Bus();
        aca_AnioLectivo_Rubro_List Lista_RubroAnio = new aca_AnioLectivo_Rubro_List();
        aca_AnioLectivo_Rubro_Periodo_List ListaRubroAnioPeriodo = new aca_AnioLectivo_Rubro_Periodo_List();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        string mensaje = string.Empty;
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_rubro = bus_rubro.GetList(IdEmpresa, false);
            ViewBag.lst_rubro = lst_rubro;

            var lst_impuesto = bus_impuesto.get_list("IVA", false);
            ViewBag.lst_impuesto = lst_impuesto;
        }
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_Rubro_Periodo_Info());
        }
        public ActionResult CmbProducto()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORMODULO, cl_enumeradores.eModulo.ACA, Convert.ToInt32(SessionFixed.IdSucursal));
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
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
            aca_AnioLectivo_Rubro_Info model = new aca_AnioLectivo_Rubro_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Rubro_Info> lista = bus_rubro_anio.GetList(model.IdEmpresa,model.IdAnio, true);
            Lista_RubroAnio.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Rubro_Info model)
        {
            List<aca_AnioLectivo_Rubro_Info> lista = bus_rubro_anio.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_RubroAnio.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_RubroPorAnioPorPeriodo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Rubro_Info> model = Lista_RubroAnio.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_RubroPorAnioPorPeriodo", model);
        }
        #endregion

        #region Json
        public JsonResult CalcularValoresProducto(decimal IdProducto = 0)
        {
            string IdCod_Impuesto_Iva = "";
            double precio = 0;
            double PorcentajeIVA = 0;
            double IVA_Valor = 0;
            double ValorTotal = 0;
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (IdProducto > 0)
            {
                var producto = bus_producto.get_info(IdEmpresa, IdProducto);
                var info_impuesto = bus_impuesto.get_info(producto.IdCod_Impuesto_Iva);
                IdCod_Impuesto_Iva = producto.IdCod_Impuesto_Iva;
                precio = producto.precio_1;
                PorcentajeIVA = info_impuesto.porcentaje;
                IVA_Valor = Math.Round((precio * (PorcentajeIVA / 100)),2);
                ValorTotal = Math.Round((precio + IVA_Valor),2);
            }
            

            return Json(new { Subtotal = precio, IdCod_Impuesto_Iva = IdCod_Impuesto_Iva, Porcentaje = PorcentajeIVA, ValorIva = IVA_Valor, Total = ValorTotal }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CalcularTotal(decimal IdProducto = 0, double Subtotal=0)
        {
            double PorcentajeIVA = 0;
            double IVA_Valor = 0;
            double ValorTotal = 0;
            in_Producto_Bus bus_producto = new in_Producto_Bus();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (IdProducto > 0)
            {
                var producto = bus_producto.get_info(IdEmpresa, IdProducto);
                var info_impuesto = bus_impuesto.get_info(producto.IdCod_Impuesto_Iva);
                PorcentajeIVA = info_impuesto.porcentaje;
                IVA_Valor = Math.Round((Subtotal * (PorcentajeIVA / 100)),2);
                ValorTotal = Math.Round((Subtotal + IVA_Valor),2);
            }


            return Json(new { ValorIva = IVA_Valor, Total = ValorTotal }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult buscar_registros(int IdEmpresa = 0, int IdAnio = 0, int IdRubro = 0, decimal IdTransaccionSession = 0)
        {
            var existe = 0;
            var info_registro = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);
            var info_rubro = bus_rubro.GetInfo(IdEmpresa, IdRubro);
            var Empresa = 0;
            var Anio = 0;
            var Rubro = 0;
            if (info_registro != null)
            {
                existe = 1;
                Empresa = info_registro.IdEmpresa;
                Anio = info_registro.IdAnio;
                Rubro = info_registro.IdRubro;
            }
            else
            {
                existe=0;
                var lista_detalle = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
                lista_detalle = bus_rubro_anio_periodo.GetListAsignacion(IdEmpresa, IdAnio, IdRubro);
                ListaRubroAnioPeriodo.set_list(lista_detalle, Convert.ToDecimal(IdTransaccionSession));
            }
            
            return Json(new { existe_registro = existe, IdEmpresa=Empresa, IdAnio=Anio, IdRubro=Rubro}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult guardar(int IdEmpresa = 0, int IdAnio = 0, int IdRubro = 0, int IdProducto = 0, decimal Subtotal = 0, string IdCod_Impuesto_Iva = "", decimal Porcentaje = 0, decimal ValorIVA = 0, decimal Total = 0, bool AplicaProntoPago = false, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var mensaje = "";
            var Empresa = 0;
            var Anio = 0;
            var Rubro = 0;

            var info_rubro = bus_rubro.GetInfo(IdEmpresa, IdRubro);
            aca_AnioLectivo_Rubro_Info info_rubro_anio = new aca_AnioLectivo_Rubro_Info
            {
                IdEmpresa = IdEmpresa,
                IdAnio = IdAnio,
                IdRubro = IdRubro,
                AplicaProntoPago = AplicaProntoPago,
                NomRubro = (info_rubro == null ? "" : info_rubro.NomRubro),
                IdProducto = IdProducto,
                Subtotal = Subtotal,
                IdCod_Impuesto_Iva = IdCod_Impuesto_Iva,
                Porcentaje = Porcentaje,
                ValorIVA = ValorIVA,
                Total = Total,
                lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>()
            };

            string[] array = Ids.Split(',');
            if (Ids != "")
            {
                foreach (var item in array)
                {
                    aca_AnioLectivo_Rubro_Periodo_Info info_det = new aca_AnioLectivo_Rubro_Periodo_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdAnio = IdAnio,
                        IdRubro = IdRubro,
                        IdPeriodo = Convert.ToInt32(item)
                    };
                    info_rubro_anio.lst_rubro_anio_periodo.Add(info_det);
                }
            }

            if (IdAnio!=0 && IdRubro!=0 && IdProducto!=0)
            {
                if (!bus_rubro_anio.GuardarDB(info_rubro_anio))
                {
                    mensaje = "No se ha podido guardar el registro";
                }
                Empresa = info_rubro_anio.IdEmpresa;
                Anio = info_rubro_anio.IdAnio;
                Rubro = info_rubro_anio.IdRubro;
            }
            else
            {
                mensaje = "Ingrese la información solicitada";
            }

            return Json(new { msg = mensaje, IdEmpresa = Empresa, IdAnio = Anio, IdRubro = Rubro }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult actualizar(int IdEmpresa = 0, int IdAnio = 0, int IdRubro = 0, int IdProducto = 0, decimal Subtotal = 0, string IdCod_Impuesto_Iva = "", decimal Porcentaje = 0, decimal ValorIVA = 0, decimal Total = 0, bool AplicaProntoPago = false, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var mensaje = "";
            var Empresa = 0;
            var Anio = 0;
            var Rubro = 0;
            aca_AnioLectivo_Rubro_Info info_rubro_anio = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);
            info_rubro_anio.AplicaProntoPago = AplicaProntoPago;
            info_rubro_anio.IdProducto = IdProducto;
            info_rubro_anio.Subtotal = Subtotal;
            info_rubro_anio.IdCod_Impuesto_Iva = IdCod_Impuesto_Iva;
            info_rubro_anio.Porcentaje = Porcentaje;
            info_rubro_anio.ValorIVA = ValorIVA;
            info_rubro_anio.Total = Total;
            info_rubro_anio.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();

            string[] array = Ids.Split(',');
            if (Ids != "")
            {
                foreach (var item in array)
                {
                    aca_AnioLectivo_Rubro_Periodo_Info info_det = new aca_AnioLectivo_Rubro_Periodo_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdAnio = IdAnio,
                        IdRubro = IdRubro,
                        IdPeriodo = Convert.ToInt32(item)
                    };
                    info_rubro_anio.lst_rubro_anio_periodo.Add(info_det);
                }

                if (!bus_rubro_anio.ModificarDB(info_rubro_anio))
                {
                    mensaje = "No se ha podido modificar el registro";
                }
            }
            Empresa = info_rubro_anio.IdEmpresa;
            Anio = info_rubro_anio.IdAnio;
            Rubro = info_rubro_anio.IdRubro;

            return Json(new { msg = mensaje, IdEmpresa = Empresa, IdAnio = Anio, IdRubro = Rubro }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult setProntoPago(int IdEmpresa = 0, int IdRubro = 0)
        {
            var AplicaProntoPago = 0;
            var info_rubro = bus_rubro.GetInfo(IdEmpresa, IdRubro);
            if (info_rubro != null)
            {
                AplicaProntoPago = (info_rubro.AplicaProntoPago== true ? 1 : 0);
            }

            return Json(AplicaProntoPago, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Acciones
        [ValidateInput(false)]
        public ActionResult GridViewPartial_RubroPorPeriodo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Rubro_Periodo_Info> model = ListaRubroAnioPeriodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_RubroPorPeriodo", model);
        }

        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivo_Rubro_Info model = new aca_AnioLectivo_Rubro_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "RubroPorPeriodo", "Index");
            if (!inf.Nuevo)
                return RedirectToAction("Index");
            #endregion

            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            ListaRubroAnioPeriodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            cargar_combos();
            return View(model);
        }

        public ActionResult Consultar(int IdEmpresa = 0, int IdAnio = 0, int IdRubro = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivo_Rubro_Info model = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);

            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "RubroPorPeriodo", "Index");
            var info_anio = bus_anio.GetInfo(IdEmpresa, IdAnio);
            if (info_anio.BloquearMatricula == true)
            {
                inf.Modificar = false;
                inf.Anular = false;
            }

            ViewBag.Nuevo = inf.Nuevo;
            ViewBag.Modificar = inf.Modificar;
            ViewBag.Anular = inf.Anular;
            #endregion

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            model.lst_rubro_anio_periodo = bus_rubro_anio_periodo.GetListAsignacion(model.IdEmpresa, model.IdAnio, model.IdRubro);
            ListaRubroAnioPeriodo.set_list(model.lst_rubro_anio_periodo, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa=0, int IdAnio=0, int IdRubro=0, bool Exito=false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "RubroPorPeriodo", "Index");
            if (!inf.Modificar)
                return RedirectToAction("Index");
            #endregion
            aca_AnioLectivo_Rubro_Info model = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            model.lst_rubro_anio_periodo = bus_rubro_anio_periodo.GetListAsignacion(model.IdEmpresa, model.IdAnio, model.IdRubro);
            ListaRubroAnioPeriodo.set_list(model.lst_rubro_anio_periodo, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAnio = 0, int IdRubro=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "RubroPorPeriodo", "Index");
            if (!inf.Anular)
                return RedirectToAction("Index");
            #endregion

            aca_AnioLectivo_Rubro_Info model = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            model.lst_rubro_anio_periodo = bus_rubro_anio_periodo.GetListAsignacion(model.IdEmpresa, model.IdAnio, model.IdRubro);
            ListaRubroAnioPeriodo.set_list(model.lst_rubro_anio_periodo, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_AnioLectivo_Rubro_Info model)
        {
            if (!bus_rubro_anio.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class aca_AnioLectivo_Rubro_List
    {
        string Variable = "aca_AnioLectivo_Rubro_Info";
        public List<aca_AnioLectivo_Rubro_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Rubro_Info> list = new List<aca_AnioLectivo_Rubro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Rubro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Rubro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class aca_AnioLectivo_Rubro_Periodo_List
    {
        string Variable = "aca_AnioLectivo_Rubro_Periodo_Info";
        public List<aca_AnioLectivo_Rubro_Periodo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Rubro_Periodo_Info> list = new List<aca_AnioLectivo_Rubro_Periodo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Rubro_Periodo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Rubro_Periodo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}