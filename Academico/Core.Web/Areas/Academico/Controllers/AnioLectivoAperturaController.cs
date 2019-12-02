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
    public class AnioLectivoAperturaController : Controller
    {
        #region Variables
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_List Lista_AnioLectivo = new aca_AnioLectivo_List();
        aca_AnioLectivo_Sede_NivelAcademico_Bus bus_sede_nivel = new aca_AnioLectivo_Sede_NivelAcademico_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_Info());
        }

        #region Metodos
        private bool validar(aca_AnioLectivo_Info info, ref string msg)
        {
            var lst_asignacion = bus_sede_nivel.GetListAsignacion(info.IdEmpresa, info.IdSede, info.IdAnioApertura);
            var existe_asignacion = lst_asignacion.Where(q => q.seleccionado == true).ToList();

            if (existe_asignacion.Count > 0)
            {
                msg = "Ya existe asignaciones para el año lectivo seleccionado";
                return false;
            }

            return true;
        }
        #endregion

        #region Json
        public JsonResult set_FechaAnio(int IdEmpresa = 0, int IdAnio = 0)
        {
            var info_anio = bus_anio.GetInfo(IdEmpresa, IdAnio);
            return Json(new { FechaDesde = info_anio.FechaDesde, FechaHasta = info_anio.FechaHasta }, JsonRequestBehavior.AllowGet);
        }
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

            var info_anio_curso = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            var lst_anio = bus_anio.GetList(Convert.ToInt32(SessionFixed.IdEmpresa), false);
            var ultimo_anio = lst_anio.Max(q=>q.IdAnio);

            aca_AnioLectivo_Info model = new aca_AnioLectivo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = info_anio_curso.IdAnio,
                FechaDesde = info_anio_curso.FechaDesde,
                FechaHasta = info_anio_curso.FechaHasta,
                IdAnioApertura = ultimo_anio,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Info model)
        {

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_anio.GuardarAperturaDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }

            ViewBag.MensajeSuccess = MensajeSuccess;
            return View(model);
        }
        #endregion
    }
}