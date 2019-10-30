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
    public class AnioLectivoController : Controller
    {
        #region Variables
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_List Lista_AnioLectivo = new aca_AnioLectivo_List();
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

            aca_AnioLectivo_Info model = new aca_AnioLectivo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Info> lista = bus_anio.GetList(model.IdEmpresa, true);
            Lista_AnioLectivo.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_anio_lectivo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Info> model = Lista_AnioLectivo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_anio_lectivo", model);
        }
        #endregion

        #region Metodos
        private bool validar(aca_AnioLectivo_Info info, ref string msg)
        {
            if (info.EnCurso== true)
            {
                var AnioEnCurso = bus_anio.GetInfo_AnioEnCurso(info.IdEmpresa, info.IdAnio);
                if (AnioEnCurso != null)
                {
                    msg = "Ya existe un año lectivo en curso";
                    return false;
                }
            }
            
            return true;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            
            aca_AnioLectivo_Info model = new aca_AnioLectivo_Info
            {
                IdEmpresa = IdEmpresa,
                FechaDesde = DateTime.Now,
                FechaHasta = DateTime.Now.AddYears(1)
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_AnioLectivo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_anio.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivo_Info model = bus_anio.GetInfo(IdEmpresa, IdAnio);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_AnioLectivo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_anio.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAnio = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivo_Info model = bus_anio.GetInfo(IdEmpresa, IdAnio);

            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_AnioLectivo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_anio.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion
    }

    public class aca_AnioLectivo_List
    {
        string Variable = "aca_AnioLectivo_Info";
        public List<aca_AnioLectivo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Info> list = new List<aca_AnioLectivo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}