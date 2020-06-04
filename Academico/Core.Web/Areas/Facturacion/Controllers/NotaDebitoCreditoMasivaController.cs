using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Bus.Inventario;
using Core.Info.Academico;
using Core.Info.Facturacion;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Facturacion.Controllers
{
    public class NotaDebitoCreditoMasivaController : Controller
    {
        #region Variables
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        fa_notaCreDeb_Masiva_List Lista = new fa_notaCreDeb_Masiva_List();
        fa_notaCreDeb_MasivaDet_List ListaDet = new fa_notaCreDeb_MasivaDet_List();
        fa_notaCreDeb_Bus bus_nota = new fa_notaCreDeb_Bus();
        in_Producto_Bus bus_producto = new in_Producto_Bus();
        tb_sis_Impuesto_Bus bus_impuesto = new tb_sis_Impuesto_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        string mensaje = string.Empty;
        fa_notaCreDeb_det_Bus bus_det = new fa_notaCreDeb_det_Bus();
        fa_TipoNota_Bus bus_tipo_nota = new fa_TipoNota_Bus();
        tb_sis_Documento_Tipo_Talonario_Bus bus_talonario = new tb_sis_Documento_Tipo_Talonario_Bus();
        ct_periodo_Bus bus_periodo = new ct_periodo_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        fa_notaCreDeb_Masiva_Bus bus_NotaMasiva = new fa_notaCreDeb_Masiva_Bus();
        fa_notaCreDeb_MasivaDet_Bus bus_NotaMasivaDet = new fa_notaCreDeb_MasivaDet_Bus();
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
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                fecha_ini = DateTime.Now.AddMonths(-1),
                fecha_fin = DateTime.Now
            };

            List<fa_notaCreDeb_Masiva_Info> lista = bus_NotaMasiva.Get_list(model.IdEmpresa, model.IdSucursal, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdEmpresa);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDebitoCreditoMasiva", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            List<fa_notaCreDeb_Masiva_Info> lista = bus_NotaMasiva.Get_list(model.IdEmpresa, model.IdSucursal, model.fecha_ini, model.fecha_fin, true);
            Lista.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model.IdEmpresa);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Facturacion", "NotaDeCreditoFacturacion", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, SessionFixed.IdUsuario, true);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_NotaDebitoCreditoMasiva(DateTime? Fecha_ini, DateTime? Fecha_fin, int IdSucursal = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ViewBag.Fecha_ini = Fecha_ini == null ? DateTime.Now.Date.AddMonths(-1) : Convert.ToDateTime(Fecha_ini);
            ViewBag.Fecha_fin = Fecha_fin == null ? DateTime.Now.Date : Convert.ToDateTime(Fecha_fin);
            ViewBag.IdSucursal = IdSucursal;

            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<fa_notaCreDeb_Masiva_Info> model = Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;

            return PartialView("_GridViewPartial_NotaDebitoCreditoMasiva", model);
        }

        public ActionResult GridViewPartial_NotaDebitoCreditoDetMasiva()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = ListaDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_NotaDebitoCreditoDetMasiva", model);
        }
        #endregion
    }

    public class fa_notaCreDeb_Masiva_List
    {
        string Variable = "fa_notaCreDeb_Masiva_Info";
        public List<fa_notaCreDeb_Masiva_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_notaCreDeb_Masiva_Info> list = new List<fa_notaCreDeb_Masiva_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_notaCreDeb_Masiva_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_notaCreDeb_Masiva_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class fa_notaCreDeb_MasivaDet_List
    {
        string Variable = "fa_notaCreDeb_MasivaDet_Info";
        public List<fa_notaCreDeb_MasivaDet_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<fa_notaCreDeb_MasivaDet_Info> list = new List<fa_notaCreDeb_MasivaDet_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<fa_notaCreDeb_MasivaDet_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<fa_notaCreDeb_MasivaDet_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}