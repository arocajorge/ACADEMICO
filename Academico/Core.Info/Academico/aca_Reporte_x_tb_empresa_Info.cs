using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Reporte_x_tb_empresa_Info
    {
        public int IdEmpresa { get; set; }
        public string CodReporte { get; set; }
        public byte[] ReporteDisenio { get; set; }
        public string Nom_Carpeta { get; set; }
        public string Reporte { get; set; }
    }
}
