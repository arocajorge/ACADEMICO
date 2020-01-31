﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Core.Bus.General;
using System.Collections.Generic;
using Core.Bus.Reportes.Academico;
using Core.Info.Reportes.Academico;
using Core.Bus.Academico;
using Core.Info.Helps;
using Core.Web.Helps;

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_001_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        public string usuario { get; set; }
        public string empresa { get; set; }

        public ACA_001_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_001_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            aca_Sede_Bus bus_sede = new aca_Sede_Bus();
            aca_Familia_Bus bus_familia = new aca_Familia_Bus();
            lbl_fecha.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            lbl_usuario.Text = usuario;

            int IdEmpresa = string.IsNullOrEmpty(p_IdEmpresa.Value.ToString()) ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            int IdAnio = string.IsNullOrEmpty(p_IdAnio.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAnio.Value);
            int IdSede = string.IsNullOrEmpty(p_IdSede.Value.ToString()) ? 0 : Convert.ToInt32(p_IdSede.Value);
            decimal IdAlumno = string.IsNullOrEmpty(p_IdAlumno.Value.ToString()) ? 0 : Convert.ToInt32(p_IdAlumno.Value);

            ACA_001_Bus bus_rpt = new ACA_001_Bus();
            List<ACA_001_Info> lst_rpt = bus_rpt.GetList(IdEmpresa, IdAlumno, IdAnio);

            var info_rep = bus_familia.GetInfo_Representante(IdEmpresa,IdAlumno, cl_enumeradores.eTipoRepresentante.LEGAL.ToString());
            string RepLegal = "";
            if (info_rep!=null)
            {
                RepLegal = info_rep.pe_nombreCompleto;
            }

            lbl_compromiso.Text.Replace("_.", RepLegal);

            var info_sede = bus_sede.GetInfo(IdEmpresa,IdSede);
            lbl_sede.Text = info_sede.NomSede;
            lbl_rector.Text = info_sede.NombreRector;
            lbl_secretaria.Text = info_sede.NombreSecretaria;
            lbl_representante.Text = info_rep.pe_cedulaRuc;

            this.DataSource = lst_rpt;
        }
        
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRSubreport)sender).ReportSource.Parameters["p_IdEmpresa"].Value = p_IdEmpresa.Value == null ? 0 : Convert.ToInt32(p_IdEmpresa.Value);
            ((XRSubreport)sender).ReportSource.Parameters["p_IdAlumno"].Value = p_IdAlumno.Value == null ? 0 : Convert.ToDecimal(p_IdAlumno.Value);

            ((XRSubreport)sender).ReportSource.RequestParameters = false;
        }
    }
}
