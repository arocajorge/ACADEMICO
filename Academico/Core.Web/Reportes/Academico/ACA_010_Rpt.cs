using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.Academico;
using Core.Bus.General;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_010_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        public ACA_010_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_010_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
            aca_CatalogoTipo_Bus bus_catalogo_tipo = new aca_CatalogoTipo_Bus();
            aca_AnioLectivo_Bus bus_anio = new aca_AnioLectivo_Bus();
            aca_Sede_Bus bus_sede = new aca_Sede_Bus();
            tb_empresa_Bus bus_empresa = new tb_empresa_Bus();

            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            int IdMateria = string.IsNullOrEmpty(p_IdMateria.Value.ToString()) ? 0 : Convert.ToInt32(p_IdMateria.Value);
            int IdCatalogoParcial = string.IsNullOrEmpty(p_IdCatalogoParcial.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoParcial.Value);

            //var info_catalogo = bus_catalogo.GetInfo(IdCatalogoParcial);
            //var info_catalogo_tipo = bus_catalogo_tipo.GetInfo((info_catalogo == null ? 0 : info_catalogo.IdCatalogoTipo));
            //var info_anio = bus_anio.GetInfo(IdEmpresa, IdAnio);
            //var TituloParcial = (info_catalogo_tipo==null ? "" : info_catalogo_tipo.NomCatalogoTipo) + " - " + (info_catalogo==null ? "" : info_catalogo.NomCatalogo) + " - " + (info_anio==null ? "" : info_anio.Descripcion);
            //lbl_parcial.Text = TituloParcial;

            var info_sede = bus_sede.GetInfo(IdEmpresa, IdSede);
            if (info_sede != null)
            {
                //lbl_secretaria.Text = info_sede.NombreSecretaria;
            }

            var emp = bus_empresa.get_info(IdEmpresa);
            if (emp != null)
            {
                if (emp.em_logo != null)
                {
                    ImageConverter obj = new ImageConverter();
                    lbl_imagen.Image = (Image)obj.ConvertFrom(emp.em_logo);
                }
            }

            ACA_010_Bus bus_rpt = new ACA_010_Bus();
            List<ACA_010_Info> lst_rpt = new List<ACA_010_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdMateria, IdCatalogoParcial);

            this.DataSource = lst_rpt;
        }
    }
}
