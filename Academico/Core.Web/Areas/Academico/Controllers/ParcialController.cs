using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ParcialController : Controller
    {
        #region Variables
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_AnioLectivoParcial_List ListaParcial = new aca_AnioLectivoParcial_List();
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
            aca_AnioLectivoParcial_Info model = new aca_AnioLectivoParcial_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };

            List<aca_AnioLectivoParcial_Info> lista = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            ListaParcial.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivoParcial_Info model)
        {
            List<aca_AnioLectivoParcial_Info> lista = bus_parcial.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            ListaParcial.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Parcial()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivoParcial_Info> model = ListaParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Parcial", model);
        }
        #endregion

        #region Combos
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_AnioLectivo_NivelAcademico_Jornada_Info());
        }
        public ActionResult ComboBoxPartial_Sede()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio });
        }
        #endregion

        #region Funciones del Grid
        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_AnioLectivoParcial_Info info_det)
        {

            if (ModelState.IsValid)
                ListaParcial.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<aca_AnioLectivoParcial_Info> model = new List<aca_AnioLectivoParcial_Info>();
            model = ListaParcial.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_Parcial", model);
        }
        #endregion

        #region Json
        public JsonResult generar(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, decimal IdTransaccionSession = 0)
        {
            var resultado = "";
            var lista = bus_parcial.GetList(IdEmpresa, IdSede, IdAnio);
            var lst_parcial = new List<aca_AnioLectivoParcial_Info>();
            if (lista.Count()==0)
            {
                var lst_catalogo = bus_catalogo.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.PARCIAL), false);

                foreach (var item in lst_catalogo)
                {
                    lst_parcial.Add(new aca_AnioLectivoParcial_Info {
                        IdEmpresa = IdEmpresa,
                        IdSede = IdSede,
                        IdAnio = IdAnio,
                        IdCatalogoParcial = item.IdCatalogo,
                        IdUsuarioCreacion = SessionFixed.IdUsuario
                    });
                }

                if (!bus_parcial.GuardarDB(lst_parcial))
                {
                    resultado = "No se ha podido guardar los registros";
                }
            }
            else
            {
                resultado = "Ya existen parciales para el año lectivo seleccionado";
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_AnioLectivoParcial_List
    {
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();

        string Variable = "aca_AnioLectivoParcial_Info";
        public List<aca_AnioLectivoParcial_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivoParcial_Info> list = new List<aca_AnioLectivoParcial_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivoParcial_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivoParcial_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void UpdateRow(aca_AnioLectivoParcial_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_AnioLectivoParcial_Info edited_info = get_list(IdTransaccionSession).Where(m => m.IdCatalogoParcial == info_det.IdCatalogoParcial).FirstOrDefault();
            edited_info.FechaInicio = info_det.FechaInicio;
            edited_info.FechaFin = info_det.FechaFin;
            bus_parcial.ModificarDB(edited_info);
        }
    }
}