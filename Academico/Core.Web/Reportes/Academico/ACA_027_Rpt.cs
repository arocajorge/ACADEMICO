using System;
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

namespace Core.Web.Reportes.Academico
{
    public partial class ACA_027_Rpt : DevExpress.XtraReports.UI.XtraReport
    {
        tb_empresa_Bus bus_empresa = new tb_empresa_Bus();
        public string usuario { get; set; }
        public string empresa { get; set; }
        public ACA_027_Rpt()
        {
            InitializeComponent();
        }

        private void ACA_027_Rpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            int IdCatalogoParcial = string.IsNullOrEmpty(p_IdCatalogoParcial.Value.ToString()) ? 0 : Convert.ToInt32(p_IdCatalogoParcial.Value);
            bool MostrarRetirados = string.IsNullOrEmpty(p_MostrarRetirados.Value.ToString()) ? false : Convert.ToBoolean(p_MostrarRetirados.Value);

            aca_Catalogo_Bus bus_catalogo = new aca_Catalogo_Bus();
            var parcial = bus_catalogo.GetInfo(IdCatalogoParcial);
            if (parcial != null)
            {
                lbl_parcial.Text = parcial.NomCatalogo;
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

            ACA_027_Bus bus_rpt = new ACA_027_Bus();
            List<ACA_027_Info> lst_rpt = new List<ACA_027_Info>();
            lst_rpt = bus_rpt.GetList(IdEmpresa, IdAnio, IdSede, IdNivel, IdJornada, IdCurso, IdParalelo, IdCatalogoParcial, MostrarRetirados);

            List<ACA_027_Info> ListaGrafico = new List<ACA_027_Info>();
            ListaGrafico = (from q in lst_rpt
                             group q by new
                             {
                                 q.IdEmpresa,
                                 q.Letra,
                                 q.Num
                             } into ing
                             select new ACA_027_Info
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
