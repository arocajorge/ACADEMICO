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
    public class AlumnoFamiliaController : Controller
    {
        #region Variables
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        aca_Familia_List Lista_Familia = new aca_Familia_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        #endregion

        #region Index
        public ActionResult Index(int IdEmpresa = 0, int IdAlumno = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;
            aca_Familia_Info model = new aca_Familia_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Familia_Info> lista = bus_familia.GetList(IdEmpresa, IdAlumno);
            Lista_Familia.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnoFamilia()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Familia_Info> model = Lista_Familia.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AlumnoFamilia", model);
        }
        #endregion

        #region Metodos ComboBox bajo demanda cliente
        public ActionResult Cmb_FamiliaAlumno()
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

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdAlumno = 0)
        {
            aca_Familia_Info model = new aca_Familia_Info
            {
                IdEmpresa = IdEmpresa,
                IdAlumno = IdAlumno
            };
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;

            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Familia_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!bus_familia.guardarDB(model))
            {
                ViewBag.IdAlumno = model.IdAlumno;
                ViewBag.IdEmpresa = model.IdEmpresa;
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAlumno = 0, int Secuencia = 0)
        {
            aca_Familia_Info model = bus_familia.GetInfo(IdEmpresa, IdAlumno, Secuencia);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdAlumno = IdAlumno });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_Familia_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            if (!bus_familia.modificarDB(model))
            {
                ViewBag.IdAlumno = model.IdAlumno;
                ViewBag.IdEmpresa = model.IdEmpresa;
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAlumno = 0, int Secuencia = 0)
        {
            aca_Familia_Info model = bus_familia.GetInfo(IdEmpresa, IdAlumno, Secuencia);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdAlumno = IdAlumno });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;

            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(aca_Familia_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_familia.eliminarDB(model))
            {
                ViewBag.IdAlumno = model.IdAlumno;
                ViewBag.IdEmpresa = model.IdEmpresa;
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno });
        }

        #endregion
    }

    public class aca_Familia_List
    {
        string Variable = "aca_Familia_Info";
        public List<aca_Familia_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Familia_Info> list = new List<aca_Familia_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Familia_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Familia_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}