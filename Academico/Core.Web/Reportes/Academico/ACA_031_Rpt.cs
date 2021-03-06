﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using System.Collections.Generic;
using Core.Bus.General;
using Core.Bus.Academico;
using System.Linq;
using Core.Info.Helps;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_031_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_031_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_031_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            int IdNivel = string.IsNullOrEmpty(p_IdNivel.Value.ToString()) ? 0 : Convert.ToInt32(p_IdNivel.Value);
            int IdJornada = string.IsNullOrEmpty(p_IdJornada.Value.ToString()) ? 0 : Convert.ToInt32(p_IdJornada.Value);
            int IdCurso = string.IsNullOrEmpty(p_IdCurso.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCurso.Value);
            int IdParalelo = string.IsNullOrEmpty(p_IdParalelo.Value.ToString()) ? 0 : Convert.ToInt32(p_IdParalelo.Value);
            string IdCatalogoParcialTipo = string.IsNullOrEmpty(p_IdCatalogoParcialTipo.Value.ToString()) ? "" : p_IdCatalogoParcialTipo.Value.ToString();

            var parcial = "";
            if (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_1.ToString())
            {
                parcial = "PROMEDIO QUIMESTRE 1";
            }
            if (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.QUIMESTRE_2.ToString())
            {
                parcial = "PROMEDIO QUIMESTRE 2";
            }
            if (IdCatalogoParcialTipo == cl_enumeradores.eTipoCatalogoAcademicoConductaFinal.PROMEDIOFINAL.ToString())
            {
                parcial = "PROMEDIO FINAL";
            }

            if (parcial != null)
            {
                lbl_parcial.Text = parcial;
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

            ACA_031_Bus bus_rpt = new ACA_031_Bus();
            List<ACA_031_Info> lst_rpt = new List<ACA_031_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcialTipo);

            List<ACA_031_Info> ListaGrafico = new List<ACA_031_Info>();
            ListaGrafico = (from q in lst_rpt
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.Letra,
                                 q.Num
                             } into ing
                             select new ACA_031_Info
                             {
                                 IdEmpresa = ing.Key.IdEmpresa,
                                 Letra = ing.Key.Letra,
                                 Num =  ing.Sum(q=>q.Num)
                             }).ToList();

            xrChart1.DataSource = ListaGrafico;
            this.DataSource = lst_rpt;
        }

        private void xrChart1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
