using Core.Bus.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
                mail.Body = CorreoInfo.Cuerpo;
                #endregion

                #region smtp
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = CorreoInfo.ParametroInfo.Host;
                smtp.EnableSsl = CorreoInfo.ParametroInfo.PermitirSSL;
                smtp.Port = CorreoInfo.ParametroInfo.Puerto;
                smtp.Credentials = new NetworkCredential(CorreoInfo.ParametroInfo.Usuario, CorreoInfo.ParametroInfo.Contrasenia);
                smtp.Send(mail);
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnStop()
        {
        }
    }
}
