using DevExpress.Web.Mvc;
using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using Core.Web.Areas.Academico.Controllers;

namespace Core.Web.Areas.General.Controllers
{
    public class CorreoMasivoController : Controller
    {
        #region Variables
        tb_ColaCorreo_List ListaCorreo = new tb_ColaCorreo_List();
        tb_ColaCorreo_Bus bus_cola = new tb_ColaCorreo_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Curso_Paralelo_Bus bus_treelist = new aca_AnioLectivo_Curso_Paralelo_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        TreeList_List Lista_TreeList = new TreeList_List();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_alumno_PeriodoActual_List List_AlumnosPeriodoActual = new aca_alumno_PeriodoActual_List();
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
            return bus_persona.get_list_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
        }
        public tb_persona_Info get_info_bajo_demanda_alumno(ListEditItemRequestedByValueEventArgs args)
        {
            return bus_persona.get_info_bajo_demanda(args, Convert.ToInt32(SessionFixed.IdEmpresa), cl_enumeradores.eTipoPersona.ALUMNO_MATRICULA.ToString());
        }
        #endregion

        #region Metodos
        private bool validar(tb_ColaCorreo_Info info, ref string msg)
        {
            //if (bus_cola.Existe_codigo(info.IdEmpresa, info.Codigo))
            //{
            //    msg = "Ya existe registrado el codigo del reporte";
            //    return false;
            //}

            return true;
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

            tb_ColaCorreo_Info model = new tb_ColaCorreo_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdAnio = info_anio.IdAnio,
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession),
                Cuerpo = "Escribe tu mensaje",
                RepEconomico = true,
                RepLegal = true,
                lst_correo_masivo = new List<TreeList_Info>()
            };

            model.lst_correo_masivo = bus_treelist.GetList_CorreoMasivo(model.IdEmpresa, model.IdAnio);
            Lista_TreeList.set_list(model.lst_correo_masivo, model.IdTransaccionSession);

            #region Permisos
            aca_Menu_x_seg_usuario_Info info = bus_permisos.get_list_menu_accion(Convert.ToInt32(SessionFixed.IdEmpresa), Convert.ToInt32(SessionFixed.IdSede), SessionFixed.IdUsuario, "General", "ColaCorreo", "Index");
            ViewBag.Nuevo = info.Nuevo;
            ViewBag.Modificar = info.Modificar;
            ViewBag.Anular = info.Anular;
            #endregion

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(tb_ColaCorreo_Info model)
        {
            model.lst_correo_masivo = Lista_TreeList.get_list(model.IdTransaccionSession);
            var lst = model.lst_correo_masivo.Where(q=>q.Seleccionado==true);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                return View(model);
            }

            //if (!bus_cola.GuardarDB(model))
            //{
            //    ViewBag.mensaje = "No se ha podido guardar el registro";
            //    return View(model);
            //}
            return RedirectToAction("Index");
        }
        #endregion

        #region Editor
        public ActionResult HtmlEditorPartial(tb_ColaCorreo_Info model)
        {
            return PartialView("_HtmlEditorPartial", model);
        }
        #endregion

        #region TreeList
        [ValidateInput(false)]
        public ActionResult aca_AnioLectivo_ParaleloTreeList(int IdEmpresa = 0, int IdAnio = 0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<TreeList_Info> model = Lista_TreeList.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            //List<TreeList_Info> model = bus_treelist.GetList_CorreoMasivo(IdEmpresa, IdAnio);
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAnio = IdAnio;

            ViewData["selectedIDs"] = Request.Params["selectedIDs"];
            if (ViewData["selectedIDs"] == null)
            {
                int x = 0;
                string selectedIDs = "";
                foreach (var item in model.Where(q => q.Seleccionado == true).ToList())
                {
                    if (x == 0)
                        selectedIDs = item.IdString.ToString();
                    else
                        selectedIDs += "," + item.IdString.ToString();
                    x++;
                }
                ViewData["selectedIDs"] = selectedIDs;
            }
            return PartialView("_aca_AnioLectivo_ParaleloTreeList", model);
        }

        #endregion

        #region Json
        public JsonResult guardar(int IdEmpresa = 0, int Modificado = 0, string Ids = "", decimal IdAlumno=0, string Copia="", string Asunto="", string Cuerpo="", bool RepLegal=false, bool RepEconomico=false, decimal IdTransaccionSession=0)
        {
            string[] array = Ids.Split(',');
            List<TreeList_Info> lista = new List<TreeList_Info>();
            var lst_TreeList = Lista_TreeList.get_list(IdTransaccionSession);
            if (Ids != "")
            {
                var output = array.GroupBy(q => q).ToList();
                foreach (var item in lst_TreeList)
                {
                    foreach (var item2 in output)
                    {
                        if (item.IdString == Convert.ToString(item2.Key))
                        {
                            lista.Add(item);
                        }
                    }
                }
            }

            var AlumnosPorPeriodoActual = bus_alumno.GetList_PeriodoActual(IdEmpresa);
            List_AlumnosPeriodoActual.set_list(AlumnosPorPeriodoActual, IdTransaccionSession);
            var lstAlumnos = List_AlumnosPeriodoActual.get_list(IdTransaccionSession).ToList();

            var lstParalelos = lista.Where(q => q.IdString.Count() == 15).ToList();

            if (string.IsNullOrEmpty(Asunto) || string.IsNullOrEmpty(Cuerpo))
            {
                mensaje = "Es obligatorio ingresar asunto y cuerpo del correo";
            }
            else if (IdAlumno==0 && lstParalelos.Count==0)
            {
                mensaje = "Debe ingresar al menos un destinario";
            }
            else
            {
                foreach (var item in lstParalelos)
                {
                    var IdSede = Convert.ToInt32(item.IdString.Substring(0, 3));
                    var IdJornada = Convert.ToInt32(item.IdString.Substring(3, 3));
                    var IdNivel = Convert.ToInt32(item.IdString.Substring(6, 3));
                    var IdCurso = Convert.ToInt32(item.IdString.Substring(9, 3));
                    var IdParalelo = Convert.ToInt32(item.IdString.Substring(12, 3));


                    var lstAlumnosPorParalelos = lstAlumnos.Where(q => q.IdEmpresa == IdEmpresa && q.IdSede == IdSede && q.IdJornada == IdJornada && q.IdNivel == IdNivel && q.IdCurso == IdCurso && q.IdParalelo == IdParalelo).ToList();

                    foreach (var item1 in lstAlumnosPorParalelos)
                    {
                        var CorreoXAlumno = lstAlumnosPorParalelos.Where(q => q.IdEmpresa == IdEmpresa && q.IdAlumno == item1.IdAlumno).FirstOrDefault();
                        var Destinatarios = (RepLegal == true ? (CorreoXAlumno == null ? "" : CorreoXAlumno.CorreoRepLegal + ";") : "") + (RepEconomico == true ? (CorreoXAlumno == null ? "" : CorreoXAlumno.correoRepEconomico + ";") : "") + (!string.IsNullOrEmpty(Copia) ? Copia + ";" : "");

                        var info = new tb_ColaCorreo_Info
                        {
                            IdEmpresa = IdEmpresa,
                            Asunto = Asunto,
                            Cuerpo = Cuerpo,
                            Destinatarios = Destinatarios,
                            Codigo = " ",
                            Parametros = " ",
                            IdUsuarioCreacion = SessionFixed.IdUsuario
                        };

                        if (!bus_cola.GuardarDB(info))
                        {
                            mensaje = "Ha ocurrido un error al guardar los registros";
                        }
                    }
                }
                if (IdAlumno > 0)
                {
                    var CorreosAlumno = List_AlumnosPeriodoActual.get_list(IdTransaccionSession).Where(q => q.IdAlumno == IdAlumno).FirstOrDefault();
                    var DestinatarioAlumno = (RepLegal == true ? (CorreosAlumno == null ? "" : CorreosAlumno.CorreoRepLegal + ";") : "") + (RepEconomico == true ? (CorreosAlumno == null ? "" : CorreosAlumno.correoRepEconomico + ";") : "") + (!string.IsNullOrEmpty(Copia) ? Copia + ";" : "");
                    var info = new tb_ColaCorreo_Info
                    {
                        IdEmpresa = IdEmpresa,
                        Asunto = Asunto,
                        Cuerpo = Cuerpo,
                        Destinatarios = DestinatarioAlumno,
                        Codigo = " ",
                        Parametros = " ",
                        IdUsuarioCreacion = SessionFixed.IdUsuario
                    };

                    if (!bus_cola.GuardarDB(info))
                    {
                        mensaje = "No se ha podido guardar el registro";
                    }
                }
            }
            
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }



    public class TreeList_List
    {
        string Variable = "TreeList_Info";
        public List<TreeList_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<TreeList_Info> list = new List<TreeList_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<TreeList_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<TreeList_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}