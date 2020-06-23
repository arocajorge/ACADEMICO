using Core.Bus.Academico;
using Core.Bus.Contabilidad;
using Core.Info.Academico;
using Core.Info.Contabilidad;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Contabilidad.Controllers
{
    public class AnioFiscalController : Controller
    {
        #region Index

        ct_anio_fiscal_Bus bus_anio_fiscal = new ct_anio_fiscal_Bus();
        ct_anio_fiscal_x_cuenta_utilidad_Bus bus_aniocta = new ct_anio_fiscal_x_cuenta_utilidad_Bus();
        string mensaje = string.Empty;
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        public ActionResult Index()
        {
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "AnioFiscal", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_anio_fiscal(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            var model = bus_anio_fiscal.get_list(true);
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_anio_fiscal", model);
        }
        private void cargar_combos(int IdEmpresa)
        {
            ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
            var lst_ctacble = bus_plancta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cuentas = lst_ctacble;
        }
        #endregion

        #region Metodos ComboBox bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuenta_Anio()
        {
            string model = "";
            return PartialView("_CmbCuenta_Anio", model);
        }
        public ActionResult CmbCuenta_Anio_Cierre()
        {
            string model = "";
            return PartialView("_CmbCuenta_Anio_Cierre", model);
        }
        public List<ct_plancta_Info> get_list_bajo_demanda(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }



        public List<ct_plancta_Info> get_list_bajo_demanda_Cierre(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_plancta.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), false);
        }
        public ct_plancta_Info get_info_bajo_demanda_Cierre(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_plancta.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa));
        }

        #endregion

        private bool validar(ct_anio_fiscal_Info i_validar, ref string msg)
        {
            if (string.IsNullOrEmpty(i_validar.info_anio_ctautil.IdCtaCble))
            {
                msg = "El campo cuenta contable es obligatorio";
                return false;
            }
            return true;
        }

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdanioFiscal = 0)
        {
            ct_anio_fiscal_Info model = new ct_anio_fiscal_Info
            {
                af_fechaIni = DateTime.Now,
                af_fechaFin = DateTime.Now,
                info_anio_ctautil = new ct_anio_fiscal_x_cuenta_utilidad_Info()
            };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "AnioFiscal", "Index");
            if (!info.Nuevo)
                return RedirectToAction("Index");
            #endregion
            cargar_combos(IdEmpresa);
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(ct_anio_fiscal_Info model)
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            model.lst_periodo = new List<ct_periodo_Info>();
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            if (bus_anio_fiscal.validar_existe_Idanio(model.IdanioFiscal))
            {
                ViewBag.mensaje = "El año ya se encuentra registrado";
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            model.info_anio_ctautil.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var mes_ini = model.af_fechaIni.Month;
            var mes_fin = model.af_fechaFin.Month;
            var meses = mes_fin - mes_ini;
            for (int i = mes_ini; i <= mes_fin; i++)
            {
                var mes = i.ToString().PadLeft(2, '0');
                var IdPeriodo = model.IdanioFiscal + mes;
                var anio = model.IdanioFiscal;
                var ini = new DateTime(anio, Convert.ToInt32(i), 1);
                var fin = new DateTime(anio, Convert.ToInt32(i), 1).AddMonths(1).AddDays(-1);

                var info_periodo = new ct_periodo_Info
                {
                    IdEmpresa = IdEmpresa,
                    IdanioFiscal = model.IdanioFiscal,
                    IdPeriodo = Convert.ToInt32(IdPeriodo),
                    pe_mes = i,
                    pe_FechaIni = ini,
                    pe_FechaFin = fin,
                    pe_estado = "A",
                    pe_cerrado = "N"
                };

                model.lst_periodo.Add(info_periodo);
            }

            if (!bus_anio_fiscal.guardarDB(model))
            {
                model.info_anio_ctautil.IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
                model.info_anio_ctautil.IdanioFiscal = model.IdanioFiscal;
                bus_aniocta.guardarDB(model.info_anio_ctautil);
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return RedirectToAction("Index");
            }
            return RedirectToAction("Consultar", new { IdanioFiscal = model.IdanioFiscal, Exito = true });
        }

        public ActionResult Consultar(int IdanioFiscal = 0, bool Exito=false)
        {
            ct_anio_fiscal_Info model = bus_anio_fiscal.get_info(IdanioFiscal);
            if (model == null)
                return RedirectToAction("Index");
            model.info_anio_ctautil = bus_aniocta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdanioFiscal);
            if (model.info_anio_ctautil == null)
                model.info_anio_ctautil = new ct_anio_fiscal_x_cuenta_utilidad_Info
                {
                    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                    IdanioFiscal = model.IdanioFiscal
                };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "AnioFiscal", "Index");
            if (model.af_estado == "I")
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

            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            return View(model);
        }

        public ActionResult Modificar(int IdanioFiscal = 0)
        {
            ct_anio_fiscal_Info model = bus_anio_fiscal.get_info(IdanioFiscal);
            if (model == null)
                return RedirectToAction("Index");
            model.info_anio_ctautil = bus_aniocta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdanioFiscal);
            if (model.info_anio_ctautil == null)
                model.info_anio_ctautil = new ct_anio_fiscal_x_cuenta_utilidad_Info
                {
                    IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                    IdanioFiscal = model.IdanioFiscal
                };
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "AnioFiscal", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(ct_anio_fiscal_Info model)
        {
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            if (!bus_anio_fiscal.modificarDB(model))
            {
                model.info_anio_ctautil.IdEmpresa = model.info_anio_ctautil.IdEmpresa;
                model.info_anio_ctautil.IdanioFiscal = model.IdanioFiscal;
                bus_aniocta.eliminarDB(Convert.ToInt32(SessionFixed.IdEmpresa), model.IdanioFiscal);
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdanioFiscal = model.IdanioFiscal, Exito = true });
        }
        public ActionResult Anular(int IdanioFiscal = 0)
        {
            ct_anio_fiscal_Info model = bus_anio_fiscal.get_info(IdanioFiscal);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Contabilidad", "AnioFiscal", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            model.info_anio_ctautil = bus_aniocta.get_info(Convert.ToInt32(SessionFixed.IdEmpresa), IdanioFiscal);
            if (model.info_anio_ctautil == null)
                model.info_anio_ctautil = new ct_anio_fiscal_x_cuenta_utilidad_Info();
            cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(ct_anio_fiscal_Info model)
        {
            if (!bus_anio_fiscal.anularDB(model))
            {
                cargar_combos(Convert.ToInt32(SessionFixed.IdEmpresa));
                return View(model);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }

    public class ct_anio_fiscal_List
    {
        string Variable = "ct_anio_fiscal_Info";
        public List<ct_anio_fiscal_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession] == null)
            {
                List<ct_anio_fiscal_Info> list = new List<ct_anio_fiscal_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
            }
            return (List<ct_anio_fiscal_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession];
        }

        public void set_list(List<ct_anio_fiscal_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession] = list;
        }
    }
}