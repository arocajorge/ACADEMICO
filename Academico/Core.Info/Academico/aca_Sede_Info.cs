using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Info.Academico
{
    public class aca_Sede_Info
    {
        public decimal IdTransaccionSession { get; set; }
        public int IdEmpresa { get; set; }
        public int IdSede { get; set; }
        [Required(ErrorMessage = "El campo sucursal es obligatorio")]
        public int IdSucursal { get; set; }
        [StringLength(500, MinimumLength = 1, ErrorMessage = "el campo nombre de sede debe tener mínimo 1 caracter y máximo 500")]
        [Required(ErrorMessage = "El campo nombre de sede es obligatorio")]
        public string NomSede { get; set; }
        public string Direccion { get; set; }
        public string NombreRector { get; set; }
        public string TelefonoRector { get; set; }
        public string CelularRector { get; set; }
        public string CorreoRector { get; set; }
        public string NombreSecretaria { get; set; }
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
