using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_parametro_Info
    {
        public int IdEmpresa { get; set; }
        public string RutaImagen_Alumno { get; set; }
        public string RutaImagen_Profesor { get; set; }
        public string RutaImagen_Seguimiento { get; set; }
        public string FtpUser { get; set; }
        public string FtpPassword { get; set; }
        public string FtpUrl { get; set; }
    }
}
