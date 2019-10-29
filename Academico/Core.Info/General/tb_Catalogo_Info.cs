using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.General
{
    public class tb_Catalogo_Info
    {
        public string CodCatalogo { get; set; }
        public int IdTipoCatalogo { get; set; }
        public int IdCatalogo { get; set; }
        public string ca_descripcion { get; set; }
        public string ca_estado { get; set; }
        public int ca_orden { get; set; }
        public string IdUsuario { get; set; }
        public Nullable<System.DateTime> Fecha_Transaccion { get; set; }
        public string IdUsuarioUltModi { get; set; }
        public Nullable<System.DateTime> Fecha_UltMod { get; set; }
        public string IdUsuarioUltAnu { get; set; }
        public Nullable<System.DateTime> Fecha_UltAnu { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
