using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_ConciliacionNotaCredito_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdConciliacion { get; set; }
        [Required(ErrorMessage ="El campo alumno es obligatorio")]
        public decimal IdAlumno { get; set; }
        public int IdSucursal { get; set; }
        public int IdBodega { get; set; }
        public decimal IdNota { get; set; }
        public decimal IdCobro { get; set; }
        public Nullable<decimal> IdAplicacion { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        public double Valor { get; set; }
        public string Observacion { get; set; }
        public bool Estado { get; set; }
        public Nullable<int> IdTipoCbte { get; set; }
        public Nullable<decimal> IdCbteCble { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string Referencia { get; set; }
        public string pe_nombreCompleto { get; set; }
        public string Codigo { get; set; }
        public List<cxc_ConciliacionNotaCreditoDet_Info> ListaDet { get; set; }
        public string IdString { get; set; }
        #endregion
    }
}
