using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_CatalogoFicha_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdCatalogoFicha { get; set; }
        public int IdCatalogoTipoFicha { get; set; }
        public string Codigo { get; set; }
        public string NomCatalogoFicha { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        public string MotivoAnulacion { get; set; }
    }
}
