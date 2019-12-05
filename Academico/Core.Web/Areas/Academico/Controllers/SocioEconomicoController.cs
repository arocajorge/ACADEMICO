using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class SocioEconomicoController : Controller
    {
        #region Variables
        aca_SocioEconomico_Bus bus_socio_economico = new aca_SocioEconomico_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_Matricula_List Lista_Matricula = new aca_Matricula_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_SocioEconomico_List Lista_SocioEconomica = new aca_SocioEconomico_List();
        aca_CatalogoFicha_Bus bus_catalogo_socioeconomico = new aca_CatalogoFicha_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        #endregion

        #region Combos bajo demanada
        public ActionResult Cmb_Alumno()
        {
            decimal model = new decimal();
            return PartialView("_CmbAlumno", model);
        }
        public List<tb_persona_Info> get_list_bajo_demanda_alumno(ListEditItemsRequestedByFilterConditionEventArgs args)
        {
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO.ToString());
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_vivienda = bus_catalogo_socioeconomico.GetList_x_Tipo( Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.VIVIENDA), false);
            ViewBag.lst_vivienda = lst_vivienda;
            var lst_tipo_vivienda = bus_catalogo_socioeconomico.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.TIPOVIVIENDA), false);
            ViewBag.lst_tipo_vivienda = lst_tipo_vivienda;
            var lst_agua = bus_catalogo_socioeconomico.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.AGUA), false);
            ViewBag.lst_agua = lst_agua;
            var lst_ing_institucion = bus_catalogo_socioeconomico.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.MOTIVOING), false);
            ViewBag.lst_ing_institucion = lst_ing_institucion;
            var lst_institucion = bus_catalogo_socioeconomico.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.INSTITUCION), false);
            ViewBag.lst_institucion = lst_institucion;
            var lst_financiamiento = bus_catalogo_socioeconomico.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.ESTUDIOS), false);
            ViewBag.lst_financiamiento = lst_financiamiento;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, decimal IdAlumno=0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_SocioEconomico_Info model = new aca_SocioEconomico_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAlumno = IdAlumno,
                SI_HERM = false,
                NO_HERM = true,
                SI_ENERG = true,
                NO_ENERG = false
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_SocioEconomico_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!bus_socio_economico.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSocioEconomico = model.IdSocioEconomico });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdSocioEconomico = 0, bool Exito=false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_SocioEconomico_Info model = bus_socio_economico.GetInfo(IdEmpresa, IdSocioEconomico);

            if (model == null)
                return RedirectToAction("Index", "Matricula");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Modificar(aca_SocioEconomico_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!bus_socio_economico.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdSocioEconomico = model.IdSocioEconomico });
        }
        #endregion
    }

    public class aca_SocioEconomico_List
    {
        string Variable = "aca_SocioEconomico_Info";
        public List<aca_SocioEconomico_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_SocioEconomico_Info> list = new List<aca_SocioEconomico_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_SocioEconomico_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_SocioEconomico_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}