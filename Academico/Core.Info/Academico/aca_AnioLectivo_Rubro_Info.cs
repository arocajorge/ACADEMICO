using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AnioLectivo_Rubro_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo año lectivo es obligatorio")]
        public int IdAnio { get; set; }  
        public int IdRubro { get; set; }
        public bool AplicaProntoPago { get; set; }
        [Required(ErrorMessage = "El campo rubro es obligatorio")]
        public string NomRubro { get; set; }
        [Required(ErrorMessage = "El campo producto es obligatorio")]
        public decimal IdProducto { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Subtotal { get; set; }
        public string IdCod_Impuesto_Iva { get; set; }
        public decimal Porcentaje { get; set; }
        public decimal ValorIVA { get; set; }
        public decimal Total { get; set; }
        public int NumeroCuotas { get; set; }

        #region Campos que no existen en la tabla
        public string IdString { get; set; }
        public string Descripcion { get; set; }
        public List<aca_AnioLectivo_Rubro_Periodo_Info> lst_rubro_anio_periodo { get; set; }
        #endregion
    }
}
