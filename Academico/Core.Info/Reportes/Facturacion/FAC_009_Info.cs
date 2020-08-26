using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Facturacion
{
    public class FAC_009_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public int IdSucursal_fac_nd_doc_mod { get; set; }
        public int IdBodega_fac_nd_doc_mod { get; set; }
        public decimal IdCbteVta_fac_nd_doc_mod { get; set; }
        public string vt_tipoDoc { get; set; }
        public System.DateTime no_fecha { get; set; }
        public string sc_observacion { get; set; }
        public string vt_Observacion { get; set; }
        public int IdTipoNota { get; set; }
        public string No_Descripcion { get; set; }
        public System.DateTime fecha_cruce { get; set; }
        public decimal Total { get; set; }
        public double Valor_Aplicado { get; set; }
        public string Tipo { get; set; }
        public string vt_NumFactura { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string CodigoAlumno { get; set; }
        public Nullable<System.DateTime> vt_fecha { get; set; }
        public Nullable<decimal> IdConciliacion { get; set; }
    }
}
