using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Paralelo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdParalelo { get; set; }
        [StringLength(10, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 10")]
        [Required(ErrorMessage = "El campo código es obligatorio")]
        public string CodigoParalelo { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo paralelo debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo paralelo es obligatorio")]
        public string NomParalelo { get; set; }
        [Required(ErrorMessage = "El campo orden es obligatorio")]
        public int OrdenParalelo { get; set; }
        public bool Estado { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string IdUsuarioAnulacion { get; set; }
        public Nullable<System.DateTime> FechaAnulacion { get; set; }
        [Required(ErrorMessage = "El campo motivo de anulación es obligatorio")]
        public string MotivoAnulacion { get; set; }
    }
}
