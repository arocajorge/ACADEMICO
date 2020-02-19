using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Bus.Academico;
using Core.Info.Academico;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCondicionalParrafoController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
    }
}