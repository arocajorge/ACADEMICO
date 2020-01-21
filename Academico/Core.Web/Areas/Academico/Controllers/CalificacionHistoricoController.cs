using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class CalificacionHistoricoController : Controller
    {
        #region Variables
        aca_AnioLectivoCalificacionHistorico_List Lista_CalificacionHistorico = new aca_AnioLectivoCalificacionHistorico_List();
        aca_AnioLectivoCalificacionHistorico_Bus bus_CalificacionHistorico = new aca_AnioLectivoCalificacionHistorico_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_AnioLectivo_Sede_NivelAcademico_Bus bus_anio_nivel = new aca_AnioLectivo_Sede_NivelAcademico_Bus();
        aca_AnioLectivo_Jornada_Curso_Bus bus_anio_curso = new aca_AnioLectivo_Jornada_Curso_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivoConductaEquivalencia_Bus bus_conducta = new aca_AnioLectivoConductaEquivalencia_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
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
        public ActionResult ComboBoxPartial_Jornada()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            return PartialView("_ComboBoxPartial_Jornada", new aca_AnioLectivo_Jornada_Curso_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel });
        }

        public ActionResult ComboBoxPartial_Curso()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            int IdSede = (Request.Params["IdSede"] != null) ? int.Parse(Request.Params["IdSede"]) : -1;
            int IdNivel = (Request.Params["IdNivel"] != null) ? int.Parse(Request.Params["IdNivel"]) : -1;
            int IdJornada = (Request.Params["IdJornada"] != null) ? int.Parse(Request.Params["IdJornada"]) : -1;
            return PartialView("_ComboBoxPartial_Curso", new aca_AnioLectivo_Curso_Paralelo_Info { IdAnio = IdAnio, IdSede = IdSede, IdNivel = IdNivel, IdJornada = IdJornada });
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_AnioLectivoCalificacionHistorico_Info info)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_conducta = bus_conducta.GetList(info.IdEmpresa, info.IdAnio, false);
            ViewBag.lst_conducta = lst_conducta;
            var lst_nivel = bus_anio_nivel.GetListNivel_x_Anio(info.IdEmpresa, info.IdAnio);
            ViewBag.lst_nivel = lst_nivel;
            var lst_curso = bus_anio_curso.GetListCurso_x_Anio(info.IdEmpresa, info.IdAnio);
            ViewBag.lst_curso = lst_curso;
        }
        #endregion

        #region Index
        public ActionResult Index(decimal IdAlumno=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivoCalificacionHistorico_Info model = new aca_AnioLectivoCalificacionHistorico_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAlumno = IdAlumno,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            ViewBag.IdAlumno = model.IdAlumno;
            List<aca_AnioLectivoCalificacionHistorico_Info> lista = bus_CalificacionHistorico.GetList(model.IdEmpresa, model.IdAlumno, true);
            Lista_CalificacionHistorico.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        //[HttpPost]
        //public ActionResult Index(aca_AnioLectivoCalificacionHistorico_Info model)
        //{
        //    List<aca_AnioLectivoCalificacionHistorico_Info> lista = bus_CalificacionHistorico.GetList(model.IdEmpresa, model.IdAlumno, true);
        //    Lista_CalificacionHistorico.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

        //    return View(model);
        //}

        [ValidateInput(false)]
        public ActionResult GridViewPartial_CalificacionHistorico(decimal IdAlumno=0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ViewBag.IdAlumno = IdAlumno;
            List<aca_AnioLectivoCalificacionHistorico_Info> model = Lista_CalificacionHistorico.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_CalificacionHistorico", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa=0, decimal IdAlumno=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_AnioLectivoCalificacionHistorico_Info model = new aca_AnioLectivoCalificacionHistorico_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdAlumno = IdAlumno,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            ViewBag.IdAlumno = model.IdAlumno;
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_AnioLectivoCalificacionHistorico_Info model)
        {
            if (!bus_CalificacionHistorico.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model);
                return View(model);
            }
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, IdAlumno = model.IdAlumno, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio = 0, int IdAlumno = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_AnioLectivoCalificacionHistorico_Info model = bus_CalificacionHistorico.GetInfo(IdEmpresa, IdAnio, IdAlumno);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_AnioLectivoCalificacionHistorico_Info model)
        {
            if (!bus_CalificacionHistorico.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model);
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, IdAlumno = model.IdAlumno, Exito = true });
        }
        #endregion
    }
    public class aca_AnioLectivoCalificacionHistorico_List
    {
        string Variable = "aca_AnioLectivoCalificacionHistorico_Info";
        public List<aca_AnioLectivoCalificacionHistorico_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivoCalificacionHistorico_Info> list = new List<aca_AnioLectivoCalificacionHistorico_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivoCalificacionHistorico_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivoCalificacionHistorico_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}