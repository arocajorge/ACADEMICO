using Core.Bus.Academico;
using Core.Bus.SeguridadAcceso;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.SeguridadAcceso;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.SeguridadAcceso.Controllers
{
    public class UsuarioController : Controller
    {
        #region Index/Metodos
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        //seg_usuario_x_aca_Sede_Bus bus_usuario_x_sede = new seg_usuario_x_aca_Sede_Bus();
        aca_Menu_Bus bus_menu = new aca_Menu_Bus();
        seg_usuario_x_aca_Sede_List List_det = new seg_usuario_x_aca_Sede_List();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        seg_usuario_x_aca_Sede_Bus bus_usuario_x_sede = new seg_usuario_x_aca_Sede_Bus();
        seg_usuario_academico_List Lista_Usuarios = new seg_usuario_academico_List();
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            seg_usuario_Info model = new seg_usuario_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            List<seg_usuario_Info> lista = bus_usuario.get_list(true);
            Lista_Usuarios.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_usuario()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<seg_usuario_Info> model = Lista_Usuarios.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_usuario", model);
        }

        private void cargar_combos(seg_usuario_Info model)
        {
            var lst_menu = bus_menu.get_list_combo(false);
            lst_menu.Add(new aca_Menu_Info { IdMenu = 0, DescripcionMenu_combo = "== Seleccione ==" });
            ViewBag.lst_menu = lst_menu;
        }

        #endregion

        #region ComboBox Bajo demanda
        public ActionResult CmbEmpresa_det()
        {
            seg_usuario_x_aca_Sede_Info model = new seg_usuario_x_aca_Sede_Info();
            return PartialView("_CmbEmpresa_det", model);
        }
        public List<tb_empresa_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_empresa.get_list_bajo_demanda(args);
        }

        public tb_empresa_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_empresa.get_info_bajo_demanda(args);
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
            seg_usuario_Info model = new seg_usuario_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual),
            };
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            var lst = bus_usuario_x_sede.GetList(Convert.ToString(SessionFixed.IdUsuario));
            List_det.set_list(lst, model.IdTransaccionSession);

            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(seg_usuario_Info model)
        {
            if (bus_usuario.validar_existe_usuario(model.IdUsuario))
            {
                ViewBag.mensaje = "Usuario ya se encuentra registrado";
                return View(model);
            }

            model.lst_usuario_sede = List_det.get_list(model.IdTransaccionSession);
            if (!bus_usuario.guardarDB(model))
                return View(model);
            cargar_combos(model);

            return RedirectToAction("Index");
        }

        public ActionResult Modificar(string IdUsuario = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            seg_usuario_Info model = bus_usuario.get_info(IdUsuario);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_usuario_sede = bus_usuario_x_sede.GetList(model.IdUsuario);
            List_det.set_list(model.lst_usuario_sede, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(seg_usuario_Info model)
        {
            model.lst_usuario_sede = List_det.get_list(model.IdTransaccionSession);
            if (!bus_usuario.modificarDB(model))
                return View(model);

            return RedirectToAction("Index");
        }

        public ActionResult Anular(string IdUsuario = "")
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            seg_usuario_Info model = bus_usuario.get_info(IdUsuario);
            if (model == null)
                return RedirectToAction("Index");
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_usuario_sede = bus_usuario_x_sede.GetList(model.IdUsuario);
            List_det.set_list(model.lst_usuario_sede, model.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(seg_usuario_Info model)
        {
            if (!bus_usuario.anularDB(model))
                return View(model);

            return RedirectToAction("Index");
        }
        #endregion

        #region Json
        public JsonResult ResetearContrasena(string IdUsuario = "")
        {
            int resultado = 0;

            if (bus_usuario.ResetearContrasenia(IdUsuario, "1234"))
                resultado = 1;

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Detalle
        public ActionResult CargarSede()
        {
            int IdEmpresa = 0;
            if (Request.Params["var_IdEmpresa"] == null || !Int32.TryParse(Request.Params["var_IdEmpresa"].ToString(), out IdEmpresa))
                IdEmpresa = 0;

            var lst = bus_sede.GetList(IdEmpresa, false);
            return GridViewExtension.GetComboBoxCallbackResult(p =>
            {
                p.TextField = "NomSede";
                p.ValueField = "IdSede";
                p.Columns.Add("NomSede", "Sede");
                p.TextFormatString = "{0}";
                p.ValueType = typeof(int);
                p.BindList(bus_sede.GetList(IdEmpresa, false));
            });
        }

        private void cargar_combos_det()
        {
            var lst_empresas = bus_empresa.get_list(false);
            ViewBag.lst_empresas = lst_empresas;

            var lst_sedes = bus_sede.GetListSinEmpresa(false);
            ViewBag.lst_sedes = lst_sedes;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_Usuario_x_Sede()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_Usuario_x_Sede", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] seg_usuario_x_aca_Sede_Info info_det)
        {
            if (ModelState.IsValid)
            {
                List_det.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            cargar_combos_det();
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Usuario_x_Sede", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] seg_usuario_x_aca_Sede_Info info_det)
        {
            if (ModelState.IsValid)
            {
                List_det.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_det();
            return PartialView("_GridViewPartial_Usuario_x_Sede", model);
        }
        public ActionResult EditingDelete(string IdString = "")
        {
            List_det.DeleteRow(IdString, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = List_det.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Usuario_x_Sede", model);
        }
        #endregion
    }
    public class seg_usuario_x_aca_Sede_List
    {
        string Variable = "seg_usuario_x_aca_Sede_Info";
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        public List<seg_usuario_x_aca_Sede_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<seg_usuario_x_aca_Sede_Info> list = new List<seg_usuario_x_aca_Sede_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<seg_usuario_x_aca_Sede_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<seg_usuario_x_aca_Sede_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(seg_usuario_x_aca_Sede_Info info_det, decimal IdTransaccionSession)
        {
            List<seg_usuario_x_aca_Sede_Info> list = get_list(IdTransaccionSession);

            if (list.Where(q => q.IdEmpresa == info_det.IdEmpresa && q.IdSede == info_det.IdSede).Count() == 0)
            {
                var emp = bus_empresa.get_info(info_det.IdEmpresa);
                var sede = bus_sede.GetInfo(info_det.IdEmpresa, info_det.IdSede );

                if (emp != null)
                {
                    info_det.em_nombre = emp.em_nombre;
                }
                if (sede != null)
                {
                    info_det.NomSede = sede.NomSede;
                }

                list.Add(info_det);

                info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
                info_det.IdString = info_det.IdEmpresa.ToString("000") + info_det.IdSede.ToString("000");               
            }

        }

        public void UpdateRow(seg_usuario_x_aca_Sede_Info info_det, decimal IdTransaccionSession)
        {
            seg_usuario_x_aca_Sede_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdString == info_det.IdString).First();
            edited_info.IdUsuario = info_det.IdUsuario;
            edited_info.IdSede = info_det.IdSede;
            edited_info.IdEmpresa = info_det.IdEmpresa;

            if (info_det != null)
            {
                var emp = bus_empresa.get_info(info_det.IdEmpresa);
                var sede = bus_sede.GetInfo(info_det.IdEmpresa, info_det.IdSede );
                if (emp != null)
                {
                    edited_info.em_nombre = emp.em_nombre;
                }
                if (sede != null)
                {
                    edited_info.NomSede = sede.NomSede;
                }

                edited_info.IdString = info_det.IdEmpresa.ToString("000") + info_det.IdSede.ToString("000");
            }
        }

        public void DeleteRow(string IdString, decimal IdTransaccionSession)
        {
            List<seg_usuario_x_aca_Sede_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.IdString == IdString.ToString()).FirstOrDefault());
        }
    }

    public class seg_usuario_academico_List
    {
        string Variable = "seg_usuario_academico_Info";
        public List<seg_usuario_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<seg_usuario_Info> list = new List<seg_usuario_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<seg_usuario_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<seg_usuario_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}