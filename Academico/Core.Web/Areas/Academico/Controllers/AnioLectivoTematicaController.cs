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
    public class AnioLectivoTematicaController : Controller
    {
        #region Variables
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Tematica_Bus bus_anio_tematica = new aca_AnioLectivo_Tematica_Bus();
        aca_AnioLectivo_Tematica_List Lista_AnioTematica = new aca_AnioLectivo_Tematica_List();
        aca_Tematica_Bus bus_tematica = new aca_Tematica_Bus();
        string mensaje = string.Empty;
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
            aca_AnioLectivo_Tematica_Info model = new aca_AnioLectivo_Tematica_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Tematica_Info> lista = bus_anio_tematica.get_list_asignacion(model.IdEmpresa, model.IdAnio);
            Lista_AnioTematica.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Tematica_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<aca_AnioLectivo_Tematica_Info> lista = bus_anio_tematica.get_list_asignacion(model.IdEmpresa, model.IdAnio);
            Lista_AnioTematica.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AnioLectivoTematica()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Tematica_Info> model = Lista_AnioTematica.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AnioLectivoTematica", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_anio = bus_anio.GetList(IdEmpresa, false);
            ViewBag.lst_anio = lst_anio;
        }
        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int IdAnio = 0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var resultado = 1;
            List<aca_AnioLectivo_Tematica_Info> lista = new List<aca_AnioLectivo_Tematica_Info>();
            string[] array = Ids.Split(',');

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    var info_tematica = bus_tematica.GetInfo(IdEmpresa, Convert.ToInt32(item));

                    aca_AnioLectivo_Tematica_Info info = new aca_AnioLectivo_Tematica_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdAnio = IdAnio,
                        IdTematica = Convert.ToInt32(item),
                        IdCampoAccion = info_tematica.IdCampoAccion,
                        NombreTematica = info_tematica.NombreTematica,
                        OrdenTematica = info_tematica.OrdenTematica,
                        NombreCampoAccion = info_tematica.NombreCampoAccion,
                        OrdenCampoAccion = info_tematica.OrdenCampoAccion
                        
                    };
                    lista.Add(info);
                }
            }

            if (!bus_anio_tematica.guardarDB(IdEmpresa, IdAnio, lista))
            {
                resultado = 0;
            }
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListTematicaXAnio(int IdEmpresa = 0, int IdAnio = 0, decimal IdTransaccionSession = 0)
        {
            List<aca_AnioLectivo_Tematica_Info> lista = new List<aca_AnioLectivo_Tematica_Info>();
            lista = bus_anio_tematica.get_list_asignacion(IdEmpresa, IdAnio);
            Lista_AnioTematica.set_list(lista, IdTransaccionSession);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_AnioLectivo_Tematica_List
    {
        aca_AnioLectivo_Tematica_Bus bus_anio_tematica = new aca_AnioLectivo_Tematica_Bus();
        string Variable = "aca_AnioLectivo_Tematica_Info";
        public List<aca_AnioLectivo_Tematica_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Tematica_Info> list = new List<aca_AnioLectivo_Tematica_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Tematica_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Tematica_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}