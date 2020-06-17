using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.Contabilidad;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Contabilidad.Controllers
{
    public class PeriodoContableController : Controller
    {
        #region Variables
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        ct_anio_fiscal_Bus bus_anio = new ct_anio_fiscal_Bus();
        tb_mes_Bus bus_mes = new tb_mes_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion
        #region Index
        public ActionResult Index(int IdanioFiscal = 0)
        {
            ViewBag.IdanioFiscal = IdanioFiscal;
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "PeriodoContable", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_periodocontable(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            List<ct_periodo_Info> model = new List<ct_periodo_Info>();
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model = bus_periodo.get_list(IdEmpresa, true);
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_periodocontable", model);
        }

        private void cargar_combos()
        {
            var lst_anio = bus_anio.get_list(false);
            ViewBag.lst_anio_fiscal = lst_anio;

            var lst_mes = bus_mes.get_list();
            ViewBag.lst_Mes = lst_mes;
        }

        #endregion
        #region Acciones

        public ActionResult Nuevo(int IdPeriodo = 0)
        {
            cargar_combos();
            ct_periodo_Info model = new ct_periodo_Info();
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "PeriodoContable", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ct_periodo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!bus_periodo.guardarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdPeriodo = model.IdPeriodo, Exito = true });
        }
        public ActionResult Consultar(int IdPeriodo = 0, bool Exito=false)
        {
            ct_periodo_Info model = bus_periodo.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdPeriodo);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "PeriodoContable", "Index");
            if (model.pe_estado == "I")
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        public ActionResult Modificar(int IdPeriodo = 0)
        {
            ct_periodo_Info model = bus_periodo.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdPeriodo);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "PeriodoContable", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(ct_periodo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!bus_periodo.modificarDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdPeriodo = model.IdPeriodo, Exito = true });
        }

        public ActionResult Anular(int IdPeriodo = 0)
        {
            ct_periodo_Info model = bus_periodo.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdPeriodo);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "PeriodoContable", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(ct_periodo_Info model)
        {
            model.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (!bus_periodo.anularDB(model))
            {
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}