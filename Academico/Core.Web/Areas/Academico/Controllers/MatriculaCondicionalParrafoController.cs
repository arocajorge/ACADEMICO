using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Info.Helps;
using System.Web.Mvc;
using Core.Info.General;
using Core.Bus.General;
using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCondicionalParrafoController : Controller
    {
        #region Variables
        aca_MatriculaCondicionalParrafo_Bus busMatriculaParrafo = new aca_MatriculaCondicionalParrafo_Bus();
        aca_MatriculaCondicionalParrafo_List lstMatriculaCondicionalParrafo = new aca_MatriculaCondicionalParrafo_List();
        aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
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
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual)
            };
            lstMatriculaCondicionalParrafo.set_list(busMatriculaParrafo.GetList(), model.IdTransaccionSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            return View(model);

        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCondicionalParrafo()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = lstMatriculaCondicionalParrafo.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MatriculaCondicionalParrafo", model);
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            aca_MatriculaCondicionalParrafo_Info model = new aca_MatriculaCondicionalParrafo_Info()
            {
                Id = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual))

            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_MatriculaCondicionalParrafo_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            if (!busMatriculaParrafo.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Modificar(int Id = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MatriculaCondicionalParrafo_Info model = busMatriculaParrafo.GetInfo(Id);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_MatriculaCondicionalParrafo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            
            if (!busMatriculaParrafo.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { Id = model.Id, Exito = true });
        }

        public ActionResult Anular(int Id = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MatriculaCondicionalParrafo_Info model = busMatriculaParrafo.GetInfo(Id);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_MatriculaCondicionalParrafo_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!busMatriculaParrafo.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Metodos
        

        private void cargar_combos()
        {
            var lst_tipo_condicional = bus_catalogo.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.CONDIC), false);
            ViewBag.lst_tipo_condicional = lst_tipo_condicional;
        }
        #endregion

    }

    public class aca_MatriculaCondicionalParrafo_List
    {
        string Variable = "aca_MatriculaCondicionalParrafo_Info";
        public List<aca_MatriculaCondicionalParrafo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCondicionalParrafo_Info> list = new List<aca_MatriculaCondicionalParrafo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCondicionalParrafo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCondicionalParrafo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}