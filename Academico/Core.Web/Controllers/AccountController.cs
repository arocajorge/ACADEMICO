using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Core.Web.Models;
using Core.Info.SeguridadAcceso;
using Core.Bus.SeguridadAcceso;
using Core.Bus.Academico;
using Core.Web.Helps;
using Core.Bus.General;
using Core.Web.Areas.SeguridadAcceso.Controllers;
using Core.Web.Areas.Academico.Controllers;
using Core.Bus.Caja;

namespace Core.Web.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        seg_usuario_x_aca_Sede_Bus bus_usuario_x_empresa = new seg_usuario_x_aca_Sede_Bus();
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_Menu_Bus bus_menu = new aca_Menu_Bus();
        aca_Menu_x_seg_usuario_Bus bus_usuario_x_sede = new aca_Menu_x_seg_usuario_Bus();
        seg_usuario_x_aca_Sede_Bus bus_MenuPorSede = new seg_usuario_x_aca_Sede_Bus();
        caj_Caja_Bus bus_caja = new caj_Caja_Bus();
        #endregion
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            seg_usuario_Info info_usuario = bus_usuario.validar_login(model.IdUsuario, model.Contrasena);
            if (info_usuario != null)
            {
                if (info_usuario.CambiarContraseniaSgtSesion)
                    return RedirectToAction("CambiarContrasena", new { IdUsuario = model.IdUsuario });
                return RedirectToAction("LoginEmpresa", new { IdUsuario = model.IdUsuario });
            }
            ViewBag.mensaje = "Credenciales incorrectas";
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LoginEmpresa(string IdUsuario = "")
        {
            var lst = bus_usuario_x_empresa.GetList(IdUsuario);
            if (lst.Count == 0)
                return RedirectToAction("Login");
            ViewBag.lst_empresas = lst;
            LoginModel model = new LoginModel
            {
                IdUsuario = IdUsuario,
                IdEmpresa = lst.FirstOrDefault().IdEmpresa
            };
            cargar_combos(model.IdEmpresa, IdUsuario);
            return View(model);
        }
        [HttpPost]
        public ActionResult LoginEmpresa(LoginModel model)
        {
            var info_empresa = bus_empresa.get_info(model.IdEmpresa);
            if (info_empresa == null)
            {
                cargar_combos(model.IdEmpresa, model.IdUsuario);
                return View(model);
            }
            Session["IdUsuario"] = model.IdUsuario;
            Session["IdEmpresa"] = model.IdEmpresa;
            Session["nom_empresa"] = info_empresa.em_nombre;
            Session["IdSede"] = model.IdSede;
            Session["IdSucursal"] = model.IdSucursal= bus_sede.GetInfo(model.IdEmpresa, model.IdSede).IdSucursal;
            Session["IdNivel"] = model.IdNivel;
            Session["em_direccion"] = info_empresa.em_direccion;
            SessionFixed.NomEmpresa = info_empresa.em_nombre;
            //SessionFixed.Ruc = info_empresa.em_ruc;
            SessionFixed.IdEmpresa = model.IdEmpresa.ToString();
            SessionFixed.IdSede = model.IdSede.ToString();
            SessionFixed.IdSucursal = model.IdSucursal.ToString();
            SessionFixed.IdNivel = model.IdNivel.ToString();
            //SessionFixed.em_direccion = info_empresa.em_direccion;
            SessionFixed.IdTransaccionSession = string.IsNullOrEmpty(SessionFixed.IdTransaccionSession) ? "1" : (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;

            var usuario = bus_usuario.get_info(model.IdUsuario);
            if (usuario != null)
            {
                SessionFixed.IdUsuario = usuario.IdUsuario;
                SessionFixed.EsContador = Convert.ToString(usuario.EsContador);
                SessionFixed.ManejaAdmision = Convert.ToString(usuario.ManejaAdmision);
                SessionFixed.EsSuperAdmin = usuario.es_super_admin.ToString();
                SessionFixed.IdCaja = bus_caja.GetIdCajaPorUsuario(model.IdEmpresa, SessionFixed.IdUsuario).ToString();
                var lista = bus_usuario_x_sede.get_list(model.IdEmpresa, model.IdSede, usuario.IdUsuario, false);
                seg_Menu_x_Sede_x_Usuario_Lista.set_list(bus_usuario_x_sede.get_list(model.IdEmpresa, model.IdSede, usuario.IdUsuario, false));
                if (usuario.IdMenu != null)
                {
                    var menu = bus_menu.get_info((int)usuario.IdMenu);
                    if (menu != null && !string.IsNullOrEmpty(menu.web_nom_Action))
                        return RedirectToAction(menu.web_nom_Action, menu.web_nom_Controller, new { Area = menu.web_nom_Area });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult CambiarContrasena(string IdUsuario = "")
        {
            LoginModel model = new LoginModel { IdUsuario = IdUsuario };
            return View(model);
        }
        [HttpPost]
        public ActionResult CambiarContrasena(LoginModel model)
        {
            model.Contrasena = string.IsNullOrEmpty(model.Contrasena) ? "" : model.Contrasena.Trim();
            model.new_Contrasena = string.IsNullOrEmpty(model.new_Contrasena) ? "" : model.new_Contrasena.Trim();
            if (model.Contrasena == model.new_Contrasena)
            {
                ViewBag.mensaje = "La nueva contraseña no debe ser igual a la anterior";
                return View(model);
            }
            if (!bus_usuario.modificarDB(model.IdUsuario, model.Contrasena, model.new_Contrasena))
            {
                ViewBag.mensaje = "Credenciales incorrectas, por favor corrija";
                return View(model);
            }
            return RedirectToAction("Login");
        }
        private void cargar_combos(int IdEmpresa, string IdUsuario)
        {
            var lst_sedes = bus_sede.GetList(IdEmpresa, false);
            ViewBag.lst_sedes = lst_sedes;

            //var lst_empresas = bus_empresa.get_list(false);
            //ViewBag.lst_empresas = lst_empresas;
        }
        #region Json
        public JsonResult cargar_sede_x_empresa(int IdEmpresa = 0, string IdUsuario = "")
        {
            var login = bus_sede.GetList(IdEmpresa, false);
            return Json(login, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}