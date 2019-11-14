using Core.Bus.Academico;
using Core.Data.Academico;
using Core.Info.Academico;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class PlantillaController : Controller
    {
        #region Variables
        aca_Rubro_Bus bus_rubro = new aca_Rubro_Bus();
        aca_AnioLectivo_Rubro_Bus bus_rubro_anio = new aca_AnioLectivo_Rubro_Bus();
        aca_AnioLectivo_Rubro_Periodo_Bus bus_rubro_anio_periodo = new aca_AnioLectivo_Rubro_Periodo_Bus();
        aca_Plantilla_List Lista_Plantilla = new aca_Plantilla_List();
        aca_Plantilla_Rubro_List Lista_PlantillaRubro = new aca_Plantilla_Rubro_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Plantilla_Bus bus_plantilla = new aca_Plantilla_Bus();
        aca_Plantilla_Rubro_Bus bus_plantilla_rubro = new aca_Plantilla_Rubro_Bus();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        #endregion

        #region Metodos ComboBox bajo demanda
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_Plantilla_Info());
        }
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
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_Plantilla_Info model = new aca_Plantilla_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>()
            };

            List<aca_Plantilla_Info> lista = bus_plantilla.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_Plantilla.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Plantilla_Info model)
        {
            List<aca_Plantilla_Info> lista = bus_plantilla.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_Plantilla.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_Plantilla()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Plantilla_Info> model = Lista_Plantilla.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Plantilla", model);
        }
        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int IdAnio = 0, string NomPlantilla = "", decimal Valor = 0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var mensaje = "";

            aca_Plantilla_Info info_plantilla = new aca_Plantilla_Info
            {
                IdEmpresa = IdEmpresa,
                IdAnio = IdAnio,
                NomPlantilla = NomPlantilla,
                Valor = Valor,
                lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>()
            };

            string[] array = Ids.Split(',');
            if (Ids != "")
            {
                var lista_det = Lista_PlantillaRubro.get_list(IdTransaccionSession);
                foreach (var item in array)
                {
                    var info = lista_det.Where(q=>q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdRubro == Convert.ToInt32(item) ).FirstOrDefault();
                    aca_Plantilla_Rubro_Info info_det = new aca_Plantilla_Rubro_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdAnio = IdAnio,
                        IdRubro = info.IdRubro,
                        IdProducto = info.IdProducto,
                        Subtotal = info.Subtotal,
                        IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva,
                        Porcentaje = info.Porcentaje,
                        Total = info.Total
                    };

                    info_plantilla.lst_Plantilla_Rubro.Add(info_det);
                }
            }

            if (!bus_plantilla.GuardarDB(info_plantilla))
            {
                mensaje = "No se ha podido guardar el registro";
            }

            return Json(new { msg = mensaje, info = info_plantilla }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult actualizar(int IdEmpresa = 0, int IdAnio = 0, int IdPlantilla = 0, string NomPlantilla = "", decimal Valor = 0, string Ids = "", decimal IdTransaccionSession = 0)
        {
            var mensaje = "";
            aca_Plantilla_Info info_plantilla = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
            info_plantilla.IdAnio = IdAnio;
            info_plantilla.NomPlantilla = NomPlantilla;
            info_plantilla.Valor = Valor;
            info_plantilla.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();

            string[] array = Ids.Split(',');
            if (Ids != "")
            {
                var lista_det = Lista_PlantillaRubro.get_list(IdTransaccionSession);
                foreach (var item in array)
                {
                    var info = lista_det.Where(q => q.IdEmpresa == IdEmpresa && q.IdAnio == IdAnio && q.IdRubro == Convert.ToInt32(item)).FirstOrDefault();
                    aca_Plantilla_Rubro_Info info_det = new aca_Plantilla_Rubro_Info
                    {
                        IdEmpresa = IdEmpresa,
                        IdAnio = IdAnio,
                        IdRubro = info.IdRubro,
                        IdProducto = info.IdProducto,
                        Subtotal = info.Subtotal,
                        IdCod_Impuesto_Iva = info.IdCod_Impuesto_Iva,
                        Porcentaje = info.Porcentaje,
                        Total = info.Total
                    };

                    info_plantilla.lst_Plantilla_Rubro.Add(info_det);
                }
            }

            if (!bus_plantilla.ModificarDB(info_plantilla))
            {
                mensaje = "No se ha podido modificar el registro";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Acciones
        [ValidateInput(false)]
        public ActionResult GridViewPartial_PlantillaRubro()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Plantilla_Rubro_Info> model = Lista_PlantillaRubro.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PlantillaRubro", model);
        }

        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_Plantilla_Info model = new aca_Plantilla_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetListAsignacion(model.IdEmpresa, model.IdAnio);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_Plantilla_Info model)
        {
            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetListAsignacion(model.IdEmpresa, model.IdAnio);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAnio=0, int IdPlantilla = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Plantilla_Info model = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetListAsignacion(model.IdEmpresa, model.IdAnio);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_Plantilla_Info model)
        {
            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetListAsignacion(model.IdEmpresa, model.IdAnio);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(model.IdTransaccionSession));

            return View(model);
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAnio = 0, int IdPlantilla=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Plantilla_Info model = bus_plantilla.GetInfo(IdEmpresa, IdAnio, IdPlantilla);
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession);
            if (model == null)
                return RedirectToAction("Index");

            model.lst_Plantilla_Rubro = new List<aca_Plantilla_Rubro_Info>();
            model.lst_Plantilla_Rubro = bus_plantilla_rubro.GetListAsignacion(model.IdEmpresa, model.IdAnio);
            Lista_PlantillaRubro.set_list(model.lst_Plantilla_Rubro, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Plantilla_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_plantilla.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";

                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion
    }

    public class aca_Plantilla_List
    {
        string Variable = "aca_Plantilla_Info";
        public List<aca_Plantilla_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Plantilla_Info> list = new List<aca_Plantilla_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Plantilla_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Plantilla_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class aca_Plantilla_Rubro_List
    {
        string Variable = "aca_Plantilla_Rubro_Info";
        public List<aca_Plantilla_Rubro_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Plantilla_Rubro_Info> list = new List<aca_Plantilla_Rubro_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Plantilla_Rubro_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Plantilla_Rubro_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}