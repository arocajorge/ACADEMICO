using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class PeriodoPorAnioLectivoController : Controller
    {
        #region Variables
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Periodo_Bus bus_anio_periodo = new aca_AnioLectivo_Periodo_Bus();
        aca_AnioLectivo_Periodo_List Lista_AnioLectivo_Periodo = new aca_AnioLectivo_Periodo_List();
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

            aca_AnioLectivo_Periodo_Info model = new aca_AnioLectivo_Periodo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Periodo_Info> lista = bus_anio_periodo.GetList(model.IdEmpresa);
            Lista_AnioLectivo_Periodo.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_anio_lectivo_periodo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Periodo_Info> model = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_anio_lectivo_periodo", model);
        }
        #endregion

        #region Combos
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_Periodo_Info());
        }
        #endregion

        #region funciones del detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_periodos()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Periodo_Info> model = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_periodos", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivo_Periodo_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var Lista = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            if (Lista.Where(q => q.IdPeriodo == info_det.IdPeriodo).ToList().Count == 0)
            {
                Lista_AnioLectivo_Periodo.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }

            var model = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_periodos", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivo_Periodo_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdAnio = Convert.ToInt32(SessionFixed.IdAnio);
            if (info_det != null)
            {
                Lista_AnioLectivo_Periodo.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }

            var model = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_periodos", model);
        }

        public ActionResult EditingDelete(int IdRubro)
        {
            Lista_AnioLectivo_Periodo.DeleteRow(IdRubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_periodos", model);
        }
        #endregion

        #region Acciones
        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivo_Periodo_Info model = new aca_AnioLectivo_Periodo_Info();
            model.IdEmpresa = IdEmpresa;
            model.IdAnio = IdAnio;
            model.lst_detalle = bus_anio_periodo.GetList(IdEmpresa, IdAnio, true);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.lst_detalle = new List<aca_AnioLectivo_Periodo_Info>();
            model.lst_detalle = bus_anio_periodo.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_AnioLectivo_Periodo.set_list(model.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_AnioLectivo_Periodo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.lst_detalle = new List<aca_AnioLectivo_Periodo_Info>();
            model.lst_detalle = Lista_AnioLectivo_Periodo.get_list(Convert.ToDecimal(model.IdTransaccionSession));
            model.lst_detalle.ForEach(q=> q.IdUsuarioModificacion = model.IdUsuarioModificacion);

            if (!bus_anio_periodo.ModificarDB(model.lst_detalle))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
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

            aca_AnioLectivo_Periodo_Info model = new aca_AnioLectivo_Periodo_Info();
            model.lst_detalle = bus_anio_periodo.GetList(IdEmpresa, IdAnio, true);

            if (model == null)
                return RedirectToAction("Index");

            model.lst_detalle = new List<aca_AnioLectivo_Periodo_Info>();
            model.lst_detalle = bus_anio_periodo.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_AnioLectivo_Periodo.set_list(model.lst_detalle, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_AnioLectivo_Periodo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_anio_periodo.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();

                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class aca_AnioLectivo_Periodo_List
    {
        string Variable = "aca_AnioLectivo_Periodo_Info";
        public List<aca_AnioLectivo_Periodo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Periodo_Info> list = new List<aca_AnioLectivo_Periodo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Periodo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Periodo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(aca_AnioLectivo_Periodo_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            List<aca_AnioLectivo_Periodo_Info> list = get_list(IdTransaccionSession);
            list.Add(info_det);
        }

        public void UpdateRow(aca_AnioLectivo_Periodo_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_AnioLectivo_Periodo_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdEmpresa == IdEmpresa && m.IdPeriodo == info_det.IdPeriodo).FirstOrDefault();
            var mes = info_det.FechaDesde.Month;
            edited_info.IdMes = mes;
            edited_info.FechaDesde = info_det.FechaDesde;
            edited_info.FechaHasta = info_det.FechaHasta;
            edited_info.FechaProntoPago = info_det.FechaProntoPago;
        }

        public void DeleteRow(int IdPeriodo, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            List<aca_AnioLectivo_Periodo_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(q => q.IdPeriodo == IdPeriodo).FirstOrDefault());
        }
    }
}