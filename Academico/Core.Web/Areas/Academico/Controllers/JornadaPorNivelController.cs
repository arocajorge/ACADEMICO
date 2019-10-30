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
    public class JornadaPorNivelController : Controller
    {
        #region Variables
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_NivelAcademico_Bus bus_nivel = new aca_NivelAcademico_Bus();
        aca_Jornada_Bus bus_jornada = new aca_Jornada_Bus();
        aca_AnioLectivo_NivelAcademico_Jornada_Bus bus_NivelPorJornada = new aca_AnioLectivo_NivelAcademico_Jornada_Bus();
        aca_AnioLectivo_NivelAcademico_Jornada_List Lista_JornadaPorNivel = new aca_AnioLectivo_NivelAcademico_Jornada_List();
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
            aca_AnioLectivo_NivelAcademico_Jornada_Info model = new aca_AnioLectivo_NivelAcademico_Jornada_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdNivel = 0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista = bus_NivelPorJornada.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdNivel);
            Lista_JornadaPorNivel.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_NivelAcademico_Jornada_Info model)
        {
            List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista = bus_NivelPorJornada.GetListAsignacion(model.IdEmpresa, model.IdSede, model.IdAnio, model.IdNivel);
            Lista_JornadaPorNivel.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_JornadaPorNivel()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AnioLectivo_NivelAcademico_Jornada_Info> model = Lista_JornadaPorNivel.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_JornadaPorNivel", model);
        }
        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel=0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var resultado = 1;
            List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista = new List<aca_AnioLectivo_NivelAcademico_Jornada_Info>();
            string[] array = Ids.Split(',');

            if (Ids != "")
            {
                foreach (var item in array)
                {
                    var info_jornada = bus_jornada.GetInfo(IdEmpresa, Convert.ToInt32(item));

                    aca_AnioLectivo_NivelAcademico_Jornada_Info info = new aca_AnioLectivo_NivelAcademico_Jornada_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdSede = IdSede,
                        IdAnio = IdAnio,
                        IdNivel = IdNivel,
                        IdJornada = Convert.ToInt32(item),
                        NomJornada = info_jornada.NomJornada,
                        OrdenJornada = info_jornada.OrdenJornada
                    };
                    lista.Add(info);
                }       
            }

            if (!bus_NivelPorJornada.GuardarDB(IdEmpresa, IdSede, IdAnio, IdNivel, lista))
            {
                resultado = 0;
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListJornadaPorNivel(int IdEmpresa = 0, int IdSede = 0, int IdAnio = 0, int IdNivel = 0, decimal IdTransaccionSession = 0)
        {
            List<aca_AnioLectivo_NivelAcademico_Jornada_Info> lista = new List<aca_AnioLectivo_NivelAcademico_Jornada_Info>();
            lista = bus_NivelPorJornada.GetListAsignacion(IdEmpresa, IdSede, IdAnio, IdNivel);
            Lista_JornadaPorNivel.set_list(lista, IdTransaccionSession);
            return Json(lista, JsonRequestBehavior.AllowGet);
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
        public ActionResult ComboBoxPartial_Nivel()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            return PartialView("_ComboBoxPartial_Nivel", new aca_AnioLectivo_NivelAcademico_Jornada_Info { IdAnio = IdAnio, IdSede = IdSede });
        }
        #endregion
    }

    public class aca_AnioLectivo_NivelAcademico_Jornada_List
    {
        string Variable = "aca_AnioLectivo_NivelAcademico_Jornada_Info";
        public List<aca_AnioLectivo_NivelAcademico_Jornada_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_NivelAcademico_Jornada_Info> list = new List<aca_AnioLectivo_NivelAcademico_Jornada_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_NivelAcademico_Jornada_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_NivelAcademico_Jornada_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}