using Core.Bus.Banco;
using Core.Bus.Contabilidad;
using Core.Bus.General;
using Core.Info.Banco;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Banco.Controllers
{
    [SessionTimeout]
    public class ParametroBancoController : Controller
    {
        #region Variables
        ba_parametros_Bus bus_parametro = new ba_parametros_Bus();
        ct_cbtecble_tipo_Bus bus_tipo_comprobante = new ct_cbtecble_tipo_Bus();
        ct_plancta_Bus bus_cta = new ct_plancta_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ba_parametros_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new ba_parametros_Info { IdEmpresa = IdEmpresa };
            cargar_combos(IdEmpresa);
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ba_parametros_Info model)
        {
            model.IdUsuario = SessionFixed.IdUsuario;
            model.IdUsuarioUltMod = SessionFixed.IdUsuario;
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";
            cargar_combos(model.IdEmpresa);
            return View(model);
        }

        #endregion

        #region Metodos
        private void cargar_combos(int IdEmpresa)
        {
            var lst_tipo_comprobante = bus_tipo_comprobante.get_list(IdEmpresa, false);
            ViewBag.lst_tipo_comprobante = lst_tipo_comprobante;

            var lst_cta = bus_cta.get_list(IdEmpresa, false, false);
            ViewBag.lst_cta = lst_cta;

            var lst_ciudad = bus_ciudad.get_list("", false);
            ViewBag.lst_ciudad = lst_ciudad;
        }

        #endregion
    }
}