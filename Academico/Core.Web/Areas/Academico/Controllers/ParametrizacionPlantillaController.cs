using Core.Bus.Contabilidad;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Info.Academico;
using Core.Bus.Academico;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ParametrizacionPlantillaController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            cl_filtros_Info model = new cl_filtros_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal)
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(cl_filtros_Info model)
        {
            return View(model);
        }
        #endregion

        #region Combos bajo demanda
        ct_plancta_Bus bus_plancta = new ct_plancta_Bus();
        public ActionResult CmbCuentaDebe()
        {
            aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info model = new aca_AnioLectivo_Curso_Plantilla_Parametrizacion_Info();
            return PartialView("_CmbCuentaDebe", model);
        }
        #endregion
    }
}