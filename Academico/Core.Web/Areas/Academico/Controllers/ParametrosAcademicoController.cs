using Core.Bus.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ParametrosAcademicoController : Controller
    {
        #region Variables
        aca_parametro_Bus bus_parametro = new aca_parametro_Bus();
        #endregion

        #region Index
        public ActionResult Index()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            aca_parametro_Info model = bus_parametro.get_info(IdEmpresa);
            if (model == null)
                model = new aca_parametro_Info { IdEmpresa = IdEmpresa };

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(aca_parametro_Info model)
        {
            if (!bus_parametro.guardarDB(model))
                ViewBag.mensaje = "No se pudieron actualizar los registros";

            return View(model);
        }

        #endregion
    }
}