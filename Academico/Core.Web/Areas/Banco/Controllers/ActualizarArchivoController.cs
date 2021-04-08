using Core.Info.Banco;
using Core.Web.Helps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Bus.Banco;
using Core.Bus.General;
using Core.Info.General;
using ExcelDataReader;
using System.IO;
using DevExpress.Web.Mvc;

namespace Core.Web.Areas.Banco.Controllers
{
    public class ActualizarArchivoController : Controller
    {
        ba_ArchivoRecaudacionDet_List Lista = new ba_ArchivoRecaudacionDet_List();
        public ActionResult Importar()
        {
            #region Validar Session
            if (string.IsNullOrEmpty(SessionFixed.IdTransaccionSession))
                return RedirectToAction("Login", new { Area = "", Controller = "Account" });
            SessionFixed.IdTransaccionSession = (Convert.ToDecimal(SessionFixed.IdTransaccionSession) + 1).ToString();
            SessionFixed.IdTransaccionSessionActual = SessionFixed.IdTransaccionSession;
            #endregion

            ba_Archivo_Transferencia_Info model = new ba_Archivo_Transferencia_Info
            {
                IdSucursal = Convert.ToInt32(SessionFixed.IdSucursal),
                IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa),
                IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSession)
            };
            return View(model);
        }
        public ActionResult UploadControlUpload()
        {
            UploadControlExtension.GetUploadedFiles("UploadControlFile", UploadControlSettings.UploadValidationSettings, UploadControlSettings.FileUploadComplete);
            return null;
        }
        public ActionResult GridViewPartial_Importacion()
        {
            SessionFixed.IdTransaccionSessionActual = Request.Params["TransaccionFixed"] != null ? Request.Params["TransaccionFixed"].ToString() : SessionFixed.IdTransaccionSessionActual;
            var model = Lista.get_list(Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual));
            return PartialView("_GridViewPartial_Importacion", model);
        }
        public ActionResult DescargarArchivo(decimal IdTransaccion = 0)
        {
            string fileName = Guid.NewGuid() + ".txt";
            string filePath = "~/Content/UploadedFiles/Reports/" + fileName;
            StreamWriter sw = new StreamWriter(Server.MapPath(filePath));
            sw.WriteLine("9990940           "+new DateTime(DateTime.Now.Date.Year,DateTime.Now.Month,1).ToString("MM/dd/yyyy"));
            var lst = Lista.get_list(IdTransaccion);
            foreach (var item in lst)
            {
                decimal valorEntero = Math.Floor(item.Saldo);
                decimal valorDecimal = Convert.ToDecimal((item.Saldo - valorEntero).ToString("N2")) * 100;
                string ValorS = valorEntero.ToString("n0").Replace(",","").PadLeft(8, '0') + "." + valorDecimal.ToString("n0").Replace(",", "").PadRight(2, '0');
                sw.WriteLine("094"
                    + item.CodigoAlumno.PadRight(15, ' ')
                    + Convert.ToDateTime(item.Fecha).ToString("MM/dd/yyyy")
                    + "0  "
                    + ValorS
                    //+ new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Date.Year, DateTime.Now.Month)).ToString("MM/dd/yyyy")
                    + Convert.ToDateTime(item.FechaFin).ToString("MM/dd/yyyy")
                    + "01/01/1900"
                    + "N"
                    + (item.pe_nombreCompleto.Trim().Length > 30 ? item.pe_nombreCompleto.Trim().Substring(0, 30) : item.pe_nombreCompleto.Trim().PadRight(30, ' '))
                    + ("").PadRight(15, ' ')
                    + ("").PadRight(3, ' ')
                    + ("").PadRight(15, ' ')
                    + ValorS
                    + ("").PadRight(10, ' ')
                    +"1"
                    + ValorS
                    + ValorS
                    + ("").PadRight(6, ' ')
                    + ("").PadRight(2, ' ')
                    + ("").PadRight(30, ' ')
                    + ("").PadRight(15, ' ')
                    + ("").PadRight(14, ' ')
                    + ("").PadRight(30, ' ')
                    +"E"
                    );

            }
            sw.Flush();
            sw.Dispose();
            sw.Close();
            return File(filePath,"text/txt","ACTALUMNOS094.TXT");
        }

    }

    public class UploadControlSettings
    {
        
        
        public static DevExpress.Web.UploadControlValidationSettings UploadValidationSettings = new DevExpress.Web.UploadControlValidationSettings()
        {
            AllowedFileExtensions = new string[] { ".xlsx" },
            MaxFileSize = 40000000
        };
        public static void FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            #region Variables
            int cont = 0;
            decimal IdTransaccionSession = Convert.ToDecimal(SessionFixed.IdTransaccionSessionActual);
            int IdEmpresa = Convert.ToInt32(SessionFixed.IdEmpresa);
            ba_ArchivoRecaudacionDet_List ListaDet = new ba_ArchivoRecaudacionDet_List();
            ba_ArchivoRecaudacionDet_Bus busRecaudacion = new ba_ArchivoRecaudacionDet_Bus();
            List<ba_ArchivoRecaudacionDet_Info> ListDet = new List<ba_ArchivoRecaudacionDet_Info>();
            #endregion

            Stream stream = new MemoryStream(e.UploadedFile.FileBytes);
            if (stream.Length > 0)
            {
                IExcelDataReader reader = null;
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                #region Alumno
                var no_validas = "";
                var repetidos = "";
                string Codigos = "";
                while (reader.Read())
                {
                    if (!reader.IsDBNull(0) && cont > 0)
                    {
                        ListDet.Add(new ba_ArchivoRecaudacionDet_Info
                        {
                            CodigoAlumno = reader[1].ToString(),
                            pe_nombreCompleto = reader[2].ToString(),
                            FechaProceso = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Month, 1),
                            Fecha = Convert.ToDateTime(reader[3]),
                            FechaFin = Convert.ToDateTime(reader[4]),
                            FechaProntoPago = Convert.ToDateTime(reader[11]),
                            Saldo = Convert.ToDecimal(reader[6]),
                            SaldoProntoPago = Convert.ToDecimal(reader[10])

                        });
                        Codigos += (string.IsNullOrEmpty(Codigos)) ? ("'"+reader[1].ToString()+"'") : ("," + ("'"+reader[1].ToString()+"'"));
                        
                        
                                  
                    }
                    else
                        cont++;
                }

                var Alumno = busRecaudacion.GetList(IdEmpresa, Codigos);
                ListDet = (from a in ListDet
                           join b in Alumno
                           on a.CodigoAlumno equals b.CodigoAlumno into gj
                           from c in gj.DefaultIfEmpty()
                           select new ba_ArchivoRecaudacionDet_Info
                           {
                               CodigoAlumno = a.CodigoAlumno,
                               pe_nombreCompleto = a.pe_nombreCompleto,
                               FechaProceso = a.FechaProceso,
                               IdAlumno = c?.IdAlumno ?? 0,
                               FechaProntoPago = a?.FechaProntoPago,
                               Fecha = a.Fecha,
                               FechaFin = a.FechaFin,
                               Saldo = a?.Saldo ?? 0,
                               SaldoProntoPago = a?.SaldoProntoPago ?? 0
                           }).ToList();

                

                no_validas = " " + no_validas;
                repetidos = " " + repetidos;
                ListaDet.set_list(ListDet, IdTransaccionSession);
                #endregion
            }
        }
    }
}