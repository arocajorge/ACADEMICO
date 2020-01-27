using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class CatalogoFichaController : Controller
    {
        #region Variables
        aca_CatalogoFicha_Bus bus_catalogo = new aca_CatalogoFicha_Bus();
        aca_CatalogoTipoFicha_Bus bus_catalogo_tipo = new aca_CatalogoTipoFicha_Bus();
        aca_CatalogoFicha_List ListaCatalogoFicha = new aca_CatalogoFicha_List();
        #endregion

        #region Index

        public ActionResult Index(int IdCatalogoTipoFicha = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ViewBag.IdCatalogoTipoFicha = IdCatalogoTipoFicha;
            aca_CatalogoFicha_Info model = new aca_CatalogoFicha_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            List<aca_CatalogoFicha_Info> lista = new List<aca_CatalogoFicha_Info>();
            lista = bus_catalogo.GetList_x_Tipo(IdCatalogoTipoFicha, true);
            ListaCatalogoFicha.set_list(lista, model.IdTransaccionSession);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_ficha(int IdCatalogoTipoFicha = 0)
        {   
            ViewBag.IdCatalogoTipoFicha = IdCatalogoTipoFicha;

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_CatalogoFicha_Info> model = ListaCatalogoFicha.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_catalogo_ficha", model);
        }

        private void cargar_combos()
        {
            var lst_catalogo_tipo = bus_catalogo_tipo.get_list();
            ViewBag.lst_tipos = lst_catalogo_tipo;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdCatalogoTipoFicha = 0)
        {
            aca_CatalogoFicha_Info model = new aca_CatalogoFicha_Info
            {
                IdCatalogoTipoFicha = IdCatalogoTipoFicha
            };
            ViewBag.IdCatalogoTipoFicha = IdCatalogoTipoFicha;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_CatalogoFicha_Info model)
        {
            if (bus_catalogo.validar_existe_CodCatalogo(model.Codigo))
            {
                ViewBag.mensaje = "El código ya se encuentra registrado";
                ViewBag.IdCatalogoTipoFicha = model.IdCatalogoTipoFicha;
                cargar_combos();
                return View(model);
            }

            if (!bus_catalogo.GuardarDB(model))
            {
                ViewBag.IdCatalogoTipoFicha = model.IdCatalogoTipoFicha;
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", new { IdCatalogoTipoFicha = model.IdCatalogoTipoFicha });
        }

        public ActionResult Modificar(int IdCatalogoFicha = 0, int IdCatalogoTipoFicha = 0)
        {
            aca_CatalogoFicha_Info model = bus_catalogo.GetInfo(IdCatalogoFicha);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogoTipoFicha = IdCatalogoTipoFicha });
            ViewBag.IdCatalogoTipoFicha = model.IdCatalogoTipoFicha;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_CatalogoFicha_Info model)
        {
            if (!bus_catalogo.ModificarDB(model))
            {
                ViewBag.IdCatalogoTipoFicha = model.IdCatalogoTipoFicha;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogoTipoFicha = model.IdCatalogoTipoFicha });

        }

        public ActionResult Anular(int IdCatalogoFicha = 0, int IdCatalogoTipoFicha = 0)
        {
            aca_CatalogoFicha_Info model = bus_catalogo.GetInfo(IdCatalogoFicha);
            if (model == null)
                return RedirectToAction("Index", new { IdCatalogoTipoFicha = IdCatalogoTipoFicha });
            ViewBag.IdCatalogoTipoFicha = model.IdCatalogoTipoFicha;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_CatalogoFicha_Info model)
        {
            if (!bus_catalogo.AnularDB(model))
            {
                ViewBag.IdCatalogoTipoFicha = model.IdCatalogoTipoFicha;
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index", new { IdCatalogoTipoFicha = model.IdCatalogoTipoFicha });

        }
        #endregion
    }

    public class aca_CatalogoFicha_List
    {
        string Variable = "aca_CatalogoFicha_Info";
        public List<aca_CatalogoFicha_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_CatalogoFicha_Info> list = new List<aca_CatalogoFicha_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_CatalogoFicha_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_CatalogoFicha_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}