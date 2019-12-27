using Core.Bus.Contabilidad;
using Core.Bus.Facturacion;
using Core.Bus.Inventario;
using Core.Info.Contabilidad;
using Core.Info.Facturacion;
using Core.Info.Helps;
using Core.Info.Inventario;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class TipoNotaController : Controller
    {
        #region Variables
        fa_TipoNota_Bus bus_tiponota = new fa_TipoNota_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_tiponota()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var model = bus_tiponota.get_list(IdEmpresa, true);
            return PartialView("_GridViewPartial_tiponota", model);
        }
        private void cargar_combos()
        {
            Dictionary<string, string> lst_tipos = new Dictionary<string, string>();
            lst_tipos.Add("C", "Credito");
            lst_tipos.Add("D", "Debito");
            ViewBag.lst_tipos = lst_tipos;
        }

        #endregion

        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_TipoNota()
        {
            fa_TipoNota_Info model = new fa_TipoNota_Info();
            return PartialView("_CmbCuenta_TipoNota", model);
        }

        public ActionResult CmbCuentaCxC_TipoNota()
        {
            fa_TipoNota_Info model = new fa_TipoNota_Info();
            return PartialView("_CmbCuentaCxC_TipoNota", model);
        }

        #region Metodos ComboBox bajo demanda producto
        public ActionResult CmbProducto_TipoNota()
        {
            decimal model = new decimal();
            return PartialView("_CmbProducto_TipoNota", model);
        }
        public List<in_Producto_Info> get_list_bajo_demandaProducto(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_producto.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoBusquedaProducto.PORSUCURSAL, cl_enumeradores.eModulo.FAC, 0, Convert.ToInt32(SessionFixed.IdSucursal));
        }
        public in_Producto_Info get_info_bajo_demandaProducto(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_producto.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            fa_TipoNota_Info model = new fa_TipoNota_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_TipoNota_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario.ToString();
            if (!bus_tiponota.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdTipoNota = 0)
        {
            fa_TipoNota_Info model = bus_tiponota.get_info(IdEmpresa, IdTipoNota);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_TipoNota_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdUsuario.ToString();
            if (!bus_tiponota.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdTipoNota = 0)
        {
            fa_TipoNota_Info model = bus_tiponota.get_info(IdEmpresa, IdTipoNota);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_TipoNota_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario.ToString();
            if (!bus_tiponota.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}