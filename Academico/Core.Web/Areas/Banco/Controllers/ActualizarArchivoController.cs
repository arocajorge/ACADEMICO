using Core.Info.Banco;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Bus.Banco;
using Core.Bus.General;
using Core.Info.General;

namespace Core.Web.Areas.Banco.Controllers
{
    public class ActualizarArchivoController : Controller
    {
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        public ActualizarArchivoController()
        {
            bus_sucursal = new tb_sucursal_Bus();
        }

        public ActionResult Index()
        {
            ba_Archivo_Transferencia_Info model = new ba_Archivo_Transferencia_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ba_Archivo_Transferencia_Info model)
        {
            return View(model);
        }

        private void CargarCombos(ba_Archivo_Transferencia_Info model)
        {
            //var lst_cuenta_bancarias = bus_cuentas_bancarias.get_list(model.IdEmpresa, model.IdSucursal, false);
            //ViewBag.lst_cuenta_bancarias = lst_cuenta_bancarias;

            //var lst_proceso = bus_procesos_bancarios.get_list(model.IdEmpresa, false);
            //ViewBag.lst_proceso = lst_proceso;

            var lst_sucursal = bus_sucursal.GetList(model.IdEmpresa, SessionFixed.IdUsuario, false);
            ViewBag.lst_sucursal = lst_sucursal;
        }
    }
}