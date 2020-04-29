using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "El campo descripción es obligatorio")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo código es obligatorio")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo valor mínimo es obligatorio")]
        public decimal ValorMinimo { get; set; }
        [Required(ErrorMessage = "El campo valor máximo es obligatorio")]
        public decimal ValorMaximo { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string DescripcionAnio { get; set; }
        #endregion
    }
}
