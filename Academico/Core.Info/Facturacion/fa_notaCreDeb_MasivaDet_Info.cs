using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "El campo alumno es obligatorio")]
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

        [Required(ErrorMessage = "El campo cliente es obligatorio")]
        public decimal IdCliente { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Codigo { get; set; }
        public bool TieneCliente { get; set; }
    }
}
