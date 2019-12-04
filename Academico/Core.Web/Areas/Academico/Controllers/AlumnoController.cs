﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using Core.Info.Academico;
using Core.Web.Helps;
using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Helps;
using Core.Info.General;

namespace Core.Web.Areas.Academico.Controllers
{
    public class AlumnoController : Controller
    {
        #region Variables
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_AlumnoDocumento_Bus bus_alumno_documento = new aca_AlumnoDocumento_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
        aca_alumno_List Lista_Alumno = new aca_alumno_List();
        aca_AlumnoDocumento_List ListaAlumnoDocumento = new aca_AlumnoDocumento_List();
        aca_SocioEconomico_Bus bus_socioeconomico = new aca_SocioEconomico_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        public static UploadedFile file { get; set; }
        public int IdAlumno_ { get; set; }
        public static byte[] imagen { get; set; }
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
                    pe_nombreCompleto = model.pe_nombreCompleto,
                    pe_razonSocial = model.pe_razonSocial_padre,
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
                    pe_nombreCompleto = model.pe_nombreCompleto,
                    pe_razonSocial = model.pe_razonSocial_madre,
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

        #region Documentos
        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnoDocumento()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;

            List<aca_AlumnoDocumento_Info> model = ListaAlumnoDocumento.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AlumnoDocumento", model);
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
                CodCatalogoCONADIS = "",
                FechaIngreso = DateTime.Now,
                alu_foto = new byte[0],
                lst_alumno_documentos = new List<aca_AlumnoDocumento_Info>()
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

            model.IdCatalogoESTALU = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoAlumno.CURSANDO);
            model.IdCatalogoESTMAT = Convert.ToInt32(cl_enumeradores.eCatalogoAcademicoMatricula.REGISTRADO);
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
            model.IdTransaccionSession = Convert.ToInt32(SessionFixed.IdTransaccionSessionActual);
            model.lst_alumno_documentos = bus_alumno_documento.GetList(IdEmpresa, IdAlumno, true);
            ListaAlumnoDocumento.set_list(model.lst_alumno_documentos, model.IdTransaccionSession);

            aca_Familia_Info info_fam_padre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
            aca_Familia_Info info_fam_madre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA));

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);

            if (model.alu_foto == null)
                model.alu_foto = new byte[0];

            try
            {

                model.alu_foto = System.IO.File.ReadAllBytes(Server.MapPath(UploadDirectory) + model.IdEmpresa.ToString("000") + model.IdAlumno.ToString("000000") + ".jpg");
            }
            catch (Exception)
            {

                model.alu_foto = new byte[0];
            }

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
            model.pe_nombreCompleto_padre = info_fam_padre.pe_nombreCompleto;
            model.pe_razonSocial_padre = info_fam_padre.pe_razonSocial;
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
            model.pe_nombreCompleto_madre = info_fam_madre.pe_nombreCompleto;
            model.pe_razonSocial_madre = info_fam_madre.pe_razonSocial;
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
                return View(model);
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
            model.IdTransaccionSession = Convert.ToInt32(SessionFixed.IdTransaccionSessionActual);
            model.lst_alumno_documentos = bus_alumno_documento.GetList(IdEmpresa, IdAlumno, true);
            ListaAlumnoDocumento.set_list(model.lst_alumno_documentos, model.IdTransaccionSession);

            aca_Familia_Info info_fam_padre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA));
            aca_Familia_Info info_fam_madre = bus_familia.GetListTipo(IdEmpresa, IdAlumno, Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA));

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);

            if (model.alu_foto == null)
                model.alu_foto = new byte[0];

            if (model.alu_foto == null)
                model.alu_foto = new byte[0];

            try
            {

                model.alu_foto = System.IO.File.ReadAllBytes(Server.MapPath(UploadDirectory) + model.IdEmpresa.ToString("000") + model.IdAlumno.ToString("000000") + ".jpg");
            }
            catch (Exception)
            {

                model.alu_foto = new byte[0];
            }

            info_fam_padre = (info_fam_padre == null ? new aca_Familia_Info() : info_fam_padre);
            info_fam_madre = (info_fam_madre == null ? new aca_Familia_Info() : info_fam_madre);

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

        public JsonResult VerFichaSocioEconomica(int IdEmpresa = 0, int IdAlumno = 0)
        {
            mensaje = "";
            int IdSocioEconomico = 0;
            var info_socioeconomico = bus_socioeconomico.GetInfo_by_Alumno(IdEmpresa, IdAlumno);
            IdSocioEconomico = (info_socioeconomico == null ? 0 : info_socioeconomico.IdSocioEconomico);

            return Json(new { SocioEconomico = IdSocioEconomico }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Funciones imagen alumno
        public JsonResult nombre_imagen(decimal IdAlumno = 0)
        {
            try
            {
                if (IdAlumno == 0)
                    IdAlumno = bus_alumno.GetId(Convert.ToInt32(SessionFixed.IdEmpresa));
                SessionFixed.NombreImagenAlumno = IdAlumno.ToString("000000");
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult get_imagen_general()
        {

            byte[] model = empresa_imagen.pr_imagen;
            if (model == null)
                model = new byte[0];
            return PartialView("_Empresa_imagen", model);
        }
        public class empresa_imagen
        {
            public static byte[] pr_imagen { get; set; }
            public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
            {
                AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" },
                MaxFileSize = 4000000
            };
            public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
            {

                if (e.UploadedFile.IsValid)
                {
                    pr_imagen = e.UploadedFile.FileBytes;
                }
            }
        }
        public JsonResult actualizar_div()
        {
            return Json(SessionFixed.NombreImagenAlumno, JsonRequestBehavior.AllowGet);
        }
        public string UploadDirectory = "~/Content/imagenes/alumnos/";
        public ActionResult DragAndDropImageUpload([ModelBinder(typeof(DragAndDropSupportDemoBinder))]IEnumerable<UploadedFile> ucDragAndDrop)
        {

            try
            {
                //Extract Image File Name.
                string fileName = System.IO.Path.GetFileName(ucDragAndDrop.FirstOrDefault().FileName);
                var IdEmpresa = Convert.ToString(SessionFixed.IdEmpresa).PadLeft(3,'0');
                //Set the Image File Path.
                UploadDirectory = UploadDirectory + IdEmpresa + SessionFixed.NombreImagenAlumno + ".jpg";
                imagen = ucDragAndDrop.FirstOrDefault().FileBytes;
                //Save the Image File in Folder.
                ucDragAndDrop.FirstOrDefault().SaveAs(Server.MapPath(UploadDirectory));
                SessionFixed.NombreImagenAlumno = UploadDirectory;

                file = ucDragAndDrop.FirstOrDefault();
                return Json(ucDragAndDrop.FirstOrDefault().FileBytes, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return View();
            }

        }

        #endregion
    }

    #region Clases para imagen alumno
    public class DragAndDropSupportDemoBinder : DevExpressEditorsBinder
    {
        public DragAndDropSupportDemoBinder()
        {
            UploadControlBinderSettings.ValidationSettings.Assign(UploadControlDemosHelper.UploadValidationSettings);
            UploadControlBinderSettings.FileUploadCompleteHandler = UploadControlDemosHelper.FileUploadComplete;
        }
    }
    public class UploadControlDemosHelper
    {
        public static byte[] em_foto { get; set; }
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" },
            MaxFileSize = 4000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {

            if (e.UploadedFile.IsValid)
            {
                em_foto = e.UploadedFile.FileBytes;
                //var filename = Path.GetFileName(e.UploadedFile.FileName);
                //e.UploadedFile.SaveAs("~/Content/imagenes/"+e.UploadedFile.FileName, true);
            }
        }
    }
    #endregion
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

    public class aca_AlumnoDocumento_List
    {
        string Variable = "aca_AlumnoDocumento_Info";
        public List<aca_AlumnoDocumento_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_AlumnoDocumento_Info> list = new List<aca_AlumnoDocumento_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_AlumnoDocumento_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_AlumnoDocumento_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}