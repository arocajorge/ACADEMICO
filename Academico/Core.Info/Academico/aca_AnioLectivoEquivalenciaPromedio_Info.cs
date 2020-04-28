using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivoEquivalenciaPromedio_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdEquivalenciaPromedio { get; set; }
        public int IdAnio { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
    }
}
