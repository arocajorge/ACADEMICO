﻿using Core.Bus.Academico;
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

namespace Core.Web.Areas.Academico.Controllers
{
    public class AlumnoController : Controller
    {
        #region Variables
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
        aca_alumno_List Lista_Alumno = new aca_alumno_List();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
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

            aca_Alumno_Info model = new aca_Alumno_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Alumno_Info> lista = bus_alumno.GetList(model.IdEmpresa, true);
            Lista_Alumno.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_alumno()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_Alumno_Info> model = Lista_Alumno.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_alumno", model);
        }
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            var lst_tipo_sangre = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPOSANGRE), false);
            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = ""});

            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_tipo_sangre = lst_tipo_sangre;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
        }

        private bool validar(aca_Alumno_Info info, ref string msg)
        {
            string return_naturaleza = "";
            string return_naturaleza_padre = "";
            string return_naturaleza_madre = "";

            if (cl_funciones.ValidaIdentificacion(info.info_persona_alumno.IdTipoDocumento, info.info_persona_alumno.pe_Naturaleza, info.info_persona_alumno.pe_cedulaRuc, ref return_naturaleza))
            {
                info.info_persona_alumno.pe_Naturaleza = return_naturaleza;
            }
            else
            {
                msg = "Número de identificación del alumno inválida";
                return false;
            }

            if (info.info_persona_padre.IdTipoDocumento != "" && info.info_persona_padre.pe_Naturaleza != "" && info.info_persona_padre.pe_cedulaRuc != null)
            {
                if (cl_funciones.ValidaIdentificacion(info.info_persona_padre.IdTipoDocumento, info.info_persona_padre.pe_Naturaleza, info.info_persona_padre.pe_cedulaRuc, ref return_naturaleza_padre))
                {
                    info.info_persona_padre.pe_Naturaleza = return_naturaleza_padre;
                    info.info_valido_padre = true;
                }
                else
                {
                    msg = "Número de identificación del padre inválida";
                    info.info_valido_padre = false;
                    return false;
                }
            }
            else
            {
                info.info_valido_padre = false;
            }

            if (info.info_persona_madre.IdTipoDocumento != "" && info.info_persona_madre.pe_Naturaleza != "" && info.info_persona_madre.pe_cedulaRuc != null)
            {
                if (cl_funciones.ValidaIdentificacion(info.info_persona_madre.IdTipoDocumento, info.info_persona_madre.pe_Naturaleza, info.info_persona_madre.pe_cedulaRuc, ref return_naturaleza_madre))
                {
                    info.info_persona_madre.pe_Naturaleza = return_naturaleza_madre;
                    info.info_valido_madre = true;
                }
                else
                {
                    msg = "Número de identificación de la madre inválida";
                    info.info_valido_madre = false;
                    return false;
                }
            }
            else
            {
                info.info_valido_madre = false;
            }

            return true;
        }

        private tb_persona_Info armar_info_padre(aca_Alumno_Info model)
        {
            var info_persona = new tb_persona_Info
                {
                    IdPersona = model.IdPersona_padre,
                    pe_Naturaleza = model.pe_Naturaleza_padre,
                    IdTipoDocumento = model.IdTipoDocumento_padre,
                    pe_cedulaRuc = (model.pe_cedulaRuc_padre=="" ? null : model.pe_cedulaRuc_padre),
                    pe_nombre = model.pe_nombre_padre,
                    pe_apellido = model.pe_apellido_padre,
                    pe_nombreCompleto = model.pe_apellido_padre + " " + model.pe_nombre_padre,
                    pe_razonSocial = model.pe_apellido_padre + " " + model.pe_nombre_padre,
                    pe_sexo = model.pe_sexo_padre,
                    CodCatalogoCONADIS = model.CodCatalogoCONADIS_padre,
                    NumeroCarnetConadis = model.NumeroCarnetConadis_padre,
                    PorcentajeDiscapacidad = model.PorcentajeDiscapacidad_padre,
                    pe_fechaNacimiento = model.pe_fechaNacimiento_padre,
                    pe_telfono_Contacto = model.pe_telfono_Contacto_padre,
                    pe_correo = model.Correo_padre,
                    pe_celular = model.Celular_padre,
                    pe_direccion = model.Direccion_padre,
                    IdEstadoCivil = model.IdEstadoCivil_padre
                };        

            return info_persona;
        }

        private tb_persona_Info armar_info_madre(aca_Alumno_Info model)
        {
            var info_persona = new tb_persona_Info
                {
                    IdPersona = model.IdPersona_madre,
                    pe_Naturaleza = model.pe_Naturaleza_madre,
                    IdTipoDocumento = model.IdTipoDocumento_madre,
                    pe_cedulaRuc = (model.pe_cedulaRuc_madre== "" ? null : model.pe_cedulaRuc_madre),
                    pe_nombre = model.pe_nombre_madre,
                    pe_apellido = model.pe_apellido_madre,
                    pe_nombreCompleto = model.pe_apellido_madre + " " + model.pe_nombre_madre,
                    pe_razonSocial = model.pe_apellido_madre + " " + model.pe_nombre_madre,
                    pe_sexo = model.pe_sexo_madre,
                    CodCatalogoCONADIS = model.CodCatalogoCONADIS_madre,
                    NumeroCarnetConadis = model.NumeroCarnetConadis_madre,
                    PorcentajeDiscapacidad = model.PorcentajeDiscapacidad_madre,
                    pe_fechaNacimiento = model.pe_fechaNacimiento_madre,
                    pe_telfono_Contacto = model.pe_telfono_Contacto_madre,
                    pe_correo = model.Correo_madre,
                    pe_celular = model.Celular_madre,
                    pe_direccion = model.Direccion_madre,
                    IdEstadoCivil = model.IdEstadoCivil_madre
                };

            return info_persona;
        }
        #endregion

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            aca_Alumno_Info model = new aca_Alumno_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                pe_Naturaleza = "NATU",
                CodCatalogoCONADIS = ""
            };
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Alumno_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;

            var info_persona_alumno = new tb_persona_Info
            {
                IdPersona = model.IdPersona,
                pe_Naturaleza = model.pe_Naturaleza,
                IdTipoDocumento = model.IdTipoDocumento,
                pe_cedulaRuc = model.pe_cedulaRuc,
                pe_nombre = model.pe_nombre,
                pe_apellido = model.pe_apellido,
                pe_nombreCompleto = model.pe_nombreCompleto,
                pe_razonSocial = model.pe_nombreCompleto,
                pe_sexo = model.pe_sexo,
                CodCatalogoSangre = model.CodCatalogoSangre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                NumeroCarnetConadis =model.NumeroCarnetConadis,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                pe_fechaNacimiento = model.pe_fechaNacimiento,
                pe_telfono_Contacto = model.pe_telfono_Contacto,
                pe_correo = model.Correo,
                pe_celular = model.Celular,
                pe_direccion = model.Direccion
            };

            model.IdCatalogoESTALU = Convert.ToInt32(cl_enumeradores.eCatalogoAcademico.CURSANDO);
            model.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademico.REGISTRADO);
            model.info_persona_alumno = info_persona_alumno;
            model.info_persona_padre = armar_info_padre(model);
            model.info_persona_madre = armar_info_madre(model);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }

            if (!bus_alumno.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAlumno = 0, bool Exito = false)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Alumno_Info model = bus_alumno.GetInfo(IdEmpresa, IdAlumno);
            aca_Familia_Info info_fam_padre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
            aca_Familia_Info info_fam_madre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA));

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);
            info_fam_padre = (info_fam_padre == null ? new aca_Familia_Info() : info_fam_padre);
            info_fam_madre = (info_fam_madre == null ? new aca_Familia_Info() : info_fam_madre);

            model.IdPersona_padre = info_fam_padre.IdPersona;
            model.SeFactura_padre = info_fam_padre.SeFactura;
            model.EsRepresentante_padre = info_fam_padre.EsRepresentante;
            model.IdTipoDocumento_padre = info_fam_padre.IdTipoDocumento;
            model.pe_Naturaleza_padre = info_fam_padre.pe_Naturaleza;
            model.pe_cedulaRuc_padre = info_fam_padre.pe_cedulaRuc;
            model.pe_nombre_padre = info_fam_padre.pe_nombre;
            model.pe_apellido_padre = info_fam_padre.pe_apellido;
            model.pe_fechaNacimiento_padre = info_fam_padre.pe_fechaNacimiento;
            model.pe_sexo_padre = info_fam_padre.pe_sexo;
            model.IdEstadoCivil_padre = info_fam_padre.IdEstadoCivil;
            model.pe_telfono_Contacto_padre = info_fam_padre.pe_telfono_Contacto;
            model.Celular_padre = info_fam_padre.Celular;
            model.Correo_padre = info_fam_padre.Correo;
            model.Direccion_padre = info_fam_padre.Direccion;
            model.PorcentajeDiscapacidad_padre = info_fam_padre.PorcentajeDiscapacidad;
            model.NumeroCarnetConadis_padre = info_fam_padre.NumeroCarnetConadis;
            model.CodCatalogoCONADIS_padre = (info_fam_padre == null || info_fam_padre.CodCatalogoCONADIS == null ? "" : info_fam_padre.CodCatalogoCONADIS);

            model.IdPersona_madre = info_fam_madre.IdPersona;
            model.SeFactura_madre = info_fam_madre.SeFactura;
            model.EsRepresentante_madre = info_fam_madre.EsRepresentante;
            model.IdTipoDocumento_madre = info_fam_madre.IdTipoDocumento;
            model.pe_Naturaleza_madre = info_fam_madre.pe_Naturaleza;
            model.pe_cedulaRuc_madre = info_fam_madre.pe_cedulaRuc;
            model.pe_nombre_madre = info_fam_madre.pe_nombre;
            model.pe_apellido_madre = info_fam_madre.pe_apellido;
            model.pe_fechaNacimiento_madre = info_fam_madre.pe_fechaNacimiento;
            model.pe_sexo_madre = info_fam_madre.pe_sexo;
            model.IdEstadoCivil_madre = info_fam_madre.IdEstadoCivil;
            model.pe_telfono_Contacto_madre = info_fam_madre.pe_telfono_Contacto;
            model.Celular_madre = info_fam_madre.Celular;
            model.Correo_madre = info_fam_madre.Correo;
            model.Direccion_madre = info_fam_madre.Direccion;
            model.PorcentajeDiscapacidad_madre = info_fam_madre.PorcentajeDiscapacidad;
            model.NumeroCarnetConadis_madre = info_fam_madre.NumeroCarnetConadis;
            model.CodCatalogoCONADIS_madre = (info_fam_madre == null || info_fam_madre.CodCatalogoCONADIS == null ? "" : info_fam_madre.CodCatalogoCONADIS);

            if (model == null)
                return RedirectToAction("Index");

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_Alumno_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;

            var info_persona_alumno = new tb_persona_Info
            {
                IdPersona = model.IdPersona,
                pe_Naturaleza = model.pe_Naturaleza,
                IdTipoDocumento = model.IdTipoDocumento,
                pe_cedulaRuc = model.pe_cedulaRuc,
                pe_nombre = model.pe_nombre,
                pe_apellido = model.pe_apellido,
                pe_nombreCompleto = model.pe_nombreCompleto,
                pe_razonSocial = model.pe_nombreCompleto,
                pe_sexo = model.pe_sexo,
                CodCatalogoSangre = model.CodCatalogoSangre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                NumeroCarnetConadis = model.NumeroCarnetConadis,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                pe_fechaNacimiento = model.pe_fechaNacimiento,
                pe_telfono_Contacto = model.pe_telfono_Contacto,
                pe_correo = model.Correo,
                pe_celular = model.Celular,
                pe_direccion = model.Direccion
            };

            model.info_persona_alumno = info_persona_alumno;
            model.info_persona_padre = armar_info_padre(model);
            model.info_persona_madre = armar_info_madre(model);

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }

            if (!bus_alumno.ModificarDB(model))
            {
                ViewBag.mensaje = "No se ha podido modificar el registro";
                cargar_combos();
                return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Exito = true });
            }

            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAlumno = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Alumno_Info model = bus_alumno.GetInfo(IdEmpresa, IdAlumno);
            aca_Familia_Info info_fam_padre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
            aca_Familia_Info info_fam_madre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA));

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);

            model.IdPersona_padre = info_fam_padre.IdPersona;
            model.SeFactura_padre = info_fam_padre.SeFactura;
            model.IdTipoDocumento_padre = info_fam_padre.IdTipoDocumento;
            model.pe_Naturaleza_padre = info_fam_padre.pe_Naturaleza;
            model.pe_cedulaRuc_padre = info_fam_padre.pe_cedulaRuc;
            model.pe_nombre_padre = info_fam_padre.pe_nombre;
            model.pe_apellido_padre = info_fam_padre.pe_apellido;
            model.pe_fechaNacimiento_padre = info_fam_padre.pe_fechaNacimiento;
            model.pe_sexo_padre = info_fam_padre.pe_sexo;
            model.IdEstadoCivil_padre = info_fam_padre.IdEstadoCivil;
            model.pe_telfono_Contacto_padre = info_fam_padre.pe_telfono_Contacto;
            model.Celular_padre = info_fam_padre.Celular;
            model.Correo_padre = info_fam_padre.Correo;
            model.Direccion_padre = info_fam_padre.Direccion;
            model.PorcentajeDiscapacidad_padre = info_fam_padre.PorcentajeDiscapacidad;
            model.NumeroCarnetConadis_padre = info_fam_padre.NumeroCarnetConadis;
            model.CodCatalogoCONADIS_padre = (info_fam_padre.CodCatalogoCONADIS == null ? "" : info_fam_padre.CodCatalogoCONADIS);

            model.IdPersona_madre = info_fam_madre.IdPersona;
            model.SeFactura_madre = info_fam_madre.SeFactura;
            model.IdTipoDocumento_madre = info_fam_madre.IdTipoDocumento;
            model.pe_Naturaleza_madre = info_fam_madre.pe_Naturaleza;
            model.pe_cedulaRuc_madre = info_fam_madre.pe_cedulaRuc;
            model.pe_nombre_madre = info_fam_madre.pe_nombre;
            model.pe_apellido_madre = info_fam_madre.pe_apellido;
            model.pe_fechaNacimiento_madre = info_fam_madre.pe_fechaNacimiento;
            model.pe_sexo_madre = info_fam_madre.pe_sexo;
            model.IdEstadoCivil_madre = info_fam_madre.IdEstadoCivil;
            model.pe_telfono_Contacto_madre = info_fam_madre.pe_telfono_Contacto;
            model.Celular_madre = info_fam_madre.Celular;
            model.Correo_madre = info_fam_madre.Correo;
            model.Direccion_madre = info_fam_madre.Direccion;
            model.PorcentajeDiscapacidad_madre = info_fam_madre.PorcentajeDiscapacidad;
            model.NumeroCarnetConadis_madre = info_fam_madre.NumeroCarnetConadis;
            model.CodCatalogoCONADIS_madre = (info_fam_madre.CodCatalogoCONADIS == null ? "" : info_fam_madre.CodCatalogoCONADIS);

            if (model == null)
                return RedirectToAction("Index");

            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Anular(aca_Alumno_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_alumno.AnularDB(model))
            {
                ViewBag.mensaje = "No se ha podido anular el registro";
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Json
        public JsonResult Validar_cedula_ruc(string naturaleza = "", string tipo_documento = "", string cedula_ruc = "")
        {
            var return_naturaleza = "";
            var isValid = cl_funciones.ValidaIdentificacion(tipo_documento, naturaleza, cedula_ruc, ref return_naturaleza);

            return Json(new { isValid = isValid, return_naturaleza = return_naturaleza }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula(int IdEmpresa = 0, string pe_cedulaRuc = "")
        {
            var resultado = bus_alumno.get_info_x_num_cedula(IdEmpresa, pe_cedulaRuc);
            resultado.anio = Convert.ToDateTime(resultado.pe_fechaNacimiento).Year.ToString();
            var mes = Convert.ToDateTime(resultado.pe_fechaNacimiento).Month;
            mes = mes - 1;
            resultado.mes = mes.ToString();
            resultado.dia = Convert.ToDateTime(resultado.pe_fechaNacimiento).Day.ToString();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula_familia(int IdEmpresa = 0, decimal IdAlumno = 0, string pe_cedulaRuc = "")
        {
            var resultado = bus_familia.get_info_x_num_cedula(IdEmpresa, IdAlumno, pe_cedulaRuc);
            resultado.anio = Convert.ToDateTime(resultado.pe_fechaNacimiento).Year.ToString();
            var mes = Convert.ToDateTime(resultado.pe_fechaNacimiento).Month;
            mes = mes - 1;
            resultado.mes = mes.ToString();
            resultado.dia = Convert.ToDateTime(resultado.pe_fechaNacimiento).Day.ToString();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

    public class aca_alumno_List
    {
        string Variable = "aca_Alumno_Info";
        public List<aca_Alumno_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Alumno_Info> list = new List<aca_Alumno_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Alumno_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Alumno_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}