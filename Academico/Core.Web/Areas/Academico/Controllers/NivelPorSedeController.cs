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
    public class NivelPorSedeController : Controller
    {
        #region Variables
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_NivelAcademico_Bus bus_nivel = new aca_NivelAcademico_Bus();
        aca_AnioLectivo_Sede_NivelAcademico_Bus bus_SedePorNivel = new aca_AnioLectivo_Sede_NivelAcademico_Bus();
        aca_AnioLectivo_Sede_NivelAcademico_List Lista_NivelPorSede = new aca_AnioLectivo_Sede_NivelAcademico_List();
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
            aca_AnioLectivo_Sede_NivelAcademico_Info model = new aca_AnioLectivo_Sede_NivelAcademico_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info== null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Sede_NivelAcademico_Info> lista = bus_SedePorNivel.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_NivelPorSede.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Sede_NivelAcademico_Info model)
        {
            List<aca_AnioLectivo_Sede_NivelAcademico_Info> lista = bus_SedePorNivel.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_NivelPorSede.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_NivelPorSede()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_Sede_NivelAcademico_Info> model = Lista_NivelPorSede.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_NivelPorSede", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sede = bus_sede.GetList(IdEmpresa, false);
            ViewBag.lst_sede = lst_sede;

            var lst_anio = bus_anio.GetList(IdEmpresa, false);
            ViewBag.lst_anio = lst_anio;
        }
        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int IdSede = 0, int IdAnio=0, string Ids = "", decimal IdTransaccionSession=0)
        {
            var resultado = 1;
            List<aca_AnioLectivo_Sede_NivelAcademico_Info> lista = new List<aca_AnioLectivo_Sede_NivelAcademico_Info>();
            var info_sede = bus_sede.GetInfo(IdEmpresa, IdSede);
            string[] array = Ids.Split(',');

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    var info_nivel = bus_nivel.GetInfo(IdEmpresa, Convert.ToInt32(item));

                    aca_AnioLectivo_Sede_NivelAcademico_Info info = new aca_AnioLectivo_Sede_NivelAcademico_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdSede = IdSede,
                        IdAnio = IdAnio,
                        IdNivel = Convert.ToInt32(item),
                        NomSede = info_sede.NomSede,
                        NomNivel = info_nivel.NomNivel
                    };
                    lista.Add(info);
                }

                if (!bus_SedePorNivel.GuardarDB(IdEmpresa, IdSede, IdAnio, lista))
                {
                    resultado = 0;
                }
            }
            
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListNivelPorSede(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, decimal IdTransaccionSession = 0)
        {
            List<aca_AnioLectivo_Sede_NivelAcademico_Info> lista = new List<aca_AnioLectivo_Sede_NivelAcademico_Info>();
            lista = bus_SedePorNivel.GetListAsignacion(IdEmpresa, IdSede, IdAnio);
            Lista_NivelPorSede.set_list(lista, IdTransaccionSession);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_AnioLectivo_Sede_NivelAcademico_List
    {
        aca_NivelAcademico_Bus bus_nivel = new aca_NivelAcademico_Bus();
        string Variable = "aca_AnioLectivo_Sede_NivelAcademico_Info";
        public List<aca_AnioLectivo_Sede_NivelAcademico_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Sede_NivelAcademico_Info> list = new List<aca_AnioLectivo_Sede_NivelAcademico_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Sede_NivelAcademico_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Sede_NivelAcademico_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}