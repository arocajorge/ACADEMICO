using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Facturacion
{
    public class fa_notaCreDeb_MasivaDet_Info
    {
        public int IdEmpresa { get; set; }
        public decimal IdNCMasivo { get; set; }
        public int Secuencia { get; set; }
        public decimal IdAlumno { get; set; }
        public double Subtotal { get; set; }
        public double IVA { get; set; }
        public double vt_por_iva { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public double Total { get; set; }
        public string ObservacionDet { get; set; }
        public Nullable<int> IdSucursal { get; set; }
        public Nullable<int> IdBodega { get; set; }
        public Nullable<decimal> IdNota { get; set; }
    }
}
