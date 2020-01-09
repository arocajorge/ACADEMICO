using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Info.Facturacion
{
    public class fa_factura_det_Info
    {
        public int IdEmpresa { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdCbteVta { get; set; }
        public int Secuencia { get; set; }
        public Nullable<int> aca_IdPeriodo { get; set; }
        public Nullable<int> aca_IdRubro { get; set; }
        [Required(ErrorMessage ="El campo producto es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo cantidad es obligatorio")]
        public decimal IdProducto { get; set; }
        [Required(ErrorMessage = "El campo cantidad es obligatorio")]
        [Range(1,double.MaxValue,ErrorMessage ="El campo cantidad es obligatorio")]
        public double vt_cantidad { get; set; }
        public double vt_Precio { get; set; }
        public double vt_PorDescUnitario { get; set; }
        public double vt_DescUnitario { get; set; }
        public double vt_PrecioFinal { get; set; }
        public double vt_Subtotal { get; set; }
        public double vt_iva { get; set; }
        public double vt_total { get; set; }
        public string vt_detallexItems { get; set; }
        public double vt_por_iva { get; set; }
        public Nullable<int> IdPunto_Cargo { get; set; }
        public Nullable<int> IdPunto_cargo_grupo { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public string IdCentroCosto { get; set; }
        public Nullable<int> IdEmpresa_pf { get; set; }
        public Nullable<int> IdSucursal_pf { get; set; }
        public Nullable<decimal> IdProforma { get; set; }
        public Nullable<int> Secuencia_pf { get; set; }
        
        #region Campos que no existen en la tabla
        public string pr_descripcion { get; set; }
        public string nom_presentacion { get; set; }
        public string lote_num_lote { get; set; }
        public DateTime? lote_fecha_vcto { get; set; }
        public string secuencial { get; set; }
        public string tp_manejaInven { get; set; }
        public double CantidadAnterior { get; set; }
        public bool? se_distribuye { get; set; }
        public string IdString { get; set; }
        public decimal IdMatricula { get; set; }
        public bool AplicaProntoPago { get; set; }
        public int IdAnio { get; set; }
        public decimal DescuentoProntoPago { get; set; }
        public decimal TotalProntoPago { get; set; }
        public bool EnMatricula { get; set; }
        public System.DateTime FechaDesde { get; set; }
        public string NumPeriodo { get; set; }
        public string Periodo { get; set; }
        public DateTime? FechaProntoPago { get; set; }
        #endregion

    }
}
