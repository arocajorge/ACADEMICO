using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class AsignacionTutorInspectorController : Controller
    {
        #region Variables
        aca_AnioLectivo_Curso_Paralelo_List Lista_Paralelo = new aca_AnioLectivo_Curso_Paralelo_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Curso_Paralelo_Bus bus_ParaleloPorCurso = new aca_AnioLectivo_Curso_Paralelo_Bus();
        aca_AnioLectivo_Curso_Paralelo_List Lista_ParaleloPorCurso = new aca_AnioLectivo_Curso_Paralelo_List();
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_ProfesorTutor()
        {
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info();
            return PartialView("_Cmb_ProfesorTutor", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_tutor(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.TUTOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_tutor(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.TUTOR.ToString());
        }

        public ActionResult Cmb_ProfesorInspector()
        {
            aca_Curso_Info model = new aca_Curso_Info();
            return PartialView("_Cmb_ProfesorInspector", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_inspector(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.INSPECTOR.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_inspector(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.INSPECTOR.ToString());
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
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivo_Curso_Paralelo_Info model = new aca_AnioLectivo_Curso_Paralelo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Curso_Paralelo_Info> lista = bus_ParaleloPorCurso.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_ParaleloPorCurso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Curso_Paralelo_Info model)
        {
            List<aca_AnioLectivo_Curso_Paralelo_Info> lista = bus_ParaleloPorCurso.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_ParaleloPorCurso.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AnioLectivoCurso()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Curso_Paralelo_Info> model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AnioLectivoCurso", model);
        }
        #endregion

        #region FuncionesDetalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AsignacionTutorInspector()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_ParaleloPorCurso.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            //ViewBag.BatchEditingOptions = options;
            return PartialView("_GridViewPartial_AsignacionTutorInspector", model);
        }

        [ValidateInput(false)]
        public ActionResult BatchEditingUpdateModel(MVCxGridViewBatchUpdateValues<aca_AnioLectivo_Curso_Paralelo_Info, int> updateValues)
        {
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    Lista_ParaleloPorCurso.UpdateRow(product, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            }
            return GridViewPartial_AsignacionTutorInspector();
        }
        #endregion
    }
}