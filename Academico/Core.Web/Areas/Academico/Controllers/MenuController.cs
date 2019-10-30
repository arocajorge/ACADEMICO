using Core.Bus.Academico;
using Core.Bus.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.SeguridadAcceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MenuController : Controller
    {
        #region Index/Metodos
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_Bus bus_menu = new aca_Menu_Bus();
        string mensaje = string.Empty;

        public ActionResult Index()
        {
            return View();
        }
        private void cargar_combos()
        {
            var lst_menu = bus_menu.get_list_combo(false);
            lst_menu.Add(new aca_Menu_Info { DescripcionMenu_combo = "== Seleccione ==" });
            ViewBag.lst_menu = lst_menu;
        }

        #endregion

        [ValidateInput(false)]
        public ActionResult TreeListPartial_menu()
        {
            List<aca_Menu_Info> model = bus_menu.get_list(true);
            return PartialView("_TreeListPartial_menu", model);
        }

        #region Acciones

        public ActionResult Nuevo()
        {
            aca_Menu_Info model = new aca_Menu_Info();
            cargar_combos();
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Menu_Info model)
        {
            if (!bus_menu.guardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdMenu = model.IdMenu, Exito = true });
        }

        public ActionResult Modificar(int IdMenu = 0, bool Exito = false)
        {
            aca_Menu_Info model = bus_menu.get_info(IdMenu);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_Menu_Info model)
        {
            if (!bus_menu.modificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdMenu = model.IdMenu, Exito = true });
        }

        public ActionResult Anular(int IdMenu = 0)
        {
            aca_Menu_Info model = bus_menu.get_info(IdMenu);
            if (model == null)
                return RedirectToAction("Index");
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Menu_Info model)
        {
            if (!bus_menu.anularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}