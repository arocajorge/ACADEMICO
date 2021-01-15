using Core.Bus.General;
using Core.Info.General;
using Core.Web.Reportes.CuentasPorCobrar;
using Core.Web.Reportes.Facturacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.TestServicios
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                tb_ColaCorreo_Bus busCorreo = new tb_ColaCorreo_Bus();
                tb_ColaCorreoParametros_Bus busCorreoParam = new tb_ColaCorreoParametros_Bus();
                tb_sis_reporte_x_tb_empresa_Bus bus_rep_x_emp = new tb_sis_reporte_x_tb_empresa_Bus();
                tb_empresa_Bus busEmpresa = new tb_empresa_Bus();

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
                        case "CXC_002":

                            #region CXC_002
                            CXC_002_Rpt rpt_CXC_002 = new CXC_002_Rpt();

                            #region Parametros
                            if (!string.IsNullOrEmpty(CorreoInfo.Parametros))
                            {
                                string[] Parametros = CorreoInfo.Parametros.Split(';');
                                rpt_CXC_002.p_IdEmpresa.Value = Parametros[0];
                                rpt_CXC_002.p_IdSucursal.Value = Parametros[1];
                                rpt_CXC_002.p_IdCobro.Value = Parametros[2];
                            }
                            #endregion


                            rpt_CXC_002.usuario = "SRVFIX";
                            rpt_CXC_002.empresa = Empresa.em_nombre;
                            rpt_CXC_002.RequestParameters = false;

                            rpt_CXC_002.ExportToPdf(mem);

                            // Create a new attachment and put the PDF report into it.
                            mem.Seek(0, System.IO.SeekOrigin.Begin);
                            Attachment att_CXC_002 = new Attachment(mem, "RECIBO DE  CAJA.pdf", "application/pdf");
                            mail.Attachments.Add(att_CXC_002);

                            AlternateView htmlView_CXC_002 = AlternateView.CreateAlternateViewFromString(CorreoInfo.Cuerpo, null, "text/html");
                            mail.AlternateViews.Add(htmlView_CXC_002);
                            #endregion

                            break;
                        case "CXC_011":

                            #region CXC_011
                            CXC_011_Rpt rpt_CXC_011 = new CXC_011_Rpt();

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
                tb_LogError_Bus LogData = new tb_LogError_Bus();
                LogData.GuardarDB(new tb_LogError_Info { Descripcion = ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.InnerException.Message, Clase = "srvColaCorreo", Metodo = "Serv_ColaCorreo", IdUsuario = "Server" });
            }
        }
    }
}