using Core.Bus.Academico;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class AdmisionController : Controller
    {
        #region Variables
        aca_Admision_Bus bus_admision = new aca_Admision_Bus();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
        aca_Admision_List Lista_Admision= new aca_Admision_List();
        //aca_Familia_List Lista_Familia = new aca_Familia_List();
        //aca_AlumnoDocumento_List ListaAlumnoDocumento = new aca_AlumnoDocumento_List();
        aca_SocioEconomico_Bus bus_socioeconomico = new aca_SocioEconomico_Bus();
        tb_profesion_Bus bus_profesion = new tb_profesion_Bus();
        aca_CatalogoFicha_Bus bus_catalogo_ficha = new aca_CatalogoFicha_Bus();
        fa_cliente_tipo_Bus bus_clientetipo = new fa_cliente_tipo_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        fa_cliente_contactos_Bus bus_cliente_cont = new fa_cliente_contactos_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_Documento_Bus bus_documento = new aca_Documento_Bus();
        tb_Religion_Bus bus_religion = new tb_Religion_Bus();
        tb_GrupoEtnico_Bus bus_grupoetnico = new tb_GrupoEtnico_Bus();
        aca_parametro_Bus bus_parametro = new aca_parametro_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        #endregion

        #region Combos bajo demanda
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

        #region Index
        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa),0);
            aca_Admision_Info model = new aca_Admision_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info_anio==null?0: info_anio.IdAnio), 
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Admision_Info> lista = bus_admision.GetList(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_Admision.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "Admision", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Admision(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_Admision_Info> model = Lista_Admision.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            return PartialView("_GridViewPartial_Admision", model);
        }
        #endregion

        #region JSON
        public JsonResult CantidadAdmisiones(string IdUsuario = "")
        {
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            var IdAnio = (info_anio == null ? 0 : info_anio.IdAnio);
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var IdSede = Convert.ToInt32(SessionFixed.IdSede);

            var lst_admisiones = bus_admision.GetList(IdEmpresa, IdSede, IdAnio);
            var cantidad = lst_admisiones.Count();
            return Json(cantidad, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_Admision_List
    {
        string Variable = "aca_Admision_Info";
        public List<aca_Admision_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Admision_Info> list = new List<aca_Admision_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Admision_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Admision_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}