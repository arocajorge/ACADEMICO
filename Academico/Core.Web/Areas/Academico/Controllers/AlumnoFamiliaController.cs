using Core.Bus.Academico;
using Core.Bus.Facturacion;
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
    public class AlumnoFamiliaController : Controller
    {
        #region Variables
        aca_Familia_Bus bus_familia = new aca_Familia_Bus();
        aca_Familia_List Lista_Familia = new aca_Familia_List();
        tb_persona_Bus bus_persona = new tb_persona_Bus();
        tb_Catalogo_Bus bus_catalogo = new tb_Catalogo_Bus();
        aca_Catalogo_Bus bus_aca_catalogo = new aca_Catalogo_Bus();
        tb_profesion_Bus bus_profesion = new tb_profesion_Bus();
        aca_CatalogoFicha_Bus bus_catalogo_ficha = new aca_CatalogoFicha_Bus();
        aca_Sede_Bus bus_sede = new aca_Sede_Bus();
        fa_cliente_tipo_Bus bus_clientetipo = new fa_cliente_tipo_Bus();
        fa_TerminoPago_Bus bus_termino_pago = new fa_TerminoPago_Bus();
        fa_cliente_Bus bus_cliente = new fa_cliente_Bus();
        fa_cliente_contactos_Bus bus_cliente_cont = new fa_cliente_contactos_Bus();
        tb_ciudad_Bus bus_ciudad = new tb_ciudad_Bus();
        tb_Religion_Bus bus_religion = new tb_Religion_Bus();
        tb_sucursal_Bus bus_sucursal = new tb_sucursal_Bus();
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        #endregion

        #region Metodos
        private void cargar_combos()
        {
            var IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            var lst_tipo_sangre = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPOSANGRE), false);
            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            var lst_parentezco = bus_aca_catalogo.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoAcademico.PAREN),false);
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "" });
            var lst_instruccion = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.INSTRUCCION), false);
            lst_instruccion.Add(new aca_CatalogoFicha_Info { IdCatalogoFicha = 0, NomCatalogoFicha = "" });
            var lst_profesion = bus_profesion.GetList(false);
            lst_profesion.Add(new tb_profesion_Info { IdProfesion = 0, Descripcion = "" });
            var lst_religion = bus_religion.GetList(false);

            ViewBag.lst_sexo = lst_sexo;
            ViewBag.lst_estado_civil = lst_estado_civil;
            ViewBag.lst_tipo_doc = lst_tipo_doc;
            ViewBag.lst_tipo_naturaleza = lst_tipo_naturaleza;
            ViewBag.lst_tipo_sangre = lst_tipo_sangre;
            ViewBag.lst_tipo_discapacidad = lst_tipo_discapacidad;
            ViewBag.lst_parentezco = lst_parentezco;
            ViewBag.lst_profesion = lst_profesion;
            ViewBag.lst_instruccion = lst_instruccion;
            ViewBag.lst_religion = lst_religion;

            var lst_termino_pago = bus_termino_pago.get_list(false);
            ViewBag.lst_termino_pago = lst_termino_pago;

            var lst_clientetipo = bus_clientetipo.get_list(IdEmpresa, false);
            ViewBag.lst_clientetipo = lst_clientetipo;

            var lst_ciudad = bus_ciudad.get_list("", false);
            ViewBag.lst_ciudad = lst_ciudad;
        }

        private bool validar(aca_Familia_Info info, ref string msg)
        {
            string return_naturaleza = "";

            if (cl_funciones.ValidaIdentificacion(info.IdTipoDocumento, info.pe_Naturaleza, info.pe_cedulaRuc, ref return_naturaleza))
            {
                info.pe_Naturaleza = return_naturaleza;
            }
            else
            {
                msg = "Número de identificación del alumno inválida";
                return false;
            }

            return true;
        }
        #endregion

        #region Combos
        public ActionResult cmb_parroquia()
        {
            string IdCiudad_fact = (Request.Params["fx_IdCiudad_fact"] != null) ? Request.Params["fx_IdCiudad_fact"].ToString() : "";
            return PartialView("_cmb_parroquia", new aca_Familia_Info { IdCiudad_fact = IdCiudad_fact });
        }

        public ActionResult ComboBoxPartial_Pais()
        {
            return PartialView("_ComboBoxPartial_Pais", new aca_Alumno_Info());
        }
        public ActionResult ComboBoxPartial_Region()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            return PartialView("_ComboBoxPartial_Region", new aca_Alumno_Info { IdPais = IdPais });
        }
        public ActionResult ComboBoxPartial_Provincia()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            string Cod_Region = (Request.Params["Cod_Region"] != null) ? Convert.ToString(Request.Params["Cod_Region"]) : "";
            return PartialView("_ComboBoxPartial_Provincia", new aca_Alumno_Info { IdPais = IdPais, Cod_Region = Cod_Region });
        }
        public ActionResult ComboBoxPartial_Ciudad()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            string Cod_Region = (Request.Params["Cod_Region"] != null) ? Convert.ToString(Request.Params["Cod_Region"]) : "";
            string IdProvincia = (Request.Params["IdProvincia"] != null) ? Convert.ToString(Request.Params["IdProvincia"]) : "";
            return PartialView("_ComboBoxPartial_Ciudad", new aca_Alumno_Info { IdPais = IdPais, Cod_Region = Cod_Region, IdProvincia = IdProvincia });
        }
        public ActionResult ComboBoxPartial_Parroquia()
        {
            string IdPais = (Request.Params["IdPais"] != null) ? Convert.ToString(Request.Params["IdPais"]) : "";
            string Cod_Region = (Request.Params["Cod_Region"] != null) ? Convert.ToString(Request.Params["Cod_Region"]) : "";
            string IdProvincia = (Request.Params["IdProvincia"] != null) ? Convert.ToString(Request.Params["IdProvincia"]) : "";
            string IdCiudad = (Request.Params["IdCiudad"] != null) ? Convert.ToString(Request.Params["IdCiudad"]) : "";
            return PartialView("_ComboBoxPartial_Parroquia", new aca_Alumno_Info { IdPais = IdPais, Cod_Region = Cod_Region, IdProvincia = IdProvincia, IdCiudad = IdCiudad });
        }
        #endregion

        #region Index
        public ActionResult Index(int IdEmpresa = 0, int IdAlumno = 0)
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;
            aca_Familia_Info model = new aca_Familia_Info
            {
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };

            List<aca_Familia_Info> lista = bus_familia.GetList(IdEmpresa, IdAlumno);
            Lista_Familia.set_list(lista, Convert.ToDecimal(SessionFixed.IdTransaccionSession));

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial_AlumnoFamilia(int IdEmpresa=0, decimal IdAlumno=0)
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;
            List<aca_Familia_Info> model = Lista_Familia.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_AlumnoFamilia", model);
        }
        #endregion

        #region Metodos ComboBox bajo demanda alumno
        public ActionResult Cmb_FamiliaAlumno()
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

        #region Json
        public JsonResult Validar_cedula_ruc(string naturaleza = "", string tipo_documento = "", string cedula_ruc = "")
        {
            var return_naturaleza = "";
            var isValid = cl_funciones.ValidaIdentificacion(tipo_documento, naturaleza, cedula_ruc, ref return_naturaleza);

            return Json(new { isValid = isValid, return_naturaleza = return_naturaleza }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula(int IdEmpresa = 0, decimal IdAlumno=0, string pe_cedulaRuc = "")
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

        #region Acciones
        public ActionResult Nuevo(int IdEmpresa = 0, int IdAlumno = 0)
        {
            aca_Familia_Info model = new aca_Familia_Info
            {
                IdEmpresa = IdEmpresa,
                IdAlumno = IdAlumno,
                pe_Naturaleza = "NATU",
                CodCatalogoCONADIS = "",
                IdTipoCredito ="CON",
                Idtipo_cliente = 1,
                IdCiudad_fact = "09",
                IdParroquia_fact = "09",
                IdPais = "1",
                Cod_Region = "00001",
                IdProvincia = "09",
                IdCiudad = "09",
                IdParroquia = "09",
            };
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;
            cargar_combos();
            return View(model);
        }
        [HttpPost]
        public ActionResult Nuevo(aca_Familia_Info model)
        {
            model.IdUsuarioCreacion = SessionFixed.IdUsuario;
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            model.IdSucursal = bus_sede.GetInfo(model.IdEmpresa, model.IdSede).IdSucursal;

            var info_persona_familia = new tb_persona_Info
            {
                IdPersona = model.IdPersona,
                pe_Naturaleza = model.pe_Naturaleza,
                IdTipoDocumento = model.IdTipoDocumento,
                pe_cedulaRuc = model.pe_cedulaRuc,
                pe_nombre = model.pe_nombre,
                pe_apellido = model.pe_apellido,
                pe_nombreCompleto = model.pe_nombreCompleto,
                pe_razonSocial = model.pe_razonSocial,
                IdEstadoCivil = model.IdEstadoCivil,
                pe_sexo = model.pe_sexo,
                CodCatalogoSangre = model.CodCatalogoSangre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                NumeroCarnetConadis = model.NumeroCarnetConadis,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                pe_fechaNacimiento = model.pe_fechaNacimiento,
                pe_telfono_Contacto = model.pe_telfono_Contacto,
                pe_correo = model.Correo,
                pe_celular = model.Celular,
                pe_direccion = model.Direccion,
                IdProfesion = model.IdProfesion,
                IdReligion = model.IdReligion,
                AsisteCentroCristiano = model.AsisteCentroCristiano
            };

            model.info_persona= info_persona_familia;

            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos();
                return View(model);
            }

            if (!bus_familia.guardarDB(model))
            {
                ViewBag.IdAlumno = model.IdAlumno;
                ViewBag.IdEmpresa = model.IdEmpresa;
                cargar_combos();
                return View(model);
            }

            //return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno });
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Secuencia = model.Secuencia, Exito = true });
        }

        public ActionResult Modificar(int IdEmpresa = 0, int IdAlumno = 0, int Secuencia = 0, bool Exito = false)
        {
            aca_Familia_Info model = bus_familia.GetInfo(IdEmpresa, IdAlumno, Secuencia);
            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdAlumno = IdAlumno });

            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);
            var info_cliente = bus_cliente.get_info_x_num_cedula(model.IdEmpresa, model.pe_cedulaRuc);
            var cliente = bus_cliente.get_info(model.IdEmpresa, info_cliente.IdCliente);
            model.IdTipoCredito = ((cliente == null || cliente.IdCliente==0) ? "CON" : cliente.IdTipoCredito);
            model.Idtipo_cliente = ((cliente == null || cliente.Idtipo_cliente == 0) ? 1 : cliente.Idtipo_cliente);
            var IdCliente = ((cliente == null || cliente.IdCliente == 0) ? 0 : cliente.IdCliente);
            var info_contacto = bus_cliente_cont.get_info(model.IdEmpresa, IdCliente, 1);
            model.IdCiudad_fact = (info_contacto == null ? "09" : info_contacto.IdCiudad);
            model.IdParroquia_fact = (info_contacto == null ? "09" : info_contacto.IdParroquia);

            if (Exito)
                ViewBag.MensajeSuccess = MensajeSuccess;

            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificar(aca_Familia_Info model)
        {
            model.IdUsuarioModificacion = SessionFixed.IdUsuario;
            model.IdSede = Convert.ToInt32(SessionFixed.IdSede);
            model.IdSucursal = bus_sede.GetInfo(model.IdEmpresa, model.IdSede).IdSucursal;

            var info_persona_familia = new tb_persona_Info
            {
                IdPersona = model.IdPersona,
                pe_Naturaleza = model.pe_Naturaleza,
                IdTipoDocumento = model.IdTipoDocumento,
                pe_cedulaRuc = model.pe_cedulaRuc,
                pe_nombre = model.pe_nombre,
                pe_apellido = model.pe_apellido,
                pe_nombreCompleto = model.pe_nombreCompleto,
                pe_razonSocial = model.pe_razonSocial,
                IdEstadoCivil = model.IdEstadoCivil,
                pe_sexo = model.pe_sexo,
                CodCatalogoSangre = model.CodCatalogoSangre,
                CodCatalogoCONADIS = model.CodCatalogoCONADIS,
                NumeroCarnetConadis = model.NumeroCarnetConadis,
                PorcentajeDiscapacidad = model.PorcentajeDiscapacidad,
                pe_fechaNacimiento = model.pe_fechaNacimiento,
                pe_telfono_Contacto = model.pe_telfono_Contacto,
                pe_correo = model.Correo,
                pe_celular = model.Celular,
                pe_direccion = model.Direccion,
                IdReligion=model.IdReligion,
                AsisteCentroCristiano = model.AsisteCentroCristiano,
                IdProfesion = model.IdProfesion
            };

            model.info_persona = info_persona_familia;

            if (!bus_familia.modificarDB(model))
            {
                ViewBag.IdAlumno = model.IdAlumno;
                ViewBag.IdEmpresa = model.IdEmpresa;
                cargar_combos();
                return View(model);
            }

            //return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno });
            return RedirectToAction("Modificar", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno, Secuencia = model.Secuencia, Exito = true });
        }

        public ActionResult Anular(int IdEmpresa = 0, int IdAlumno = 0, int Secuencia = 0)
        {
            aca_Familia_Info model = bus_familia.GetInfo(IdEmpresa, IdAlumno, Secuencia);
            model.CodCatalogoCONADIS = (model.CodCatalogoCONADIS == null ? "" : model.CodCatalogoCONADIS);
            var info_cliente = bus_cliente.get_info_x_num_cedula(model.IdEmpresa, model.pe_cedulaRuc);
            var cliente = bus_cliente.get_info(model.IdEmpresa, info_cliente.IdCliente);
            model.IdTipoCredito = ((cliente == null || cliente.IdCliente == 0) ? "" : cliente.IdTipoCredito);
            model.Idtipo_cliente = ((cliente == null || cliente.Idtipo_cliente == 0) ? 1 : info_cliente.Idtipo_cliente);
            var IdCliente = ((cliente == null || cliente.IdCliente == 0) ? 0 : cliente.IdCliente);
            var info_contacto = bus_cliente_cont.get_info(model.IdEmpresa, IdCliente, 1);
            model.IdCiudad_fact = (info_contacto == null ? "09" : info_contacto.IdCiudad);
            model.IdParroquia_fact = (info_contacto == null ? "09" : info_contacto.IdParroquia);

            if (model == null)
                return RedirectToAction("Index", new { IdEmpresa = IdEmpresa, IdAlumno = IdAlumno });
            ViewBag.IdEmpresa = IdEmpresa;
            ViewBag.IdAlumno = IdAlumno;
            cargar_combos();
            return View(model);
        }

        [HttpPost]
        public ActionResult Anular(aca_Familia_Info model)
        {
            model.IdUsuarioAnulacion = SessionFixed.IdUsuario;
            if (!bus_familia.anularDB(model))
            {
                ViewBag.IdAlumno = model.IdAlumno;
                ViewBag.IdEmpresa = model.IdEmpresa;
                cargar_combos();
                return View(model);
            }

            return RedirectToAction("Index", new { IdEmpresa = model.IdEmpresa, IdAlumno = model.IdAlumno });
        }

        #endregion
    }

    public class aca_Familia_List
    {
        string Variable = "aca_Familia_Info";
        public List<aca_Familia_Info> get_list(decimal IdTransaccionSession)
        {
            if (HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] == null)
            {
                List<aca_Familia_Info> list = new List<aca_Familia_Info>();

                HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
            }
            return (List<aca_Familia_Info>)HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()];
        }

        public void set_list(List<aca_Familia_Info> list, decimal IdTransaccionSession)
        {
            HttpContext.Current.Session[Variable + IdTransaccionSession.ToString()] = list;
        }
    }
}