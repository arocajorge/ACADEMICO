using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Core.Admision.Controllers
{
    public class AdmisionController : Controller
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
        string MensajeSuccess = "La transacción se ha realizado con éxito";
        string mensaje = string.Empty;
        #endregion

        public ActionResult Index()
        {
            int IdEmpresa = 1;
            var info_anio = bus_anio.GetInfo_AnioEnCurso(IdEmpresa,0); 
            var model = new aca_Admision_Info
            {
                CedulaRuc_Aspirante = "0919571018",
                IdEmpresa= IdEmpresa,
                IdAnio = (info_anio==null ? 0 : info_anio.IdAnio),
                IdSede=0,
                IdJornada=0,
                IdNivel=0,
                IdCurso=0,
                Naturaleza_Aspirante = "NATU",
                Naturaleza_Padre = "NATU",
                Naturaleza_Madre = "NATU",
                Naturaleza_Representante = "NATU",
                IdTipoDocumento_Aspirante = "CED",
                IdTipoDocumento_Madre = "CED",
                IdTipoDocumento_Padre = "CED",
                IdTipoDocumento_Representante = "CED",
                IdReligion_Aspirante = 0,
                IdReligion_Padre = 0,
                IdReligion_Madre = 0,
                IdReligion_Representante = 0,
                CodCatalogoCONADIS_Aspirante = "",
                CodCatalogoCONADIS_Padre = "",
                CodCatalogoCONADIS_Madre= "",
                CodCatalogoCONADIS_Representante = "",
                IdGrupoEtnico_Aspirante = 0,
                IdGrupoEtnico_Padre = 0,
                IdGrupoEtnico_Madre = 0,
                IdGrupoEtnico_Representante = 0,
                IdEstadoCivil_Padre = "",
                IdEstadoCivil_Madre = "",
                IdEstadoCivil_Representante = "",
                AsisteCentroCristiano_Aspirante = false,
                AsisteCentroCristiano_Padre = false,
                AsisteCentroCristiano_Madre = false,
                AsisteCentroCristiano_Representante = false,
                CodCatalogoSangre_Aspirante = "",
                Sexo_Aspirante = "",
                Sexo_Padre = "SEXO_MAS",
                Sexo_Madre = "SEXO_FEM",
                Sector_Representante = "",
                Sector_Aspirante = "",
                Sector_Padre = "",
                Sector_Madre = "",
                IdPais_Aspirante = "1",
                IdPais_Padre = "1",
                IdPais_Madre= "1",
                Cod_Region_Aspirante = "00001",
                Cod_Region_Padre = "00001",
                Cod_Region_Madre = "00001",
                Cod_Region_Representante = "00001",
                IdProvincia_Aspirante = "09",
                IdProvincia_Padre = "09",
                IdProvincia_Madre = "09",
                IdProvincia_Representante = "09",
                IdProfesion_Madre=0,
                IdProfesion_Padre=0,
                IdProfesion_Representante=0,
                IdCatalogoFichaInst_Madre = 0,
                IdCatalogoFichaInst_Padre=0,
                IdCatalogoFichaInst_Representante=0,
                IdCatalogoPAREN_Madre = Convert.ToInt32(cl_enumeradores.eTipoParentezco.MAMA),
                IdCatalogoPAREN_Padre = Convert.ToInt32(cl_enumeradores.eTipoParentezco.PAPA),
                IdCatalogoPAREN_Representante = Convert.ToInt32(cl_enumeradores.eTipoParentezco.OTROS),
                SueldoPadre = 0,
                SueldoMadre= 0,
                OtroIngresoMadre = 0,
                OtroIngresoPadre = 0,
                GastoAlimentacion = 0,
                GastoArriendo = 0,
                GastoPrestamo = 0,
                GastoEducacion = 0,
                GastoSalud = 0,
                GastoServicioBasico = 0,
                OtroGasto = 0,
                TotalEgresos = 0,
                TotalIngresos = 0,
                Saldo = 0,
                IdCiudad_Aspirante = "",
                IdCiudad_Madre = "",
                IdCiudad_Padre = "",
                IdCiudad_Representante = "",
                IdParroquia_Aspirante = "",
                IdParroquia_Padre = "",
                IdParroquia_Madre = "",
                IdParroquia_Representante = "",
                FechaActual = DateTime.Now,
                FechaNacimiento_Aspirante = DateTime.Now,
                FechaNacimiento_Padre = DateTime.Now,
                FechaNacimiento_Madre = DateTime.Now,
                FechaNacimiento_Representante = DateTime.Now,
                IdCatalogoESTADM = Convert.ToInt32(cl_enumeradores.eTipoCatalogoAdmision.REGISTRADO)
            };

            cargar_combos(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(aca_Admision_Info model, IEnumerable<HttpPostedFileBase> ArchivosAspirante)
        {
            if (!validar(model, ref mensaje))
            {
                ViewBag.mensaje = mensaje;
                cargar_combos(model);
                return View(model);
            }

            if (!bus_admision.GuardarDB(model))
            {
                ViewBag.mensaje = "No se ha podido guardar el registro";
                cargar_combos(model);
                return View(model);
            }
            
            if (ArchivosAspirante != null)
            {
                //var IdAdmision = bus_admision.GetId(model.IdEmpresa);
                var FilePath = Server.MapPath("~/Content/aspirantes/"+model.IdAdmision);
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }

                foreach (var file in ArchivosAspirante)
                {
                    if (file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(FilePath, fileName);
                        file.SaveAs(path);
                    }
                }
            }

            return RedirectToAction("Index");
        }

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
        }

        private bool validar(aca_Admision_Info info, ref string msg)
        {
            string return_naturaleza = "";
            string return_naturaleza_padre = "";
            string return_naturaleza_madre = "";
            string return_naturaleza_representante = "";

            //if (info.CantidadArchivos == 0)
            //{
            //    msg = "Debe subir los documentos solicitados para la admisión";
            //    info.info_valido_aspirante = false;
            //    return false;
            //}

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

            if (info.info_valido_aspirante == true && info.info_valido_padre==true && info.info_valido_madre == true && info.info_valido_representante==true)
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
        public JsonResult CargarJornada(int IdEmpresa = 0, int IdAnio=0, int IdSede = 0)
        {
            var lst_jornada = bus_jornada.GetList_Combos(IdEmpresa, IdAnio, IdSede);

            var resultado = (lst_jornada == null ? new List<aca_Jornada_Info>() : lst_jornada);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarNivel(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada=0)
        {
            var lst_nivel = bus_nivel.GetList(IdEmpresa, IdAnio, IdSede);

            var resultado = (lst_nivel == null ? new List<aca_NivelAcademico_Info>() : lst_nivel);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarCurso(int IdEmpresa = 0, int IdAnio = 0, int IdSede = 0, int IdJornada = 0, int IdNivel = 0)
        {
            var lst_curso = bus_curso.GetList_Combos(IdEmpresa, IdAnio, IdSede, IdJornada, IdNivel);

            var resultado = (lst_curso == null ? new List<aca_Curso_Info>() : lst_curso);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarRegion(string IdPais= "")
        {
            var lst_region = bus_region.get_list(IdPais, false);
            lst_region.Add(new tb_region_Info { Cod_Region = "", Nom_region = "--- Seleccione ---" });
            var resultado = (lst_region == null ? new List<tb_region_Info>() : lst_region);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarProvincia(string IdPais = "")
        {
            var lst_provincia = bus_provincia.get_list(IdPais, false);
            lst_provincia.Add(new tb_provincia_Info { IdProvincia = "", Descripcion_Prov = "--- Seleccione ---" });

            var resultado = (lst_provincia == null ? new List<tb_provincia_Info>() : lst_provincia);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarCiudad(string IdProvincia="")
        {
            var lst_ciudad = bus_ciudad.get_listCombos(IdProvincia, false);
            lst_ciudad.Add(new tb_ciudad_Info { IdCiudad = "", Descripcion_Ciudad = "--- Seleccione ---" });

            var resultado = (lst_ciudad == null ? new List<tb_ciudad_Info>() : lst_ciudad);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargarParroquia(string IdCiudad="")
        {
            var lst_parroquia = bus_parroquia.get_listCombos(IdCiudad, false);
            lst_parroquia.Add(new tb_parroquia_Info { IdParroquia = "", nom_parroquia = "--- Seleccione ---" });

            var resultado = (lst_parroquia == null ? new List<tb_parroquia_Info>() : lst_parroquia);
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
            resultado.IdAdmision = (info_admision==null ? 0 : info_admision.IdAdmision);
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
        #endregion
    }
}