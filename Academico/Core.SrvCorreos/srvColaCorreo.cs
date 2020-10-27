using Core.Bus.General;
using Core.Web.Reportes.CuentasPorCobrar;
using Core.Web.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Core.SrvCorreos
{
    public partial class srvColaCorreo : ServiceBase
    {
        tb_ColaCorreo_Bus busCorreo = new tb_ColaCorreo_Bus();
        tb_ColaCorreoParametros_Bus busCorreoParam = new tb_ColaCorreoParametros_Bus();
        tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
        
        tb_empresa_Bus busEmpresa = new tb_empresa_Bus();
        public srvColaCorreo()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Serv_ColaCorreo();
        }

        private void Serv_ColaCorreo()
        {
            try
            {
                #region Get correo por enviar
                var CorreoInfo = busCorreo.GetInfoPendienteEnviar();
                if (CorreoInfo == null)
                    return;
                #endregion
              
                #region Armar correo
                MailMessage mail = new MailMessage();
                try
                {
                    
                    mail.From = new MailAddress(CorreoInfo.ParametroInfo.Usuario);
                    mail.Subject = CorreoInfo.Asunto;
                    mail.IsBodyHtml = true;

                    string[] Correos = CorreoInfo.Destinatarios.Split(';');
                    foreach (var item in Correos)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            mail.To.Add(item.Trim());
                        }
                    }
                }
                catch (Exception ex)
                {
                    CorreoInfo.Error = ex.Message;
                    busCorreo.ModificarDB(CorreoInfo);
                }
                #endregion

                #region Adjunto
                MemoryStream mem = new MemoryStream();
                try
                {
                    var Empresa = busEmpresa.get_info(CorreoInfo.IdEmpresa);
                    string RootReporte = System.IO.Path.GetTempPath() + "Rpt_CorreoAdjunto" + CorreoInfo.Codigo + CorreoInfo.Parametros + ".repx";
                    switch (CorreoInfo.Codigo)
                    {
                        case "FAC_002":

                            #region FAC_002
                            FAC_002_Rpt rpt = new FAC_002_Rpt();

                            #region Cargo diseño desde base
                            var reporte = bus_rep_x_emp.GetInfo(CorreoInfo.IdEmpresa, "FAC_002");
                            if (reporte != null)
                            {
                                System.IO.File.WriteAllBytes(RootReporte, reporte.ReporteDisenio);
                                rpt.LoadLayout(RootReporte);
                            }
                            #endregion

                            #region Parametros
                            if (!string.IsNullOrEmpty(CorreoInfo.Parametros))
                            {
                                string[] Parametros = CorreoInfo.Parametros.Split(';');
                                rpt.p_IdEmpresa.Value = Parametros[0];
                                rpt.p_IdSucursal.Value = Parametros[1];
                                rpt.p_IdBodega.Value = Parametros[2];
                                rpt.p_IdCbteVta.Value = Parametros[3];
                            }
                            #endregion


                            rpt.usuario = "SRVFIX";
                            rpt.empresa = Empresa.em_nombre;
                            rpt.RequestParameters = false;

                            rpt.ExportToPdf(mem);

                            // Create a new attachment and put the PDF report into it.
                            mem.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att = new Attachment(mem, "AVISO DE PAGO.pdf", "application/pdf");
                            mail.Attachments.Add(att);

                            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(CorreoInfo.Cuerpo, null, "text/html");
                            mail.AlternateViews.Add(htmlView);
                            #endregion

                            break;
                        case "CXC_011":

                            #region CXC_011
                            CXC_011_Rpt rpt_CXC_011 = new CXC_011_Rpt();

                            #region Cargo diseño desde base
                            var reporte_CXC_011 = bus_rep_x_emp.GetInfo(CorreoInfo.IdEmpresa, "CXC_011");
                            if (reporte_CXC_011 != null)
                            {
                                System.IO.File.WriteAllBytes(RootReporte, reporte_CXC_011.ReporteDisenio);
                                rpt_CXC_011.LoadLayout(RootReporte);
                            }
                            #endregion

                            #region Parametros
                            if (!string.IsNullOrEmpty(CorreoInfo.Parametros))
                            {
                                string[] Parametros = CorreoInfo.Parametros.Split(';');
                                rpt_CXC_011.p_IdEmpresa.Value = Parametros[0];
                                rpt_CXC_011.p_IdSede.Value = Parametros[1];
                                rpt_CXC_011.p_IdAlumno.Value = Parametros[2];
                            }
                            #endregion


                            rpt_CXC_011.usuario = "SRVFIX";
                            rpt_CXC_011.empresa = Empresa.em_nombre;
                            rpt_CXC_011.RequestParameters = false;

                            rpt_CXC_011.ExportToPdf(mem);

                            // Create a new attachment and put the PDF report into it.
                            mem.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att_CXC_011 = new Attachment(mem, "ESTADO DE CUENTA.pdf", "application/pdf");
                            mail.Attachments.Add(att_CXC_011);

                            AlternateView htmlView_CXC_011 = AlternateView.CreateAlternateViewFromString(CorreoInfo.Cuerpo, null, "text/html");
                            mail.AlternateViews.Add(htmlView_CXC_011);
                            #endregion

                            break;
                        case "CXC_013":

                            #region CXC_013
                            CXC_013_Rpt rpt_CXC_013 = new CXC_013_Rpt();

                            #region Cargo diseño desde base
                            var reporte_CXC_013 = bus_rep_x_emp.GetInfo(CorreoInfo.IdEmpresa, "CXC_013");
                            if (reporte_CXC_013 != null)
                            {
                                System.IO.File.WriteAllBytes(RootReporte, reporte_CXC_013.ReporteDisenio);
                                rpt_CXC_013.LoadLayout(RootReporte);
                            }
                            #endregion

                            #region Parametros
                            if (!string.IsNullOrEmpty(CorreoInfo.Parametros))
                            {
                                string[] Parametros = CorreoInfo.Parametros.Split(';');
                                rpt_CXC_013.p_IdEmpresa.Value = Parametros[0];
                                rpt_CXC_013.p_IdAlumno.Value = Parametros[2];
                            }
                            #endregion


                            rpt_CXC_013.usuario = "SRVFIX";
                            rpt_CXC_013.empresa = Empresa.em_nombre;
                            rpt_CXC_013.RequestParameters = false;

                            rpt_CXC_013.ExportToPdf(mem);

                            // Create a new attachment and put the PDF report into it.
                            mem.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att_CXC_013 = new Attachment(mem, "ESTADO DE CUENTA.pdf", "application/pdf");
                            mail.Attachments.Add(att_CXC_013);

                            AlternateView htmlView_CXC_013 = AlternateView.CreateAlternateViewFromString(CorreoInfo.Cuerpo, null, "text/html");
                            mail.AlternateViews.Add(htmlView_CXC_013);
                            #endregion

                            break;
                        case "CXC_014":

                            #region CXC_014
                            CXC_014_Rpt rpt_CXC_014 = new CXC_014_Rpt();

                            #region Cargo diseño desde base
                            var reporte_CXC_014 = bus_rep_x_emp.GetInfo(CorreoInfo.IdEmpresa, "CXC_014");
                            if (reporte_CXC_014 != null)
                            {
                                System.IO.File.WriteAllBytes(RootReporte, reporte_CXC_014.ReporteDisenio);
                                rpt_CXC_014.LoadLayout(RootReporte);
                            }
                            #endregion

                            #region Parametros
                            if (!string.IsNullOrEmpty(CorreoInfo.Parametros))
                            {
                                string[] Parametros = CorreoInfo.Parametros.Split(';');
                                rpt_CXC_014.p_IdEmpresa.Value = Parametros[0];
                                rpt_CXC_014.p_IdAlumno.Value = Parametros[2];
                            }
                            #endregion


                            rpt_CXC_014.usuario = "SRVFIX";
                            rpt_CXC_014.empresa = Empresa.em_nombre;
                            rpt_CXC_014.RequestParameters = false;

                            rpt_CXC_014.ExportToPdf(mem);

                            // Create a new attachment and put the PDF report into it.
                            mem.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att_CXC_014 = new Attachment(mem, "ESTADO DE CUENTA.pdf", "application/pdf");
                            mail.Attachments.Add(att_CXC_014);

                            AlternateView htmlView_CXC_014 = AlternateView.CreateAlternateViewFromString(CorreoInfo.Cuerpo, null, "text/html");
                            mail.AlternateViews.Add(htmlView_CXC_014);
                            #endregion

                            break;
                        case "CXC_015":

                            #region CXC_015
                            CXC_015_Rpt rpt_CXC_015 = new CXC_015_Rpt();

                            #region Cargo diseño desde base
                            var reporte_CXC_015 = bus_rep_x_emp.GetInfo(CorreoInfo.IdEmpresa, "CXC_015");
                            if (reporte_CXC_015 != null)
                            {
                                System.IO.File.WriteAllBytes(RootReporte, reporte_CXC_015.ReporteDisenio);
                                rpt_CXC_015.LoadLayout(RootReporte);
                            }
                            #endregion

                            #region Parametros
                            if (!string.IsNullOrEmpty(CorreoInfo.Parametros))
                            {
                                string[] Parametros = CorreoInfo.Parametros.Split(';');
                                rpt_CXC_015.p_IdEmpresa.Value = Parametros[0];
                                rpt_CXC_015.p_IdAlumno.Value = Parametros[2];
                            }
                            #endregion


                            rpt_CXC_015.usuario = "SRVFIX";
                            rpt_CXC_015.empresa = Empresa.em_nombre;
                            rpt_CXC_015.RequestParameters = false;

                            rpt_CXC_015.ExportToPdf(mem);

                            // Create a new attachment and put the PDF report into it.
                            mem.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att_CXC_015 = new Attachment(mem, "ESTADO DE CUENTA.pdf", "application/pdf");
                            mail.Attachments.Add(att_CXC_015);

                            AlternateView htmlView_CXC_015 = AlternateView.CreateAlternateViewFromString(CorreoInfo.Cuerpo, null, "text/html");
                            mail.AlternateViews.Add(htmlView_CXC_015);
                            #endregion

                            break;
                        default:
                            mail.Body = CorreoInfo.Cuerpo;
                            break;
                    }
                    
                }
                catch (Exception ex)
                {
                    CorreoInfo.Error = ex.Message;
                    busCorreo.ModificarDB(CorreoInfo);
                }
                #endregion

                #region smtp
                try
                {
                    
                    SmtpClient smtp = new SmtpClient();
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = CorreoInfo.ParametroInfo.Host;
                    smtp.EnableSsl = CorreoInfo.ParametroInfo.PermitirSSL;
                    smtp.Port = CorreoInfo.ParametroInfo.Puerto;
                    smtp.Credentials = new NetworkCredential(CorreoInfo.ParametroInfo.Usuario, CorreoInfo.ParametroInfo.Contrasenia);
                    smtp.Send(mail);
                    
                }
                catch (Exception ex)
                {
                    CorreoInfo.Error = ex.Message;
                    busCorreo.ModificarDB(CorreoInfo);
                }
                #endregion

                mem.Close();
                mem.Flush();
            }
            catch (Exception ex)
            {
                
            }
        }

        protected override void OnStop()
        {
        }
    }
}
