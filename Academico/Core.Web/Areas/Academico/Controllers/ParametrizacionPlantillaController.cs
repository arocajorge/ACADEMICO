using Core.Bus.Contabilidad;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Info.Academico;
using Core.Bus.Academico;
using Core.Info.Contabilidad;
using DevExpress.Web;
using DevExpress.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ParametrizacionPlantillaController : Controller
    {
        #region Variables
        aca_AnioLectivo_Curso_Plantilla_Parametrizacion_List ListParametrizacion = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_List();
        aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Bus busParametrizacion = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Bus();
        aca_AnioLectivo_Bus busAnioLectivo = new aca_AnioLectivo_Bus();
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

            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = Convert.ToInt32(SessionFixed.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            CargarCombos(model);
            ListParametrizacion.set_list(busParametrizacion.GetList(model.IdEmpresa, model.IdAnio),model.IdTransaccionSession);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            CargarCombos(model);
            return View(model);
        }
        #endregion

        #region Metodos
        private void CargarCombos(cl_filtros_Info model)
        {
            var lstAnio = busAnioLectivo.GetList(model.IdEmpresa, false);
            ViewBag.lstAnio = lstAnio;
        }
        #endregion

        #region Combos bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuentaHaber_Parametrizacion()
        {
            aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info model = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info();
            return PartialView("_CmbCuentaHaber_Parametrizacion", model);
        }

        public ActionResult CmbCuentaDebe_Parametrizacion()
        {
            aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info model = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info();
            return PartialView("_CmbCuentaDebe_Parametrizacion", model);
        }

        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region FuncionesDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_ParametrizacionDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListParametrizacion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ParametrizacionDet", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info info_det)
        {
            ListParametrizacion.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = ListParametrizacion.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_ParametrizacionDet", model);
        }
        #endregion
    }

    public class aca_AnioLectivo_Curso_Plantilla_Parametrizacion_List
    {
        aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Bus busParametrizacion = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Bus();
        string Variable = "aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info";
        public List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info> list = new List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
        public void UpdateRow(aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info info_det, decimal IdTransaccionSession)
        {
            aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdString == info_det.IdString).FirstOrDefault();
            edited_info.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            if (busParametrizacion.ModificarDB(edited_info))
            {

            }
        }
    }
}