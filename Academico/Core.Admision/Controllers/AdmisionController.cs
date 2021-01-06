using Core.Bus.Academico;
using Core.Bus.General;
using Core.Info.Academico;
using Core.Info.General;
using Core.Info.Helps;
using System;
using System.Collections.Generic;
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
        #endregion

        public ActionResult Index()
        {
            var model = new aca_Admision_Info
            {
                IdEmpresa = 1
            };

            cargar_combos(model);
            return View(model);
        }

        #region Metodos
        private void cargar_combos(aca_Admision_Info model)
        {
            var lst_sexo = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.SEXO), false);
            var lst_estado_civil = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.ESTCIVIL), false);
            var lst_tipo_doc = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODOC), false);
            var lst_tipo_naturaleza = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPONATPER), false);
            var lst_tipo_sangre = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPOSANGRE), false);
            var lst_tipo_discapacidad = bus_catalogo.get_list(Convert.ToInt32(cl_enumeradores.eTipoCatalogoGeneral.TIPODISCAP), false);
            var lst_instruccion = bus_catalogo_ficha.GetList_x_Tipo(Convert.ToInt32(cl_enumeradores.eTipoCatalogoSocioEconomico.INSTRUCCION), false);
            lst_instruccion.Add(new aca_CatalogoFicha_Info { IdCatalogoFicha = 0, NomCatalogoFicha = "" });
            lst_tipo_discapacidad.Add(new tb_Catalogo_Info { CodCatalogo = "", ca_descripcion = "" });
            var lst_profesion = bus_profesion.GetList(false);
            var lst_religion = bus_religion.GetList(false);
            var lst_grupoetnico = bus_grupoetnico.GetList(false);

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
        }

        #endregion

        #region JSON
        public JsonResult Validar_cedula_ruc(string naturaleza = "", string tipo_documento = "", string cedula_ruc = "")
        {
            var return_naturaleza = "";
            var isValid = cl_funciones.ValidaIdentificacion(tipo_documento, naturaleza, cedula_ruc, ref return_naturaleza);

            return Json(new { isValid = isValid, return_naturaleza = return_naturaleza }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult get_info_x_num_cedula(string pe_cedulaRuc = "")
        {
            var info_persona = bus_persona.get_info_x_num_cedula(pe_cedulaRuc);

            var resultado = (info_persona == null ? new tb_persona_Info() : info_persona);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}