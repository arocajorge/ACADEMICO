using Core.Bus.Academico;
using Core.Bus.SeguridadAcceso;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.SeguridadAcceso;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MenuPorSedePorUsuarioController : Controller
    {
        #region Index

        aca_Menu_x_seg_usuario_Bus bus_menu_sede_usuario = new aca_Menu_x_seg_usuario_Bus();
        seg_Menu_x_Sede_x_Usuario_Lista_Memoria Lista_menu_usuario = new seg_Menu_x_Sede_x_Usuario_Lista_Memoria();
        seg_Menu_x_Sede_x_Usuario_AccesoUsuario_List ListaUsuario = new seg_Menu_x_Sede_x_Usuario_AccesoUsuario_List();

        public ActionResult Index()
        {
            aca_Menu_x_seg_usuario_Info model = new aca_Menu_x_seg_usuario_Info();
            model.modificado = 0;
            Lista_menu_usuario.set_list(new List<aca_Menu_x_seg_usuario_Info>());
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(aca_Menu_x_seg_usuario_Info model)
        {
            model.modificado = 0;
            var lista_menu = bus_menu_sede_usuario.get_list(model.IdEmpresa, model.IdSede, model.IdUsuario, true);
            var lista_usuario = bus_menu_sede_usuario.get_list(model.IdEmpresa, model.IdSede, model.IdUsuario, false);

            ViewBag.IdEmpresa = model.IdEmpresa;
            ViewBag.IdSede = model.IdSede;
            ViewBag.IdUsuario = model.IdUsuario;

            Lista_menu_usuario.set_list(lista_menu);
            ListaUsuario.set_list(lista_usuario);

            cargar_combos();
            return View(model);
        }
        public static void CreateTreeViewNodesRecursive(List<aca_Menu_x_seg_usuario_Info> model, MVCxTreeViewNodeCollection nodesCollection, Int32 parentID, int IdEmpresa = 0, int IdSede = 0, string IdUsuario = "")
        {

            //var rows = bus_menu_x_empresa_x_usuario.get_list(IdEmpresa, IdUsuario, parentID);
            var rows = seg_Menu_x_Sede_x_Usuario_Lista.get_list().Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdUsuario.ToLower() == IdUsuario.ToLower() && q.info_menu.IdMenuPadre == parentID).ToList();

            foreach (aca_Menu_x_seg_usuario_Info row in rows)
            {
                var url = string.IsNullOrEmpty(row.info_menu.web_nom_Controller) ? null :
                    ("~/" + row.info_menu.web_nom_Area + "/" + row.info_menu.web_nom_Controller + "/" + row.info_menu.web_nom_Action + "/");
                MVCxTreeViewNode node = nodesCollection.Add(row.info_menu.DescripcionMenu, row.IdMenu.ToString(), null, url);
                CreateTreeViewNodesRecursive(model, node.Nodes, row.IdMenu, IdEmpresa, IdSede, IdUsuario);
            }
        }
        private void cargar_combos()
        {
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
            var lst_empresas = bus_empresa.get_list(false);
            ViewBag.lst_empresas = lst_empresas;

            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_Sede_Bus bus_sede = new aca_Sede_Bus();
            var lst_sedes = bus_sede.GetList(IdEmpresa, false);
            ViewBag.lst_sedes = lst_sedes;

            seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
            var lst_usuario = bus_usuario.get_list(false);
            ViewBag.lst_usuario = lst_usuario;
        }
        #endregion

        [ValidateInput(false)]
        public ActionResult TreeListPartial_menu_x_usuario(int IdEmpresa = 0, int IdSede = 0, string IdUsuario = "")
        {
            //var model = bus_menu_x_empresa_x_usuario.get_list(IdEmpresa, IdUsuario,true);
            var model = Lista_menu_usuario.get_list();

            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdSede = IdSede;
            ViewBag.IdUsuario = IdUsuario;

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

            return PartialView("_TreeListPartial_menu_x_usuario", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_Menu_x_seg_usuario_Info info)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            info.IdEmpresa = IdEmpresa;
            if (info != null)
                Lista_menu_usuario.UpdateRow(info);

            var model = Lista_menu_usuario.get_list();
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
            return PartialView("_TreeListPartial_menu_x_usuario", model);
        }
        public JsonResult guardar(int IdEmpresa = 0, int IdSede = 0, string IdUsuario = "", int Modificado = 0, string Ids = "")
        {
            string[] array = Ids.Split(',');

            List<aca_Menu_x_seg_usuario_Info> lista = new List<aca_Menu_x_seg_usuario_Info>();
            if (Modificado == 0)
            {
                var lst_menu_usuario = Lista_menu_usuario.get_list();
                foreach (var item in lst_menu_usuario)
                {

                    if (item != null)
                        lista.Add(item);
                }
            }
            else
            {
                //var lst_menu = Lista_menu_usuario.get_list();

                var output = array.GroupBy(q => q).ToList();
                foreach (var item in output)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                    {
                        var lst_menu = Lista_menu_usuario.get_list();
                        var menu = lst_menu.Where(q => q.IdMenu == Convert.ToInt32(item.Key)).FirstOrDefault();

                        if (menu != null)
                            lista.Add(menu);
                    }
                }
            }

            bus_menu_sede_usuario.eliminarDB(IdEmpresa, IdSede, IdUsuario);
            var resultado = bus_menu_sede_usuario.guardarDB(lista, IdEmpresa, IdSede, IdUsuario);


            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }

    public static class seg_Menu_x_Sede_x_Usuario_Lista
    {
        static string Variable = "fx_MenuXEmpresaXUsuarioFixed";
        public static List<aca_Menu_x_seg_usuario_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<aca_Menu_x_seg_usuario_Info> list = new List<aca_Menu_x_seg_usuario_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<aca_Menu_x_seg_usuario_Info>)HttpContext.Current.Session[Variable];
        }

        public static void set_list(List<aca_Menu_x_seg_usuario_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }
    }

    public class seg_Menu_x_Sede_x_Usuario_Lista_Memoria
    {
        static string Variable = "fx_MenuXEmpresaXUsuarioFixed_Lista";
        public List<aca_Menu_x_seg_usuario_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<aca_Menu_x_seg_usuario_Info> list = new List<aca_Menu_x_seg_usuario_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<aca_Menu_x_seg_usuario_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<aca_Menu_x_seg_usuario_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }

        public void UpdateRow(aca_Menu_x_seg_usuario_Info info)
        {
            aca_Menu_x_seg_usuario_Info edited_info = get_list().Where(q => q.IdMenu == info.IdMenu).FirstOrDefault();
            edited_info.Nuevo = info.Nuevo;
            edited_info.Modificar = info.Modificar;
            edited_info.Anular = info.Anular;
        }
    }

    public class seg_Menu_x_Sede_x_Usuario_AccesoUsuario_List
    {
        static string Variable = "seg_Menu_x_Empresa_x_Usuario_AccesoUsuario";
        public List<aca_Menu_x_seg_usuario_Info> get_list()
        {
            if (HttpContext.Current.Session[Variable] == null)
            {
                List<aca_Menu_x_seg_usuario_Info> list = new List<aca_Menu_x_seg_usuario_Info>();

                HttpContext.Current.Session[Variable] = list;
            }
            return (List<aca_Menu_x_seg_usuario_Info>)HttpContext.Current.Session[Variable];
        }

        public void set_list(List<aca_Menu_x_seg_usuario_Info> list)
        {
            HttpContext.Current.Session[Variable] = list;
        }
    }
}