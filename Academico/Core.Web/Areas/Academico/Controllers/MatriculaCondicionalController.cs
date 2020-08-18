using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class MatriculaCondicionalController : Controller
    {
        #region Variables
        aca_CondicionalMatricula_List Lista_MatriculaCondicional = new aca_CondicionalMatricula_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_MatriculaCondicional_Bus bus_condicional = new aca_MatriculaCondicional_Bus();
        aca_MatriculaCondicional_Det_Bus bus_condicional_det = new aca_MatriculaCondicional_Det_Bus();
        aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_MatriculaCondicionalParrafo_Bus bus_parrafo = new aca_MatriculaCondicionalParrafo_Bus();
        aca_MatriculaCondicional_Det_List Lista_CondicionalDet = new aca_MatriculaCondicional_Det_List();
        string mensaje = string.Empty;
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        #endregion

        #region Metodos ComboBox bajo demanda
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

        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_MatriculaCondicional_Info());
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
            aca_MatriculaCondicional_Info model = new aca_MatriculaCondicional_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_MatriculaCondicional_Info> lista = bus_condicional.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_MatriculaCondicional.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCondicional", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_MatriculaCondicional_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            List<aca_MatriculaCondicional_Info> lista = bus_condicional.GetList(model.IdEmpresa, model.IdAnio, true);
            Lista_MatriculaCondicional.set_list(lista, Convert.ToDecimal(model.IdTransaccionSession));
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCondicional", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_MatriculaCondicional(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_MatriculaCondicional_Info> model = Lista_MatriculaCondicional.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            ViewBag.Nuevo = Nuevo;
            ViewBag.Modificar = Modificar;
            ViewBag.Anular = Anular;
            return PartialView("_GridViewPartial_MatriculaCondicional", model);
        }
        #endregion

        #region Metodos
        private bool validar(aca_MatriculaCondicional_Info info, ref string msg)
        {
            var lst_registros = bus_condicional.GetList_ExisteCondicional(info.IdEmpresa, info.IdAnio, info.IdAlumno, info.IdCatalogoCONDIC);

            if (lst_registros.Count() > 1)
            {
                msg = "Ya existe una matrícula condicional de ese tipo para el alumno en el año lectivo seleccionado";
                return false;
            }

            if (info.IdAlumno == 0)
            {
                msg = "Debe de seleccionar un alumno";
                return false;
            }

            info.lst_detalle = Lista_CondicionalDet.get_list(info.IdTransaccionSession);

            foreach (var item1 in info.lst_detalle)
            {
                var contador = 0;
                foreach (var item2 in info.lst_detalle)
                {
                    if (item1.IdParrafo == item2.IdParrafo)
                    {
                        contador++;
                    }

                    if (contador > 1)
                    {
                        mensaje = "Existen párrafos repetidos en el detalle";
                        return false;
                    }
                }
            }

            return true;
        }

        private void cargar_combos()
        {
            var lst_tipo_condicional = bus_catalogo.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.CONDIC), false);
            ViewBag.lst_tipo_condicional = lst_tipo_condicional;
        }
        private void cargar_combos_detalle()
        {
            var lst_parrafo = bus_parrafo.GetList(false);
            ViewBag.lst_parrafo = lst_parrafo;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_MatriculaCondicional_Info model = new aca_MatriculaCondicional_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = (info == null ? 0 : info.IdAnio),
                Fecha = DateTime.Now.Date,
                IdCatalogoCONDIC = Convert.ToInt32(cl_enumeradores.eCatalogoMatriculaCondicional.APROVECHAMIENTO),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            model.lst_detalle = new List<aca_MatriculaCondicional_Det_Info>();
            var lst_pararafo = bus_condicional_det.getList(model.IdEmpresa,model.IdCatalogoCONDIC);
            Lista_CondicionalDet.set_list(lst_pararafo, model.IdTransaccionSession);
            cargar_combos();
            #region Permisos
            aca_Menu_x_seg_usuario_Info inf = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCondicional", "Index");
            if (!inf.Nuevo)
                return RedirectToAction("Index");
            #endregion
            return View(model);
        }

        [HttpPost]
        public ActionResult Nuevo(aca_MatriculaCondicional_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            model.lst_detalle = Lista_CondicionalDet.get_list(model.IdTransaccionSession);
            if (!bus_condicional.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }
            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdMatriculaCondicional = model.IdMatriculaCondicional, Exito = true });
        }

        public ActionResult Consultar(int IdEmpresa = 0, int IdMatriculaCondicional = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MatriculaCondicional_Info model = bus_condicional.GetInfo(IdEmpresa, IdMatriculaCondicional);

            if (model == null)
                return RedirectToAction("Index");

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCondicional", "Index");
            if (model.Estado == false)
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

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_detalle = bus_condicional_det.getList(model.IdEmpresa, model.IdMatriculaCondicional);
            Lista_CondicionalDet.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdMatriculaCondicional = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MatriculaCondicional_Info model = bus_condicional.GetInfo(IdEmpresa, IdMatriculaCondicional);

            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCondicional", "Index");
            if (!info.Modificar)
                return RedirectToAction("Index");
            #endregion
            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_detalle = bus_condicional_det.getList(model.IdEmpresa, model.IdMatriculaCondicional);
            Lista_CondicionalDet.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_MatriculaCondicional_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            model.lst_detalle = Lista_CondicionalDet.get_list(model.IdTransaccionSession);
            if (!bus_condicional.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Consultar", new { IdEmpresa = model.IdEmpresa, IdMatriculaCondicional = model.IdMatriculaCondicional, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdMatriculaCondicional = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_MatriculaCondicional_Info model = bus_condicional.GetInfo(IdEmpresa, IdMatriculaCondicional);
            if (model == null)
                return RedirectToAction("Index");
            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "Academico", "MatriculaCondicional", "Index");
            if (!info.Anular)
                return RedirectToAction("Index");
            #endregion
            model.IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            model.lst_detalle = bus_condicional_det.getList(model.IdEmpresa, model.IdMatriculaCondicional);
            Lista_CondicionalDet.set_list(model.lst_detalle, model.IdTransaccionSession);
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_MatriculaCondicional_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_condicional.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Json
        public JsonResult SetListaDetalle(int IdEmpresa = 0, int IdCatalogoCONDIC=0)
        {
            var lst_pararafo = bus_condicional_det.getList(IdEmpresa, IdCatalogoCONDIC);
            Lista_CondicionalDet.set_list(lst_pararafo, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return Json(lst_pararafo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Metodos del detalle
        public ActionResult GridViewPartial_MatriculaCondicionalParrafo()
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            cargar_combos_detalle();
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_CondicionalDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_MatriculaCondicionalParrafo", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] aca_MatriculaCondicional_Det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                Lista_CondicionalDet.AddRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_CondicionalDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaCondicionalParrafo", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditingUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] aca_MatriculaCondicional_Det_Info info_det)
        {
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);

            if (info_det != null)
                Lista_CondicionalDet.UpdateRow(info_det, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            var model = Lista_CondicionalDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();
            return PartialView("_GridViewPartial_MatriculaCondicionalParrafo", model);
        }

        public ActionResult EditingDelete(int Secuencia)
        {
            Lista_CondicionalDet.DeleteRow(Secuencia, Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            var model = Lista_CondicionalDet.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            cargar_combos_detalle();

            return PartialView("_GridViewPartial_MatriculaCondicionalParrafo", model);
        }
        #endregion        
    }

    public class aca_CondicionalMatricula_List
    {
        string Variable = "aca_CondicionalMatricula_Info";
        public List<aca_MatriculaCondicional_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCondicional_Info> list = new List<aca_MatriculaCondicional_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCondicional_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCondicional_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }

    public class aca_MatriculaCondicional_Det_List
    {
        aca_MatriculaCondicional_Det_Bus bus_condicional_det = new aca_MatriculaCondicional_Det_Bus();

        string Variable = "aca_MatriculaCondicional_Det_Info";
        public List<aca_MatriculaCondicional_Det_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_MatriculaCondicional_Det_Info> list = new List<aca_MatriculaCondicional_Det_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_MatriculaCondicional_Det_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_MatriculaCondicional_Det_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }

        public void AddRow(aca_MatriculaCondicional_Det_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);
            List<aca_MatriculaCondicional_Det_Info> list = get_list(IdTransaccionSession);
            info_det.Secuencia = list.Count == 0 ? 1 : list.Max(q => q.Secuencia) + 1;
 
            list.Add(info_det);
        }

        public void UpdateRow(aca_MatriculaCondicional_Det_Info info_det, decimal IdTransaccionSession)
        {
            int IdEmpresa = string.IsNullOrEmpty(SessionFixed.IdEmpresa) ? 0 : Convert.ToInt32(SessionFixed.IdEmpresa);

            aca_MatriculaCondicional_Det_Info edited_info = get_list(IdTransaccionSession).Where(m => m.Secuencia == info_det.Secuencia).FirstOrDefault();
            edited_info.IdParrafo = info_det.IdParrafo;
            edited_info.Nombre = info_det.Nombre;
        }

        public void DeleteRow(int Secuencia, decimal IdTransaccionSession)
        {
            List<aca_MatriculaCondicional_Det_Info> list = get_list(IdTransaccionSession);
            var info_det = list.Where(q => q.Secuencia == Secuencia).FirstOrDefault();

            list.Remove(list.Where(q => q.Secuencia == Secuencia).FirstOrDefault());
        }
    }
}