using DevExpress.Web.Mvc;
using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;

namespace Core.Web.Areas.General.Controllers
{
    public class CorreoMasivoController : Controller
    {
        #region Variables
        tb_ColaCorreo_List ListaCorreo = new tb_ColaCorreo_List();
        tb_ColaCorreo_Bus bus_cola = new tb_ColaCorreo_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Curso_Paralelo_Bus bus_treelist = new aca_AnioLectivo_Curso_Paralelo_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_AnioLectivo_Curso_Paralelo_TreeList Lista_TreeList = new aca_AnioLectivo_Curso_Paralelo_TreeList();
        string mensaje = string.Empty;
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
        }
        #endregion

        #region Metodos
        private bool validar(tb_ColaCorreo_Info info, ref string msg)
        {
            //if (bus_cola.Existe_codigo(info.IdEmpresa, info.Codigo))
            //{
            //    msg = "Ya existe registrado el codigo del reporte";
            //    return false;
            //}

            return true;
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);

            tb_ColaCorreo_Info model = new tb_ColaCorreo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = info_anio.IdAnio,
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                Cuerpo = "Escribe tu mensaje",
                lst_correo_masivo = new List<aca_AnioLectivo_Curso_Paralelo_Info>()
            };

            //model.lst_correo_masivo = bus_treelist.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            //Lista_TreeList.set_list(model.lst_correo_masivo, model.IdTransaccionSession);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "General", "ColaCorreo", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(tb_ColaCorreo_Info model)
        {
            model.lst_correo_masivo = Lista_TreeList.get_list(model.IdTransaccionSession);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            if (!bus_cola.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                return View(model);
            }
            return RedirectToAction("Nuevo");
        }
        #endregion

        #region Editor
        public ActionResult HtmlEditorPartial(tb_ColaCorreo_Info model)
        {
            return PartialView("_HtmlEditorPartial", model);
        }
        #endregion

        #region TreeList
        [ValidateInput(false)]
        public ActionResult TreeListPartial_CorreoMasivo(int IdEmpresa = 0, int IdAnio = 0, int IdSede=0)
        {
            //SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            //List<aca_AnioLectivo_Curso_Paralelo_Info> model = Lista_TreeList.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            List<aca_AnioLectivo_Curso_Paralelo_Info> model = bus_treelist.GetList_CorreoMAsivo(IdEmpresa, IdSede, IdAnio);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAnio = IdAnio;
            ViewBag.IdSede = IdSede;
            ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            if (ViewData["selectedIDs"] == null)
            {
                int x = 0;
                string selectedIDs = "";
                foreach (var item in model.Where(q => q.seleccionado == true).ToList())
                {
                    if (x == 0)
                        selectedIDs = item.IdString.ToString();
                    else
                        selectedIDs += "," + item.IdString.ToString();
                    x++;
                }
                ViewData["selectedIDs"] = selectedIDs;
            }
            return PartialView("_TreeListPartial_CorreoMasivo", model);
        }

        #endregion

    }



    public class aca_AnioLectivo_Curso_Paralelo_TreeList
    {
        string Variable = "aca_AnioLectivo_Curso_Paralelo_TreeList_Info";
        public List<aca_AnioLectivo_Curso_Paralelo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Curso_Paralelo_Info> list = new List<aca_AnioLectivo_Curso_Paralelo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Curso_Paralelo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Curso_Paralelo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}