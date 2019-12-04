using Core.Web.Helps;
using Core.Web.Reportes.Academico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Reportes.Controllers
{
    public class AcademicoController : Controller
    {
        public ActionResult ACA_001(int IdEmpresa = 0, int IdMatricula = 0)
        {
            ACA_001_Rpt model = new ACA_001_Rpt();
            model.p_IdEmpresa.Value = IdEmpresa;
            model.p_IdMatricula.Value = IdMatricula;

            model.usuario = SessionFixed.IdUsuario;
            model.empresa = SessionFixed.NomEmpresa;
            return View(model);
        }
    }
}