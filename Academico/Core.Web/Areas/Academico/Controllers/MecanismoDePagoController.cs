﻿using Core.Bus.Academico;
using Core.Bus.Facturacion;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MecanismoDePagoController : Controller
    {
        #region Variables
        aca_MecanismoDePago_List Lista_MecanismoPago = new aca_MecanismoDePago_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_MecanismoDePago_Bus bus_mecanismo = new aca_MecanismoDePago_Bus();
        fa_TerminoPago_Bus bus_termino = new fa_TerminoPago_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_MecanismoDePago_Info model = new aca_MecanismoDePago_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_MecanismoDePago_Info> lista = bus_mecanismo.GetList(model.IdEmpresa, true);
            Lista_MecanismoPago.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_MecanismoDePago_Info model)
        {
            List<aca_MecanismoDePago_Info> lista = bus_mecanismo.GetList(model.IdEmpresa, true);
            Lista_MecanismoPago.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MecanismoPago()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_MecanismoDePago_Info> model = Lista_MecanismoPago.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MecanismoPago", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_termino = bus_termino.get_list(false);
            ViewBag.lst_termino = lst_termino;
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

            aca_MecanismoDePago_Info model = new aca_MecanismoDePago_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_MecanismoDePago_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!bus_mecanismo.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMecanismo = model.IdMecanismo, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdMecanismo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MecanismoDePago_Info model = bus_mecanismo.GetInfo(IdEmpresa, IdMecanismo);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_MecanismoDePago_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!bus_mecanismo.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdMecanismo = model.IdMecanismo, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdMecanismo = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MecanismoDePago_Info model = bus_mecanismo.GetInfo(IdEmpresa, IdMecanismo);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_MecanismoDePago_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_mecanismo.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class aca_MecanismoDePago_List
    {
        string Variable = "aca_MecanismoDePago_Info";
        public List<aca_MecanismoDePago_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MecanismoDePago_Info> list = new List<aca_MecanismoDePago_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MecanismoDePago_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MecanismoDePago_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}