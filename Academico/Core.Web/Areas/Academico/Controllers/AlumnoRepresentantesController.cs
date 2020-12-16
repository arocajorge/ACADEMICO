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
    public class AlumnoRepresentantesController : Controller
    {
        #region Variables
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_AlumnoRepresentante_List Lista_AlumnoRepresentante = new aca_AlumnoRepresentante_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda alumno
        public ActionResult CmbAlumno()
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

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Alumno_Info model = new aca_Alumno_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAlumno=0,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Alumno_Info> lista = bus_alumno.GetList_AlumnoRepresentante(model.IdEmpresa, model.IdAlumno);
            Lista_AlumnoRepresentante.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "AlumnoRepresentantes", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Alumno_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<aca_Alumno_Info> lista = bus_alumno.GetList_AlumnoRepresentante(model.IdEmpresa, model.IdAlumno);
            Lista_AlumnoRepresentante.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "AlumnoRepresentantes", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnoRepresentante()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_Alumno_Info> model = Lista_AlumnoRepresentante.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AlumnoRepresentante", model);
        }
        #endregion
    }

    public class aca_AlumnoRepresentante_List
    {
        string Variable = "aca_AlumnoRepresentante_Info";
        public List<aca_Alumno_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Alumno_Info> list = new List<aca_Alumno_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Alumno_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Alumno_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}