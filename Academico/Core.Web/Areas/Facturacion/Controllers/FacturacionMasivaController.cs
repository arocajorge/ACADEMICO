using Core.Bus.Academico;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class FacturacionMasivaController : Controller
    {
        #region Variables
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        fa_PuntoVta_Bus bus_punto_venta = new fa_PuntoVta_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        aca_AnioLectivo_Periodo_Bus bus_periodo = new aca_AnioLectivo_Periodo_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        aca_AnioLectivo_Periodo_FactMasiva_List Lista_FactMasiva = new aca_AnioLectivo_Periodo_FactMasiva_List();
        aca_Matricula_Rubro_FactMasiva_List Lista_FactMasivaDet = new aca_Matricula_Rubro_FactMasiva_List();
        aca_Matricula_Rubro_Bus bus_MatRubro = new aca_Matricula_Rubro_Bus();
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);

            aca_AnioLectivo_Periodo_Info model = new aca_AnioLectivo_Periodo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_AnioLectivo_Periodo_Info> lista = bus_periodo.getList_AnioCurso(model.IdEmpresa);
            Lista_FactMasiva.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "FacturacionMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_AnioLectivo_Periodo_Info model)
        {
            List<aca_AnioLectivo_Periodo_Info> lista = bus_periodo.getList_AnioCurso(model.IdEmpresa);
            Lista_FactMasiva.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "FacturacionMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_FacturacionMasiva()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_AnioLectivo_Periodo_Info> model = Lista_FactMasiva.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_FacturacionMasiva", model);
        }
        #endregion

        #region Detalle
        [ValidateInput(false)]
        public ActionResult GridViewPartial_FacturacionMasivaDet()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_FactMasivaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_FacturacionMasivaDet", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            int IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            bool EsContador = Convert.ToBoolean(SessionFixed.EsContador);

            var lst_sucursal = bus_sucursal.get_list(IdEmpresa, false);
            ViewBag.lst_sucursal = lst_sucursal;

            var lst_ptoventa = bus_punto_venta.GetListUsuario(IdEmpresa, IdSucursal, false, SessionFixed.IdUsuario, EsContador, "FACT");
            ViewBag.lst_ptoventa = lst_ptoventa;
        }
        #endregion

        #region Acciones
        public ActionResult Consultar(int IdEmpresa = 0, int IdAnio=0, int IdPeriodo = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "FacturacionMasiva", "Index");
            ViewBag.Nuevo = inf.Nuevo;
            ViewBag.Modificar = inf.Modificar;
            ViewBag.Anular = inf.Anular;
            #endregion

            aca_AnioLectivo_Periodo_Info model = bus_periodo.GetInfo(IdEmpresa, IdAnio, IdPeriodo);
            model.IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_det_fact_masiva = bus_MatRubro.getList_FactMasiva(IdEmpresa, IdAnio, IdPeriodo);
            Lista_FactMasivaDet.set_list(model.lst_det_fact_masiva, model.IdTransaccionSession);
            ViewBag.MostrarBoton = true;
            if (model.Procesado==true)
            {
                ViewBag.MostrarBoton = false;
            }

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Consultar(aca_AnioLectivo_Periodo_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            model.lst_det_fact_masiva = Lista_FactMasivaDet.get_list(model.IdTransaccionSession);

            var info_periodo_anterior = new aca_AnioLectivo_Periodo_Info();
            var periodo_anterior = model.IdPeriodo - 1;
            if (periodo_anterior!=0)
            {
                info_periodo_anterior = bus_periodo.GetInfo(model.IdEmpresa, model.IdAnio, periodo_anterior);
            }

            if (info_periodo_anterior!=null && (info_periodo_anterior.Procesado==false || info_periodo_anterior.Procesado ==null))
            {
                ViewBag.mensaje = "No se puede procesar este periodo, tiene pendiente procesar el periodo anterior";
                cargar_combos();
                return View(model);
            }

            if (!bus_periodo.Modificar_FacturacionMasiva(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdAnio = model.IdAnio, IdPeriodo=model.IdPeriodo, Exito = true });
        }
        #endregion
    }

    public class aca_AnioLectivo_Periodo_FactMasiva_List
    {
        string Variable = "aca_AnioLectivo_Periodo_FactMasiva_Info";
        public List<aca_AnioLectivo_Periodo_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AnioLectivo_Periodo_Info> list = new List<aca_AnioLectivo_Periodo_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AnioLectivo_Periodo_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AnioLectivo_Periodo_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class aca_Matricula_Rubro_FactMasiva_List
    {
        string Variable = "aca_Matricula_Rubro_FactMasiva_Info";
        public List<aca_Matricula_Rubro_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Matricula_Rubro_Info> list = new List<aca_Matricula_Rubro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Matricula_Rubro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Matricula_Rubro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}