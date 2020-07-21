using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.General
{
    public class tb_ColaCorreoCodigo_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "El campo código es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 50")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "El campo asunto es obligatorio")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "el campo código debe tener mínimo 1 caracter y máximo 1000")]
        public string Asunto { get; set; }
        [Required(ErrorMessage = "El campo cuerpo es obligatorio")]
        public string Cuerpo { get; set; }
        public Nullable<bool> ApareceSeguimientoCobranza { get; set; }
    }
}
