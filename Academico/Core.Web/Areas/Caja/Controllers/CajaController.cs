using Core.Bus.Academico;
using Core.Bus.Caja;
using Core.Bus.Contabilidad;
using Core.Bus.General;
using Core.Bus.SeguridadAcceso;
using Core.Info.Academico;
using Core.Info.Caja;
using Core.Info.Contabilidad;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Caja.Controllers
{
    [SessionTimeout]
    public class CajaController : Controller
    {
        #region Variables
        caj_Caja_Bus bus_caja = new caj_Caja_Bus();
        caj_Caja_x_seg_usuario_Bus bus_caja_responsables = new caj_Caja_x_seg_usuario_Bus();
        seg_usuario_Bus bus_usuario = new seg_usuario_Bus();
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        caj_Caja_x_seg_usuario_List Lista_caj_Caja_x_seg_usuario = new caj_Caja_x_seg_usuario_List();
        string mensaje = string.Empty;
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda

        public ActionResult CmbCuenta_Caja()
        {
            caj_Caja_Info model = new caj_Caja_Info();
            return PartialView("_CmbCuenta_Caja", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Caja", "Caja", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos(model.IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Caja", "Caja", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            cargar_combos(model.IdEmpresa);
            return View(model);
        }


        [ValidateInput(false)]
        public ActionResult GridViewPartial_caja(int IdSucursal = 0, bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            bool EsContador = Convert.ToBoolean(SessionFixed.EsContador);
            ViewBag.IdSucursal = IdSucursal;
            var model = bus_caja.GetList(IdEmpresa, IdSucursal, true, SessionFixed.IdUsuario, EsContador);
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_caja", model);
        }

        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_cuentas = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_cuentas;

            var lst_sucursal = bus_sucursal.GetList(IdEmpresa, Convert.ToString(SessionFixed.IdUsuario), false);
            ViewBag.lst_sucursal = lst_sucursal;
        }

        #endregion

        #region Metodos del detalle
        public ActionResult GridViewPartial_caja_usuario()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_detalle();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_caj_Caja_x_seg_usuario.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_caja_usuario", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] caj_Caja_x_seg_usuario_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                Lista_caj_Caja_x_seg_usuario.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_caj_Caja_x_seg_usuario.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle();
            return PartialView("_GridViewPartial_caja_usuario", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] caj_Caja_x_seg_usuario_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                Lista_caj_Caja_x_seg_usuario.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var model = Lista_caj_Caja_x_seg_usuario.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_caja_usuario", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            Lista_caj_Caja_x_seg_usuario.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_caj_Caja_x_seg_usuario.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_caja_usuario", model);
        }

        private void cargar_combos_detalle()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            var lst_responsable = bus_usuario.get_list(false);
            ViewBag.lst_responsable = lst_responsable;
        }

        private bool Validar(caj_Caja_Info i_validar, ref string msg)
        {
            i_validar.ListaResponsables = Lista_caj_Caja_x_seg_usuario.get_list(i_validar.IdTransaccionSession);

            foreach (var item1 in i_validar.ListaResponsables)
            {
                var contador = 0;
                foreach (var item2 in i_validar.ListaResponsables)
                {
                    if (item1.IdUsuario == item2.IdUsuario)
                    {
                        contador++;
                    }

                    if (contador > 1)
                    {
                        mensaje = "Existen usuarios repetidos en el detalle";
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion        

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            caj_Caja_Info model = new caj_Caja_Info
            {
                IdEmpresa = IdEmpresa,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
            };
            model.ListaResponsables = new List<caj_Caja_x_seg_usuario_Info>();
            Lista_caj_Caja_x_seg_usuario.set_list(model.ListaResponsables, model.IdTransaccionSession);
            cargar_combos(IdEmpresa);
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Caja", "Caja", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(caj_Caja_Info model)
        {
            model.ListaResponsables = Lista_caj_Caja_x_seg_usuario.get_list(model.IdTransaccionSession);
            model.IdUsuario = SessionFixed.IdUsuario;
            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            if (!bus_caja.guardarDB(model))
            {
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCaja = model.IdCaja, Exito = true });
        }
        public ActionResult Consultar(int IdEmpresa = 0, int IdCaja = 0, bool Exito = false)
        {
            caj_Caja_Info model = bus_caja.get_info(IdEmpresa, IdCaja);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaResponsables = bus_caja_responsables.get_list(model.IdEmpresa, IdCaja);
            Lista_caj_Caja_x_seg_usuario.set_list(model.ListaResponsables, model.IdTransaccionSession);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Caja", "Caja", "Index");
            if (model.Estado == "I")
            {
                info.Modificar = false;
                info.Anular = false;
            }
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos(IdEmpresa);
            return View(model);
        }
        public ActionResult Modificar(int IdEmpresa = 0, int IdCaja = 0, bool Exito=false)
        {
            caj_Caja_Info model = bus_caja.get_info(IdEmpresa, IdCaja);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaResponsables = bus_caja_responsables.get_list(model.IdEmpresa, IdCaja);
            Lista_caj_Caja_x_seg_usuario.set_list(model.ListaResponsables, model.IdTransaccionSession);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Caja", "Caja", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(caj_Caja_Info model)
        {
            model.ListaResponsables = Lista_caj_Caja_x_seg_usuario.get_list(model.IdTransaccionSession);
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            if (!bus_caja.modificarDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdCaja = model.IdCaja, Exito = true });
        }
        public ActionResult Anular(int IdEmpresa = 0, int IdCaja = 0)
        {
            caj_Caja_Info model = bus_caja.get_info(IdEmpresa, IdCaja);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            model.ListaResponsables = bus_caja_responsables.get_list(model.IdEmpresa, IdCaja);
            Lista_caj_Caja_x_seg_usuario.set_list(model.ListaResponsables, model.IdTransaccionSession);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Caja", "Caja", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(caj_Caja_Info model)
        {
            model.IdUsuarioUltAnu = SessionFixed.IdUsuario;
            model.ListaResponsables = Lista_caj_Caja_x_seg_usuario.get_list(model.IdTransaccionSession);

            if (!Validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model.IdEmpresa);
                return View(model);
            }

            if (!bus_caja.anularDB(model))
            {
                cargar_combos(model.IdEmpresa);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }

    public class caj_Caja_x_seg_usuario_List
    {
        string Variable = "caj_Caja_x_seg_usuario_Info";
        public List<caj_Caja_x_seg_usuario_Info> get_list(decimal IdTransaccionSession)
        {

            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<caj_Caja_x_seg_usuario_Info> list = new List<caj_Caja_x_seg_usuario_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<caj_Caja_x_seg_usuario_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<caj_Caja_x_seg_usuario_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(caj_Caja_x_seg_usuario_Info info_det, decimal IdTransaccionSession)
        {
            List<caj_Caja_x_seg_usuario_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
            info_det.IdUsuario = info_det.IdUsuario;

            list.Add(info_det);
        }

        public void UpdateRow(caj_Caja_x_seg_usuario_Info info_det, decimal IdTransaccionSession)
        {
            caj_Caja_x_seg_usuario_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).First();
            edited_info.IdUsuario = info_det.IdUsuario;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<caj_Caja_x_seg_usuario_Info> list = get_list(IdTransaccionSession);
            list.Remove(list.Where(m => m.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}