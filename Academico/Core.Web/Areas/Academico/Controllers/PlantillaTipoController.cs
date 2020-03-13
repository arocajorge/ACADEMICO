using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Info.Academico;
using Core.Web.Helps;
using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Helps;
using Core.Info.General;

namespace Core.Web.Areas.Academico.Controllers
{
    public class PlantillaTipoController : Controller
    {
        #region Variables
        aca_PlantillaTipo_Bus busPlantillaTipo = new aca_PlantillaTipo_Bus();
        #endregion

        // GET: Academico/PlantillaTipo
        public ActionResult Index()
        {
            return View();
        }
    }
}