using Core.Bus.General;
using Core.Info.General;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.General.Controllers
{
    public class CorreoParametroController : Controller
    {
        #region Variables
        tb_ColaCorreoParametros_Bus bus_parametro = new tb_ColaCorreoParametros_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Index
        public ActionResult Index(bool Exito=false)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            tb_ColaCorreoParametros_Info model = bus_parametro.GetInfo(IdEmpresa);
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(tb_ColaCorreoParametros_Info model)
        {
            if (!bus_parametro.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }

            return RedirectToAction("Index", new { Exito = true });
        }
        #endregion
    }

}