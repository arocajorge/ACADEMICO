using Core.Bus.Academico;
using Core.Bus.SeguridadAcceso;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.SeguridadAcceso;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MenuPorSedeController : Controller
    {
        #region Index
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        aca_Menu_x_aca_Sede_Bus bus_menu_x_sede = new aca_Menu_x_aca_Sede_Bus();
        public ActionResult Index()
        {
            aca_Menu_x_aca_Sede_Info model = new aca_Menu_x_aca_Sede_Info();
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Menu_x_aca_Sede_Info model)
        {
            cargar_combos();
            return View(model);
        }

        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_empresas = bus_empresa.get_list(false);
            ViewBag.lst_empresas = lst_empresas;

            var lst_sedes = bus_sede.GetList(IdEmpresa, false);
            ViewBag.lst_sedes = lst_sedes;
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult TreeListPartial_menu_x_sede(int IdEmpresa = 0, int IdSede = 0)
        {
            List<aca_Menu_x_aca_Sede_Info> model = bus_menu_x_sede.get_list(IdEmpresa, IdSede);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSede = IdSede;
            ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            if (ViewData["selectedIDs"] == null)
            {
                int x = 0;
                string selectedIDs = "";
                foreach (var item in model.Where(q => q.seleccionado == true).ToList())
                {
                    if (x == 0)
                        selectedIDs = item.IdMenu.ToString();
                    else
                        selectedIDs += "," + item.IdMenu.ToString();
                    x++;
                }
                ViewData["selectedIDs"] = selectedIDs;
            }
            return PartialView("_TreeListPartial_menu_x_sede", model);
        }

        public JsonResult guardar(int IdEmpresa = 0, int IdSede = 0, string Ids = "")
        {
            string[] array = Ids.Split(',');
            List<aca_Menu_x_aca_Sede_Info> lista = new List<aca_Menu_x_aca_Sede_Info>();
            var output = array.GroupBy(q => q).ToList();
            bool resultado = false;
            if (Ids!= "")
            {
                foreach (var item in output)
                {
                    aca_Menu_x_aca_Sede_Info info = new aca_Menu_x_aca_Sede_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdSede = IdSede,
                        IdMenu = Convert.ToInt32(item.Key),
                    };
                    lista.Add(info);
                }
                bus_menu_x_sede.eliminarDB(IdEmpresa, IdSede);
                resultado = bus_menu_x_sede.guardarDB(lista);
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}