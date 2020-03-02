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
    public class CatalogoTipoFichaController : Controller
    {
        
        #region Index
        aca_CatalogoTipoFicha_List Lista_CatalogoTipoFicha = new aca_CatalogoTipoFicha_List();
        aca_CatalogoTipoFicha_Bus bus_catalogo_tipo_ficha = new aca_CatalogoTipoFicha_Bus();
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_CatalogoTipoFicha_Info model = new aca_CatalogoTipoFicha_Info
            {
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_CatalogoTipoFicha_Info> lista = bus_catalogo_tipo_ficha.get_list();
            Lista_CatalogoTipoFicha.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_catalogo_tipo_ficha()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_CatalogoTipoFicha_Info> model = Lista_CatalogoTipoFicha.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_catalogo_tipo_ficha", model);
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

            aca_CatalogoTipoFicha_Info model = new aca_CatalogoTipoFicha_Info();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_CatalogoTipoFicha_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_catalogo_tipo_ficha.guardarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int IdCatalogoTipoFicha = 0)
        {
            aca_CatalogoTipoFicha_Info model = bus_catalogo_tipo_ficha.get_info(IdCatalogoTipoFicha);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_CatalogoTipoFicha_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_catalogo_tipo_ficha.modificarDB(model))
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_CatalogoTipoFicha_List
    {
        string Variable = "aca_CatalogoTipoFicha_Info";
        public List<aca_CatalogoTipoFicha_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_CatalogoTipoFicha_Info> list = new List<aca_CatalogoTipoFicha_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_CatalogoTipoFicha_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_CatalogoTipoFicha_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}