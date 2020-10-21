using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Contabilidad
{
    public class CONTA_002_Info
    {
        public string Tipo { get; set; }
        public string Referencia { get; set; }
        public int IdEmpresa { get; set; }
        public int? IdTipoCbte { get; set; }
        public decimal? IdCbteCble { get; set; }
        public int? Secuencia { get; set; }
        public string IdCtaCble { get; set; }
        public string pc_Cuenta { get; set; }
        public string dc_Observacion { get; set; }
        public DateTime cb_Fecha { get; set; }
        public string cb_Observacion { get; set; }
        public string Su_Descripcion { get; set; }
        public double dc_ValorDebe { get; set; }
        public double dc_ValorHaber { get; set; }
    }
}
