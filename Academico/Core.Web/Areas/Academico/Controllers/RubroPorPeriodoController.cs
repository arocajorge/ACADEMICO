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
                IVA_Valor = precio * (PorcentajeIVA / 100);
                ValorTotal = precio + IVA_Valor;
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
                IVA_Valor = Subtotal * (PorcentajeIVA / 100);
                ValorTotal = Subtotal + IVA_Valor;
            }


            return Json(new { ValorIva = IVA_Valor, Total = ValorTotal }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult guardar(int IdEmpresa = 0, int IdAnio = 0, int IdRubro = 0, int IdProducto = 0, decimal Subtotal=0, string IdCod_Impuesto_Iva="", decimal Porcentaje=0, decimal ValorIVA=0, decimal Total =0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var mensaje = ""; ;
            var registro_existe = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);

            if (registro_existe == null)
            {
                var info_rubro = bus_rubro.GetInfo(IdEmpresa, IdRubro);
                aca_AnioLectivo_Rubro_Info info_rubro_anio = new aca_AnioLectivo_Rubro_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdAnio = IdAnio,
                    IdRubro = IdRubro,
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
                            IdPeriodo = Convert.ToInt32(item),
                            FechaFacturacion = null,
                            FechaPago = null
                        };
                        info_rubro_anio.lst_rubro_anio_periodo.Add(info_det);
                    }
                }

                if (!bus_rubro_anio.GuardarDB(info_rubro_anio))
                {
                    mensaje = "No se ha podido guardar el registro";
                }
            }
            else
            {
                mensaje = "Ya existe el rubro en el año lectivo seleccionado";
            }
            

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult modificar(int IdEmpresa = 0, int IdAnio = 0, int IdRubro = 0, int IdProducto = 0, decimal Subtotal = 0, string IdCod_Impuesto_Iva = "", decimal Porcentaje = 0, decimal ValorIVA = 0, decimal Total = 0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var mensaje = ""; ;
            aca_AnioLectivo_Rubro_Info info_rubro_anio = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);
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
                        IdPeriodo = Convert.ToInt32(item),
                        FechaFacturacion = null,
                        FechaPago = null
                    };
                    info_rubro_anio.lst_rubro_anio_periodo.Add(info_det);
                }
            }

            if (!bus_rubro_anio.ModificarDB(info_rubro_anio))
            {
                mensaje = "No se ha podido modificar el registro";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
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

            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            model.lst_rubro_anio_periodo= bus_rubro_anio_periodo.GetListAsignacion(model.IdEmpresa, model.IdAnio, model.IdRubro);
            ListaRubroAnioPeriodo.set_list(model.lst_rubro_anio_periodo, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_AnioLectivo_Rubro_Info model)
        {
            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            model.lst_rubro_anio_periodo = bus_rubro_anio_periodo.GetListAsignacion(model.IdEmpresa, model.IdAnio, model.IdRubro);
            ListaRubroAnioPeriodo.set_list(model.lst_rubro_anio_periodo, Convert.ToDecimal(model.IdTransaccionSession));

            cargar_combos();
            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa=0, int IdAnio=0, int IdRubro=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            aca_AnioLectivo_Rubro_Info model = bus_rubro_anio.GetInfo(IdEmpresa, IdAnio, IdRubro);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_rubro_anio_periodo = new List<aca_AnioLectivo_Rubro_Periodo_Info>();
            model.lst_rubro_anio_periodo = bus_rubro_anio_periodo.GetListAsignacion(model.IdEmpresa, model.IdAnio, model.IdRubro);
            ListaRubroAnioPeriodo.set_list(model.lst_rubro_anio_periodo, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            cargar_combos();
            return View(model);
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