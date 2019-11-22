using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Facturacion
{
    public class fa_TerminoPago_Info
    {
        public string IdTerminoPago { get; set; }
        public string nom_TerminoPago { get; set; }
        public int Num_Coutas { get; set; }
        public int Dias_Vct { get; set; }
        public bool estado { get; set; }
        public Nullable<bool> AplicaDescuentoNomina { get; set; }
        public string CodigoRubroDescto { get; set; }
    }
}
