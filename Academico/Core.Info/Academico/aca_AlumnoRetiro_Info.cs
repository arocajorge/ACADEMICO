using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_AlumnoRetiro_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public decimal IdRetiro { get; set; }
        [Required(ErrorMessage = "El campo alumno es obligatorio")]
        public decimal IdAlumno { get; set; }
        [Required(ErrorMessage = "El campo fecha es obligatorio")]
        public System.DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El campo observación es obligatorio")]
        public string Observacion { get; set; }
        public int IdCatalogoESTALU { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }

        #region Campos que no existen en la tabla
        public string pe_cedulaRuc { get; set; }
        public string pe_nombreCompleto { get; set; }
        #endregion
    }
}
