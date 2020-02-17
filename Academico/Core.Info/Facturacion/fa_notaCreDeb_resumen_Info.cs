using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Facturacion
{
    public class fa_notaCreDeb_resumen_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public decimal SubtotalIVASinDscto { get; set; }
        public decimal SubtotalSinIVASinDscto { get; set; }
        public decimal SubtotalSinDscto { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubtotalIVAConDscto { get; set; }
        public decimal SubtotalSinIVAConDscto { get; set; }
        public decimal SubtotalConDscto { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
        public Nullable<decimal> PorIva { get; set; }
        public string IdCod_Impuesto_IVA { get; set; }
        public Nullable<int> IdAnio { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
    }
}
