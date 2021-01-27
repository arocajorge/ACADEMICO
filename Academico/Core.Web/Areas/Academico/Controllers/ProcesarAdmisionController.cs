using Core.Bus.Academico;
using Core.Bus.Facturacion;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Web.Areas.Academico.Controllers
{
    public class ProcesarAdmisionController : Controller
    {
        #region Variables
        tb_Religion_Bus bus_religion = new tb_Religion_Bus();
        tb_GrupoEtnico_Bus bus_grupoetnico = new tb_GrupoEtnico_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
        aca_Admision_Bus bus_admision = new aca_Admision_Bus();
        aca_SocioEconomico_Bus bus_socioeconomico = new aca_SocioEconomico_Bus();
        tb_profesion_Bus bus_profesion = new tb_profesion_Bus();
        aca_CatalogoFicha_Bus bus_catalogo_ficha = new aca_CatalogoFicha_Bus();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        aca_Jornada_Bus bus_jornada = new aca_Jornada_Bus();
        aca_NivelAcademico_Bus bus_nivel = new aca_NivelAcademico_Bus();
        aca_Curso_Bus bus_curso = new aca_Curso_Bus();
        tb_pais_Bus bus_pais = new tb_pais_Bus();
        tb_region_Bus bus_region = new tb_region_Bus();
        tb_provincia_Bus bus_provincia = new tb_provincia_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        tb_parroquia_Bus bus_parroquia = new tb_parroquia_Bus();
        aca_Alumno_Bus bus_alumno = new aca_Alumno_Bus();
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        aca_ProcesarAdmision_List Lista_ProcesarAdmision = new aca_ProcesarAdmision_List();
        fa_cliente_tipo_Bus bus_clientetipo = new fa_cliente_tipo_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();

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

            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa, 0);

            var model = new aca_Admision_Info
            {
                IdEmpresa = IdEmpresa,
                IdAnio = (info_anio == null ? 0 : info_anio.IdAnio),
                IdSede = Convert.ToInt32(SessionFixed.IdSede),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Admision_Info> lst_admisiones = bus_admision.GetList(model.IdEmpresa, model.IdAnio, model.IdSede);
            Lista_ProcesarAdmision.set_list(lst_admisiones, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Admision_Info model)
        {
            List<aca_Admision_Info> lst_admisiones = bus_admision.GetList(model.IdEmpresa, model.IdAnio, model.IdSede);
            Lista_ProcesarAdmision.set_list(lst_admisiones, Convert.ToDecimal(SessionFixed.IdTransaccionSession));
            cargar_combos(model);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_ProcesarAdmision(bool Nuevo = false, bool Modificar = false, bool Anular = false)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            List<aca_Admision_Info> model = Lista_ProcesarAdmision.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));

            return PartialView("_GridViewPartial_ProcesarAdmision", model);
        }
        #endregion

        #region Combos
        public ActionResult ComboBoxPartial_Pais()
        {
            return PartialView("_ComboBoxPartial_Pais", new aca_Admision_Info());
        }
        public ActionResult ComboBoxPartial_Region()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            return PartialView("_ComboBoxPartial_Region", new aca_Admision_Info { IdPais_Aspirante = IdPais });
        }
        public ActionResult ComboBoxPartial_Provincia()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            string Cod_Region = (Request.Params["Cod_Region"] != null) ? Convert.ToString(Request.Params["Cod_Region"]) : "";
            return PartialView("_ComboBoxPartial_Provincia", new aca_Admision_Info { IdPais_Aspirante = IdPais, Cod_Region_Aspirante = Cod_Region });
        }
        public ActionResult ComboBoxPartial_Ciudad()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            string Cod_Region = (Request.Params["Cod_Region"] != null) ? Convert.ToString(Request.Params["Cod_Region"]) : "";
            string IdProvincia = (Request.Params["IdProvincia"] != null) ? Convert.ToString(Request.Params["IdProvincia"]) : "";
            return PartialView("_ComboBoxPartial_Ciudad", new aca_Admision_Info { IdPais_Aspirante = IdPais, Cod_Region_Aspirante = Cod_Region, IdProvincia_Aspirante = IdProvincia });
        }
        public ActionResult ComboBoxPartial_Parroquia()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            string Cod_Region = (Request.Params["Cod_Region"] != null) ? Convert.ToString(Request.Params["Cod_Region"]) : "";
            string IdProvincia = (Request.Params["IdProvincia"] != null) ? Convert.ToString(Request.Params["IdProvincia"]) : "";
            string IdCiudad = (Request.Params["IdCiudad"] != null) ? Convert.ToString(Request.Params["IdCiudad"]) : "";
            return PartialView("_ComboBoxPartial_Parroquia", new aca_Admision_Info { IdPais_Aspirante = IdPais, Cod_Region_Aspirante = Cod_Region, IdProvincia_Aspirante = IdProvincia, IdCiudad_Aspirante = IdCiudad });
        }

        public ActionResult cmb_parroquia_padre()
        {
            string IdCiudadPadre = (Request.Params["fx_IdCiudad_padre_fact"] != null) ? Request.Params["fx_IdCiudad_padre_fact"].ToString() : "";
            return PartialView("_cmb_parroquia_padre", new aca_Admision_Info { IdCiudad_Padre_Fact = IdCiudadPadre });
        }
        public ActionResult cmb_parroquia_madre()
        {
            string IdCiudadMadre = (Request.Params["fx_IdCiudad_madre_fact"] != null) ? Request.Params["fx_IdCiudad_madre_fact"].ToString() : "";
            return PartialView("_cmb_parroquia_madre", new aca_Admision_Info { IdCiudad_Madre_Fact = IdCiudadMadre });
        }
        public ActionResult cmb_parroquia_representante()
        {
            string IdCiudadMadre = (Request.Params["fx_IdCiudad_representante_fact"] != null) ? Request.Params["fx_IdCiudad_representante_fact"].ToString() : "";
            return PartialView("_cmb_parroquia_representante", new aca_Admision_Info { IdCiudad_Representante_Fact = IdCiudadMadre });
        }
        #endregion
        #region CombosPadre
        public ActionResult ComboBoxPartial_Pais_padre()
        {
            return PartialView("_ComboBoxPartial_Pais_padre", new aca_Admision_Info());
        }
        public ActionResult ComboBoxPartial_Region_padre()
        {
            string IdPais = (Request.Params["IdPais_padre"] != null) ? Convert.ToString(Request.Params["IdPais_padre"]) : "";
            return PartialView("_ComboBoxPartial_Region_padre", new aca_Admision_Info { IdPais_Padre = IdPais });
        }
        public ActionResult ComboBoxPartial_Provincia_padre()
        {
            string IdPais = (Request.Params["IdPais_padre"] != null) ? Convert.ToString(Request.Params["IdPais_padre"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_padre"] != null) ? Convert.ToString(Request.Params["Cod_Region_padre"]) : "";
            return PartialView("_ComboBoxPartial_Provincia_padre", new aca_Admision_Info { IdPais_Padre = IdPais, Cod_Region_Padre = Cod_Region });
        }
        public ActionResult ComboBoxPartial_Ciudad_padre()
        {
            string IdPais = (Request.Params["IdPais_padre"] != null) ? Convert.ToString(Request.Params["IdPais_padre"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_padre"] != null) ? Convert.ToString(Request.Params["Cod_Region_padre"]) : "";
            string IdProvincia = (Request.Params["IdProvincia_padre"] != null) ? Convert.ToString(Request.Params["IdProvincia_padre"]) : "";
            return PartialView("_ComboBoxPartial_Ciudad_padre", new aca_Admision_Info { IdPais_Padre = IdPais, Cod_Region_Padre = Cod_Region, IdProvincia_Aspirante = IdProvincia });
        }
        public ActionResult ComboBoxPartial_Parroquia_padre()
        {
            string IdPais = (Request.Params["IdPais_padre"] != null) ? Convert.ToString(Request.Params["IdPais_padre"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_padre"] != null) ? Convert.ToString(Request.Params["Cod_Region_padre"]) : "";
            string IdProvincia = (Request.Params["IdProvincia_padre"] != null) ? Convert.ToString(Request.Params["IdProvincia_padre"]) : "";
            string IdCiudad = (Request.Params["IdCiudad_padre"] != null) ? Convert.ToString(Request.Params["IdCiudad_padre"]) : "";
            return PartialView("_ComboBoxPartial_Parroquia_padre", new aca_Admision_Info { IdPais_Padre = IdPais, Cod_Region_Padre = Cod_Region, IdProvincia_Padre = IdProvincia, IdCiudad_Padre = IdCiudad });
        }
        #endregion
        #region CombosMadre
        public ActionResult ComboBoxPartial_Pais_madre()
        {
            return PartialView("_ComboBoxPartial_Pais_madre", new aca_Admision_Info());
        }
        public ActionResult ComboBoxPartial_Region_madre()
        {
            string IdPais = (Request.Params["IdPais_madre"] != null) ? Convert.ToString(Request.Params["IdPais_madre"]) : "";
            return PartialView("_ComboBoxPartial_Region_madre", new aca_Admision_Info { IdPais_Madre = IdPais });
        }
        public ActionResult ComboBoxPartial_Provincia_madre()
        {
            string IdPais = (Request.Params["IdPais_madre"] != null) ? Convert.ToString(Request.Params["IdPais_madre"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_madre"] != null) ? Convert.ToString(Request.Params["Cod_Region_madre"]) : "";
            return PartialView("_ComboBoxPartial_Provincia_madre", new aca_Admision_Info { IdPais_Madre = IdPais, Cod_Region_Madre = Cod_Region });
        }
        public ActionResult ComboBoxPartial_Ciudad_madre()
        {
            string IdPais = (Request.Params["IdPais_madre"] != null) ? Convert.ToString(Request.Params["IdPais_madre"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_madre"] != null) ? Convert.ToString(Request.Params["Cod_Region_madre"]) : "";
            string IdProvincia = (Request.Params["IdProvincia_madre"] != null) ? Convert.ToString(Request.Params["IdProvincia_madre"]) : "";
            return PartialView("_ComboBoxPartial_Ciudad_madre", new aca_Admision_Info { IdPais_Madre = IdPais, Cod_Region_Madre = Cod_Region, IdProvincia_Madre = IdProvincia });
        }
        public ActionResult ComboBoxPartial_Parroquia_madre()
        {
            string IdPais = (Request.Params["IdPais_madre"] != null) ? Convert.ToString(Request.Params["IdPais_madre"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_madre"] != null) ? Convert.ToString(Request.Params["Cod_Region_madre"]) : "";
            string IdProvincia = (Request.Params["IdProvincia_madre"] != null) ? Convert.ToString(Request.Params["IdProvincia_madre"]) : "";
            string IdCiudad = (Request.Params["IdCiudad_madre"] != null) ? Convert.ToString(Request.Params["IdCiudad_madre"]) : "";
            return PartialView("_ComboBoxPartial_Parroquia_madre", new aca_Admision_Info { IdPais_Madre = IdPais, Cod_Region_Madre = Cod_Region, IdProvincia_Madre = IdProvincia, IdCiudad_Madre = IdCiudad });
        }
        #endregion
        #region CombosMadre
        public ActionResult ComboBoxPartial_Pais_representante()
        {
            return PartialView("_ComboBoxPartial_Pais_representante", new aca_Admision_Info());
        }
        public ActionResult ComboBoxPartial_Region_representante()
        {
            string IdPais = (Request.Params["IdPais_representante"] != null) ? Convert.ToString(Request.Params["IdPais_representante"]) : "";
            return PartialView("_ComboBoxPartial_Region_representante", new aca_Admision_Info { IdPais_Representante = IdPais });
        }
        public ActionResult ComboBoxPartial_Provincia_representante()
        {
            string IdPais = (Request.Params["IdPais_representante"] != null) ? Convert.ToString(Request.Params["IdPais_representante"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_representante"] != null) ? Convert.ToString(Request.Params["Cod_Region_representante"]) : "";
            return PartialView("_ComboBoxPartial_Provincia_representante", new aca_Admision_Info { IdPais_Representante = IdPais, Cod_Region_Representante = Cod_Region });
        }
        public ActionResult ComboBoxPartial_Ciudad_representante()
        {
            string IdPais = (Request.Params["IdPais_representante"] != null) ? Convert.ToString(Request.Params["IdPais_representante"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_representante"] != null) ? Convert.ToString(Request.Params["Cod_Region_representante"]) : "";
            string IdProvincia = (Request.Params["IdProvincia_representante"] != null) ? Convert.ToString(Request.Params["IdProvincia_representante"]) : "";
            return PartialView("_ComboBoxPartial_Ciudad_representante", new aca_Admision_Info { IdPais_Representante = IdPais, Cod_Region_Representante = Cod_Region, IdProvincia_Representante = IdProvincia });
        }
        public ActionResult ComboBoxPartial_Parroquia_representante()
        {
            string IdPais = (Request.Params["IdPais_representante"] != null) ? Convert.ToString(Request.Params["IdPais_representante"]) : "";
            string Cod_Region = (Request.Params["Cod_Region_representante"] != null) ? Convert.ToString(Request.Params["Cod_Region_representante"]) : "";
            string IdProvincia = (Request.Params["IdProvincia_representante"] != null) ? Convert.ToString(Request.Params["IdProvincia_representante"]) : "";
            string IdCiudad = (Request.Params["IdCiudad_representante"] != null) ? Convert.ToString(Request.Params["IdCiudad_representante"]) : "";
            return PartialView("_ComboBoxPartial_Parroquia_representante", new aca_Admision_Info { IdPais_Representante = IdPais, Cod_Region_Representante = Cod_Region, IdProvincia_Representante = IdProvincia, IdCiudad_Representante = IdCiudad });
        }
        #endregion

        #region Acciones
        public ActionResult Consultar(int IdEmpresa = 0, decimal IdAdmision = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            aca_Admision_Info model = bus_admision.GetInfo(IdEmpresa, IdAdmision);

            if (model == null)
                return RedirectToAction("Index");

            //if (Exito)
            //    ViewBag.MensajeSuccess = MensajeSuccess;

            cargar_combos(model);
            return View(model);
        }
        #endregion

        #region Metodos
        private void cargar_combos(aca_Admision_Info model)
        {
            var lst_anio = bus_anio.GetList_Matricula(model.IdEmpresa, false);

            var lst_sede = bus_sede.GetList(model.IdEmpresa, false);
            var lst_jornada = new List<aca_Jornada_Info>();
            var lst_nivel = new List<aca_NivelAcademico_Info>();
            var lst_curso = new List<aca_Curso_Info>();

            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            lst_sexo.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "--- Seleccione ---" });

            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            lst_estado_civil.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "--- Seleccione ---" });

            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);

            var lst_tipo_sangre = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPOSANGRE), false);
            lst_tipo_sangre.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "--- Seleccione ---" });

            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "--- Seleccione ---" });

            var lst_instruccion = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.INSTRUCCION), false);
            lst_instruccion.Add(new aca_CatalogoFicha_Info { IdCatalogoFicha = 0, NomCatalogoFicha = "--- Seleccione ---" });

            var lst_vive = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.VIVECON), false);
            var lst_tipo_vivienda = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.TIPOVIVIENDA), false);
            var lst_vivienda = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.VIVIENDA), false);
            var lst_agua = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.AGUA), false);
            var lst_ing_institucion = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.MOTIVOING), false);
            var lst_institucion = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.INSTITUCION), false);
            var lst_financiamiento = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.ESTUDIOS), false);

            var lst_profesion = bus_profesion.GetList(false);
            lst_profesion.Add(new tb_profesion_Info { IdProfesion = 0, Descripcion = "--- Seleccione ---" });

            var lst_religion = bus_religion.GetList(false);
            lst_religion.Add(new tb_Religion_Info { IdReligion = 0, NomReligion = "--- Seleccione ---" });

            var lst_grupoetnico = bus_grupoetnico.GetList(false);
            lst_grupoetnico.Add(new tb_GrupoEtnico_Info { IdGrupoEtnico = 0, NomGrupoEtnico = "--- Seleccione ---" });

            var lst_parentesco = bus_aca_catalogo.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.PAREN), false);
            var lst_pais = bus_pais.get_list(false);
            lst_pais.Add(new tb_pais_Info { IdPais = "", Nombre = "--- Seleccione ---" });
            var lst_region = new List<tb_region_Info>();
            var lst_provincia = new List<tb_provincia_Info>();
            var lst_ciudad = new List<tb_ciudad_Info>();
            var lst_parroquia = new List<tb_parroquia_Info>();

            var lst_termino_pago = bus_termino_pago.get_list(false);
            var lst_clientetipo = bus_clientetipo.get_list(model.IdEmpresa, false);
            var lst_ciudad_factura = bus_ciudad.get_list("", false);
            var lst_parroquia_factura = new List<tb_parroquia_Info>();

            ViewBag.lst_anio = lst_anio;
            ViewBag.lst_sede = lst_sede;
            ViewBag.lst_jornada = lst_jornada;
            ViewBag.lst_nivel = lst_nivel;
            ViewBag.lst_curso = lst_curso;
            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_tipo_sangre = lst_tipo_sangre;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
            ViewBag.lst_profesion = lst_profesion;
            ViewBag.lst_instruccion = lst_instruccion;
            ViewBag.lst_religion = lst_religion;
            ViewBag.lst_grupoetnico = lst_grupoetnico;
            ViewBag.lst_pais = lst_pais;
            ViewBag.lst_region = lst_region;
            ViewBag.lst_provincia = lst_provincia;
            ViewBag.lst_ciudad = lst_ciudad;
            ViewBag.lst_parroquia = lst_parroquia;
            ViewBag.lst_parentesco = lst_parentesco;
            ViewBag.lst_vive = lst_vive;
            ViewBag.lst_tipo_vivienda = lst_tipo_vivienda;
            ViewBag.lst_vivienda = lst_vivienda;
            ViewBag.lst_agua = lst_agua;
            ViewBag.lst_ing_institucion = lst_ing_institucion;
            ViewBag.lst_institucion = lst_institucion;
            ViewBag.lst_financiamiento = lst_financiamiento;
            ViewBag.lst_termino_pago = lst_termino_pago;
            ViewBag.lst_clientetipo = lst_clientetipo;
            ViewBag.lst_ciudad_factura = lst_ciudad_factura;
            ViewBag.lst_parroquia_factura = lst_parroquia_factura;
        }

        private bool validar(aca_Admision_Info info, ref string msg)
        {
            string return_naturaleza = "";
            string return_naturaleza_padre = "";
            string return_naturaleza_madre = "";
            string return_naturaleza_representante = "";
            if (info.FotoAspirante != null)
            {
                if (info.FotoAspirante.ContentLength > 0 && info.FotoAspirante.ContentLength >= 4000000)
                {
                    msg = "Peso de archivo (foto del aspirante) no permitido";
                    return false;
                }
            }
            else
            {
                msg = "Cargue el archivo (foto del aspirante)";
                return false;
            }

            if (info.CedulaAspirante != null)
            {
                if (info.CedulaAspirante.ContentLength > 0 && info.CedulaAspirante.ContentLength >= 4000000)
                {
                    msg = "Peso de archivo (cédula del aspirante) no permitido";
                    return false;
                }
            }
            else
            {
                msg = "Cargue el archivo (cédula del aspirante)";
                return false;
            }
            if (info.CedulaRepresentante != null)
            {
                if (info.CedulaRepresentante.ContentLength > 0 && info.CedulaRepresentante.ContentLength >= 4000000)
                {
                    msg = "Peso de archivo (cédula del representante) no permitido";
                    return false;
                }
            }
            else
            {
                msg = "Cargue el archivo (cédula del representante)";
                return false;
            }
            if (info.RecordAcademicoAspirante != null)
            {
                if (info.RecordAcademicoAspirante.ContentLength > 0 && info.RecordAcademicoAspirante.ContentLength >= 4000000)
                {
                    msg = "Peso de archivo (record académico del aspirante) no permitido";
                    return false;
                }
            }
            else
            {
                msg = "Cargue el archivo (record académico del aspirante)";
                return false;
            }
            if (info.PagoAlDiaAspirante != null)
            {
                if (info.PagoAlDiaAspirante.ContentLength > 0 && info.PagoAlDiaAspirante.ContentLength >= 4000000)
                {
                    msg = "Peso de archivo (pago al día) no permitido";
                    return false;
                }
            }
            else
            {
                msg = "Cargue el archivo (pago al día)";
                return false;
            }
            if (info.CertificadoLaboral != null)
            {
                if (info.CertificadoLaboral.ContentLength > 0 && info.CertificadoLaboral.ContentLength >= 4000000)
                {
                    msg = "Peso de archivo (certificado laboral) no permitido";
                    return false;
                }
            }
            else
            {
                msg = "Cargue el archivo (certificado laboral)";
                return false;
            }

            info.NombreCompleto_Aspirante = info.Apellidos_Aspirante + ' ' + info.Nombres_Aspirante;
            info.FechaNacimiento_Aspirante = (info.FechaNacimiento_Aspirante == null ? (DateTime?)null : info.FechaNacimiento_Aspirante);
            info.FechaNacimiento_Padre = (info.FechaNacimiento_Padre == null ? (DateTime?)null : info.FechaNacimiento_Padre);
            info.FechaNacimiento_Madre = (info.FechaNacimiento_Madre == null ? (DateTime?)null : info.FechaNacimiento_Madre);

            if (string.IsNullOrEmpty(info.RazonSocial_Padre))
            {
                info.NombreCompleto_Padre = info.Apellidos_Padre + ' ' + info.Nombres_Padre;
            }
            else
            {
                info.NombreCompleto_Padre = info.RazonSocial_Padre;
            }

            if (string.IsNullOrEmpty(info.RazonSocial_Madre))
            {
                info.NombreCompleto_Madre = info.Apellidos_Madre + ' ' + info.Nombres_Madre;
            }
            else
            {
                info.NombreCompleto_Madre = info.RazonSocial_Madre;
            }

            if (info.Representante == "P")
            {
                info.Naturaleza_Representante = info.Naturaleza_Padre;
                info.IdTipoDocumento_Representante = info.IdTipoDocumento_Padre;
                info.CedulaRuc_Representante = info.CedulaRuc_Padre;
                info.Nombres_Representante = info.Nombres_Padre;
                info.Apellidos_Representante = info.Apellidos_Padre;
                info.NombreCompleto_Representante = info.Apellidos_Padre + ' ' + info.Nombres_Padre;
                info.RazonSocial_Representante = info.RazonSocial_Padre;
                info.Direccion_Representante = info.Direccion_Padre;
                info.Telefono_Representante = info.Telefono_Padre;
                info.Celular_Representante = info.Celular_Padre;
                info.Correo_Representante = info.Correo_Padre;
                info.Sexo_Representante = (info.Sexo_Padre == "" ? null : info.Sexo_Padre);
                info.FechaNacimiento_Representante = (info.FechaNacimiento_Padre == null ? (DateTime?)null : info.FechaNacimiento_Padre);
                info.CodCatalogoCONADIS_Representante = (info.CodCatalogoCONADIS_Padre == "" ? null : info.CodCatalogoCONADIS_Padre);
                info.PorcentajeDiscapacidad_Representante = info.PorcentajeDiscapacidad_Padre;
                info.NumeroCarnetConadis_Representante = info.NumeroCarnetConadis_Padre;
                info.IdGrupoEtnico_Representante = (info.IdGrupoEtnico_Padre == 0 ? (int?)null : info.IdGrupoEtnico_Padre);
                info.IdReligion_Representante = (info.IdReligion_Padre == 0 ? (int?)null : info.IdReligion_Padre);
                info.IdEstadoCivil_Representante = (info.IdEstadoCivil_Padre == "" ? null : info.IdEstadoCivil_Padre);
                info.AsisteCentroCristiano_Representante = info.AsisteCentroCristiano_Padre;
                info.IdPais_Representante = (info.IdPais_Padre == "" ? null : info.IdPais_Padre);
                info.Cod_Region_Representante = (info.Cod_Region_Padre == "" ? null : info.Cod_Region_Padre);
                info.IdProvincia_Representante = (info.IdProvincia_Padre == "" ? null : info.IdProvincia_Padre);
                info.IdCiudad_Representante = (info.IdCiudad_Padre == "" ? null : info.IdCiudad_Padre);
                info.IdParroquia_Representante = (info.IdParroquia_Padre == "" ? null : info.IdParroquia_Padre);
                info.Sector_Representante = info.Sector_Padre;
                info.IdCatalogoPAREN_Representante = info.IdCatalogoPAREN_Padre;
                info.IdCatalogoFichaInst_Representante = (info.IdCatalogoFichaInst_Padre == 0 ? (int?)null : info.IdCatalogoFichaInst_Padre);
                info.EmpresaTrabajo_Representante = info.EmpresaTrabajo_Padre;
                info.IdProfesion_Representante = (info.IdProfesion_Padre == 0 ? (int?)null : info.IdCatalogoFichaInst_Padre);
                info.DireccionTrabajo_Representante = info.DireccionTrabajo_Padre;
                info.TelefonoTrabajo_Representante = info.TelefonoTrabajo_Padre;
                info.CargoTrabajo_Representante = info.CargoTrabajo_Padre;
                info.AniosServicio_Representante = info.AniosServicio_Padre;
                info.IngresoMensual_Representante = info.IngresoMensual_Padre;
                info.VehiculoPropio_Representante = info.VehiculoPropio_Padre;
                info.Marca_Representante = info.Marca_Padre;
                info.Modelo_Representante = info.Modelo_Padre;
                info.AnioVehiculo_Representante = info.AnioVehiculo_Padre;
                info.CasaPropia_Representante = info.CasaPropia_Padre;
                info.EstaFallecido_Representante = info.EstaFallecido_Padre;
            }
            else if (info.Representante == "M")
            {
                info.Naturaleza_Representante = info.Naturaleza_Madre;
                info.IdTipoDocumento_Representante = info.IdTipoDocumento_Madre;
                info.CedulaRuc_Representante = info.CedulaRuc_Madre;
                info.Nombres_Representante = info.Nombres_Madre;
                info.Apellidos_Representante = info.Apellidos_Madre;
                info.NombreCompleto_Representante = info.Apellidos_Madre + ' ' + info.Nombres_Madre;
                info.RazonSocial_Representante = info.RazonSocial_Madre;
                info.Direccion_Representante = info.Direccion_Madre;
                info.Telefono_Representante = info.Telefono_Madre;
                info.Celular_Representante = info.Celular_Madre;
                info.Correo_Representante = info.Correo_Madre;
                info.Sexo_Representante = (info.Sexo_Madre == "" ? null : info.Sexo_Madre);
                info.FechaNacimiento_Representante = (info.FechaNacimiento_Madre == null ? (DateTime?)null : info.FechaNacimiento_Madre);
                info.CodCatalogoCONADIS_Representante = (info.CodCatalogoCONADIS_Madre == "" ? null : info.CodCatalogoCONADIS_Madre);
                info.PorcentajeDiscapacidad_Representante = info.PorcentajeDiscapacidad_Madre;
                info.NumeroCarnetConadis_Representante = info.NumeroCarnetConadis_Madre;
                info.IdGrupoEtnico_Representante = (info.IdGrupoEtnico_Madre == 0 ? (int?)null : info.IdGrupoEtnico_Madre);
                info.IdReligion_Representante = (info.IdReligion_Madre == 0 ? (int?)null : info.IdReligion_Madre);
                info.IdEstadoCivil_Representante = (info.IdEstadoCivil_Madre == "" ? null : info.IdEstadoCivil_Madre);
                info.AsisteCentroCristiano_Representante = info.AsisteCentroCristiano_Madre;
                info.IdPais_Representante = (info.IdPais_Madre == "" ? null : info.IdPais_Madre);
                info.Cod_Region_Representante = (info.Cod_Region_Madre == "" ? null : info.Cod_Region_Madre);
                info.IdProvincia_Representante = (info.IdProvincia_Madre == "" ? null : info.IdProvincia_Madre);
                info.IdCiudad_Representante = (info.IdCiudad_Madre == "" ? null : info.IdCiudad_Madre);
                info.IdParroquia_Representante = (info.IdParroquia_Madre == "" ? null : info.IdParroquia_Madre);
                info.Sector_Representante = info.Sector_Madre;
                info.IdCatalogoPAREN_Representante = info.IdCatalogoPAREN_Madre;
                info.IdCatalogoFichaInst_Representante = (info.IdCatalogoFichaInst_Madre == 0 ? (int?)null : info.IdCatalogoFichaInst_Madre);
                info.EmpresaTrabajo_Representante = info.EmpresaTrabajo_Madre;
                info.IdProfesion_Representante = (info.IdProfesion_Madre == 0 ? (int?)null : info.IdCatalogoFichaInst_Madre);
                info.DireccionTrabajo_Representante = info.DireccionTrabajo_Madre;
                info.TelefonoTrabajo_Representante = info.TelefonoTrabajo_Madre;
                info.CargoTrabajo_Representante = info.CargoTrabajo_Madre;
                info.AniosServicio_Representante = info.AniosServicio_Madre;
                info.IngresoMensual_Representante = info.IngresoMensual_Madre;
                info.VehiculoPropio_Representante = info.VehiculoPropio_Madre;
                info.Marca_Representante = info.Marca_Madre;
                info.Modelo_Representante = info.Modelo_Madre;
                info.AnioVehiculo_Representante = info.AnioVehiculo_Madre;
                info.CasaPropia_Representante = info.CasaPropia_Madre;
                info.EstaFallecido_Representante = info.EstaFallecido_Madre;
            }
            else
            {
                info.Naturaleza_Representante = info.Naturaleza_Representante;
                info.IdTipoDocumento_Representante = info.IdTipoDocumento_Representante;
                info.CedulaRuc_Representante = info.CedulaRuc_Representante;
                info.Nombres_Representante = info.Nombres_Representante;
                info.Apellidos_Representante = info.Apellidos_Representante;
                info.NombreCompleto_Representante = info.Apellidos_Representante + ' ' + info.Nombres_Representante;
                info.RazonSocial_Representante = info.RazonSocial_Representante;
                info.Direccion_Representante = info.Direccion_Representante;
                info.Telefono_Representante = info.Telefono_Representante;
                info.Celular_Representante = info.Celular_Representante;
                info.Correo_Representante = info.Correo_Representante;
                info.Sexo_Representante = (info.Sexo_Representante == "" ? null : info.Sexo_Representante);
                info.FechaNacimiento_Representante = (info.FechaNacimiento_Representante == null ? (DateTime?)null : info.FechaNacimiento_Representante);
                info.CodCatalogoCONADIS_Representante = (info.CodCatalogoCONADIS_Representante == "" ? null : info.CodCatalogoCONADIS_Representante);
                info.PorcentajeDiscapacidad_Representante = info.PorcentajeDiscapacidad_Representante;
                info.NumeroCarnetConadis_Representante = info.NumeroCarnetConadis_Representante;
                info.IdGrupoEtnico_Representante = (info.IdGrupoEtnico_Representante == 0 ? (int?)null : info.IdGrupoEtnico_Representante);
                info.IdReligion_Representante = (info.IdReligion_Representante == 0 ? (int?)null : info.IdReligion_Representante);
                info.IdEstadoCivil_Representante = (info.IdEstadoCivil_Representante == "" ? null : info.IdEstadoCivil_Representante);
                info.AsisteCentroCristiano_Representante = info.AsisteCentroCristiano_Representante;
                info.IdPais_Representante = (info.IdPais_Representante == "" ? null : info.IdPais_Representante);
                info.Cod_Region_Representante = (info.Cod_Region_Representante == "" ? null : info.Cod_Region_Representante);
                info.IdProvincia_Representante = (info.IdProvincia_Representante == "" ? null : info.IdProvincia_Representante);
                info.IdCiudad_Representante = (info.IdCiudad_Representante == "" ? null : info.IdCiudad_Representante);
                info.IdParroquia_Representante = (info.IdParroquia_Representante == "" ? null : info.IdParroquia_Representante);
                info.Sector_Representante = info.Sector_Representante;
                info.IdCatalogoPAREN_Representante = info.IdCatalogoPAREN_Representante;
                info.IdCatalogoFichaInst_Representante = (info.IdCatalogoFichaInst_Representante == 0 ? (int?)null : info.IdCatalogoFichaInst_Representante);
                info.EmpresaTrabajo_Representante = info.EmpresaTrabajo_Representante;
                info.IdProfesion_Representante = (info.IdProfesion_Representante == 0 ? (int?)null : info.IdCatalogoFichaInst_Representante);
                info.DireccionTrabajo_Representante = info.DireccionTrabajo_Representante;
                info.TelefonoTrabajo_Representante = info.TelefonoTrabajo_Representante;
                info.CargoTrabajo_Representante = info.CargoTrabajo_Representante;
                info.AniosServicio_Representante = info.AniosServicio_Representante;
                info.IngresoMensual_Representante = info.IngresoMensual_Representante;
                info.VehiculoPropio_Representante = info.VehiculoPropio_Representante;
                info.Marca_Representante = info.Marca_Representante;
                info.Modelo_Representante = info.Modelo_Representante;
                info.AnioVehiculo_Representante = info.AnioVehiculo_Representante;
                info.CasaPropia_Representante = info.CasaPropia_Representante;
                info.EstaFallecido_Representante = info.EstaFallecido_Representante;
            }

            if (!string.IsNullOrEmpty(info.IdTipoDocumento_Aspirante) && !string.IsNullOrEmpty(info.Naturaleza_Aspirante) && !string.IsNullOrEmpty(info.CedulaRuc_Aspirante))
            {
                if (cl_funciones.ValidaIdentificacion(info.IdTipoDocumento_Aspirante, info.Naturaleza_Aspirante, info.CedulaRuc_Aspirante, ref return_naturaleza))
                {
                    info.Naturaleza_Aspirante = return_naturaleza;
                    info.info_valido_aspirante = true;
                }
                else
                {
                    msg = "Número de identificación del aspirante inválida";
                    info.info_valido_aspirante = false;
                    return false;
                }
            }
            else
            {
                msg = "Complete la información del aspirante";
                info.info_valido_aspirante = false;
                return false;
            }

            if (!string.IsNullOrEmpty(info.IdTipoDocumento_Padre) && !string.IsNullOrEmpty(info.Naturaleza_Padre) && !string.IsNullOrEmpty(info.CedulaRuc_Padre))
            {
                if (cl_funciones.ValidaIdentificacion(info.IdTipoDocumento_Padre, info.Naturaleza_Padre, info.CedulaRuc_Padre, ref return_naturaleza_padre))
                {
                    info.Naturaleza_Padre = return_naturaleza_padre;
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
                msg = "Complete la información del padre";
                info.info_valido_padre = false;
                return false;
            }

            if (!string.IsNullOrEmpty(info.IdTipoDocumento_Madre) && !string.IsNullOrEmpty(info.Naturaleza_Madre) && !string.IsNullOrEmpty(info.CedulaRuc_Madre))
            {
                if (cl_funciones.ValidaIdentificacion(info.IdTipoDocumento_Madre, info.Naturaleza_Madre, info.CedulaRuc_Madre, ref return_naturaleza_madre))
                {
                    info.Naturaleza_Madre = return_naturaleza_madre;
                    info.info_valido_madre = true;
                }
                else
                {
                    msg = "Número de identificación del madre inválida";
                    info.info_valido_madre = false;
                    return false;
                }
            }
            else
            {
                msg = "Complete la información de la madre";
                info.info_valido_madre = false;
                return false;
            }

            if (!string.IsNullOrEmpty(info.IdTipoDocumento_Representante) && !string.IsNullOrEmpty(info.Naturaleza_Representante) && !string.IsNullOrEmpty(info.CedulaRuc_Representante))
            {
                if (cl_funciones.ValidaIdentificacion(info.IdTipoDocumento_Representante, info.Naturaleza_Representante, info.CedulaRuc_Representante, ref return_naturaleza_representante))
                {
                    info.Naturaleza_Representante = return_naturaleza_representante;
                    info.info_valido_representante = true;
                }
                else
                {
                    msg = "Número de identificación del representante inválida";
                    info.info_valido_representante = false;
                    return false;
                }
            }
            else
            {
                msg = "Complete la información del representante";
                info.info_valido_representante = false;
                return false;
            }

            if (info.info_valido_aspirante == true && info.info_valido_padre == true && info.info_valido_madre == true && info.info_valido_representante == true)
            {
                if (info.CedulaRuc_Padre != null && info.CedulaRuc_Madre != null && (info.CedulaRuc_Padre == info.CedulaRuc_Madre))
                {
                    msg = "No se puede registrar a la misma persona como padre y madre";
                    return false;
                }
            }
            else
            {
                msg = "Complete la información solicitada";
                return false;
            }

            return true;
        }
        #endregion

        #region JSON
        public JsonResult CantidadAdmisiones(string IdUsuario = "")
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var IdSede = Convert.ToInt32(SessionFixed.IdSede);
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa,0);
            var IdAnio = info_anio == null ? 0 : info_anio.IdAnio;

            var lst_admisiones = bus_admision.GetList_Academico(IdEmpresa,IdSede, IdAnio);
            var cantidad = lst_admisiones.Count();
            return Json(cantidad, JsonRequestBehavior.AllowGet);
    }
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

            var info_admision = bus_admision.GetInfo_CedulaAspirante(IdEmpresa, pe_cedulaRuc);
            resultado.IdAdmision = (info_admision == null ? 0 : info_admision.IdAdmision);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula_persona(string pe_cedulaRuc = "")
        {
            var resultado = new tb_persona_Info();
            resultado = bus_persona.get_info_x_num_cedula(pe_cedulaRuc);
            resultado = (resultado == null ? new tb_persona_Info() : resultado);

            resultado.anio = Convert.ToDateTime(resultado.pe_fechaNacimiento).Year.ToString();
            var mes = Convert.ToDateTime(resultado.pe_fechaNacimiento).Month;
            mes = mes - 1;
            resultado.mes = mes.ToString();
            resultado.dia = Convert.ToDateTime(resultado.pe_fechaNacimiento).Day.ToString();

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProcesarAdmision(int IdEmpresa = 0, decimal IdAdmision=0)
        {
            var resultado = "";
            var info_admision = bus_admision.GetInfo(IdEmpresa, IdAdmision);
            if (info_admision!=null)
            {
                if (info_admision.IdCatalogoESTADM == Convert.ToInt32(cl_enumeradores.eTipoCatalogoAdmision.REGISTRADO))
                {
                    info_admision.IdUsuarioModificacion = SessionFixed.IdUsuario;
                    info_admision.IdUsuarioRevision = SessionFixed.IdUsuario;
                    info_admision.IdCatalogoESTADM = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAdmision.ENPROCESO);

                    if (bus_admision.ModificarEstadoEnProceso(info_admision))
                    {
                        resultado = "La admisión esta en proceso de revisión";
                    }
                    else
                    {
                        resultado = "El registro no se ha actualizado";
                    }
                }
                else
                {
                    resultado = "La admisión ya esta siendo procesada por otro usuario: " + info_admision.IdUsuarioRevision;
                }   
            }
            else
            {
                resultado = "No existe el registro";
            }
            

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}

public class aca_ProcesarAdmision_List
{
    string Variable = "aca_ProcesarAdmision_Info";
    public List<aca_Admision_Info> get_list(decimal IdTransaccionSession)
    {
        if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
        {
            List<aca_Admision_Info> list = new List<aca_Admision_Info>();

            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
        return (List<aca_Admision_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
    }

    public void set_list(List<aca_Admision_Info> list, decimal IdTransaccionSession)
    {
        HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
    }
}