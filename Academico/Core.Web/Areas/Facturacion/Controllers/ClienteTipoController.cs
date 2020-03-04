using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Bus.Facturacion;
using Core.Info.Academico;
using Core.Info.Contabilidad;
using Core.Info.Facturacion;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class ClienteTipoController : Controller
    {
        #region Variables
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion
        #region Index
        fa_cliente_tipo_Bus bus_clientetipo = new fa_cliente_tipo_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult Index()
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "ClienteTipo", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_clientetipo(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            List<fa_cliente_tipo_Info> model = bus_clientetipo.get_list(IdEmpresa, true);
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_clientetipo", model);
        }

        #endregion
        #region Combo Cuenta bajo demanda
        public ActionResult CmbCuenta_Anticipo()
        {
            fa_cliente_tipo_Info model = new fa_cliente_tipo_Info();
            return PartialView("_CmbCuenta_Anticipo", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion
        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var lst_ctacble = bus_plancta.get_list(IdEmpresa, false, true);
            ViewBag.lst_cuentas = lst_ctacble;
        }

        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            fa_cliente_tipo_Info model = new fa_cliente_tipo_Info
            {
                IdEmpresa = IdEmpresa
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "ClienteTipo", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(fa_cliente_tipo_Info model)
        {
            model.IdUsuario = SessionFixed.IdEmpresa.ToString();
            if (!bus_clientetipo.guardarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int IdEmpresa = 0, int Idtipo_cliente = 0)
        {
            fa_cliente_tipo_Info model = bus_clientetipo.get_info(IdEmpresa, Idtipo_cliente);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "ClienteTipo", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(fa_cliente_tipo_Info model)
        {
            model.IdUsuarioUltMod = SessionFixed.IdEmpresa.ToString();
            if (!bus_clientetipo.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Anular(int IdEmpresa = 0, int Idtipo_cliente = 0)
        {
            fa_cliente_tipo_Info model = bus_clientetipo.get_info(IdEmpresa, Idtipo_cliente);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "ClienteTipo", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(fa_cliente_tipo_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdEmpresa.ToString();
            if (!bus_clientetipo.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }

    public class fa_cliente_tipo_List
    {
        string Variable = "fa_cliente_tipo_Info";
        public List<fa_cliente_tipo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_cliente_tipo_Info> list = new List<fa_cliente_tipo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_cliente_tipo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_cliente_tipo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}