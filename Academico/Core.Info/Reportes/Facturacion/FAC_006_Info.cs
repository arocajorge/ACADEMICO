using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Reportes.Facturacion
{
  public class FAC_006_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public string vt_NumFactura { get; set; }
        public Nullable<decimal> IdAlumno { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string NombreFormaPago { get; set; }
        public string IdCatalogo_FormaPago { get; set; }
        public string Estado { get; set; }
        public System.DateTime vt_fecha { get; set; }
        public string Ve_Vendedor { get; set; }
        public string IdVendedor { get; set; }
        public string Su_Descripcion { get; set; }
        public string Su_Telefonos { get; set; }
        public string Su_Direccion { get; set; }
        public string Su_Ruc { get; set; }
        public Nullable<decimal> SubtotalIVAConDscto { get; set; }
        public Nullable<decimal> SubtotalSinIVAConDscto { get; set; }
        public Nullable<decimal> ValorIVA { get; set; }
        public Nullable<decimal> Total { get; set; }
        public int FacturasAnuladas { get; set; }
        public string nom_FormaPago { get; set; }
        public string pe_cedulaRuc { get; set; }
        public string vt_Observacion { get; set; }
        public string Tarifa { get; set; }
        public Nullable<int> IdJornada { get; set; }
        public Nullable<int> OrdenJornada { get; set; }
        public string NomJornada { get; set; }
        public Nullable<int> IdNivel { get; set; }
        public Nullable<int> OrdenNivel { get; set; }
        public string NomNivel { get; set; }
        public int Cantidad { get; set; }
        public string NomRubro { get; set; }
        public Nullable<int> idMes { get; set; }
        public string smes { get; set; }
        public string NomRuboDetalle { get; set; }
        public Nullable<int> IdPeriodo { get; set; }
    }
}
