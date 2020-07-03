using Core.Bus.General;
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
                try
                {

                }
                catch (Exception ex)
                {
                    CorreoInfo.Error = ex.Message;
                    busCorreo.ModificarDB(CorreoInfo);
                }


                #region Armar correo
                MailMessage mail = new MailMessage();
                try
                {
                    
                    mail.From = new MailAddress(CorreoInfo.ParametroInfo.Usuario);
                    mail.Subject = CorreoInfo.Asunto;
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
                                rpt.p_IdBodega.Value = Parametros[1];
                                rpt.p_IdSucursal.Value = Parametros[2];
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
