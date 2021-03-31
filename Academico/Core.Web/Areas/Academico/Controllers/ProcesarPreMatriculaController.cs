using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ProcesarPreMatriculaController : Controller
    {
        #region Variables
        aca_PreMatricula_Bus bus_prematricula = new aca_PreMatricula_Bus();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_Matricula_Bus bus_matricula = new aca_Matricula_Bus();
        aca_PreMatricula_List Lista_PreMatricula = new aca_PreMatricula_List();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Menu_x_seg_usuario_Bus bus_permisos = new aca_Menu_x_seg_usuario_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_PreMatricula_Rubro_Bus bus_prematricula_rubro = new aca_PreMatricula_Rubro_Bus();
        aca_AnioLectivo_Periodo_Bus bus_anio_periodo = new aca_AnioLectivo_Periodo_Bus();
        aca_AnioLectivo_Paralelo_Profesor_Bus bus_materias_x_paralelo = new aca_AnioLectivo_Paralelo_Profesor_Bus();
        aca_AnioLectivoParcial_Bus bus_parcial = new aca_AnioLectivoParcial_Bus();
        aca_AlumnoDocumento_Bus bus_aludocumentos = new aca_AlumnoDocumento_Bus();
        aca_Admision_Bus bus_admision = new aca_Admision_Bus();
        #endregion

        #region Combos
        public ActionResult ComboBoxPartial_Anio()
        {
            return PartialView("_ComboBoxPartial_Anio", new aca_PreMatricula_Info());
        }
        public ActionResult ComboBoxPartial_Sede()
        {
            int IdAnio = (Request.Params["IdAnio"] != null) ? int.Parse(Request.Params["IdAnio"]) : -1;
            return PartialView("_ComboBoxPartial_Sede", new aca_PreMatricula_Info { IdAnio = IdAnio });
        }
        #endregion

        public ActionResult Index()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            var info_anio = bus_anio.GetInfo_AnioEnCurso(Convert.ToInt32(SessionFixed.IdEmpresa), 0);
            aca_PreMatricula_Info model = new aca_PreMatricula_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            var lst_PreMatricula = bus_prematricula.GetList_Procesar(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_PreMatricula.set_list(lst_PreMatricula, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_PreMatricula_Info model)
        {
            SessionFixed.IdTransaccionSessionActual = model.IdTransaccionSession.ToString();
            var lst_PreMatricula = bus_prematricula.GetList_Procesar(model.IdEmpresa, model.IdSede, model.IdAnio);
            Lista_PreMatricula.set_list(lst_PreMatricula, model.IdTransaccionSession);

            return View(model);
        }

        public ActionResult GridViewPartial_PreMatricula()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista_PreMatricula.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_PreMatricula", model);
        }

        #region JSON
        public JsonResult ProcesarPreMatricula(string IdString = "", decimal IdTransaccionSession=0)
        {
            var IdEmpresa = Convert.ToInt32(IdString.Substring(0,4));
            var IdSede = Convert.ToInt32(IdString.Substring(4, 4));
            var IdAnio = Convert.ToInt32(IdString.Substring(8, 4));
            var IdAlumno = Convert.ToInt32(IdString.Substring(12, 6));

            var matricula = bus_matricula.GetInfo_ExisteMatricula(IdEmpresa, IdAnio, IdAlumno);
            var info_PreMatricula = bus_prematricula.GetInfo_PorIdAlumno(IdEmpresa, IdSede, IdAnio, IdAlumno);
            var IdAdmision = (info_PreMatricula == null ? 0 : info_PreMatricula.IdAdmision);
            var IdPreMatricula = (info_PreMatricula == null ? 0 : info_PreMatricula.IdPreMatricula);
            var lst_PreMatricula_Detalle = bus_prematricula_rubro.GetList(IdEmpresa, IdPreMatricula);
            decimal Total = 0;
            decimal TotalProntoPago = 0;
            decimal ValorRubro = 0;
            var mensaje = "";

            if (matricula==null)
            {
                if (lst_PreMatricula_Detalle.Count > 0)
                {
                    foreach (var item in lst_PreMatricula_Detalle)
                    {
                        var info = lst_PreMatricula_Detalle.Where(q => q.IdString == item.IdString).FirstOrDefault();
                        //if (info != null)
                        //{
                        //    lst_PreMatricula_Detalle.Where(q => q.IdString == item.IdString).FirstOrDefault().seleccionado = true;
                        //}
                        if (info.EnMatricula == true)
                        {
                            var info_anio_periodo = bus_anio_periodo.GetInfo(info.IdEmpresa, info.IdAnio, info.IdPeriodo);
                            if (info.AplicaProntoPago == true)
                            {
                                if (DateTime.Now.Date <= info_anio_periodo.FechaProntoPago)
                                {
                                    ValorRubro = info.ValorProntoPago;
                                    TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                                }
                                else
                                {
                                    ValorRubro = info.Total;
                                    TotalProntoPago = TotalProntoPago + Math.Round((ValorRubro), 2, MidpointRounding.AwayFromZero);
                                }

                                Total = Total + Math.Round((info.Total), 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                ValorRubro = info.Total;
                                TotalProntoPago = TotalProntoPago + Math.Round(ValorRubro, 2, MidpointRounding.AwayFromZero);
                                Total = Total + Math.Round((info.Total), 2, MidpointRounding.AwayFromZero);
                            }
                            //item.ValorProntoPago = ValorRubro;
                        }
                    }
                }

                var info_matricula = new aca_Matricula_Info();

                if (info_PreMatricula != null)
            
                    info_matricula = new aca_Matricula_Info
                    {
                        IdEmpresa = info_PreMatricula.IdEmpresa,
                        IdAlumno = info_PreMatricula.IdAlumno,
                        IdAnio = info_PreMatricula.IdAnio,
                        IdSede = info_PreMatricula.IdSede,
                        IdNivel = info_PreMatricula.IdNivel,
                        IdJornada = info_PreMatricula.IdJornada,
                        IdCurso = info_PreMatricula.IdCurso,
                        IdParalelo = info_PreMatricula.IdParalelo,
                        IdPersonaF = info_PreMatricula.IdPersonaF,
                        IdPersonaR = info_PreMatricula.IdPersonaR,
                        IdPlantilla = info_PreMatricula.IdPlantilla,
                        IdMecanismo = info_PreMatricula.IdMecanismo,
                        Fecha = info_PreMatricula.Fecha,
                        Observacion = info_PreMatricula.Observacion,
                        IdCatalogo_FormaPago = "CRE",
                        IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoMatricula.MATRICULADO),
                        IdEmpresa_rol = info_PreMatricula.IdEmpresa_rol,
                        IdEmpleado = info_PreMatricula.IdEmpleado,
                        EsPatrocinado = info_PreMatricula.EsPatrocinado,
                        IdUsuarioCreacion = SessionFixed.IdUsuario,
                        lst_MatriculaRubro = new List<aca_Matricula_Rubro_Info>(),
                        lst_documentos = new List<aca_AlumnoDocumento_Info>(),
                        pe_cedulaRuc = info_PreMatricula.pe_cedulaRuc,
                        pe_nombreCompleto = info_PreMatricula.pe_nombreCompleto,
                        IdSucursal = info_PreMatricula.IdSucursal,
                        IdPuntoVta = info_PreMatricula.IdPuntoVta,
                        ValidaImportacionPreMatricula = true,
                        IdAdmision = IdAdmision,
                        IdPreMatricula = IdPreMatricula
                    };

                    #region Calificacion y conducta
                    var lst_materias_x_curso = bus_materias_x_paralelo.GetList(info_PreMatricula.IdEmpresa, info_PreMatricula.IdSede, info_PreMatricula.IdAnio, info_PreMatricula.IdNivel, info_PreMatricula.IdJornada, info_PreMatricula.IdCurso, info_PreMatricula.IdParalelo);
                    var lst_materias_cualitativas = lst_materias_x_curso.Where(q => q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUALI)).ToList();
                    var lst_materias_cuantitativas = lst_materias_x_curso.Where(q => q.IdCatalogoTipoCalificacion == Convert.ToInt32(cl_enumeradores.eCatalogoTipoCalificacion.CUANTI)).ToList();

                    var lst_parcial = bus_parcial.GetList_x_Tipo(info_PreMatricula.IdEmpresa, info_PreMatricula.IdSede, info_PreMatricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM1));
                    lst_parcial.AddRange(bus_parcial.GetList_x_Tipo(info_PreMatricula.IdEmpresa, info_PreMatricula.IdSede, info_PreMatricula.IdAnio, Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.QUIM2)));
                    
                    info_matricula.lst_MatriculaCalificacionCualitativa = new List<aca_MatriculaCalificacionCualitativa_Info>();
                    info_matricula.lst_calificacion_parcial = new List<aca_MatriculaCalificacionParcial_Info>();
                    info_matricula.lst_calificacion = new List<aca_MatriculaCalificacion_Info>();
                    info_matricula.lst_conducta = new List<aca_MatriculaConducta_Info>();
                    info_matricula.lst_asistencia = new List<aca_MatriculaAsistencia_Info>();
                    info_matricula.lst_MatriculaCalificacionCualitativaPromedio = new List<aca_MatriculaCalificacionCualitativaPromedio_Info>();

                    if (lst_materias_cuantitativas != null && lst_materias_cuantitativas.Count > 0)
                    {
                        foreach (var item in lst_materias_cuantitativas)
                        {
                            if (lst_parcial != null && lst_parcial.Count > 0)
                            {
                                foreach (var item_p in lst_parcial)
                                {
                                    var info_calificacion_parcial = new aca_MatriculaCalificacionParcial_Info
                                    {
                                        IdProfesor = item.IdProfesor,
                                        IdMateria = item.IdMateria,
                                        IdCatalogoParcial = item_p.IdCatalogoParcial
                                    };

                                    info_matricula.lst_calificacion_parcial.Add(info_calificacion_parcial);

                                }
                            }

                            var info_calificacion = new aca_MatriculaCalificacion_Info
                            {
                                IdProfesor = item.IdProfesor,
                                IdMateria = item.IdMateria
                            };

                            info_matricula.lst_calificacion.Add(info_calificacion);
                        }
                    }
                    if (lst_materias_cualitativas != null && lst_materias_cualitativas.Count > 0)
                    {
                        foreach (var item in lst_materias_cualitativas)
                        {
                            if (lst_parcial != null && lst_parcial.Count > 0)
                            {
                                foreach (var item_p in lst_parcial)
                                {
                                    var info_calificacion_cualitativa_parcial = new aca_MatriculaCalificacionCualitativa_Info
                                    {
                                        IdProfesor = item.IdProfesor,
                                        IdMateria = item.IdMateria,
                                        IdCatalogoParcial = item_p.IdCatalogoParcial
                                    };

                                    info_matricula.lst_MatriculaCalificacionCualitativa.Add(info_calificacion_cualitativa_parcial);

                                }
                            }
                            var info_calificacion_cualitativa_promedio = new aca_MatriculaCalificacionCualitativaPromedio_Info
                            {
                                IdProfesor = item.IdProfesor,
                                IdMateria = item.IdMateria
                            };

                            info_matricula.lst_MatriculaCalificacionCualitativaPromedio.Add(info_calificacion_cualitativa_promedio);
                        }
                    }
                    #endregion

                    if (lst_PreMatricula_Detalle.Count > 0)
                    {
                        foreach (var item in lst_PreMatricula_Detalle)
                        {
                            var info_detalle = new aca_Matricula_Rubro_Info
                            {
                                IdEmpresa = item.IdEmpresa,
                                IdPeriodo = item.IdPeriodo,
                                IdRubro = item.IdRubro,
                                IdMecanismo = item.IdMecanismo,
                                IdProducto = item.IdProducto,
                                Subtotal = item.Subtotal,
                                IdCod_Impuesto_Iva = item.IdCod_Impuesto_Iva,
                                Porcentaje = item.Porcentaje,
                                ValorIVA = item.ValorIVA,
                                Total = item.Total,
                                IdAnio = item.IdAnio,
                                IdPlantilla = item.IdPlantilla,
                                ValorProntoPago = item.ValorProntoPago,
                                FechaProntoPago = item.FechaProntoPago,
                                EnMatricula = item.EnMatricula,
                                IdBodega = item.IdBodega,
                                IdSucursal = item.IdSucursal,
                                IdCbteVta = item.IdCbteVta,
                                FechaFacturacion = item.FechaFacturacion,
                                IdSede = item.IdSede,
                                IdNivel = item.IdNivel,
                                IdJornada = item.IdJornada,
                                IdCurso = item.IdCurso,
                                IdParalelo = item.IdParalelo
                            };

                            info_matricula.lst_MatriculaRubro.Add(info_detalle);
                        }
                    }

                    var lstDocumentos = bus_aludocumentos.GetList(info_matricula.IdEmpresa, info_matricula.IdAlumno, true);
                    info_matricula.lst_documentos = lstDocumentos;

                    if (bus_matricula.GuardarPreMatriculaDB(info_matricula))
                    {
                        mensaje = "Registro procesado correctamente";
                    }
                    else
                    {
                        mensaje = "El registro no fue procesado correctamente";
                    }
                }
                else {
                    mensaje = "La PreMatricula ya fue procesada";
                }

            var lst_PreMatricula = bus_prematricula.GetList_Procesar(info_PreMatricula.IdEmpresa, info_PreMatricula.IdSede, info_PreMatricula.IdAnio);
            Lista_PreMatricula.set_list(lst_PreMatricula, IdTransaccionSession);

            return Json(mensaje, JsonRequestBehavior.AllowGet);
            }
            #endregion
    }

    public class aca_PreMatricula_List
    {
        string Variable = "aca_PreMatriculaProcesar_Info";
        public List<aca_PreMatricula_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_PreMatricula_Info> list = new List<aca_PreMatricula_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_PreMatricula_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_PreMatricula_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}