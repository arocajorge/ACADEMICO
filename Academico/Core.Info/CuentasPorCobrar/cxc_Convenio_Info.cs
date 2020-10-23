using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.CuentasPorCobrar
{
    public class cxc_Convenio_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdConvenio { get; set; }
        [Required(ErrorMessage = "El campo estudiante es obligatorio")]
        public decimal IdAlumno { get; set; }
        public Nullable<decimal> IdMatricula { get; set; }
        [Required(ErrorMessage = "El campo persona es obligatorio")]
        public decimal IdPersonaConvenio { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo fecha primer pago es obligatorio")]
        public System.DateTime FechaPrimerPago { get; set; }
        [Required(ErrorMessage = "El campo valor es obligatorio")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "El campo numero de cuotas es obligatorio")]
        public int NumCuotas { get; set; }
        public bool Estado { get; set; }
        public string Observacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public List<cxc_Convenio_Det_Info> lst_detalle { get; set; }
        public string PersonaConvenio { get; set; }
        public string Alumno { get; set; }
        #endregion
    }
}
